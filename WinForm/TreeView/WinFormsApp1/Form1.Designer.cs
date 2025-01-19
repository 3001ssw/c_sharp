namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            treeView = new TreeView();
            tbText = new TextBox();
            label2 = new Label();
            btnAddSelectNode = new Button();
            btnRemoveSelectNode = new Button();
            btnExpandSelectNode = new Button();
            btnCollapseSelectNode = new Button();
            btnAddRoot = new Button();
            imageList1 = new ImageList(components);
            btnClearSelectedNode = new Button();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.FullRowSelect = true;
            treeView.Location = new Point(26, 18);
            treeView.Name = "treeView";
            treeView.Size = new Size(305, 260);
            treeView.TabIndex = 0;
            treeView.AfterSelect += treeView_AfterSelect;
            // 
            // tbText
            // 
            tbText.Location = new Point(443, 16);
            tbText.Name = "tbText";
            tbText.Size = new Size(236, 23);
            tbText.TabIndex = 2;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(337, 18);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 1;
            label2.Text = "Text";
            // 
            // btnAddSelectNode
            // 
            btnAddSelectNode.Location = new Point(453, 59);
            btnAddSelectNode.Name = "btnAddSelectNode";
            btnAddSelectNode.Size = new Size(110, 23);
            btnAddSelectNode.TabIndex = 4;
            btnAddSelectNode.Text = "선택 노드에 추가";
            btnAddSelectNode.UseVisualStyleBackColor = true;
            btnAddSelectNode.Click += btnAddSelectNode_Click;
            // 
            // btnRemoveSelectNode
            // 
            btnRemoveSelectNode.Location = new Point(569, 59);
            btnRemoveSelectNode.Name = "btnRemoveSelectNode";
            btnRemoveSelectNode.Size = new Size(110, 23);
            btnRemoveSelectNode.TabIndex = 5;
            btnRemoveSelectNode.Text = "선택 노드 삭제";
            btnRemoveSelectNode.UseVisualStyleBackColor = true;
            btnRemoveSelectNode.Click += btnRemoveSelectNode_Click;
            // 
            // btnExpandSelectNode
            // 
            btnExpandSelectNode.Location = new Point(453, 155);
            btnExpandSelectNode.Name = "btnExpandSelectNode";
            btnExpandSelectNode.Size = new Size(110, 23);
            btnExpandSelectNode.TabIndex = 7;
            btnExpandSelectNode.Text = "선택 노드 확장";
            btnExpandSelectNode.UseVisualStyleBackColor = true;
            btnExpandSelectNode.Click += btnExpandSelectNode_Click;
            // 
            // btnCollapseSelectNode
            // 
            btnCollapseSelectNode.Location = new Point(569, 155);
            btnCollapseSelectNode.Name = "btnCollapseSelectNode";
            btnCollapseSelectNode.Size = new Size(110, 23);
            btnCollapseSelectNode.TabIndex = 8;
            btnCollapseSelectNode.Text = "선택 노드 축소";
            btnCollapseSelectNode.UseVisualStyleBackColor = true;
            btnCollapseSelectNode.Click += btnCollapseSelectNode_Click;
            // 
            // btnAddRoot
            // 
            btnAddRoot.Location = new Point(337, 59);
            btnAddRoot.Name = "btnAddRoot";
            btnAddRoot.Size = new Size(110, 23);
            btnAddRoot.TabIndex = 3;
            btnAddRoot.Text = "Root에 노드 추가";
            btnAddRoot.UseVisualStyleBackColor = true;
            btnAddRoot.Click += btnAddRoot_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "normal.png");
            imageList1.Images.SetKeyName(1, "select.png");
            // 
            // btnClearSelectedNode
            // 
            btnClearSelectedNode.Location = new Point(569, 88);
            btnClearSelectedNode.Name = "btnClearSelectedNode";
            btnClearSelectedNode.Size = new Size(110, 23);
            btnClearSelectedNode.TabIndex = 6;
            btnClearSelectedNode.Text = "하위 노드 삭제";
            btnClearSelectedNode.UseVisualStyleBackColor = true;
            btnClearSelectedNode.Click += btnClearSelectedNode_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 294);
            Controls.Add(btnCollapseSelectNode);
            Controls.Add(btnClearSelectedNode);
            Controls.Add(btnRemoveSelectNode);
            Controls.Add(btnExpandSelectNode);
            Controls.Add(btnAddRoot);
            Controls.Add(btnAddSelectNode);
            Controls.Add(label2);
            Controls.Add(tbText);
            Controls.Add(treeView);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeView;
        private TextBox tbText;
        private Label label2;
        private Button btnAddSelectNode;
        private Button btnRemoveSelectNode;
        private Button btnExpandSelectNode;
        private Button btnCollapseSelectNode;
        private Button btnAddRoot;
        private ImageList imageList1;
        private Button btnClearSelectedNode;
    }
}
