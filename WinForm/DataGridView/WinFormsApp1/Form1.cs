using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DataGridView_DataTable
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
            this.Text = "DataGridView - DataTable";

            //������ ����
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            textBox.ScrollBars = ScrollBars.Vertical;

            // �Ӽ�
            dataGridView.Dock = DockStyle.Fill; // ȭ�鿡 ä���
            dataGridView.AllowUserToAddRows = false; // ����ڰ� �� �߰� ����
            dataGridView.AllowUserToDeleteRows = false; // ����ڰ� �� ���� ����
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // ǥ�õǴ� ���� �ڵ� ũ�� ���� ���. �� ä���
            //dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // �� ���� ���. ��� �� ����
            //dataGridView.MultiSelect = true; // ���� �� ����
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
            // cell Ŭ��
            if ((0 <= e.RowIndex && e.RowIndex < dataGridView.Rows.Count) &&
                (0 <= e.ColumnIndex && e.ColumnIndex < dataGridView.Columns.Count))
            {
                string? strVal = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (strVal != null)
                {
                    textBox.AppendText(Environment.NewLine);
                    textBox.AppendText($"[{e.RowIndex}, {e.ColumnIndex}] �� Ŭ��: {strVal}");
                }
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // �� �� ���� �� ȣ��
            textBox.AppendText(Environment.NewLine);
            textBox.AppendText($"[{e.RowIndex}, {e.ColumnIndex}] �� �����: {dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}");
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // row Ŭ��
            StringBuilder sbTotal = new StringBuilder();
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                StringBuilder sbRow = new StringBuilder();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string? strVal = cell.Value.ToString();
                    if (strVal != null)
                    {
                        if (0 < sbRow.Length)
                            sbRow.Append(",");
                        sbRow.Append(strVal);
                    }
                }
                string strRow = $"{row.Index}: {sbRow.ToString()}";
                sbTotal.AppendLine(strRow);
            }

            if (0 < sbTotal.Length)
            {
                string strTotal = "SelectionChanged" + Environment.NewLine + sbTotal.ToString();
                textBox.AppendText(Environment.NewLine);
                textBox.AppendText(strTotal);
            }
        }
    }
}
