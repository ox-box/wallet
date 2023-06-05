namespace OX.Notecase
{
    partial class MnemonicsWallet
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
            this.bt_next = new OX.Wallets.UI.Controls.DarkButton();
            this.RoundPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lb_warn = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_notify = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_notify2 = new OX.Wallets.UI.Controls.DarkLabel();
            this.bt_ok = new OX.Wallets.UI.Controls.DarkButton();
            this.lb_input = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_input = new OX.Wallets.UI.Controls.DarkTextBox();
            this.bt_input = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_reduce = new OX.Wallets.UI.Controls.DarkButton();
            this.lb_reinput = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // bt_next
            // 
            this.bt_next.Location = new System.Drawing.Point(783, 386);
            this.bt_next.Name = "bt_next";
            this.bt_next.Padding = new System.Windows.Forms.Padding(5);
            this.bt_next.Size = new System.Drawing.Size(149, 23);
            this.bt_next.TabIndex = 9;
            this.bt_next.Text = "button1";
            this.bt_next.Click += new System.EventHandler(this.bt_next_Click);
            // 
            // RoundPanel
            // 
            this.RoundPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoundPanel.AutoScroll = true;
            this.RoundPanel.Location = new System.Drawing.Point(12, 145);
            this.RoundPanel.Name = "RoundPanel";
            this.RoundPanel.Size = new System.Drawing.Size(1106, 173);
            this.RoundPanel.TabIndex = 0;
            // 
            // lb_warn
            // 
            this.lb_warn.AutoSize = true;
            this.lb_warn.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb_warn.ForeColor = System.Drawing.Color.Red;
            this.lb_warn.Location = new System.Drawing.Point(12, 29);
            this.lb_warn.Name = "lb_warn";
            this.lb_warn.Size = new System.Drawing.Size(83, 27);
            this.lb_warn.TabIndex = 8;
            this.lb_warn.Text = "roomid";
            // 
            // lb_notify
            // 
            this.lb_notify.AutoSize = true;
            this.lb_notify.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb_notify.ForeColor = System.Drawing.Color.Coral;
            this.lb_notify.Location = new System.Drawing.Point(12, 78);
            this.lb_notify.Name = "lb_notify";
            this.lb_notify.Size = new System.Drawing.Size(51, 17);
            this.lb_notify.TabIndex = 8;
            this.lb_notify.Text = "roomid";
            // 
            // lb_notify2
            // 
            this.lb_notify2.AutoSize = true;
            this.lb_notify2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb_notify2.ForeColor = System.Drawing.Color.Coral;
            this.lb_notify2.Location = new System.Drawing.Point(12, 104);
            this.lb_notify2.Name = "lb_notify2";
            this.lb_notify2.Size = new System.Drawing.Size(51, 17);
            this.lb_notify2.TabIndex = 8;
            this.lb_notify2.Text = "roomid";
            // 
            // bt_ok
            // 
            this.bt_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_ok.Location = new System.Drawing.Point(964, 386);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Padding = new System.Windows.Forms.Padding(5);
            this.bt_ok.Size = new System.Drawing.Size(149, 23);
            this.bt_ok.TabIndex = 9;
            this.bt_ok.Text = "button1";
            this.bt_ok.Visible = false;
            // 
            // lb_input
            // 
            this.lb_input.AutoSize = true;
            this.lb_input.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_input.Location = new System.Drawing.Point(12, 389);
            this.lb_input.Name = "lb_input";
            this.lb_input.Size = new System.Drawing.Size(51, 17);
            this.lb_input.TabIndex = 8;
            this.lb_input.Text = "roomid";
            this.lb_input.Visible = false;
            // 
            // tb_input
            // 
            this.tb_input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_input.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_input.Location = new System.Drawing.Point(90, 387);
            this.tb_input.Name = "tb_input";
            this.tb_input.Size = new System.Drawing.Size(175, 23);
            this.tb_input.TabIndex = 1;
            this.tb_input.Visible = false;
            this.tb_input.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // bt_input
            // 
            this.bt_input.Location = new System.Drawing.Point(283, 386);
            this.bt_input.Name = "bt_input";
            this.bt_input.Padding = new System.Windows.Forms.Padding(5);
            this.bt_input.Size = new System.Drawing.Size(92, 23);
            this.bt_input.TabIndex = 9;
            this.bt_input.Text = "button1";
            this.bt_input.Visible = false;
            this.bt_input.Click += new System.EventHandler(this.bt_input_Click);
            // 
            // bt_reduce
            // 
            this.bt_reduce.Location = new System.Drawing.Point(415, 386);
            this.bt_reduce.Name = "bt_reduce";
            this.bt_reduce.Padding = new System.Windows.Forms.Padding(5);
            this.bt_reduce.Size = new System.Drawing.Size(92, 23);
            this.bt_reduce.TabIndex = 9;
            this.bt_reduce.Text = "button1";
            this.bt_reduce.Visible = false;
            this.bt_reduce.Click += new System.EventHandler(this.bt_reduce_Click);
            // 
            // lb_reinput
            // 
            this.lb_reinput.AutoSize = true;
            this.lb_reinput.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb_reinput.ForeColor = System.Drawing.Color.Coral;
            this.lb_reinput.Location = new System.Drawing.Point(12, 345);
            this.lb_reinput.Name = "lb_reinput";
            this.lb_reinput.Size = new System.Drawing.Size(51, 17);
            this.lb_reinput.TabIndex = 8;
            this.lb_reinput.Text = "roomid";
            this.lb_reinput.Visible = false;
            // 
            // NmenonicsWallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 428);
            this.Controls.Add(this.lb_reinput);
            this.Controls.Add(this.bt_reduce);
            this.Controls.Add(this.bt_input);
            this.Controls.Add(this.tb_input);
            this.Controls.Add(this.lb_input);
            this.Controls.Add(this.bt_ok);
            this.Controls.Add(this.lb_notify2);
            this.Controls.Add(this.lb_notify);
            this.Controls.Add(this.lb_warn);
            this.Controls.Add(this.bt_next);
            this.Controls.Add(this.RoundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NmenonicsWallet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.NmenonicsWallet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.FlowLayoutPanel RoundPanel;
        private OX.Wallets.UI.Controls.DarkButton bt_next;
        private OX.Wallets.UI.Controls.DarkLabel lb_warn;
        private OX.Wallets.UI.Controls.DarkLabel lb_notify;
        private OX.Wallets.UI.Controls.DarkLabel lb_notify2;
        private OX.Wallets.UI.Controls.DarkButton bt_ok;
        private OX.Wallets.UI.Controls.DarkLabel lb_input;
        private OX.Wallets.UI.Controls.DarkTextBox tb_input;
        private OX.Wallets.UI.Controls.DarkButton bt_input;
        private OX.Wallets.UI.Controls.DarkButton bt_reduce;
        private OX.Wallets.UI.Controls.DarkLabel lb_reinput;
    }
}