namespace OX.Wallets.Base
{
    partial class DialogViewPrivateAssets
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
            this.darkTreeView1 = new OX.Wallets.UI.Controls.DarkTreeView();
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
            // darkTreeView1
            // 
            this.darkTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkTreeView1.Location = new System.Drawing.Point(0, 0);
            this.darkTreeView1.MaxDragChange = 20;
            this.darkTreeView1.Name = "darkTreeView1";
            this.darkTreeView1.Size = new System.Drawing.Size(1168, 589);
            this.darkTreeView1.TabIndex = 2;
            this.darkTreeView1.Text = "darkTreeView1";
            // 
            // DialogViewPrivateAssets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 672);
            this.Controls.Add(this.darkTreeView1);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "DialogViewPrivateAssets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查看私钥";
            this.Controls.SetChildIndex(this.darkTreeView1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Controls.DarkTreeView darkTreeView1;
    }
}