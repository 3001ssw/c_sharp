using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDialogService
{
    public class MainViewModel : BindableBase
    {
        public DelegateCommand PopupDialogCommand { get; }
        public DelegateCommand PopupDialog2Command { get; }
        public DelegateCommand PopupDialog3Command { get; }
        public MainViewModel()
        {
            PopupDialogCommand = new DelegateCommand(OnPopupDialog, CanPopupDialog);
            PopupDialog2Command = new DelegateCommand(OnPopupDialog2, CanPopupDialog2);
            PopupDialog3Command = new DelegateCommand(OnPopupDialog3, CanPopupDialog3);
        }

        private void OnPopupDialog()
        {
            var dialog = new DialogService();
            var vm = new UserDialogViewModel();

            bool? result = dialog.ShowDialogActivator(vm);
            Debug.WriteLine($"result: {result}");
        }

        private bool CanPopupDialog()
        {
            return true;
        }

        private void OnPopupDialog2()
        {
            var dialog = new DialogService();
            var vm = new UserDialog2ViewModel();

            bool? result = dialog.ShowDialogDataTemplate(vm);
            Debug.WriteLine($"result: {result}");
        }

        private bool CanPopupDialog2()
        {
            return true;
        }

        private void OnPopupDialog3()
        {
            var dialog = new DialogService();
            var vm = new UserDialogViewModel();

            bool? result = dialog.ShowDialog<UserDialogView>(vm);
            Debug.WriteLine($"result: {result}");
        }

        private bool CanPopupDialog3()
        {
            return true;
        }

    }
}
