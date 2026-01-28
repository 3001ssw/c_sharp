using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Message
{
    public class TokenMessage : ValueChangedMessage<(string ch, object obj, string str)>
    {
        #region properties
        public string Channel { get; set; }
        public object Object { get; set; }
        public string String { get; set; }

        #endregion
        public TokenMessage((string ch, object obj, string str) value) : base(value)
        {
            Channel = value.ch;
            Object = value.obj;
            String = value.str;
        }
    }
}
