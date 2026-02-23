using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUdp
{
    public class UdpMessage
    {
        public string Time { get; set; } = "";
        public string Ip { get; set; } = "";
        public string Port { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
