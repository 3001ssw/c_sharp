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
        private string channel = "Channel 1";
        public string Channel
        {
            get => channel;
            set
            {
                SetProperty(ref channel, value);
                SetChannelCommand.RaiseCanExecuteChanged();
            }
        }

        private object sendObject = null;
        public object SendObject { get => sendObject; set => SetProperty(ref sendObject, value); }

        private string receiveText = "";
        public string ReceiveText { get => receiveText; set => SetProperty(ref receiveText, value); }
        #endregion

        #region commans
        public DelegateCommand SetChannelCommand { get; private set; }

        private string _currentChannel = "";
        private void OnSetChannel()
        {
            if (string.IsNullOrEmpty(_currentChannel) == false)
            {
                WeakReferenceMessenger.Default.Unregister<StringMessage, string>(this, _currentChannel);
                _currentChannel = "";
            }

            WeakReferenceMessenger.Default.Register<StringMessage, string>(this, Channel, OnMessageReceived);
            _currentChannel = Channel;
        }

        private bool CanSetChannel()
        {
            if (string.IsNullOrEmpty(Channel))
                return false;

            return true;
        }
        #endregion

        #region constructor
        public Receiver2ViewModel()
        {
            SetChannelCommand = new DelegateCommand(OnSetChannel, CanSetChannel);
            OnSetChannel();
        }
        #endregion


        private void OnMessageReceived(object recipient, StringMessage message)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                SendObject = message.Object;
                ReceiveText = message.String;
            });
        }
    }
}
