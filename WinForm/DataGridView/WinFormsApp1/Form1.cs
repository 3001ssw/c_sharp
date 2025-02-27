using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        DataTable m_table = new DataTable("DataTable");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "DataGridView Sample";

            //������ ����
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbSelect.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            // �Ӽ�
            //dataGridView.Dock = DockStyle.Fill; // ȭ�鿡 ä���
            dataGridView.AllowUserToAddRows = false; // ����ڰ� �� �߰��ϱ�
            dataGridView.AllowUserToDeleteRows = false; // ����ڰ� �� ���� ��������
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // ǥ�õǴ� ���� �ڵ� ũ�� ���� ���. �� ü���
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // �� ���� ���. ��� �� ����
            dataGridView.MultiSelect = true; // ������ ����
            //dataGridView.ReadOnly = true; // �б� ����

            // ���� �� �⺻ ��Ÿ�� ����
            dataGridView.BackgroundColor = Color.LightGray;  // ��ü ����
            dataGridView.GridColor = Color.DarkGray;        // �� �׵θ� ����

            // �� ��Ÿ�� ����
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            // �÷� ��� ��Ÿ�� ����
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // ���õ� �� ��Ÿ�� ����
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.DarkCyan;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            // �� ���� �� �� �ʺ� �ڵ� ����
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // �� �ʺ� �ڵ� ����
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;  // �� ���� �ڵ� ����

            // �� �� �� �׵θ� ��Ÿ�� ����
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // �� ��Ÿ�� ����
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // ¦�� �� ���� ����

            // DataTable ����
            m_table.Columns.Add("Name", typeof(string));
            m_table.Columns.Add("Price", typeof(int));

            m_table.Rows.Add("Apple", 1200);
            m_table.Rows.Add("Banana", 1000);
            m_table.Rows.Add("Blueberry", 1700);
            m_table.Rows.Add("Cherry", 2500);
            m_table.Rows.Add("Melon", 3000);

            dataGridView.DataSource = m_table;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // cell click
            if ((0 <= e.RowIndex && e.RowIndex < dataGridView.Rows.Count) &&
                (0 <= e.ColumnIndex && e.ColumnIndex < dataGridView.Columns.Count))
            {
                string? strVal = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (strVal != null)
                    lbSelect.Text = strVal;
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // row Ŭ��
            StringBuilder sbTotal = new StringBuilder();
            for (int iRowIndex = 0; iRowIndex < dataGridView.SelectedRows.Count; iRowIndex++)
            {
                StringBuilder sbRow = new StringBuilder();
                for (int iColIndex = 0; iColIndex < dataGridView.Columns.Count; iColIndex++)
                {
                    string? strVal = dataGridView.SelectedRows[iRowIndex].Cells[iColIndex].Value.ToString();
                    if (strVal != null)
                    {
                        if (0 < sbRow.Length)
                            sbRow.Append(",");
                        sbRow.Append(strVal);
                    }
                }
                sbTotal.AppendLine(sbRow.ToString());
            }
            lbSelect.Text = sbTotal.ToString();
        }
    }
}
