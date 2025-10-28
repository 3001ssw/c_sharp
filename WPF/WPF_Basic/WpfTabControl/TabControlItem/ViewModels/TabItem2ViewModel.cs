using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTabControl.TabControlItem.ViewModels
{
    public class TabItem2ViewModel : TabItemBaseViewModel
    {
        private string insertText = "";
        public string InsertText
        {
            get => insertText;
            set
            {
                insertText = value;
                OnPropertyChangedAll();
            }
        }

        public TabItem2ViewModel()
        {
            
        }
    }
}
