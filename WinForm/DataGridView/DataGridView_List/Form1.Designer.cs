namespace DataGridView_List
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
            dataGridView = new DataGridView();
            btnIncreaseAge = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 12);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(505, 298);
            dataGridView.TabIndex = 1;
            dataGridView.SelectionChanged += dataGridView_SelectionChanged;
            // 
            // btnIncreaseAge
            // 
            btnIncreaseAge.Location = new Point(442, 316);
            btnIncreaseAge.Name = "btnIncreaseAge";
            btnIncreaseAge.Size = new Size(75, 23);
            btnIncreaseAge.TabIndex = 2;
            btnIncreaseAge.Text = "button1";
            btnIncreaseAge.UseVisualStyleBackColor = true;
            btnIncreaseAge.Click += btnIncreaseAge_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 358);
            Controls.Add(btnIncreaseAge);
            Controls.Add(dataGridView);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView;
        private Button btnIncreaseAge;
    }
}
