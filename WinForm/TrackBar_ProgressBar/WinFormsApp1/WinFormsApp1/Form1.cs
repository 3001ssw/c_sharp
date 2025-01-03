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
            trbTest.Minimum = 0; // 최소값
            trbTest.Maximum = 100; // 최대값
            trbTest.SmallChange = 1; // 키보드 이동 시 증감값
            trbTest.LargeChange = 10; // Page Up/Down 시 증감값
            //trbTest.Orientation = Orientation.Horizontal; // 수평
            trbTest.Value = 0; // 값 설정

            numTrackBar.Minimum = 0; // 최소값
            numTrackBar.Maximum = 100; // 최대값
            numTrackBar.Increment = 1; // 증감 값
            numTrackBar.Value = 0; // 값 설정

            pbTest.Minimum = 0; // 최소값
            pbTest.Maximum = 100; // 최대값
            pbTest.Value = 0; // 값 설정

            numProgressBar.Minimum = 0; // 최소값
            numProgressBar.Maximum = 100; // 최대값
            numProgressBar.Increment = 1; // 증감 값
            numProgressBar.Value = 0; // 값 설정
        }

        private void trbTest_ValueChanged(object sender, EventArgs e)
        {
            numTrackBar.Value = trbTest.Value; // TracBar값 NumericUpDown에 설정
        }

        private void numTrackBar_ValueChanged(object sender, EventArgs e)
        {
            trbTest.Value = int.Parse(numTrackBar.Value.ToString()); // NumericUpDown에값 TracBar에 설정
        }

        private void numProgressBar_ValueChanged(object sender, EventArgs e)
        {
            pbTest.Value = int.Parse(numProgressBar.Value.ToString()); // NumericUpDown에값 ProgressBar에 설정
        }

        private void btnBlocks_Click(object sender, EventArgs e)
        {
            pbTest.Style = ProgressBarStyle.Blocks; // Blocks 스타일
        }

        private void btnContinuous_Click(object sender, EventArgs e)
        {
            pbTest.Style = ProgressBarStyle.Continuous;// Continuous 스타일
        }

        private void btnMarquee_Click(object sender, EventArgs e)
        {
            //pbTest.MarqueeAnimationSpeed = 100; // marquee 애니메이트 속도
            pbTest.Style = ProgressBarStyle.Marquee; // Marquee 스타일
        }
    }
}
