using System.Data;

namespace Data
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // DataTable 생성
            DataTable table = new DataTable("Fruit");

            // 컬럼 추가
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(int));

            // 행 추가
            table.Rows.Add("Apple", 1200);
            table.Rows.Add("Banana", 1000);

            DataRow rowCherry = table.NewRow();
            rowCherry["Name"] = "Cherry";
            rowCherry["Price"] = 2500;
            table.Rows.Add(rowCherry);


            // 데이터 출력
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row["Name"]}, {row["Price"]}");
            }
        }
    }
}
