using System.Data;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        DataTable m_table;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 과일 테이블 생성
            DataTable dtFruit = new DataTable("Fruit");
            dtFruit.Columns.Add("Name", typeof(string));
            dtFruit.Columns.Add("Price", typeof(int));

            dtFruit.Rows.Add("Apple", 1200);
            dtFruit.Rows.Add("Banana", 1000);
            dtFruit.Rows.Add("Blueberry", 1700);
            dtFruit.Rows.Add("Cherry", 2500);
            dtFruit.Rows.Add("Melon", 3000);

            dataGridView.DataSource = dtFruit;
        }
    }
}
