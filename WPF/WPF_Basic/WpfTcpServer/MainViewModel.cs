using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfTcpServer.Chat;

namespace WpfTcpServer
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private TcpListener? _server = null;
        private Thread? serverAcceptThread = null;
        private CancellationTokenSource? acceptCts = null;


        private int serverPort = 7000;
        public int ServerPort { get => serverPort; set => SetProperty(ref serverPort, value); }


        private ObservableCollection<ClientModel> clients = new ObservableCollection<ClientModel>();
        public ObservableCollection<ClientModel> Clients { get => clients; set => SetProperty(ref clients, value); }
        private object serverLock = new object();

        private ObservableCollection<ChatMessage> chatMessages = new ObservableCollection<ChatMessage>();
        public ObservableCollection<ChatMessage> ChatMessages { get => chatMessages; set => SetProperty(ref chatMessages, value); }
        private object chatMessageLock = new object();

        private string sendMessage = "";
        public string SendMessage { get => sendMessage; set => SetProperty(ref sendMessage, value); }
        #endregion

        #region commands

        public DelegateCommand OpenServerCommand { get; private set; }
        public DelegateCommand CloseServerCommand { get; private set; }
        public DelegateCommand SendMessageCommand { get; private set; }

        private void OnOpenServer()
        {
            if (_server != null)
                return;

            try
            {
                _server = new TcpListener(IPAddress.Any, ServerPort); // 7000번 포트 열기
                _server.Start();

                acceptCts = new CancellationTokenSource();
                serverAcceptThread = new Thread(() => ThreadAccept(acceptCts.Token))
                {
                    IsBackground = true,
                };
                serverAcceptThread.Start();
            }
            catch (Exception ex)
            {
                acceptCts?.Cancel();
                serverAcceptThread?.Join(3000);

                _server?.Dispose();
                _server = null;
                serverAcceptThread = null;
                acceptCts = null;
            }

            AllRaiseCanExecuteChanged();
        }

        private bool CanOpenServer()
        {
            if (_server != null)
                return false;

            return true;
        }

        private void OnCloseServer()
        {
            acceptCts?.Cancel();
            serverAcceptThread?.Join(3000);

            _server?.Stop();
            foreach (var client in Clients)
            {
                client.ClientSocket?.Close();
            }
            _server = null;
            serverAcceptThread = null;
            acceptCts = null;

            AllRaiseCanExecuteChanged();
        }

        private bool CanCloseServer()
        {
            if (_server == null)
                return false;

            return true;
        }

        private void OnSendMessage()
        {
            foreach (ClientModel client in Clients)
            {
                TcpClient clientSocket = client.ClientSocket;

                NetworkStream stream = clientSocket.GetStream();

                byte[] data = Encoding.UTF8.GetBytes(SendMessage);
                stream.Write(data, 0, data.Length);
            }
            ChatMessages.Add(new ChatMessage()
            {
                IP = "All",
                Message = SendMessage,
            });
        }

        private bool CanSendMessage()
        {
            if (_server == null)
                return false;

            if (Clients.Count <= 0)
                return false;

            if (string.IsNullOrEmpty(SendMessage))
                return false;

            return true;
        }

        #endregion


        #region constructor
        public MainViewModel()
        {
            OpenServerCommand = new DelegateCommand(OnOpenServer, CanOpenServer);
            CloseServerCommand = new DelegateCommand(OnCloseServer, CanCloseServer);
            SendMessageCommand = new DelegateCommand(OnSendMessage, CanSendMessage).ObservesProperty(() => SendMessage);

            BindingOperations.EnableCollectionSynchronization(Clients, serverLock);
            BindingOperations.EnableCollectionSynchronization(ChatMessages, chatMessageLock);

            AllRaiseCanExecuteChanged();
        }
        #endregion

        private void AllRaiseCanExecuteChanged()
        {
            OpenServerCommand.RaiseCanExecuteChanged();
            CloseServerCommand.RaiseCanExecuteChanged();
        }

        private void ThreadAccept(CancellationToken token)
        {
            try
            {
                while (token.IsCancellationRequested == false)
                {
                    if (_server != null && _server.Pending())
                    {
                        TcpClient newClient = _server.AcceptTcpClient();
                        ClientModel newClientModel = new ClientModel()
                        {
                            ClientSocket = newClient
                        };
                        Clients.Add(newClientModel);

                        Thread clientThread = new Thread(() => HandleClient(newClientModel, token))
                        {
                            IsBackground = true // 메인 창 닫히면 같이 죽도록 반드시 설정!
                        };
                        clientThread.Start();

                        token.WaitHandle.WaitOne(100);
                    }
                    else
                        token.WaitHandle.WaitOne(1000);
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"취소 요청: {e}");
            }
            catch (Exception e)
            {
            }
        }

        private void HandleClient(ClientModel clientModel, CancellationToken token)
        {
            TcpClient clientSocket = clientModel.ClientSocket;

            try
            {
                NetworkStream stream = clientSocket.GetStream();

                while (token.IsCancellationRequested == false)
                {
                    if (clientSocket == null || !clientSocket.Connected)
                        break;

                    if (0 < clientSocket.Available)
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                            break;

                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        ChatMessages.Add(new ChatMessage()
                        {
                            IP = clientModel.IP,
                            Port = clientModel.Port,
                            Message = message,
                        });
                    }
                    else
                    {
                        if (clientSocket.Client.Poll(1000, SelectMode.SelectRead))
                        {
                            if (clientSocket.Client.Available == 0)
                            {
                                break;

                            }
                        }

                        token.WaitHandle.WaitOne(100);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                clientSocket?.Close();

                Clients.Remove(clientModel);
            }
        }
    }
}
