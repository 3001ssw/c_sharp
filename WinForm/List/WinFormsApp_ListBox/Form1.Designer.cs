namespace WinFormsApp_ListBox
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
            listBox1 = new ListBox();
            btnAdd = new Button();
            btnDelete = new Button();
            tbInput = new TextBox();
            groupBox1 = new GroupBox();
            btnDeleteAll = new Button();
            groupBox2 = new GroupBox();
            listBox2 = new ListBox();
            btnMultiAdd = new Button();
            btnMultiDelete = new Button();
            btnSort = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(6, 22);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(200, 124);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(220, 61);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(169, 23);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "추가";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(220, 90);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(169, 23);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "삭제";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // tbInput
            // 
            tbInput.Location = new Point(220, 22);
            tbInput.Name = "tbInput";
            tbInput.Size = new Size(169, 23);
            tbInput.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(tbInput);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(btnSort);
            groupBox1.Controls.Add(btnDeleteAll);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(407, 189);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Simple ListBox";
            // 
            // btnDeleteAll
            // 
            btnDeleteAll.Location = new Point(220, 119);
            btnDeleteAll.Name = "btnDeleteAll";
            btnDeleteAll.Size = new Size(169, 23);
            btnDeleteAll.TabIndex = 4;
            btnDeleteAll.Text = "모두 삭제";
            btnDeleteAll.UseVisualStyleBackColor = true;
            btnDeleteAll.Click += btnDeleteAll_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(listBox2);
            groupBox2.Controls.Add(btnMultiAdd);
            groupBox2.Controls.Add(btnMultiDelete);
            groupBox2.Location = new Point(12, 222);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(407, 158);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Multi Select ListBox";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(6, 22);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(200, 124);
            listBox2.TabIndex = 0;
            // 
            // btnMultiAdd
            // 
            btnMultiAdd.Location = new Point(220, 61);
            btnMultiAdd.Name = "btnMultiAdd";
            btnMultiAdd.Size = new Size(169, 23);
            btnMultiAdd.TabIndex = 2;
            btnMultiAdd.Text = "10000개 추가";
            btnMultiAdd.UseVisualStyleBackColor = true;
            btnMultiAdd.Click += btnMultiAdd_Click;
            // 
            // btnMultiDelete
            // 
            btnMultiDelete.Location = new Point(220, 90);
            btnMultiDelete.Name = "btnMultiDelete";
            btnMultiDelete.Size = new Size(169, 23);
            btnMultiDelete.TabIndex = 3;
            btnMultiDelete.Text = "선택한 항목 삭제";
            btnMultiDelete.UseVisualStyleBackColor = true;
            btnMultiDelete.Click += btnMultiDelete_Click;
            // 
            // btnSort
            // 
            btnSort.Location = new Point(220, 148);
            btnSort.Name = "btnSort";
            btnSort.Size = new Size(169, 23);
            btnSort.TabIndex = 4;
            btnSort.Text = "정렬";
            btnSort.UseVisualStyleBackColor = true;
            btnSort.Click += btnSort_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(475, 392);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button btnAdd;
        private Button btnDelete;
        private TextBox tbInput;
        private GroupBox groupBox1;
        private Button btnDeleteAll;
        private GroupBox groupBox2;
        private ListBox listBox2;
        private Button btnMultiAdd;
        private Button btnMultiDelete;
        private Button btnSort;
    }
}
