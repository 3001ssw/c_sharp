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
        public DelegateCommand ShowDialogCommand { get; }
        public DelegateCommand ShowCommand { get; }
        public DelegateCommand ShowDialogActivatorCommand { get; }
        public DelegateCommand ShowDialogDataTemplateCommand { get; }
        public MainViewModel()
        {
            ShowDialogCommand = new DelegateCommand(OnShowDialog, CanShowDialog);
            ShowCommand = new DelegateCommand(OnShow, CanShow);
            ShowDialogActivatorCommand = new DelegateCommand(OnShowDialogActivator, CanShowDialogActivator);
            ShowDialogDataTemplateCommand = new DelegateCommand(OnShowDialogDataTemplate, CanShowDialogDataTemplate);
        }

        private void OnShowDialogActivator()
        {
            var dialog = new DialogService();
            var vm = new UserDialogViewModel();

            bool? result = dialog.ShowDialogActivator(vm);
            Debug.WriteLine($"result: {result}");
        }

        private bool CanShowDialogActivator()
        {
            return true;
        }

        private void OnShowDialogDataTemplate()
        {
            var dialog = new DialogService();
            var vm = new UserControlViewModel();

            bool? result = dialog.ShowDialogDataTemplate(vm);
            Debug.WriteLine($"result: {result}");
        }

        private bool CanShowDialogDataTemplate()
        {
            return true;
        }

        private void OnShowDialog()
        {
            var dialog = new DialogService();
            var vm = new UserDialogViewModel();

            bool? result = dialog.ShowDialog<UserDialogView>(vm);
            Debug.WriteLine($"result: {result}");
        }

        private bool CanShowDialog()
        {
            return true;
        }

        private void OnShow()
        {
            var dialog = new DialogService();
            var vm = new UserDialogViewModel();

            dialog.Show<UserDialogView>(vm);
        }

        private bool CanShow()
        {
            return true;
        }

    }
}
