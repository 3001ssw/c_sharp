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
        private string inputText = "";
        public string InputText
        {
            get => inputText;
            set
            {
                SetProperty(ref inputText, value);
                InputTextSendCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand InputTextSendCommand { get; private set; }
        #endregion

        public SenderViewModel()
        {
            InputTextSendCommand = new DelegateCommand(OnInputTextSend, CanInputTextSend);
        }

        private void OnInputTextSend()
        {
            WeakReferenceMessenger.Default.Send(new MyMessage((this, InputText)));
            WeakReferenceMessenger.Default.Send(new MyMessage((this, InputText)), "channel1");
        }

        private bool CanInputTextSend()
        {
            if (string.IsNullOrEmpty(InputText))
                return false;
            else
                return true;
        }
    }
}
