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
            lvPublish = new ListView();
            lvSubscribe = new ListView();
            btnPublish = new Button();
            btnSubscribe = new Button();
            groupBox1 = new GroupBox();
            tbPublish = new TextBox();
            groupBox2 = new GroupBox();
            tbSubcribe = new TextBox();
            btnCancelSubscribe = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // lvPublish
            // 
            lvPublish.Location = new Point(409, 22);
            lvPublish.Name = "lvPublish";
            lvPublish.Size = new Size(168, 231);
            lvPublish.TabIndex = 1;
            lvPublish.UseCompatibleStateImageBehavior = false;
            lvPublish.View = View.Details;
            lvPublish.SelectedIndexChanged += lvPublish_SelectedIndexChanged;
            // 
            // lvSubscribe
            // 
            lvSubscribe.Location = new Point(409, 22);
            lvSubscribe.Name = "lvSubscribe";
            lvSubscribe.Size = new Size(178, 233);
            lvSubscribe.TabIndex = 1;
            lvSubscribe.UseCompatibleStateImageBehavior = false;
            lvSubscribe.View = View.Details;
            lvSubscribe.SelectedIndexChanged += lvSubscribe_SelectedIndexChanged;
            // 
            // btnPublish
            // 
            btnPublish.Location = new Point(502, 259);
            btnPublish.Name = "btnPublish";
            btnPublish.Size = new Size(75, 23);
            btnPublish.TabIndex = 2;
            btnPublish.Text = "발행";
            btnPublish.UseVisualStyleBackColor = true;
            btnPublish.Click += btnPublish_Click;
            // 
            // btnSubscribe
            // 
            btnSubscribe.Location = new Point(407, 261);
            btnSubscribe.Name = "btnSubscribe";
            btnSubscribe.Size = new Size(75, 23);
            btnSubscribe.TabIndex = 2;
            btnSubscribe.Text = "구독";
            btnSubscribe.UseVisualStyleBackColor = true;
            btnSubscribe.Click += btnSubscribe_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tbPublish);
            groupBox1.Controls.Add(lvPublish);
            groupBox1.Controls.Add(btnPublish);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(595, 300);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "publish";
            // 
            // tbPublish
            // 
            tbPublish.BorderStyle = BorderStyle.FixedSingle;
            tbPublish.Location = new Point(6, 22);
            tbPublish.Multiline = true;
            tbPublish.Name = "tbPublish";
            tbPublish.ReadOnly = true;
            tbPublish.ScrollBars = ScrollBars.Vertical;
            tbPublish.Size = new Size(386, 231);
            tbPublish.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tbSubcribe);
            groupBox2.Controls.Add(lvSubscribe);
            groupBox2.Controls.Add(btnCancelSubscribe);
            groupBox2.Controls.Add(btnSubscribe);
            groupBox2.Location = new Point(14, 337);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(595, 300);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "subscribe";
            // 
            // tbSubcribe
            // 
            tbSubcribe.BorderStyle = BorderStyle.FixedSingle;
            tbSubcribe.Location = new Point(6, 22);
            tbSubcribe.Multiline = true;
            tbSubcribe.Name = "tbSubcribe";
            tbSubcribe.ReadOnly = true;
            tbSubcribe.ScrollBars = ScrollBars.Vertical;
            tbSubcribe.Size = new Size(386, 231);
            tbSubcribe.TabIndex = 0;
            // 
            // btnCancelSubscribe
            // 
            btnCancelSubscribe.Location = new Point(514, 261);
            btnCancelSubscribe.Name = "btnCancelSubscribe";
            btnCancelSubscribe.Size = new Size(75, 23);
            btnCancelSubscribe.TabIndex = 3;
            btnCancelSubscribe.Text = "구독 취소";
            btnCancelSubscribe.UseVisualStyleBackColor = true;
            btnCancelSubscribe.Click += btnCancelSubscribe_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(619, 650);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListView lvPublish;
        private ListView lvSubscribe;
        private Button btnPublish;
        private Button btnSubscribe;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox tbPublish;
        private TextBox tbSubcribe;
        private Button btnCancelSubscribe;
    }
}
