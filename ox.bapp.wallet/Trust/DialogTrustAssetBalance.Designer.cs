using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;

namespace OX.Wallets.Base
{
    partial class DialogTrustAssetBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogTrustAssetBalance));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.darkListView1 = new OX.Wallets.UI.Controls.DarkListView();
            this.pnlMain.SuspendLayout();
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
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.darkListView1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15, 15, 15, 5);
            this.pnlMain.Size = new System.Drawing.Size(932, 885);
            this.pnlMain.TabIndex = 2;
            // 
            // darkListView1
            // 
            this.darkListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkListView1.Location = new System.Drawing.Point(15, 15);
            this.darkListView1.Name = "darkListView1";
            this.darkListView1.Size = new System.Drawing.Size(902, 865);
            this.darkListView1.TabIndex = 0;
            this.darkListView1.Text = "darkListView1";
            // 
            // DialogTrustAssetBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 968);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogTrustAssetBalance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogTrustAssetBalance";
            this.Load += new System.EventHandler(this.DialogTrustAssetBalance_Load);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private DarkListView darkListView1;
    }
}