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
            btnRead = new Button();
            btnWrite = new Button();
            tbText = new TextBox();
            chkCheckOption = new CheckBox();
            cbCombo = new ComboBox();
            groupBox1 = new GroupBox();
            rdOption3 = new RadioButton();
            rdOption2 = new RadioButton();
            rdOption1 = new RadioButton();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnRead
            // 
            btnRead.Location = new Point(12, 292);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(117, 23);
            btnRead.TabIndex = 4;
            btnRead.Text = "Read";
            btnRead.UseVisualStyleBackColor = true;
            btnRead.Click += btnRead_Click;
            // 
            // btnWrite
            // 
            btnWrite.Location = new Point(135, 292);
            btnWrite.Name = "btnWrite";
            btnWrite.Size = new Size(117, 23);
            btnWrite.TabIndex = 5;
            btnWrite.Text = "Write";
            btnWrite.UseVisualStyleBackColor = true;
            btnWrite.Click += btnWrite_Click;
            // 
            // tbText
            // 
            tbText.Location = new Point(12, 12);
            tbText.Name = "tbText";
            tbText.Size = new Size(240, 23);
            tbText.TabIndex = 0;
            // 
            // chkCheckOption
            // 
            chkCheckOption.AutoSize = true;
            chkCheckOption.Location = new Point(12, 57);
            chkCheckOption.Name = "chkCheckOption";
            chkCheckOption.Size = new Size(78, 19);
            chkCheckOption.TabIndex = 1;
            chkCheckOption.Text = "체크 옵션";
            chkCheckOption.UseVisualStyleBackColor = true;
            // 
            // cbCombo
            // 
            cbCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCombo.FormattingEnabled = true;
            cbCombo.Location = new Point(12, 91);
            cbCombo.Name = "cbCombo";
            cbCombo.Size = new Size(240, 23);
            cbCombo.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rdOption3);
            groupBox1.Controls.Add(rdOption2);
            groupBox1.Controls.Add(rdOption1);
            groupBox1.Location = new Point(12, 137);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(240, 133);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            // 
            // rdOption3
            // 
            rdOption3.Location = new Point(22, 92);
            rdOption3.Name = "rdOption3";
            rdOption3.Size = new Size(145, 24);
            rdOption3.TabIndex = 2;
            rdOption3.TabStop = true;
            rdOption3.Text = "옵션3";
            rdOption3.UseVisualStyleBackColor = true;
            // 
            // rdOption2
            // 
            rdOption2.Location = new Point(22, 62);
            rdOption2.Name = "rdOption2";
            rdOption2.Size = new Size(145, 24);
            rdOption2.TabIndex = 1;
            rdOption2.TabStop = true;
            rdOption2.Text = "옵션2";
            rdOption2.UseVisualStyleBackColor = true;
            // 
            // rdOption1
            // 
            rdOption1.Location = new Point(22, 32);
            rdOption1.Name = "rdOption1";
            rdOption1.Size = new Size(145, 24);
            rdOption1.TabIndex = 0;
            rdOption1.TabStop = true;
            rdOption1.Text = "옵션1";
            rdOption1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(274, 331);
            Controls.Add(groupBox1);
            Controls.Add(cbCombo);
            Controls.Add(chkCheckOption);
            Controls.Add(tbText);
            Controls.Add(btnWrite);
            Controls.Add(btnRead);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRead;
        private Button btnWrite;
        private TextBox tbText;
        private CheckBox chkCheckOption;
        private ComboBox cbCombo;
        private GroupBox groupBox1;
        private RadioButton rdOption3;
        private RadioButton rdOption2;
        private RadioButton rdOption1;
    }
}
