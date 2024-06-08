
namespace OX.Wallets.Flash.Chat
{
	partial class UnicastChatbox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnicastChatbox));
            topPanel = new System.Windows.Forms.Panel();
            talkName = new System.Windows.Forms.Label();
            talkLabel = new System.Windows.Forms.Label();
            bottomPanel = new System.Windows.Forms.Panel();
            queueButton = new System.Windows.Forms.Button();
            chatTextbox = new System.Windows.Forms.TextBox();
            attachButton = new System.Windows.Forms.Button();
            sendButton = new System.Windows.Forms.Button();
            itemsPanel = new System.Windows.Forms.Panel();
            topPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = System.Drawing.Color.RoyalBlue;
            topPanel.Controls.Add(talkName);
            topPanel.Controls.Add(talkLabel);
            topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            topPanel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            topPanel.Location = new System.Drawing.Point(0, 0);
            topPanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            topPanel.Name = "topPanel";
            topPanel.Padding = new System.Windows.Forms.Padding(10, 5, 5, 10);
            topPanel.Size = new System.Drawing.Size(1324, 92);
            topPanel.TabIndex = 0;
            topPanel.Paint += topPanel_Paint;
            // 
            // talkName
            // 
            talkName.AutoSize = true;
            talkName.Dock = System.Windows.Forms.DockStyle.Bottom;
            talkName.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            talkName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            talkName.Location = new System.Drawing.Point(10, 52);
            talkName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            talkName.Name = "talkName";
            talkName.Size = new System.Drawing.Size(160, 30);
            talkName.TabIndex = 1;
            talkName.Text = "(408) 262-9190";
            // 
            // talkLabel
            // 
            talkLabel.AutoSize = true;
            talkLabel.Dock = System.Windows.Forms.DockStyle.Top;
            talkLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            talkLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            talkLabel.Location = new System.Drawing.Point(10, 5);
            talkLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            talkLabel.Name = "talkLabel";
            talkLabel.Size = new System.Drawing.Size(149, 32);
            talkLabel.TabIndex = 0;
            talkLabel.Text = "Client Name";
            // 
            // bottomPanel
            // 
            bottomPanel.BackColor = System.Drawing.Color.RoyalBlue;
            bottomPanel.Controls.Add(queueButton);
            bottomPanel.Controls.Add(chatTextbox);
            bottomPanel.Controls.Add(attachButton);
            bottomPanel.Controls.Add(sendButton);
            bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            bottomPanel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            bottomPanel.Location = new System.Drawing.Point(0, 1013);
            bottomPanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Padding = new System.Windows.Forms.Padding(28, 19, 28, 19);
            bottomPanel.Size = new System.Drawing.Size(1324, 99);
            bottomPanel.TabIndex = 1;
            // 
            // queueButton
            // 
            queueButton.BackColor = System.Drawing.Color.GhostWhite;
            queueButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("queueButton.BackgroundImage");
            queueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            queueButton.Dock = System.Windows.Forms.DockStyle.Left;
            queueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            queueButton.ForeColor = System.Drawing.SystemColors.ControlText;
            queueButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            queueButton.Location = new System.Drawing.Point(28, 19);
            queueButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            queueButton.Name = "queueButton";
            queueButton.Size = new System.Drawing.Size(64, 61);
            queueButton.TabIndex = 5;
            queueButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            queueButton.UseVisualStyleBackColor = false;
            // 
            // chatTextbox
            // 
            chatTextbox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            chatTextbox.Location = new System.Drawing.Point(90, 19);
            chatTextbox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            chatTextbox.Multiline = true;
            chatTextbox.Name = "chatTextbox";
            chatTextbox.Size = new System.Drawing.Size(1008, 61);
            chatTextbox.TabIndex = 7;
            // 
            // attachButton
            // 
            attachButton.BackColor = System.Drawing.Color.GhostWhite;
            attachButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("attachButton.BackgroundImage");
            attachButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            attachButton.Dock = System.Windows.Forms.DockStyle.Right;
            attachButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            attachButton.ForeColor = System.Drawing.SystemColors.ControlText;
            attachButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            attachButton.Location = new System.Drawing.Point(1094, 19);
            attachButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            attachButton.Name = "attachButton";
            attachButton.Size = new System.Drawing.Size(64, 61);
            attachButton.TabIndex = 6;
            attachButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            attachButton.UseVisualStyleBackColor = false;
            // 
            // sendButton
            // 
            sendButton.BackColor = System.Drawing.Color.RoyalBlue;
            sendButton.Dock = System.Windows.Forms.DockStyle.Right;
            sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            sendButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            sendButton.Location = new System.Drawing.Point(1158, 19);
            sendButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            sendButton.Name = "sendButton";
            sendButton.Size = new System.Drawing.Size(138, 61);
            sendButton.TabIndex = 1;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = false;
            // 
            // itemsPanel
            // 
            itemsPanel.AutoScroll = true;
            itemsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            itemsPanel.Location = new System.Drawing.Point(0, 92);
            itemsPanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            itemsPanel.Name = "itemsPanel";
            itemsPanel.Size = new System.Drawing.Size(1324, 921);
            itemsPanel.TabIndex = 2;
            // 
            // UnicastChatbox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            Controls.Add(itemsPanel);
            Controls.Add(bottomPanel);
            Controls.Add(topPanel);
            Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            Name = "UnicastChatbox";
            Size = new System.Drawing.Size(1324, 1112);
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Label talkName;
		private System.Windows.Forms.Label talkLabel;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.Button attachButton;
		private System.Windows.Forms.TextBox chatTextbox;
        private System.Windows.Forms.Button queueButton;
        private System.Windows.Forms.Panel itemsPanel;
	}
}
