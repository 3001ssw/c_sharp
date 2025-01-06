using System.Data;
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

            listView1.Scrollable = true;

            listView1.Columns.Add("Δ®·³1");
            listView1.Columns.Add("Ό³Έν");
            listView1.Columns.Add("Ό³Έν");
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
                listViewItem.Text = $"item{i}";
                listViewItem.SubItems.Add($"SubItems1 {i}");
                listViewItem.SubItems.Add($"SubItems2 {i}");
                listView1.Items.Add(listViewItem);
            }
            listView1.EndUpdate();
        }
    }
}
