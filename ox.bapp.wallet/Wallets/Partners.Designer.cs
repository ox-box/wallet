using OX.Wallets.UI.Config;
using OX.Wallets.UI.Docking;

namespace OX.Wallets.Base
{
    partial class Partners
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treePartners = new OX.Wallets.UI.Controls.DarkTreeView();
            this.SuspendLayout();
            // 
            // treeAsset
            // 
            this.treePartners.AllowMoveNodes = true;
            this.treePartners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePartners.Location = new System.Drawing.Point(0, 25);
            this.treePartners.MaxDragChange = 20;
            this.treePartners.MultiSelect = true;
            this.treePartners.Name = "treeAsset";
            this.treePartners.ShowIcons = true;
            this.treePartners.Size = new System.Drawing.Size(280, 425);
            this.treePartners.TabIndex = 1;
            this.treePartners.Text = "darkTreeView1";
            // 
            // DockAsset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treePartners);
            this.DefaultDockArea = OX.Wallets.UI.Docking.DarkDockArea.Right;
            this.DockText = UIHelper.LocalString("交易伙伴", "Transfer Partner");
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.Icon = global::OX.GT.Icons.properties_16xLG;
            this.Name = "Partners";
            this.SerializationKey = "Partners";
            this.Size = new System.Drawing.Size(280, 450);
            this.ResumeLayout(false);

        }

        #endregion

        private OX.Wallets.UI.Controls.DarkTreeView treePartners;

    }
}
