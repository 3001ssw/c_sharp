using Chat;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Data;

namespace WpfTcpServer
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private CancellationTokenSource? cs = null;

        private TcpListener? chatServer = null;
        public TcpListener? ChatServer { get => chatServer; set => SetProperty(ref chatServer, value); }


        private string serverIP = "127.0.0.1";
        public string ServerIP { get => serverIP; set => SetProperty(ref serverIP, value); }

        private int serverPort = 7000;
        public int ServerPort { get => serverPort; set => SetProperty(ref serverPort, value); }


        private object clientLock = new object();
        private ObservableCollection<UserInfo> users = new ObservableCollection<UserInfo>();
        public ObservableCollection<UserInfo> Users { get => users; set => SetProperty(ref users, value); }

        private UserInfo? selectedUser = null;
        public UserInfo? SelectedUser { get => selectedUser; set => SetProperty(ref selectedUser, value); }

        private Dictionary<Guid, TcpClient> _dicIdNUser = new Dictionary<Guid, TcpClient>();


        private object chatMessageLock = new object();
        private ObservableCollection<ChatMessage> chatMessages = new ObservableCollection<ChatMessage>();
        public ObservableCollection<ChatMessage> ChatMessages { get => chatMessages; set => SetProperty(ref chatMessages, value); }


        private string sendMessage = "";
        public string SendMessage { get => sendMessage; set => SetProperty(ref sendMessage, value); }
        #endregion

        #region commands

        public DelegateCommand OpenServerCommand { get; private set; }
        public DelegateCommand CloseServerCommand { get; private set; }
        public DelegateCommand CloseClientCommand { get; private set; }
        public DelegateCommand SendMessageCommand { get; private set; }

        public DelegateCommand OkCommand { get; private set; }


        #region constructor
        public MainViewModel()
        {
            OpenServerCommand = new DelegateCommand(OnOpenServer, CanOpenServer).ObservesProperty(() => ChatServer);
            CloseServerCommand = new DelegateCommand(OnCloseServer, CanCloseServer).ObservesProperty(() => ChatServer);
            CloseClientCommand = new DelegateCommand(OnCloseClient, CanCloseClient).ObservesProperty(() => ChatServer).ObservesProperty(() => SelectedUser);
            SendMessageCommand = new DelegateCommand(OnSendMessage, CanSendMessage).ObservesProperty(() => ChatServer).ObservesProperty(() => SendMessage);
            OkCommand = new DelegateCommand(OnOk, CanOk);

            BindingOperations.EnableCollectionSynchronization(Users, clientLock);
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

        private void OnCloseClient()
        {
            if (SelectedUser == null)
                return;

            if (_dicIdNUser.TryGetValue(SelectedUser.Id, out TcpClient? tcpClient))
                tcpClient?.Close();
            _dicIdNUser.Remove(SelectedUser.Id);
            UserInfo? user = Users.FirstOrDefault(x => x.Id == SelectedUser.Id);
            if (user != null)
                Users.Remove(user);
           SelectedUser = null;
        }

        private bool CanCloseClient()
        {
            if (ChatServer == null)
                return false;

            if (SelectedUser == null)
                return false;

            return true;
        }

        private void OnSendMessage()
        {
            lock (Users)
            {
                JsonPacket data = new JsonPacket()
                {
                    Message = new ChatMessage()
                    {
                        Name = "Server",
                        Message = SendMessage,
                    }
                };
                string jsonString = JsonSerializer.Serialize(data);

                foreach (UserInfo user in Users)
                {
                    if (_dicIdNUser.TryGetValue(user.Id, out TcpClient? tcpClient))
                    {
                        if (tcpClient != null)
                        {
                            using (StreamWriter writer = new StreamWriter(stream: tcpClient.GetStream(), encoding: new UTF8Encoding(false), leaveOpen: true))
                            {
                                writer.AutoFlush = true;
                                writer.WriteLine(jsonString);
                            }
                        }
                    }
                }

                lock (ChatMessages)
                    ChatMessages.Add(data.Message);
            }
            SendMessage = "";
        }

        private bool CanSendMessage()
        {
            if (ChatServer == null)
                return false;

            if (Users.Count <= 0)
                return false;

            if (string.IsNullOrEmpty(SendMessage))
                return false;

            return true;
        }
        private void OnOk()
        {
            SendMessageCommand.Execute();
        }

        private bool CanOk()
        {
            return SendMessageCommand.CanExecute();
        }

        #endregion


        private void AcceptTask(CancellationToken token)
        {
            try
            {
                IPAddress localIp = IPAddress.Parse(ServerIP);
                ChatServer = new TcpListener(localIp, ServerPort); // 7000번 포트 열기
                ChatServer.Start();

                while (!token.IsCancellationRequested)
                {
                    if (ChatServer != null && ChatServer.Pending())
                    {
                        TcpClient client = ChatServer.AcceptTcpClient();

                        Task.Run(() => UserTask(client, token));

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

        private void UserTask(TcpClient client, CancellationToken token)
        {
            try
            {
                while (token.IsCancellationRequested == false)
                {
                    if (client == null || !client.Connected)
                        break;

                    if (0 < client.Available)
                    {
                        using (StreamReader reader = new StreamReader(stream: client.GetStream(), encoding: new UTF8Encoding(false), detectEncodingFromByteOrderMarks: true, leaveOpen: true))
                        {
                            string? jsonString = reader.ReadLine();
                            if (string.IsNullOrEmpty(jsonString) == false)
                            {
                                JsonPacket? data = JsonSerializer.Deserialize<JsonPacket>(jsonString);
                                if (data?.Type == nameof(ChatMessage) && data.Message != null)
                                {
                                    ChatMessages.Add(data.Message);
                                }
                                if (data?.Type == nameof(UserInfo) && data.User != null)
                                {
                                    lock (Users)
                                    {
                                        Users.Add(data.User);
                                        _dicIdNUser.Add(data.User.Id, client);
                                    }
                                }


                                lock (Users)
                                {
                                    foreach (UserInfo c in Users)
                                    {
                                        if (_dicIdNUser.TryGetValue(c.Id, out TcpClient? tcpClient))
                                        {
                                            if (tcpClient != null)
                                            {
                                                using (StreamWriter writer = new StreamWriter(stream: tcpClient.GetStream(), encoding: new UTF8Encoding(false), leaveOpen: true))
                                                {
                                                    writer.AutoFlush = true;
                                                    writer.WriteLine(jsonString);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (client.Client.Poll(1000000, SelectMode.SelectRead))
                        {
                            if (client.Client.Available == 0)
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
                Debug.WriteLine(ex);
            }
            finally
            {
                lock (Users)
                {
                    Guid id = _dicIdNUser.FirstOrDefault(x => x.Value == client).Key;
                    UserInfo? user = Users.FirstOrDefault(x => x.Id == id);
                    if (user != null)
                    {
                        Users.Remove(user);
                        JsonPacket data = new JsonPacket()
                        {
                            Message = new ChatMessage()
                            {
                                Name = user.Name,
                                Message = $"사용자가 나갔습니다.",
                            }
                        };
                        string jsonString = JsonSerializer.Serialize(data);
                        foreach (UserInfo c in Users)
                        {
                            if (_dicIdNUser.TryGetValue(c.Id, out TcpClient? tcpClient))
                            {
                                if (tcpClient != null)
                                {
                                    using (StreamWriter writer = new StreamWriter(stream: tcpClient.GetStream(), encoding: new UTF8Encoding(false), leaveOpen: true))
                                    {
                                        writer.AutoFlush = true;
                                        writer.WriteLine(jsonString);
                                    }
                                }
                            }
                        }
                    }
                    _dicIdNUser.Remove(id);

                }
                client?.Close();
            }
        }
    }
}
