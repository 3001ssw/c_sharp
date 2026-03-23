using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPipeClient
{
    public class PipeMessage
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public string Message { get; set; } = "";
    }
}
