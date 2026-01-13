using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMessenger
{
    public class ReceiveViewModel : BindableBase, IRecipient<MyMessage>
    {
        #region fields, properties
        private string receiveText = "";
        public string ReceiveText { get => receiveText; set => SetProperty(ref receiveText, value); }
        #endregion

        //public ReceiveViewModel()
        //{
        //    WeakReferenceMessenger.Default.Register<MyMessage>(this, OnMessageReceived);
        //}
        //
        //private void OnMessageReceived(object recipient, MyMessage message)
        //{
        //    Application.Current.Dispatcher.BeginInvoke(() =>
        //    {
        //        ReceiveText = message.Value;
        //    });
        //}

        public ReceiveViewModel()
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public void Receive(MyMessage message)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                ReceiveText = message.Value;
            });
        }
    }
}
