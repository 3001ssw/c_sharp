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
            // �޺� �ڽ� ����
            cbViewMode.Items.AddRange(new string[] {
                "View.LargeIcon",
                "View.Details",
                "View.SmallIcon",
                "View.List",
                "View.Tile" });
            cbViewMode.SelectedIndex = 1; // �޺� �ڽ� Details �����ϰ�

            // ����Ʈ��
            //listView.View = View.LargeIcon; // ū ������ ����
            listView.View = View.Details; // ���λ���(�⺻��)
            //listView.View = View.SmallIcon; // ���� ������ ����
            //listView.View = View.List; // ������ �ϳ��� ��
            //listView.View = View.Tile; // Ÿ������
            listView.Scrollable = true; // ��ũ�� ǥ��
            listView.MultiSelect = true; // ��Ƽ ����
            listView.FullRowSelect = true; // ���� ��ü ����
            listView.GridLines = true; // ���� ǥ��

            // ����Ʈ�� Į��
            listView.Columns.Add("Į��1", 100); // Į�� �߰�, �ʺ� 100
            listView.Columns.Add("Į��2", 100); // Į�� �߰�, �ʺ� 100
            listView.Columns.Add("Į��3", 100); // Į�� �߰�, �ʺ� 100
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listView.BeginUpdate();
            for (int i = 0; i < 3000; i++)
            {
                ListViewItem listViewItem = new ListViewItem(new[] { $"{i}��, 1��°", $"{i}��, 2��°", $"{i}��, 3��°" });

                //�Ǵ�
                //ListViewItem listViewItem = new ListViewItem();
                //listViewItem.Text = $"{i}�� 1��°";
                //listViewItem.SubItems.Add($"{i}�� 2��°");
                //listViewItem.SubItems.Add($"{i}�� 3��°");

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

            listView.View = iStyle; // View ��Ÿ�� ����
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count <= 0) // �����Ѱ� ������ �׳� �Լ� ����
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

            tbSelectInfo.Text = stringBuilder.ToString(); // �ؽ�Ʈ �ڽ��� ǥ��
        }
    }
}
