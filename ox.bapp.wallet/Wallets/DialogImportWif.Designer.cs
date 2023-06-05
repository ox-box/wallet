namespace OX.Wallets.Base
{
    partial class DialogImportWif
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
            this.tbWif = new OX.Wallets.UI.Controls.DarkTextBox();
            this.darkLabel1 = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // tbWif
            // 
            this.tbWif.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tbWif.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWif.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tbWif.Location = new System.Drawing.Point(119, 54);
            this.tbWif.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbWif.Name = "tbWif";
            this.tbWif.Size = new System.Drawing.Size(680, 30);
            this.tbWif.TabIndex = 10;
            this.tbWif.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // darkLabel1
            // 
            this.darkLabel1.AutoSize = true;
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(44, 60);
            this.darkLabel1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(50, 24);
            this.darkLabel1.TabIndex = 9;
            this.darkLabel1.Text = "私钥:";
            this.darkLabel1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // DialogImportWif
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 228);
            this.Controls.Add(this.tbWif);
            this.Controls.Add(this.darkLabel1);
            this.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.Name = "DialogImportWif";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "导入私钥";
            this.Controls.SetChildIndex(this.darkLabel1, 0);
            this.Controls.SetChildIndex(this.tbWif, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OX.Wallets.UI.Controls.DarkTextBox tbWif;
        private OX.Wallets.UI.Controls.DarkLabel darkLabel1;
    }
}