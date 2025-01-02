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
            cbFruit = new ComboBox();
            lbComboBox = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            rdSelect4 = new RadioButton();
            rdSelect3 = new RadioButton();
            rdSelect2 = new RadioButton();
            rdSelect1 = new RadioButton();
            lbRadio = new Label();
            groupBox3 = new GroupBox();
            numYear = new NumericUpDown();
            lbNumeric = new Label();
            numMonth = new NumericUpDown();
            numDay = new NumericUpDown();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDay).BeginInit();
            SuspendLayout();
            // 
            // cbFruit
            // 
            cbFruit.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFruit.FormattingEnabled = true;
            cbFruit.Location = new Point(392, 24);
            cbFruit.Name = "cbFruit";
            cbFruit.Size = new Size(163, 23);
            cbFruit.TabIndex = 1;
            cbFruit.SelectedIndexChanged += cbFruit_SelectedIndexChanged;
            // 
            // lbComboBox
            // 
            lbComboBox.BorderStyle = BorderStyle.FixedSingle;
            lbComboBox.Location = new Point(24, 24);
            lbComboBox.Name = "lbComboBox";
            lbComboBox.Size = new Size(362, 23);
            lbComboBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lbComboBox);
            groupBox1.Controls.Add(cbFruit);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(574, 62);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "ComboBox";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rdSelect4);
            groupBox2.Controls.Add(rdSelect3);
            groupBox2.Controls.Add(rdSelect2);
            groupBox2.Controls.Add(rdSelect1);
            groupBox2.Controls.Add(lbRadio);
            groupBox2.Location = new Point(12, 104);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(574, 62);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "RadioButton";
            // 
            // rdSelect4
            // 
            rdSelect4.AutoSize = true;
            rdSelect4.Location = new Point(468, 25);
            rdSelect4.Name = "rdSelect4";
            rdSelect4.Size = new Size(56, 19);
            rdSelect4.TabIndex = 4;
            rdSelect4.Text = "선택4";
            rdSelect4.UseVisualStyleBackColor = true;
            rdSelect4.CheckedChanged += rdSelect_CheckedChanged;
            // 
            // rdSelect3
            // 
            rdSelect3.AutoSize = true;
            rdSelect3.Location = new Point(392, 24);
            rdSelect3.Name = "rdSelect3";
            rdSelect3.Size = new Size(56, 19);
            rdSelect3.TabIndex = 3;
            rdSelect3.Text = "선택3";
            rdSelect3.UseVisualStyleBackColor = true;
            rdSelect3.CheckedChanged += rdSelect_CheckedChanged;
            // 
            // rdSelect2
            // 
            rdSelect2.AutoSize = true;
            rdSelect2.Location = new Point(313, 25);
            rdSelect2.Name = "rdSelect2";
            rdSelect2.Size = new Size(56, 19);
            rdSelect2.TabIndex = 2;
            rdSelect2.Text = "선택2";
            rdSelect2.UseVisualStyleBackColor = true;
            rdSelect2.CheckedChanged += rdSelect_CheckedChanged;
            // 
            // rdSelect1
            // 
            rdSelect1.AutoSize = true;
            rdSelect1.Location = new Point(240, 25);
            rdSelect1.Name = "rdSelect1";
            rdSelect1.Size = new Size(56, 19);
            rdSelect1.TabIndex = 1;
            rdSelect1.Text = "선택1";
            rdSelect1.UseVisualStyleBackColor = true;
            rdSelect1.CheckedChanged += rdSelect_CheckedChanged;
            // 
            // lbRadio
            // 
            lbRadio.BorderStyle = BorderStyle.FixedSingle;
            lbRadio.Location = new Point(24, 24);
            lbRadio.Name = "lbRadio";
            lbRadio.Size = new Size(195, 23);
            lbRadio.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(numDay);
            groupBox3.Controls.Add(numMonth);
            groupBox3.Controls.Add(numYear);
            groupBox3.Controls.Add(lbNumeric);
            groupBox3.Location = new Point(12, 199);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(574, 62);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "NumericUpDown";
            // 
            // numYear
            // 
            numYear.Location = new Point(225, 24);
            numYear.Name = "numYear";
            numYear.Size = new Size(97, 23);
            numYear.TabIndex = 1;
            numYear.ValueChanged += numYearMonthDay_ValueChanged;
            // 
            // lbNumeric
            // 
            lbNumeric.BorderStyle = BorderStyle.FixedSingle;
            lbNumeric.Location = new Point(24, 24);
            lbNumeric.Name = "lbNumeric";
            lbNumeric.Size = new Size(195, 23);
            lbNumeric.TabIndex = 0;
            // 
            // numMonth
            // 
            numMonth.Location = new Point(324, 24);
            numMonth.Name = "numMonth";
            numMonth.Size = new Size(97, 23);
            numMonth.TabIndex = 2;
            numMonth.ValueChanged += numYearMonthDay_ValueChanged;
            // 
            // numDay
            // 
            numDay.Location = new Point(427, 24);
            numDay.Name = "numDay";
            numDay.Size = new Size(97, 23);
            numDay.TabIndex = 3;
            numDay.ValueChanged += numYearMonthDay_ValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(598, 279);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDay).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbFruit;
        private Label lbComboBox;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioButton rdSelect1;
        private Label lbRadio;
        private RadioButton rdSelect4;
        private RadioButton rdSelect3;
        private RadioButton rdSelect2;
        private GroupBox groupBox3;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Label lbNumeric;
        private NumericUpDown numYear;
        private NumericUpDown numDay;
        private NumericUpDown numMonth;
    }
}
