using System.Data;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp_ListView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 콤보 박스 설정
            cbViewMode.Items.AddRange(new string[] {
                "View.LargeIcon",
                "View.Details",
                "View.SmallIcon",
                "View.List",
                "View.Tile" });
            cbViewMode.SelectedIndex = 1; // 콤보 박스 Details 선택하게

            // 리스트뷰
            //listView.View = View.LargeIcon; // 큰 아이콘 형태
            listView.View = View.Details; // 세부사항(기본값)
            //listView.View = View.SmallIcon; // 작은 아이콘 형태
            //listView.View = View.List; // 간단한 하나의 행
            //listView.View = View.Tile; // 타일형태
            listView.Scrollable = true; // 스크롤 표시
            listView.MultiSelect = true; // 멀티 선택
            listView.FullRowSelect = true; // 한줄 전체 선택
            listView.GridLines = true; // 라인 표시

            // 리스트뷰 칼럼
            listView.Columns.Add("칼럼1", 100); // 칼럼 추가, 너비 100
            listView.Columns.Add("칼럼2", 100); // 칼럼 추가, 너비 100
            listView.Columns.Add("칼럼3", 100); // 칼럼 추가, 너비 100
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listView.BeginUpdate();
            for (int i = 0; i < 3000; i++)
            {
                ListViewItem listViewItem = new ListViewItem(new[] { $"{i}행, 1번째", $"{i}행, 2번째", $"{i}행, 3번째" });

                //또는
                //ListViewItem listViewItem = new ListViewItem();
                //listViewItem.Text = $"{i}행 1번째";
                //listViewItem.SubItems.Add($"{i}행 2번째");
                //listViewItem.SubItems.Add($"{i}행 3번째");

                listView.Items.Add(listViewItem);
            }
            listView.EndUpdate();
        }

        private void cbViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            View iStyle = View.Details;
            switch (cbViewMode.Text)
            {
                case "View.LargeIcon":
                    iStyle = View.LargeIcon;
                    break;
                case "View.Details":
                    iStyle = View.Details;
                    break;
                case "View.SmallIcon":
                    iStyle = View.SmallIcon;
                    break;
                case "View.List":
                    iStyle = View.List;
                    break;
                case "View.Tile":
                    iStyle = View.Tile;
                    break;
            }

            listView.View = iStyle; // View 스타일 설정
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count <= 0) // 선택한거 없으면 그냥 함수 종료
                return;

            StringBuilder stringBuilder = new StringBuilder();
            foreach (ListViewItem item in listView.SelectedItems)
            {
                stringBuilder.AppendLine("=== " + item.Text + " ===");
                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    stringBuilder.AppendLine(subitem.Text);
                }
                stringBuilder.AppendLine();
            }

            tbSelectInfo.Text = stringBuilder.ToString(); // 텍스트 박스에 표시
        }
    }
}
