namespace OX.Wallets.Base
{
    partial class DialogImportWatchAccount
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
            this.tbAddress = new OX.Wallets.UI.Controls.DarkTextBox();
            this.darkLabel1 = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // tbAddress
            // 
            this.tbAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tbAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tbAddress.Location = new System.Drawing.Point(67, 35);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(372, 21);
            this.tbAddress.TabIndex = 12;
            this.tbAddress.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // darkLabel1
            // 
            this.darkLabel1.AutoSize = true;
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(26, 38);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(35, 12);
            this.darkLabel1.TabIndex = 11;
            this.darkLabel1.Text = UIHelper.LocalString("地址:", "Address:");
            // 
            // DialogImportWatchAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 127);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.darkLabel1);
            this.Name = "DialogImportWatchAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = UIHelper.LocalString("添加监视地址", "Add Watch Address");
            this.Controls.SetChildIndex(this.darkLabel1, 0);
            this.Controls.SetChildIndex(this.tbAddress, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OX.Wallets.UI.Controls.DarkTextBox tbAddress;
        private OX.Wallets.UI.Controls.DarkLabel darkLabel1;
    }
}