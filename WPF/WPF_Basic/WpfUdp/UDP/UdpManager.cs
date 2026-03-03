using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfUdp.UDP
{
    public class UdpManager
    {
        private UdpClient? _listener;
        private Thread? _receiveThread;
        private bool _isRunning = false;

        public event EventHandler<UdpMessage>? MessageReceived;

        public void StartListening(int port)
        {
            if (_isRunning)
                return;

            try
            {
                _listener = new UdpClient(port);
                _isRunning = true;

                //수신 쓰레드 실행
                _receiveThread = new Thread(ThreadReceive)
                {
                    IsBackground = true
                };
                _receiveThread.Start();

                MessageReceived?.Invoke(this, new UdpMessage
                {
                    TxRx = "",
                    Message = "Start Listening"
                });
            }
            catch (Exception ex)
            {

                MessageReceived?.Invoke(this, new UdpMessage
                {
                    TxRx = "",
                    Message = $"Fail: {ex.Message}"
                });
            }
        }

        public void StopListening()
        {
            _isRunning = false;

            _listener?.Close();
            _listener = null;

            MessageReceived?.Invoke(this, new UdpMessage
            {
                TxRx = "",
                Message = "Stop Listening"
            });
        }

        public int Send(string targetIp, string targetPort, string message)
        {
            int sendLen = 0;
            if (_isRunning == false || _listener == null)
                return sendLen;

            if (!int.TryParse(targetPort, out int port))
                return sendLen;

            byte[] data = Encoding.UTF8.GetBytes(message);

            MessageReceived?.Invoke(this, new UdpMessage
            {
                TxRx = "Tx",
                Ip = targetIp,
                Port = targetPort,
                Message = message
            });
            sendLen = _listener.Send(data, data.Length, targetIp, port);

            return sendLen;
        }

        public bool IsOpen()
        {
            return _isRunning;
        }

        private void ThreadReceive()
        {
            try
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

                while (_isRunning && _listener != null)
                {
                    byte[] buffer = _listener.Receive(ref remoteEndPoint);

                    string message = Encoding.UTF8.GetString(buffer);
                    string senderIp = remoteEndPoint.Address.ToString();
                    string senderPort = remoteEndPoint.Port.ToString();

                    MessageReceived?.Invoke(this, new UdpMessage
                    {
                        TxRx = "Rx",
                        Ip = senderIp,
                        Port = senderPort,
                        Message = message
                    });
                }
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted || ex.SocketErrorCode == SocketError.OperationAborted)
            {
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ReceiveLoop Error: {ex.Message}");
            }
        }
    }
}
