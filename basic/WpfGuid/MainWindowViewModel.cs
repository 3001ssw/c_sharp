using System.Collections.ObjectModel;
using System.Windows;
using WpfGuid.ViewModels;

namespace WpfGuid
{
    public partial class MainWindowViewModel : BindableBase
    {
        public static MainWindowModel AppData => mainModel;

        private static MainWindowModel mainModel = new MainWindowModel();
        public MainWindowModel MainModel
        {
            get => mainModel;
            set => SetProperty(ref mainModel, value);
        }

        public ObservableCollection<BasicTabViewModel> Tabs { get; set; } = new ObservableCollection<BasicTabViewModel>();

        public BasicTabViewModel SelectedTab { get; set; } = null;

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CloseWindowCommand { get; }

        public MainWindowViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CloseWindowCommand = new DelegateCommand(CloseWindow);
            AppData.LoadData();

            SchoolTabViewModel schoolvm = new SchoolTabViewModel();
            schoolvm.SchoolModels = mainModel.SchoolModels;
            Tabs.Add(schoolvm);

            SelectedTab = schoolvm;
        }

        private void Save()
        {
            AppData.SaveData();
        }

        private void CloseWindow()
        {
            (Application.Current.MainWindow)?.Close();
        }
    }
}
