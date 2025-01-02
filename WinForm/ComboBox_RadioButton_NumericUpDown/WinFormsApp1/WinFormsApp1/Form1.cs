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
            //cbFruit.Items.Add("사과");
            //cbFruit.Items.Add("배");
            //cbFruit.Items.Add("딸기");
            //cbFruit.Items.Add("수박");
            //cbFruit.Items.Add("귤");
            //cbFruit.Items.Add("포도");

            // add 2
            string[] fruits = { "사과", "배", "딸기", "수박", "귤", "포도" };
            cbFruit.Items.AddRange(fruits);

            cbFruit.SelectedIndex = 0; // 첫번째 항목 선택
            int iCount = cbFruit.Items.Count; // 개수 얻기


            rdSelect1.Checked = true; // 라디오 선택하기

            numYear.Minimum = 1900; // 최소값
            numYear.Maximum = 2199; // 최대값
            numYear.Increment = 5; // 증/감버튼 클릭 시, 상/하 방향키 클릭 시, 또는 마우스 휠 변경 되는 delta 값
            //numYear.DecimalPlaces = 2; // 소수점 이하 몇 자리 까지 표시할지
            numYear.Value = 1900; // 값 설정

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
