namespace string_basic
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
            tbText = new TextBox();
            btnDo = new Button();
            lbContain = new Label();
            lbEqual = new Label();
            lbLength = new Label();
            lbReplace = new Label();
            lbSplit = new Label();
            lbSubstring = new Label();
            lbToLower = new Label();
            lbToUpper = new Label();
            lbTrim = new Label();
            SuspendLayout();
            // 
            // tbText
            // 
            tbText.Location = new Point(15, 14);
            tbText.Name = "tbText";
            tbText.Size = new Size(414, 23);
            tbText.TabIndex = 0;
            // 
            // btnDo
            // 
            btnDo.Location = new Point(435, 12);
            btnDo.Name = "btnDo";
            btnDo.Size = new Size(75, 23);
            btnDo.TabIndex = 1;
            btnDo.Text = "button1";
            btnDo.UseVisualStyleBackColor = true;
            btnDo.Click += btnDo_Click;
            // 
            // lbContain
            // 
            lbContain.BorderStyle = BorderStyle.FixedSingle;
            lbContain.Location = new Point(16, 47);
            lbContain.Name = "lbContain";
            lbContain.Size = new Size(413, 23);
            lbContain.TabIndex = 2;
            lbContain.Text = "Contain \"abc\": ";
            // 
            // lbEqual
            // 
            lbEqual.BorderStyle = BorderStyle.FixedSingle;
            lbEqual.Location = new Point(16, 83);
            lbEqual.Name = "lbEqual";
            lbEqual.Size = new Size(413, 23);
            lbEqual.TabIndex = 3;
            lbEqual.Text = "Equal \"abc\": ";
            // 
            // lbLength
            // 
            lbLength.BorderStyle = BorderStyle.FixedSingle;
            lbLength.Location = new Point(15, 121);
            lbLength.Name = "lbLength";
            lbLength.Size = new Size(413, 23);
            lbLength.TabIndex = 4;
            lbLength.Text = "Length: ";
            // 
            // lbReplace
            // 
            lbReplace.BorderStyle = BorderStyle.FixedSingle;
            lbReplace.Location = new Point(15, 157);
            lbReplace.Name = "lbReplace";
            lbReplace.Size = new Size(413, 23);
            lbReplace.TabIndex = 5;
            lbReplace.Text = "Replace 'a' to 'b': ";
            // 
            // lbSplit
            // 
            lbSplit.BorderStyle = BorderStyle.FixedSingle;
            lbSplit.Location = new Point(17, 193);
            lbSplit.Name = "lbSplit";
            lbSplit.Size = new Size(413, 80);
            lbSplit.TabIndex = 6;
            lbSplit.Text = "Split(,): ";
            // 
            // lbSubstring
            // 
            lbSubstring.BorderStyle = BorderStyle.FixedSingle;
            lbSubstring.Location = new Point(17, 284);
            lbSubstring.Name = "lbSubstring";
            lbSubstring.Size = new Size(413, 23);
            lbSubstring.TabIndex = 7;
            lbSubstring.Text = "Substring(2, 3): ";
            // 
            // lbToLower
            // 
            lbToLower.BorderStyle = BorderStyle.FixedSingle;
            lbToLower.Location = new Point(16, 322);
            lbToLower.Name = "lbToLower";
            lbToLower.Size = new Size(413, 23);
            lbToLower.TabIndex = 8;
            lbToLower.Text = "ToLower: ";
            // 
            // lbToUpper
            // 
            lbToUpper.BorderStyle = BorderStyle.FixedSingle;
            lbToUpper.Location = new Point(16, 358);
            lbToUpper.Name = "lbToUpper";
            lbToUpper.Size = new Size(413, 23);
            lbToUpper.TabIndex = 9;
            lbToUpper.Text = "ToUpper: ";
            // 
            // lbTrim
            // 
            lbTrim.BorderStyle = BorderStyle.FixedSingle;
            lbTrim.Location = new Point(15, 392);
            lbTrim.Name = "lbTrim";
            lbTrim.Size = new Size(413, 23);
            lbTrim.TabIndex = 10;
            lbTrim.Text = "Trim: ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 465);
            Controls.Add(lbTrim);
            Controls.Add(lbToUpper);
            Controls.Add(lbToLower);
            Controls.Add(lbReplace);
            Controls.Add(lbSubstring);
            Controls.Add(lbLength);
            Controls.Add(lbSplit);
            Controls.Add(lbEqual);
            Controls.Add(lbContain);
            Controls.Add(btnDo);
            Controls.Add(tbText);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbText;
        private Button btnDo;
        private Label lbContain;
        private Label lbEqual;
        private Label lbLength;
        private Label lbReplace;
        private Label lbSplit;
        private Label lbSubstring;
        private Label lbToLower;
        private Label lbToUpper;
        private Label lbTrim;
    }
}
