using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDialogService
{
    public class MainViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        public DelegateCommand NotMVVMCommand { get; }
        public DelegateCommand ShowDialogCommand { get; }
        public DelegateCommand ShowCommand { get; }
        public DelegateCommand ShowDialogActivatorCommand { get; }
        public DelegateCommand ShowDialogDataTemplateCommand { get; }
        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            NotMVVMCommand = new DelegateCommand(OnNotMVVM);
            ShowDialogCommand = new DelegateCommand(OnShowDialog);
            ShowCommand = new DelegateCommand(OnShow);
            ShowDialogActivatorCommand = new DelegateCommand(OnShowDialogActivator);
            ShowDialogDataTemplateCommand = new DelegateCommand(OnShowDialogDataTemplate);
        }

        private void OnNotMVVM()
        {
            UserDialogView v = new UserDialogView();
            UserDialogViewModel vm = new UserDialogViewModel();
            v.DataContext = vm;
            v.Owner = Application.Current.MainWindow;
            v.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            v.ShowDialog();
        }

        private void OnShowDialog()
        {
            var dialog = new DialogService();
            var vm = new UserDialogViewModel();

            bool? result = _dialogService.ShowDialog<UserDialogView>(vm);
            Debug.WriteLine($"result: {result}");
        }

        private void OnShow()
        {
            var vm = new UserDialogViewModel();

            _dialogService.Show<UserDialogView>(vm);
        }

        private void OnShowDialogActivator()
        {
            var vm = new UserDialogViewModel();

            bool? result = _dialogService.ShowDialogActivator(vm);
            Debug.WriteLine($"result: {result}");
        }

        private void OnShowDialogDataTemplate()
        {
            var vm = new UserControlViewModel();

            bool? result = _dialogService.ShowDialogDataTemplate(vm);
            Debug.WriteLine($"result: {result}");
        }

    }
}
