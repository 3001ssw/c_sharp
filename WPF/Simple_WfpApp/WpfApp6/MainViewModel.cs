using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6
{
    public class MainViewModel
    {
        public ObservableCollection<TreeItem> Items { get; set; }

        public MainViewModel()
        {
            Items = new ObservableCollection<TreeItem>
        {
            new TreeItem("Fruit")
            {
                Children = {
                    new TreeItem("Apple"),
                    new TreeItem("Banana")
                }
            },
            new TreeItem("Vegetables")
            {
                Children = {
                    new TreeItem("Carrot"),
                    new TreeItem("Spinach")
                }
            }
        };
        }
    }

}
