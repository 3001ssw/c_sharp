using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.Dialogs
{
    public interface IDialogService
    {
        //T OpenDialog<T>(DialogViewModelBase<T> viewModel);
        //void ShowMessage(string message, string title);
        void CloseDialog(bool result = false);
    }
}
