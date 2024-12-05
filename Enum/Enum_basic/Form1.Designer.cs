namespace Enum_basic
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
            lbWeek = new ListBox();
            label1 = new Label();
            tbSelect = new TextBox();
            SuspendLayout();
            // 
            // lbWeek
            // 
            lbWeek.FormattingEnabled = true;
            lbWeek.ItemHeight = 15;
            lbWeek.Location = new Point(12, 47);
            lbWeek.Name = "lbWeek";
            lbWeek.Size = new Size(215, 214);
            lbWeek.TabIndex = 0;
            lbWeek.SelectedValueChanged += lbWeek_SelectedValueChanged;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(13, 9);
            label1.Name = "label1";
            label1.Size = new Size(214, 23);
            label1.TabIndex = 1;
            label1.Text = "요일";
            // 
            // tbSelect
            // 
            tbSelect.Enabled = false;
            tbSelect.Location = new Point(13, 267);
            tbSelect.Name = "tbSelect";
            tbSelect.Size = new Size(214, 23);
            tbSelect.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(246, 309);
            Controls.Add(tbSelect);
            Controls.Add(label1);
            Controls.Add(lbWeek);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lbWeek;
        private Label label1;
        private TextBox tbSelect;
    }
}
