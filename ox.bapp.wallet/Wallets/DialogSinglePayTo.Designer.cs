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
            lb_address = new UI.Controls.DarkLabel();
            lb_amount = new UI.Controls.DarkLabel();
            textBox3 = new UI.Controls.DarkTextBox();
            textBox1 = new UI.Controls.DarkTextBox();
            textBox2 = new UI.Controls.DarkTextBox();
            cb_assets = new UI.Controls.DarkComboBox();
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
            lb_balance.Location = new System.Drawing.Point(630, 192);
            lb_balance.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_balance.Name = "lb_balance";
            lb_balance.Size = new System.Drawing.Size(50, 24);
            lb_balance.TabIndex = 3;
            lb_balance.Text = "余额:";
            // 
            // lb_address
            // 
            lb_address.AutoSize = true;
            lb_address.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_address.Location = new System.Drawing.Point(42, 126);
            lb_address.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_address.Name = "lb_address";
            lb_address.Size = new System.Drawing.Size(50, 24);
            lb_address.TabIndex = 4;
            lb_address.Text = "地址:";
            // 
            // lb_amount
            // 
            lb_amount.AutoSize = true;
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_amount.Location = new System.Drawing.Point(42, 196);
            lb_amount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_amount.Name = "lb_amount";
            lb_amount.Size = new System.Drawing.Size(50, 24);
            lb_amount.TabIndex = 5;
            lb_amount.Text = "金额:";
            // 
            // textBox3
            // 
            textBox3.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox3.Location = new System.Drawing.Point(717, 190);
            textBox3.Margin = new System.Windows.Forms.Padding(6);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new System.Drawing.Size(320, 30);
            textBox3.TabIndex = 8;
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox1.Location = new System.Drawing.Point(130, 121);
            textBox1.Margin = new System.Windows.Forms.Padding(6);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(907, 30);
            textBox1.TabIndex = 9;
            textBox1.TextChanged += textBox_TextChanged;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox2.Location = new System.Drawing.Point(130, 191);
            textBox2.Margin = new System.Windows.Forms.Padding(6);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(320, 30);
            textBox2.TabIndex = 10;
            textBox2.TextChanged += textBox_TextChanged;
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
            // DialogSinglePayTo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1049, 339);
            Controls.Add(cb_assets);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(textBox3);
            Controls.Add(lb_amount);
            Controls.Add(lb_address);
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
            Controls.SetChildIndex(lb_address, 0);
            Controls.SetChildIndex(lb_amount, 0);
            Controls.SetChildIndex(textBox3, 0);
            Controls.SetChildIndex(textBox1, 0);
            Controls.SetChildIndex(textBox2, 0);
            Controls.SetChildIndex(cb_assets, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel lb_asset;
        private OX.Wallets.UI.Controls.DarkLabel lb_balance;
        private OX.Wallets.UI.Controls.DarkLabel lb_address;
        private OX.Wallets.UI.Controls.DarkLabel lb_amount;
        private OX.Wallets.UI.Controls.DarkTextBox textBox3;
        private OX.Wallets.UI.Controls.DarkTextBox textBox1;
        private OX.Wallets.UI.Controls.DarkTextBox textBox2;
        private UI.Controls.DarkComboBox cb_assets;
    }
}