using System;
using System.Collections.Generic;
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
        private Person _person;
        public Command SaveCommand { get; }

        public MainWindowViewModel()
        {
            _person = new Person { Name = "홍길동", Age = 30 };
            SaveCommand = new Command(OnSave, CanExecuteSave);
        }

        public string Name
        {
            get => _person.Name;
            set
            {
                if (_person.Name != value)
                {
                    _person.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int Age
        {
            get => _person.Age;
            set
            {
                if (_person.Age != value)
                {
                    _person.Age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        private void OnSave()
        {
            MessageBox.Show($"이름: {Name}\n나이: {Age}", "Person 정보");
        }

        private bool CanExecuteSave()
        {
            return true;
        }
    }

}
