namespace OX.Notecase
{
    partial class ImportPrivateKey
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
            this.bt_ok = new OX.Wallets.UI.Controls.DarkButton();
            this.tb_privateKey = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_import = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // bt_ok
            // 
            this.bt_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_ok.Location = new System.Drawing.Point(401, 90);
            this.bt_ok.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.bt_ok.Size = new System.Drawing.Size(200, 32);
            this.bt_ok.SpecialBorderColor = null;
            this.bt_ok.SpecialFillColor = null;
            this.bt_ok.SpecialTextColor = null;
            this.bt_ok.TabIndex = 9;
            this.bt_ok.Text = "button1";
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // tb_privateKey
            // 
            this.tb_privateKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_privateKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_privateKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_privateKey.Location = new System.Drawing.Point(164, 34);
            this.tb_privateKey.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tb_privateKey.Name = "tb_privateKey";
            this.tb_privateKey.Size = new System.Drawing.Size(437, 30);
            this.tb_privateKey.TabIndex = 1;
            this.tb_privateKey.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // lb_import
            // 
            this.lb_import.AutoSize = true;
            this.lb_import.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_import.Location = new System.Drawing.Point(32, 39);
            this.lb_import.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lb_import.Name = "lb_import";
            this.lb_import.Size = new System.Drawing.Size(104, 24);
            this.lb_import.TabIndex = 0;
            this.lb_import.Text = "Wallet File:";
            // 
            // ImportPrivateKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 188);
            this.Controls.Add(this.lb_import);
            this.Controls.Add(this.tb_privateKey);
            this.Controls.Add(this.bt_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportPrivateKey";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OX.Wallets.UI.Controls.DarkButton bt_ok;
        private OX.Wallets.UI.Controls.DarkTextBox tb_privateKey;
        private OX.Wallets.UI.Controls.DarkLabel lb_import;
    }
}