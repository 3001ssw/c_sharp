using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp2
{
    [POCOViewModel]
    public class MainWindowViewModel
    {
        public virtual string Message { get; set; } = "";


        public MainWindowViewModel()
        {
        }

        public void Click()
        {
            MessageBox.Show($"Message: {Message}");
        }

        public bool CanClick()
        {
            if (string.IsNullOrEmpty(Message))
                return false;
            else
                return true;
        }

        public void ClickParameter(string message)
        {
            MessageBox.Show($"Message: {message}");
        }

        public bool CanClickParameter(string message)
        {
            if (string.IsNullOrEmpty(message))
                return false;
            else
                return true;
        }
    }
}
