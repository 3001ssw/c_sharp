using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDatabase.Database.Tables;

namespace WpfDatabase.Database
{
    public class AppDbContext : DbContext
    {
        #region fields
        private SqliteConnectionStringBuilder? _sqliteBuilder = null; // sqlite
        private SqlConnectionStringBuilder? _mssqlBuilder = null; // mssql
        //private MySqlConnectionStringBuilder? _mysqlBuilder = null; // mysql, mariadb
        #endregion

        #region Tables
        public DbSet<Student> Students { get; set; }
        #endregion

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 데이터베이스를 구성하기 위해 메서드 재정의
            if (_sqliteBuilder != null)
                optionsBuilder.UseSqlite(_sqliteBuilder?.ConnectionString);
            else if (_mssqlBuilder != null)
                optionsBuilder.UseSqlServer(_mssqlBuilder?.ConnectionString);
        }

        public void ConnectSqlite(string dbFilePath, SqliteOpenMode mode = SqliteOpenMode.ReadWriteCreate)
        {
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
            builder.DataSource = dbFilePath;
            builder.Mode = mode;
            _sqliteBuilder = builder;

            Students.Load();
        }

        public void ConnectMssql(string ip, int port, string dbName, string userID, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = $"{ip},{port}";
            builder.InitialCatalog = dbName;
            builder.UserID = userID;
            builder.Password = password;
            builder.TrustServerCertificate = true;
            _mssqlBuilder = builder;
        }

        public void CloseSqlite()
        {
            var connection = Database.GetDbConnection() as SqliteConnection;
            if (connection != null)
                SqliteConnection.ClearPool(connection);
        }
    }
}
