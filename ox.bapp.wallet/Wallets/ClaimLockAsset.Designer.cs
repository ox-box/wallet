namespace OX.Wallets.Base
{
    partial class ClaimLockAsset
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
            this.lb_progress = new OX.Wallets.UI.Controls.DarkLabel();
            this.Available = new OX.Wallets.UI.Controls.DarkLabel();
            this.Unavailable = new OX.Wallets.UI.Controls.DarkLabel();
            this.Available_v = new OX.Wallets.UI.Controls.DarkLabel();
            this.Unavailable_v = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            // lb_progress
            // 
            this.lb_progress.AutoSize = true;
            this.lb_progress.Font = new System.Drawing.Font("Microsoft YaHei UI", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb_progress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_progress.Location = new System.Drawing.Point(189, 9);
            this.lb_progress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_progress.Name = "lb_progress";
            this.lb_progress.Size = new System.Drawing.Size(0, 104);
            this.lb_progress.TabIndex = 3;
            // 
            // Available
            // 
            this.Available.AutoSize = true;
            this.Available.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Available.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Available.Location = new System.Drawing.Point(96, 155);
            this.Available.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Available.Name = "Available";
            this.Available.Size = new System.Drawing.Size(60, 24);
            this.Available.TabIndex = 10;
            this.Available.Text = "Asset:";
            // 
            // Unavailable
            // 
            this.Unavailable.AutoSize = true;
            this.Unavailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Unavailable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Unavailable.Location = new System.Drawing.Point(96, 201);
            this.Unavailable.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Unavailable.Name = "Unavailable";
            this.Unavailable.Size = new System.Drawing.Size(60, 24);
            this.Unavailable.TabIndex = 11;
            this.Unavailable.Text = "Asset:";
            // 
            // Available_v
            // 
            this.Available_v.AutoSize = true;
            this.Available_v.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Available_v.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Available_v.Location = new System.Drawing.Point(203, 155);
            this.Available_v.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Available_v.Name = "Available_v";
            this.Available_v.Size = new System.Drawing.Size(0, 24);
            this.Available_v.TabIndex = 12;
            // 
            // Unavailable_v
            // 
            this.Unavailable_v.AutoSize = true;
            this.Unavailable_v.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Unavailable_v.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Unavailable_v.Location = new System.Drawing.Point(203, 201);
            this.Unavailable_v.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Unavailable_v.Name = "Unavailable_v";
            this.Unavailable_v.Size = new System.Drawing.Size(0, 24);
            this.Unavailable_v.TabIndex = 13;
            // 
            // ClaimLockAsset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 436);
            this.Controls.Add(this.Unavailable_v);
            this.Controls.Add(this.Available_v);
            this.Controls.Add(this.Unavailable);
            this.Controls.Add(this.Available);
            this.Controls.Add(this.lb_progress);
            this.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.Name = "ClaimLockAsset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Defragment";
            this.Controls.SetChildIndex(this.lb_progress, 0);
            this.Controls.SetChildIndex(this.Available, 0);
            this.Controls.SetChildIndex(this.Unavailable, 0);
            this.Controls.SetChildIndex(this.Available_v, 0);
            this.Controls.SetChildIndex(this.Unavailable_v, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OX.Wallets.UI.Controls.DarkLabel lb_progress;
        private UI.Controls.DarkLabel Available;
        private UI.Controls.DarkLabel Unavailable;
        private UI.Controls.DarkLabel Available_v;
        private UI.Controls.DarkLabel Unavailable_v;
    }
}