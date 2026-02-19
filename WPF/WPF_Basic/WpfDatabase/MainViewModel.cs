using Microsoft.Data.Sqlite;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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
        private AppDbContext? _db = null;

        private string status = "";
        public string Status { get => status; set => SetProperty(ref status, value); }

        private string connectionDbFilePath = "";
        public string ConnectionDbFilePath { get => connectionDbFilePath; set => SetProperty(ref connectionDbFilePath, value); }
        #endregion

        #region commands

        public DelegateCommand ConnectionDbFilePathCommand { get; private set; }

        private void OnConnectionDbFilePath()
        {
            try
            {
                SqliteConnectionStringBuilder sqliteBuilder = new SqliteConnectionStringBuilder();
                sqliteBuilder.DataSource = ConnectionDbFilePath;
                sqliteBuilder.Mode = SqliteOpenMode.ReadWriteCreate;

                _db = new AppDbContext(sqliteBuilder);
                _db.Database.EnsureCreated();

                //// 4. 데이터 1건 추가 (INSERT)
                //_db.Students.Add(new Student { Name = "C# 마스터" });

                //// 5. 실제 DB에 쿼리 전송 (COMMIT)
                //_db.SaveChanges();

                //// 6. 데이터 잘 들어갔나 확인 (SELECT)
                //int count = _db.Students.Count();
            }
            catch (Exception e)
            {

            }
            finally
            {
                ConnectionDbFilePathCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanConnectionDbFilePath()
        {
            if (_db != null)
                return false;
            if (_db == null)
            {
                if (string.IsNullOrEmpty(ConnectionDbFilePath))
                    return false;
            }

            return true;
        }

        #endregion



        #region constructor
        public MainViewModel()
        {
            ConnectionDbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.db");

            ConnectionDbFilePathCommand = new DelegateCommand(OnConnectionDbFilePath, CanConnectionDbFilePath);
        }
        #endregion
    }
}
