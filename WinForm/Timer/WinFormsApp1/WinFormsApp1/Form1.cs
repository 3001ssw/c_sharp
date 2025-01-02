namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ���� �ð��� �󺧿� ���
        private void DisplayCurrentTimeToLabel()
        {
            DateTime now = DateTime.Now;
            string strNow = now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            lbDisp.Text = strNow;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayCurrentTimeToLabel(); // ���� �ð� ǥ��
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tmTest.Interval = Int32.Parse(numInterval.Value.ToString()); // �ֱ� ����
            tmTest.Start(); // Ÿ�̸� ����

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            this.ActiveControl = btnStop;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmTest.Stop(); // Ÿ�̸� ����

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            this.ActiveControl = numInterval;
        }

        private void tmTest_Tick(object sender, EventArgs e)
        {
            DisplayCurrentTimeToLabel(); // ���� �ð� ǥ��
        }
    }
}
