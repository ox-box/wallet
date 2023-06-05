namespace OX.Wallets.Base
{
    partial class IssueNFT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IssueNFT));
            panel = new System.Windows.Forms.Panel();
            tb_holdername = new UI.Controls.DarkTextBox();
            lb_holdername = new UI.Controls.DarkLabel();
            tb_sn = new UI.Controls.DarkTextBox();
            lb_sn = new UI.Controls.DarkLabel();
            lb_nfthash_v = new UI.Controls.DarkLabel();
            lb_nfthash = new UI.Controls.DarkLabel();
            tb_newowner = new UI.Controls.DarkTextBox();
            lb_newowner = new UI.Controls.DarkLabel();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(btnOk, "btnOk");
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            // 
            // btnClose
            // 
            resources.ApplyResources(btnClose, "btnClose");
            // 
            // btnYes
            // 
            resources.ApplyResources(btnYes, "btnYes");
            // 
            // btnNo
            // 
            resources.ApplyResources(btnNo, "btnNo");
            // 
            // btnRetry
            // 
            resources.ApplyResources(btnRetry, "btnRetry");
            // 
            // btnIgnore
            // 
            resources.ApplyResources(btnIgnore, "btnIgnore");
            // 
            // panel
            // 
            panel.Controls.Add(tb_holdername);
            panel.Controls.Add(lb_holdername);
            panel.Controls.Add(tb_sn);
            panel.Controls.Add(lb_sn);
            panel.Controls.Add(lb_nfthash_v);
            panel.Controls.Add(lb_nfthash);
            panel.Controls.Add(tb_newowner);
            panel.Controls.Add(lb_newowner);
            resources.ApplyResources(panel, "panel");
            panel.Name = "panel";
            // 
            // tb_holdername
            // 
            tb_holdername.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_holdername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_holdername.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_holdername, "tb_holdername");
            tb_holdername.Name = "tb_holdername";
            // 
            // lb_holdername
            // 
            resources.ApplyResources(lb_holdername, "lb_holdername");
            lb_holdername.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_holdername.Name = "lb_holdername";
            // 
            // tb_sn
            // 
            tb_sn.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_sn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_sn.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_sn, "tb_sn");
            tb_sn.Name = "tb_sn";
            // 
            // lb_sn
            // 
            resources.ApplyResources(lb_sn, "lb_sn");
            lb_sn.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_sn.Name = "lb_sn";
            // 
            // lb_nfthash_v
            // 
            resources.ApplyResources(lb_nfthash_v, "lb_nfthash_v");
            lb_nfthash_v.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_nfthash_v.Name = "lb_nfthash_v";
            // 
            // lb_nfthash
            // 
            resources.ApplyResources(lb_nfthash, "lb_nfthash");
            lb_nfthash.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_nfthash.Name = "lb_nfthash";
            // 
            // tb_newowner
            // 
            tb_newowner.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_newowner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_newowner.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_newowner, "tb_newowner");
            tb_newowner.Name = "tb_newowner";
            // 
            // lb_newowner
            // 
            resources.ApplyResources(lb_newowner, "lb_newowner");
            lb_newowner.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_newowner.Name = "lb_newowner";
            // 
            // IssueNFT
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panel);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IssueNFT";
            FormClosing += ClaimForm_FormClosing;
            Load += NewEvent_Load;
            Controls.SetChildIndex(panel, 0);
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkLabel lb_newowner;
        private UI.Controls.DarkTextBox tb_newowner;
        private UI.Controls.DarkLabel lb_nfthash_v;
        private UI.Controls.DarkLabel lb_nfthash;
        private UI.Controls.DarkLabel lb_sn;
        private UI.Controls.DarkTextBox tb_holdername;
        private UI.Controls.DarkLabel lb_holdername;
        private UI.Controls.DarkTextBox tb_sn;
    }
}