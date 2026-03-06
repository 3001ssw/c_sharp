using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WpfTcpServer.Chat;

namespace WpfTcpServer
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private TcpListener? _server = null;
        private Thread? serverAcceptThread = null;
        private CancellationTokenSource? acceptCts = null;

        private int serverPort = 0;
        public int ServerPort { get => serverPort; set => SetProperty(ref serverPort, value); }

        private ObservableCollection<ClientModel> accepClients = new ObservableCollection<ClientModel>();
        public ObservableCollection<ClientModel> AccepClients { get => accepClients; set => SetProperty(ref accepClients, value); }

        #endregion

        #region commands

        public DelegateCommand OpenServerCommand { get; private set; }
        public DelegateCommand CloseServerCommand { get; private set; }

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

            _server?.Dispose();
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

        #endregion


        #region constructor
        public MainViewModel()
        {
            OpenServerCommand = new DelegateCommand(OnOpenServer, CanOpenServer);
            CloseServerCommand = new DelegateCommand(OnCloseServer, CanCloseServer);

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
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    TcpClient newClient = _server.AcceptTcpClient();
                    if (newClient.Client.RemoteEndPoint is IPEndPoint endPoint)
                    {
                        string clientIP = endPoint.Address.ToString();
                        int clientPort = endPoint.Port;

                        ClientModel newClientModel = new ClientModel()
                        {
                            IP = clientIP,
                            Port = clientPort,
                        };
                        AccepClients.Add(newClientModel);
                    }
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
    }
}
