using OX.Wallets.UI.Controls;

namespace OX.Wallets.UI.Forms
{
    partial class DarkMessageBox
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
            picIcon = new System.Windows.Forms.PictureBox();
            lblText = new DarkLabel();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(18, 18);
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
            // picIcon
            // 
            picIcon.Location = new System.Drawing.Point(10, 10);
            picIcon.Name = "picIcon";
            picIcon.Size = new System.Drawing.Size(32, 32);
            picIcon.TabIndex = 3;
            picIcon.TabStop = false;
            // 
            // lblText
            // 
            lblText.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lblText.Location = new System.Drawing.Point(71, 9);
            lblText.Name = "lblText";
            lblText.Size = new System.Drawing.Size(185, 34);
            lblText.TabIndex = 4;
            lblText.Text = "Something something something";
            // 
            // DarkMessageBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1230, 423);
            Controls.Add(picIcon);
            Controls.Add(lblText);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DarkMessageBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Message box";
            Controls.SetChildIndex(lblText, 0);
            Controls.SetChildIndex(picIcon, 0);
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox picIcon;
        private DarkLabel lblText;
    }
}