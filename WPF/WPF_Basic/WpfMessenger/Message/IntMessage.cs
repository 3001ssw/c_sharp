using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Message
{
    public class IntMessage : ValueChangedMessage<(object obj, int integer)>
    {
        public object Object { get; set; }
        public int Integer{ get; set; }
        public IntMessage((object obj, int integer) value) : base(value)
        {
            Object = value.obj;
            Integer = value.integer;
        }
    }
}
