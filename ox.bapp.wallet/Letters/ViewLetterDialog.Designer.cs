namespace OX.Wallets.Base
{
    partial class ViewLetterDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewLetterDialog));
            this.panel = new System.Windows.Forms.Panel();
            this.tb_fromAddr = new OX.Wallets.UI.Controls.DarkTextBox();
            this.tb_fromPubkey = new OX.Wallets.UI.Controls.DarkTextBox();
            this.tb_to = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_from = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_msg = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_msg = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_to = new OX.Wallets.UI.Controls.DarkLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            // 
            // btnYes
            // 
            resources.ApplyResources(this.btnYes, "btnYes");
            // 
            // btnNo
            // 
            resources.ApplyResources(this.btnNo, "btnNo");
            // 
            // btnRetry
            // 
            resources.ApplyResources(this.btnRetry, "btnRetry");
            // 
            // btnIgnore
            // 
            resources.ApplyResources(this.btnIgnore, "btnIgnore");
            // 
            // panel
            // 
            this.panel.Controls.Add(this.tb_fromAddr);
            this.panel.Controls.Add(this.tb_fromPubkey);
            this.panel.Controls.Add(this.tb_to);
            this.panel.Controls.Add(this.lb_from);
            this.panel.Controls.Add(this.lb_msg);
            this.panel.Controls.Add(this.tb_msg);
            this.panel.Controls.Add(this.lb_to);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // tb_fromAddr
            // 
            this.tb_fromAddr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_fromAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_fromAddr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_fromAddr, "tb_fromAddr");
            this.tb_fromAddr.Name = "tb_fromAddr";
            this.tb_fromAddr.ReadOnly = true;
            // 
            // tb_fromPubkey
            // 
            this.tb_fromPubkey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_fromPubkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_fromPubkey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_fromPubkey, "tb_fromPubkey");
            this.tb_fromPubkey.Name = "tb_fromPubkey";
            this.tb_fromPubkey.ReadOnly = true;
            // 
            // tb_to
            // 
            this.tb_to.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_to.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_to.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_to, "tb_to");
            this.tb_to.Name = "tb_to";
            this.tb_to.ReadOnly = true;
            // 
            // lb_from
            // 
            resources.ApplyResources(this.lb_from, "lb_from");
            this.lb_from.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_from.Name = "lb_from";
            // 
            // lb_msg
            // 
            resources.ApplyResources(this.lb_msg, "lb_msg");
            this.lb_msg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_msg.Name = "lb_msg";
            // 
            // tb_msg
            // 
            this.tb_msg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_msg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_msg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_msg, "tb_msg");
            this.tb_msg.Name = "tb_msg";
            this.tb_msg.ReadOnly = true;
            // 
            // lb_to
            // 
            resources.ApplyResources(this.lb_to, "lb_to");
            this.lb_to.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_to.Name = "lb_to";
            // 
            // ViewLetterDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.DialogButtons = OX.Wallets.UI.Forms.DarkDialogButton.OkCancel;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewLetterDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClaimForm_FormClosing);
            this.Load += new System.EventHandler(this.ClaimForm_Load);
            this.Controls.SetChildIndex(this.panel, 0);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkLabel lb_to;
        private UI.Controls.DarkTextBox tb_msg;
        private UI.Controls.DarkLabel lb_msg;
        private UI.Controls.DarkLabel lb_from;
        private UI.Controls.DarkTextBox tb_to;
        private UI.Controls.DarkTextBox tb_fromAddr;
        private UI.Controls.DarkTextBox tb_fromPubkey;
    }
}