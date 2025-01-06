using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace WinFormsApp_ListBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.ScrollAlwaysVisible = true; // 항상 스크롤바 보이게
            //listBox1.SelectionMode = SelectionMode.One; // 단일 선택(기본값)

            listBox2.ScrollAlwaysVisible = true; // 항상 스크롤바 보이게
            //listBox2.SelectionMode = SelectionMode.None; // 선택 못함
            //listBox2.SelectionMode = SelectionMode.One; // 단일 선택(기본값)
            //listBox2.SelectionMode = SelectionMode.MultiSimple; // 다중 선택 가능. 단순 클릭으로 선택/선택해제 가능
            listBox2.SelectionMode = SelectionMode.MultiExtended; // 다중 선택 가능. Ctrl, Shift 키와 마우스 클릭으로 조합
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 추가 버튼
            if (tbInput.Text == string.Empty)
                return;

            listBox1.Items.Add(tbInput.Text); // 텍스트 박스의 문자열을 리스트박스에 추가
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 삭제 버튼
            if (listBox1.SelectedItem == null)
                return;

            listBox1.Items.Remove(listBox1.SelectedItem); // 선택한 항목 한개 삭제
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            // 모두 삭제 버튼
            listBox1.Items.Clear();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            // 정렬 버튼
            listBox1.Sorted = !listBox1.Sorted;
            string strMsg = $"정렬 속성값이 [{listBox1.Sorted}]으로 설정되었습니다.";
            MessageBox.Show(strMsg);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            tbInput.Text = listBox1.SelectedItem.ToString();
        }

        private void btnMultiAdd_Click(object sender, EventArgs e)
        {
            // 10000개 아이템 추가 버튼
            //listBox2.BeginUpdate(); // 화면 갱신을 일시적으로 중지

            for (int i = 0; i < 10000; i++)
            {
                string strItem = i.ToString();
                listBox2.Items.Add(strItem);
            }

            //listBox2.EndUpdate(); // 화면 갱신을 다시 시작
        }

        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            // 선택한 항목 삭제 버튼
            listBox2.BeginUpdate(); // 화면 갱신을 일시적으로 중지

            // 삭제는 역순으로 해야함
            for (int iSelectIndex = listBox2.SelectedItems.Count - 1; 0 <= iSelectIndex; iSelectIndex--)
            {
                int iItemIndex = listBox2.SelectedIndices[iSelectIndex];
                listBox2.Items.RemoveAt(iItemIndex);
            }

            listBox2.EndUpdate(); // 화면 갱신을 다시 시작
        }
    }
}
