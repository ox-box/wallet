namespace OX.Wallets.Base
{
    partial class DialogAssetLockVote
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
            lb_asset = new UI.Controls.DarkLabel();
            lb_balance = new UI.Controls.DarkLabel();
            lb_amount = new UI.Controls.DarkLabel();
            tb_balance = new UI.Controls.DarkTextBox();
            tb_amount = new UI.Controls.DarkTextBox();
            cb_accounts = new UI.Controls.DarkComboBox();
            darkLabel1 = new UI.Controls.DarkLabel();
            tb_expire = new UI.Controls.DarkTextBox();
            lv_votes = new UI.Controls.DarkListView();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(166, 18);
            btnCancel.Click += btnCancel_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(18, 18);
            // 
            // btnYes
            // 
            btnYes.Location = new System.Drawing.Point(18, 18);
            // 
            // btnNo
            // 
            btnNo.Location = new System.Drawing.Point(18, 18);
            // 
            // btnRetry
            // 
            btnRetry.Location = new System.Drawing.Point(708, 18);
            // 
            // btnIgnore
            // 
            btnIgnore.Location = new System.Drawing.Point(708, 18);
            // 
            // lb_asset
            // 
            lb_asset.AutoSize = true;
            lb_asset.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_asset.Location = new System.Drawing.Point(42, 58);
            lb_asset.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_asset.Name = "lb_asset";
            lb_asset.Size = new System.Drawing.Size(50, 24);
            lb_asset.TabIndex = 2;
            lb_asset.Text = "资产:";
            // 
            // lb_balance
            // 
            lb_balance.AutoSize = true;
            lb_balance.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_balance.Location = new System.Drawing.Point(624, 172);
            lb_balance.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_balance.Name = "lb_balance";
            lb_balance.Size = new System.Drawing.Size(50, 24);
            lb_balance.TabIndex = 3;
            lb_balance.Text = "余额:";
            // 
            // lb_amount
            // 
            lb_amount.AutoSize = true;
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_amount.Location = new System.Drawing.Point(629, 236);
            lb_amount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_amount.Name = "lb_amount";
            lb_amount.Size = new System.Drawing.Size(50, 24);
            lb_amount.TabIndex = 5;
            lb_amount.Text = "金额:";
            // 
            // tb_balance
            // 
            tb_balance.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_balance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_balance.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_balance.Location = new System.Drawing.Point(717, 170);
            tb_balance.Margin = new System.Windows.Forms.Padding(6);
            tb_balance.Name = "tb_balance";
            tb_balance.ReadOnly = true;
            tb_balance.Size = new System.Drawing.Size(320, 30);
            tb_balance.TabIndex = 8;
            // 
            // tb_amount
            // 
            tb_amount.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_amount.Location = new System.Drawing.Point(717, 231);
            tb_amount.Margin = new System.Windows.Forms.Padding(6);
            tb_amount.Name = "tb_amount";
            tb_amount.Size = new System.Drawing.Size(320, 30);
            tb_amount.TabIndex = 10;
            tb_amount.TextChanged += textBox_TextChanged;
            // 
            // cb_accounts
            // 
            cb_accounts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            cb_accounts.FormattingEnabled = true;
            cb_accounts.Location = new System.Drawing.Point(130, 55);
            cb_accounts.Name = "cb_accounts";
            cb_accounts.Size = new System.Drawing.Size(907, 31);
            cb_accounts.SpecialBorderColor = null;
            cb_accounts.SpecialFillColor = null;
            cb_accounts.SpecialTextColor = null;
            cb_accounts.TabIndex = 11;
            cb_accounts.SelectedIndexChanged += cb_assets_SelectedIndexChanged;
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel1.Location = new System.Drawing.Point(629, 295);
            darkLabel1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new System.Drawing.Size(50, 24);
            darkLabel1.TabIndex = 12;
            darkLabel1.Text = "金额:";
            // 
            // tb_expire
            // 
            tb_expire.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_expire.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_expire.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_expire.Location = new System.Drawing.Point(717, 293);
            tb_expire.Margin = new System.Windows.Forms.Padding(6);
            tb_expire.Name = "tb_expire";
            tb_expire.Size = new System.Drawing.Size(320, 30);
            tb_expire.TabIndex = 13;
            tb_expire.TextChanged += textBox_TextChanged;
            // 
            // lv_votes
            // 
            lv_votes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lv_votes.Location = new System.Drawing.Point(42, 114);
            lv_votes.Name = "lv_votes";
            lv_votes.Size = new System.Drawing.Size(549, 290);
            lv_votes.TabIndex = 14;
            lv_votes.Text = "darkListView1";
            lv_votes.SelectedIndicesChanged += lv_votes_SelectedIndicesChanged;
            // 
            // DialogAssetLockVote
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1103, 515);
            Controls.Add(lv_votes);
            Controls.Add(tb_expire);
            Controls.Add(darkLabel1);
            Controls.Add(cb_accounts);
            Controls.Add(tb_amount);
            Controls.Add(tb_balance);
            Controls.Add(lb_amount);
            Controls.Add(lb_balance);
            Controls.Add(lb_asset);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            Name = "DialogAssetLockVote";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "转账";
            Load += PayToDialog_Load;
            Controls.SetChildIndex(lb_asset, 0);
            Controls.SetChildIndex(lb_balance, 0);
            Controls.SetChildIndex(lb_amount, 0);
            Controls.SetChildIndex(tb_balance, 0);
            Controls.SetChildIndex(tb_amount, 0);
            Controls.SetChildIndex(cb_accounts, 0);
            Controls.SetChildIndex(darkLabel1, 0);
            Controls.SetChildIndex(tb_expire, 0);
            Controls.SetChildIndex(lv_votes, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel lb_asset;
        private OX.Wallets.UI.Controls.DarkLabel lb_balance;
        private OX.Wallets.UI.Controls.DarkLabel lb_amount;
        private OX.Wallets.UI.Controls.DarkTextBox tb_balance;
        private OX.Wallets.UI.Controls.DarkTextBox tb_amount;
        private UI.Controls.DarkComboBox cb_accounts;
        private UI.Controls.DarkLabel darkLabel1;
        private UI.Controls.DarkTextBox tb_expire;
        private UI.Controls.DarkListView lv_votes;
    }
}