using Serialize.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Serialize
{
    public class MainWindowViewModel : Notifier
    {
        public ObservableCollection<PersonModel> People { get; set; }

        public PersonModel _selectedPerson;
        public PersonModel SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        public Command ISaveXml { get; private set; }
        public Command ILoadXml { get; private set; }

        public MainWindowViewModel()
        {
            People = new ObservableCollection<PersonModel>();
            People.Add(new PersonModel("김", 10));
            People.Add(new PersonModel("이", 20));
            People.Add(new PersonModel("박", 30));
            People.Add(new PersonModel("최", 40));

            ISaveXml = new Command(SaveXml, CanExecuteSaveXml);
            ILoadXml = new Command(LoadXml, CanExecuteLoadXml);
        }
        private void SaveXml()
        {
            SaveXml(".\\text.xml");
        }

        private bool CanExecuteSaveXml()
        {
            bool bRet = People.Count <= 0 ? false : true;
            
            return bRet;
        }

        private void LoadXml()
        {
            LoadFromXml(".\\text.xml");
        }

        private bool CanExecuteLoadXml()
        {
            bool bRet = true;
            return bRet;
        }

        private void SaveXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PersonModel>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, People);
            }
        }

        private void LoadFromXml(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            var serializer = new XmlSerializer(typeof(ObservableCollection<PersonModel>));
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                People.Clear();
                ObservableCollection<PersonModel> LoadPeople = (ObservableCollection<PersonModel>)serializer.Deserialize(stream);
                foreach (PersonModel Person in LoadPeople)
                    People.Add(Person);
            }
        }
    }
}
