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
            panel = new System.Windows.Forms.Panel();
            darkLabel3 = new UI.Controls.DarkLabel();
            tb_minIndex = new UI.Controls.DarkTextBox();
            lb_minIndex = new UI.Controls.DarkLabel();
            tb_maxIndex = new UI.Controls.DarkTextBox();
            lb_maxIndex = new UI.Controls.DarkLabel();
            bt_build = new UI.Controls.DarkButton();
            tb_copy = new UI.Controls.DarkButton();
            lb_signature = new UI.Controls.DarkLabel();
            tb_signature = new UI.Controls.DarkTextBox();
            lb_nfthash_v = new UI.Controls.DarkLabel();
            lb_nfthash = new UI.Controls.DarkLabel();
            tb_amount = new UI.Controls.DarkTextBox();
            lb_amount = new UI.Controls.DarkLabel();
            bt_publish = new UI.Controls.DarkButton();
            panel.SuspendLayout();
            SuspendLayout();
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
            panel.Controls.Add(bt_publish);
            panel.Controls.Add(darkLabel3);
            panel.Controls.Add(tb_minIndex);
            panel.Controls.Add(lb_minIndex);
            panel.Controls.Add(tb_maxIndex);
            panel.Controls.Add(lb_maxIndex);
            panel.Controls.Add(bt_build);
            panel.Controls.Add(tb_copy);
            panel.Controls.Add(lb_signature);
            panel.Controls.Add(tb_signature);
            panel.Controls.Add(lb_nfthash_v);
            panel.Controls.Add(lb_nfthash);
            panel.Controls.Add(tb_amount);
            panel.Controls.Add(lb_amount);
            resources.ApplyResources(panel, "panel");
            panel.Name = "panel";
            panel.Paint += panel_Paint;
            // 
            // darkLabel3
            // 
            resources.ApplyResources(darkLabel3, "darkLabel3");
            darkLabel3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel3.Name = "darkLabel3";
            // 
            // tb_minIndex
            // 
            tb_minIndex.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_minIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_minIndex.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_minIndex, "tb_minIndex");
            tb_minIndex.Name = "tb_minIndex";
            tb_minIndex.TextChanged += tb_amount_TextChanged;
            // 
            // lb_minIndex
            // 
            resources.ApplyResources(lb_minIndex, "lb_minIndex");
            lb_minIndex.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_minIndex.Name = "lb_minIndex";
            // 
            // tb_maxIndex
            // 
            tb_maxIndex.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_maxIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_maxIndex.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_maxIndex, "tb_maxIndex");
            tb_maxIndex.Name = "tb_maxIndex";
            tb_maxIndex.TextChanged += tb_amount_TextChanged;
            // 
            // lb_maxIndex
            // 
            resources.ApplyResources(lb_maxIndex, "lb_maxIndex");
            lb_maxIndex.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_maxIndex.Name = "lb_maxIndex";
            // 
            // bt_build
            // 
            resources.ApplyResources(bt_build, "bt_build");
            bt_build.Name = "bt_build";
            bt_build.SpecialBorderColor = null;
            bt_build.SpecialFillColor = null;
            bt_build.SpecialTextColor = null;
            bt_build.Click += bt_build_Click;
            // 
            // tb_copy
            // 
            resources.ApplyResources(tb_copy, "tb_copy");
            tb_copy.Name = "tb_copy";
            tb_copy.SpecialBorderColor = null;
            tb_copy.SpecialFillColor = null;
            tb_copy.SpecialTextColor = null;
            tb_copy.Click += tb_copy_Click;
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
            tb_signature.ReadOnly = true;
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
            // tb_amount
            // 
            tb_amount.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_amount, "tb_amount");
            tb_amount.Name = "tb_amount";
            tb_amount.TextChanged += tb_amount_TextChanged;
            // 
            // lb_amount
            // 
            resources.ApplyResources(lb_amount, "lb_amount");
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_amount.Name = "lb_amount";
            // 
            // bt_publish
            // 
            resources.ApplyResources(bt_publish, "bt_publish");
            bt_publish.Name = "bt_publish";
            bt_publish.SpecialBorderColor = null;
            bt_publish.SpecialFillColor = null;
            bt_publish.SpecialTextColor = null;
            bt_publish.Click += bt_publish_Click;
            // 
            // SellNFT
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SellNFT";
            FormClosing += ClaimForm_FormClosing;
            Load += NewEvent_Load;
            Controls.SetChildIndex(panel, 0);
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
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
        private UI.Controls.DarkButton bt_publish;
    }
}