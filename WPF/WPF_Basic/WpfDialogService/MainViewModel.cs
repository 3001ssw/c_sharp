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
        public MainViewModel()
        {
            PopupDialogCommand = new DelegateCommand(OnPopupDialog, CanPopupDialog);
        }

        private void OnPopupDialog()
        {
            var dialog = new DialogService();
            var vm = new UserDialogViewModel();

            bool? result = dialog.ShowDialog(this, vm);
            Debug.WriteLine($"result: {result}");
        }

        private bool CanPopupDialog()
        {
            return true;
        }

    }
}
