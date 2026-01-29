using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAction.Interfaces;

namespace WpfAction.Popup.ViewModels
{
    public class ShowDialogViewModel : BindableBase, IDialogViewModel
    {
        #region properties

        public Action<bool?>? DoCloseAction { get; set; }

        #endregion


        #region commands

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        #endregion


        #region constructor
        public ShowDialogViewModel()
        {
            OkCommand = new DelegateCommand(() => DoCloseAction?.Invoke(true));
            CancelCommand = new DelegateCommand(() => DoCloseAction?.Invoke(false));
        }
        #endregion
    }
}
