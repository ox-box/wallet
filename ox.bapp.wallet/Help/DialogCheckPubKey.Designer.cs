using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;

namespace OX.Wallets.Base
{
    partial class DialogCheckPubKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogCheckPubKey));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.bt_copy = new OX.Wallets.UI.Controls.DarkButton();
            this.tb_addr = new OX.Wallets.UI.Controls.DarkTextBox();
            this.tb_pubkey = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_addr = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_pubkey = new OX.Wallets.UI.Controls.DarkLabel();
            this.pnlMain.SuspendLayout();
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
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.bt_copy);
            this.pnlMain.Controls.Add(this.tb_addr);
            this.pnlMain.Controls.Add(this.tb_pubkey);
            this.pnlMain.Controls.Add(this.lb_addr);
            this.pnlMain.Controls.Add(this.lb_pubkey);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15, 15, 15, 5);
            this.pnlMain.Size = new System.Drawing.Size(1039, 189);
            this.pnlMain.TabIndex = 2;
            // 
            // bt_copy
            // 
            this.bt_copy.Location = new System.Drawing.Point(909, 104);
            this.bt_copy.Name = "bt_copy";
            this.bt_copy.Padding = new System.Windows.Forms.Padding(5);
            this.bt_copy.Size = new System.Drawing.Size(112, 34);
            this.bt_copy.SpecialBorderColor = null;
            this.bt_copy.SpecialFillColor = null;
            this.bt_copy.SpecialTextColor = null;
            this.bt_copy.TabIndex = 4;
            this.bt_copy.Text = "darkButton1";
            this.bt_copy.Click += new System.EventHandler(this.bt_copy_Click);
            // 
            // tb_addr
            // 
            this.tb_addr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_addr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_addr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_addr.Location = new System.Drawing.Point(192, 107);
            this.tb_addr.Name = "tb_addr";
            this.tb_addr.ReadOnly = true;
            this.tb_addr.Size = new System.Drawing.Size(691, 31);
            this.tb_addr.TabIndex = 3;
            // 
            // tb_pubkey
            // 
            this.tb_pubkey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_pubkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_pubkey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_pubkey.Location = new System.Drawing.Point(192, 42);
            this.tb_pubkey.Name = "tb_pubkey";
            this.tb_pubkey.Size = new System.Drawing.Size(829, 31);
            this.tb_pubkey.TabIndex = 2;
            this.tb_pubkey.TextChanged += new System.EventHandler(this.tb_pubkey_TextChanged);
            // 
            // lb_addr
            // 
            this.lb_addr.AutoSize = true;
            this.lb_addr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_addr.Location = new System.Drawing.Point(34, 113);
            this.lb_addr.Name = "lb_addr";
            this.lb_addr.Size = new System.Drawing.Size(98, 25);
            this.lb_addr.TabIndex = 1;
            this.lb_addr.Text = "darkLabel2";
            // 
            // lb_pubkey
            // 
            this.lb_pubkey.AutoSize = true;
            this.lb_pubkey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_pubkey.Location = new System.Drawing.Point(34, 44);
            this.lb_pubkey.Name = "lb_pubkey";
            this.lb_pubkey.Size = new System.Drawing.Size(98, 25);
            this.lb_pubkey.TabIndex = 0;
            this.lb_pubkey.Text = "darkLabel1";
            // 
            // DialogCheckPubKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 272);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogCheckPubKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogCheckPubKey";
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private DarkButton bt_copy;
        private DarkTextBox tb_addr;
        private DarkTextBox tb_pubkey;
        private DarkLabel lb_addr;
        private DarkLabel lb_pubkey;
    }
}