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

            //"abc" 문자열이 있는지
            string txt = tbText.Text;
            bool bContains = txt.Contains("abc");
            lbContain.Text = "Contain \"abc\": " + bContains;

            //"abc" 문자열과 같은지
            bool bEqual = txt.Equals("abc");
            lbEqual.Text = "Equal \"abc\": " + bEqual;

            //길이 반환
            int iLength = txt.Length;
            lbLength.Text = "Length: " + iLength;

            //'a'를 'x'로 변환
            string txtReplace = txt.Replace('a', 'x');
            lbReplace.Text = "Replace \'a\' to \'b\': " + txt + " to " + txtReplace;

            //쉼표(,)를 기준으로 나누기
            string[] strSplit = txt.Split(',');
            lbSplit.Text = "Split(,): \n";
            for (int i = 0; i < strSplit.Length; i++)
            {
                lbSplit.Text += string.Format("{0}: {1}\n", i, strSplit[i]);
            }

            //2번째 인덱스에서 3개 추출하기
            if (tbText.Text.Length < 5)
                lbSubstring.Text = "Substring(2, 3): " + "input text least 5word";
            else
            {
                string txtSubstring = txt.Substring(2, 3);
                lbSubstring.Text = "Substring(2, 3): " + txtSubstring;
            }

            //소문자로
            string strToLower = txt.ToLower();
            lbToLower.Text = "ToLower: " + strToLower;

            //대문자로
            string strToUpper = txt.ToUpper();
            lbToUpper.Text = "ToUpper: " + strToUpper;

            //양쪽 빈문자열 제거
            string strTrim = txt.Trim();
            lbTrim.Text = "Trim: " + strTrim;

            //"abc" 문자열과 비교, 같으면 0 다르면 -1 또는 1
            int iCompare = string.Compare(txt, "abc");
            lbCompare.Text = "Compare \"abc\": " + iCompare;

            //"abc" 문자열과 비교, 같으면 0 다르면 -1 또는 1
            int iCompareTo = txt.CompareTo("abc");
            lbCompareTo.Text = "CompareTo \"abc\": " + iCompareTo;

            //뒤에 이어 붙이기
            string strConcat = string.Concat(txt, "123");
            lbConcat.Text = "concat \"123\": " + strConcat;

            //문자열 찾기, 찾은 인덱스 반환
            int iIndexOf = txt.IndexOf('c');
            lbIndexOf.Text = "IndexOf \'c\': : " + iIndexOf;

            //1번째 인덱스에 "def" 문자열 삽입
            string strInsert = txt.Insert(1, "def");
            lbInsert.Text = "Insert \"def\" index 1: " + strInsert;
        }
    }
}
