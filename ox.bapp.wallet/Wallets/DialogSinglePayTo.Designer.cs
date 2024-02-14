namespace OX.Wallets.Base
{
    partial class DialogSinglePayTo
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
            lb_native_targetAddress = new UI.Controls.DarkLabel();
            lb_amount = new UI.Controls.DarkLabel();
            tb_balance = new UI.Controls.DarkTextBox();
            tb_native_targetAddress = new UI.Controls.DarkTextBox();
            tb_amount = new UI.Controls.DarkTextBox();
            cb_assets = new UI.Controls.DarkComboBox();
            tab_target = new UI.Controls.DarkTabControl();
            tab_native = new System.Windows.Forms.TabPage();
            tab_lock = new System.Windows.Forms.TabPage();
            cb_lockself = new UI.Controls.DarkCheckBox();
            dtp_time = new System.Windows.Forms.DateTimePicker();
            tb_block = new UI.Controls.DarkTextBox();
            lb_lock_targetAddress = new UI.Controls.DarkLabel();
            lb_lock_type = new UI.Controls.DarkLabel();
            panel2 = new System.Windows.Forms.Panel();
            rbBlock = new UI.Controls.DarkRadioButton();
            rbTime = new UI.Controls.DarkRadioButton();
            tb_lock_targetPubkey = new UI.Controls.DarkTextBox();
            lb_lock_targetPubkey = new UI.Controls.DarkLabel();
            tab_eth = new System.Windows.Forms.TabPage();
            lb_mapaddress = new UI.Controls.DarkLabel();
            tb_eth_lockIndex = new UI.Controls.DarkTextBox();
            lb_eth_lockindex = new UI.Controls.DarkLabel();
            tb_eth_targetAddress = new UI.Controls.DarkTextBox();
            lb_eth_targetAddress = new UI.Controls.DarkLabel();
            tab_target.SuspendLayout();
            tab_native.SuspendLayout();
            tab_lock.SuspendLayout();
            panel2.SuspendLayout();
            tab_eth.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
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
            lb_balance.Location = new System.Drawing.Point(624, 116);
            lb_balance.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_balance.Name = "lb_balance";
            lb_balance.Size = new System.Drawing.Size(50, 24);
            lb_balance.TabIndex = 3;
            lb_balance.Text = "余额:";
            // 
            // lb_native_targetAddress
            // 
            lb_native_targetAddress.AutoSize = true;
            lb_native_targetAddress.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_native_targetAddress.Location = new System.Drawing.Point(38, 124);
            lb_native_targetAddress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_native_targetAddress.Name = "lb_native_targetAddress";
            lb_native_targetAddress.Size = new System.Drawing.Size(54, 25);
            lb_native_targetAddress.TabIndex = 4;
            lb_native_targetAddress.Text = "地址:";
            // 
            // lb_amount
            // 
            lb_amount.AutoSize = true;
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_amount.Location = new System.Drawing.Point(42, 120);
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
            tb_balance.Location = new System.Drawing.Point(717, 114);
            tb_balance.Margin = new System.Windows.Forms.Padding(6);
            tb_balance.Name = "tb_balance";
            tb_balance.ReadOnly = true;
            tb_balance.Size = new System.Drawing.Size(320, 30);
            tb_balance.TabIndex = 8;
            // 
            // tb_native_targetAddress
            // 
            tb_native_targetAddress.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_native_targetAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_native_targetAddress.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_native_targetAddress.Location = new System.Drawing.Point(152, 119);
            tb_native_targetAddress.Margin = new System.Windows.Forms.Padding(6);
            tb_native_targetAddress.Name = "tb_native_targetAddress";
            tb_native_targetAddress.Size = new System.Drawing.Size(774, 31);
            tb_native_targetAddress.TabIndex = 9;
            tb_native_targetAddress.TextChanged += textBox_TextChanged;
            // 
            // tb_amount
            // 
            tb_amount.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_amount.Location = new System.Drawing.Point(130, 115);
            tb_amount.Margin = new System.Windows.Forms.Padding(6);
            tb_amount.Name = "tb_amount";
            tb_amount.Size = new System.Drawing.Size(320, 30);
            tb_amount.TabIndex = 10;
            tb_amount.TextChanged += textBox_TextChanged;
            // 
            // cb_assets
            // 
            cb_assets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            cb_assets.FormattingEnabled = true;
            cb_assets.Location = new System.Drawing.Point(130, 55);
            cb_assets.Name = "cb_assets";
            cb_assets.Size = new System.Drawing.Size(907, 31);
            cb_assets.SpecialBorderColor = null;
            cb_assets.SpecialFillColor = null;
            cb_assets.SpecialTextColor = null;
            cb_assets.TabIndex = 11;
            cb_assets.SelectedIndexChanged += cb_assets_SelectedIndexChanged;
            // 
            // tab_target
            // 
            tab_target.AllowDrop = true;
            tab_target.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tab_target.Controls.Add(tab_native);
            tab_target.Controls.Add(tab_lock);
            tab_target.Controls.Add(tab_eth);
            tab_target.DisableClose = true;
            tab_target.DisableDragging = true;
            tab_target.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tab_target.Location = new System.Drawing.Point(42, 173);
            tab_target.Name = "tab_target";
            tab_target.Padding = new System.Drawing.Point(14, 4);
            tab_target.SelectedIndex = 0;
            tab_target.SelectedTabTextColor = System.Drawing.Color.Teal;
            tab_target.Size = new System.Drawing.Size(995, 332);
            tab_target.TabIndex = 12;
            tab_target.SelectedIndexChanged += tab_target_SelectedIndexChanged;
            tab_target.Selecting += tab_target_Selecting;
            // 
            // tab_native
            // 
            tab_native.BackColor = System.Drawing.Color.DarkGray;
            tab_native.Controls.Add(tb_native_targetAddress);
            tab_native.Controls.Add(lb_native_targetAddress);
            tab_native.Location = new System.Drawing.Point(4, 32);
            tab_native.Name = "tab_native";
            tab_native.Padding = new System.Windows.Forms.Padding(3);
            tab_native.Size = new System.Drawing.Size(987, 296);
            tab_native.TabIndex = 0;
            tab_native.Text = "tab_native";
            // 
            // tab_lock
            // 
            tab_lock.BackColor = System.Drawing.Color.DarkGray;
            tab_lock.Controls.Add(cb_lockself);
            tab_lock.Controls.Add(dtp_time);
            tab_lock.Controls.Add(tb_block);
            tab_lock.Controls.Add(lb_lock_targetAddress);
            tab_lock.Controls.Add(lb_lock_type);
            tab_lock.Controls.Add(panel2);
            tab_lock.Controls.Add(tb_lock_targetPubkey);
            tab_lock.Controls.Add(lb_lock_targetPubkey);
            tab_lock.Location = new System.Drawing.Point(4, 32);
            tab_lock.Name = "tab_lock";
            tab_lock.Padding = new System.Windows.Forms.Padding(3);
            tab_lock.Size = new System.Drawing.Size(192, 64);
            tab_lock.TabIndex = 1;
            tab_lock.Text = "tabPage2";
            // 
            // cb_lockself
            // 
            cb_lockself.AutoSize = true;
            cb_lockself.Location = new System.Drawing.Point(154, 107);
            cb_lockself.Name = "cb_lockself";
            cb_lockself.Size = new System.Drawing.Size(159, 29);
            cb_lockself.SpecialBorderColor = null;
            cb_lockself.SpecialFillColor = null;
            cb_lockself.SpecialTextColor = null;
            cb_lockself.TabIndex = 32;
            cb_lockself.Text = "darkCheckBox1";
            cb_lockself.CheckedChanged += cb_lockself_CheckedChanged;
            // 
            // dtp_time
            // 
            dtp_time.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dtp_time.CustomFormat = "yyyy/MM/dd  HH:mm:ss";
            dtp_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtp_time.Location = new System.Drawing.Point(600, 231);
            dtp_time.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dtp_time.Name = "dtp_time";
            dtp_time.Size = new System.Drawing.Size(0, 31);
            dtp_time.TabIndex = 31;
            dtp_time.Visible = false;
            // 
            // tb_block
            // 
            tb_block.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_block.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_block.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_block.Location = new System.Drawing.Point(154, 234);
            tb_block.Margin = new System.Windows.Forms.Padding(6);
            tb_block.Name = "tb_block";
            tb_block.Size = new System.Drawing.Size(300, 31);
            tb_block.TabIndex = 30;
            tb_block.TextChanged += tb_block_TextChanged;
            // 
            // lb_lock_targetAddress
            // 
            lb_lock_targetAddress.AutoSize = true;
            lb_lock_targetAddress.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_lock_targetAddress.Location = new System.Drawing.Point(153, 68);
            lb_lock_targetAddress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_lock_targetAddress.Name = "lb_lock_targetAddress";
            lb_lock_targetAddress.Size = new System.Drawing.Size(0, 25);
            lb_lock_targetAddress.TabIndex = 29;
            // 
            // lb_lock_type
            // 
            lb_lock_type.AutoSize = true;
            lb_lock_type.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_lock_type.Location = new System.Drawing.Point(39, 163);
            lb_lock_type.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_lock_type.Name = "lb_lock_type";
            lb_lock_type.Size = new System.Drawing.Size(92, 25);
            lb_lock_type.TabIndex = 28;
            lb_lock_type.Text = "锁仓类型:";
            // 
            // panel2
            // 
            panel2.Controls.Add(rbBlock);
            panel2.Controls.Add(rbTime);
            panel2.Location = new System.Drawing.Point(154, 141);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(647, 64);
            panel2.TabIndex = 27;
            // 
            // rbBlock
            // 
            rbBlock.AutoSize = true;
            rbBlock.Checked = true;
            rbBlock.Location = new System.Drawing.Point(25, 18);
            rbBlock.Margin = new System.Windows.Forms.Padding(6);
            rbBlock.Name = "rbBlock";
            rbBlock.Size = new System.Drawing.Size(72, 29);
            rbBlock.SpecialBorderColor = null;
            rbBlock.SpecialFillColor = null;
            rbBlock.SpecialTextColor = null;
            rbBlock.TabIndex = 6;
            rbBlock.TabStop = true;
            rbBlock.Text = "OXS";
            rbBlock.CheckedChanged += rbBlock_CheckedChanged;
            // 
            // rbTime
            // 
            rbTime.AutoSize = true;
            rbTime.Location = new System.Drawing.Point(498, 18);
            rbTime.Margin = new System.Windows.Forms.Padding(6);
            rbTime.Name = "rbTime";
            rbTime.Size = new System.Drawing.Size(73, 29);
            rbTime.SpecialBorderColor = null;
            rbTime.SpecialFillColor = null;
            rbTime.SpecialTextColor = null;
            rbTime.TabIndex = 7;
            rbTime.Text = "OXC";
            rbTime.CheckedChanged += rbBlock_CheckedChanged;
            // 
            // tb_lock_targetPubkey
            // 
            tb_lock_targetPubkey.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_lock_targetPubkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_lock_targetPubkey.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_lock_targetPubkey.Location = new System.Drawing.Point(153, 25);
            tb_lock_targetPubkey.Margin = new System.Windows.Forms.Padding(6);
            tb_lock_targetPubkey.Name = "tb_lock_targetPubkey";
            tb_lock_targetPubkey.Size = new System.Drawing.Size(790, 31);
            tb_lock_targetPubkey.TabIndex = 26;
            tb_lock_targetPubkey.TextChanged += tb_lock_targetPubkey_TextChanged;
            // 
            // lb_lock_targetPubkey
            // 
            lb_lock_targetPubkey.AutoSize = true;
            lb_lock_targetPubkey.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_lock_targetPubkey.Location = new System.Drawing.Point(39, 28);
            lb_lock_targetPubkey.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_lock_targetPubkey.Name = "lb_lock_targetPubkey";
            lb_lock_targetPubkey.Size = new System.Drawing.Size(92, 25);
            lb_lock_targetPubkey.TabIndex = 25;
            lb_lock_targetPubkey.Text = "收款公钥:";
            // 
            // tab_eth
            // 
            tab_eth.BackColor = System.Drawing.Color.DarkGray;
            tab_eth.Controls.Add(lb_mapaddress);
            tab_eth.Controls.Add(tb_eth_lockIndex);
            tab_eth.Controls.Add(lb_eth_lockindex);
            tab_eth.Controls.Add(tb_eth_targetAddress);
            tab_eth.Controls.Add(lb_eth_targetAddress);
            tab_eth.Location = new System.Drawing.Point(4, 32);
            tab_eth.Name = "tab_eth";
            tab_eth.Size = new System.Drawing.Size(987, 296);
            tab_eth.TabIndex = 2;
            tab_eth.Text = "tabPage3";
            // 
            // lb_mapaddress
            // 
            lb_mapaddress.AutoSize = true;
            lb_mapaddress.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_mapaddress.Location = new System.Drawing.Point(152, 105);
            lb_mapaddress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_mapaddress.Name = "lb_mapaddress";
            lb_mapaddress.Size = new System.Drawing.Size(0, 25);
            lb_mapaddress.TabIndex = 19;
            // 
            // tb_eth_lockIndex
            // 
            tb_eth_lockIndex.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_eth_lockIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_eth_lockIndex.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_eth_lockIndex.Location = new System.Drawing.Point(148, 178);
            tb_eth_lockIndex.Margin = new System.Windows.Forms.Padding(6);
            tb_eth_lockIndex.Name = "tb_eth_lockIndex";
            tb_eth_lockIndex.Size = new System.Drawing.Size(256, 31);
            tb_eth_lockIndex.TabIndex = 18;
            tb_eth_lockIndex.Text = "0";
            tb_eth_lockIndex.TextChanged += tb_block_TextChanged;
            // 
            // lb_eth_lockindex
            // 
            lb_eth_lockindex.AutoSize = true;
            lb_eth_lockindex.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_eth_lockindex.Location = new System.Drawing.Point(25, 183);
            lb_eth_lockindex.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_eth_lockindex.Name = "lb_eth_lockindex";
            lb_eth_lockindex.Size = new System.Drawing.Size(54, 25);
            lb_eth_lockindex.TabIndex = 17;
            lb_eth_lockindex.Text = "金额:";
            // 
            // tb_eth_targetAddress
            // 
            tb_eth_targetAddress.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_eth_targetAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_eth_targetAddress.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_eth_targetAddress.Location = new System.Drawing.Point(148, 59);
            tb_eth_targetAddress.Margin = new System.Windows.Forms.Padding(6);
            tb_eth_targetAddress.Name = "tb_eth_targetAddress";
            tb_eth_targetAddress.Size = new System.Drawing.Size(805, 31);
            tb_eth_targetAddress.TabIndex = 16;
            tb_eth_targetAddress.TextChanged += tb_eth_targetAddress_TextChanged;
            // 
            // lb_eth_targetAddress
            // 
            lb_eth_targetAddress.AutoSize = true;
            lb_eth_targetAddress.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_eth_targetAddress.Location = new System.Drawing.Point(25, 64);
            lb_eth_targetAddress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_eth_targetAddress.Name = "lb_eth_targetAddress";
            lb_eth_targetAddress.Size = new System.Drawing.Size(54, 25);
            lb_eth_targetAddress.TabIndex = 15;
            lb_eth_targetAddress.Text = "地址:";
            // 
            // DialogSinglePayTo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1049, 597);
            Controls.Add(tab_target);
            Controls.Add(cb_assets);
            Controls.Add(tb_amount);
            Controls.Add(tb_balance);
            Controls.Add(lb_amount);
            Controls.Add(lb_balance);
            Controls.Add(lb_asset);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            Name = "DialogSinglePayTo";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "转账";
            Load += PayToDialog_Load;
            Controls.SetChildIndex(lb_asset, 0);
            Controls.SetChildIndex(lb_balance, 0);
            Controls.SetChildIndex(lb_amount, 0);
            Controls.SetChildIndex(tb_balance, 0);
            Controls.SetChildIndex(tb_amount, 0);
            Controls.SetChildIndex(cb_assets, 0);
            Controls.SetChildIndex(tab_target, 0);
            tab_target.ResumeLayout(false);
            tab_native.ResumeLayout(false);
            tab_native.PerformLayout();
            tab_lock.ResumeLayout(false);
            tab_lock.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tab_eth.ResumeLayout(false);
            tab_eth.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel lb_asset;
        private OX.Wallets.UI.Controls.DarkLabel lb_balance;
        private OX.Wallets.UI.Controls.DarkLabel lb_native_targetAddress;
        private OX.Wallets.UI.Controls.DarkLabel lb_amount;
        private OX.Wallets.UI.Controls.DarkTextBox tb_balance;
        private OX.Wallets.UI.Controls.DarkTextBox tb_native_targetAddress;
        private OX.Wallets.UI.Controls.DarkTextBox tb_amount;
        private UI.Controls.DarkComboBox cb_assets;
        private UI.Controls.DarkTabControl tab_target;
        private System.Windows.Forms.TabPage tab_native;
        private System.Windows.Forms.TabPage tab_lock;
        private System.Windows.Forms.TabPage tab_eth;
        private UI.Controls.DarkCheckBox cb_lockself;
        private System.Windows.Forms.DateTimePicker dtp_time;
        private UI.Controls.DarkTextBox tb_block;
        private UI.Controls.DarkLabel lb_lock_targetAddress;
        private UI.Controls.DarkLabel lb_lock_type;
        private System.Windows.Forms.Panel panel2;
        private UI.Controls.DarkRadioButton rbBlock;
        private UI.Controls.DarkRadioButton rbTime;
        private UI.Controls.DarkTextBox tb_lock_targetPubkey;
        private UI.Controls.DarkLabel lb_lock_targetPubkey;
        private UI.Controls.DarkTextBox tb_eth_lockIndex;
        private UI.Controls.DarkLabel lb_eth_lockindex;
        private UI.Controls.DarkTextBox tb_eth_targetAddress;
        private UI.Controls.DarkLabel lb_eth_targetAddress;
        private UI.Controls.DarkLabel lb_mapaddress;
    }
}