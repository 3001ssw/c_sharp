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
        private CancellationTokenSource? cs = null;

        private TcpListener? chatServer = null;
        public TcpListener? ChatServer { get => chatServer; set => SetProperty(ref chatServer, value); }

        private int serverPort = 7000;
        public int ServerPort { get => serverPort; set => SetProperty(ref serverPort, value); }


        private ObservableCollection<ClientModel> clients = new ObservableCollection<ClientModel>();
        public ObservableCollection<ClientModel> Clients { get => clients; set => SetProperty(ref clients, value); }
        private object clientLock = new object();

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


        #region constructor
        public MainViewModel()
        {
            OpenServerCommand = new DelegateCommand(OnOpenServer, CanOpenServer).ObservesProperty(() => ChatServer);
            CloseServerCommand = new DelegateCommand(OnCloseServer, CanCloseServer).ObservesProperty(() => ChatServer);
            SendMessageCommand = new DelegateCommand(OnSendMessage, CanSendMessage).ObservesProperty(() => ChatServer).ObservesProperty(() => SendMessage);

            BindingOperations.EnableCollectionSynchronization(Clients, clientLock);
            BindingOperations.EnableCollectionSynchronization(ChatMessages, chatMessageLock);
        }
        #endregion

        private void OnOpenServer()
        {
            cs = new CancellationTokenSource();
            Task.Run(() => AcceptTask(cs.Token));
        }

        private bool CanOpenServer()
        {
            if (ChatServer != null)
                return false;

            return true;
        }

        private void OnCloseServer()
        {
            cs?.Cancel();
            cs = null;
        }

        private bool CanCloseServer()
        {
            if (ChatServer == null)
                return false;

            return true;
        }

        private void OnSendMessage()
        {
            lock (Clients)
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
        }

        private bool CanSendMessage()
        {
            if (ChatServer == null)
                return false;

            if (Clients.Count <= 0)
                return false;

            if (string.IsNullOrEmpty(SendMessage))
                return false;

            return true;
        }

        #endregion


        private void AcceptTask(CancellationToken token)
        {
            try
            {
                ChatServer = new TcpListener(IPAddress.Any, ServerPort); // 7000번 포트 열기
                ChatServer.Start();

                while (!token.IsCancellationRequested)
                {
                    if (ChatServer != null && ChatServer.Pending())
                    {
                        TcpClient newClient = ChatServer.AcceptTcpClient();
                        ClientModel newClientModel = new ClientModel()
                        {
                            ClientSocket = newClient
                        };

                        Task.Run(() => ClientTask(newClientModel, token));

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
            finally
            {
                ChatServer?.Dispose();
                ChatServer = null;
            }
        }

        private void ClientTask(ClientModel clientModel, CancellationToken token)
        {
            lock (Clients)
                Clients.Add(clientModel);

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

                lock (Clients)
                {
                    clientSocket?.Close();
                    Clients.Remove(clientModel);
                }
            }
        }
    }
}
