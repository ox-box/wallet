namespace OX.Wallets.Base
{
    partial class SetPortalHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetPortalHome));
            lb_name = new UI.Controls.DarkLabel();
            tb_name = new UI.Controls.DarkTextBox();
            tb_remark = new UI.Controls.DarkTextBox();
            lb_remark = new UI.Controls.DarkLabel();
            panel = new System.Windows.Forms.Panel();
            tb_baseUrl = new UI.Controls.DarkTextBox();
            lb_baseUrl = new UI.Controls.DarkLabel();
            bt_cancel = new UI.Controls.DarkButton();
            bt_ok = new UI.Controls.DarkButton();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // lb_name
            // 
            resources.ApplyResources(lb_name, "lb_name");
            lb_name.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_name.Name = "lb_name";
            // 
            // tb_name
            // 
            tb_name.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_name.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_name, "tb_name");
            tb_name.Name = "tb_name";
            // 
            // tb_remark
            // 
            tb_remark.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_remark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_remark.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_remark, "tb_remark");
            tb_remark.Name = "tb_remark";
            // 
            // lb_remark
            // 
            resources.ApplyResources(lb_remark, "lb_remark");
            lb_remark.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_remark.Name = "lb_remark";
            // 
            // panel
            // 
            panel.Controls.Add(tb_baseUrl);
            panel.Controls.Add(lb_baseUrl);
            panel.Controls.Add(bt_cancel);
            panel.Controls.Add(bt_ok);
            panel.Controls.Add(lb_remark);
            panel.Controls.Add(tb_remark);
            panel.Controls.Add(tb_name);
            panel.Controls.Add(lb_name);
            resources.ApplyResources(panel, "panel");
            panel.Name = "panel";
            // 
            // tb_baseUrl
            // 
            tb_baseUrl.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_baseUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_baseUrl.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_baseUrl, "tb_baseUrl");
            tb_baseUrl.Name = "tb_baseUrl";
            // 
            // lb_baseUrl
            // 
            resources.ApplyResources(lb_baseUrl, "lb_baseUrl");
            lb_baseUrl.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_baseUrl.Name = "lb_baseUrl";
            // 
            // bt_cancel
            // 
            resources.ApplyResources(bt_cancel, "bt_cancel");
            bt_cancel.Name = "bt_cancel";
            bt_cancel.SpecialBorderColor = null;
            bt_cancel.SpecialFillColor = null;
            bt_cancel.SpecialTextColor = null;
            bt_cancel.Click += bt_cancel_Click;
            // 
            // bt_ok
            // 
            resources.ApplyResources(bt_ok, "bt_ok");
            bt_ok.Name = "bt_ok";
            bt_ok.SpecialBorderColor = null;
            bt_ok.SpecialFillColor = null;
            bt_ok.SpecialTextColor = null;
            bt_ok.Click += bt_ok_Click;
            // 
            // SetPortalHome
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SetPortalHome";
            FormClosing += ClaimForm_FormClosing;
            Load += ClaimForm_Load;
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private UI.Controls.DarkLabel lb_name;
        private UI.Controls.DarkTextBox tb_name;
        private UI.Controls.DarkTextBox tb_remark;
        private UI.Controls.DarkLabel lb_remark;
        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkButton bt_cancel;
        private UI.Controls.DarkButton bt_ok;
        private UI.Controls.DarkTextBox tb_baseUrl;
        private UI.Controls.DarkLabel lb_baseUrl;
    }
}