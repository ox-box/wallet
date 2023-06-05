﻿namespace OX.Wallets.Base
{
    partial class NewEvent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewEvent));
            this.panel = new System.Windows.Forms.Panel();
            this.cb_Private = new OX.Wallets.UI.Controls.DarkCheckBox();
            this.lb_Private = new OX.Wallets.UI.Controls.DarkLabel();
            this.cbAccounts = new OX.Wallets.UI.Controls.DarkComboBox();
            this.lb_from = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_remark = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_remark = new OX.Wallets.UI.Controls.DarkTextBox();
            this.tb_name = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_name = new OX.Wallets.UI.Controls.DarkLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.cb_Private);
            this.panel.Controls.Add(this.lb_Private);
            this.panel.Controls.Add(this.cbAccounts);
            this.panel.Controls.Add(this.lb_from);
            this.panel.Controls.Add(this.lb_remark);
            this.panel.Controls.Add(this.tb_remark);
            this.panel.Controls.Add(this.tb_name);
            this.panel.Controls.Add(this.lb_name);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // cb_Private
            // 
            resources.ApplyResources(this.cb_Private, "cb_Private");
            this.cb_Private.Name = "cb_Private";
            this.cb_Private.SpecialBorderColor = null;
            this.cb_Private.SpecialFillColor = null;
            this.cb_Private.SpecialTextColor = null;
            // 
            // lb_Private
            // 
            resources.ApplyResources(this.lb_Private, "lb_Private");
            this.lb_Private.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_Private.Name = "lb_Private";
            // 
            // cbAccounts
            // 
            this.cbAccounts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            resources.ApplyResources(this.cbAccounts, "cbAccounts");
            this.cbAccounts.Name = "cbAccounts";
            this.cbAccounts.SpecialBorderColor = null;
            this.cbAccounts.SpecialFillColor = null;
            this.cbAccounts.SpecialTextColor = null;
            // 
            // lb_from
            // 
            resources.ApplyResources(this.lb_from, "lb_from");
            this.lb_from.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_from.Name = "lb_from";
            // 
            // lb_remark
            // 
            resources.ApplyResources(this.lb_remark, "lb_remark");
            this.lb_remark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_remark.Name = "lb_remark";
            // 
            // tb_remark
            // 
            this.tb_remark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_remark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_remark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_remark, "tb_remark");
            this.tb_remark.Name = "tb_remark";
            // 
            // tb_name
            // 
            this.tb_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_name, "tb_name");
            this.tb_name.Name = "tb_name";
            // 
            // lb_name
            // 
            resources.ApplyResources(this.lb_name, "lb_name");
            this.lb_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_name.Name = "lb_name";
            // 
            // NewEvent
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewEvent";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClaimForm_FormClosing);
            this.Load += new System.EventHandler(this.NewEvent_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkLabel lb_name;
        private UI.Controls.DarkTextBox tb_name;
        private UI.Controls.DarkTextBox tb_remark;
        private UI.Controls.DarkLabel lb_remark;
        private UI.Controls.DarkComboBox cbAccounts;
        private UI.Controls.DarkLabel lb_from;
        private UI.Controls.DarkLabel lb_Private;
        private UI.Controls.DarkCheckBox cb_Private;
    }
}