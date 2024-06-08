namespace OX.Wallets.Flash
{
    partial class EditTalkLineName
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
            tb_linename = new UI.Controls.DarkTextBox();
            lb_linename = new UI.Controls.DarkLabel();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(166, 18);
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
            // tb_linename
            // 
            tb_linename.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_linename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_linename.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_linename.Location = new System.Drawing.Point(230, 47);
            tb_linename.Margin = new System.Windows.Forms.Padding(6);
            tb_linename.MaxLength = 256;
            tb_linename.Name = "tb_linename";
            tb_linename.Size = new System.Drawing.Size(336, 30);
            tb_linename.TabIndex = 39;
            // 
            // lb_linename
            // 
            lb_linename.AutoSize = true;
            lb_linename.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_linename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_linename.Location = new System.Drawing.Point(57, 52);
            lb_linename.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_linename.Name = "lb_linename";
            lb_linename.Size = new System.Drawing.Size(86, 24);
            lb_linename.TabIndex = 38;
            lb_linename.Text = "Claim to:";
            // 
            // EditTalkLineName
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(616, 208);
            Controls.Add(tb_linename);
            Controls.Add(lb_linename);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            Name = "EditTalkLineName";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "NewLetter";
            Load += EditTalkLineName_Load;
            Controls.SetChildIndex(lb_linename, 0);
            Controls.SetChildIndex(tb_linename, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UI.Controls.DarkTextBox tb_linename;
        private UI.Controls.DarkLabel lb_linename;
    }
}