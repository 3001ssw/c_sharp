using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Message
{

    public class StringMessage : ValueChangedMessage<(object obj, string str)>
    {
        #region properties

        public object Object { get; set; } // 보내는 객체
        public string String { get; set; } // 보내는 데이터(문자열)

        #endregion

        public StringMessage((object obj, string str) value) : base(value)
        {
            Object = value.obj;
            String = value.str;
        }
    }
}
