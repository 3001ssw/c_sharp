using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
                optionsBuilder.UseSqlite(_sqliteBuilder?.ToString());
        }

        public void ConnectSqlite(string dbFilePath, SqliteOpenMode mode = SqliteOpenMode.ReadWriteCreate)
        {
            SqliteConnectionStringBuilder sqliteBuilder = new SqliteConnectionStringBuilder();
            sqliteBuilder.DataSource = dbFilePath;
            sqliteBuilder.Mode = mode;
            _sqliteBuilder = sqliteBuilder;

            Database.EnsureCreated();
            Students.Load();
        }

        public void CloseSqlite()
        {
            var connection = Database.GetDbConnection() as SqliteConnection;
            if (connection != null)
                SqliteConnection.ClearPool(connection);
        }
    }
}
