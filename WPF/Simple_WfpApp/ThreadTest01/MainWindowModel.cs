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

        public ObservableCollection<ListItem> ListMsg { get; set; } = new ObservableCollection<ListItem>();

        public MainWindowModel()
        {
            
        }

        public int AddListMsg(string message = "", string description = "")
        {
            ListItem item = new ListItem(ListMsg.Count, message, description);
            ListMsg.Add(item);

            return ListMsg.Count;
        }
    }
}
