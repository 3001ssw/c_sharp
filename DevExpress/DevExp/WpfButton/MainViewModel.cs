using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfButton
{
    public class MainViewModel : ViewModelBase
    {
        public DelegateCommand SimpleButtonCommand { get; }

        public DelegateCommand SplitButtonCommand { get; }
        public DelegateCommand SplitButtonMenu1Command { get; }
        public DelegateCommand SplitButtonMenu2Command { get; }

        public DelegateCommand DropDownButtonMenu1Command { get; }
        public DelegateCommand DropDownButtonMenu2Command { get; }

        public MainViewModel()
        {
            SimpleButtonCommand = new DelegateCommand(OnSimpleButton);

            SplitButtonCommand = new DelegateCommand(OnSplitButton);
            SplitButtonMenu1Command = new DelegateCommand(OnSplitMenu1);
            SplitButtonMenu2Command = new DelegateCommand(OnSplitMenu2);

            DropDownButtonMenu1Command = new DelegateCommand(OnDropDownMenu1);
            DropDownButtonMenu2Command = new DelegateCommand(OnDropDownMenu2);
        }

        private void OnSimpleButton() => MessageBox.Show("SimpleButton 클릭");

        private void OnSplitButton() => MessageBox.Show("SplitButton 클릭");
        private void OnSplitMenu1() => MessageBox.Show("SplitButton Menu 1");
        private void OnSplitMenu2() => MessageBox.Show("SplitButton Menu 2");

        private void OnDropDownMenu1() => MessageBox.Show("DropDownButton Menu 1");
        private void OnDropDownMenu2() => MessageBox.Show("DropDownButton Menu 2");
    }
}
