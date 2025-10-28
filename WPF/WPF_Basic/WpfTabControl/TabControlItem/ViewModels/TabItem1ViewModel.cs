using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Util;

namespace WpfTabControl.TabControlItem.ViewModels
{
    public class TabItem1ViewModel : TabItemBaseViewModel
    {
        private string txtBlock = "";
        public string TxtBlock
        {
            get => txtBlock;
            set
            {
                txtBlock = value;
                OnPropertyChangedAll();
            }
        }

        public Command ButtonCommand { get; set; }

        public TabItem1ViewModel()
        {
            ButtonCommand = new Command(OnButtonCommand, OnCanExeButtonCommand);
        }

        private void OnButtonCommand()
        {
            MessageBox.Show(TxtBlock);
        }

        private bool OnCanExeButtonCommand()
        {
            return !string.IsNullOrEmpty(TxtBlock);
        }
    }
}
