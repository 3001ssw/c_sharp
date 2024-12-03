using System.ComponentModel.Design.Serialization;

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

            //"abc" ���ڿ��� �ִ���
            string txt = tbText.Text;
            bool bContains = txt.Contains("abc");
            lbContain.Text = "Contain \"abc\": " + bContains;

            //"abc" ���ڿ��� ������
            bool bEqual = txt.Equals("abc");
            lbEqual.Text = "Equal \"abc\": " + bEqual;

            //���� ��ȯ
            int iLength = txt.Length;
            lbLength.Text = "Length: " + iLength;

            //'a'�� 'x'�� ��ȯ
            string txtReplace = txt.Replace('a', 'x');
            lbReplace.Text = "Replace \'a\' to \'b\': " + txt + " to " + txtReplace;

            //��ǥ(,)�� �������� ������
            string[] strSplit = txt.Split(',');
            lbSplit.Text = "Split(,): \n";
            for (int i = 0; i < strSplit.Length; i++)
            {
                lbSplit.Text += string.Format("{0}: {1}\n", i, strSplit[i]);
            }

            //2��° �ε������� 3�� �����ϱ�
            if (tbText.Text.Length < 5)
                lbSubstring.Text = "Substring(2, 3): " + "input text least 5word";
            else
            {
                string txtSubstring = txt.Substring(2, 3);
                lbSubstring.Text = "Substring(2, 3): " + txtSubstring;
            }

            //�ҹ��ڷ�
            string strToLower = txt.ToLower();
            lbToLower.Text = "ToLower: " + strToLower;

            //�빮�ڷ�
            string strToUpper = txt.ToUpper();
            lbToUpper.Text = "ToUpper: " + strToUpper;

            //���� ���ڿ� ����
            string strTrim = txt.Trim();
            lbTrim.Text = "Trim: " + strTrim;

            //"abc" ���ڿ��� ��, ������ 0 �ٸ��� -1 �Ǵ� 1
            int iCompare = string.Compare(txt, "abc");
            lbCompare.Text = "Compare \"abc\": " + iCompare;

            //"abc" ���ڿ��� ��, ������ 0 �ٸ��� -1 �Ǵ� 1
            int iCompareTo = txt.CompareTo("abc");
            lbCompareTo.Text = "CompareTo \"abc\": " + iCompareTo;

            //�ڿ� �̾� ���̱�
            string strConcat = string.Concat(txt, "123");
            lbConcat.Text = "concat \"123\": " + strConcat;

            //���ڿ� ã��, ã�� �ε��� ��ȯ
            int iIndexOf = txt.IndexOf('c');
            lbIndexOf.Text = "IndexOf \'c\': : " + iIndexOf;

            //1��° �ε����� "def" ���ڿ� ����
            string strInsert = txt.Insert(1, "def");
            lbInsert.Text = "Insert \"def\" index 1: " + strInsert;
        }
    }
}
