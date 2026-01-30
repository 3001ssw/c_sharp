using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfDialogService
{
    public class UserControlViewModel : BindableBase, IDialogViewModel
    {
        public Action<bool?>? DoCloseAction { get; set; }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public UserControlViewModel()
        {
            OkCommand = new DelegateCommand(() => DoCloseAction?.Invoke(true), () => { return true; });    // DialogResult = true

            CancelCommand = new DelegateCommand(() => DoCloseAction?.Invoke(false), () => {return true; });   // DialogResult = false
        }
    }
}
