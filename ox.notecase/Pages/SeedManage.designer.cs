namespace OX.Notecase
{
    partial class SeedManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.button1 = new OX.Wallets.UI.Controls.DarkButton();
            this.button2 = new OX.Wallets.UI.Controls.DarkButton();
            this.button3 = new OX.Wallets.UI.Controls.DarkButton();
            this.listBox1 = new OX.Wallets.UI.Controls.DarkListView();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBox1.Location = new System.Drawing.Point(13, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(239, 30);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(274, 28);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(5);
            this.button1.Size = new System.Drawing.Size(92, 30);
            this.button1.SpecialBorderColor = null;
            this.button1.SpecialFillColor = null;
            this.button1.SpecialTextColor = null;
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(385, 28);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(5);
            this.button2.Size = new System.Drawing.Size(108, 30);
            this.button2.SpecialBorderColor = null;
            this.button2.SpecialFillColor = null;
            this.button2.SpecialTextColor = null;
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(385, 429);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(5);
            this.button3.Size = new System.Drawing.Size(95, 35);
            this.button3.SpecialBorderColor = null;
            this.button3.SpecialFillColor = null;
            this.button3.SpecialTextColor = null;
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Location = new System.Drawing.Point(13, 82);
            this.listBox1.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.listBox1.MultiSelect = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(493, 321);
            this.listBox1.TabIndex = 7;
            this.listBox1.Text = "darkListView1";
            // 
            // SeedManage
            // 
            this.ClientSize = new System.Drawing.Size(522, 476);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Name = "SeedManage";
            this.Load += new System.EventHandler(this.SeedManage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private OX.Wallets.UI.Controls.DarkTextBox textBox1;
        private OX.Wallets.UI.Controls.DarkButton button1;
        private OX.Wallets.UI.Controls.DarkButton button2;
        private OX.Wallets.UI.Controls.DarkButton button3;
        private OX.Wallets.UI.Controls.DarkListView listBox1;
    }
}