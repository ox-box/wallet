namespace OX.Wallets.Base
{
    partial class DialogLockTransfer
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
            darkLabel2 = new UI.Controls.DarkLabel();
            darkLabel3 = new UI.Controls.DarkLabel();
            darkLabel4 = new UI.Controls.DarkLabel();
            textBox3 = new UI.Controls.DarkTextBox();
            textBox1 = new UI.Controls.DarkTextBox();
            textBox2 = new UI.Controls.DarkTextBox();
            panel2 = new System.Windows.Forms.Panel();
            rbBlock = new UI.Controls.DarkRadioButton();
            rbTime = new UI.Controls.DarkRadioButton();
            darkLabel5 = new UI.Controls.DarkLabel();
            darkLabel6 = new UI.Controls.DarkLabel();
            tb_block = new UI.Controls.DarkTextBox();
            dtp_time = new System.Windows.Forms.DateTimePicker();
            cb_assets = new UI.Controls.DarkComboBox();
            lb_asset = new UI.Controls.DarkLabel();
            cb_lockself = new UI.Controls.DarkCheckBox();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(18, 18);
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
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel2.Location = new System.Drawing.Point(42, 114);
            darkLabel2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new System.Drawing.Size(50, 24);
            darkLabel2.TabIndex = 3;
            darkLabel2.Text = "余额:";
            // 
            // darkLabel3
            // 
            darkLabel3.AutoSize = true;
            darkLabel3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel3.Location = new System.Drawing.Point(42, 186);
            darkLabel3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            darkLabel3.Name = "darkLabel3";
            darkLabel3.Size = new System.Drawing.Size(86, 24);
            darkLabel3.TabIndex = 4;
            darkLabel3.Text = "收款公钥:";
            // 
            // darkLabel4
            // 
            darkLabel4.AutoSize = true;
            darkLabel4.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel4.Location = new System.Drawing.Point(487, 114);
            darkLabel4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            darkLabel4.Name = "darkLabel4";
            darkLabel4.Size = new System.Drawing.Size(50, 24);
            darkLabel4.TabIndex = 5;
            darkLabel4.Text = "金额:";
            // 
            // textBox3
            // 
            textBox3.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox3.Location = new System.Drawing.Point(141, 112);
            textBox3.Margin = new System.Windows.Forms.Padding(6);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new System.Drawing.Size(268, 30);
            textBox3.TabIndex = 8;
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox1.Location = new System.Drawing.Point(140, 182);
            textBox1.Margin = new System.Windows.Forms.Padding(6);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(786, 30);
            textBox1.TabIndex = 9;
            textBox1.TextChanged += textBox_TextChanged;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox2.Location = new System.Drawing.Point(587, 112);
            textBox2.Margin = new System.Windows.Forms.Padding(6);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(339, 30);
            textBox2.TabIndex = 10;
            textBox2.TextChanged += textBox_TextChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(rbBlock);
            panel2.Controls.Add(rbTime);
            panel2.Location = new System.Drawing.Point(141, 301);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(647, 64);
            panel2.TabIndex = 12;
            // 
            // rbBlock
            // 
            rbBlock.AutoSize = true;
            rbBlock.Checked = true;
            rbBlock.Location = new System.Drawing.Point(25, 18);
            rbBlock.Margin = new System.Windows.Forms.Padding(6);
            rbBlock.Name = "rbBlock";
            rbBlock.Size = new System.Drawing.Size(72, 28);
            rbBlock.SpecialBorderColor = null;
            rbBlock.SpecialFillColor = null;
            rbBlock.SpecialTextColor = null;
            rbBlock.TabIndex = 6;
            rbBlock.TabStop = true;
            rbBlock.Text = "OXS";
            rbBlock.CheckedChanged += rbTime_CheckedChanged;
            // 
            // rbTime
            // 
            rbTime.AutoSize = true;
            rbTime.Location = new System.Drawing.Point(498, 18);
            rbTime.Margin = new System.Windows.Forms.Padding(6);
            rbTime.Name = "rbTime";
            rbTime.Size = new System.Drawing.Size(74, 28);
            rbTime.SpecialBorderColor = null;
            rbTime.SpecialFillColor = null;
            rbTime.SpecialTextColor = null;
            rbTime.TabIndex = 7;
            rbTime.Text = "OXC";
            rbTime.CheckedChanged += rbTime_CheckedChanged;
            // 
            // darkLabel5
            // 
            darkLabel5.AutoSize = true;
            darkLabel5.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel5.Location = new System.Drawing.Point(42, 321);
            darkLabel5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            darkLabel5.Name = "darkLabel5";
            darkLabel5.Size = new System.Drawing.Size(86, 24);
            darkLabel5.TabIndex = 13;
            darkLabel5.Text = "锁定类型:";
            // 
            // darkLabel6
            // 
            darkLabel6.AutoSize = true;
            darkLabel6.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel6.Location = new System.Drawing.Point(140, 228);
            darkLabel6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            darkLabel6.Name = "darkLabel6";
            darkLabel6.Size = new System.Drawing.Size(0, 24);
            darkLabel6.TabIndex = 14;
            // 
            // tb_block
            // 
            tb_block.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_block.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_block.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_block.Location = new System.Drawing.Point(141, 394);
            tb_block.Margin = new System.Windows.Forms.Padding(6);
            tb_block.Name = "tb_block";
            tb_block.Size = new System.Drawing.Size(300, 30);
            tb_block.TabIndex = 16;
            tb_block.TextChanged += tb_block_TextChanged;
            // 
            // dtp_time
            // 
            dtp_time.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dtp_time.CustomFormat = "yyyy/MM/dd  HH:mm:ss";
            dtp_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtp_time.Location = new System.Drawing.Point(587, 391);
            dtp_time.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dtp_time.Name = "dtp_time";
            dtp_time.Size = new System.Drawing.Size(300, 30);
            dtp_time.TabIndex = 17;
            dtp_time.Visible = false;
            // 
            // cb_assets
            // 
            cb_assets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            cb_assets.FormattingEnabled = true;
            cb_assets.Location = new System.Drawing.Point(140, 35);
            cb_assets.Name = "cb_assets";
            cb_assets.Size = new System.Drawing.Size(786, 31);
            cb_assets.SpecialBorderColor = null;
            cb_assets.SpecialFillColor = null;
            cb_assets.SpecialTextColor = null;
            cb_assets.TabIndex = 19;
            cb_assets.SelectedIndexChanged += cb_assets_SelectedIndexChanged;
            // 
            // lb_asset
            // 
            lb_asset.AutoSize = true;
            lb_asset.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_asset.Location = new System.Drawing.Point(52, 38);
            lb_asset.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_asset.Name = "lb_asset";
            lb_asset.Size = new System.Drawing.Size(50, 24);
            lb_asset.TabIndex = 18;
            lb_asset.Text = "资产:";
            // 
            // cb_lockself
            // 
            cb_lockself.AutoSize = true;
            cb_lockself.Location = new System.Drawing.Point(141, 267);
            cb_lockself.Name = "cb_lockself";
            cb_lockself.Size = new System.Drawing.Size(169, 28);
            cb_lockself.SpecialBorderColor = null;
            cb_lockself.SpecialFillColor = null;
            cb_lockself.SpecialTextColor = null;
            cb_lockself.TabIndex = 24;
            cb_lockself.Text = "darkCheckBox1";
            cb_lockself.CheckedChanged += cb_lockself_CheckedChanged;
            // 
            // DialogLockTransfer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(980, 559);
            Controls.Add(cb_lockself);
            Controls.Add(cb_assets);
            Controls.Add(lb_asset);
            Controls.Add(dtp_time);
            Controls.Add(tb_block);
            Controls.Add(darkLabel6);
            Controls.Add(darkLabel5);
            Controls.Add(panel2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(textBox3);
            Controls.Add(darkLabel4);
            Controls.Add(darkLabel3);
            Controls.Add(darkLabel2);
            Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            Name = "DialogLockTransfer";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "锁仓转账";
            Load += PayToDialog_Load;
            Controls.SetChildIndex(darkLabel2, 0);
            Controls.SetChildIndex(darkLabel3, 0);
            Controls.SetChildIndex(darkLabel4, 0);
            Controls.SetChildIndex(textBox3, 0);
            Controls.SetChildIndex(textBox1, 0);
            Controls.SetChildIndex(textBox2, 0);
            Controls.SetChildIndex(panel2, 0);
            Controls.SetChildIndex(darkLabel5, 0);
            Controls.SetChildIndex(darkLabel6, 0);
            Controls.SetChildIndex(tb_block, 0);
            Controls.SetChildIndex(dtp_time, 0);
            Controls.SetChildIndex(lb_asset, 0);
            Controls.SetChildIndex(cb_assets, 0);
            Controls.SetChildIndex(cb_lockself, 0);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OX.Wallets.UI.Controls.DarkLabel darkLabel2;
        private OX.Wallets.UI.Controls.DarkLabel darkLabel3;
        private OX.Wallets.UI.Controls.DarkLabel darkLabel4;
        private OX.Wallets.UI.Controls.DarkTextBox textBox3;
        private OX.Wallets.UI.Controls.DarkTextBox textBox1;
        private OX.Wallets.UI.Controls.DarkTextBox textBox2;
        private System.Windows.Forms.Panel panel2;
        private UI.Controls.DarkRadioButton rbBlock;
        private UI.Controls.DarkRadioButton rbTime;
        private UI.Controls.DarkLabel darkLabel5;
        private UI.Controls.DarkLabel darkLabel6;
        private UI.Controls.DarkTextBox tb_block;
        private System.Windows.Forms.DateTimePicker dtp_time;
        private UI.Controls.DarkComboBox cb_assets;
        private UI.Controls.DarkLabel lb_asset;
        private UI.Controls.DarkCheckBox cb_lockself;
    }
}