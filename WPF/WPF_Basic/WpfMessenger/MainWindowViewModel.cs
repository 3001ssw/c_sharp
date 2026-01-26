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
        private SenderViewModel senderVM = new SenderViewModel();
        public SenderViewModel SenderVM { get => senderVM; set => SetProperty(ref senderVM, value); }

        private Receiver1ViewModel receiver1VM = new Receiver1ViewModel();
        public Receiver1ViewModel Receiver1VM { get => receiver1VM; set => SetProperty(ref receiver1VM, value); }

        private Receiver2ViewModel receiver2VM = new Receiver2ViewModel();
        public Receiver2ViewModel Receiver2VM { get => receiver2VM; set => SetProperty(ref receiver2VM, value); }
        #endregion

        public MainWindowViewModel()
        {
            
        }
    }
}
