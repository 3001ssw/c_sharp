using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfTcpServer.Chat
{
    public class ClientModel
    {
        public string IP { get; set; } = "";
        public int Port { get; set; } = 0;

        private TcpClient clientSocket = null;
        public TcpClient ClientSocket
        {
            
            get => clientSocket;
            set
            {
                clientSocket = value;
                if (clientSocket.Client.RemoteEndPoint is IPEndPoint endPoint)
                {
                    IP = endPoint.Address.ToString();
                    Port = endPoint.Port;
                }
            }
        }
    }
}
