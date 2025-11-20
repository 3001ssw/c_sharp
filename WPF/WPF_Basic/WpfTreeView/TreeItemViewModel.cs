using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTreeView
{
    public class TreeItemViewModel
    {
        public string Header { get; set; } = "";
        public ObservableCollection<TreeItemViewModel> Children { get; set; } = new ObservableCollection<TreeItemViewModel>();

        public TreeItemViewModel()
        {
            
        }
    }
}
