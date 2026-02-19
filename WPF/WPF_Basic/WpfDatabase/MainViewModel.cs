using Microsoft.Data.Sqlite;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace WpfDatabase
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private AppDbContext? databaseContext = null;
        public AppDbContext? DatabaseContext { get => databaseContext; set => SetProperty(ref databaseContext, value); }

        private string status = "";
        public string Status { get => status; set => SetProperty(ref status, value); }

        private string connectionDbFilePath = "";
        public string ConnectionDbFilePath { get => connectionDbFilePath; set => SetProperty(ref connectionDbFilePath, value); }

        private string studentName = "";
        public string StudentName { get => studentName; set => SetProperty(ref studentName, value); }

        private string deleteID = "";
        public string DeleteID { get => deleteID; set => SetProperty(ref deleteID, value); }

        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        public ObservableCollection<Student> Students { get => students; set => SetProperty(ref students, value); }
        #endregion

        #region commands

        public DelegateCommand ConnectionDbFilePathCommand { get; private set; }
        public DelegateCommand CloseDbFilePathCommand { get; private set; }
        public DelegateCommand AddStudentCommand { get; private set; }

        public DelegateCommand DeleteStudentCommand { get; private set; }

        public DelegateCommand SaveStudentCommand { get; private set; }

        private void OnConnectionDbFilePath()
        {
            try
            {

                DatabaseContext = new AppDbContext(ConnectionDbFilePath);
                DatabaseContext.Database.EnsureCreated();

                Students = new ObservableCollection<Student>(DatabaseContext.Students.ToArray());
                Status = "Connect Database";
            }
            catch (Exception e)
            {
                Status = "Connect Database Fail";
            }
            finally
            {
            }
        }

        private bool CanConnectionDbFilePath()
        {
            if (DatabaseContext != null)
                return false;
            if (DatabaseContext == null)
            {
                if (string.IsNullOrEmpty(ConnectionDbFilePath))
                    return false;
            }

            return true;
        }


        private void OnCloseDbFilePath()
        {
            DatabaseContext?.Dispose();
            DatabaseContext = null;
            Status = "Close Database";
        }

        private bool CanCloseDbFilePath()
        {
            if (DatabaseContext == null)
                return false;

            return true;
        }


        private void OnAddStudent()
        {
            DatabaseContext?.Students.Add(new Student { Name = StudentName });

            DatabaseContext?.SaveChanges();

            Students = new ObservableCollection<Student>(DatabaseContext?.Students.ToArray());
        }

        private bool CanAddStudent()
        {
            if (string.IsNullOrEmpty(StudentName))
                return false;
            if (DatabaseContext == null)
                return false;

            return true;
        }

        private void OnDeleteStudent()
        {
            var targetStudent = Students.FirstOrDefault(s => s.Id.ToString() == DeleteID);

            // 2. 일치하는 데이터를 찾았다면?
            if (targetStudent != null)
            {
                // 3. 바구니에서 삭제! (★이 코드가 실행되는 즉시 DataGrid 화면에서도 해당 줄이 싹 사라집니다)
                Students.Remove(targetStudent);
            }
            
        }

        private bool CanDeleteStudent()
        {
            if (string.IsNullOrEmpty(DeleteID))
                return false;
            if (DatabaseContext == null)
                return false;

            return true;
        }
        private void OnSaveStudent()
        {
            DatabaseContext.Students.UpdateRange(Students);

            DatabaseContext.SaveChanges();
        }

        private bool CanSaveStudent()
        {
            return true;
        }
        #endregion



        #region constructor
        public MainViewModel()
        {
            ConnectionDbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.db");

            ConnectionDbFilePathCommand = new DelegateCommand(OnConnectionDbFilePath, CanConnectionDbFilePath).ObservesProperty(() => DatabaseContext); ;
            CloseDbFilePathCommand = new DelegateCommand(OnCloseDbFilePath, CanCloseDbFilePath).ObservesProperty(() => DatabaseContext); ;
            AddStudentCommand = new DelegateCommand(OnAddStudent, CanAddStudent).ObservesProperty(() => StudentName);
            DeleteStudentCommand = new DelegateCommand(OnDeleteStudent, CanDeleteStudent).ObservesProperty(() => DeleteID);
            SaveStudentCommand = new DelegateCommand(OnSaveStudent, CanSaveStudent);
        }
        #endregion
    }
}
