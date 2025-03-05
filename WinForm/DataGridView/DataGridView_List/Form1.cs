using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace DataGridView_List
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "DataGridView - List";

            InitDataGridViewProperty();

            // List�� �߰�
            //List<CPerson> persons = new List<CPerson>();
            //persons.Add(new CPerson("Jake", 10));
            //persons.Add(new CPerson("David", 12));
            //persons.Add(new CPerson("Rachel", 20));
            //dataGridView.DataSource = persons;

            // Į��
            dataGridView.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "NAME"; // �Ӽ� �̸�
            colName.HeaderText = "Person Name"; // ��� ǥ��

            DataGridViewTextBoxColumn colBirth = new DataGridViewTextBoxColumn();
            colBirth.DataPropertyName = "AGE"; // �Ӽ� �̸�
            colBirth.HeaderText = "Person Age"; // ��� ǥ��

            dataGridView.Columns.Add(colName);
            dataGridView.Columns.Add(colBirth);

            // BindingList�� �߰�
            BindingList<CPerson> persons = new BindingList<CPerson>();
            persons.Add(new CPerson("Jake", 10));
            persons.Add(new CPerson("David", 12));
            persons.Add(new CPerson("Rachel", 20));
            dataGridView.DataSource = persons;
        }

        private void InitDataGridViewProperty()
        {

            //������ ����
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // �Ӽ�
            //dataGridView.Dock = DockStyle.Fill; // ȭ�鿡 ä���
            dataGridView.AllowUserToAddRows = true; // ����ڰ� �� �߰��ϱ�
            dataGridView.AllowUserToDeleteRows = true; // ����ڰ� �� ���� ��������
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // ǥ�õǴ� ���� �ڵ� ũ�� ���� ���. �� ä���
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // �� ���� ���. ��� �� ����
            //dataGridView.MultiSelect = true; // ������ ����
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
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                CPerson? person = row.DataBoundItem as CPerson;
                if (person != null)
                {
                    Debug.WriteLine($"Name: {person.NAME}, Age: {person.AGE}");
                }
            }
        }
    }
}
