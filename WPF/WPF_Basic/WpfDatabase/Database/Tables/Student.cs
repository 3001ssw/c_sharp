using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfDatabase.Database.Tables
{
    [Table("TBL_STUDENT")]
    public class Student
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]: DB가 알아서 번호를 매기게 합니다. (int의 기본값)
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]: **"내가 직접 값을 넣을 테니 DB는 신경 꺼라"**는 뜻입니다. (Guid를 직접 할당할 때 씁니다)
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]: 값이 수정될 때마다 DB가 자동으로 계산하게 합니다.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("col_id")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("col_name")]
        public string Name { get; set; } = "";

        [Column("col_grade")]
        public int Grade { get; set; } = 0;

        [NotMapped] // db에는 안생김
        public string NotMapped { get; set; } = "";
    }
}
