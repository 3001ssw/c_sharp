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

            // List로 추가
            //List<CPerson> persons = new List<CPerson>();
            //persons.Add(new CPerson("Jake", 10));
            //persons.Add(new CPerson("David", 12));
            //persons.Add(new CPerson("Rachel", 20));
            //dataGridView.DataSource = persons;

            // 칼럼
            dataGridView.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "NAME"; // 속성 이름
            colName.HeaderText = "Person Name"; // 헤더 표시

            DataGridViewTextBoxColumn colBirth = new DataGridViewTextBoxColumn();
            colBirth.DataPropertyName = "AGE"; // 속성 이름
            colBirth.HeaderText = "Person Age"; // 헤더 표시

            dataGridView.Columns.Add(colName);
            dataGridView.Columns.Add(colBirth);

            // BindingList로 추가
            BindingList<CPerson> persons = new BindingList<CPerson>();
            persons.Add(new CPerson("Jake", 10));
            persons.Add(new CPerson("David", 12));
            persons.Add(new CPerson("Rachel", 20));
            dataGridView.DataSource = persons;
        }

        private void InitDataGridViewProperty()
        {

            //사이즈 변경
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // 속성
            //dataGridView.Dock = DockStyle.Fill; // 화면에 채우기
            dataGridView.AllowUserToAddRows = true; // 사용자가 행 추가하기
            dataGridView.AllowUserToDeleteRows = true; // 사용자가 행 삭제 가능한지
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // 표시되는 열의 자동 크기 조정 모드. 열 채우기
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // 셀 선택 모드. 모든 행 선택
            //dataGridView.MultiSelect = true; // 여러줄 선택
            //dataGridView.ReadOnly = true; // 읽기 전용

            // 배경색 및 기본 스타일 설정
            dataGridView.BackgroundColor = Color.LightGray;  // 전체 배경색
            dataGridView.GridColor = Color.DarkGray;        // 셀 테두리 색상

            // 행 스타일 변경
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            // 컬럼 헤더 스타일 변경
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // 선택된 행 스타일 변경
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.DarkCyan;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            // 행 높이 및 열 너비 자동 조정
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // 열 너비 자동 조절
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;  // 행 높이 자동 조절

            // 행 및 열 테두리 스타일 변경
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // 행 스타일 변경
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // 짝수 행 배경색 변경
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
