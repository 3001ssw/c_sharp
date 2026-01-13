using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMessenger
{
    public class OtherReceiverViewModel : BindableBase
    {
        #region fields, properties
        private string receiveText = "";
        public string ReceiveText { get => receiveText; set => SetProperty(ref receiveText, value); }
        #endregion

        public OtherReceiverViewModel()
        {
            WeakReferenceMessenger.Default.Register<MyMessage>(this, (r, m) =>
            {
                // r: 수신자(this), m: 받은 메시지 객체
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    ReceiveText = m.Value;
                });
            });
        }
    }
}
