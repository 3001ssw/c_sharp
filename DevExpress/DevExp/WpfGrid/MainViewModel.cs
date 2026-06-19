using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGrid
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Employee> Employees
        {
            get { return GetValue<ObservableCollection<Employee>>(); }
            set { SetValue(value); }
        }

        public Employee SelectedEmployee
        {
            get { return GetValue<Employee>(); }
            set { SetValue(value); }
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

        public MainViewModel()
        {
            Employees = new ObservableCollection<Employee>
            {
                new Employee { Id = 1, Name = "김철수", Department = "개발팀", JoinDate = new DateTime(2020, 3, 1), Salary = 45000000 },
                new Employee { Id = 2, Name = "이영희", Department = "기획팀", JoinDate = new DateTime(2019, 7, 15), Salary = 48000000 },
                new Employee { Id = 3, Name = "박민수", Department = "개발팀", JoinDate = new DateTime(2021, 1, 10), Salary = 42000000 },
            };

            AddCommand = new DelegateCommand(OnAdd);
            DeleteCommand = new DelegateCommand(OnDelete, CanDelete);
        }

        private void OnAdd()
        {
            int newId = Employees.Count > 0 ? Employees[Employees.Count - 1].Id + 1 : 1;
            Employees.Add(new Employee
            {
                Id = newId,
                Name = "신규직원",
                Department = "미배정",
                JoinDate = DateTime.Today,
                Salary = 0
            });
        }

        private void OnDelete()
        {
            if (SelectedEmployee != null)
                Employees.Remove(SelectedEmployee);
        }

        private bool CanDelete()
        {
            return SelectedEmployee != null;
        }
    }

    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }
    }
}
