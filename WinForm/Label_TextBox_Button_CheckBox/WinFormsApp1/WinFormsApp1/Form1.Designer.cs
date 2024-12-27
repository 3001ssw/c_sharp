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
            lbDisp = new Label();
            tbInput = new TextBox();
            btnDisp = new Button();
            chkDisp = new CheckBox();
            SuspendLayout();
            // 
            // lbDisp
            // 
            lbDisp.BorderStyle = BorderStyle.FixedSingle;
            lbDisp.Location = new Point(19, 16);
            lbDisp.Name = "lbDisp";
            lbDisp.Size = new Size(140, 23);
            lbDisp.TabIndex = 0;
            lbDisp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbInput
            // 
            tbInput.Location = new Point(192, 16);
            tbInput.Name = "tbInput";
            tbInput.Size = new Size(100, 23);
            tbInput.TabIndex = 1;
            tbInput.TextChanged += tbInput_TextChanged;
            // 
            // btnDisp
            // 
            btnDisp.Location = new Point(312, 16);
            btnDisp.Name = "btnDisp";
            btnDisp.Size = new Size(75, 23);
            btnDisp.TabIndex = 2;
            btnDisp.Text = "버튼";
            btnDisp.UseVisualStyleBackColor = true;
            btnDisp.Click += btnDisp_Click;
            // 
            // chkDisp
            // 
            chkDisp.AutoSize = true;
            chkDisp.Location = new Point(412, 18);
            chkDisp.Name = "chkDisp";
            chkDisp.Size = new Size(85, 19);
            chkDisp.TabIndex = 3;
            chkDisp.Text = "Unchecked";
            chkDisp.UseVisualStyleBackColor = true;
            chkDisp.CheckedChanged += chkDisp_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(576, 215);
            Controls.Add(chkDisp);
            Controls.Add(btnDisp);
            Controls.Add(tbInput);
            Controls.Add(lbDisp);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbDisp;
        private TextBox tbInput;
        private Button btnDisp;
        private CheckBox chkDisp;
    }
}
