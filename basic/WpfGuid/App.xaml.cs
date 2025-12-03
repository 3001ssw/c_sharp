using Prism.Dialogs;
using Prism.Ioc;
using Prism.Unity;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfGuid.ViewModels;
using WpfGuid.Views;

namespace WpfGuid
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        //public static MainWindowModel? Data
        //{
        //    get
        //    {
        //        MainWindow? v = Current?.MainWindow as MainWindow;
        //        MainWindowViewModel? vm = v?.DataContext as MainWindowViewModel;
        //        return vm?.MainModel;
        //    }
        //}
        protected override Window CreateShell() => ContainerProvider.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<SampleDialog, SampleDialogViewModel>();
        }
    }

}
