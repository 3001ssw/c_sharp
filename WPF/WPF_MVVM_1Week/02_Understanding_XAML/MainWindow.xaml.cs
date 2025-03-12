using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _02_Understanding_XAML
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MessageBox.Show($"Car Info" + Environment.NewLine +
                $"CarName: {MyCar.CarName}" + Environment.NewLine +
                $"Speed: {MyCar.Speed}" + Environment.NewLine +
                $"Driver Name: {MyCar.Driver.Name}" + Environment.NewLine +
                $"Driver Age: {MyCar.Driver.Age}");
        }
    }
}
