using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WpfUdp.UDP;

namespace WpfUdp
{
    public class MainViewModel : BindableBase
    {
        #region Fields & Properties

        private UdpManager _udpManager = new UdpManager();

        private string myPort = "8080";
        public string MyPort { get => myPort; set => SetProperty(ref myPort, value); }

        private string targetIp = "127.0.0.1";
        public string TargetIp { get => targetIp; set => SetProperty(ref targetIp, value); }

        private string targetPort = "8081";
        public string TargetPort { get => targetPort; set => SetProperty(ref targetPort, value); }

        private string sendData = "";
        public string SendData { get => sendData; set => SetProperty(ref sendData, value); }

        private ObservableCollection<UdpMessage> udpRecvMessages = new ObservableCollection<UdpMessage>();
        public ObservableCollection<UdpMessage> UdpRecvMessages { get => udpRecvMessages; set => SetProperty(ref udpRecvMessages, value); }
        private object _lockUdpRecvMessages = new object(); // 동기화를 위한 자물쇠

        #endregion

        #region Commands

        public DelegateCommand UdpOpenCommand { get; private set; }
        public DelegateCommand UdpCloseCommand { get; private set; }
        public DelegateCommand UdpSendCommand { get; private set; }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            BindingOperations.EnableCollectionSynchronization(UdpRecvMessages, _lockUdpRecvMessages);

            UdpOpenCommand = new DelegateCommand(OnUdpOpen);
            UdpCloseCommand = new DelegateCommand(OnUdpClose);
            UdpSendCommand = new DelegateCommand(OnUdpSend);
        }

        #endregion

        #region Methods

        private void OnUdpOpen()
        {
            if (!int.TryParse(MyPort, out int port))
            {
                MessageBox.Show("올바른 포트 번호를 입력하세요.");
                return;
            }

            if (_udpManager.IsOpen())
                return;

            _udpManager.MessageReceived += OnMessageReceived;
            _udpManager.StartListening(port);
        }

        private void OnUdpClose()
        {
            if (!_udpManager.IsOpen())
                return;

            _udpManager.StopListening();
            _udpManager.MessageReceived -= OnMessageReceived;
        }

        private void OnUdpSend()
        {
            _udpManager.Send(TargetIp, TargetPort, SendData);
        }

        private void OnMessageReceived(object? sender, UdpMessage message)
        {
            UdpRecvMessages.Add(message);
        }
        #endregion
    }
}
