namespace OX.Wallets.Flash.State
{
    partial class NewFlashStateComment
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
            cbAccounts = new UI.Controls.DarkComboBox();
            lb_from = new UI.Controls.DarkLabel();
            tb_comment = new UI.Controls.DarkTextBox();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            btnOk.Click += btnOk_Click;
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
            // cbAccounts
            // 
            cbAccounts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            cbAccounts.Location = new System.Drawing.Point(196, 22);
            cbAccounts.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            cbAccounts.Name = "cbAccounts";
            cbAccounts.Size = new System.Drawing.Size(589, 31);
            cbAccounts.SpecialBorderColor = null;
            cbAccounts.SpecialFillColor = null;
            cbAccounts.SpecialTextColor = null;
            cbAccounts.TabIndex = 36;
            cbAccounts.SelectedIndexChanged += cbAccounts_SelectedIndexChanged;
            // 
            // lb_from
            // 
            lb_from.AutoSize = true;
            lb_from.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_from.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_from.Location = new System.Drawing.Point(33, 25);
            lb_from.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_from.Name = "lb_from";
            lb_from.Size = new System.Drawing.Size(86, 24);
            lb_from.TabIndex = 35;
            lb_from.Text = "Claim to:";
            // 
            // tb_comment
            // 
            tb_comment.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_comment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_comment.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_comment.Location = new System.Drawing.Point(33, 84);
            tb_comment.MaxLength = 300;
            tb_comment.Multiline = true;
            tb_comment.Name = "tb_comment";
            tb_comment.Size = new System.Drawing.Size(752, 203);
            tb_comment.TabIndex = 37;
            // 
            // NewFlashStateComment
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(827, 384);
            Controls.Add(tb_comment);
            Controls.Add(cbAccounts);
            Controls.Add(lb_from);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            Name = "NewFlashStateComment";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "NewLetter";
            Load += NewLetter_Load;
            Controls.SetChildIndex(lb_from, 0);
            Controls.SetChildIndex(cbAccounts, 0);
            Controls.SetChildIndex(tb_comment, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UI.Controls.DarkComboBox cbAccounts;
        private UI.Controls.DarkLabel lb_from;
        private UI.Controls.DarkTextBox tb_comment;
    }
}