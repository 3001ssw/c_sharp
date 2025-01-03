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
            trbTest.Minimum = 0; // �ּҰ�
            trbTest.Maximum = 100; // �ִ밪
            trbTest.SmallChange = 1; // Ű���� �̵� �� ������
            trbTest.LargeChange = 10; // Page Up/Down �� ������
            //trbTest.Orientation = Orientation.Horizontal; // ����
            trbTest.Value = 0; // �� ����

            numTrackBar.Minimum = 0; // �ּҰ�
            numTrackBar.Maximum = 100; // �ִ밪
            numTrackBar.Increment = 1; // ���� ��
            numTrackBar.Value = 0; // �� ����

            pbTest.Minimum = 0; // �ּҰ�
            pbTest.Maximum = 100; // �ִ밪
            pbTest.Value = 0; // �� ����

            numProgressBar.Minimum = 0; // �ּҰ�
            numProgressBar.Maximum = 100; // �ִ밪
            numProgressBar.Increment = 1; // ���� ��
            numProgressBar.Value = 0; // �� ����
        }

        private void trbTest_ValueChanged(object sender, EventArgs e)
        {
            numTrackBar.Value = trbTest.Value; // TracBar�� NumericUpDown�� ����
        }

        private void numTrackBar_ValueChanged(object sender, EventArgs e)
        {
            trbTest.Value = int.Parse(numTrackBar.Value.ToString()); // NumericUpDown���� TracBar�� ����
        }

        private void numProgressBar_ValueChanged(object sender, EventArgs e)
        {
            pbTest.Value = int.Parse(numProgressBar.Value.ToString()); // NumericUpDown���� ProgressBar�� ����
        }

        private void btnBlocks_Click(object sender, EventArgs e)
        {
            pbTest.Style = ProgressBarStyle.Blocks; // Blocks ��Ÿ��
        }

        private void btnContinuous_Click(object sender, EventArgs e)
        {
            pbTest.Style = ProgressBarStyle.Continuous;// Continuous ��Ÿ��
        }

        private void btnMarquee_Click(object sender, EventArgs e)
        {
            //pbTest.MarqueeAnimationSpeed = 100; // marquee �ִϸ���Ʈ �ӵ�
            pbTest.Style = ProgressBarStyle.Marquee; // Marquee ��Ÿ��
        }
    }
}
