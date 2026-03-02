using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUdp.UDP
{
    public class UdpMessage
    {
        public string Time { get; set; } = "";
        public string TxRx { get; set; } = "";
        public string Ip { get; set; } = "";
        public string Port { get; set; } = "";
        public string Message { get; set; } = "";

        public UdpMessage()
        {
            Time = DateTime.Now.ToString("HH:mm:ss.fff");
        }
    }
}
