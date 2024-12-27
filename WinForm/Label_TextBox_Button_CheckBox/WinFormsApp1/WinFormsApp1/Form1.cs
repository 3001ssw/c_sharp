namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbDisp.Text = "텍스트를 표시합니다."; // 라벨에 표시
        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            lbDisp.Text = tbInput.Text; // 텍스트 박스에 문자를 입력하면 라벨에 표시
        }

        private void btnDisp_Click(object sender, EventArgs e)
        {
            lbDisp.Text = "버튼 클릭"; // 버튼 클릭 시 라벨에 문자열 표시
        }

        private void chkDisp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisp.Checked)
            {
                chkDisp.Text = "Checked"; // 체크 됨
            }
            else
            {
                chkDisp.Text = "Unchecked"; // 체크 안 됨
            }
        }
    }
}
