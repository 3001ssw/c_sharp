using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MathLibrary;

namespace WpfAppStatic
{
    public class MainViewModel : BindableBase
    {
        public DelegateCommand AddCommand { get; private set; }
        delegate int AddDelegate(int a, int b);

        public MainViewModel()
        {
            AddCommand = new DelegateCommand(OnAdd);
        }

        private void OnAdd()
        {
            Calculator c = new Calculator();

            int result = c.Add(50, 60);
            MessageBox.Show($"대리자 호출 결과: {result}");
        }
    }
}
