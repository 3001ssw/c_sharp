using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfFunction;

namespace WpfFunction
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties

        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        public ObservableCollection<Student> Students { get => students; set => SetProperty(ref students, value); }

        private ObservableCollection<Student> results = new ObservableCollection<Student>();
        public ObservableCollection<Student> Results { get => results; set => SetProperty(ref results, value); }

        private int score = 80;
        public int Score { get => score; set => SetProperty(ref score, value); }

        public Func<Student, bool> GetPass { get => (s) => s.Score >= Score; }
        public Func<Student, bool> GetFail { get => (s) => s.Score < Score; }

        #endregion

        #region commands

        public DelegateCommand GetPassCommand { get; private set; }
        public DelegateCommand GetFailCommand { get; private set; }

        #endregion

        #region command methods

        private void OnGetPass()
        {
            var passStudents = GetList(GetPass);

            Results.Clear();
            foreach (var student in passStudents)
            {
                Results.Add(student);
            }
        }

        private bool CanGetPass()
        {
            return true;
        }

        private void OnGetFail()
        {
            var passStudents = GetList(GetFail);

            Results.Clear();
            foreach (var student in passStudents)
            {
                Results.Add(student);
            }
        }

        private bool CanGetFail()
        {
            return true;
        }

        #endregion

        #region functions

        IEnumerable<Student> GetList(Func<Student, bool> func)
        {
            return Students.Where(func).ToList();
        }

        #endregion



        #region constructor
        public MainViewModel()
        {
            GetPassCommand = new DelegateCommand(OnGetPass, CanGetPass);
            GetFailCommand = new DelegateCommand(OnGetFail, CanGetFail);

            for (int i = 0; i < 20; i++)
            {
                Students.Add(new Student());
            }
        }
        #endregion
    }
}
