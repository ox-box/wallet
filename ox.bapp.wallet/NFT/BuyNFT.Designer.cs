namespace OX.Wallets.Base
{
    partial class BuyNFT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuyNFT));
            panel = new System.Windows.Forms.Panel();
            lb_amount_v = new UI.Controls.DarkLabel();
            lb_amount = new UI.Controls.DarkLabel();
            cbAccounts = new UI.Controls.DarkComboBox();
            lb_from = new UI.Controls.DarkLabel();
            lb_signature = new UI.Controls.DarkLabel();
            tb_signature = new UI.Controls.DarkTextBox();
            tb_sn = new UI.Controls.DarkTextBox();
            lb_sn = new UI.Controls.DarkLabel();
            tb_fullname = new UI.Controls.DarkTextBox();
            lb_fullname = new UI.Controls.DarkLabel();
            lb_nfthash_v = new UI.Controls.DarkLabel();
            lb_nfthash = new UI.Controls.DarkLabel();
            lb_lockmsg = new UI.Controls.DarkLabel();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
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
            panel.Controls.Add(lb_lockmsg);
            panel.Controls.Add(lb_amount_v);
            panel.Controls.Add(lb_amount);
            panel.Controls.Add(cbAccounts);
            panel.Controls.Add(lb_from);
            panel.Controls.Add(lb_signature);
            panel.Controls.Add(tb_signature);
            panel.Controls.Add(tb_sn);
            panel.Controls.Add(lb_sn);
            panel.Controls.Add(tb_fullname);
            panel.Controls.Add(lb_fullname);
            panel.Controls.Add(lb_nfthash_v);
            panel.Controls.Add(lb_nfthash);
            resources.ApplyResources(panel, "panel");
            panel.Name = "panel";
            panel.Paint += panel_Paint;
            // 
            // lb_amount_v
            // 
            resources.ApplyResources(lb_amount_v, "lb_amount_v");
            lb_amount_v.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_amount_v.Name = "lb_amount_v";
            // 
            // lb_amount
            // 
            resources.ApplyResources(lb_amount, "lb_amount");
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_amount.Name = "lb_amount";
            // 
            // cbAccounts
            // 
            cbAccounts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            resources.ApplyResources(cbAccounts, "cbAccounts");
            cbAccounts.Name = "cbAccounts";
            cbAccounts.SpecialBorderColor = null;
            cbAccounts.SpecialFillColor = null;
            cbAccounts.SpecialTextColor = null;
            // 
            // lb_from
            // 
            resources.ApplyResources(lb_from, "lb_from");
            lb_from.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_from.Name = "lb_from";
            // 
            // lb_signature
            // 
            resources.ApplyResources(lb_signature, "lb_signature");
            lb_signature.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_signature.Name = "lb_signature";
            // 
            // tb_signature
            // 
            tb_signature.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_signature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_signature.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_signature, "tb_signature");
            tb_signature.Name = "tb_signature";
            tb_signature.TextChanged += tb_signature_TextChanged;
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
            // tb_fullname
            // 
            tb_fullname.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_fullname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_fullname.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_fullname, "tb_fullname");
            tb_fullname.Name = "tb_fullname";
            // 
            // lb_fullname
            // 
            resources.ApplyResources(lb_fullname, "lb_fullname");
            lb_fullname.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_fullname.Name = "lb_fullname";
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
            // lb_lockmsg
            // 
            resources.ApplyResources(lb_lockmsg, "lb_lockmsg");
            lb_lockmsg.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_lockmsg.Name = "lb_lockmsg";
            // 
            // BuyNFT
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BuyNFT";
            FormClosing += ClaimForm_FormClosing;
            Load += NewEvent_Load;
            Controls.SetChildIndex(panel, 0);
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkLabel lb_nfthash_v;
        private UI.Controls.DarkLabel lb_nfthash;
        private UI.Controls.DarkLabel lb_fullname;
        private UI.Controls.DarkTextBox tb_sn;
        private UI.Controls.DarkLabel lb_sn;
        private UI.Controls.DarkTextBox tb_fullname;
        private UI.Controls.DarkLabel lb_signature;
        private UI.Controls.DarkTextBox tb_signature;
        private UI.Controls.DarkComboBox cbAccounts;
        private UI.Controls.DarkLabel lb_from;
        private UI.Controls.DarkLabel lb_amount_v;
        private UI.Controls.DarkLabel lb_amount;
        private UI.Controls.DarkLabel lb_lockmsg;
    }
}