using OX.Wallets.UI.Config;
using OX.Wallets.UI.Docking;

namespace OX.Wallets.Base
{
    partial class OXTokens
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
            this.treeAsset = new OX.Wallets.UI.Controls.DarkTreeView();
            this.SuspendLayout();
            // 
            // treeAsset
            // 
            this.treeAsset.AllowMoveNodes = true;
            this.treeAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeAsset.Location = new System.Drawing.Point(0, 25);
            this.treeAsset.MaxDragChange = 20;
            this.treeAsset.MultiSelect = true;
            this.treeAsset.Name = "treeAsset";
            this.treeAsset.ShowIcons = true;
            this.treeAsset.Size = new System.Drawing.Size(280, 425);
            this.treeAsset.TabIndex = 1;
            this.treeAsset.Text = "darkTreeView1";
            // 
            // DockAsset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeAsset);
            this.DefaultDockArea = OX.Wallets.UI.Docking.DarkDockArea.Right;
            this.DockText = UIHelper.LocalString("私营资产详情", "Private Asset Details");
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.Icon = global::OX.GT.Icons.properties_16xLG;
            this.Name = "PrivateAssetList";
            this.SerializationKey = "PrivateAssetList";
            this.Size = new System.Drawing.Size(280, 450);
            this.ResumeLayout(false);

        }

        #endregion

        private OX.Wallets.UI.Controls.DarkTreeView treeAsset;

    }
}
