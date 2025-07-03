using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp5.Dialogs
{
    public class DialogService : IDialogService
    {
        private readonly Window _dialog;
        public DialogService(Window dialog)
        {
            _dialog = dialog;
        }

        //public T OpenDialog<T>(DialogViewModelBase<T> viewModel)
        //{
        //    IDialogWindow window = new TestWindow();
        //    window.DataContext = viewModel;
        //    window.ShowDialog();
        //    return viewModel.DialogResult;
        //}
        public void CloseDialog(bool result = false)
        {
            _dialog.DialogResult = result;
            _dialog.Close();
        }
    }
}
