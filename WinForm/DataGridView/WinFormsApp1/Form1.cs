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

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "DataGridView Sample";

            m_ds.Tables.Add(m_table);

            m_table.Columns.Add("Name", typeof(string));
            m_table.Columns.Add("Price", typeof(int));

            m_table.Rows.Add("Apple", 1200);
            m_table.Rows.Add("Banana", 1000);
            m_table.Rows.Add("Blueberry", 1700);
            m_table.Rows.Add("Cherry", 2500);
            m_table.Rows.Add("Melon", 3000);

            dataGridView.DataSource = m_ds.Tables[0];
            //dataGridView.Dock = DockStyle.Fill;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = true;
            dataGridView.ReadOnly = true;
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
