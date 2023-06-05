namespace OX.Wallets.Base
{
    partial class DialogSingleContributeTo
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
            this.darkLabel2 = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkLabel4 = new OX.Wallets.UI.Controls.DarkLabel();
            this.textBox3 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.cbAmount = new OX.Wallets.UI.Controls.DarkComboBox();
            this.darkLabel1 = new OX.Wallets.UI.Controls.DarkLabel();
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
            // darkLabel2
            // 
            this.darkLabel2.AutoSize = true;
            this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel2.Location = new System.Drawing.Point(42, 120);
            this.darkLabel2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel2.Name = "darkLabel2";
            this.darkLabel2.Size = new System.Drawing.Size(50, 24);
            this.darkLabel2.TabIndex = 3;
            this.darkLabel2.Text = "余额:";
            // 
            // darkLabel4
            // 
            this.darkLabel4.AutoSize = true;
            this.darkLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel4.Location = new System.Drawing.Point(42, 204);
            this.darkLabel4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel4.Name = "darkLabel4";
            this.darkLabel4.Size = new System.Drawing.Size(50, 24);
            this.darkLabel4.TabIndex = 5;
            this.darkLabel4.Text = "金额:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBox3.Location = new System.Drawing.Point(168, 118);
            this.textBox3.Margin = new System.Windows.Forms.Padding(6);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(369, 30);
            this.textBox3.TabIndex = 8;
            // 
            // cbAmount
            // 
            this.cbAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAmount.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbAmount.FormattingEnabled = true;
            this.cbAmount.Location = new System.Drawing.Point(164, 201);
            this.cbAmount.Margin = new System.Windows.Forms.Padding(5);
            this.cbAmount.Name = "cbAmount";
            this.cbAmount.Size = new System.Drawing.Size(369, 31);
            this.cbAmount.SpecialBorderColor = null;
            this.cbAmount.SpecialFillColor = null;
            this.cbAmount.SpecialTextColor = null;
            this.cbAmount.TabIndex = 10;
            this.cbAmount.SelectedIndexChanged += new System.EventHandler(this.cbAmount_SelectedIndexChanged);
            // 
            // darkLabel1
            // 
            this.darkLabel1.AutoSize = true;
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(554, 204);
            this.darkLabel1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(39, 24);
            this.darkLabel1.TabIndex = 11;
            this.darkLabel1.Text = "oxc";
            // 
            // DialogSingleContributeTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 361);
            this.Controls.Add(this.darkLabel1);
            this.Controls.Add(this.cbAmount);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.darkLabel4);
            this.Controls.Add(this.darkLabel2);
            this.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.Name = "DialogSingleContributeTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "转账";
            this.Load += new System.EventHandler(this.PayToDialog_Load);
            this.Controls.SetChildIndex(this.darkLabel2, 0);
            this.Controls.SetChildIndex(this.darkLabel4, 0);
            this.Controls.SetChildIndex(this.textBox3, 0);
            this.Controls.SetChildIndex(this.cbAmount, 0);
            this.Controls.SetChildIndex(this.darkLabel1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OX.Wallets.UI.Controls.DarkLabel darkLabel2;
        private OX.Wallets.UI.Controls.DarkLabel darkLabel4;
        private OX.Wallets.UI.Controls.DarkTextBox textBox3;
        private UI.Controls.DarkComboBox cbAmount;
        private UI.Controls.DarkLabel darkLabel1;
    }
}