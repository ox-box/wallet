namespace OX.Wallets.Base
{
    partial class DialogRegisterSemanticName
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
            tb_name = new UI.Controls.DarkTextBox();
            lb_name = new UI.Controls.DarkLabel();
            lb_address = new UI.Controls.DarkLabel();
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
            // tb_name
            // 
            tb_name.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_name.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_name.Location = new System.Drawing.Point(154, 112);
            tb_name.Margin = new System.Windows.Forms.Padding(6);
            tb_name.Name = "tb_name";
            tb_name.Size = new System.Drawing.Size(443, 30);
            tb_name.TabIndex = 17;
            tb_name.TextChanged += tbPublickey_TextChanged;
            // 
            // lb_name
            // 
            lb_name.AutoSize = true;
            lb_name.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_name.Location = new System.Drawing.Point(28, 119);
            lb_name.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_name.Name = "lb_name";
            lb_name.Size = new System.Drawing.Size(50, 24);
            lb_name.TabIndex = 15;
            lb_name.Text = "公钥:";
            // 
            // lb_address
            // 
            lb_address.AutoSize = true;
            lb_address.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_address.Location = new System.Drawing.Point(28, 48);
            lb_address.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lb_address.Name = "lb_address";
            lb_address.Size = new System.Drawing.Size(50, 24);
            lb_address.TabIndex = 14;
            lb_address.Text = "地址:";
            // 
            // DialogRegisterSemanticName
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(681, 279);
            Controls.Add(tb_name);
            Controls.Add(lb_name);
            Controls.Add(lb_address);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            Margin = new System.Windows.Forms.Padding(8);
            Name = "DialogRegisterSemanticName";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "查看私钥";
            Controls.SetChildIndex(lb_address, 0);
            Controls.SetChildIndex(lb_name, 0);
            Controls.SetChildIndex(tb_name, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OX.Wallets.UI.Controls.DarkTextBox tb_name;
        private OX.Wallets.UI.Controls.DarkLabel lb_name;
        private OX.Wallets.UI.Controls.DarkLabel lb_address;
    }
}