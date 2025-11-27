using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfGuid.Models;

namespace WpfGuid.ViewModels
{
    public class FindSchoolViewModel : BindableBase, IDialogAware
    {
        public ObservableCollection<SchoolModel> SchoolModels { get; private set; }

        public SchoolModel SelectedItem { get; set; } = null;

        public DelegateCommand OkCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public DialogCloseListener RequestClose => throw new NotImplementedException();

        public FindSchoolViewModel()
        {
            SchoolModels = App.Data.SchoolModels;
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Ok()
        {
            // todo .. : 
        }

        private void Cancel()
        {
            // todo .. : 
        }

        public bool CanCloseDialog()
        {
            throw new NotImplementedException();
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
