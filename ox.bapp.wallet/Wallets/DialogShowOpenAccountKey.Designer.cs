namespace OX.Wallets.Base
{
    partial class DialogShowOpenAccountKey
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
            this.tbHex = new OX.Wallets.UI.Controls.DarkTextBox();
            this.Hex = new OX.Wallets.UI.Controls.DarkLabel();
            this.tbPublickey = new OX.Wallets.UI.Controls.DarkTextBox();
            this.tbAddress = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_publicKey = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_address = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // tbHex
            // 
            this.tbHex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tbHex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tbHex.Location = new System.Drawing.Point(154, 184);
            this.tbHex.Margin = new System.Windows.Forms.Padding(6);
            this.tbHex.Name = "tbHex";
            this.tbHex.ReadOnly = true;
            this.tbHex.Size = new System.Drawing.Size(819, 30);
            this.tbHex.TabIndex = 20;
            // 
            // Hex
            // 
            this.Hex.AutoSize = true;
            this.Hex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Hex.Location = new System.Drawing.Point(28, 188);
            this.Hex.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Hex.Name = "Hex";
            this.Hex.Size = new System.Drawing.Size(47, 24);
            this.Hex.TabIndex = 18;
            this.Hex.Text = "Hex:";
            // 
            // tbPublickey
            // 
            this.tbPublickey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tbPublickey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPublickey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tbPublickey.Location = new System.Drawing.Point(154, 112);
            this.tbPublickey.Margin = new System.Windows.Forms.Padding(6);
            this.tbPublickey.Name = "tbPublickey";
            this.tbPublickey.ReadOnly = true;
            this.tbPublickey.Size = new System.Drawing.Size(819, 30);
            this.tbPublickey.TabIndex = 17;
            // 
            // tbAddress
            // 
            this.tbAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tbAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tbAddress.Location = new System.Drawing.Point(154, 44);
            this.tbAddress.Margin = new System.Windows.Forms.Padding(6);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.ReadOnly = true;
            this.tbAddress.Size = new System.Drawing.Size(819, 30);
            this.tbAddress.TabIndex = 16;
            // 
            // lb_publicKey
            // 
            this.lb_publicKey.AutoSize = true;
            this.lb_publicKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_publicKey.Location = new System.Drawing.Point(28, 119);
            this.lb_publicKey.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_publicKey.Name = "lb_publicKey";
            this.lb_publicKey.Size = new System.Drawing.Size(50, 24);
            this.lb_publicKey.TabIndex = 15;
            this.lb_publicKey.Text = "公钥:";
            // 
            // lb_address
            // 
            this.lb_address.AutoSize = true;
            this.lb_address.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_address.Location = new System.Drawing.Point(28, 48);
            this.lb_address.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_address.Name = "lb_address";
            this.lb_address.Size = new System.Drawing.Size(50, 24);
            this.lb_address.TabIndex = 14;
            this.lb_address.Text = "地址:";
            // 
            // DialogShowOpenAccountKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 327);
            this.Controls.Add(this.tbHex);
            this.Controls.Add(this.Hex);
            this.Controls.Add(this.tbPublickey);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.lb_publicKey);
            this.Controls.Add(this.lb_address);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "DialogShowOpenAccountKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查看私钥";
            this.Controls.SetChildIndex(this.lb_address, 0);
            this.Controls.SetChildIndex(this.lb_publicKey, 0);
            this.Controls.SetChildIndex(this.tbAddress, 0);
            this.Controls.SetChildIndex(this.tbPublickey, 0);
            this.Controls.SetChildIndex(this.Hex, 0);
            this.Controls.SetChildIndex(this.tbHex, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OX.Wallets.UI.Controls.DarkTextBox tbHex;
        private OX.Wallets.UI.Controls.DarkLabel Hex;
        private OX.Wallets.UI.Controls.DarkTextBox tbPublickey;
        private OX.Wallets.UI.Controls.DarkTextBox tbAddress;
        private OX.Wallets.UI.Controls.DarkLabel lb_publicKey;
        private OX.Wallets.UI.Controls.DarkLabel lb_address;
    }
}