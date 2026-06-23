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
                new Employee { Id = 4, Name = "김민준", Department = "총무팀", JoinDate = new DateTime(2021, 3, 13), Salary = 60000000 },
                new Employee { Id = 5, Name = "이서연", Department = "마케팅팀", JoinDate = new DateTime(2023, 9, 9), Salary = 42000000 },
                new Employee { Id = 6, Name = "박지훈", Department = "재무팀", JoinDate = new DateTime(2021, 11, 6), Salary = 48000000 },
                new Employee { Id = 7, Name = "최수아", Department = "고객지원팀", JoinDate = new DateTime(2018, 9, 3), Salary = 55000000 },
                new Employee { Id = 8, Name = "정우진", Department = "개발팀", JoinDate = new DateTime(2018, 4, 11), Salary = 40000000 },
                new Employee { Id = 9, Name = "강하은", Department = "기획팀", JoinDate = new DateTime(2022, 5, 31), Salary = 60000000 },
                new Employee { Id = 10, Name = "윤도현", Department = "영업팀", JoinDate = new DateTime(2023, 4, 1), Salary = 48000000 },
                new Employee { Id = 11, Name = "임지원", Department = "인사팀", JoinDate = new DateTime(2023, 8, 19), Salary = 42000000 },
                new Employee { Id = 12, Name = "한소희", Department = "총무팀", JoinDate = new DateTime(2021, 3, 11), Salary = 55000000 },
                new Employee { Id = 13, Name = "오민서", Department = "마케팅팀", JoinDate = new DateTime(2019, 10, 10), Salary = 40000000 },
                new Employee { Id = 14, Name = "신예준", Department = "재무팀", JoinDate = new DateTime(2016, 11, 4), Salary = 40000000 },
                new Employee { Id = 15, Name = "홍채원", Department = "고객지원팀", JoinDate = new DateTime(2022, 8, 16), Salary = 60000000 },
                new Employee { Id = 16, Name = "문준혁", Department = "개발팀", JoinDate = new DateTime(2017, 5, 7), Salary = 55000000 },
                new Employee { Id = 17, Name = "배나연", Department = "기획팀", JoinDate = new DateTime(2019, 2, 28), Salary = 38000000 },
                new Employee { Id = 18, Name = "송태양", Department = "영업팀", JoinDate = new DateTime(2017, 8, 2), Salary = 35000000 },
                new Employee { Id = 19, Name = "전유나", Department = "인사팀", JoinDate = new DateTime(2021, 7, 23), Salary = 55000000 },
                new Employee { Id = 20, Name = "노건우", Department = "총무팀", JoinDate = new DateTime(2020, 11, 11), Salary = 48000000 },
                new Employee { Id = 21, Name = "류아린", Department = "마케팅팀", JoinDate = new DateTime(2017, 6, 14), Salary = 42000000 },
                new Employee { Id = 22, Name = "고준서", Department = "재무팀", JoinDate = new DateTime(2016, 9, 27), Salary = 40000000 },
                new Employee { Id = 23, Name = "백다은", Department = "고객지원팀", JoinDate = new DateTime(2015, 1, 15), Salary = 48000000 },
                new Employee { Id = 24, Name = "심재원", Department = "개발팀", JoinDate = new DateTime(2021, 2, 13), Salary = 60000000 },
                new Employee { Id = 25, Name = "안소율", Department = "기획팀", JoinDate = new DateTime(2022, 11, 5), Salary = 40000000 },
                new Employee { Id = 26, Name = "권혁준", Department = "영업팀", JoinDate = new DateTime(2015, 2, 20), Salary = 40000000 },
                new Employee { Id = 27, Name = "엄지수", Department = "인사팀", JoinDate = new DateTime(2015, 4, 14), Salary = 35000000 },
                new Employee { Id = 28, Name = "남도윤", Department = "총무팀", JoinDate = new DateTime(2023, 1, 12), Salary = 50000000 },
                new Employee { Id = 29, Name = "황예린", Department = "마케팅팀", JoinDate = new DateTime(2021, 10, 10), Salary = 35000000 },
                new Employee { Id = 30, Name = "조성민", Department = "재무팀", JoinDate = new DateTime(2015, 6, 15), Salary = 60000000 },
                new Employee { Id = 31, Name = "서채린", Department = "고객지원팀", JoinDate = new DateTime(2022, 7, 26), Salary = 48000000 },
                new Employee { Id = 32, Name = "장민호", Department = "개발팀", JoinDate = new DateTime(2018, 8, 21), Salary = 50000000 },
                new Employee { Id = 33, Name = "구나영", Department = "기획팀", JoinDate = new DateTime(2021, 5, 7), Salary = 55000000 },
                new Employee { Id = 34, Name = "김민준", Department = "영업팀", JoinDate = new DateTime(2019, 12, 22), Salary = 38000000 },
                new Employee { Id = 35, Name = "이서연", Department = "인사팀", JoinDate = new DateTime(2015, 12, 24), Salary = 35000000 },
                new Employee { Id = 36, Name = "박지훈", Department = "총무팀", JoinDate = new DateTime(2021, 8, 5), Salary = 50000000 },
                new Employee { Id = 37, Name = "최수아", Department = "마케팅팀", JoinDate = new DateTime(2015, 8, 1), Salary = 35000000 },
                new Employee { Id = 38, Name = "정우진", Department = "재무팀", JoinDate = new DateTime(2020, 5, 29), Salary = 38000000 },
                new Employee { Id = 39, Name = "강하은", Department = "고객지원팀", JoinDate = new DateTime(2022, 4, 13), Salary = 48000000 },
                new Employee { Id = 40, Name = "윤도현", Department = "개발팀", JoinDate = new DateTime(2016, 3, 22), Salary = 35000000 },
                new Employee { Id = 41, Name = "임지원", Department = "기획팀", JoinDate = new DateTime(2017, 8, 19), Salary = 45000000 },
                new Employee { Id = 42, Name = "한소희", Department = "영업팀", JoinDate = new DateTime(2023, 3, 4), Salary = 60000000 },
                new Employee { Id = 43, Name = "오민서", Department = "인사팀", JoinDate = new DateTime(2022, 11, 4), Salary = 42000000 },
                new Employee { Id = 44, Name = "신예준", Department = "총무팀", JoinDate = new DateTime(2023, 6, 13), Salary = 55000000 },
                new Employee { Id = 45, Name = "홍채원", Department = "마케팅팀", JoinDate = new DateTime(2017, 10, 5), Salary = 42000000 },
                new Employee { Id = 46, Name = "문준혁", Department = "재무팀", JoinDate = new DateTime(2017, 3, 1), Salary = 38000000 },
                new Employee { Id = 47, Name = "배나연", Department = "고객지원팀", JoinDate = new DateTime(2023, 7, 20), Salary = 45000000 },
                new Employee { Id = 48, Name = "송태양", Department = "개발팀", JoinDate = new DateTime(2023, 9, 24), Salary = 35000000 },
                new Employee { Id = 49, Name = "전유나", Department = "기획팀", JoinDate = new DateTime(2023, 11, 21), Salary = 38000000 },
                new Employee { Id = 50, Name = "노건우", Department = "영업팀", JoinDate = new DateTime(2020, 12, 8), Salary = 50000000 },
                new Employee { Id = 51, Name = "류아린", Department = "인사팀", JoinDate = new DateTime(2015, 11, 9), Salary = 55000000 },
                new Employee { Id = 52, Name = "고준서", Department = "총무팀", JoinDate = new DateTime(2021, 12, 7), Salary = 38000000 },
                new Employee { Id = 53, Name = "백다은", Department = "마케팅팀", JoinDate = new DateTime(2023, 1, 11), Salary = 40000000 },
                new Employee { Id = 54, Name = "심재원", Department = "재무팀", JoinDate = new DateTime(2021, 2, 10), Salary = 50000000 },
                new Employee { Id = 55, Name = "안소율", Department = "고객지원팀", JoinDate = new DateTime(2021, 9, 8), Salary = 42000000 },
                new Employee { Id = 56, Name = "권혁준", Department = "개발팀", JoinDate = new DateTime(2020, 1, 20), Salary = 42000000 },
                new Employee { Id = 57, Name = "엄지수", Department = "기획팀", JoinDate = new DateTime(2017, 9, 3), Salary = 60000000 },
                new Employee { Id = 58, Name = "남도윤", Department = "영업팀", JoinDate = new DateTime(2020, 6, 20), Salary = 38000000 },
                new Employee { Id = 59, Name = "황예린", Department = "인사팀", JoinDate = new DateTime(2015, 8, 9), Salary = 35000000 },
                new Employee { Id = 60, Name = "조성민", Department = "총무팀", JoinDate = new DateTime(2023, 9, 16), Salary = 48000000 },
                new Employee { Id = 61, Name = "서채린", Department = "마케팅팀", JoinDate = new DateTime(2020, 1, 6), Salary = 48000000 },
                new Employee { Id = 62, Name = "장민호", Department = "재무팀", JoinDate = new DateTime(2017, 4, 13), Salary = 42000000 },
                new Employee { Id = 63, Name = "구나영", Department = "고객지원팀", JoinDate = new DateTime(2015, 9, 22), Salary = 42000000 },
                new Employee { Id = 64, Name = "김민준", Department = "개발팀", JoinDate = new DateTime(2015, 6, 7), Salary = 45000000 },
                new Employee { Id = 65, Name = "이서연", Department = "기획팀", JoinDate = new DateTime(2022, 9, 24), Salary = 35000000 },
                new Employee { Id = 66, Name = "박지훈", Department = "영업팀", JoinDate = new DateTime(2021, 1, 4), Salary = 35000000 },
                new Employee { Id = 67, Name = "최수아", Department = "인사팀", JoinDate = new DateTime(2020, 11, 14), Salary = 60000000 },
                new Employee { Id = 68, Name = "정우진", Department = "총무팀", JoinDate = new DateTime(2016, 8, 9), Salary = 42000000 },
                new Employee { Id = 69, Name = "강하은", Department = "마케팅팀", JoinDate = new DateTime(2016, 8, 25), Salary = 35000000 },
                new Employee { Id = 70, Name = "윤도현", Department = "재무팀", JoinDate = new DateTime(2021, 4, 22), Salary = 38000000 },
                new Employee { Id = 71, Name = "임지원", Department = "고객지원팀", JoinDate = new DateTime(2020, 12, 21), Salary = 35000000 },
                new Employee { Id = 72, Name = "한소희", Department = "개발팀", JoinDate = new DateTime(2019, 11, 15), Salary = 45000000 },
                new Employee { Id = 73, Name = "오민서", Department = "기획팀", JoinDate = new DateTime(2022, 6, 13), Salary = 40000000 },
                new Employee { Id = 74, Name = "신예준", Department = "영업팀", JoinDate = new DateTime(2023, 7, 18), Salary = 40000000 },
                new Employee { Id = 75, Name = "홍채원", Department = "인사팀", JoinDate = new DateTime(2022, 7, 11), Salary = 38000000 },
                new Employee { Id = 76, Name = "문준혁", Department = "총무팀", JoinDate = new DateTime(2021, 6, 4), Salary = 48000000 },
                new Employee { Id = 77, Name = "배나연", Department = "마케팅팀", JoinDate = new DateTime(2018, 2, 10), Salary = 35000000 },
                new Employee { Id = 78, Name = "송태양", Department = "재무팀", JoinDate = new DateTime(2015, 6, 18), Salary = 55000000 },
                new Employee { Id = 79, Name = "전유나", Department = "고객지원팀", JoinDate = new DateTime(2019, 1, 19), Salary = 38000000 },
                new Employee { Id = 80, Name = "노건우", Department = "개발팀", JoinDate = new DateTime(2016, 4, 17), Salary = 38000000 },
                new Employee { Id = 81, Name = "류아린", Department = "기획팀", JoinDate = new DateTime(2017, 8, 11), Salary = 35000000 },
                new Employee { Id = 82, Name = "고준서", Department = "영업팀", JoinDate = new DateTime(2021, 6, 9), Salary = 50000000 },
                new Employee { Id = 83, Name = "백다은", Department = "인사팀", JoinDate = new DateTime(2015, 6, 25), Salary = 60000000 },
                new Employee { Id = 84, Name = "심재원", Department = "총무팀", JoinDate = new DateTime(2021, 1, 22), Salary = 45000000 },
                new Employee { Id = 85, Name = "안소율", Department = "마케팅팀", JoinDate = new DateTime(2019, 6, 15), Salary = 42000000 },
                new Employee { Id = 86, Name = "권혁준", Department = "재무팀", JoinDate = new DateTime(2016, 11, 10), Salary = 42000000 },
                new Employee { Id = 87, Name = "엄지수", Department = "고객지원팀", JoinDate = new DateTime(2021, 9, 3), Salary = 60000000 },
                new Employee { Id = 88, Name = "남도윤", Department = "개발팀", JoinDate = new DateTime(2021, 2, 14), Salary = 40000000 },
                new Employee { Id = 89, Name = "황예린", Department = "기획팀", JoinDate = new DateTime(2015, 2, 18), Salary = 38000000 },
                new Employee { Id = 90, Name = "조성민", Department = "영업팀", JoinDate = new DateTime(2019, 8, 27), Salary = 48000000 },
                new Employee { Id = 91, Name = "서채린", Department = "인사팀", JoinDate = new DateTime(2020, 7, 14), Salary = 48000000 },
                new Employee { Id = 92, Name = "장민호", Department = "총무팀", JoinDate = new DateTime(2022, 11, 4), Salary = 48000000 },
                new Employee { Id = 93, Name = "구나영", Department = "마케팅팀", JoinDate = new DateTime(2017, 3, 27), Salary = 45000000 },
                new Employee { Id = 94, Name = "김민준", Department = "재무팀", JoinDate = new DateTime(2018, 3, 18), Salary = 50000000 },
                new Employee { Id = 95, Name = "이서연", Department = "고객지원팀", JoinDate = new DateTime(2020, 3, 23), Salary = 40000000 },
                new Employee { Id = 96, Name = "박지훈", Department = "개발팀", JoinDate = new DateTime(2019, 5, 2), Salary = 38000000 },
                new Employee { Id = 97, Name = "최수아", Department = "기획팀", JoinDate = new DateTime(2018, 2, 15), Salary = 50000000 },
                new Employee { Id = 98, Name = "정우진", Department = "영업팀", JoinDate = new DateTime(2023, 8, 3), Salary = 60000000 },
                new Employee { Id = 99, Name = "강하은", Department = "인사팀", JoinDate = new DateTime(2015, 8, 22), Salary = 40000000 },
                new Employee { Id = 100, Name = "윤도현", Department = "총무팀", JoinDate = new DateTime(2018, 4, 3), Salary = 42000000 },
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
