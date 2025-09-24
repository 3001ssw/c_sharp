using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Util;

namespace WpfConverter
{
    public class MainWindowViewModel : Notifier
    {
        private bool _checked = false;
        public bool Checked { get => _checked; set => OnPropertyChanged(ref _checked, value); }
        public MainWindowViewModel()
        {
            
        }
    }
}
