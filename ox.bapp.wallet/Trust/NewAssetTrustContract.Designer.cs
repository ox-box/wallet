namespace OX.Wallets.Base
{
    partial class NewAssetTrustContract
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAssetTrustContract));
            panel = new System.Windows.Forms.Panel();
            lb_side_scope = new UI.Controls.DarkLabel();
            bt_copy = new UI.Controls.DarkButton();
            tb_trustAddr = new UI.Controls.DarkTextBox();
            lb_trustAddr = new UI.Controls.DarkLabel();
            lb_trusteeAddress = new UI.Controls.DarkLabel();
            bt_Close = new UI.Controls.DarkButton();
            tb_amount = new UI.Controls.DarkTextBox();
            lb_amount = new UI.Controls.DarkLabel();
            tb_balance = new UI.Controls.DarkTextBox();
            lb_balance = new UI.Controls.DarkLabel();
            bt_OK = new UI.Controls.DarkButton();
            bt_add = new UI.Controls.DarkButton();
            dlv_addrs = new UI.Controls.DarkListView();
            cbAccounts = new UI.Controls.DarkComboBox();
            lb_truster = new UI.Controls.DarkLabel();
            tb_addr = new UI.Controls.DarkTextBox();
            lb_main_scope = new UI.Controls.DarkLabel();
            tb_trusteePubkey = new UI.Controls.DarkTextBox();
            lb_trusteePubKey = new UI.Controls.DarkLabel();
            pl_side_scope = new System.Windows.Forms.FlowLayoutPanel();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // panel
            // 
            panel.Controls.Add(pl_side_scope);
            panel.Controls.Add(lb_side_scope);
            panel.Controls.Add(bt_copy);
            panel.Controls.Add(tb_trustAddr);
            panel.Controls.Add(lb_trustAddr);
            panel.Controls.Add(lb_trusteeAddress);
            panel.Controls.Add(bt_Close);
            panel.Controls.Add(tb_amount);
            panel.Controls.Add(lb_amount);
            panel.Controls.Add(tb_balance);
            panel.Controls.Add(lb_balance);
            panel.Controls.Add(bt_OK);
            panel.Controls.Add(bt_add);
            panel.Controls.Add(dlv_addrs);
            panel.Controls.Add(cbAccounts);
            panel.Controls.Add(lb_truster);
            panel.Controls.Add(tb_addr);
            panel.Controls.Add(lb_main_scope);
            panel.Controls.Add(tb_trusteePubkey);
            panel.Controls.Add(lb_trusteePubKey);
            resources.ApplyResources(panel, "panel");
            panel.Name = "panel";
            panel.Paint += panel_Paint;
            // 
            // lb_side_scope
            // 
            resources.ApplyResources(lb_side_scope, "lb_side_scope");
            lb_side_scope.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_side_scope.Name = "lb_side_scope";
            // 
            // bt_copy
            // 
            resources.ApplyResources(bt_copy, "bt_copy");
            bt_copy.Name = "bt_copy";
            bt_copy.SpecialBorderColor = null;
            bt_copy.SpecialFillColor = null;
            bt_copy.SpecialTextColor = null;
            bt_copy.Click += bt_copy_Click;
            // 
            // tb_trustAddr
            // 
            tb_trustAddr.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_trustAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_trustAddr.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_trustAddr, "tb_trustAddr");
            tb_trustAddr.Name = "tb_trustAddr";
            tb_trustAddr.ReadOnly = true;
            // 
            // lb_trustAddr
            // 
            resources.ApplyResources(lb_trustAddr, "lb_trustAddr");
            lb_trustAddr.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_trustAddr.Name = "lb_trustAddr";
            // 
            // lb_trusteeAddress
            // 
            resources.ApplyResources(lb_trusteeAddress, "lb_trusteeAddress");
            lb_trusteeAddress.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_trusteeAddress.Name = "lb_trusteeAddress";
            // 
            // bt_Close
            // 
            resources.ApplyResources(bt_Close, "bt_Close");
            bt_Close.Name = "bt_Close";
            bt_Close.SpecialBorderColor = null;
            bt_Close.SpecialFillColor = null;
            bt_Close.SpecialTextColor = null;
            bt_Close.Click += bt_Close_Click;
            // 
            // tb_amount
            // 
            tb_amount.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_amount, "tb_amount");
            tb_amount.Name = "tb_amount";
            // 
            // lb_amount
            // 
            resources.ApplyResources(lb_amount, "lb_amount");
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_amount.Name = "lb_amount";
            // 
            // tb_balance
            // 
            tb_balance.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_balance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_balance.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_balance, "tb_balance");
            tb_balance.Name = "tb_balance";
            tb_balance.ReadOnly = true;
            // 
            // lb_balance
            // 
            resources.ApplyResources(lb_balance, "lb_balance");
            lb_balance.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_balance.Name = "lb_balance";
            // 
            // bt_OK
            // 
            resources.ApplyResources(bt_OK, "bt_OK");
            bt_OK.Name = "bt_OK";
            bt_OK.SpecialBorderColor = null;
            bt_OK.SpecialFillColor = null;
            bt_OK.SpecialTextColor = null;
            bt_OK.Click += bt_OK_Click;
            // 
            // bt_add
            // 
            resources.ApplyResources(bt_add, "bt_add");
            bt_add.Name = "bt_add";
            bt_add.SpecialBorderColor = null;
            bt_add.SpecialFillColor = null;
            bt_add.SpecialTextColor = null;
            bt_add.Click += bt_add_Click;
            // 
            // dlv_addrs
            // 
            resources.ApplyResources(dlv_addrs, "dlv_addrs");
            dlv_addrs.Name = "dlv_addrs";
            dlv_addrs.MouseDoubleClick += dlv_addrs_MouseDoubleClick;
            // 
            // cbAccounts
            // 
            cbAccounts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            resources.ApplyResources(cbAccounts, "cbAccounts");
            cbAccounts.Name = "cbAccounts";
            cbAccounts.SpecialBorderColor = null;
            cbAccounts.SpecialFillColor = null;
            cbAccounts.SpecialTextColor = null;
            cbAccounts.SelectedIndexChanged += cbAccounts_SelectedIndexChanged;
            // 
            // lb_truster
            // 
            resources.ApplyResources(lb_truster, "lb_truster");
            lb_truster.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_truster.Name = "lb_truster";
            // 
            // tb_addr
            // 
            tb_addr.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_addr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_addr.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_addr, "tb_addr");
            tb_addr.Name = "tb_addr";
            // 
            // lb_main_scope
            // 
            resources.ApplyResources(lb_main_scope, "lb_main_scope");
            lb_main_scope.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_main_scope.Name = "lb_main_scope";
            // 
            // tb_trusteePubkey
            // 
            tb_trusteePubkey.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_trusteePubkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_trusteePubkey.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_trusteePubkey, "tb_trusteePubkey");
            tb_trusteePubkey.Name = "tb_trusteePubkey";
            tb_trusteePubkey.TextChanged += tb_trusteePubkey_TextChanged;
            // 
            // lb_trusteePubKey
            // 
            resources.ApplyResources(lb_trusteePubKey, "lb_trusteePubKey");
            lb_trusteePubKey.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_trusteePubKey.Name = "lb_trusteePubKey";
            // 
            // pl_side_scope
            // 
            resources.ApplyResources(pl_side_scope, "pl_side_scope");
            pl_side_scope.Name = "pl_side_scope";
            // 
            // NewAssetTrustContract
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewAssetTrustContract";
            FormClosing += ClaimForm_FormClosing;
            Load += NewEvent_Load;
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkLabel lb_trusteePubKey;
        private UI.Controls.DarkTextBox tb_trusteePubkey;
        private UI.Controls.DarkTextBox tb_addr;
        private UI.Controls.DarkLabel lb_main_scope;
        private UI.Controls.DarkComboBox cbAccounts;
        private UI.Controls.DarkLabel lb_truster;
        private UI.Controls.DarkListView dlv_addrs;
        private UI.Controls.DarkButton bt_add;
        private UI.Controls.DarkTextBox tb_amount;
        private UI.Controls.DarkLabel lb_amount;
        private UI.Controls.DarkTextBox tb_balance;
        private UI.Controls.DarkLabel lb_balance;
        private UI.Controls.DarkButton bt_OK;
        private UI.Controls.DarkButton bt_Close;
        private UI.Controls.DarkLabel lb_trusteeAddress;
        private UI.Controls.DarkButton bt_copy;
        private UI.Controls.DarkTextBox tb_trustAddr;
        private UI.Controls.DarkLabel lb_trustAddr;
        private UI.Controls.DarkLabel lb_side_scope;
        private System.Windows.Forms.FlowLayoutPanel pl_side_scope;
    }
}