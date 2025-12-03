using System.Configuration;
using System.Data;
using System.Windows;
using YourApp.ViewModels;

namespace WpfPrismDialogApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<MyDialog, MyDialogViewModel>();
        }
    }

}
