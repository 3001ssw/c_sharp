using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp5.Dialogs
{
    public abstract class DialogViewModelBase<T>
    {
        private readonly IDialogService _dialogService;
        public DialogViewModelBase(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
    }
}
