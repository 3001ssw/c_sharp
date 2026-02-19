using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDatabase
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";
    }

    public class Grade
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";
    }

    public class AppDbContext : DbContext
    {
        private SqliteConnectionStringBuilder _sqliteBuilder = null;
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        // 생성자: 화면에서 선택한 DB 종류와 주소를 받아옵니다.
        public AppDbContext(SqliteConnectionStringBuilder sqliteBuilder)
        {
            _sqliteBuilder = sqliteBuilder;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_sqliteBuilder.ToString());
        }
    }
}
