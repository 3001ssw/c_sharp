using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMessenger.Message;

namespace WpfMessenger.ViewModels
{
    public class Receiver2ViewModel : BindableBase
    {

        #region fields, properties
        private object sendObject = null;
        public object SendObject { get => sendObject; set => SetProperty(ref sendObject, value); }

        private string receiveText = "";
        public string ReceiveText { get => receiveText; set => SetProperty(ref receiveText, value); }
        #endregion

        public Receiver2ViewModel()
        {
            WeakReferenceMessenger.Default.Register<MyMessage>(this, (r, m) =>
            {
                // r: 수신자(this), m: 받은 메시지 객체
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    SendObject = m.Value.obj;
                    ReceiveText = m.Value.txt;
                });
            });
            //WeakReferenceMessenger.Default.Register<MyMessage>(this, OnMessageReceived);

        }
        //private void OnMessageReceived(object recipient, MyMessage message)
        //{
        //    Application.Current.Dispatcher.BeginInvoke(() =>
        //    {
        //        ReceiveText = message.Value.txt;
        //    });
        //}
    }
}
