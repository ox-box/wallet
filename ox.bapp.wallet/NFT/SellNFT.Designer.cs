namespace OX.Wallets.Base
{
    partial class SellNFT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SellNFT));
            this.panel = new System.Windows.Forms.Panel();
            this.darkLabel3 = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_minIndex = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_minIndex = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_maxIndex = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_maxIndex = new OX.Wallets.UI.Controls.DarkLabel();
            this.bt_build = new OX.Wallets.UI.Controls.DarkButton();
            this.tb_copy = new OX.Wallets.UI.Controls.DarkButton();
            this.lb_signature = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_signature = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_nfthash_v = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_nfthash = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_amount = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_amount = new OX.Wallets.UI.Controls.DarkLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
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
            this.panel.Controls.Add(this.darkLabel3);
            this.panel.Controls.Add(this.tb_minIndex);
            this.panel.Controls.Add(this.lb_minIndex);
            this.panel.Controls.Add(this.tb_maxIndex);
            this.panel.Controls.Add(this.lb_maxIndex);
            this.panel.Controls.Add(this.bt_build);
            this.panel.Controls.Add(this.tb_copy);
            this.panel.Controls.Add(this.lb_signature);
            this.panel.Controls.Add(this.tb_signature);
            this.panel.Controls.Add(this.lb_nfthash_v);
            this.panel.Controls.Add(this.lb_nfthash);
            this.panel.Controls.Add(this.tb_amount);
            this.panel.Controls.Add(this.lb_amount);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // darkLabel3
            // 
            resources.ApplyResources(this.darkLabel3, "darkLabel3");
            this.darkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel3.Name = "darkLabel3";
            // 
            // tb_minIndex
            // 
            this.tb_minIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_minIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_minIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_minIndex, "tb_minIndex");
            this.tb_minIndex.Name = "tb_minIndex";
            this.tb_minIndex.TextChanged += new System.EventHandler(this.tb_amount_TextChanged);
            // 
            // lb_minIndex
            // 
            resources.ApplyResources(this.lb_minIndex, "lb_minIndex");
            this.lb_minIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_minIndex.Name = "lb_minIndex";
            // 
            // tb_maxIndex
            // 
            this.tb_maxIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_maxIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_maxIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_maxIndex, "tb_maxIndex");
            this.tb_maxIndex.Name = "tb_maxIndex";
            this.tb_maxIndex.TextChanged += new System.EventHandler(this.tb_amount_TextChanged);
            // 
            // lb_maxIndex
            // 
            resources.ApplyResources(this.lb_maxIndex, "lb_maxIndex");
            this.lb_maxIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_maxIndex.Name = "lb_maxIndex";
            // 
            // bt_build
            // 
            resources.ApplyResources(this.bt_build, "bt_build");
            this.bt_build.Name = "bt_build";
            this.bt_build.SpecialBorderColor = null;
            this.bt_build.SpecialFillColor = null;
            this.bt_build.SpecialTextColor = null;
            this.bt_build.Click += new System.EventHandler(this.bt_build_Click);
            // 
            // tb_copy
            // 
            resources.ApplyResources(this.tb_copy, "tb_copy");
            this.tb_copy.Name = "tb_copy";
            this.tb_copy.SpecialBorderColor = null;
            this.tb_copy.SpecialFillColor = null;
            this.tb_copy.SpecialTextColor = null;
            this.tb_copy.Click += new System.EventHandler(this.tb_copy_Click);
            // 
            // lb_signature
            // 
            resources.ApplyResources(this.lb_signature, "lb_signature");
            this.lb_signature.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_signature.Name = "lb_signature";
            // 
            // tb_signature
            // 
            this.tb_signature.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_signature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_signature.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_signature, "tb_signature");
            this.tb_signature.Name = "tb_signature";
            this.tb_signature.ReadOnly = true;
            // 
            // lb_nfthash_v
            // 
            resources.ApplyResources(this.lb_nfthash_v, "lb_nfthash_v");
            this.lb_nfthash_v.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_nfthash_v.Name = "lb_nfthash_v";
            // 
            // lb_nfthash
            // 
            resources.ApplyResources(this.lb_nfthash, "lb_nfthash");
            this.lb_nfthash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_nfthash.Name = "lb_nfthash";
            // 
            // tb_amount
            // 
            this.tb_amount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_amount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_amount, "tb_amount");
            this.tb_amount.Name = "tb_amount";
            this.tb_amount.TextChanged += new System.EventHandler(this.tb_amount_TextChanged);
            // 
            // lb_amount
            // 
            resources.ApplyResources(this.lb_amount, "lb_amount");
            this.lb_amount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_amount.Name = "lb_amount";
            // 
            // SellNFT
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SellNFT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClaimForm_FormClosing);
            this.Load += new System.EventHandler(this.NewEvent_Load);
            this.Controls.SetChildIndex(this.panel, 0);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkLabel lb_amount;
        private UI.Controls.DarkTextBox tb_amount;
        private UI.Controls.DarkLabel lb_nfthash_v;
        private UI.Controls.DarkLabel lb_nfthash;
        private UI.Controls.DarkLabel lb_signature;
        private UI.Controls.DarkTextBox tb_signature;
        private UI.Controls.DarkButton tb_copy;
        private UI.Controls.DarkButton bt_build;
        private UI.Controls.DarkTextBox tb_minIndex;
        private UI.Controls.DarkLabel lb_minIndex;
        private UI.Controls.DarkTextBox tb_maxIndex;
        private UI.Controls.DarkLabel lb_maxIndex;
        private UI.Controls.DarkLabel darkLabel3;
    }
}