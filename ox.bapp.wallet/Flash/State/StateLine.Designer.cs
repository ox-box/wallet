using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
namespace OX.Wallets.Flash.State
{
    partial class StateLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateLine));
            bt_pre = new DarkButton();
            bt_next = new DarkButton();
            renderer = new System.Windows.Forms.WebBrowser();
            TagsPanel = new System.Windows.Forms.FlowLayoutPanel();
            bt_more = new DarkButton();
            bt_first = new DarkButton();
            SuspendLayout();
            // 
            // bt_pre
            // 
            bt_pre.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_pre.Location = new System.Drawing.Point(303, 499);
            bt_pre.Name = "bt_pre";
            bt_pre.Padding = new System.Windows.Forms.Padding(5);
            bt_pre.Size = new System.Drawing.Size(288, 36);
            bt_pre.SpecialBorderColor = null;
            bt_pre.SpecialFillColor = null;
            bt_pre.SpecialTextColor = null;
            bt_pre.TabIndex = 1;
            bt_pre.Text = "darkButton2";
            bt_pre.Click += bt_pre_Click;
            // 
            // bt_next
            // 
            bt_next.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_next.Location = new System.Drawing.Point(597, 499);
            bt_next.Name = "bt_next";
            bt_next.Padding = new System.Windows.Forms.Padding(5);
            bt_next.Size = new System.Drawing.Size(288, 36);
            bt_next.SpecialBorderColor = null;
            bt_next.SpecialFillColor = null;
            bt_next.SpecialTextColor = null;
            bt_next.TabIndex = 2;
            bt_next.Text = "darkButton2";
            bt_next.Click += bt_next_Click;
            // 
            // renderer
            // 
            renderer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            renderer.Location = new System.Drawing.Point(7, 72);
            renderer.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            renderer.MinimumSize = new System.Drawing.Size(27, 29);
            renderer.Name = "renderer";
            renderer.Size = new System.Drawing.Size(1176, 421);
            renderer.TabIndex = 48;
            renderer.DocumentCompleted += renderer_DocumentCompleted;
            // 
            // TagsPanel
            // 
            TagsPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TagsPanel.AutoScroll = true;
            TagsPanel.AutoSize = true;
            TagsPanel.Location = new System.Drawing.Point(25, 13);
            TagsPanel.Name = "TagsPanel";
            TagsPanel.Size = new System.Drawing.Size(1062, 48);
            TagsPanel.TabIndex = 49;
            TagsPanel.SizeChanged += TagsPanel_SizeChanged;
            // 
            // bt_more
            // 
            bt_more.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_more.Image = (System.Drawing.Image)resources.GetObject("bt_more.Image");
            bt_more.Location = new System.Drawing.Point(1094, 23);
            bt_more.Name = "bt_more";
            bt_more.Padding = new System.Windows.Forms.Padding(5);
            bt_more.Size = new System.Drawing.Size(44, 29);
            bt_more.SpecialBorderColor = null;
            bt_more.SpecialFillColor = null;
            bt_more.SpecialTextColor = null;
            bt_more.TabIndex = 0;
            bt_more.Click += bt_more_Click;
            // 
            // bt_first
            // 
            bt_first.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_first.Location = new System.Drawing.Point(7, 499);
            bt_first.Name = "bt_first";
            bt_first.Padding = new System.Windows.Forms.Padding(5);
            bt_first.Size = new System.Drawing.Size(288, 36);
            bt_first.SpecialBorderColor = null;
            bt_first.SpecialFillColor = null;
            bt_first.SpecialTextColor = null;
            bt_first.TabIndex = 50;
            bt_first.Text = "darkButton2";
            bt_first.Click += bt_first_Click;
            // 
            // StateLine
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(bt_first);
            Controls.Add(bt_next);
            Controls.Add(bt_more);
            Controls.Add(bt_pre);
            Controls.Add(TagsPanel);
            Controls.Add(renderer);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "StateLine";
            Size = new System.Drawing.Size(1190, 543);
            Load += StateLine_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.WebBrowser renderer;
        protected System.Windows.Forms.FlowLayoutPanel TagsPanel;
        private DarkButton bt_pre;
        private DarkButton bt_more;
        private DarkButton bt_next;
        private DarkButton bt_first;
    }
}
