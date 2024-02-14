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
            lb_DurationIndex = new UI.Controls.DarkLabel();
            tb_DurationIndex = new UI.Controls.DarkTextBox();
            cb_detainState = new UI.Controls.DarkComboBox();
            lb_detainState = new UI.Controls.DarkLabel();
            lb_address = new UI.Controls.DarkLabel();
            lb_address_v = new UI.Controls.DarkLabel();
            lb_state = new UI.Controls.DarkLabel();
            lb_state_v = new UI.Controls.DarkLabel();
            lb_expireIndex = new UI.Controls.DarkLabel();
            lb_expireIndex_v = new UI.Controls.DarkLabel();
            lb_fee = new UI.Controls.DarkLabel();
            lb_fee_v = new UI.Controls.DarkLabel();
            lb_oxsBalance = new UI.Controls.DarkLabel();
            lb_oxsBalance_v = new UI.Controls.DarkLabel();
            darkTitle1 = new UI.Controls.DarkTitle();
            lb_AskFee = new UI.Controls.DarkLabel();
            tb_askFee_V = new UI.Controls.DarkTextBox();
            darkLabel1 = new UI.Controls.DarkLabel();
            SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(btnOk, "btnOk");
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            // 
            // btnClose
            // 
            resources.ApplyResources(btnClose, "btnClose");
            // 
            // btnYes
            // 
            resources.ApplyResources(btnYes, "btnYes");
            // 
            // btnNo
            // 
            resources.ApplyResources(btnNo, "btnNo");
            // 
            // btnRetry
            // 
            resources.ApplyResources(btnRetry, "btnRetry");
            // 
            // btnIgnore
            // 
            resources.ApplyResources(btnIgnore, "btnIgnore");
            // 
            // lb_DurationIndex
            // 
            resources.ApplyResources(lb_DurationIndex, "lb_DurationIndex");
            lb_DurationIndex.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_DurationIndex.Name = "lb_DurationIndex";
            // 
            // tb_DurationIndex
            // 
            tb_DurationIndex.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_DurationIndex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tb_DurationIndex.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_DurationIndex, "tb_DurationIndex");
            tb_DurationIndex.Name = "tb_DurationIndex";
            tb_DurationIndex.TextChanged += tb_DurationIndex_TextChanged;
            // 
            // cb_detainState
            // 
            cb_detainState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            cb_detainState.FormattingEnabled = true;
            resources.ApplyResources(cb_detainState, "cb_detainState");
            cb_detainState.Name = "cb_detainState";
            cb_detainState.SpecialBorderColor = null;
            cb_detainState.SpecialFillColor = null;
            cb_detainState.SpecialTextColor = null;
            cb_detainState.TextChanged += combo_address_TextChanged;
            // 
            // lb_detainState
            // 
            resources.ApplyResources(lb_detainState, "lb_detainState");
            lb_detainState.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_detainState.Name = "lb_detainState";
            // 
            // lb_address
            // 
            resources.ApplyResources(lb_address, "lb_address");
            lb_address.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_address.Name = "lb_address";
            // 
            // lb_address_v
            // 
            resources.ApplyResources(lb_address_v, "lb_address_v");
            lb_address_v.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_address_v.Name = "lb_address_v";
            // 
            // lb_state
            // 
            resources.ApplyResources(lb_state, "lb_state");
            lb_state.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_state.Name = "lb_state";
            // 
            // lb_state_v
            // 
            resources.ApplyResources(lb_state_v, "lb_state_v");
            lb_state_v.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_state_v.Name = "lb_state_v";
            // 
            // lb_expireIndex
            // 
            resources.ApplyResources(lb_expireIndex, "lb_expireIndex");
            lb_expireIndex.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_expireIndex.Name = "lb_expireIndex";
            // 
            // lb_expireIndex_v
            // 
            resources.ApplyResources(lb_expireIndex_v, "lb_expireIndex_v");
            lb_expireIndex_v.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_expireIndex_v.Name = "lb_expireIndex_v";
            // 
            // lb_fee
            // 
            resources.ApplyResources(lb_fee, "lb_fee");
            lb_fee.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_fee.Name = "lb_fee";
            // 
            // lb_fee_v
            // 
            resources.ApplyResources(lb_fee_v, "lb_fee_v");
            lb_fee_v.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_fee_v.Name = "lb_fee_v";
            // 
            // lb_oxsBalance
            // 
            resources.ApplyResources(lb_oxsBalance, "lb_oxsBalance");
            lb_oxsBalance.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_oxsBalance.Name = "lb_oxsBalance";
            // 
            // lb_oxsBalance_v
            // 
            resources.ApplyResources(lb_oxsBalance_v, "lb_oxsBalance_v");
            lb_oxsBalance_v.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_oxsBalance_v.Name = "lb_oxsBalance_v";
            // 
            // darkTitle1
            // 
            resources.ApplyResources(darkTitle1, "darkTitle1");
            darkTitle1.Name = "darkTitle1";
            // 
            // lb_AskFee
            // 
            resources.ApplyResources(lb_AskFee, "lb_AskFee");
            lb_AskFee.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_AskFee.Name = "lb_AskFee";
            // 
            // tb_askFee_V
            // 
            tb_askFee_V.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_askFee_V.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tb_askFee_V.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_askFee_V, "tb_askFee_V");
            tb_askFee_V.Name = "tb_askFee_V";
            tb_askFee_V.TextChanged += tb_askFee_V_TextChanged;
            // 
            // darkLabel1
            // 
            resources.ApplyResources(darkLabel1, "darkLabel1");
            darkLabel1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel1.Name = "darkLabel1";
            // 
            // DetainDialog
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(darkLabel1);
            Controls.Add(tb_askFee_V);
            Controls.Add(lb_AskFee);
            Controls.Add(darkTitle1);
            Controls.Add(lb_oxsBalance_v);
            Controls.Add(lb_oxsBalance);
            Controls.Add(lb_fee_v);
            Controls.Add(lb_fee);
            Controls.Add(lb_expireIndex_v);
            Controls.Add(lb_expireIndex);
            Controls.Add(lb_state_v);
            Controls.Add(lb_state);
            Controls.Add(lb_address_v);
            Controls.Add(lb_address);
            Controls.Add(lb_detainState);
            Controls.Add(cb_detainState);
            Controls.Add(tb_DurationIndex);
            Controls.Add(lb_DurationIndex);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DetainDialog";
            Load += ClaimForm_Load;
            Controls.SetChildIndex(lb_DurationIndex, 0);
            Controls.SetChildIndex(tb_DurationIndex, 0);
            Controls.SetChildIndex(cb_detainState, 0);
            Controls.SetChildIndex(lb_detainState, 0);
            Controls.SetChildIndex(lb_address, 0);
            Controls.SetChildIndex(lb_address_v, 0);
            Controls.SetChildIndex(lb_state, 0);
            Controls.SetChildIndex(lb_state_v, 0);
            Controls.SetChildIndex(lb_expireIndex, 0);
            Controls.SetChildIndex(lb_expireIndex_v, 0);
            Controls.SetChildIndex(lb_fee, 0);
            Controls.SetChildIndex(lb_fee_v, 0);
            Controls.SetChildIndex(lb_oxsBalance, 0);
            Controls.SetChildIndex(lb_oxsBalance_v, 0);
            Controls.SetChildIndex(darkTitle1, 0);
            Controls.SetChildIndex(lb_AskFee, 0);
            Controls.SetChildIndex(tb_askFee_V, 0);
            Controls.SetChildIndex(darkLabel1, 0);
            ResumeLayout(false);
            PerformLayout();
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