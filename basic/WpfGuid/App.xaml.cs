using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfGuid
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindowModel? Data
        {
            get
            {
                MainWindow? v = Current?.MainWindow as MainWindow;
                MainWindowViewModel? vm = v?.DataContext as MainWindowViewModel;
                return vm?.MainModel;
            }
        }
    }

}
