using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace WpfMessenger.Message
{
    public class MessageViewModel : BindableBase
    {
        #region fields, properties
        private DateTime time = DateTime.Now;
        public DateTime Time { get => time; set => SetProperty(ref time, value); }

        private string messageType = string.Empty;
        public string MessageType { get => messageType; set => SetProperty(ref messageType, value); }

        private string token = string.Empty;
        public string Token { get => token; set => SetProperty(ref token, value); }

        private object obj = null;
        public object Object { get => obj; set => SetProperty(ref obj, value); }

        private string message = string.Empty;
        public string Message { get => message; set => SetProperty(ref message, value); }

        public StringMessage StringMessage
        {
            set
            {
                MessageType = value.GetType().ToString();
                Object = value.Object;
                Message = value.String;
            }
        }

        public IntMessage IntMessage
        {
            set
            {
                MessageType = value.GetType().ToString();
                Object = value.Object;
                Message = $"{value.Integer}";
            }
        }

        public TokenMessage ChannelMessage
        {
            set
            {
                MessageType = value.GetType().ToString();
                Token = value.Channel;
                Object = value.Object;
                Message = value.String;
            }
        }

        #endregion


        #region constructor
        public MessageViewModel()
        {

        }

        #endregion
    }
}
