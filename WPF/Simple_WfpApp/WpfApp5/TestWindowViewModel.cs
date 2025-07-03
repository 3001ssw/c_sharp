using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp5.Dialogs;
using WpfApp5.Util;

namespace WpfApp5
{
    class TestWindowViewModel : Notifier
    {
        private readonly IDialogService _dialogService;

        public Command PopupMessage { get; set; }
        public Command BtnClickOk { get; set; }
        public Command BtnClickCancel { get; set; }

        public string InsertText { get; set; }

        //<TextBox Text = "{Binding InsertText, UpdateSourceTrigger=PropertyChanged}"/>
        //<Button Content="Ok" Command="{Binding BtnClickOk}"/>
        //<Button Content = "Cancel" Command="{Binding BtnClickCancel}"/>

        public TestWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            PopupMessage = new Command(OnPopupMessage, CanExecutePopupMessage);
            BtnClickOk = new Command(OnBtnClickOk, CanExecuteBtnClickOk);
            BtnClickCancel = new Command(OnBtnClickCancel, CanExecuteBtnClickCancel);
        }

        private void OnPopupMessage()
        {
            MessageBox.Show("hi");
        }

        private bool CanExecutePopupMessage()
        {
            return true;
        }

        private void OnBtnClickOk()
        {
            _dialogService.CloseDialog(true);
        }

        private bool CanExecuteBtnClickOk()
        {
            return true;
        }

        private void OnBtnClickCancel()
        {
            _dialogService.CloseDialog(false);
        }

        private bool CanExecuteBtnClickCancel()
        {
            return true;
        }
    }
}
