namespace OX.Wallets.Base
{
    partial class ViewBlockDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewBlockDialog));
            this.panel = new System.Windows.Forms.Panel();
            this.lstHistory = new OX.Wallets.UI.Controls.DarkListView();
            this.tb_blockHash = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_blockHash = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_blockNonce = new OX.Wallets.UI.Controls.DarkTextBox();
            this.bt_query = new OX.Wallets.UI.Controls.DarkButton();
            this.tb_blockIndex = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_blockIndex = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_txs = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_blocknonce = new OX.Wallets.UI.Controls.DarkLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            // 
            // btnYes
            // 
            resources.ApplyResources(this.btnYes, "btnYes");
            // 
            // btnNo
            // 
            resources.ApplyResources(this.btnNo, "btnNo");
            // 
            // btnRetry
            // 
            resources.ApplyResources(this.btnRetry, "btnRetry");
            // 
            // btnIgnore
            // 
            resources.ApplyResources(this.btnIgnore, "btnIgnore");
            // 
            // panel
            // 
            this.panel.Controls.Add(this.lstHistory);
            this.panel.Controls.Add(this.tb_blockHash);
            this.panel.Controls.Add(this.lb_blockHash);
            this.panel.Controls.Add(this.tb_blockNonce);
            this.panel.Controls.Add(this.bt_query);
            this.panel.Controls.Add(this.tb_blockIndex);
            this.panel.Controls.Add(this.lb_blockIndex);
            this.panel.Controls.Add(this.lb_txs);
            this.panel.Controls.Add(this.lb_blocknonce);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // lstHistory
            // 
            resources.ApplyResources(this.lstHistory, "lstHistory");
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstHistory_MouseDown);
            // 
            // tb_blockHash
            // 
            this.tb_blockHash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_blockHash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_blockHash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_blockHash, "tb_blockHash");
            this.tb_blockHash.Name = "tb_blockHash";
            this.tb_blockHash.ReadOnly = true;
            // 
            // lb_blockHash
            // 
            resources.ApplyResources(this.lb_blockHash, "lb_blockHash");
            this.lb_blockHash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_blockHash.Name = "lb_blockHash";
            // 
            // tb_blockNonce
            // 
            this.tb_blockNonce.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_blockNonce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_blockNonce.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_blockNonce, "tb_blockNonce");
            this.tb_blockNonce.Name = "tb_blockNonce";
            this.tb_blockNonce.ReadOnly = true;
            // 
            // bt_query
            // 
            resources.ApplyResources(this.bt_query, "bt_query");
            this.bt_query.Name = "bt_query";
            this.bt_query.SpecialBorderColor = null;
            this.bt_query.SpecialFillColor = null;
            this.bt_query.SpecialTextColor = null;
            this.bt_query.Click += new System.EventHandler(this.bt_query_Click);
            // 
            // tb_blockIndex
            // 
            this.tb_blockIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_blockIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_blockIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_blockIndex, "tb_blockIndex");
            this.tb_blockIndex.Name = "tb_blockIndex";
            // 
            // lb_blockIndex
            // 
            resources.ApplyResources(this.lb_blockIndex, "lb_blockIndex");
            this.lb_blockIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_blockIndex.Name = "lb_blockIndex";
            // 
            // lb_txs
            // 
            resources.ApplyResources(this.lb_txs, "lb_txs");
            this.lb_txs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_txs.Name = "lb_txs";
            // 
            // lb_blocknonce
            // 
            resources.ApplyResources(this.lb_blocknonce, "lb_blocknonce");
            this.lb_blocknonce.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_blocknonce.Name = "lb_blocknonce";
            // 
            // ViewBlockDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewBlockDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClaimForm_FormClosing);
            this.Load += new System.EventHandler(this.ClaimForm_Load);
            this.Controls.SetChildIndex(this.panel, 0);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkLabel lb_blocknonce;
        private UI.Controls.DarkLabel lb_txs;
        private UI.Controls.DarkLabel lb_blockIndex;
        private UI.Controls.DarkTextBox tb_blockIndex;
        private UI.Controls.DarkButton bt_query;
        private UI.Controls.DarkTextBox tb_blockNonce;
        private UI.Controls.DarkTextBox tb_blockHash;
        private UI.Controls.DarkLabel lb_blockHash;
        private UI.Controls.DarkListView lstHistory;
    }
}