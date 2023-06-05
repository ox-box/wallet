namespace OX.Wallets.Base
{
    partial class DiggList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiggList));
            this.panel = new System.Windows.Forms.Panel();
            this.bt_close = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_newDigg = new OX.Wallets.UI.Controls.DarkButton();
            this.lb_pageIndex = new OX.Wallets.UI.Controls.DarkLabel();
            this.bt_next100 = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_next10 = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_pre100 = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_pre10 = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_next = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_pre = new OX.Wallets.UI.Controls.DarkButton();
            this.RoundPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tb_EngraveMessage = new OX.Wallets.UI.Controls.DarkTextBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.bt_close);
            this.panel.Controls.Add(this.bt_newDigg);
            this.panel.Controls.Add(this.lb_pageIndex);
            this.panel.Controls.Add(this.bt_next100);
            this.panel.Controls.Add(this.bt_next10);
            this.panel.Controls.Add(this.bt_pre100);
            this.panel.Controls.Add(this.bt_pre10);
            this.panel.Controls.Add(this.bt_next);
            this.panel.Controls.Add(this.bt_pre);
            this.panel.Controls.Add(this.RoundPanel);
            this.panel.Controls.Add(this.tb_EngraveMessage);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // bt_close
            // 
            resources.ApplyResources(this.bt_close, "bt_close");
            this.bt_close.Name = "bt_close";
            this.bt_close.SpecialBorderColor = null;
            this.bt_close.SpecialFillColor = null;
            this.bt_close.SpecialTextColor = null;
            this.bt_close.Click += new System.EventHandler(this.bt_close_Click);
            // 
            // bt_newDigg
            // 
            resources.ApplyResources(this.bt_newDigg, "bt_newDigg");
            this.bt_newDigg.Name = "bt_newDigg";
            this.bt_newDigg.SpecialBorderColor = null;
            this.bt_newDigg.SpecialFillColor = null;
            this.bt_newDigg.SpecialTextColor = null;
            this.bt_newDigg.Click += new System.EventHandler(this.bt_newDigg_Click);
            // 
            // lb_pageIndex
            // 
            resources.ApplyResources(this.lb_pageIndex, "lb_pageIndex");
            this.lb_pageIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_pageIndex.Name = "lb_pageIndex";
            // 
            // bt_next100
            // 
            resources.ApplyResources(this.bt_next100, "bt_next100");
            this.bt_next100.Name = "bt_next100";
            this.bt_next100.SpecialBorderColor = null;
            this.bt_next100.SpecialFillColor = null;
            this.bt_next100.SpecialTextColor = null;
            this.bt_next100.Click += new System.EventHandler(this.bt_next100_Click);
            // 
            // bt_next10
            // 
            resources.ApplyResources(this.bt_next10, "bt_next10");
            this.bt_next10.Name = "bt_next10";
            this.bt_next10.SpecialBorderColor = null;
            this.bt_next10.SpecialFillColor = null;
            this.bt_next10.SpecialTextColor = null;
            this.bt_next10.Click += new System.EventHandler(this.bt_next10_Click);
            // 
            // bt_pre100
            // 
            resources.ApplyResources(this.bt_pre100, "bt_pre100");
            this.bt_pre100.Name = "bt_pre100";
            this.bt_pre100.SpecialBorderColor = null;
            this.bt_pre100.SpecialFillColor = null;
            this.bt_pre100.SpecialTextColor = null;
            this.bt_pre100.Click += new System.EventHandler(this.bt_pre100_Click);
            // 
            // bt_pre10
            // 
            resources.ApplyResources(this.bt_pre10, "bt_pre10");
            this.bt_pre10.Name = "bt_pre10";
            this.bt_pre10.SpecialBorderColor = null;
            this.bt_pre10.SpecialFillColor = null;
            this.bt_pre10.SpecialTextColor = null;
            this.bt_pre10.Click += new System.EventHandler(this.bt_pre10_Click);
            // 
            // bt_next
            // 
            resources.ApplyResources(this.bt_next, "bt_next");
            this.bt_next.Name = "bt_next";
            this.bt_next.SpecialBorderColor = null;
            this.bt_next.SpecialFillColor = null;
            this.bt_next.SpecialTextColor = null;
            this.bt_next.Click += new System.EventHandler(this.bt_next_Click);
            // 
            // bt_pre
            // 
            resources.ApplyResources(this.bt_pre, "bt_pre");
            this.bt_pre.Name = "bt_pre";
            this.bt_pre.SpecialBorderColor = null;
            this.bt_pre.SpecialFillColor = null;
            this.bt_pre.SpecialTextColor = null;
            this.bt_pre.Click += new System.EventHandler(this.bt_pre_Click);
            // 
            // RoundPanel
            // 
            resources.ApplyResources(this.RoundPanel, "RoundPanel");
            this.RoundPanel.Name = "RoundPanel";
            // 
            // tb_EngraveMessage
            // 
            resources.ApplyResources(this.tb_EngraveMessage, "tb_EngraveMessage");
            this.tb_EngraveMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_EngraveMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_EngraveMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_EngraveMessage.Name = "tb_EngraveMessage";
            this.tb_EngraveMessage.ReadOnly = true;
            // 
            // DiggList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "DiggList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClaimForm_FormClosing);
            this.Load += new System.EventHandler(this.DiggList_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkTextBox tb_EngraveMessage;
        protected System.Windows.Forms.FlowLayoutPanel RoundPanel;
        private UI.Controls.DarkLabel lb_pageIndex;
        private UI.Controls.DarkButton bt_next100;
        private UI.Controls.DarkButton bt_next10;
        private UI.Controls.DarkButton bt_pre100;
        private UI.Controls.DarkButton bt_pre10;
        private UI.Controls.DarkButton bt_next;
        private UI.Controls.DarkButton bt_pre;
        private UI.Controls.DarkButton bt_close;
        private UI.Controls.DarkButton bt_newDigg;
    }
}