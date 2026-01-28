using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Message;

namespace WpfMessenger.ViewModels
{
    public class SendViewModel : BindableBase
    {
        #region Fields, Properties
        private string sendValue = "";
        public string SendValue
        {
            get => sendValue;
            set
            {
                SetProperty(ref sendValue, value);
                SendValueCommand.RaiseCanExecuteChanged();
            }
        }

        private string messageToken = "";
        public string MessageToken { get => messageToken; set => SetProperty(ref messageToken, value); }

        private string message = "";
        public string Message
        {
            get => message;
            set
            {
                SetProperty(ref message, value);
                SendMessageTokenCommand.RaiseCanExecuteChanged();
            }
        }

        private string sendString = string.Empty;
        public string SendString
        {
            get => sendString;
            set
            {
                SetProperty(ref sendString, value);
                SendStringCommand.RaiseCanExecuteChanged();
            }
        }

        private string sendInteger = string.Empty;
        public string SendInteger
        {
            get => sendInteger;
            set
            {
                SetProperty(ref sendInteger, value);
                SendIntegerCommand.RaiseCanExecuteChanged();
            }
        }

        private string token = string.Empty;
        public string Token
        {
            get => token;
            set
            {
                SetProperty(ref token, value);
                SendTextTokenCommand.RaiseCanExecuteChanged();
            }
        }

        private string _text = string.Empty;
        public string Text
        {
            get => _text;
            set
            {
                SetProperty(ref _text, value);
                SendTextTokenCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Commands
        public DelegateCommand SendValueCommand { get; private set; }
        public DelegateCommand SendMessageTokenCommand { get; private set; }
        public DelegateCommand SendStringCommand { get; private set; }
        public DelegateCommand SendIntegerCommand { get; private set; }
        public DelegateCommand SendTextTokenCommand { get; private set; }
        #endregion

        #region Command Methods
        // ValueChangedMessage
        private void OnSendValue()
        {
            ValueChangedMessage<string> message = new ValueChangedMessage<string>(SendValue);
            WeakReferenceMessenger.Default.Send(message);
        }

        private bool CanSendValue()
        {
            if (string.IsNullOrEmpty(SendValue))
                return false;
            return true;
        }
        // ValueChangedMessage By Token
        private void OnSendMessageToken()
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>(Message), MessageToken);
        }

        private bool CanSendMessageToken()
        {
            if (string.IsNullOrEmpty(Message))
                return false;
            return true;
        }

        // String 전송 로직
        private void OnSendString()
        {
            // TODO: 메시지 전송 로직 구현
            WeakReferenceMessenger.Default.Send(new StringMessage((this, SendString)));
        }

        private bool CanSendString()
        {
            if (string.IsNullOrEmpty(SendString))
                return false;
            return true;
        }

        // Integer 전송 로직
        private void OnSendInteger()
        {
            // TODO: 메시지 전송 로직 구현
            if (int.TryParse(SendInteger, out int integer))
                WeakReferenceMessenger.Default.Send(new IntMessage((this, integer)));
        }

        private bool CanSendInteger()
        {
            if (string.IsNullOrEmpty(SendInteger))
                return false;
            if (int.TryParse(SendInteger, out int integer) == false)
                return false;

            return true;
        }

        // Token & Text 전송 로직
        private void OnSendTextToken()
        {
            WeakReferenceMessenger.Default.Send(new TokenMessage((Token, this, Text)), Token);
        }

        private bool CanSendTextToken()
        {
            if (string.IsNullOrEmpty(Text))
                return false;
            return true;
        }

        #endregion

        public SendViewModel()
        {
            // 커맨드 초기화
            SendValueCommand = new DelegateCommand(OnSendValue, CanSendValue);
            SendMessageTokenCommand = new DelegateCommand(OnSendMessageToken, CanSendMessageToken);
            SendStringCommand = new DelegateCommand(OnSendString, CanSendString);
            SendIntegerCommand = new DelegateCommand(OnSendInteger, CanSendInteger);
            SendTextTokenCommand = new DelegateCommand(OnSendTextToken, CanSendTextToken);
        }
    }
}