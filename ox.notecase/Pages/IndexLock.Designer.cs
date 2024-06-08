namespace OX.Notecase.Pages
{
    partial class IndexLock
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
            lblHeader = new Wallets.UI.Controls.DarkLabel();
            lb2 = new Wallets.UI.Controls.DarkLabel();
            btOpenWallet = new Wallets.UI.Controls.DarkButton();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblHeader.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lblHeader.Location = new System.Drawing.Point(0, 0);
            lblHeader.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(1018, 113);
            lblHeader.TabIndex = 5;
            lblHeader.Text = "O X";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb2
            // 
            lb2.AutoSize = true;
            lb2.ForeColor = System.Drawing.Color.White;
            lb2.Location = new System.Drawing.Point(351, 168);
            lb2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb2.Name = "lb2";
            lb2.Size = new System.Drawing.Size(63, 24);
            lb2.TabIndex = 7;
            lb2.Text = "label2";
            // 
            // btOpenWallet
            // 
            btOpenWallet.Location = new System.Drawing.Point(915, 221);
            btOpenWallet.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            btOpenWallet.Name = "btOpenWallet";
            btOpenWallet.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            btOpenWallet.Size = new System.Drawing.Size(78, 40);
            btOpenWallet.SpecialBorderColor = null;
            btOpenWallet.SpecialFillColor = null;
            btOpenWallet.SpecialTextColor = null;
            btOpenWallet.TabIndex = 9;
            btOpenWallet.Text = "button2";
            btOpenWallet.Click += btOpenWallet_Click;
            // 
            // IndexLock
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            ClientSize = new System.Drawing.Size(1018, 274);
            Controls.Add(lb2);
            Controls.Add(btOpenWallet);
            Controls.Add(lblHeader);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Name = "IndexLock";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "O X";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OX.Wallets.UI.Controls.DarkLabel lblHeader;
        private OX.Wallets.UI.Controls.DarkLabel lb2;
        private OX.Wallets.UI.Controls.DarkButton btOpenWallet;
    }
}