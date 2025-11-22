using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Message
        {
            get => GetProperty(() => Message);
            set => SetProperty(() => Message, value);
        }

        public ICommand ClickCommand { get; }
        public ICommand ClickParameterCommand { get; }

        public MainWindowViewModel()
        {
            ClickCommand = new DelegateCommand(OnClickCommand, OnCanClickCommand);
            ClickParameterCommand = new DelegateCommand<string>(OnClickParameterCommand, OnCanClickParameterCommand);
        }

        private void OnClickCommand()
        {
            MessageBox.Show($"Message: {Message}");
        }

        private bool OnCanClickCommand()
        {
            if (string.IsNullOrEmpty(Message))
                return false;
            else
                return true;
        }

        private void OnClickParameterCommand(string message)
        {
            MessageBox.Show($"Message: {message}");
        }

        private bool OnCanClickParameterCommand(string message)
        {
            if (string.IsNullOrEmpty(message))
                return false;
            else
                return true;
        }
    }
}
