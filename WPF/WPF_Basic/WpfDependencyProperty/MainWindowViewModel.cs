using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDependencyProperty
{
    public class MainWindowViewModel : BindableBase
    {
        private double percent = 0.0;
        public double Percent { get => percent; set => SetProperty(ref percent, value); }

        private bool isChecked = false;
        public bool IsChecked { get => isChecked; set => SetProperty(ref isChecked, value); }
        public MainWindowViewModel()
        {
            
        }
    }
}
