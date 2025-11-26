using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfGuid.Models;

namespace WpfGuid.ViewModels
{
    public partial class SchoolTabViewModel : BasicTabViewModel
    {
        [ObservableProperty]
        private ObservableCollection<SchoolModel> schoolModels = null;

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

        [ObservableProperty]
        private string schoolId = "";

        [ObservableProperty]
        private string schoolName = "";

        [ObservableProperty]
        private string schoolAddress = "";

        [RelayCommand]
        private void Add()
        {
            SchoolModels?.Add(new SchoolModel()
            {
                Name = SchoolName,
                Address = SchoolAddress,
            });
            InitEdit();
        }

        [RelayCommand]
        private void Delete()
        {
            SchoolModel item = SchoolModels.FirstOrDefault(item =>
            {
                if (item.Id == SchoolId)
                    return true;

                return false;
            });
            if (item != null)
                schoolModels.Remove(item);

            InitEdit();
        }

        [RelayCommand]
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

        public SchoolTabViewModel()
        {
            Header = "학교";
        }
    }
}
