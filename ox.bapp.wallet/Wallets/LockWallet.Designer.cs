namespace OX.Wallets.Base
{
    partial class LockWallet
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
            this.lblHeader = new OX.Wallets.UI.Controls.DarkLabel();
            this.btOpenWallet = new OX.Wallets.UI.Controls.DarkButton();
            this.lb2 = new OX.Wallets.UI.Controls.DarkLabel();
            this.tbPwd = new OX.Wallets.UI.Controls.DarkTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1018, 113);
            this.lblHeader.TabIndex = 5;
            this.lblHeader.Text = "O X";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btOpenWallet
            // 
            this.btOpenWallet.Location = new System.Drawing.Point(751, 149);
            this.btOpenWallet.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btOpenWallet.Name = "btOpenWallet";
            this.btOpenWallet.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btOpenWallet.Size = new System.Drawing.Size(156, 40);
            this.btOpenWallet.SpecialBorderColor = null;
            this.btOpenWallet.SpecialFillColor = null;
            this.btOpenWallet.SpecialTextColor = null;
            this.btOpenWallet.TabIndex = 9;
            this.btOpenWallet.Text = "button2";
            this.btOpenWallet.Click += new System.EventHandler(this.btOpenWallet_Click);
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.ForeColor = System.Drawing.Color.White;
            this.lb2.Location = new System.Drawing.Point(115, 161);
            this.lb2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(63, 24);
            this.lb2.TabIndex = 7;
            this.lb2.Text = "label2";
            // 
            // tbPwd
            // 
            this.tbPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tbPwd.Location = new System.Drawing.Point(313, 155);
            this.tbPwd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.Size = new System.Drawing.Size(324, 30);
            this.tbPwd.TabIndex = 10;
            this.tbPwd.PasswordChar = '*';
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // LockWallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(1018, 274);
            this.Controls.Add(this.tbPwd);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.btOpenWallet);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "LockWallet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "O X";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OX.Wallets.UI.Controls.DarkLabel lblHeader;
        private OX.Wallets.UI.Controls.DarkButton btOpenWallet;
        private OX.Wallets.UI.Controls.DarkLabel lb2;
        private OX.Wallets.UI.Controls.DarkTextBox tbPwd;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}