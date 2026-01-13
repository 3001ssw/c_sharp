using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger
{

    public partial class MainWindowViewModel : BindableBase
    {
        #region fields, properties
        private SenderViewModel senderVM = new SenderViewModel();
        public SenderViewModel SenderVM { get => senderVM; set => SetProperty(ref senderVM, value); }

        private ReceiveViewModel receiverVM = new ReceiveViewModel();
        public ReceiveViewModel ReceiverVM { get => receiverVM; set => SetProperty(ref receiverVM, value); }

        private ReceiveViewModel otherReceiverVM = new ReceiveViewModel();
        public ReceiveViewModel OtherReceiverVM { get => otherReceiverVM; set => SetProperty(ref otherReceiverVM, value); }
        #endregion

        public MainWindowViewModel()
        {
            
        }
    }
}
