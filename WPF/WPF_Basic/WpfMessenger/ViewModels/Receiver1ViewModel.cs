using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using WpfMessenger.Message;

namespace WpfMessenger.ViewModels
{
    public class Receiver1ViewModel : BindableBase
    {
        #region fields, properties
        private object sendObject = null;
        public object SendObject { get => sendObject; set => SetProperty(ref sendObject, value); }

        private string receiveText = "";
        public string ReceiveText { get => receiveText; set => SetProperty(ref receiveText, value); }
        #endregion

        public Receiver1ViewModel()
        {
            //WeakReferenceMessenger.Default.RegisterAll(this); // IRecipient<StringMessage> 해줘야함
            WeakReferenceMessenger.Default.Register<StringMessage>(this, OnReceive); // IRecipient<StringMessage> 없어도 됨
        }

        //public void Receive(StringMessage message)
        //{
        //    Application.Current.Dispatcher.BeginInvoke(() =>
        //    {
        //        SendObject = message.Object;
        //        ReceiveText = message.Text;
        //    });
        //}

        private void OnReceive(object recipient, StringMessage message)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                SendObject = message.Object;
                ReceiveText = message.String;
            });
        }
    }
}
