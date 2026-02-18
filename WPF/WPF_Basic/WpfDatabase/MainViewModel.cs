using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        private SQLiteConnection _sqlConnection;

        private string createDbPath = AppDomain.CurrentDomain.BaseDirectory;
        public string CreateDbPath { get => createDbPath; set => SetProperty(ref createDbPath, value); }

        private string createDbStatus = "";
        public string CreateDbStatus { get => createDbStatus; set => SetProperty(ref createDbStatus, value); }

        #endregion

        #region commands

        public DelegateCommand CreateDbCommand { get; private set; }

        private void OnCreateDb()
        {
            SQLiteConnection.CreateFile(CreateDbPath);
            CreateDbStatus = "Create Database";
        }

        private bool CanCreateDb()
        {
            if (string.IsNullOrEmpty(CreateDbPath))
                return false;

            return true;
        }

        #endregion



        #region constructor
        public MainViewModel()
        {
            CreateDbCommand = new DelegateCommand(OnCreateDb, CanCreateDb);
        }
        #endregion
    }
}
