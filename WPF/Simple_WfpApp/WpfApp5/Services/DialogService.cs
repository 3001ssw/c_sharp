using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp5.Services
{
    public class DialogService : IDialogService
    {
        private readonly Window _dialog;
        public DialogService(Window dialog)
        {
            _dialog = dialog;
        }

        public bool? ShowDialog(object viewModel)
        {
            string viewTypeName = viewModel.GetType().FullName.Replace("ViewModel", "View");
            var viewAssembly = viewModel.GetType().Assembly;
            var viewType = viewAssembly.GetType(viewTypeName);

            if (viewType == null)
                throw new InvalidOperationException("View를 찾을 수 없습니다.");

            var window = (Window)Activator.CreateInstance(viewType);
            window.DataContext = viewModel;

            var closable = viewModel as IDialogResultViewModel;
            if (closable != null)
            {
                closable.RequestClose += result =>
                {
                    window.DialogResult = result;
                    window.Close();
                };
            }

            return window.ShowDialog();
        }

        public void CloseDialog(bool result = false)
        {
            _dialog.DialogResult = result;
            _dialog.Close();
        }
    }
}
