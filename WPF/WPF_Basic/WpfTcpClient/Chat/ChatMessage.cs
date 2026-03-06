using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTcpClient.Chat
{
    public class ChatMessage
    {
        #region fields, properties

        public DateTime Time { get; set; } = DateTime.Now;
        public string IP { get; set; } = "";
        public int Port { get; set; } = 0;
        public string Message { get; set; } = "";

        #endregion

    }
}
