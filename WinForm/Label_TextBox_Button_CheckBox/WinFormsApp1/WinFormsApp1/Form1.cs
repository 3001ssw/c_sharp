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
            lbDisp.Text = "�ؽ�Ʈ�� ǥ���մϴ�."; // �󺧿� ǥ��
        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            lbDisp.Text = tbInput.Text; // �ؽ�Ʈ �ڽ��� ���ڸ� �Է��ϸ� �󺧿� ǥ��
        }

        private void btnDisp_Click(object sender, EventArgs e)
        {
            lbDisp.Text = "��ư Ŭ��"; // ��ư Ŭ�� �� �󺧿� ���ڿ� ǥ��
        }

        private void chkDisp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisp.Checked)
            {
                chkDisp.Text = "Checked"; // üũ ��
            }
            else
            {
                chkDisp.Text = "Unchecked"; // üũ �� ��
            }
        }
    }
}
