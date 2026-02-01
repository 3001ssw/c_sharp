using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTask.ViewModels
{
    public class TabItemBaseViewModel : BindableBase
    {
        #region fields, properties

        private string header = "";
        public string Header { get => header; set => SetProperty(ref header, value); }

        #endregion

        #region constructor
        public TabItemBaseViewModel()
        {

        }
        #endregion
    }
}
