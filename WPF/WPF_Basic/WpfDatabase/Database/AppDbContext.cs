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

        public void ConnectSqlite(string dbFilePath)
        {
            SqliteConnectionStringBuilder sqliteBuilder = new SqliteConnectionStringBuilder();
            sqliteBuilder.DataSource = dbFilePath;
            sqliteBuilder.Mode = SqliteOpenMode.ReadWriteCreate;
            //sqliteBuilder.Pooling = false;
            _sqliteBuilder = sqliteBuilder;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_sqliteBuilder?.ToString());
        }
    }
}
