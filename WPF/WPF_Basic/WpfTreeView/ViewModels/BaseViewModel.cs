using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace WpfTreeView.ViewModels
{
    public class BaseViewModel
    {
        public string Name { get; set; } = "";

        public ObservableCollection<BaseViewModel> Children { get; set; } = new ObservableCollection<BaseViewModel>();

        public BaseViewModel()
        {
        }
    }
}
