namespace OX.Wallets.Base
{
    partial class MnemonicsWallet
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
            this.bt_next = new OX.Wallets.UI.Controls.DarkButton();
            this.RoundPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bt_ok = new OX.Wallets.UI.Controls.DarkButton();
            this.SuspendLayout();
            // 
            // bt_next
            // 
            this.bt_next.Location = new System.Drawing.Point(401, 217);
            this.bt_next.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.bt_next.Name = "bt_next";
            this.bt_next.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.bt_next.Size = new System.Drawing.Size(234, 32);
            this.bt_next.SpecialBorderColor = null;
            this.bt_next.SpecialFillColor = null;
            this.bt_next.SpecialTextColor = null;
            this.bt_next.TabIndex = 9;
            this.bt_next.Text = "button1";
            this.bt_next.Click += new System.EventHandler(this.bt_next_Click);
            // 
            // RoundPanel
            // 
            this.RoundPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoundPanel.AutoScroll = true;
            this.RoundPanel.Location = new System.Drawing.Point(19, 13);
            this.RoundPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.RoundPanel.Name = "RoundPanel";
            this.RoundPanel.Size = new System.Drawing.Size(901, 175);
            this.RoundPanel.TabIndex = 0;
            // 
            // bt_ok
            // 
            this.bt_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_ok.Location = new System.Drawing.Point(686, 217);
            this.bt_ok.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.bt_ok.Size = new System.Drawing.Size(234, 32);
            this.bt_ok.SpecialBorderColor = null;
            this.bt_ok.SpecialFillColor = null;
            this.bt_ok.SpecialTextColor = null;
            this.bt_ok.TabIndex = 9;
            this.bt_ok.Text = "button1";
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // NmenonicsWallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 275);
            this.Controls.Add(this.bt_ok);
            this.Controls.Add(this.bt_next);
            this.Controls.Add(this.RoundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MnemonicsWallet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.NmenonicsWallet_Load);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.FlowLayoutPanel RoundPanel;
        private OX.Wallets.UI.Controls.DarkButton bt_next;
        private OX.Wallets.UI.Controls.DarkButton bt_ok;
    }
}