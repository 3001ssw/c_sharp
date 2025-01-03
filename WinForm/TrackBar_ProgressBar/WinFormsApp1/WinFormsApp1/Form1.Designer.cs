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
            groupBox1 = new GroupBox();
            numTrackBar = new NumericUpDown();
            trbTest = new TrackBar();
            groupBox2 = new GroupBox();
            btnMarquee = new Button();
            btnContinuous = new Button();
            btnBlocks = new Button();
            numProgressBar = new NumericUpDown();
            pbTest = new ProgressBar();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trbTest).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numProgressBar).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numTrackBar);
            groupBox1.Controls.Add(trbTest);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(486, 71);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "TrackBar";
            // 
            // numTrackBar
            // 
            numTrackBar.Location = new Point(340, 25);
            numTrackBar.Name = "numTrackBar";
            numTrackBar.Size = new Size(120, 23);
            numTrackBar.TabIndex = 1;
            numTrackBar.ValueChanged += numTrackBar_ValueChanged;
            // 
            // trbTest
            // 
            trbTest.Location = new Point(6, 22);
            trbTest.Name = "trbTest";
            trbTest.Size = new Size(287, 45);
            trbTest.TabIndex = 0;
            trbTest.ValueChanged += trbTest_ValueChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnMarquee);
            groupBox2.Controls.Add(btnContinuous);
            groupBox2.Controls.Add(btnBlocks);
            groupBox2.Controls.Add(numProgressBar);
            groupBox2.Controls.Add(pbTest);
            groupBox2.Location = new Point(12, 100);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(486, 111);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "ProgressBar";
            // 
            // btnMarquee
            // 
            btnMarquee.Location = new Point(176, 72);
            btnMarquee.Name = "btnMarquee";
            btnMarquee.Size = new Size(75, 23);
            btnMarquee.TabIndex = 2;
            btnMarquee.Text = "Marquee";
            btnMarquee.UseVisualStyleBackColor = true;
            btnMarquee.Click += btnMarquee_Click;
            // 
            // btnContinuous
            // 
            btnContinuous.Location = new Point(95, 72);
            btnContinuous.Name = "btnContinuous";
            btnContinuous.Size = new Size(75, 23);
            btnContinuous.TabIndex = 2;
            btnContinuous.Text = "Continuous";
            btnContinuous.UseVisualStyleBackColor = true;
            btnContinuous.Click += btnContinuous_Click;
            // 
            // btnBlocks
            // 
            btnBlocks.Location = new Point(14, 72);
            btnBlocks.Name = "btnBlocks";
            btnBlocks.Size = new Size(75, 23);
            btnBlocks.TabIndex = 2;
            btnBlocks.Text = "Blocks";
            btnBlocks.UseVisualStyleBackColor = true;
            btnBlocks.Click += btnBlocks_Click;
            // 
            // numProgressBar
            // 
            numProgressBar.Location = new Point(340, 26);
            numProgressBar.Name = "numProgressBar";
            numProgressBar.Size = new Size(120, 23);
            numProgressBar.TabIndex = 1;
            numProgressBar.ValueChanged += numProgressBar_ValueChanged;
            // 
            // pbTest
            // 
            pbTest.Location = new Point(14, 26);
            pbTest.Name = "pbTest";
            pbTest.Size = new Size(279, 23);
            pbTest.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 223);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)trbTest).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numProgressBar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TrackBar trbTest;
        private NumericUpDown numTrackBar;
        private NumericUpDown numProgressBar;
        private ProgressBar pbTest;
        private Button btnMarquee;
        private Button btnContinuous;
        private Button btnBlocks;
    }
}
