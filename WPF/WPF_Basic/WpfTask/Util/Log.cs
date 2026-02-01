using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTask.Util
{
    public class Log : BindableBase
    {
        private DateTime time = DateTime.Now;
        public DateTime Time { get => time; set => SetProperty(ref time, value); }

        private string message = "";
        public string Message { get => message; set => SetProperty(ref message, value); }

        #region constructor
        public Log(string msg = "")
        {
            Message = msg;
        }
        #endregion
    }
}
