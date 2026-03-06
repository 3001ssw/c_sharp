using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfTcpClient.Chat;

namespace WpfTcpClient
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private TcpClient? _client = null;
        private NetworkStream? _stream = null;

        private string _serverIP = "127.0.0.1";
        public string ServerIP { get => _serverIP; set => SetProperty(ref _serverIP, value); }

        private int _serverPort = 7000;
        public int ServerPort { get => _serverPort; set => SetProperty(ref _serverPort, value); }

        public ObservableCollection<ChatMessage> ChatMessages { get; set; } = new ObservableCollection<ChatMessage>();

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
                _stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                _client?.Dispose();
                _client = null;
                _stream = null;
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

        }

        private bool CanDisconnectServer()
        {
            if (_client == null)
                return false;

            return true;
        }

        private void OnSendMessage()
        {
            SendMessage = "";
        }

        private bool CanSendMessage()
        {
            if (_client != null)
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
            SendMessageCommand = new DelegateCommand(OnSendMessage, CanSendMessage);
            CloseCommand = new DelegateCommand(OnClose, CanClose);

            RaiseAllCanExecuteChanged();
        }

        private void RaiseAllCanExecuteChanged()
        {
            ConnectServerCommand.RaiseCanExecuteChanged();
            DisconnectServerCommand.RaiseCanExecuteChanged();
            SendMessageCommand.RaiseCanExecuteChanged();
            CloseCommand.RaiseCanExecuteChanged();
        }
    }
}
