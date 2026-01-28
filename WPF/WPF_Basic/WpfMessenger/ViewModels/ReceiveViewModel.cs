using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using WpfMessenger.Message;
using static System.Net.Mime.MediaTypeNames;

namespace WpfMessenger.ViewModels
{
    public class ReceiveViewModel
        : BindableBase
        //, IRecipient<ValueChangedMessage<string>>
        //, IRecipient<StringMessage>
        //, IRecipient<IntMessage>
    {
        #region fields, properties

        private ObservableCollection<MessageViewModel> messages = new ObservableCollection<MessageViewModel>();
        public ObservableCollection<MessageViewModel> Messages { get => messages; set => SetProperty(ref messages, value); }

        private string token = string.Empty;
        public string Token
        {
            get => token;
            set
            {
                SetProperty(ref token, value);
                SetTokenCommand.RaiseCanExecuteChanged();
            }
        }

        private string currentToken = string.Empty;
        public string CurrentToken { get => currentToken; set => SetProperty(ref currentToken, value); }


        private ObservableCollection<MessageViewModel> tokenMessage = new ObservableCollection<MessageViewModel>();
        public ObservableCollection<MessageViewModel> TokenMessages { get => tokenMessage; set => SetProperty(ref tokenMessage, value); }

        #endregion

        #region commands
        public DelegateCommand SetTokenCommand { get; private set; }

        #endregion

        #region command methods
        private void OnSetToken()
        {
            WeakReferenceMessenger.Default.Unregister<ValueChangedMessage<string>, string>(this, CurrentToken);
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>, string>(this, Token, OnReceiveMessageByToken);

            WeakReferenceMessenger.Default.Unregister<TokenMessage, string>(this, CurrentToken);
            WeakReferenceMessenger.Default.Register<TokenMessage, string>(this, Token, OnReceiveTokenMessage);
            CurrentToken = Token;
        }

        private bool CanSetToken()
        {
            return true;
        }
        #endregion

        #region constructor
        public ReceiveViewModel()
        {
            //WeakReferenceMessenger.Default.RegisterAll(this); // IRecipient 해줘야함

            WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>>(this, OnReceiveMessage);
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>, string>(this, Token, OnReceiveMessageByToken);

            WeakReferenceMessenger.Default.Register<StringMessage>(this, OnReceiveStringMessage);
            WeakReferenceMessenger.Default.Register<IntMessage>(this, OnReceiveIntMessage);
            WeakReferenceMessenger.Default.Register<TokenMessage, string>(this, Token, OnReceiveTokenMessage);

            SetTokenCommand = new DelegateCommand(OnSetToken, CanSetToken);
        }
        #endregion


        #region IRecipient Commands

        public void Receive(ValueChangedMessage<string> message)
        {
            MessageViewModel msgVM = new MessageViewModel();
            msgVM.MessageType = message.GetType().ToString();
            msgVM.Message = message.Value;
            Messages.Add(msgVM);
        }

        public void Receive(StringMessage message)
        {
            Messages.Add(new MessageViewModel()
            {
                StringMessage = message,
            });
        }

        public void Receive(IntMessage message)
        {
            Messages.Add(new MessageViewModel()
            {
                IntMessage = message,
            });
        }
        #endregion

        #region WeakReferenceMessenger.Default.Register

        private void OnReceiveMessage(object recipient, ValueChangedMessage<string> message)
        {
            MessageViewModel msgVM = new MessageViewModel();
            msgVM.MessageType = message.GetType().ToString();
            msgVM.Message = message.Value;
            Messages.Add(msgVM);
        }

        private void OnReceiveMessageByToken(object recipient, ValueChangedMessage<string> message)
        {
            MessageViewModel msgVM = new MessageViewModel();
            msgVM.MessageType = message.GetType().ToString();
            msgVM.Message = message.Value;
            Messages.Add(msgVM);
        }

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

        private void OnReceiveTokenMessage(object recipient, TokenMessage message)
        {
            TokenMessages.Add(new MessageViewModel()
            {
                ChannelMessage = message,
            });
        }
        #endregion

    }
}
