using OX.Wallets.UI.Config;
using OX.Wallets.UI.Docking;

namespace OX.Wallets.Flash
{
    partial class MyTalks
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
            treeLines = new UI.Controls.DarkTreeView();
            bt_Fresh = new UI.Controls.DarkButton();
            SuspendLayout();
            // 
            // treeAsset
            // 
            treeLines.AllowMoveNodes = true;
            treeLines.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            treeLines.Location = new System.Drawing.Point(0, 77);
            treeLines.MaxDragChange = 20;
            treeLines.MultiSelect = true;
            treeLines.Name = "treeAsset";
            treeLines.ShowIcons = true;
            treeLines.Size = new System.Drawing.Size(400, 552);
            treeLines.TabIndex = 1;
            treeLines.Text = "darkTreeView1";
            // 
            // bt_Fresh
            // 
            bt_Fresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            bt_Fresh.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            bt_Fresh.Location = new System.Drawing.Point(16, 35);
            bt_Fresh.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            bt_Fresh.Name = "bt_Fresh";
            bt_Fresh.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            bt_Fresh.Size = new System.Drawing.Size(366, 32);
            bt_Fresh.SpecialBorderColor = null;
            bt_Fresh.SpecialFillColor = null;
            bt_Fresh.SpecialTextColor = null;
            bt_Fresh.TabIndex = 5;
            bt_Fresh.Click += bt_Fresh_Click;
            // 
            // AccountAsset
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(bt_Fresh);
            Controls.Add(treeLines);
            DefaultDockArea = DarkDockArea.Right;
            DockText = "账户资产";
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "AccountAsset";
            SerializationKey = "DockLockAsset";
            Size = new System.Drawing.Size(400, 632);
            ResumeLayout(false);
        }

        #endregion

        private OX.Wallets.UI.Controls.DarkTreeView treeLines;
        private UI.Controls.DarkButton bt_Fresh;
    }
}
