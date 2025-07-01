using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp5.Util;

namespace WpfApp5
{
    public class MainWindowViewModel : Notifier
    {
        public Command PopupWindow { get; set; }
        public MainWindowViewModel()
        {
            PopupWindow = new Command(OnPopupWindow, CanExecutePopupWindow);
        }

        private void OnPopupWindow()
        {
            TestWindow test = new TestWindow();
            test.ShowDialog();
            MessageBox.Show("Show Dialog TestWindow");
        }

        private bool CanExecutePopupWindow()
        {
            return true;
        }
    }
}
