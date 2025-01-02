using Microsoft.VisualBasic.Devices;

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
            // add 1
            //cbFruit.Items.Add("���");
            //cbFruit.Items.Add("��");
            //cbFruit.Items.Add("����");
            //cbFruit.Items.Add("����");
            //cbFruit.Items.Add("��");
            //cbFruit.Items.Add("����");

            // add 2
            string[] fruits = { "���", "��", "����", "����", "��", "����" };
            cbFruit.Items.AddRange(fruits);

            cbFruit.SelectedIndex = 0; // ù��° �׸� ����
            int iCount = cbFruit.Items.Count; // ���� ���


            rdSelect1.Checked = true; // ���� �����ϱ�

            numYear.Minimum = 1900; // �ּҰ�
            numYear.Maximum = 2199; // �ִ밪
            numYear.Increment = 5; // ��/����ư Ŭ�� ��, ��/�� ����Ű Ŭ�� ��, �Ǵ� ���콺 �� ���� �Ǵ� delta ��
            //numYear.DecimalPlaces = 2; // �Ҽ��� ���� �� �ڸ� ���� ǥ������
            numYear.Value = 1900; // �� ����

            numMonth.Minimum = 1;
            numMonth.Maximum = 12;
            numMonth.Value = 1;

            numDay.Minimum = 1;
            numDay.Maximum = 31;
            numDay.Value = 1;
        }

        private void cbFruit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbComboBox.Text = "index: " + cbFruit.SelectedIndex + ", text: " + cbFruit.Text;
        }

        private void rdSelect_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdSelect = (RadioButton)sender;
            lbRadio.Text = "select radio text: " + rdSelect.Text;
        }

        private void numYearMonthDay_ValueChanged(object sender, EventArgs e)
        {
            string txt = string.Format($"{numYear.Value}-{numMonth.Value}-{numDay.Value}");
            lbNumeric.Text = txt;
        }
    }
}
