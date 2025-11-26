using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGuid.Models
{
    public partial class SchoolModel : ObservableObject
    {
        [ObservableProperty]
        public string id = "";

        [ObservableProperty]
        public string name = "";

        [ObservableProperty]
        public string address = "";

        public SchoolModel()
        {
            Id = Guid.NewGuid().ToString();
            Name = "";
            Address = "";
        }
    }
}
