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
            listBox1.ScrollAlwaysVisible = true; // �׻� ��ũ�ѹ� ���̰�
            //listBox1.SelectionMode = SelectionMode.One; // ���� ����(�⺻��)

            listBox2.ScrollAlwaysVisible = true; // �׻� ��ũ�ѹ� ���̰�
            //listBox2.SelectionMode = SelectionMode.None; // ���� ����
            //listBox2.SelectionMode = SelectionMode.One; // ���� ����(�⺻��)
            //listBox2.SelectionMode = SelectionMode.MultiSimple; // ���� ���� ����. �ܼ� Ŭ������ ����/�������� ����
            listBox2.SelectionMode = SelectionMode.MultiExtended; // ���� ���� ����. Ctrl, Shift Ű�� ���콺 Ŭ������ ����
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // �߰� ��ư
            if (tbInput.Text == string.Empty)
                return;

            listBox1.Items.Add(tbInput.Text); // �ؽ�Ʈ �ڽ��� ���ڿ��� ����Ʈ�ڽ��� �߰�
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // ���� ��ư
            if (listBox1.SelectedItem == null)
                return;

            listBox1.Items.Remove(listBox1.SelectedItem); // ������ �׸� �Ѱ� ����
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            // ��� ���� ��ư
            listBox1.Items.Clear();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            // ���� ��ư
            listBox1.Sorted = !listBox1.Sorted;
            string strMsg = $"���� �Ӽ����� [{listBox1.Sorted}]���� �����Ǿ����ϴ�.";
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
            // 10000�� ������ �߰� ��ư
            //listBox2.BeginUpdate(); // ȭ�� ������ �Ͻ������� ����

            for (int i = 0; i < 10000; i++)
            {
                string strItem = i.ToString();
                listBox2.Items.Add(strItem);
            }

            //listBox2.EndUpdate(); // ȭ�� ������ �ٽ� ����
        }

        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            // ������ �׸� ���� ��ư
            listBox2.BeginUpdate(); // ȭ�� ������ �Ͻ������� ����

            // ������ �������� �ؾ���
            for (int iSelectIndex = listBox2.SelectedItems.Count - 1; 0 <= iSelectIndex; iSelectIndex--)
            {
                int iItemIndex = listBox2.SelectedIndices[iSelectIndex];
                listBox2.Items.RemoveAt(iItemIndex);
            }

            listBox2.EndUpdate(); // ȭ�� ������ �ٽ� ����
        }
    }
}
