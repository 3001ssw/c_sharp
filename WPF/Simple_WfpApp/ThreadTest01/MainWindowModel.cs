using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest01
{
    public class MainWindowModel
    {
        public int Count { get; set; } = 0;

        public ObservableCollection<string> ListMsg { get; set; } = new ObservableCollection<string>();

        public MainWindowModel()
        {
            
        }
    }
}
