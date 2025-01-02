using System.Diagnostics;
using System.Text;

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
            string[] items = {"가", "나", "다", "라"};
            cbCombo.Items.AddRange(items);
            cbCombo.SelectedIndex = 0;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(tbText.Text);
            stringBuilder.AppendLine(chkCheckOption.Checked.ToString());
            stringBuilder.AppendLine(cbCombo.Text);
            stringBuilder.AppendLine(rdOption1.Checked.ToString());
            stringBuilder.AppendLine(rdOption2.Checked.ToString());
            stringBuilder.AppendLine(rdOption3.Checked.ToString());
            string strWrite = stringBuilder.ToString();
            Debug.WriteLine(strWrite);

            if (strWrite != string.Empty)
            {
            }
        }
    }
}
