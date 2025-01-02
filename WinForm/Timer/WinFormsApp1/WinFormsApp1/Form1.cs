namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 현재 시간을 라벨에 출력
        private void DisplayCurrentTimeToLabel()
        {
            DateTime now = DateTime.Now;
            string strNow = now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            lbDisp.Text = strNow;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayCurrentTimeToLabel(); // 현재 시간 표시
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tmTest.Interval = Int32.Parse(numInterval.Value.ToString()); // 주기 설정
            tmTest.Start(); // 타이머 시작

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            this.ActiveControl = btnStop;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmTest.Stop(); // 타이머 종료

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            this.ActiveControl = numInterval;
        }

        private void tmTest_Tick(object sender, EventArgs e)
        {
            DisplayCurrentTimeToLabel(); // 현재 시간 표시
        }
    }
}
