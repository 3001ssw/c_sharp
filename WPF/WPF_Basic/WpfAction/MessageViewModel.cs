using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAction
{
    public class MessageViewModel : BindableBase
    {
        #region fields, properties

        private DateTime time = DateTime.Now;
        public DateTime Time { get => time; set => SetProperty(ref time, value); }

        private string message = "";
        public string Message { get => message; set => SetProperty(ref message, value); }

        #endregion

        #region constructor
        public MessageViewModel()
        {

        }
        #endregion
    }
}
