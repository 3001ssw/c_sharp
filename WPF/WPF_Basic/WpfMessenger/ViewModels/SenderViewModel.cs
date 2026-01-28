using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Message;

namespace WpfMessenger.ViewModels
{
    public class SenderViewModel : BindableBase
    {
        #region fields, properties
        private string channel = "Channel 1";
        public string Channel { get => channel; set => SetProperty(ref channel, value); }

        private string inputText = "";
        public string InputText
        {
            get => inputText;
            set
            {
                SetProperty(ref inputText, value);
                SendCommand.RaiseCanExecuteChanged();
                SendAllCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region commands
        public DelegateCommand SendCommand { get; private set; }
        public DelegateCommand SendAllCommand { get; private set; }

        private void OnSend()
        {
            WeakReferenceMessenger.Default.Send(new StringMessage((this, InputText)), Channel);
        }

        private bool CanSend()
        {
            if (string.IsNullOrEmpty(InputText) || string.IsNullOrEmpty(Channel))
                return false;
            else
                return true;
        }

        private void OnSendAll()
        {
            WeakReferenceMessenger.Default.Send(new StringMessage((this, InputText)));
        }

        private bool CanSendAll()
        {
            if (string.IsNullOrEmpty(InputText))
                return false;
            else
                return true;
        }
        #endregion

        public SenderViewModel()
        {
            SendCommand = new DelegateCommand(OnSend, CanSend);
            SendAllCommand = new DelegateCommand(OnSendAll, CanSendAll);
        }
    }
}
