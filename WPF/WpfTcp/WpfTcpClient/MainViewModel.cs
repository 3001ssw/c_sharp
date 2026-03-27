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
        private CancellationTokenSource? _cs = null;

        private TcpClient? chatClient = null;
        public TcpClient? ChatClient { get => chatClient; set => SetProperty(ref chatClient, value); }


        private string serverIP = "127.0.0.1";
        public string ServerIP { get => serverIP; set => SetProperty(ref serverIP, value); }

        private int serverPort = 7000;
        public int ServerPort { get => serverPort; set => SetProperty(ref serverPort, value); }

        public ObservableCollection<ChatMessage> ChatMessages { get; set; } = new ObservableCollection<ChatMessage>();
        private object _lockChatMessages = new object();

        private string sendMessage = "";
        public string SendMessage { get => sendMessage; set => SetProperty(ref sendMessage, value); }

        #endregion

        public DelegateCommand ConnectServerCommand { get; private set; }
        public DelegateCommand DisconnectServerCommand { get; private set; }
        public DelegateCommand SendMessageCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }


        public MainViewModel()
        {
            ConnectServerCommand = new DelegateCommand(OnConnectServer, CanConnectServer).ObservesProperty(() => ChatClient);
            DisconnectServerCommand = new DelegateCommand(OnDisconnectServer, CanDisconnectServer).ObservesProperty(() => ChatClient);
            SendMessageCommand = new DelegateCommand(OnSendMessage, CanSendMessage).ObservesProperty(() => ChatClient).ObservesProperty(() => SendMessage);
            CloseCommand = new DelegateCommand(OnClose, CanClose);

            BindingOperations.EnableCollectionSynchronization(ChatMessages, _lockChatMessages);
        }


        private void OnConnectServer()
        {
            _cs = new CancellationTokenSource();
            Task.Run(() => ChatClientTask(_cs.Token));
        }

        private bool CanConnectServer()
        {
            if (ChatClient != null)
                return false;

            return true;
        }

        private void OnDisconnectServer()
        {
            _cs?.Cancel();
        }

        private bool CanDisconnectServer()
        {
            if (ChatClient == null)
                return false;

            return true;
        }

        private void OnSendMessage()
        {
            if (ChatClient == null)
                return;

            NetworkStream stream = ChatClient.GetStream();
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
            if (ChatClient == null)
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


        private void ChatClientTask(CancellationToken token)
        {
            try
            {
                ChatClient = new TcpClient();
                ChatClient.Connect(hostname: ServerIP, port: ServerPort);

                while (!token.IsCancellationRequested)
                {
                    if (0 < ChatClient.Available)
                    {
                        byte[] receiveData = new byte[4096];
                        NetworkStream stream = ChatClient.GetStream();
                        int bytesRead = stream.Read(receiveData, 0, receiveData.Length);
                        if (0 < bytesRead)
                        {
                            if (ChatClient.Client.RemoteEndPoint is IPEndPoint endPoint)
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
                        if (ChatClient.Client.Poll(1000, SelectMode.SelectRead))
                        {
                            if (ChatClient.Client.Available == 0)
                            {
                                ChatMessages.Add(new ChatMessage() { Message = "Server Closed" });

                                break;
                            }
                        }
                        //token.WaitHandle.WaitOne(1000); // poll에서 대기하니까 주석 처리
                    }
                }
            }
            catch (Exception ex)
            {
                if (ChatClient.Client.RemoteEndPoint is IPEndPoint endPoint)
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
            finally
            {
                ChatClient?.Dispose();
                ChatClient = null;
            }
        }
    }
}
