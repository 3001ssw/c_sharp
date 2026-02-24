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
using System.Windows;
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


        private string mssqlIp = "172.16.5.140";
        public string MssqlIp { get => mssqlIp; set => SetProperty(ref mssqlIp, value); }

        private int mssqlPort = 1433;
        public int MssqlPort { get => mssqlPort; set => SetProperty(ref mssqlPort, value); }

        private string mssqlDbName = "wpf_test_db";
        public string MssqlDbName { get => mssqlDbName; set => SetProperty(ref mssqlDbName, value); }

        private string mssqlID = "acnt";
        public string MssqlID { get => mssqlID; set => SetProperty(ref mssqlID, value); }

        private string mssqlPassword = "1234";
        public string MssqlPassword { get => mssqlPassword; set => SetProperty(ref mssqlPassword, value); }

        #endregion

        #region commands

        public DelegateCommand ConnectDbFilePathCommand { get; private set; }
        public DelegateCommand CloseDbFilePathCommand { get; private set; }
        public DelegateCommand SaveStudentCommand { get; private set; }
        public DelegateCommand ConnectMssqlCommand { get; private set; }

        public DelegateCommand CloseMssqlCommand { get; private set; }


        private void OnConnectDbFilePath()
        {
            try
            {
                DatabaseContext = new AppDbContext();
                DatabaseContext.ConnectSqlite(DbFilePath);
                DatabaseContext.Database.EnsureCreated();
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

        private async void OnConnectMssql()
        {
            try
            {
                DatabaseContext = new AppDbContext();
                DatabaseContext.ConnectMssql(MssqlIp, 1433, MssqlDbName, MssqlID, MssqlPassword);
                await DatabaseContext.Database.EnsureCreatedAsync();
                Status = "Connect Database";
            }
            catch (Exception e)
            {
                Status = "Connect Database Fail" + Environment.NewLine + $"error: {e.Message}";
            }
            finally
            {
            }

        }

        private bool CanConnectMssql()
        {
            return true;
        }
        private void OnCloseMssql()
        {

        }

        private bool CanCloseMssql()
        {
            return true;
        }
        #endregion

        #region constructor
        public MainViewModel()
        {
            DbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "School.db");

            ConnectDbFilePathCommand = new DelegateCommand(OnConnectDbFilePath, CanConnectDbFilePath).ObservesProperty(() => DatabaseContext);
            CloseDbFilePathCommand = new DelegateCommand(OnCloseDbFilePath, CanCloseDbFilePath).ObservesProperty(() => DatabaseContext);
            SaveStudentCommand = new DelegateCommand(OnSaveStudent, CanSaveStudent).ObservesProperty(() => DatabaseContext);
            ConnectMssqlCommand = new DelegateCommand(OnConnectMssql, CanConnectMssql);
            CloseMssqlCommand = new DelegateCommand(OnCloseMssql, CanCloseMssql);
        }
        #endregion
    }
}
