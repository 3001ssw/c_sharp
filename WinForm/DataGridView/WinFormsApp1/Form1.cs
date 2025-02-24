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
            m_table = new DataTable();
            m_table.Columns.Add("ID", typeof(int));
            m_table.Columns.Add("Name", typeof(string));
            m_table.Rows.Add(1, "Alice");
            m_table.Rows.Add(2, "Bob");

            dataGridView.DataSource = m_table;

        }
    }
}
