using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfDialogService
{
    public class UserDialogViewModel : BindableBase, IDialogViewModel
    {
        public event EventHandler<bool?>? RequestClose = null;

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public UserDialogViewModel()
        {
            OkCommand = new DelegateCommand(() => RequestClose?.Invoke(this, true));    // DialogResult = true

            CancelCommand = new DelegateCommand(() => RequestClose?.Invoke(this, false));   // DialogResult = false
        }
    }
}
