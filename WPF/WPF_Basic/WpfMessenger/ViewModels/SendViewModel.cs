using CommunityToolkit.Mvvm.Messaging;
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

        private string channel = string.Empty;
        public string Channel
        {
            get => channel;
            set
            {
                SetProperty(ref channel, value);
                SendTextChannelCommand.RaiseCanExecuteChanged();
            }
        }

        private string _text = string.Empty;
        public string Text
        {
            get => _text;
            set
            {
                SetProperty(ref _text, value);
                SendTextChannelCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Commands
        public DelegateCommand SendStringCommand { get; private set; }
        public DelegateCommand SendIntegerCommand { get; private set; }
        public DelegateCommand SendTextChannelCommand { get; private set; }
        #endregion

        #region Command Methods

        // 1. String 전송 로직
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

        // 2. Integer 전송 로직
        private void OnSendInteger()
        {
            // TODO: 메시지 전송 로직 구현
            if (int.TryParse(SendInteger, out int value))
                WeakReferenceMessenger.Default.Send(new IntMessage((this, value)));
        }

        private bool CanSendInteger()
        {
            if (string.IsNullOrEmpty(SendInteger))
                return false;
            if (int.TryParse(SendInteger, out int value) == false)
                return false;

            return true;
        }

        // 3. Channel & Text 전송 로직
        private void OnSendTextChannel()
        {
            // TODO: 채널을 이용한 메시지 전송 로직 구현
            WeakReferenceMessenger.Default.Send(new ChannelMessage((Channel, this, Text)), Channel);
        }

        private bool CanSendTextChannel()
        {
            if (string.IsNullOrEmpty(Text))
                return false;
            return true;
        }

        #endregion

        public SendViewModel()
        {
            // 커맨드 초기화
            SendStringCommand = new DelegateCommand(OnSendString, CanSendString);
            SendIntegerCommand = new DelegateCommand(OnSendInteger, CanSendInteger);
            SendTextChannelCommand = new DelegateCommand(OnSendTextChannel, CanSendTextChannel);
        }
    }
}