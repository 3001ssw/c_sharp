using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest01
{
    public class MainWindowModel
    {
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

        public MainWindowModel()
        {

        }
    }
}
