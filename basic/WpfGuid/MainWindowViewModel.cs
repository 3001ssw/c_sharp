using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfGuid.ViewModels;

namespace WpfGuid
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public static MainWindowModel AppData => mainModel;

        [ObservableProperty]
        private static MainWindowModel mainModel = new MainWindowModel();

        [ObservableProperty]
        private ObservableCollection<BasicTabViewModel> tabs = new ObservableCollection<BasicTabViewModel>();

        [ObservableProperty]
        private BasicTabViewModel selectedTab = null;

        public MainWindowViewModel()
        {
            AppData.LoadData();

            SchoolTabViewModel schoolvm = new SchoolTabViewModel();
            schoolvm.SchoolModels = mainModel.SchoolModels;
            tabs.Add(schoolvm);

            selectedTab = schoolvm;
        }

        [RelayCommand]
        private void Save()
        {
            AppData.SaveData();
        }

        [RelayCommand]
        private void CloseWindow()
        {
            (Application.Current.MainWindow)?.Close();
        }
    }
}
