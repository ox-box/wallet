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
            this.darkLabel1 = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkLabel2 = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkLabel3 = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkLabel4 = new OX.Wallets.UI.Controls.DarkLabel();
            this.tbGTS = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.rbGTC = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.textBox3 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.textBox1 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.textBox2 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbBlock = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.rbTime = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.darkLabel5 = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkLabel6 = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_block = new OX.Wallets.UI.Controls.DarkTextBox();
            this.dtp_time = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(18, 18);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(18, 18);
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(18, 18);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(18, 18);
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(708, 18);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(708, 18);
            // 
            // darkLabel1
            // 
            this.darkLabel1.AutoSize = true;
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(42, 58);
            this.darkLabel1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(50, 24);
            this.darkLabel1.TabIndex = 2;
            this.darkLabel1.Text = "资产:";
            // 
            // darkLabel2
            // 
            this.darkLabel2.AutoSize = true;
            this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel2.Location = new System.Drawing.Point(42, 114);
            this.darkLabel2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel2.Name = "darkLabel2";
            this.darkLabel2.Size = new System.Drawing.Size(50, 24);
            this.darkLabel2.TabIndex = 3;
            this.darkLabel2.Text = "余额:";
            // 
            // darkLabel3
            // 
            this.darkLabel3.AutoSize = true;
            this.darkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel3.Location = new System.Drawing.Point(42, 186);
            this.darkLabel3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel3.Name = "darkLabel3";
            this.darkLabel3.Size = new System.Drawing.Size(86, 24);
            this.darkLabel3.TabIndex = 4;
            this.darkLabel3.Text = "收款公钥:";
            // 
            // darkLabel4
            // 
            this.darkLabel4.AutoSize = true;
            this.darkLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel4.Location = new System.Drawing.Point(454, 114);
            this.darkLabel4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel4.Name = "darkLabel4";
            this.darkLabel4.Size = new System.Drawing.Size(50, 24);
            this.darkLabel4.TabIndex = 5;
            this.darkLabel4.Text = "金额:";
            // 
            // tbGTS
            // 
            this.tbGTS.AutoSize = true;
            this.tbGTS.Location = new System.Drawing.Point(154, 19);
            this.tbGTS.Margin = new System.Windows.Forms.Padding(6);
            this.tbGTS.Name = "tbGTS";
            this.tbGTS.Size = new System.Drawing.Size(72, 28);
            this.tbGTS.SpecialBorderColor = null;
            this.tbGTS.SpecialFillColor = null;
            this.tbGTS.SpecialTextColor = null;
            this.tbGTS.TabIndex = 6;
            this.tbGTS.Text = "OXS";
            this.tbGTS.CheckedChanged += new System.EventHandler(this.rbGTC_CheckedChanged);
            // 
            // rbGTC
            // 
            this.rbGTC.AutoSize = true;
            this.rbGTC.Checked = true;
            this.rbGTC.Location = new System.Drawing.Point(22, 19);
            this.rbGTC.Margin = new System.Windows.Forms.Padding(6);
            this.rbGTC.Name = "rbGTC";
            this.rbGTC.Size = new System.Drawing.Size(74, 28);
            this.rbGTC.SpecialBorderColor = null;
            this.rbGTC.SpecialFillColor = null;
            this.rbGTC.SpecialTextColor = null;
            this.rbGTC.TabIndex = 7;
            this.rbGTC.TabStop = true;
            this.rbGTC.Text = "OXC";
            this.rbGTC.CheckedChanged += new System.EventHandler(this.rbGTC_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBox3.Location = new System.Drawing.Point(141, 112);
            this.textBox3.Margin = new System.Windows.Forms.Padding(6);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(217, 30);
            this.textBox3.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBox1.Location = new System.Drawing.Point(140, 182);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(656, 30);
            this.textBox1.TabIndex = 9;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBox2.Location = new System.Drawing.Point(553, 112);
            this.textBox2.Margin = new System.Windows.Forms.Padding(6);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(243, 30);
            this.textBox2.TabIndex = 10;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbGTS);
            this.panel1.Controls.Add(this.rbGTC);
            this.panel1.Location = new System.Drawing.Point(141, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 64);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbBlock);
            this.panel2.Controls.Add(this.rbTime);
            this.panel2.Location = new System.Drawing.Point(141, 301);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 64);
            this.panel2.TabIndex = 12;
            // 
            // rbBlock
            // 
            this.rbBlock.AutoSize = true;
            this.rbBlock.Location = new System.Drawing.Point(154, 19);
            this.rbBlock.Margin = new System.Windows.Forms.Padding(6);
            this.rbBlock.Name = "rbBlock";
            this.rbBlock.Size = new System.Drawing.Size(72, 28);
            this.rbBlock.SpecialBorderColor = null;
            this.rbBlock.SpecialFillColor = null;
            this.rbBlock.SpecialTextColor = null;
            this.rbBlock.TabIndex = 6;
            this.rbBlock.Text = "OXS";
            this.rbBlock.CheckedChanged += new System.EventHandler(this.rbTime_CheckedChanged);
            // 
            // rbTime
            // 
            this.rbTime.AutoSize = true;
            this.rbTime.Checked = true;
            this.rbTime.Location = new System.Drawing.Point(22, 19);
            this.rbTime.Margin = new System.Windows.Forms.Padding(6);
            this.rbTime.Name = "rbTime";
            this.rbTime.Size = new System.Drawing.Size(74, 28);
            this.rbTime.SpecialBorderColor = null;
            this.rbTime.SpecialFillColor = null;
            this.rbTime.SpecialTextColor = null;
            this.rbTime.TabIndex = 7;
            this.rbTime.TabStop = true;
            this.rbTime.Text = "OXC";
            this.rbTime.CheckedChanged += new System.EventHandler(this.rbTime_CheckedChanged);
            // 
            // darkLabel5
            // 
            this.darkLabel5.AutoSize = true;
            this.darkLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel5.Location = new System.Drawing.Point(42, 321);
            this.darkLabel5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel5.Name = "darkLabel5";
            this.darkLabel5.Size = new System.Drawing.Size(86, 24);
            this.darkLabel5.TabIndex = 13;
            this.darkLabel5.Text = "锁定类型:";
            // 
            // darkLabel6
            // 
            this.darkLabel6.AutoSize = true;
            this.darkLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel6.Location = new System.Drawing.Point(140, 228);
            this.darkLabel6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel6.Name = "darkLabel6";
            this.darkLabel6.Size = new System.Drawing.Size(0, 24);
            this.darkLabel6.TabIndex = 14;
            // 
            // tb_block
            // 
            this.tb_block.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_block.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_block.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_block.Location = new System.Drawing.Point(141, 434);
            this.tb_block.Margin = new System.Windows.Forms.Padding(6);
            this.tb_block.Name = "tb_block";
            this.tb_block.Size = new System.Drawing.Size(300, 30);
            this.tb_block.TabIndex = 16;
            this.tb_block.Visible = false;
            this.tb_block.TextChanged += new System.EventHandler(this.tb_block_TextChanged);
            // 
            // dtp_time
            // 
            this.dtp_time.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_time.CustomFormat = "yyyy/MM/dd  HH:mm:ss";
            this.dtp_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_time.Location = new System.Drawing.Point(141, 383);
            this.dtp_time.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dtp_time.Name = "dtp_time";
            this.dtp_time.Size = new System.Drawing.Size(300, 30);
            this.dtp_time.TabIndex = 17;
            // 
            // DialogLockTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 559);
            this.Controls.Add(this.dtp_time);
            this.Controls.Add(this.tb_block);
            this.Controls.Add(this.darkLabel6);
            this.Controls.Add(this.darkLabel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.darkLabel4);
            this.Controls.Add(this.darkLabel3);
            this.Controls.Add(this.darkLabel2);
            this.Controls.Add(this.darkLabel1);
            this.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.Name = "DialogLockTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "锁仓转账";
            this.Load += new System.EventHandler(this.PayToDialog_Load);
            this.Controls.SetChildIndex(this.darkLabel1, 0);
            this.Controls.SetChildIndex(this.darkLabel2, 0);
            this.Controls.SetChildIndex(this.darkLabel3, 0);
            this.Controls.SetChildIndex(this.darkLabel4, 0);
            this.Controls.SetChildIndex(this.textBox3, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.textBox2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.darkLabel5, 0);
            this.Controls.SetChildIndex(this.darkLabel6, 0);
            this.Controls.SetChildIndex(this.tb_block, 0);
            this.Controls.SetChildIndex(this.dtp_time, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel darkLabel1;
        private OX.Wallets.UI.Controls.DarkLabel darkLabel2;
        private OX.Wallets.UI.Controls.DarkLabel darkLabel3;
        private OX.Wallets.UI.Controls.DarkLabel darkLabel4;
        private OX.Wallets.UI.Controls.DarkRadioButton tbGTS;
        private OX.Wallets.UI.Controls.DarkRadioButton rbGTC;
        private OX.Wallets.UI.Controls.DarkTextBox textBox3;
        private OX.Wallets.UI.Controls.DarkTextBox textBox1;
        private OX.Wallets.UI.Controls.DarkTextBox textBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private UI.Controls.DarkRadioButton rbBlock;
        private UI.Controls.DarkRadioButton rbTime;
        private UI.Controls.DarkLabel darkLabel5;
        private UI.Controls.DarkLabel darkLabel6;
        private UI.Controls.DarkTextBox tb_block;
        private System.Windows.Forms.DateTimePicker dtp_time;
    }
}