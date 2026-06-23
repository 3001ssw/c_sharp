using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChartControl
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<SalaryData> SalaryData
        {
            get { return GetValue<ObservableCollection<SalaryData>>(); }
            set { SetValue(value); }
        }

        public MainViewModel()
        {
            SalaryData = new ObservableCollection<SalaryData>
            {
                new SalaryData { Department = "개발팀",   AverageSalary = 48000000 },
                new SalaryData { Department = "기획팀",   AverageSalary = 45000000 },
                new SalaryData { Department = "영업팀",   AverageSalary = 42000000 },
                new SalaryData { Department = "인사팀",   AverageSalary = 40000000 },
                new SalaryData { Department = "총무팀",   AverageSalary = 38000000 },
                new SalaryData { Department = "마케팅팀", AverageSalary = 44000000 },
                new SalaryData { Department = "재무팀",   AverageSalary = 46000000 },
                new SalaryData { Department = "재팀",   AverageSalary = 99000000 },
            };
        }
    }

    public class SalaryData
    {
        public string Department { get; set; }
        public decimal AverageSalary { get; set; }
    }
}
