using Chat;
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
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;

namespace WpfTcpClient
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private Guid Id = Guid.NewGuid();
        private CancellationTokenSource? _cs = null;

        private TcpClient? client = null;
        public TcpClient? Client { get => client; set => SetProperty(ref client, value); }


        private string serverIP = "127.0.0.1";
        public string ServerIP { get => serverIP; set => SetProperty(ref serverIP, value); }

        private int serverPort = 7000;
        public int ServerPort { get => serverPort; set => SetProperty(ref serverPort, value); }

        private string name = "User";
        public string Name { get => name; set => SetProperty(ref name, value); }

        public ObservableCollection<ChatMessage> ChatMessages { get; set; } = new ObservableCollection<ChatMessage>();
        private object _lockChatMessages = new object();

        private string sendMessage = "";
        public string SendMessage { get => sendMessage; set => SetProperty(ref sendMessage, value); }

        #endregion

        public DelegateCommand ConnectServerCommand { get; private set; }
        public DelegateCommand DisconnectServerCommand { get; private set; }
        public DelegateCommand SendMessageCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }

        public DelegateCommand OkCommand { get; private set; }


        public MainViewModel()
        {
            ConnectServerCommand = new DelegateCommand(OnConnectServer, CanConnectServer).ObservesProperty(() => Client);
            DisconnectServerCommand = new DelegateCommand(OnDisconnectServer, CanDisconnectServer).ObservesProperty(() => Client);
            SendMessageCommand = new DelegateCommand(OnSendMessage, CanSendMessage).ObservesProperty(() => Client).ObservesProperty(() => SendMessage);
            CloseCommand = new DelegateCommand(OnClose, CanClose);
            OkCommand = new DelegateCommand(OnOk, CanOk);

            BindingOperations.EnableCollectionSynchronization(ChatMessages, _lockChatMessages);
        }


        private void OnConnectServer()
        {
            _cs = new CancellationTokenSource();
            Task.Run(() => ChatClientTask(_cs.Token));
        }

        private bool CanConnectServer()
        {
            if (Client != null)
                return false;

            return true;
        }

        private void OnDisconnectServer()
        {
            _cs?.Cancel();
        }

        private bool CanDisconnectServer()
        {
            if (Client == null)
                return false;

            return true;
        }

        private void OnSendMessage()
        {
            if (Client == null)
                return;

            JsonPacket data = new JsonPacket()
            {
                Message = new ChatMessage()
                {
                    Id = Id,
                    Name = Name,
                    Message = SendMessage,
                }
            };
            string jsonString = JsonSerializer.Serialize(data);
            using (StreamWriter writer = new StreamWriter(stream: Client.GetStream(), encoding: new UTF8Encoding(false), leaveOpen: true))
            {
                writer.AutoFlush = true;
                writer.WriteLine(jsonString);
            }
            SendMessage = "";
        }

        private bool CanSendMessage()
        {
            if (Client == null)
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

        private void OnOk()
        {
            SendMessageCommand.Execute();
        }

        private bool CanOk()
        {
            return SendMessageCommand.CanExecute();
        }


        private void ChatClientTask(CancellationToken token)
        {
            try
            {
                Client = new TcpClient();
                Client.Connect(hostname: ServerIP, port: ServerPort);

                // 접속 정보 날리기
                JsonPacket clientInfo = new JsonPacket()
                {
                    User = new UserInfo()
                    {
                        Id = Id,
                        Name = Name,
                    }
                };
                string jsonClientInfo = JsonSerializer.Serialize(clientInfo);
                using (StreamWriter writer = new StreamWriter(stream: Client.GetStream(), encoding: new UTF8Encoding(false), leaveOpen: true))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(jsonClientInfo);
                }

                // 메시지 읽기
                while (!token.IsCancellationRequested)
                {
                    if (0 < Client.Available)
                    {
                        using (StreamReader reader = new StreamReader(stream: Client.GetStream(), encoding: new UTF8Encoding(false), detectEncodingFromByteOrderMarks:true, leaveOpen: true))
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
                                    if (Id == data.User.Id)
                                    {
                                        ChatMessages.Add(new ChatMessage()
                                        {
                                            Id = data.User.Id,
                                            Name = data.User.Name,
                                            Message = "접속 하였습니다.",
                                        });
                                    }
                                    else
                                    {
                                        ChatMessages.Add(new ChatMessage()
                                        {
                                            Id = data.User.Id,
                                            Name = data.User.Name,
                                            Message = $"사용자({data.User.Name})가 접속 하였습니다.",
                                        });
                                    }
                                }
                            }
                            token.WaitHandle.WaitOne(100);
                        }
                    }
                    else
                    {
                        // 읽을게 있는데
                        if (Client.Client.Poll(1000000, SelectMode.SelectRead))
                        {
                            // 사용할 수없는 경우엔 소켓이 종료된 상황
                            if (Client.Client.Available == 0)
                            {
                                break;
                            }
                        }
                        //token.WaitHandle.WaitOne(1000); // poll에서 대기하니까 주석 처리
                    }
                }
            }
            catch (Exception ex)
            {
                if (Client?.Client.RemoteEndPoint is IPEndPoint endPoint)
                {
                    string serverIP = endPoint.Address.MapToIPv4().ToString();
                    int serverPort = endPoint.Port;
                    string message = $"Error: {ex.Message}";
                    ChatMessages.Add(new ChatMessage()
                    {
                        Message = message,
                    });
                }
            }
            finally
            {
                Client?.Dispose();
                Client = null;

                ChatMessages.Add(new ChatMessage()
                {
                    Id = Id,
                    Name = Name,
                    Message = "종료 되었습니다.",
                });
            }
        }
    }
}
