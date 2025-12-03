namespace YourApp.ViewModels
{
    public class MyDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "테스트 대화상자";

        // Prism 9 방식
        public DialogCloseListener RequestClose { get; }

        public MyDialogViewModel()
        {
            RequestClose = new DialogCloseListener();

            OkCommand = new DelegateCommand(OnOk);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private string _userInput;
        public string UserInput
        {
            get => _userInput;
            set => SetProperty(ref _userInput, value);
        }

        public DelegateCommand OkCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public bool CanCloseDialog() => true;

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("message"))
                Message = parameters.GetValue<string>("message");

            if (parameters.ContainsKey("default"))
                UserInput = parameters.GetValue<string>("default");
        }

        public void OnDialogClosed() { }

        private void OnOk()
        {
            var resultParams = new DialogParameters
            {
                { "confirmed", true },
                { "value", UserInput }
            };
            var result = new DialogResult
            {
                Result = ButtonResult.OK,
                Parameters = resultParams
            };

            RequestClose.Invoke(result);
        }

        private void OnCancel()
        {
            var resultParams = new DialogParameters
            {
                { "confirmed", false }
            };
            var result = new DialogResult
            {
                Result = ButtonResult.Cancel,
                Parameters = resultParams
            };

            RequestClose.Invoke(result);
        }
    }
}
