using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGuid.ViewModels
{
    public partial class BasicTabViewModel : ObservableObject
    {
        [ObservableProperty]
        private string header = "";

        public BasicTabViewModel()
        {
            
        }
    }
}
