using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp5.Util;

namespace WpfApp5
{
    class TestWindowViewModel : Notifier
    {
        public Command PopupMessage { get; set; }
        public TestWindowViewModel()
        {
            PopupMessage = new Command(OnPopupMessage, CanExecutePopupMessage);
        }

        private void OnPopupMessage()
        {
            MessageBox.Show("hi");
        }

        private bool CanExecutePopupMessage()
        {
            return true;
        }
    }
}
