namespace WinFormsApp_ListView
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
            listView1 = new ListView();
            cbViewMode = new ComboBox();
            btnAdd = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 21);
            listView1.Name = "listView1";
            listView1.Size = new Size(411, 208);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // cbViewMode
            // 
            cbViewMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbViewMode.FormattingEnabled = true;
            cbViewMode.Location = new Point(532, 21);
            cbViewMode.Name = "cbViewMode";
            cbViewMode.Size = new Size(136, 23);
            cbViewMode.TabIndex = 1;
            cbViewMode.SelectedIndexChanged += cbViewMode_SelectedIndexChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(450, 206);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(136, 23);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "추가";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(467, 24);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 3;
            label1.Text = "모드 변경";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(695, 244);
            Controls.Add(label1);
            Controls.Add(btnAdd);
            Controls.Add(cbViewMode);
            Controls.Add(listView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private ComboBox cbViewMode;
        private Button btnAdd;
        private Label label1;
    }
}
