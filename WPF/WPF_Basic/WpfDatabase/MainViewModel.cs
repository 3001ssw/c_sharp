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
        private AppDbContext db;

        private string createDbPath = AppDomain.CurrentDomain.BaseDirectory;
        public string CreateDbPath
        {
            get => createDbPath;
            set
            {
                SetProperty(ref createDbPath, value);

                ConnectionDbFilePath = Path.Combine(CreateDbPath, CreateDbName);
            }
        }

        private string createDbName = "test.db";
        public string CreateDbName
        {
            get => createDbName;
            set
            {
                SetProperty(ref createDbName, value);
                ConnectionDbFilePath = Path.Combine(CreateDbPath, CreateDbName);
            }
        }

        private string status = "";
        public string Status { get => status; set => SetProperty(ref status, value); }

        private string connectionDbFilePath = "";
        public string ConnectionDbFilePath { get => connectionDbFilePath; set => SetProperty(ref connectionDbFilePath, value); }
        #endregion

        #region commands

        public DelegateCommand CreateDbCommand { get; private set; }
        public DelegateCommand ConnectionDbFilePathCommand { get; private set; }

        private void OnCreateDb()
        {
            //string createDbFilePath = Path.Combine(CreateDbPath, CreateDbName);
            //SqliteConnection.CreateFile(createDbFilePath);
            //Status = $"Create {CreateDbName}";
        }

        private bool CanCreateDb()
        {
            if (string.IsNullOrEmpty(CreateDbPath))
                return false;
            if (string.IsNullOrEmpty(CreateDbName))
                return false;

            return true;
        }

        private void OnConnectionDbFilePath()
        {
            string connectionPath = $"Data Source={ConnectionDbFilePath};";
            try
            {
                db = new AppDbContext(connectionPath);
                db.Database.EnsureCreated();

                // 4. 데이터 1건 추가 (INSERT)
                db.Students.Add(new Student { Name = "C# 마스터" });

                // 5. 실제 DB에 쿼리 전송 (COMMIT)
                db.SaveChanges();

                // 6. 데이터 잘 들어갔나 확인 (SELECT)
                int count = db.Students.Count();
            }
            catch (Exception e)
            {

            }
            finally
            {
            }

            Status = $"Open {CreateDbName}";
        }

        private bool CanConnectionDbFilePath()
        {
            if (string.IsNullOrEmpty(ConnectionDbFilePath))
                return false;
            if (db != null)
                return false;

            return true;
        }

        #endregion



        #region constructor
        public MainViewModel()
        {
            CreateDbCommand = new DelegateCommand(OnCreateDb, CanCreateDb);
            ConnectionDbFilePathCommand = new DelegateCommand(OnConnectionDbFilePath, CanConnectionDbFilePath);

            ConnectionDbFilePath = Path.Combine(CreateDbPath, CreateDbName);
        }
        #endregion
    }
}
