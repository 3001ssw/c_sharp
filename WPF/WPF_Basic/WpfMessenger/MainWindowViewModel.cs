using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.ViewModels;

namespace WpfMessenger
{

    public partial class MainWindowViewModel : BindableBase
    {
        #region fields, properties
        private SendViewModel sendVM = new SendViewModel();
        public SendViewModel SendVM { get => sendVM; set => SetProperty(ref sendVM, value); }

        private ReceiveViewModel receiveVM = new ReceiveViewModel();
        public ReceiveViewModel ReceiveVM { get => receiveVM; set => SetProperty(ref receiveVM, value); }

        private ReceiveViewModel receiveVM2 = new ReceiveViewModel();
        public ReceiveViewModel ReceiveVM2 { get => receiveVM2; set => SetProperty(ref receiveVM2, value); }
        #endregion

        public MainWindowViewModel()
        {
            
        }
    }
}
