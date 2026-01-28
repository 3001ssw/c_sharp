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
        #region properties

        public object Object { get; set; } // 보내는 객체
        public int Integer { get; set; } // 보내는 데이터(숫자)

        #endregion

        public IntMessage((object obj, int integer) value) : base(value)
        {
            Object = value.obj;
            Integer = value.integer;
        }
    }
}
