
namespace OX.Wallets.Base
{
    partial class FindTransactionForm
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
            this.lb_txid = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_txid = new OX.Wallets.UI.Controls.DarkTextBox();
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
            // lb_txid
            // 
            this.lb_txid.AutoSize = true;
            this.lb_txid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_txid.Location = new System.Drawing.Point(28, 22);
            this.lb_txid.Name = "lb_txid";
            this.lb_txid.Size = new System.Drawing.Size(106, 24);
            this.lb_txid.TabIndex = 2;
            this.lb_txid.Text = "darkLabel1";
            // 
            // tb_txid
            // 
            this.tb_txid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_txid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_txid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_txid.Location = new System.Drawing.Point(28, 70);
            this.tb_txid.Name = "tb_txid";
            this.tb_txid.Size = new System.Drawing.Size(754, 30);
            this.tb_txid.TabIndex = 3;
            // 
            // FindTransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 205);
            this.Controls.Add(this.tb_txid);
            this.Controls.Add(this.lb_txid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindTransactionForm";
            this.Text = "FindTransactionForm";
            this.Controls.SetChildIndex(this.lb_txid, 0);
            this.Controls.SetChildIndex(this.tb_txid, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Controls.DarkLabel lb_txid;
        private UI.Controls.DarkTextBox tb_txid;
    }
}