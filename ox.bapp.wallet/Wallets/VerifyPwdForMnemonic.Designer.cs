
namespace OX.Wallets.Base
{
    partial class VerifyPwdForMnemonic
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
            this.tb_pwd = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_pwd = new OX.Wallets.UI.Controls.DarkLabel();
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
            // tb_pwd
            // 
            this.tb_pwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_pwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_pwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_pwd.Location = new System.Drawing.Point(128, 60);
            this.tb_pwd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tb_pwd.Name = "tb_pwd";
            this.tb_pwd.PasswordChar = '*';
            this.tb_pwd.Size = new System.Drawing.Size(506, 30);
            this.tb_pwd.TabIndex = 6;
            // 
            // lb_pwd
            // 
            this.lb_pwd.AutoSize = true;
            this.lb_pwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_pwd.Location = new System.Drawing.Point(18, 64);
            this.lb_pwd.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lb_pwd.Name = "lb_pwd";
            this.lb_pwd.Size = new System.Drawing.Size(50, 24);
            this.lb_pwd.TabIndex = 5;
            this.lb_pwd.Text = "地址:";
            // 
            // VerifyPwdForMnemonic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 209);
            this.Controls.Add(this.tb_pwd);
            this.Controls.Add(this.lb_pwd);
            this.Name = "VerifyPwdForMnemonic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VerifyPwdForMnemonic";
            this.Controls.SetChildIndex(this.lb_pwd, 0);
            this.Controls.SetChildIndex(this.tb_pwd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Controls.DarkTextBox tb_pwd;
        private UI.Controls.DarkLabel lb_pwd;
    }
}