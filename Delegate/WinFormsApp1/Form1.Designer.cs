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
            tbSelectSubForm = new TextBox();
            btnSubForm = new Button();
            SuspendLayout();
            // 
            // tbSelectSubForm
            // 
            tbSelectSubForm.BorderStyle = BorderStyle.FixedSingle;
            tbSelectSubForm.Location = new Point(18, 9);
            tbSelectSubForm.Multiline = true;
            tbSelectSubForm.Name = "tbSelectSubForm";
            tbSelectSubForm.ReadOnly = true;
            tbSelectSubForm.Size = new Size(275, 204);
            tbSelectSubForm.TabIndex = 0;
            // 
            // btnSubForm
            // 
            btnSubForm.Location = new Point(322, 14);
            btnSubForm.Name = "btnSubForm";
            btnSubForm.Size = new Size(75, 23);
            btnSubForm.TabIndex = 1;
            btnSubForm.Text = "서브 폼";
            btnSubForm.UseVisualStyleBackColor = true;
            btnSubForm.Click += btnSubForm_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 219);
            Controls.Add(btnSubForm);
            Controls.Add(tbSelectSubForm);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbSelectSubForm;
        private Button btnSubForm;
    }
}
