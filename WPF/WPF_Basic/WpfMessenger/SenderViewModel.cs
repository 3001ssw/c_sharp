using CommunityToolkit.Mvvm.Messaging;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfMessenger
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
            WeakReferenceMessenger.Default.Send(new MyMessage(InputText));
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
