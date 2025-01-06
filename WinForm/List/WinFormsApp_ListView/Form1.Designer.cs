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
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Location = new Point(33, 24);
            listView1.Name = "listView1";
            listView1.Size = new Size(266, 122);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // cbViewMode
            // 
            cbViewMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbViewMode.FormattingEnabled = true;
            cbViewMode.Location = new Point(351, 24);
            cbViewMode.Name = "cbViewMode";
            cbViewMode.Size = new Size(136, 23);
            cbViewMode.TabIndex = 1;
            cbViewMode.SelectedIndexChanged += cbViewMode_SelectedIndexChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(34, 159);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "추가";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAdd);
            Controls.Add(cbViewMode);
            Controls.Add(listView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private ComboBox cbViewMode;
        private Button btnAdd;
    }
}
