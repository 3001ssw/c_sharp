using System.Diagnostics;

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
            treeView.ImageList = imageList1; // �̹�������Ʈ �߰�
        }

        private void btnAddRoot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbText.Text))
                return;

            //treeView.Nodes.Add(tbText.Text); // �ؽ�Ʈ �߰�

            // TreeNode ��ü ���� �� �߰�
            //TreeNode nd = new TreeNode();
            //nd.Text = tbText.Text; // text
            //treeView.Nodes.Add(nd);

            // TreeNode ��ü ���� �� �̹������� �߰�
            TreeNode nd = new TreeNode();
            nd.Text = tbText.Text; // text
            nd.ImageIndex = 0; // �⺻ ������ �� ǥ���� �̹��� �ε���
            nd.SelectedImageIndex = 1; // ���� �Ǿ��� �� ǥ���� �̹��� �ε���
            treeView.Nodes.Add(nd);

            tbText.Text = "";
            tbText.Focus();
        }

        private void btnAddSelectNode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbText.Text))
                return;

            TreeNode ndSelect = treeView.SelectedNode; // ������ ��� ��������
            if (ndSelect != null)
            {
                TreeNode nd = new TreeNode();
                nd.Text = tbText.Text; // text
                nd.ImageIndex = 0; // �⺻ ������ �� ǥ���� �̹��� �ε���
                nd.SelectedImageIndex = 1; // ���� �Ǿ��� �� ǥ���� �̹��� �ε���

                ndSelect.Nodes.Add(nd); // ������ ��忡 �߰�
                ndSelect.Expand(); // Ȯ��

                tbText.Text = "";
                tbText.Focus();
            }
        }

        private void btnRemoveSelectNode_Click(object sender, EventArgs e)
        {
            TreeNode ndSelect = treeView.SelectedNode;
            if (ndSelect != null)
            {
                TreeNode ndParnet = ndSelect.Parent; // ������ ����� �θ�
                ndParnet.Nodes.Remove(ndSelect); // �θ𿡼� ������ ��� ����
            }
        }

        private void btnClearSelectedNode_Click(object sender, EventArgs e)
        {
            TreeNode ndSelect = treeView.SelectedNode;
            if (ndSelect != null)
            {
                ndSelect.Nodes.Clear(); // ������ ��� ���� ����
            }
        }

        private void btnExpandSelectNode_Click(object sender, EventArgs e)
        {
            TreeNode ndSelect = treeView.SelectedNode;
            if (ndSelect != null)
                ndSelect.Expand(); // Ȯ��
        }

        private void btnCollapseSelectNode_Click(object sender, EventArgs e)
        {
            TreeNode ndSelect = treeView.SelectedNode;
            if (ndSelect != null)
                ndSelect.Collapse(); // ���
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode? ndSelect = e.Node;
            if (ndSelect != null)
            {
                tbText.Text = ndSelect.Text; // ������ �׸��� �ؽ�Ʈ�� TextBox ��Ʈ�ѿ� ǥ��
            }
        }
    }
}
