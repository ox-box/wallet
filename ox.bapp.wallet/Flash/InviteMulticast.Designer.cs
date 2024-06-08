namespace OX.Wallets.Flash
{
    partial class InviteMulticast
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
            tb_pubkey = new UI.Controls.DarkTextBox();
            lb_linename = new UI.Controls.DarkLabel();
            lb_pubkey = new UI.Controls.DarkLabel();
            lv_pubs = new UI.Controls.DarkListView();
            bt_remove = new UI.Controls.DarkButton();
            bt_add = new UI.Controls.DarkButton();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
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
            // tb_pubkey
            // 
            tb_pubkey.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_pubkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_pubkey.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_pubkey.Location = new System.Drawing.Point(231, 67);
            tb_pubkey.Margin = new System.Windows.Forms.Padding(6);
            tb_pubkey.MaxLength = 256;
            tb_pubkey.Name = "tb_pubkey";
            tb_pubkey.Size = new System.Drawing.Size(728, 30);
            tb_pubkey.TabIndex = 39;
            // 
            // lb_linename
            // 
            lb_linename.AutoSize = true;
            lb_linename.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_linename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_linename.Location = new System.Drawing.Point(36, 20);
            lb_linename.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_linename.Name = "lb_linename";
            lb_linename.Size = new System.Drawing.Size(86, 24);
            lb_linename.TabIndex = 38;
            lb_linename.Text = "Claim to:";
            // 
            // lb_pubkey
            // 
            lb_pubkey.AutoSize = true;
            lb_pubkey.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_pubkey.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_pubkey.Location = new System.Drawing.Point(36, 73);
            lb_pubkey.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_pubkey.Name = "lb_pubkey";
            lb_pubkey.Size = new System.Drawing.Size(86, 24);
            lb_pubkey.TabIndex = 40;
            lb_pubkey.Text = "Claim to:";
            // 
            // lv_pubs
            // 
            lv_pubs.Location = new System.Drawing.Point(47, 166);
            lv_pubs.Name = "lv_pubs";
            lv_pubs.Size = new System.Drawing.Size(912, 747);
            lv_pubs.TabIndex = 41;
            lv_pubs.Text = "darkListView1";
            // 
            // bt_remove
            // 
            bt_remove.Location = new System.Drawing.Point(847, 117);
            bt_remove.Name = "bt_remove";
            bt_remove.Padding = new System.Windows.Forms.Padding(5);
            bt_remove.Size = new System.Drawing.Size(112, 34);
            bt_remove.SpecialBorderColor = null;
            bt_remove.SpecialFillColor = null;
            bt_remove.SpecialTextColor = null;
            bt_remove.TabIndex = 42;
            bt_remove.Text = "darkButton1";
            bt_remove.Click += bt_remove_Click;
            // 
            // bt_add
            // 
            bt_add.Location = new System.Drawing.Point(703, 117);
            bt_add.Name = "bt_add";
            bt_add.Padding = new System.Windows.Forms.Padding(5);
            bt_add.Size = new System.Drawing.Size(112, 34);
            bt_add.SpecialBorderColor = null;
            bt_add.SpecialFillColor = null;
            bt_add.SpecialTextColor = null;
            bt_add.TabIndex = 43;
            bt_add.Text = "darkButton1";
            bt_add.Click += bt_add_Click;
            // 
            // InviteMulticast
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1007, 1020);
            Controls.Add(bt_add);
            Controls.Add(bt_remove);
            Controls.Add(lv_pubs);
            Controls.Add(lb_pubkey);
            Controls.Add(tb_pubkey);
            Controls.Add(lb_linename);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            Name = "InviteMulticast";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "NewLetter";
            Load += InviteMulticast_Load;
            Controls.SetChildIndex(lb_linename, 0);
            Controls.SetChildIndex(tb_pubkey, 0);
            Controls.SetChildIndex(lb_pubkey, 0);
            Controls.SetChildIndex(lv_pubs, 0);
            Controls.SetChildIndex(bt_remove, 0);
            Controls.SetChildIndex(bt_add, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UI.Controls.DarkTextBox tb_pubkey;
        private UI.Controls.DarkLabel lb_linename;
        private UI.Controls.DarkLabel lb_pubkey;
        private UI.Controls.DarkListView lv_pubs;
        private UI.Controls.DarkButton bt_remove;
        private UI.Controls.DarkButton bt_add;
    }
}