namespace OX.Notecase
{
    partial class CreateWallet
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
            this.lb_file = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkTextBox1 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.darkButton1 = new OX.Wallets.UI.Controls.DarkButton();
            this.lb_pwd = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkTextBox2 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_repwd = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkTextBox3 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.bt_browser = new OX.Wallets.UI.Controls.DarkButton();
            this.lb_actNum = new OX.Wallets.UI.Controls.DarkLabel();
            this.rb_7 = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.rb_20 = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.rb_100 = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.SuspendLayout();
            // 
            // lb_file
            // 
            this.lb_file.AutoSize = true;
            this.lb_file.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_file.Location = new System.Drawing.Point(35, 37);
            this.lb_file.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lb_file.Name = "lb_file";
            this.lb_file.Size = new System.Drawing.Size(104, 24);
            this.lb_file.TabIndex = 0;
            this.lb_file.Text = "Wallet File:";
            // 
            // darkTextBox1
            // 
            this.darkTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.darkTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkTextBox1.Location = new System.Drawing.Point(194, 32);
            this.darkTextBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.darkTextBox1.Name = "darkTextBox1";
            this.darkTextBox1.ReadOnly = true;
            this.darkTextBox1.Size = new System.Drawing.Size(533, 30);
            this.darkTextBox1.TabIndex = 1;
            this.darkTextBox1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // darkButton1
            // 
            this.darkButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.darkButton1.Location = new System.Drawing.Point(1077, 31);
            this.darkButton1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.darkButton1.Name = "darkButton1";
            this.darkButton1.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.darkButton1.Size = new System.Drawing.Size(118, 32);
            this.darkButton1.SpecialBorderColor = null;
            this.darkButton1.SpecialFillColor = null;
            this.darkButton1.SpecialTextColor = null;
            this.darkButton1.TabIndex = 2;
            this.darkButton1.Text = "browse";
            // 
            // lb_pwd
            // 
            this.lb_pwd.AutoSize = true;
            this.lb_pwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_pwd.Location = new System.Drawing.Point(39, 86);
            this.lb_pwd.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lb_pwd.Name = "lb_pwd";
            this.lb_pwd.Size = new System.Drawing.Size(95, 24);
            this.lb_pwd.TabIndex = 5;
            this.lb_pwd.Text = "Password:";
            // 
            // darkTextBox2
            // 
            this.darkTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.darkTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkTextBox2.Location = new System.Drawing.Point(194, 82);
            this.darkTextBox2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.darkTextBox2.Name = "darkTextBox2";
            this.darkTextBox2.Size = new System.Drawing.Size(244, 30);
            this.darkTextBox2.TabIndex = 6;
            this.darkTextBox2.UseSystemPasswordChar = true;
            this.darkTextBox2.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // lb_repwd
            // 
            this.lb_repwd.AutoSize = true;
            this.lb_repwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_repwd.Location = new System.Drawing.Point(8, 133);
            this.lb_repwd.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lb_repwd.Name = "lb_repwd";
            this.lb_repwd.Size = new System.Drawing.Size(125, 24);
            this.lb_repwd.TabIndex = 7;
            this.lb_repwd.Text = "Re-Password:";
            // 
            // darkTextBox3
            // 
            this.darkTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.darkTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkTextBox3.Location = new System.Drawing.Point(194, 128);
            this.darkTextBox3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.darkTextBox3.Name = "darkTextBox3";
            this.darkTextBox3.Size = new System.Drawing.Size(244, 30);
            this.darkTextBox3.TabIndex = 8;
            this.darkTextBox3.UseSystemPasswordChar = true;
            this.darkTextBox3.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "json";
            this.saveFileDialog1.Filter = "Wallet File|*.json";
            // 
            // bt_browser
            // 
            this.bt_browser.Location = new System.Drawing.Point(782, 30);
            this.bt_browser.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.bt_browser.Name = "bt_browser";
            this.bt_browser.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.bt_browser.Size = new System.Drawing.Size(118, 32);
            this.bt_browser.SpecialBorderColor = null;
            this.bt_browser.SpecialFillColor = null;
            this.bt_browser.SpecialTextColor = null;
            this.bt_browser.TabIndex = 2;
            this.bt_browser.Text = "browse";
            this.bt_browser.Click += new System.EventHandler(this.bt_browser_Click);
            // 
            // lb_actNum
            // 
            this.lb_actNum.AutoSize = true;
            this.lb_actNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_actNum.Location = new System.Drawing.Point(8, 180);
            this.lb_actNum.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lb_actNum.Name = "lb_actNum";
            this.lb_actNum.Size = new System.Drawing.Size(125, 24);
            this.lb_actNum.TabIndex = 9;
            this.lb_actNum.Text = "Re-Password:";
            // 
            // rb_7
            // 
            this.rb_7.AutoSize = true;
            this.rb_7.Checked = true;
            this.rb_7.Location = new System.Drawing.Point(194, 178);
            this.rb_7.Name = "rb_7";
            this.rb_7.Size = new System.Drawing.Size(46, 28);
            this.rb_7.SpecialBorderColor = null;
            this.rb_7.SpecialFillColor = null;
            this.rb_7.SpecialTextColor = null;
            this.rb_7.TabIndex = 10;
            this.rb_7.TabStop = true;
            this.rb_7.Text = "7";
            // 
            // rb_20
            // 
            this.rb_20.AutoSize = true;
            this.rb_20.Location = new System.Drawing.Point(277, 178);
            this.rb_20.Name = "rb_20";
            this.rb_20.Size = new System.Drawing.Size(57, 28);
            this.rb_20.SpecialBorderColor = null;
            this.rb_20.SpecialFillColor = null;
            this.rb_20.SpecialTextColor = null;
            this.rb_20.TabIndex = 11;
            this.rb_20.Text = "20";
            // 
            // rb_100
            // 
            this.rb_100.AutoSize = true;
            this.rb_100.Location = new System.Drawing.Point(370, 178);
            this.rb_100.Name = "rb_100";
            this.rb_100.Size = new System.Drawing.Size(68, 28);
            this.rb_100.SpecialBorderColor = null;
            this.rb_100.SpecialFillColor = null;
            this.rb_100.SpecialTextColor = null;
            this.rb_100.TabIndex = 12;
            this.rb_100.Text = "100";
            // 
            // CreateWallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 302);
            this.Controls.Add(this.rb_100);
            this.Controls.Add(this.rb_20);
            this.Controls.Add(this.rb_7);
            this.Controls.Add(this.lb_actNum);
            this.Controls.Add(this.bt_browser);
            this.Controls.Add(this.darkTextBox3);
            this.Controls.Add(this.lb_repwd);
            this.Controls.Add(this.darkTextBox2);
            this.Controls.Add(this.lb_pwd);
            this.Controls.Add(this.darkButton1);
            this.Controls.Add(this.darkTextBox1);
            this.Controls.Add(this.lb_file);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateWallet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.SetChildIndex(this.lb_file, 0);
            this.Controls.SetChildIndex(this.darkTextBox1, 0);
            this.Controls.SetChildIndex(this.darkButton1, 0);
            this.Controls.SetChildIndex(this.lb_pwd, 0);
            this.Controls.SetChildIndex(this.darkTextBox2, 0);
            this.Controls.SetChildIndex(this.lb_repwd, 0);
            this.Controls.SetChildIndex(this.darkTextBox3, 0);
            this.Controls.SetChildIndex(this.bt_browser, 0);
            this.Controls.SetChildIndex(this.lb_actNum, 0);
            this.Controls.SetChildIndex(this.rb_7, 0);
            this.Controls.SetChildIndex(this.rb_20, 0);
            this.Controls.SetChildIndex(this.rb_100, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel lb_file;
        private OX.Wallets.UI.Controls.DarkTextBox darkTextBox1;
        private OX.Wallets.UI.Controls.DarkButton darkButton1;
        private OX.Wallets.UI.Controls.DarkLabel lb_pwd;
        private OX.Wallets.UI.Controls.DarkTextBox darkTextBox2;
        private OX.Wallets.UI.Controls.DarkLabel lb_repwd;
        private OX.Wallets.UI.Controls.DarkTextBox darkTextBox3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private OX.Wallets.UI.Controls.DarkButton bt_browser;
        private Wallets.UI.Controls.DarkLabel lb_actNum;
        private Wallets.UI.Controls.DarkRadioButton rb_7;
        private Wallets.UI.Controls.DarkRadioButton rb_20;
        private Wallets.UI.Controls.DarkRadioButton rb_100;
    }
}