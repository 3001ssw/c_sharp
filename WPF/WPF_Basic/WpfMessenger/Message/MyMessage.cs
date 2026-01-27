using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Message
{
    public class MyMessage : ValueChangedMessage<(object obj, string txt)>
    {
        public object Object { get; set; }
        public string Text { get; set; }
        public MyMessage((object obj, string txt) value) : base(value)
        {
            Object = value.obj;
            Text = value.txt;
        }
    }
}
