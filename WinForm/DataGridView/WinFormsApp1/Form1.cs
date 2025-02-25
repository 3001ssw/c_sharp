using System.Data;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        DataSet m_ds = new DataSet("DataSet");
        DataTable m_table = new DataTable("DataTable");

        public Form1()
        {
            InitializeComponent();

            m_ds.Tables.Add(m_table);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "DataGridView Sample";

            //dataGridView.Dock = DockStyle.Fill; // 화면에 채우기
            dataGridView.AllowUserToAddRows = false; // 사용자가 행 추가하기
            dataGridView.AllowUserToDeleteRows = false; // 사용자가 행 삭제 가능한지
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // 표시되는 열의 자동 크기 조정 모드. 열 체우기
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // 셀 선택 모드. 모든 행 선택
            dataGridView.MultiSelect = true; // 여러줄 선택
            dataGridView.ReadOnly = true; // 읽기 전용

            m_table.Columns.Add("Name", typeof(string));
            m_table.Columns.Add("Price", typeof(int));

            m_table.Rows.Add("Apple", 1200);
            m_table.Rows.Add("Banana", 1000);
            m_table.Rows.Add("Blueberry", 1700);
            m_table.Rows.Add("Cherry", 2500);
            m_table.Rows.Add("Melon", 3000);

            dataGridView.DataSource = m_ds.Tables[0];
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (0 <= e.RowIndex && 0 <= e.ColumnIndex)
            {
                string strVal = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                lbSelect.Text = strVal;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView.DataSource.GetType() == typeof(DataTable))
            {
                m_ds.WriteXml("table.xml");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            m_ds.Clear();
            m_ds.ReadXml("table.xml");            
            dataGridView.DataSource = m_table;
        }
    }
}
