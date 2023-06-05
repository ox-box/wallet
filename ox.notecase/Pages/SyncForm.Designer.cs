namespace OX.Notecase.Pages
{
    partial class SyncForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncForm));
            lbHeight = new Wallets.UI.Controls.DarkLabel();
            lblHeader = new Wallets.UI.Controls.DarkLabel();
            lb1 = new Wallets.UI.Controls.DarkLabel();
            tbWalletPath = new Wallets.UI.Controls.DarkTextBox();
            btSelectWallet = new Wallets.UI.Controls.DarkButton();
            btOpenWallet = new Wallets.UI.Controls.DarkButton();
            lb2 = new Wallets.UI.Controls.DarkLabel();
            tbPwd = new Wallets.UI.Controls.DarkTextBox();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            SuspendLayout();
            // 
            // lbHeight
            // 
            lbHeight.Dock = System.Windows.Forms.DockStyle.Top;
            lbHeight.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lbHeight.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lbHeight.Location = new System.Drawing.Point(0, 106);
            lbHeight.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lbHeight.Name = "lbHeight";
            lbHeight.Size = new System.Drawing.Size(1018, 103);
            lbHeight.TabIndex = 6;
            lbHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbHeight.MouseDown += SyncForm_MouseDown;
            // 
            // lblHeader
            // 
            lblHeader.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblHeader.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lblHeader.Image = (System.Drawing.Image)resources.GetObject("lblHeader.Image");
            lblHeader.Location = new System.Drawing.Point(0, 0);
            lblHeader.Margin = new System.Windows.Forms.Padding(5, 20, 5, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            lblHeader.Size = new System.Drawing.Size(1018, 106);
            lblHeader.TabIndex = 5;
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblHeader.MouseDown += SyncForm_MouseDown;
            // 
            // lb1
            // 
            lb1.AutoSize = true;
            lb1.ForeColor = System.Drawing.Color.White;
            lb1.Location = new System.Drawing.Point(20, 250);
            lb1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb1.Name = "lb1";
            lb1.Size = new System.Drawing.Size(63, 24);
            lb1.TabIndex = 7;
            lb1.Text = "label1";
            lb1.Visible = false;
            // 
            // tbWalletPath
            // 
            tbWalletPath.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            tbWalletPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tbWalletPath.ForeColor = System.Drawing.Color.White;
            tbWalletPath.Location = new System.Drawing.Point(190, 247);
            tbWalletPath.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            tbWalletPath.Name = "tbWalletPath";
            tbWalletPath.ReadOnly = true;
            tbWalletPath.Size = new System.Drawing.Size(605, 30);
            tbWalletPath.TabIndex = 8;
            tbWalletPath.Visible = false;
            // 
            // btSelectWallet
            // 
            btSelectWallet.Location = new System.Drawing.Point(828, 241);
            btSelectWallet.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            btSelectWallet.Name = "btSelectWallet";
            btSelectWallet.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            btSelectWallet.Size = new System.Drawing.Size(156, 40);
            btSelectWallet.SpecialBorderColor = null;
            btSelectWallet.SpecialFillColor = null;
            btSelectWallet.SpecialTextColor = null;
            btSelectWallet.TabIndex = 9;
            btSelectWallet.Text = "button1";
            btSelectWallet.Visible = false;
            btSelectWallet.Click += btSelectWallet_Click;
            // 
            // btOpenWallet
            // 
            btOpenWallet.Location = new System.Drawing.Point(828, 299);
            btOpenWallet.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            btOpenWallet.Name = "btOpenWallet";
            btOpenWallet.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            btOpenWallet.Size = new System.Drawing.Size(156, 40);
            btOpenWallet.SpecialBorderColor = null;
            btOpenWallet.SpecialFillColor = null;
            btOpenWallet.SpecialTextColor = null;
            btOpenWallet.TabIndex = 9;
            btOpenWallet.Text = "button2";
            btOpenWallet.Visible = false;
            btOpenWallet.Click += btOpenWallet_Click;
            // 
            // lb2
            // 
            lb2.AutoSize = true;
            lb2.ForeColor = System.Drawing.Color.White;
            lb2.Location = new System.Drawing.Point(20, 308);
            lb2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb2.Name = "lb2";
            lb2.Size = new System.Drawing.Size(63, 24);
            lb2.TabIndex = 7;
            lb2.Text = "label2";
            lb2.Visible = false;
            // 
            // tbPwd
            // 
            tbPwd.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            tbPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tbPwd.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tbPwd.Location = new System.Drawing.Point(190, 304);
            tbPwd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            tbPwd.Name = "tbPwd";
            tbPwd.Size = new System.Drawing.Size(605, 30);
            tbPwd.TabIndex = 10;
            tbPwd.Visible = false;
            // 
            // SyncForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            ClientSize = new System.Drawing.Size(1018, 380);
            Controls.Add(tbPwd);
            Controls.Add(lb2);
            Controls.Add(btOpenWallet);
            Controls.Add(btSelectWallet);
            Controls.Add(tbWalletPath);
            Controls.Add(lb1);
            Controls.Add(lbHeight);
            Controls.Add(lblHeader);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Name = "SyncForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "O X";
            Load += SyncForm_Load;
            MouseDown += SyncForm_MouseDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel lbHeight;
        private OX.Wallets.UI.Controls.DarkLabel lblHeader;
        private OX.Wallets.UI.Controls.DarkLabel lb1;
        private OX.Wallets.UI.Controls.DarkTextBox tbWalletPath;
        private OX.Wallets.UI.Controls.DarkButton btSelectWallet;
        private OX.Wallets.UI.Controls.DarkButton btOpenWallet;
        private OX.Wallets.UI.Controls.DarkLabel lb2;
        private OX.Wallets.UI.Controls.DarkTextBox tbPwd;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}