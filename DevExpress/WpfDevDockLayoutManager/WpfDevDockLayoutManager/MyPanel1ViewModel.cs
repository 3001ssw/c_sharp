using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDevDockLayoutManager
{
    public class MyPanel1ViewModel : MyPanelViewModel
    {
        #region fields, properties

        private string display = "";
        public string Display { get => display; set => SetValue(ref display, value); }
        #endregion

        public MyPanel1ViewModel()
        {

        }
    }
}
