using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAction.Interfaces;
using WpfAction.Popup.ViewModels;
using WpfAction.Popup.Views;

namespace WpfAction
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private DialogService _dialogService = new DialogService();

        private ObservableCollection<MessageViewModel> messages = new ObservableCollection<MessageViewModel>();
        public ObservableCollection<MessageViewModel> Messages { get => messages; set => SetProperty(ref messages, value); }
        #endregion

        #region commands
        public DelegateCommand ShowCommand { get; private set; }
        public DelegateCommand ShowDialogCommand { get; private set; }
        #endregion

        #region command methods
        private void OnShow()
        {
            ShowViewModel vm = new ShowViewModel();
            AddMessage($"[{vm.GetType()}]이 생성되었습니다.");
            _dialogService.Show<ShowView>(vm, OnResultShow);
        }

        private void OnResultShow(IDialogViewModel vm, bool? result)
        {
            AddMessage($"[{vm.GetType()}]이 종료되었습니다.(result: {result})");
        }

        private bool CanShow()
        {
            return true;
        }
        private void OnShowDialog()
        {
            ShowDialogViewModel vm = new ShowDialogViewModel();
            AddMessage($"[{vm.GetType()}]이 생성되었습니다.");
            bool? result = _dialogService.ShowDialog<ShowDialogView>(vm);
            AddMessage($"[{vm.GetType()}]이 종료되었습니다.(result: {result})");

        }

        private bool CanShowDialog()
        {
            return true;
        }
        #endregion

        #region functions
        private void AddMessage(string msg)
        {
            Messages.Add(new MessageViewModel()
            {
                Message = msg,
            });
        }

        #endregion




        #region constructor
        public MainViewModel()
        {
            ShowCommand = new DelegateCommand(OnShow, CanShow);
            ShowDialogCommand = new DelegateCommand(OnShowDialog, CanShowDialog);
        }
        #endregion
    }
}
