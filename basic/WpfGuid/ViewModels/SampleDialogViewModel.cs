using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGuid.ViewModels
{
    public class SampleDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Sample Dialog";

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        public DelegateCommand OkCommand { get; }
        public DelegateCommand CancelCommand { get; }

        DialogCloseListener IDialogAware.RequestClose => throw new NotImplementedException();

        public SampleDialogViewModel()
        {
            OkCommand = new DelegateCommand(OnOkClicked);
            CancelCommand = new DelegateCommand(OnCancelClicked);
        }

        private void OnOkClicked()
        {
            var p = new DialogParameters { { "InputText", InputText } };
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, p));
        }

        private void OnCancelClicked()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Message"))
                Message = parameters.GetValue<string>("Message");
        }
    }
}
