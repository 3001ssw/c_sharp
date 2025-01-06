using System.Data;
using System.Runtime.InteropServices.JavaScript;
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
            cbViewMode.Items.AddRange(new string[] {
                "View.LargeIcon",
                "View.Details",
                "View.SmallIcon",
                "View.List",
                "View.Tile" });
            cbViewMode.SelectedIndex = 1;

            //listView1.View = View.LargeIcon; // 큰 아이콘 형태
            listView1.View = View.Details; // 세부사항(기본값)
            //listView1.View = View.SmallIcon; // 작은 아이콘 형태
            //listView1.View = View.List; // 간단한 하나의 행
            //listView1.View = View.Tile; // 타일형태
            listView1.Scrollable = true; // 스크롤 표시
            listView1.MultiSelect = true; // 멀티 선택
            listView1.FullRowSelect = true; // 한줄 전체 선택

            listView1.Columns.Add("칼럼1", 100);
            listView1.Columns.Add("칼럼2", 200);
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

            listView1.View = iStyle;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listView1.BeginUpdate();
            for (int i = 0; i < 3000; i++)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = $"아이템 {i}";
                listViewItem.SubItems.Add($"서브아이템1 {i}");
                listViewItem.SubItems.Add($"서브아이템2 {i}");

                listView1.Items.Add(listViewItem);
            }
            listView1.EndUpdate();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems == null)
                return;
        }
    }
}
