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
        private string _addName = "홍길동";
        public string AddName // 이름 입력
        {
            get => _addName;
            set
            {
                _addName = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private int _addAge = 0;
        public int AddAge // 나이 입력
        {
            get => _addAge;
            set
            {
                _addAge = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public Command AddCommand { get; } // Add 버튼 Command

        public ObservableCollection<Person> People { get; } // 리스트에 표시되는 전체 정보

        private Person _selectedPerson;
        public Person SelectedPerson // 리스트에서 선택한 정보
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            AddCommand = new Command(OnAdd, CanExecuteAdd);

            People = new ObservableCollection<Person>();
        }

        private void OnAdd()
        {
            Person person = new Person(AddName, AddAge);
            People.Add(person);
            AddName = "";
            AddAge = 0;
        }

        private bool CanExecuteAdd()
        {
            return !string.IsNullOrEmpty(AddName);
        }
    }
}
