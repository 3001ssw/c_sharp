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
            btnThreadStart = new Button();
            numThreadCount = new NumericUpDown();
            tbOutput = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numThreadCount).BeginInit();
            SuspendLayout();
            // 
            // btnThreadStart
            // 
            btnThreadStart.Location = new Point(327, 308);
            btnThreadStart.Name = "btnThreadStart";
            btnThreadStart.Size = new Size(75, 23);
            btnThreadStart.TabIndex = 2;
            btnThreadStart.Text = "button1";
            btnThreadStart.UseVisualStyleBackColor = true;
            btnThreadStart.Click += btnThreadStart_Click;
            // 
            // numThreadCount
            // 
            numThreadCount.Location = new Point(201, 308);
            numThreadCount.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numThreadCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numThreadCount.Name = "numThreadCount";
            numThreadCount.Size = new Size(120, 23);
            numThreadCount.TabIndex = 1;
            numThreadCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // tbOutput
            // 
            tbOutput.BorderStyle = BorderStyle.FixedSingle;
            tbOutput.Location = new Point(12, 12);
            tbOutput.Multiline = true;
            tbOutput.Name = "tbOutput";
            tbOutput.ReadOnly = true;
            tbOutput.ScrollBars = ScrollBars.Both;
            tbOutput.Size = new Size(391, 290);
            tbOutput.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 343);
            Controls.Add(tbOutput);
            Controls.Add(numThreadCount);
            Controls.Add(btnThreadStart);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)numThreadCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnThreadStart;
        private NumericUpDown numThreadCount;
        private TextBox tbOutput;
    }
}
