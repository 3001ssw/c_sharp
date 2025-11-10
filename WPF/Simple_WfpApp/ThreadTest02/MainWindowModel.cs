using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest02
{
    public class MainWindowModel
    {
        public int Count { get; set; } = 0;

        public ObservableCollection<Item> ListMsg { get; set; } = new ObservableCollection<Item>();

        public MainWindowModel()
        {
            
        }

        public int AddListMsg(string message = "", string description = "")
        {
            Item item = new Item(ListMsg.Count, message, description);
            ListMsg.Add(item);

            return ListMsg.Count;
        }
    }
}
