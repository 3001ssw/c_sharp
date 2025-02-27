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

            //사이즈 변경
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbSelect.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            // 속성
            //dataGridView.Dock = DockStyle.Fill; // 화면에 채우기
            dataGridView.AllowUserToAddRows = false; // 사용자가 행 추가하기
            dataGridView.AllowUserToDeleteRows = false; // 사용자가 행 삭제 가능한지
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // 표시되는 열의 자동 크기 조정 모드. 열 체우기
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // 셀 선택 모드. 모든 행 선택
            dataGridView.MultiSelect = true; // 여러줄 선택
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

            // DataTable 생성
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
            // row 클릭
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
