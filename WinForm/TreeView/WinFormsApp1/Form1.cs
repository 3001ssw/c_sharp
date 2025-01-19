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
            treeView.ImageList = imageList1; // 이미지리스트 추가
        }

        private void btnAddRoot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbText.Text))
                return;

            //treeView.Nodes.Add(tbText.Text); // 텍스트 추가

            // TreeNode 객체 생성 후 추가
            //TreeNode nd = new TreeNode();
            //nd.Text = tbText.Text; // text
            //treeView.Nodes.Add(nd);

            // TreeNode 객체 생성 후 이미지까지 추가
            TreeNode nd = new TreeNode();
            nd.Text = tbText.Text; // text
            nd.ImageIndex = 0; // 기본 상태일 때 표시할 이미지 인덱스
            nd.SelectedImageIndex = 1; // 선택 되었을 때 표시할 이미지 인덱스
            treeView.Nodes.Add(nd);

            tbText.Text = "";
            tbText.Focus();
        }

        private void btnAddSelectNode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbText.Text))
                return;

            TreeNode ndSelect = treeView.SelectedNode; // 선택한 노드 가져오기
            if (ndSelect != null)
            {
                TreeNode nd = new TreeNode();
                nd.Text = tbText.Text; // text
                nd.ImageIndex = 0; // 기본 상태일 때 표시할 이미지 인덱스
                nd.SelectedImageIndex = 1; // 선택 되었을 때 표시할 이미지 인덱스

                ndSelect.Nodes.Add(nd); // 선택한 노드에 추가
                ndSelect.Expand(); // 확장

                tbText.Text = "";
                tbText.Focus();
            }
        }

        private void btnRemoveSelectNode_Click(object sender, EventArgs e)
        {
            TreeNode ndSelect = treeView.SelectedNode;
            if (ndSelect != null)
            {
                TreeNode ndParnet = ndSelect.Parent; // 선택한 노드의 부모
                ndParnet.Nodes.Remove(ndSelect); // 부모에서 선택한 노드 삭제
            }
        }

        private void btnClearSelectedNode_Click(object sender, EventArgs e)
        {
            TreeNode ndSelect = treeView.SelectedNode;
            if (ndSelect != null)
            {
                ndSelect.Nodes.Clear(); // 선택한 노드 하위 삭제
            }
        }

        private void btnExpandSelectNode_Click(object sender, EventArgs e)
        {
            TreeNode ndSelect = treeView.SelectedNode;
            if (ndSelect != null)
                ndSelect.Expand(); // 확장
        }

        private void btnCollapseSelectNode_Click(object sender, EventArgs e)
        {
            TreeNode ndSelect = treeView.SelectedNode;
            if (ndSelect != null)
                ndSelect.Collapse(); // 축소
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode? ndSelect = e.Node;
            if (ndSelect != null)
            {
                tbText.Text = ndSelect.Text; // 선택한 항목의 텍스트를 TextBox 컨트롤에 표시
            }
        }
    }
}
