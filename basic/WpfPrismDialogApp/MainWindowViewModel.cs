using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPrismDialogApp
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;

        public DelegateCommand OpenDialogCommand { get; }

        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            OpenDialogCommand = new DelegateCommand(OpenDialog);
        }

        private void OpenDialog()
        {
            var parameters = new DialogParameters
        {
            { "message", "정말 실행할까요?" },
            { "default", "기본값" }
        };

            _dialogService.ShowDialog("MyDialog", parameters, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    bool confirmed = result.Parameters.GetValue<bool>("confirmed");
                    string value = result.Parameters.GetValue<string>("value");

                    // TODO: 이후 처리
                }
            });
        }
    }
}
