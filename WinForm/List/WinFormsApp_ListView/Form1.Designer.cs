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
            listView = new ListView();
            cbViewMode = new ComboBox();
            btnAdd = new Button();
            label1 = new Label();
            tbSelectInfo = new TextBox();
            SuspendLayout();
            // 
            // listView
            // 
            listView.Location = new Point(12, 21);
            listView.Name = "listView";
            listView.Size = new Size(411, 208);
            listView.TabIndex = 0;
            listView.UseCompatibleStateImageBehavior = false;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            // 
            // cbViewMode
            // 
            cbViewMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbViewMode.FormattingEnabled = true;
            cbViewMode.Location = new Point(289, 233);
            cbViewMode.Name = "cbViewMode";
            cbViewMode.Size = new Size(136, 23);
            cbViewMode.TabIndex = 3;
            cbViewMode.SelectedIndexChanged += cbViewMode_SelectedIndexChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 236);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(136, 23);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "추가";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(194, 236);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 2;
            label1.Text = "View 보기 모드";
            // 
            // tbSelectInfo
            // 
            tbSelectInfo.BorderStyle = BorderStyle.FixedSingle;
            tbSelectInfo.Location = new Point(429, 21);
            tbSelectInfo.Multiline = true;
            tbSelectInfo.Name = "tbSelectInfo";
            tbSelectInfo.ReadOnly = true;
            tbSelectInfo.ScrollBars = ScrollBars.Vertical;
            tbSelectInfo.Size = new Size(254, 233);
            tbSelectInfo.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(695, 279);
            Controls.Add(tbSelectInfo);
            Controls.Add(label1);
            Controls.Add(btnAdd);
            Controls.Add(cbViewMode);
            Controls.Add(listView);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView;
        private ComboBox cbViewMode;
        private Button btnAdd;
        private Label label1;
        private TextBox tbSelectInfo;
    }
}
