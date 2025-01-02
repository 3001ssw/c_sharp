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
            components = new System.ComponentModel.Container();
            tmTest = new System.Windows.Forms.Timer(components);
            lbDisp = new Label();
            btnStart = new Button();
            btnStop = new Button();
            numInterval = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numInterval).BeginInit();
            SuspendLayout();
            // 
            // tmTest
            // 
            tmTest.Interval = 1000;
            tmTest.Tick += tmTest_Tick;
            // 
            // lbDisp
            // 
            lbDisp.BackColor = Color.DimGray;
            lbDisp.BorderStyle = BorderStyle.FixedSingle;
            lbDisp.ForeColor = Color.Transparent;
            lbDisp.Location = new Point(14, 13);
            lbDisp.Name = "lbDisp";
            lbDisp.Size = new Size(240, 60);
            lbDisp.TabIndex = 0;
            lbDisp.Text = "Display Time";
            lbDisp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(14, 134);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(111, 23);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(143, 134);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(111, 23);
            btnStop.TabIndex = 3;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // numInterval
            // 
            numInterval.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numInterval.Location = new Point(13, 89);
            numInterval.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numInterval.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numInterval.Name = "numInterval";
            numInterval.Size = new Size(241, 23);
            numInterval.TabIndex = 1;
            numInterval.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(275, 174);
            Controls.Add(numInterval);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(lbDisp);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numInterval).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer tmTest;
        private Label lbDisp;
        private Button btnStart;
        private Button btnStop;
        private NumericUpDown numInterval;
    }
}
