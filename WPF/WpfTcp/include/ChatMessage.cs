using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    [Serializable]
    public class ChatMessage
    {
        #region fields, properties
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime Time { get; set; } = DateTime.Now;
        public string Name { get; set; } = string.Empty;
        public string Message { get; set; } = "";

        #endregion

    }
}
