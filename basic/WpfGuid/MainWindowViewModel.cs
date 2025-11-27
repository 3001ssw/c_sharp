using System.Collections.ObjectModel;
using System.Windows;
using WpfGuid.ViewModels;

namespace WpfGuid
{
    public partial class MainWindowViewModel : BindableBase
    {
        private MainWindowModel mainModel = new MainWindowModel();
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
            MainModel.LoadData();

            SchoolTabViewModel schoolVm = new SchoolTabViewModel();
            schoolVm.SchoolModels = mainModel.SchoolModels;
            Tabs.Add(schoolVm);

            TeacherTabViewModel teacherVm = new TeacherTabViewModel();
            teacherVm.TeacherModels = mainModel.TeacherModels;
            Tabs.Add(teacherVm);

            SelectedTab = schoolVm;
        }

        private void Save()
        {
            MainModel.SaveData();
        }

        private void CloseWindow()
        {
            (Application.Current.MainWindow)?.Close();
        }
    }
}
