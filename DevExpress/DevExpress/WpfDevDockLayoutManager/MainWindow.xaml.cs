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

namespace WpfDevDockLayoutManager
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string LayoutFilePath =
            System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout.xml");

        public MainWindow()
        {
            InitializeComponent();

            if (DataContext is MainViewModel vm)
            {
                vm.SaveLayoutAction = () => dockManager.SaveLayoutToXml(LayoutFilePath);
                vm.LoadLayoutAction = () =>
                {
                    if (System.IO.File.Exists(LayoutFilePath))
                        dockManager.RestoreLayoutFromXml(LayoutFilePath);
                };
            }
        }
    }
}
