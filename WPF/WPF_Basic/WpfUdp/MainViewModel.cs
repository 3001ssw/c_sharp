using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfUdp
{
    public class MainViewModel : BindableBase
    {
        #region Fields & Properties

        private UdpClient? _udpListener;
        private bool _isOpened = false; // 소켓 열림 상태 추적

        private string udpIp = "127.0.0.1";
        public string UdpIp { get => udpIp; set => SetProperty(ref udpIp, value); }

        private string udpPort = "8080";
        public string UdpPort { get => udpPort; set => SetProperty(ref udpPort, value); }

        private string sendData = "";
        public string SendData { get => sendData; set => SetProperty(ref sendData, value); }

        private string targetPort = "8080"; // 기본값 설정
        public string TargetPort { get => targetPort; set => SetProperty(ref targetPort, value); }

        private ObservableCollection<UdpMessage> udpRecvMessages = new ObservableCollection<UdpMessage>();
        public ObservableCollection<UdpMessage> UdpRecvMessages { get => udpRecvMessages; set => SetProperty(ref udpRecvMessages, value); }

        #endregion

        #region Commands

        public DelegateCommand UdpOpenCommand { get; private set; }
        public DelegateCommand UdpCloseCommand { get; private set; }
        public DelegateCommand UdpSendCommand { get; private set; }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            // 버튼 활성화/비활성화 상태를 _isOpened 변수와 동기화
            UdpOpenCommand = new DelegateCommand(OnUdpOpen, () => !_isOpened);
            UdpCloseCommand = new DelegateCommand(OnUdpClose, () => _isOpened);
            UdpSendCommand = new DelegateCommand(OnUdpSend, CanUdpSend).ObservesProperty(() => SendData).ObservesProperty(() => TargetPort);
        }

        #endregion

        #region Methods

        private async void OnUdpOpen()
        {
            if (!int.TryParse(UdpPort, out int port))
            {
                MessageBox.Show("올바른 포트 번호를 입력하세요.");
                return;
            }

            try
            {
                // 지정된 포트로 수신 대기 시작
                _udpListener = new UdpClient(port);
                _isOpened = true;

                // 명령 버튼 상태 갱신 (Open은 꺼지고, Close는 켜짐)
                UdpOpenCommand.RaiseCanExecuteChanged();
                UdpCloseCommand.RaiseCanExecuteChanged();

                // 비동기 수신 루프 시작 (UI가 멈추지 않음!)
                await ReceiveLoopAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"포트 열기 실패: {ex.Message}");
            }
        }

        private async Task ReceiveLoopAsync()
        {
            try
            {
                while (_isOpened && _udpListener != null)
                {
                    // 데이터가 들어올 때까지 백그라운드에서 대기
                    UdpReceiveResult result = await _udpListener.ReceiveAsync();

                    string message = Encoding.UTF8.GetString(result.Buffer);
                    string senderIp = result.RemoteEndPoint.Address.ToString();
                    string senderPort = result.RemoteEndPoint.Port.ToString();

                    // await 이후에는 자동으로 UI 스레드로 돌아오므로 ObservableCollection을 바로 수정 가능
                    UdpRecvMessages.Add(new UdpMessage
                    {
                        Time = DateTime.Now.ToString("HH:mm:ss.fff"),
                        Ip = senderIp,
                        Port = senderPort,
                        Message = message
                    });
                }
            }
            catch (ObjectDisposedException)
            {
                // Close()를 호출하여 소켓이 닫히면 발생하는 정상적인 예외이므로 무시합니다.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"수신 오류: {ex.Message}");
            }
        }

        private void OnUdpClose()
        {
            _isOpened = false;
            _udpListener?.Close(); // 수신 대기를 강제로 깨고 소켓을 닫습니다.
            _udpListener = null;

            UdpOpenCommand.RaiseCanExecuteChanged();
            UdpCloseCommand.RaiseCanExecuteChanged();
        }

        private async void OnUdpSend()
        {
            if (!int.TryParse(TargetPort, out int port)) return;

            try
            {
                // 송신용 임시 소켓 생성
                using (UdpClient client = new UdpClient())
                {
                    byte[] data = Encoding.UTF8.GetBytes(SendData);

                    // 입력된 IP와 포트로 데이터 전송
                    await client.SendAsync(data, data.Length, UdpIp, port);

                    // (선택 사항) 내가 보낸 데이터도 화면에 표시
                    UdpRecvMessages.Add(new UdpMessage
                    {
                        Time = DateTime.Now.ToString("HH:mm:ss.fff"),
                        Ip = $"Me -> {UdpIp}",
                        Port = port.ToString(),
                        Message = SendData
                    });

                    SendData = ""; // 전송 후 입력창 비우기
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"전송 실패: {ex.Message}");
            }
        }

        private bool CanUdpSend()
        {
            // 보낼 데이터가 비어있지 않아야 Send 버튼 활성화
            return !string.IsNullOrWhiteSpace(SendData);
        }

        #endregion
    }
}
