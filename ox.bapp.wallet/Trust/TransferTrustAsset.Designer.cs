namespace OX.Wallets.Base
{
    partial class TransferTrustAsset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferTrustAsset));
            this.panel = new System.Windows.Forms.Panel();
            this.cbAssets = new OX.Wallets.UI.Controls.DarkComboBox();
            this.lb_amount = new OX.Wallets.UI.Controls.DarkLabel();
            this.bt_Close = new OX.Wallets.UI.Controls.DarkButton();
            this.tb_amount = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_asset = new OX.Wallets.UI.Controls.DarkLabel();
            this.bt_OK = new OX.Wallets.UI.Controls.DarkButton();
            this.cbTargets = new OX.Wallets.UI.Controls.DarkComboBox();
            this.lb_target = new OX.Wallets.UI.Controls.DarkLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.cbAssets);
            this.panel.Controls.Add(this.lb_amount);
            this.panel.Controls.Add(this.bt_Close);
            this.panel.Controls.Add(this.tb_amount);
            this.panel.Controls.Add(this.lb_asset);
            this.panel.Controls.Add(this.bt_OK);
            this.panel.Controls.Add(this.cbTargets);
            this.panel.Controls.Add(this.lb_target);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // cbAssets
            // 
            this.cbAssets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            resources.ApplyResources(this.cbAssets, "cbAssets");
            this.cbAssets.Name = "cbAssets";
            this.cbAssets.SpecialBorderColor = null;
            this.cbAssets.SpecialFillColor = null;
            this.cbAssets.SpecialTextColor = null;
            // 
            // lb_amount
            // 
            resources.ApplyResources(this.lb_amount, "lb_amount");
            this.lb_amount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_amount.Name = "lb_amount";
            // 
            // bt_Close
            // 
            resources.ApplyResources(this.bt_Close, "bt_Close");
            this.bt_Close.Name = "bt_Close";
            this.bt_Close.SpecialBorderColor = null;
            this.bt_Close.SpecialFillColor = null;
            this.bt_Close.SpecialTextColor = null;
            this.bt_Close.Click += new System.EventHandler(this.bt_Close_Click);
            // 
            // tb_amount
            // 
            this.tb_amount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_amount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_amount, "tb_amount");
            this.tb_amount.Name = "tb_amount";
            // 
            // lb_asset
            // 
            resources.ApplyResources(this.lb_asset, "lb_asset");
            this.lb_asset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_asset.Name = "lb_asset";
            // 
            // bt_OK
            // 
            resources.ApplyResources(this.bt_OK, "bt_OK");
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.SpecialBorderColor = null;
            this.bt_OK.SpecialFillColor = null;
            this.bt_OK.SpecialTextColor = null;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // cbTargets
            // 
            this.cbTargets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            resources.ApplyResources(this.cbTargets, "cbTargets");
            this.cbTargets.Name = "cbTargets";
            this.cbTargets.SpecialBorderColor = null;
            this.cbTargets.SpecialFillColor = null;
            this.cbTargets.SpecialTextColor = null;
            this.cbTargets.SelectedIndexChanged += new System.EventHandler(this.cbAccounts_SelectedIndexChanged);
            // 
            // lb_target
            // 
            resources.ApplyResources(this.lb_target, "lb_target");
            this.lb_target.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_target.Name = "lb_target";
            // 
            // TransferTrustAsset
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransferTrustAsset";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClaimForm_FormClosing);
            this.Load += new System.EventHandler(this.NewEvent_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkComboBox cbTargets;
        private UI.Controls.DarkLabel lb_target;
        private UI.Controls.DarkTextBox tb_amount;
        private UI.Controls.DarkLabel lb_asset;
        private UI.Controls.DarkButton bt_OK;
        private UI.Controls.DarkButton bt_Close;
        private UI.Controls.DarkComboBox cbAssets;
        private UI.Controls.DarkLabel lb_amount;
    }
}