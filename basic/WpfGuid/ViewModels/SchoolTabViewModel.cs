using System.Collections.ObjectModel;
using WpfGuid.Models;

namespace WpfGuid.ViewModels
{
    public partial class SchoolTabViewModel : BasicTabViewModel
    {
        public ObservableCollection<SchoolModel> SchoolModels { get; set; } = null;

        private SchoolModel selectedItem = null;
        public SchoolModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != null)
                {
                    SetProperty(ref selectedItem, value);
                    SchoolId = selectedItem.Id;
                    SchoolName = selectedItem.Name;
                    SchoolAddress = selectedItem.Address;
                }
                else
                {
                    SchoolId = "";
                    SchoolName = "";
                    SchoolAddress = "";
                }
            }
        }

        private string schoolId = "";

        public string SchoolId { get => schoolId; set => SetProperty(ref schoolId, value); }

        private string schoolName = "";

        public string SchoolName { get => schoolName; set => SetProperty(ref schoolName, value); }

        private string schoolAddress = "";

        public string SchoolAddress { get => schoolAddress; set => SetProperty(ref schoolAddress, value); }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand ModifyCommand { get; }

        public SchoolTabViewModel()
        {
            Header = "학교";
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete);
            ModifyCommand = new DelegateCommand(Modify);
        }

        private void Add()
        {
            SchoolModels?.Add(new SchoolModel()
            {
                Name = SchoolName,
                Address = SchoolAddress,
            });
            InitEdit();
        }

        private void Delete()
        {
            SchoolModel item = SchoolModels.FirstOrDefault(item =>
            {
                if (item.Id == SchoolId)
                    return true;

                return false;
            });
            if (item != null)
                SchoolModels.Remove(item);

            InitEdit();
        }

        private void Modify()
        {
            SchoolModel item = SchoolModels.FirstOrDefault(item =>
            {
                if (item.Id == SchoolId)
                    return true;

                return false;
            });

            if (item != null)
            {
                item.Name = SchoolName;
                item.Address = SchoolAddress;
            }
        }

        private void InitEdit()
        {
           SchoolName = "";
           SchoolAddress = "";
        }
    }
}
