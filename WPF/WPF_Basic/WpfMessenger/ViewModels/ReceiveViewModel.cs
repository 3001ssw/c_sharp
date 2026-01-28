using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using WpfMessenger.Message;

namespace WpfMessenger.ViewModels
{
    public class ReceiveViewModel : BindableBase//, IRecipient<StringMessage>, IRecipient<IntMessage>
    {
        #region fields, properties

        private ObservableCollection<MessageViewModel> messages = new ObservableCollection<MessageViewModel>();
        public ObservableCollection<MessageViewModel> Messages { get => messages; set => SetProperty(ref messages, value); }

        private string channel = string.Empty;
        public string Channel
        {
            get => channel;
            set
            {
                SetProperty(ref channel, value);
                SetChannelCommand.RaiseCanExecuteChanged();
            }
        }

        private string currentChannel = string.Empty;
        public string CurrentChannel { get => currentChannel; set => SetProperty(ref currentChannel, value); }


        private ObservableCollection<MessageViewModel> channelMessages = new ObservableCollection<MessageViewModel>();
        public ObservableCollection<MessageViewModel> ChannelMessages { get => channelMessages; set => SetProperty(ref channelMessages, value); }

        #endregion

        #region commands
        public DelegateCommand SetChannelCommand { get; private set; }

        #endregion

        #region command methods
        private void OnSetChannel()
        {
            WeakReferenceMessenger.Default.Unregister<ChannelMessage, string>(this, CurrentChannel);
            WeakReferenceMessenger.Default.Register<ChannelMessage, string>(this, Channel, OnReceiveChannelMessage);
            CurrentChannel = Channel;
        }

        private bool CanSetChannel()
        {
            return true;
        }
        #endregion

        #region constructor
        public ReceiveViewModel()
        {
            //WeakReferenceMessenger.Default.RegisterAll(this); // IRecipient 해줘야함
            WeakReferenceMessenger.Default.Register<StringMessage>(this, OnReceiveStringMessage);
            WeakReferenceMessenger.Default.Register<IntMessage>(this, OnReceiveIntMessage);

            SetChannelCommand = new DelegateCommand(OnSetChannel, CanSetChannel);
            OnSetChannel();
        }
        #endregion

        #region functions

        //public void Receive(StringMessage message)
        //{
        //    Messages.Add(new MessageViewModel()
        //    {
        //        StringMessage = message,
        //    });
        //}
        //
        //public void Receive(IntMessage message)
        //{
        //    Messages.Add(new MessageViewModel()
        //    {
        //        IntMessage = message,
        //    });
        //}

        private void OnReceiveStringMessage(object recipient, StringMessage message)
        {
            Messages.Add(new MessageViewModel()
            {
                StringMessage = message,
            });
        }
        
        private void OnReceiveIntMessage(object recipient, IntMessage message)
        {
            Messages.Add(new MessageViewModel()
            {
                IntMessage = message,
            });
        }

        private void OnReceiveChannelMessage(object recipient, ChannelMessage message)
        {
            ChannelMessages.Add(new MessageViewModel()
            {
                ChannelMessage = message,
            });
        }
        #endregion

    }
}
