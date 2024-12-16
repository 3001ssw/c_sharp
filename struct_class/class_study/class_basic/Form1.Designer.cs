namespace class_basic
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
            btnSound = new Button();
            btnMove = new Button();
            btnEat = new Button();
            btnInfo = new Button();
            txtShow = new TextBox();
            listAnimal = new ListBox();
            SuspendLayout();
            // 
            // btnSound
            // 
            btnSound.Location = new Point(171, 12);
            btnSound.Name = "btnSound";
            btnSound.Size = new Size(75, 23);
            btnSound.TabIndex = 1;
            btnSound.Text = "소리";
            btnSound.UseVisualStyleBackColor = true;
            btnSound.Click += btnSound_Click;
            // 
            // btnMove
            // 
            btnMove.Location = new Point(252, 12);
            btnMove.Name = "btnMove";
            btnMove.Size = new Size(75, 23);
            btnMove.TabIndex = 2;
            btnMove.Text = "이동";
            btnMove.UseVisualStyleBackColor = true;
            btnMove.Click += btnMove_Click;
            // 
            // btnEat
            // 
            btnEat.Location = new Point(333, 12);
            btnEat.Name = "btnEat";
            btnEat.Size = new Size(75, 23);
            btnEat.TabIndex = 3;
            btnEat.Text = "먹기";
            btnEat.UseVisualStyleBackColor = true;
            btnEat.Click += btnEat_Click;
            // 
            // btnInfo
            // 
            btnInfo.Location = new Point(414, 12);
            btnInfo.Name = "btnInfo";
            btnInfo.Size = new Size(75, 23);
            btnInfo.TabIndex = 4;
            btnInfo.Text = "정보";
            btnInfo.UseVisualStyleBackColor = true;
            btnInfo.Click += btnInfo_Click;
            // 
            // txtShow
            // 
            txtShow.BackColor = Color.DimGray;
            txtShow.BorderStyle = BorderStyle.None;
            txtShow.ForeColor = SystemColors.Info;
            txtShow.Location = new Point(171, 41);
            txtShow.Multiline = true;
            txtShow.Name = "txtShow";
            txtShow.ReadOnly = true;
            txtShow.ScrollBars = ScrollBars.Vertical;
            txtShow.Size = new Size(318, 208);
            txtShow.TabIndex = 5;
            // 
            // listAnimal
            // 
            listAnimal.FormattingEnabled = true;
            listAnimal.ItemHeight = 15;
            listAnimal.Location = new Point(12, 20);
            listAnimal.Name = "listAnimal";
            listAnimal.Size = new Size(153, 229);
            listAnimal.TabIndex = 0;
            listAnimal.SelectedValueChanged += listBox1_SelectedValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(501, 264);
            Controls.Add(listAnimal);
            Controls.Add(txtShow);
            Controls.Add(btnInfo);
            Controls.Add(btnEat);
            Controls.Add(btnMove);
            Controls.Add(btnSound);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSound;
        private Button btnMove;
        private Button btnEat;
        private Button btnInfo;
        private TextBox txtShow;
        private ListBox listAnimal;
    }
}
