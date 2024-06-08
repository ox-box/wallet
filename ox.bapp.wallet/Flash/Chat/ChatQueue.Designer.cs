namespace OX.Wallets.Flash
{
    partial class ChatQueue
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
            itemsPanel = new System.Windows.Forms.FlowLayoutPanel();
            bt_close = new UI.Controls.DarkButton();
            SuspendLayout();
            // 
            // itemsPanel
            // 
            itemsPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            itemsPanel.AutoScroll = true;
            itemsPanel.Location = new System.Drawing.Point(0, 0);
            itemsPanel.Name = "itemsPanel";
            itemsPanel.Size = new System.Drawing.Size(1335, 799);
            itemsPanel.TabIndex = 2;
            // 
            // bt_close
            // 
            bt_close.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_close.Location = new System.Drawing.Point(1179, 827);
            bt_close.Name = "bt_close";
            bt_close.Padding = new System.Windows.Forms.Padding(5);
            bt_close.Size = new System.Drawing.Size(112, 34);
            bt_close.SpecialBorderColor = null;
            bt_close.SpecialFillColor = null;
            bt_close.SpecialTextColor = null;
            bt_close.TabIndex = 3;
            bt_close.Text = "darkButton1";
            bt_close.Click += bt_close_Click;
            // 
            // ChatQueue
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1336, 882);
            Controls.Add(bt_close);
            Controls.Add(itemsPanel);
            Name = "ChatQueue";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "NewLetter";
            Load += EditTalkLineName_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel itemsPanel;
        private UI.Controls.DarkButton bt_close;
    }
}