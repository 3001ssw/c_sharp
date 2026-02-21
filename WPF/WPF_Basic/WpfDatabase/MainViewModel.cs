using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
using WpfDatabase.Database;
using WpfDatabase.Database.Tables;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
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

        private string dbFilePath = "";
        public string DbFilePath { get => dbFilePath; set => SetProperty(ref dbFilePath, value); }

        private ObservableCollection<Student>? students = null;
        public ObservableCollection<Student>? Students { get => students; set => SetProperty(ref students, value); }
        #endregion

        #region commands

        public DelegateCommand ConnectDbFilePathCommand { get; private set; }
        public DelegateCommand CloseDbFilePathCommand { get; private set; }
        public DelegateCommand SaveStudentCommand { get; private set; }

        private void OnConnectDbFilePath()
        {
            try
            {
                DatabaseContext = new AppDbContext();
                DatabaseContext.ConnectSqlite(DbFilePath);

                Students = DatabaseContext.Students.Local.ToObservableCollection();
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

        private bool CanConnectDbFilePath()
        {
            if (DatabaseContext != null)
                return false;
            if (DatabaseContext == null)
            {
                if (string.IsNullOrEmpty(DbFilePath))
                    return false;
            }

            return true;
        }


        private void OnCloseDbFilePath()
        {
            DatabaseContext?.CloseSqlite();
            DatabaseContext?.Dispose();
            DatabaseContext = null;
            Status = "Close Database";

            Students = null;
        }

        private bool CanCloseDbFilePath()
        {
            if (DatabaseContext == null)
                return false;

            return true;
        }

        private void OnSaveStudent()
        {
            DatabaseContext?.SaveChanges();

            Status = "Save";
        }

        private bool CanSaveStudent()
        {
            if (DatabaseContext == null)
                return false;
            return true;
        }
        #endregion

        #region constructor
        public MainViewModel()
        {
            DbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "School.db");

            ConnectDbFilePathCommand = new DelegateCommand(OnConnectDbFilePath, CanConnectDbFilePath).ObservesProperty(() => DatabaseContext); ;
            CloseDbFilePathCommand = new DelegateCommand(OnCloseDbFilePath, CanCloseDbFilePath).ObservesProperty(() => DatabaseContext); ;
            SaveStudentCommand = new DelegateCommand(OnSaveStudent, CanSaveStudent).ObservesProperty(() => DatabaseContext);
        }
        #endregion
    }
}
