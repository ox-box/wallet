namespace OX.Wallets.Base
{
    partial class DetainDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetainDialog));
            this.lb_DurationIndex = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_DurationIndex = new OX.Wallets.UI.Controls.DarkTextBox();
            this.cb_detainState = new OX.Wallets.UI.Controls.DarkComboBox();
            this.lb_detainState = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_address = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_address_v = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_state = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_state_v = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_expireIndex = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_expireIndex_v = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_fee = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_fee_v = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_oxsBalance = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_oxsBalance_v = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkTitle1 = new OX.Wallets.UI.Controls.DarkTitle();
            this.lb_AskFee = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_askFee_V = new OX.Wallets.UI.Controls.DarkTextBox();
            this.darkLabel1 = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // lb_DurationIndex
            // 
            resources.ApplyResources(this.lb_DurationIndex, "lb_DurationIndex");
            this.lb_DurationIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_DurationIndex.Name = "lb_DurationIndex";
            // 
            // tb_DurationIndex
            // 
            this.tb_DurationIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_DurationIndex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_DurationIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_DurationIndex, "tb_DurationIndex");
            this.tb_DurationIndex.Name = "tb_DurationIndex";
            this.tb_DurationIndex.TextChanged += new System.EventHandler(this.tb_DurationIndex_TextChanged);
            // 
            // cb_detainState
            // 
            this.cb_detainState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_detainState.FormattingEnabled = true;
            resources.ApplyResources(this.cb_detainState, "cb_detainState");
            this.cb_detainState.Name = "cb_detainState";
            this.cb_detainState.SpecialBorderColor = null;
            this.cb_detainState.SpecialFillColor = null;
            this.cb_detainState.SpecialTextColor = null;
            this.cb_detainState.TextChanged += new System.EventHandler(this.combo_address_TextChanged);
            // 
            // lb_detainState
            // 
            resources.ApplyResources(this.lb_detainState, "lb_detainState");
            this.lb_detainState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_detainState.Name = "lb_detainState";
            // 
            // lb_address
            // 
            resources.ApplyResources(this.lb_address, "lb_address");
            this.lb_address.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_address.Name = "lb_address";
            // 
            // lb_address_v
            // 
            resources.ApplyResources(this.lb_address_v, "lb_address_v");
            this.lb_address_v.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_address_v.Name = "lb_address_v";
            // 
            // lb_state
            // 
            resources.ApplyResources(this.lb_state, "lb_state");
            this.lb_state.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_state.Name = "lb_state";
            // 
            // lb_state_v
            // 
            resources.ApplyResources(this.lb_state_v, "lb_state_v");
            this.lb_state_v.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_state_v.Name = "lb_state_v";
            // 
            // lb_expireIndex
            // 
            resources.ApplyResources(this.lb_expireIndex, "lb_expireIndex");
            this.lb_expireIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_expireIndex.Name = "lb_expireIndex";
            // 
            // lb_expireIndex_v
            // 
            resources.ApplyResources(this.lb_expireIndex_v, "lb_expireIndex_v");
            this.lb_expireIndex_v.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_expireIndex_v.Name = "lb_expireIndex_v";
            // 
            // lb_fee
            // 
            resources.ApplyResources(this.lb_fee, "lb_fee");
            this.lb_fee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_fee.Name = "lb_fee";
            // 
            // lb_fee_v
            // 
            resources.ApplyResources(this.lb_fee_v, "lb_fee_v");
            this.lb_fee_v.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_fee_v.Name = "lb_fee_v";
            // 
            // lb_oxsBalance
            // 
            resources.ApplyResources(this.lb_oxsBalance, "lb_oxsBalance");
            this.lb_oxsBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_oxsBalance.Name = "lb_oxsBalance";
            // 
            // lb_oxsBalance_v
            // 
            resources.ApplyResources(this.lb_oxsBalance_v, "lb_oxsBalance_v");
            this.lb_oxsBalance_v.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_oxsBalance_v.Name = "lb_oxsBalance_v";
            // 
            // darkTitle1
            // 
            resources.ApplyResources(this.darkTitle1, "darkTitle1");
            this.darkTitle1.Name = "darkTitle1";
            // 
            // lb_AskFee
            // 
            resources.ApplyResources(this.lb_AskFee, "lb_AskFee");
            this.lb_AskFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_AskFee.Name = "lb_AskFee";
            // 
            // tb_askFee_V
            // 
            this.tb_askFee_V.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_askFee_V.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_askFee_V.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_askFee_V, "tb_askFee_V");
            this.tb_askFee_V.Name = "tb_askFee_V";
            this.tb_askFee_V.TextChanged += new System.EventHandler(this.tb_askFee_V_TextChanged);
            // 
            // darkLabel1
            // 
            resources.ApplyResources(this.darkLabel1, "darkLabel1");
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Name = "darkLabel1";
            // 
            // DetainDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.darkLabel1);
            this.Controls.Add(this.tb_askFee_V);
            this.Controls.Add(this.lb_AskFee);
            this.Controls.Add(this.darkTitle1);
            this.Controls.Add(this.lb_oxsBalance_v);
            this.Controls.Add(this.lb_oxsBalance);
            this.Controls.Add(this.lb_fee_v);
            this.Controls.Add(this.lb_fee);
            this.Controls.Add(this.lb_expireIndex_v);
            this.Controls.Add(this.lb_expireIndex);
            this.Controls.Add(this.lb_state_v);
            this.Controls.Add(this.lb_state);
            this.Controls.Add(this.lb_address_v);
            this.Controls.Add(this.lb_address);
            this.Controls.Add(this.lb_detainState);
            this.Controls.Add(this.cb_detainState);
            this.Controls.Add(this.tb_DurationIndex);
            this.Controls.Add(this.lb_DurationIndex);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetainDialog";
            this.Load += new System.EventHandler(this.ClaimForm_Load);
            this.Controls.SetChildIndex(this.lb_DurationIndex, 0);
            this.Controls.SetChildIndex(this.tb_DurationIndex, 0);
            this.Controls.SetChildIndex(this.cb_detainState, 0);
            this.Controls.SetChildIndex(this.lb_detainState, 0);
            this.Controls.SetChildIndex(this.lb_address, 0);
            this.Controls.SetChildIndex(this.lb_address_v, 0);
            this.Controls.SetChildIndex(this.lb_state, 0);
            this.Controls.SetChildIndex(this.lb_state_v, 0);
            this.Controls.SetChildIndex(this.lb_expireIndex, 0);
            this.Controls.SetChildIndex(this.lb_expireIndex_v, 0);
            this.Controls.SetChildIndex(this.lb_fee, 0);
            this.Controls.SetChildIndex(this.lb_fee_v, 0);
            this.Controls.SetChildIndex(this.lb_oxsBalance, 0);
            this.Controls.SetChildIndex(this.lb_oxsBalance_v, 0);
            this.Controls.SetChildIndex(this.darkTitle1, 0);
            this.Controls.SetChildIndex(this.lb_AskFee, 0);
            this.Controls.SetChildIndex(this.tb_askFee_V, 0);
            this.Controls.SetChildIndex(this.darkLabel1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel lb_DurationIndex;
        private OX.Wallets.UI.Controls.DarkTextBox tb_DurationIndex;
        private OX.Wallets.UI.Controls.DarkComboBox cb_detainState;
        private OX.Wallets.UI.Controls.DarkLabel lb_detainState;
        private UI.Controls.DarkLabel lb_address;
        private UI.Controls.DarkLabel lb_address_v;
        private UI.Controls.DarkLabel lb_state;
        private UI.Controls.DarkLabel lb_state_v;
        private UI.Controls.DarkLabel lb_expireIndex;
        private UI.Controls.DarkLabel lb_expireIndex_v;
        private UI.Controls.DarkLabel lb_fee;
        private UI.Controls.DarkLabel lb_fee_v;
        private UI.Controls.DarkLabel lb_oxsBalance;
        private UI.Controls.DarkLabel lb_oxsBalance_v;
        private UI.Controls.DarkTitle darkTitle1;
        private UI.Controls.DarkLabel lb_AskFee;
        private UI.Controls.DarkTextBox tb_askFee_V;
        private UI.Controls.DarkLabel darkLabel1;
    }
}