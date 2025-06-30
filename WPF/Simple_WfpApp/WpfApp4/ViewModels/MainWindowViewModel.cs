using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp4.Models;
using WpfApp4.Util;

namespace WpfApp4.ViewModels
{
    public class MainWindowViewModel : Notifier
    {
        private string name = "홍길동";
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private int age = 0;
        public int Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged(nameof(Age));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public Command SaveCommand { get; }


        public ObservableCollection<Person> People { get; }


        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        public MainWindowViewModel()
        {
            SaveCommand = new Command(OnSave, CanExecuteSave);

            People = new ObservableCollection<Person>();
        }

        private void OnSave()
        {
            Person person = new Person(Name, Age);
            People.Add(person);
            Name = "";
            Age = 0;
        }

        private bool CanExecuteSave()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }

}
