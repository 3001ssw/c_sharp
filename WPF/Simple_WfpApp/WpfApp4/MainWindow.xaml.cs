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
using WpfApp4.ViewModels;

namespace WpfApp4
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ShowInfo()
        {

        }

        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            // UserControl에서 ViewModel 가져오기
            PersonViewModel vm = PersonViewControl.DataContext as PersonViewModel;
            if (vm != null)
            {
                MessageBox.Show($"이름: {vm.Name}\n나이: {vm.Age}", "Person 정보");
            }
        }
    }
}
