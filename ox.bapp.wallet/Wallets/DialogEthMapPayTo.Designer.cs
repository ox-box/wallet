namespace OX.Wallets.Base
{
    partial class DialogEthMapPayTo
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
            lb_assetName = new UI.Controls.DarkLabel();
            lb_balance = new UI.Controls.DarkLabel();
            lb_to = new UI.Controls.DarkLabel();
            lb_amount = new UI.Controls.DarkLabel();
            textBox3 = new UI.Controls.DarkTextBox();
            textBox1 = new UI.Controls.DarkTextBox();
            textBox2 = new UI.Controls.DarkTextBox();
            lb_assetId = new UI.Controls.DarkLabel();
            lb_assetName_v = new UI.Controls.DarkLabel();
            lb_assetId_v = new UI.Controls.DarkLabel();
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
            // lb_assetName
            // 
            lb_assetName.AutoSize = true;
            lb_assetName.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_assetName.Location = new System.Drawing.Point(42, 27);
            lb_assetName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_assetName.Name = "lb_assetName";
            lb_assetName.Size = new System.Drawing.Size(50, 24);
            lb_assetName.TabIndex = 2;
            lb_assetName.Text = "资产:";
            // 
            // lb_balance
            // 
            lb_balance.AutoSize = true;
            lb_balance.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_balance.Location = new System.Drawing.Point(42, 120);
            lb_balance.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_balance.Name = "lb_balance";
            lb_balance.Size = new System.Drawing.Size(50, 24);
            lb_balance.TabIndex = 3;
            lb_balance.Text = "余额:";
            // 
            // lb_to
            // 
            lb_to.AutoSize = true;
            lb_to.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_to.Location = new System.Drawing.Point(42, 190);
            lb_to.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_to.Name = "lb_to";
            lb_to.Size = new System.Drawing.Size(50, 24);
            lb_to.TabIndex = 4;
            lb_to.Text = "地址:";
            // 
            // lb_amount
            // 
            lb_amount.AutoSize = true;
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_amount.Location = new System.Drawing.Point(42, 260);
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
            textBox3.Location = new System.Drawing.Point(130, 112);
            textBox3.Margin = new System.Windows.Forms.Padding(6);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new System.Drawing.Size(592, 30);
            textBox3.TabIndex = 8;
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox1.Location = new System.Drawing.Point(130, 182);
            textBox1.Margin = new System.Windows.Forms.Padding(6);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(592, 30);
            textBox1.TabIndex = 9;
            textBox1.TextChanged += textBox_TextChanged;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox2.Location = new System.Drawing.Point(130, 252);
            textBox2.Margin = new System.Windows.Forms.Padding(6);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(592, 30);
            textBox2.TabIndex = 10;
            textBox2.TextChanged += textBox_TextChanged;
            // 
            // lb_assetId
            // 
            lb_assetId.AutoSize = true;
            lb_assetId.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_assetId.Location = new System.Drawing.Point(42, 71);
            lb_assetId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_assetId.Name = "lb_assetId";
            lb_assetId.Size = new System.Drawing.Size(50, 24);
            lb_assetId.TabIndex = 11;
            lb_assetId.Text = "资产:";
            // 
            // lb_assetName_v
            // 
            lb_assetName_v.AutoSize = true;
            lb_assetName_v.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_assetName_v.Location = new System.Drawing.Point(130, 27);
            lb_assetName_v.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_assetName_v.Name = "lb_assetName_v";
            lb_assetName_v.Size = new System.Drawing.Size(50, 24);
            lb_assetName_v.TabIndex = 12;
            lb_assetName_v.Text = "资产:";
            // 
            // lb_assetId_v
            // 
            lb_assetId_v.AutoSize = true;
            lb_assetId_v.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_assetId_v.Location = new System.Drawing.Point(130, 71);
            lb_assetId_v.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_assetId_v.Name = "lb_assetId_v";
            lb_assetId_v.Size = new System.Drawing.Size(50, 24);
            lb_assetId_v.TabIndex = 13;
            lb_assetId_v.Text = "资产:";
            // 
            // DialogEthMapPayTo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(740, 422);
            Controls.Add(lb_assetId_v);
            Controls.Add(lb_assetName_v);
            Controls.Add(lb_assetId);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(textBox3);
            Controls.Add(lb_amount);
            Controls.Add(lb_to);
            Controls.Add(lb_balance);
            Controls.Add(lb_assetName);
            Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            Name = "DialogEthMapPayTo";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "转账";
            Load += PayToDialog_Load;
            Controls.SetChildIndex(lb_assetName, 0);
            Controls.SetChildIndex(lb_balance, 0);
            Controls.SetChildIndex(lb_to, 0);
            Controls.SetChildIndex(lb_amount, 0);
            Controls.SetChildIndex(textBox3, 0);
            Controls.SetChildIndex(textBox1, 0);
            Controls.SetChildIndex(textBox2, 0);
            Controls.SetChildIndex(lb_assetId, 0);
            Controls.SetChildIndex(lb_assetName_v, 0);
            Controls.SetChildIndex(lb_assetId_v, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel lb_assetName;
        private OX.Wallets.UI.Controls.DarkLabel lb_balance;
        private OX.Wallets.UI.Controls.DarkLabel lb_to;
        private OX.Wallets.UI.Controls.DarkLabel lb_amount;
        private OX.Wallets.UI.Controls.DarkTextBox textBox3;
        private OX.Wallets.UI.Controls.DarkTextBox textBox1;
        private OX.Wallets.UI.Controls.DarkTextBox textBox2;
        private UI.Controls.DarkLabel lb_assetId;
        private UI.Controls.DarkLabel lb_assetName_v;
        private UI.Controls.DarkLabel lb_assetId_v;
    }
}