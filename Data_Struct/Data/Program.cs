using System.Data;

namespace Data
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //////////////////////////////////
            ///DataRow, DataTable
            // DataTable 생성
            DataTable dtFruit = new DataTable("Fruit");

            // 컬럼 추가
            dtFruit.Columns.Add("Name", typeof(string));
            dtFruit.Columns.Add("Price", typeof(int));

            // DataRow 추가
            // 방법 1
            dtFruit.Rows.Add("Apple", 1200);
            dtFruit.Rows.Add("Banana", 1000);
            dtFruit.Rows.Add("Blueberry", 1700);

            // 방법 2
            DataRow rowCherry = dtFruit.NewRow();
            rowCherry["Name"] = "Cherry";
            rowCherry["Price"] = 2500;
            dtFruit.Rows.Add(rowCherry);

            DataRow rowMelon = dtFruit.NewRow();
            rowMelon["Name"] = "Melon";
            rowMelon["Price"] = 3000;
            dtFruit.Rows.Add(rowMelon);


            // 데이터 출력
            foreach (DataRow row in dtFruit.Rows)
            {
                Console.WriteLine($"{row["Name"]}, {row["Price"]}");
            }
            Console.WriteLine("============================");

            // 행 삭제
            // 방법 1
            dtFruit.Rows.RemoveAt(4); // 인덱스로 삭제
            // 방법 2
            //dtFruit.Rows.Remove(rowMelon); // DataRow로 삭제
            foreach (DataRow row in dtFruit.Rows)
            {
                Console.WriteLine($"{row["Name"]}, {row["Price"]}");
            }
            Console.WriteLine("============================");

            // 특정 조건의 행 찾기
            Console.WriteLine("가격이 1500보다 높은 과일 찾기");
            DataRow[] result = dtFruit.Select("Price > 1500");
            foreach (DataRow row in result)
            {
                Console.WriteLine($"{row["Name"]}, {row["Price"]}");
            }
            Console.WriteLine("============================");

            // XML
            dtFruit.WriteXml("Fruit.xml"); // xml로 저장

            dtFruit.Clear();

            dtFruit.ReadXml("Fruit.xml"); // xml 읽기
            foreach (DataRow row in dtFruit.Rows)
            {
                Console.WriteLine($"{row["Name"]}, {row["Price"]}");
            }
            Console.WriteLine("============================");

            // 고객
            DataTable dtCustomer = new DataTable("Custoer");
            dtCustomer.Columns.Add("Name", typeof(string));
            dtCustomer.Columns.Add("Age", typeof(int));
            dtCustomer.Columns.Add("Point", typeof(int));
            dtCustomer.Rows.Add("James", 20, 1000);
            dtCustomer.Rows.Add("Emily", 30, 2100);
            dtCustomer.Rows.Add("Michael", 29, 500);
            dtCustomer.Rows.Add("Sarah", 21, 1450);
            dtCustomer.Rows.Add("David", 34, 3450);

            // 직원
            DataTable dtEmployee = new DataTable("employee");
            dtEmployee.Columns.Add("Name", typeof(string));
            dtEmployee.Columns.Add("Age", typeof(int));
            dtEmployee.Columns.Add("Years_of_Service", typeof(int));
            dtEmployee.Rows.Add("John", 40, 5);
            dtEmployee.Rows.Add("Emma", 39, 10);
            dtEmployee.Rows.Add("Olivia", 25, 6);
            dtEmployee.Rows.Add("William", 60, 20);

            //////////////////////////////////
            ///DataSet
            DataSet ds = new DataSet("mart");
            ds.Tables.Add(dtFruit);
            ds.Tables.Add(dtCustomer);
            ds.Tables.Add(dtEmployee);

            // 전체 조회
            foreach (DataTable table in ds.Tables)
            {
                Console.WriteLine($"Table Name: {table.TableName}");
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write($"{col.ColumnName}\t");
                }
                    Console.WriteLine();
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        Console.Write($"{row[col.ColumnName]}\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("============================");
            }

            // xml 쓰기
            ds.WriteXml("mart.xml");


            // xml 읽기
            DataSet dsRead = new DataSet("read_mart");
            dsRead.ReadXml("mart.xml");

            // 전체 조회
            foreach (DataTable table in dsRead.Tables)
            {
                Console.WriteLine($"Table Name: {table.TableName}");
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write($"{col.ColumnName}\t");
                }
                Console.WriteLine();
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        Console.Write($"{row[col.ColumnName]}\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("============================");
            }
        }
    }
}
