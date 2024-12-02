namespace string_basic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            if (tbText.Text == string.Empty)
            {
                MessageBox.Show("input text");
                tbText.Focus();
                return;
            }
            if (tbText.Text.Length < 5)
            {
                MessageBox.Show("input text least 5word");
                tbText.Focus();
                return;
            }

            string txt = tbText.Text;
            bool bContains = txt.Contains("abc");
            lbContain.Text = "Contain \"abc\": " + bContains;

            bool bEqual = txt.Equals("abc");
            lbEqual.Text = "Equal \"abc\": " + bEqual;

            int iLength = txt.Length;
            lbLength.Text = "Length: " + iLength;

            string txtReplace = txt.Replace('a', 'x');
            lbReplace.Text = "Replace \'a\' to \'b\': " + txt + " to " + txtReplace;

            string[] strSplit = txt.Split(',');
            lbSplit.Text = "Split(,): \n";
            for (int i = 0; i < strSplit.Length; i++)
            {
                lbSplit.Text += string.Format("{0}: {1}\n", i, strSplit[i]);
            }

            string txtSubstring = txt.Substring(2, 3);
            lbSubstring.Text = "Substring(2, 3): " + txtSubstring;

            string strToLower = txt.ToLower();
            lbToLower.Text = "ToLower: " + strToLower;

            string strToUpper = txt.ToUpper();
            lbToUpper.Text = "ToUpper: " + strToUpper;

            string strTrim = txt.Trim();
            lbTrim.Text = "Trim: " + strTrim;
        }
    }
}
