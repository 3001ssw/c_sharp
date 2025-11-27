using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfGuid.Models;
using WpfGuid.Views;

namespace WpfGuid.ViewModels
{
    public class TeacherTabViewModel : BasicTabViewModel
    {
        public ObservableCollection<TeacherModel> TeacherModels { get; set; } = null;
        private TeacherModel selectedItem = null;
        public TeacherModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != null)
                {
                    SetProperty(ref selectedItem, value);
                    SelectedId = selectedItem.Id;
                    SelectedName = selectedItem.Name;
                    SelectedAge = selectedItem.Age;
                    SelectedGender = selectedItem.Gender;
                    SelectedPhoneNumber = selectedItem.PhoneNumber;
                    SelectedSchool = selectedItem.School;
                }
                else
                {
                    InitEdit();
                }
            }
        }


        private string selectedId= "";
        public string SelectedId
        {
            get => selectedId;
            set => SetProperty(ref selectedId, value);
        }


        private string selectedName = "";
        public string SelectedName
        {
            get => selectedName;
            set => SetProperty(ref selectedName, value);
        }

        private int selectedAge = 0;
        public int SelectedAge
        {
            get => selectedAge;
            set => SetProperty(ref selectedAge, value);
        }

        private string selectedGender = "";
        public string SelectedGender
        {
            get => selectedGender;
            set => SetProperty(ref selectedGender, value);
        }

        private string selectedPhoneNumber = "";
        public string SelectedPhoneNumber
        {
            get => selectedPhoneNumber;
            set => SetProperty(ref selectedPhoneNumber, value);
        }

        private string selectedSchool = "";
        public string SelectedSchool
        {
            get => selectedSchool;
            set => SetProperty(ref selectedSchool, value);
        }

        public DelegateCommand FindSchoolCommand { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand ModifyCommand { get; }


        public TeacherTabViewModel()
        {
            Header = "선생님";
            FindSchoolCommand = new DelegateCommand(FindSchool);
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete);
            ModifyCommand = new DelegateCommand(Modify);
        }

        private void FindSchool()
        {
            FindSchoolViewModel vm = new FindSchoolViewModel();
            FindSchoolView v = new FindSchoolView()
            {
                DataContext = vm,
            };

            v.ShowDialog();
        }

        private void Add()
        {
            TeacherModels?.Add(new TeacherModel()
            {
                Name = SelectedName,
                Age = SelectedAge,
                Gender = SelectedGender,
                PhoneNumber = SelectedPhoneNumber,
                School = SelectedSchool,
            });

            InitEdit();
        }

        private void Delete()
        {
            TeacherModel item = TeacherModels.FirstOrDefault(item =>
            {
                if (item.Id == SelectedId)
                    return true;
            
                return false;
            });
            if (item != null)
                TeacherModels.Remove(item);

            InitEdit();
        }

        private void Modify()
        {
            TeacherModel item = TeacherModels.FirstOrDefault(item =>
            {
                if (item.Id == SelectedId)
                    return true;
            
                return false;
            });
            
            if (item != null)
            {
                item.Name = SelectedName;
                item.Age = SelectedAge;
                item.Gender = SelectedGender;
                item.PhoneNumber = SelectedPhoneNumber;
                item.School = SelectedSchool;
            }
        }


        private void InitEdit()
        {
            SelectedId = "";
            SelectedName = "";
            SelectedAge = 0;
            SelectedGender = "";
            SelectedPhoneNumber = "";
            SelectedSchool = "";
        }
    }
}
