using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WpfTcpClient.Chat;

namespace WpfTcpClient
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private TcpClient? _client = null;
        private Thread? _threadReceive = null;
        private CancellationTokenSource? _cancellationTokenSource = null;

        private string _serverIP = "127.0.0.1";
        public string ServerIP { get => _serverIP; set => SetProperty(ref _serverIP, value); }

        private int _serverPort = 7000;
        public int ServerPort { get => _serverPort; set => SetProperty(ref _serverPort, value); }

        public ObservableCollection<ChatMessage> ChatMessages { get; set; } = new ObservableCollection<ChatMessage>();
        private object _lockChatMessages = new object();

        private string _sendMessage = "";
        public string SendMessage { get => _sendMessage; set => SetProperty(ref _sendMessage, value); }


        #endregion

        public DelegateCommand ConnectServerCommand { get; private set; }
        public DelegateCommand DisconnectServerCommand { get; private set; }
        public DelegateCommand SendMessageCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }

        private void OnConnectServer()
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(hostname: ServerIP, port: ServerPort);

                _cancellationTokenSource = new CancellationTokenSource();
                _threadReceive = new Thread(() => ReceiveThread(_cancellationTokenSource.Token));
                _threadReceive.IsBackground = true;
                _threadReceive.Start();
            }
            catch (Exception ex)
            {
                _client?.Dispose();
                _client = null;
            }
            finally
            {
                RaiseAllCanExecuteChanged();
            }
        }

        private bool CanConnectServer()
        {
            if (_client != null)
                return false;

            return true;
        }

        private void OnDisconnectServer()
        {
            _cancellationTokenSource?.Cancel();
            _threadReceive?.Join(3000);
            _client?.Dispose();
            _client = null;
            _threadReceive = null;

            RaiseAllCanExecuteChanged();
        }

        private bool CanDisconnectServer()
        {
            if (_client == null)
                return false;

            return true;
        }

        private void OnSendMessage()
        {
            if (_client == null)
                return;

            NetworkStream stream = _client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(SendMessage);
            stream.Write(data, 0, data.Length);

            ChatMessages.Add(new ChatMessage()
            {
                Message = SendMessage,
            });

            SendMessage = "";
        }

        private bool CanSendMessage()
        {
            if (_client == null)
                return false;

            if (string.IsNullOrEmpty(SendMessage))
                return false;

            return true;
        }

        private void OnClose()
        {

        }

        private bool CanClose()
        {
            return true;
        }


        public MainViewModel()
        {
            ConnectServerCommand = new DelegateCommand(OnConnectServer, CanConnectServer);
            DisconnectServerCommand = new DelegateCommand(OnDisconnectServer, CanDisconnectServer);
            SendMessageCommand = new DelegateCommand(OnSendMessage, CanSendMessage).ObservesProperty(() => SendMessage);
            CloseCommand = new DelegateCommand(OnClose, CanClose);

            BindingOperations.EnableCollectionSynchronization(ChatMessages, _lockChatMessages);

            RaiseAllCanExecuteChanged();
        }

        private void RaiseAllCanExecuteChanged()
        {
            ConnectServerCommand.RaiseCanExecuteChanged();
            DisconnectServerCommand.RaiseCanExecuteChanged();
            SendMessageCommand.RaiseCanExecuteChanged();
            CloseCommand.RaiseCanExecuteChanged();
        }

        private void ReceiveThread(CancellationToken token)
        {
            try
            {
                while (token.IsCancellationRequested == false)
                {
                    if (_client!= null)
                    {
                        if (0 < _client.Available)
                        {
                            byte[] receiveData = new byte[4096];
                            NetworkStream stream = _client.GetStream();
                            int bytesRead = stream.Read(receiveData, 0, receiveData.Length);
                            if (0 < bytesRead)
                            {
                                if (_client.Client.RemoteEndPoint is IPEndPoint endPoint)
                                {
                                    string serverIP = endPoint.Address.MapToIPv4().ToString();
                                    int serverPort = endPoint.Port;
                                    string message = Encoding.UTF8.GetString(receiveData, 0, bytesRead);
                                    ChatMessages.Add(new ChatMessage()
                                    {
                                        IP = serverIP,
                                        Port = serverPort,
                                        Message = message,
                                    });
                                }
                                token.WaitHandle.WaitOne(100);
                            }
                        }
                        else
                        {
                            if (_client.Client.Poll(1000, SelectMode.SelectRead))
                            {
                                if (_client.Client.Available == 0)
                                {
                                    ChatMessages.Add(new ChatMessage() { Message = "Server Closed" });

                                    // 연결 상태 초기화 (UI 커맨드 버튼 상태 변경을 위해)
                                    _client?.Dispose();
                                    _client = null;

                                    // UI 업데이트를 위해 커맨드 상태 갱신 (선택 사항)
                                    RaiseAllCanExecuteChanged();

                                    break; // 루프 탈출
                                }
                            }
                            //token.WaitHandle.WaitOne(1000); // poll에서 대기하니까 주석 처리
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (_client.Client.RemoteEndPoint is IPEndPoint endPoint)
                {
                    string serverIP = endPoint.Address.MapToIPv4().ToString();
                    int serverPort = endPoint.Port;
                    string message = $"Error: {ex.Message}";
                    ChatMessages.Add(new ChatMessage()
                    {
                        IP = serverIP,
                        Port = serverPort,
                        Message = message,
                    });
                }
            }
        }
    }
}
