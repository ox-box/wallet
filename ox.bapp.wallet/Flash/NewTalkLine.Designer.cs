namespace OX.Wallets.Flash
{
    partial class NewTalkLine
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
            tb_remote = new UI.Controls.DarkTextBox();
            cbAccounts = new UI.Controls.DarkComboBox();
            lb_local = new UI.Controls.DarkLabel();
            lb_remote = new UI.Controls.DarkLabel();
            tab_talkline = new UI.Controls.DarkTabControl();
            tp_uni = new System.Windows.Forms.TabPage();
            tb_name = new UI.Controls.DarkTextBox();
            lb_name = new UI.Controls.DarkLabel();
            tp_multi = new System.Windows.Forms.TabPage();
            bt_newKey = new UI.Controls.DarkButton();
            tb_name_2 = new UI.Controls.DarkTextBox();
            lb_name_2 = new UI.Controls.DarkLabel();
            cbAccounts_2 = new UI.Controls.DarkComboBox();
            tb_remote_2 = new UI.Controls.DarkTextBox();
            lb_remote_2 = new UI.Controls.DarkLabel();
            lb_local_2 = new UI.Controls.DarkLabel();
            tab_talkline.SuspendLayout();
            tp_uni.SuspendLayout();
            tp_multi.SuspendLayout();
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
            // tb_remote
            // 
            tb_remote.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_remote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_remote.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_remote.Location = new System.Drawing.Point(209, 117);
            tb_remote.Margin = new System.Windows.Forms.Padding(6);
            tb_remote.MaxLength = 256;
            tb_remote.Name = "tb_remote";
            tb_remote.Size = new System.Drawing.Size(770, 31);
            tb_remote.TabIndex = 37;
            tb_remote.TextChanged += tb_to_TextChanged;
            // 
            // cbAccounts
            // 
            cbAccounts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            cbAccounts.Location = new System.Drawing.Point(209, 57);
            cbAccounts.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            cbAccounts.Name = "cbAccounts";
            cbAccounts.Size = new System.Drawing.Size(770, 32);
            cbAccounts.SpecialBorderColor = null;
            cbAccounts.SpecialFillColor = null;
            cbAccounts.SpecialTextColor = null;
            cbAccounts.TabIndex = 36;
            // 
            // lb_local
            // 
            lb_local.AutoSize = true;
            lb_local.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_local.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_local.Location = new System.Drawing.Point(36, 60);
            lb_local.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_local.Name = "lb_local";
            lb_local.Size = new System.Drawing.Size(82, 25);
            lb_local.TabIndex = 35;
            lb_local.Text = "Claim to:";
            // 
            // lb_remote
            // 
            lb_remote.AutoSize = true;
            lb_remote.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_remote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_remote.Location = new System.Drawing.Point(36, 122);
            lb_remote.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_remote.Name = "lb_remote";
            lb_remote.Size = new System.Drawing.Size(82, 25);
            lb_remote.TabIndex = 34;
            lb_remote.Text = "Claim to:";
            // 
            // tab_talkline
            // 
            tab_talkline.AllowDrop = true;
            tab_talkline.Controls.Add(tp_uni);
            tab_talkline.Controls.Add(tp_multi);
            tab_talkline.DisableClose = false;
            tab_talkline.DisableDragging = false;
            tab_talkline.Dock = System.Windows.Forms.DockStyle.Fill;
            tab_talkline.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tab_talkline.Location = new System.Drawing.Point(0, 0);
            tab_talkline.Name = "tab_talkline";
            tab_talkline.Padding = new System.Drawing.Point(14, 4);
            tab_talkline.SelectedIndex = 0;
            tab_talkline.SelectedTabTextColor = System.Drawing.Color.Empty;
            tab_talkline.Size = new System.Drawing.Size(1142, 331);
            tab_talkline.TabIndex = 38;
            tab_talkline.SelectedIndexChanged += tab_talkline_SelectedIndexChanged;
            // 
            // tp_uni
            // 
            tp_uni.BackColor = System.Drawing.Color.DimGray;
            tp_uni.Controls.Add(tb_name);
            tp_uni.Controls.Add(lb_name);
            tp_uni.Controls.Add(cbAccounts);
            tp_uni.Controls.Add(tb_remote);
            tp_uni.Controls.Add(lb_remote);
            tp_uni.Controls.Add(lb_local);
            tp_uni.Location = new System.Drawing.Point(4, 32);
            tp_uni.Name = "tp_uni";
            tp_uni.Padding = new System.Windows.Forms.Padding(3);
            tp_uni.Size = new System.Drawing.Size(1134, 295);
            tp_uni.TabIndex = 0;
            tp_uni.Text = "tabPage1";
            tp_uni.Click += tp_uni_Click;
            // 
            // tb_name
            // 
            tb_name.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_name.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_name.Location = new System.Drawing.Point(209, 181);
            tb_name.Margin = new System.Windows.Forms.Padding(6);
            tb_name.MaxLength = 256;
            tb_name.Name = "tb_name";
            tb_name.Size = new System.Drawing.Size(770, 31);
            tb_name.TabIndex = 39;
            // 
            // lb_name
            // 
            lb_name.AutoSize = true;
            lb_name.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_name.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_name.Location = new System.Drawing.Point(36, 186);
            lb_name.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_name.Name = "lb_name";
            lb_name.Size = new System.Drawing.Size(82, 25);
            lb_name.TabIndex = 38;
            lb_name.Text = "Claim to:";
            // 
            // tp_multi
            // 
            tp_multi.BackColor = System.Drawing.Color.DimGray;
            tp_multi.Controls.Add(bt_newKey);
            tp_multi.Controls.Add(tb_name_2);
            tp_multi.Controls.Add(lb_name_2);
            tp_multi.Controls.Add(cbAccounts_2);
            tp_multi.Controls.Add(tb_remote_2);
            tp_multi.Controls.Add(lb_remote_2);
            tp_multi.Controls.Add(lb_local_2);
            tp_multi.Location = new System.Drawing.Point(4, 32);
            tp_multi.Name = "tp_multi";
            tp_multi.Padding = new System.Windows.Forms.Padding(3);
            tp_multi.Size = new System.Drawing.Size(1134, 295);
            tp_multi.TabIndex = 1;
            tp_multi.Text = "tabPage2";
            // 
            // bt_newKey
            // 
            bt_newKey.Location = new System.Drawing.Point(1002, 114);
            bt_newKey.Name = "bt_newKey";
            bt_newKey.Padding = new System.Windows.Forms.Padding(5);
            bt_newKey.Size = new System.Drawing.Size(108, 34);
            bt_newKey.SpecialBorderColor = null;
            bt_newKey.SpecialFillColor = null;
            bt_newKey.SpecialTextColor = null;
            bt_newKey.TabIndex = 46;
            bt_newKey.Text = "darkButton1";
            bt_newKey.Click += bt_newKey_Click;
            // 
            // tb_name_2
            // 
            tb_name_2.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_name_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_name_2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_name_2.Location = new System.Drawing.Point(209, 181);
            tb_name_2.Margin = new System.Windows.Forms.Padding(6);
            tb_name_2.MaxLength = 256;
            tb_name_2.Name = "tb_name_2";
            tb_name_2.Size = new System.Drawing.Size(770, 31);
            tb_name_2.TabIndex = 45;
            // 
            // lb_name_2
            // 
            lb_name_2.AutoSize = true;
            lb_name_2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_name_2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_name_2.Location = new System.Drawing.Point(36, 186);
            lb_name_2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_name_2.Name = "lb_name_2";
            lb_name_2.Size = new System.Drawing.Size(82, 25);
            lb_name_2.TabIndex = 44;
            lb_name_2.Text = "Claim to:";
            // 
            // cbAccounts_2
            // 
            cbAccounts_2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            cbAccounts_2.Location = new System.Drawing.Point(209, 57);
            cbAccounts_2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            cbAccounts_2.Name = "cbAccounts_2";
            cbAccounts_2.Size = new System.Drawing.Size(770, 32);
            cbAccounts_2.SpecialBorderColor = null;
            cbAccounts_2.SpecialFillColor = null;
            cbAccounts_2.SpecialTextColor = null;
            cbAccounts_2.TabIndex = 42;
            // 
            // tb_remote_2
            // 
            tb_remote_2.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_remote_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_remote_2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_remote_2.Location = new System.Drawing.Point(209, 117);
            tb_remote_2.Margin = new System.Windows.Forms.Padding(6);
            tb_remote_2.MaxLength = 256;
            tb_remote_2.Name = "tb_remote_2";
            tb_remote_2.Size = new System.Drawing.Size(770, 31);
            tb_remote_2.TabIndex = 43;
            // 
            // lb_remote_2
            // 
            lb_remote_2.AutoSize = true;
            lb_remote_2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_remote_2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_remote_2.Location = new System.Drawing.Point(36, 122);
            lb_remote_2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_remote_2.Name = "lb_remote_2";
            lb_remote_2.Size = new System.Drawing.Size(82, 25);
            lb_remote_2.TabIndex = 40;
            lb_remote_2.Text = "Claim to:";
            // 
            // lb_local_2
            // 
            lb_local_2.AutoSize = true;
            lb_local_2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_local_2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_local_2.Location = new System.Drawing.Point(36, 60);
            lb_local_2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_local_2.Name = "lb_local_2";
            lb_local_2.Size = new System.Drawing.Size(82, 25);
            lb_local_2.TabIndex = 41;
            lb_local_2.Text = "Claim to:";
            // 
            // NewTalkLine
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1142, 414);
            Controls.Add(tab_talkline);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            Name = "NewTalkLine";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "NewLetter";
            Load += NewLetter_Load;
            Controls.SetChildIndex(tab_talkline, 0);
            tab_talkline.ResumeLayout(false);
            tp_uni.ResumeLayout(false);
            tp_uni.PerformLayout();
            tp_multi.ResumeLayout(false);
            tp_multi.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private UI.Controls.DarkTextBox tb_remote;
        private UI.Controls.DarkComboBox cbAccounts;
        private UI.Controls.DarkLabel lb_local;
        private UI.Controls.DarkLabel lb_remote;
        private UI.Controls.DarkTabControl tab_talkline;
        private System.Windows.Forms.TabPage tp_uni;
        private System.Windows.Forms.TabPage tp_multi;
        private UI.Controls.DarkTextBox tb_name;
        private UI.Controls.DarkLabel lb_name;
        private UI.Controls.DarkTextBox tb_name_2;
        private UI.Controls.DarkLabel lb_name_2;
        private UI.Controls.DarkComboBox cbAccounts_2;
        private UI.Controls.DarkTextBox tb_remote_2;
        private UI.Controls.DarkLabel lb_remote_2;
        private UI.Controls.DarkLabel lb_local_2;
        private UI.Controls.DarkButton bt_newKey;
    }
}