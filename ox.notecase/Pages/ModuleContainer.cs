using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.UI.Controls;
using OX.Wallets;
using OX.Network.P2P;
using System.Linq;
using System.Drawing;
using OX.Network;
using OX.Notecase;
using OX.Bapps;

namespace OX.Wallets
{
    public class ModuleContainer : DarkForm, IModuleContainer, INotecaseTrigger
    {
        public DarkDockPanel DockPanel { get; private set; }
        public DarkMenuStrip TopMenus { get; }
        private DarkStatusStrip StripMain;
        public System.Windows.Forms.ToolStripStatusLabel ModuleStatusLabel { get; private set; }
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;

        public List<DarkDockContent> ToolWindows { get; private set; }
        public DockPanelState DockPanelState { get; private set; }

        #region 变量
        private DateTime persistence_time = DateTime.MinValue;

        #endregion
        public ModuleContainer() : base()
        {
            InitializeComponent();
            this.Text = " BOX";
            //this.Text = UPnP.GetExternalIP().ToString();
            this.Size = new System.Drawing.Size(944, 564);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ToolWindows = new List<DarkDockContent>();
            this.DockPanel = new DarkDockPanel();
            this.TopMenus = new DarkMenuStrip();
            this.TopMenus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TopMenus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TopMenus.Location = new System.Drawing.Point(0, 0);
            this.TopMenus.Name = "TopMenus";
            this.TopMenus.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.TopMenus.Size = new System.Drawing.Size(944, 25);
            this.TopMenus.TabIndex = 0;
            this.TopMenus.Text = "menus";
            this.StripMain = new OX.Wallets.UI.Controls.DarkStatusStrip();
            this.StripMain.AutoSize = true;
            this.ModuleStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.TopMenus.SuspendLayout();
            this.StripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripStatusLabel1
            // 
            this.ModuleStatusLabel.AutoSize = false;
            this.ModuleStatusLabel.Margin = new System.Windows.Forms.Padding(1, 0, 50, 0);
            this.ModuleStatusLabel.Name = "ModuleStatusLabel";
            this.ModuleStatusLabel.Size = new System.Drawing.Size(60, 24);
            this.ModuleStatusLabel.Text = UIHelper.LocalString("就绪", "Ready");
            this.ModuleStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Margin = new System.Windows.Forms.Padding(0, 0, 50, 2);
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(757, 24);
            this.toolStripStatusLabel6.Spring = true;
            this.toolStripStatusLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(46, 24);
            this.toolStripStatusLabel5.Text = "BlockChain Height";
            this.toolStripStatusLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.toolStripProgressBar.Maximum = 15;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 24);
            this.toolStripProgressBar.Step = 1;
            this.DockPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.DockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockPanel.Location = new System.Drawing.Point(0, 55);
            this.DockPanel.Name = "DockPanel";
            this.DockPanel.Size = new System.Drawing.Size(944, 563);
            this.DockPanel.TabIndex = 3;
            this.StripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModuleStatusLabel,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel5,
            this.toolStripProgressBar});
            this.Controls.Add(this.DockPanel);
            this.Controls.Add(this.TopMenus);
            this.Controls.Add(this.StripMain);
            this.MainMenuStrip = this.TopMenus;
            this.TopMenus.ResumeLayout(false);
            this.TopMenus.PerformLayout();
            this.StripMain.ResumeLayout(false);
            this.StripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            Application.AddMessageFilter(new ControlScrollFilter());
            Application.AddMessageFilter(this.DockPanel.DockContentDragFilter);
            Application.AddMessageFilter(this.DockPanel.DockResizeFilter);
            this.HookEvents();
            this.LoadModules();
            if (File.Exists("dockpanel.config"))
            {
                DeserializeDockPanel("dockpanel.config");
            }
            else
            {
                foreach (var toolWindow in this.ToolWindows)
                    DockPanel.AddContent(toolWindow);
            }

        }
        void LoadModules()
        {
            ModuleManager.LoadModule(this);
            Bapp.CrossBappMessage += OnCrossBappMessage;
        }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
            if (message.MessageType == 0)
                this.toolStripStatusLabel6.Text = message.Content;
        }

        void HookEvents()
        {
            this.FormClosing += ModuleContainer_FormClosing;
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            SerializeDockPanel("dockpanel.config");
        }

        private void ToggleToolWindow(DarkToolWindow toolWindow)
        {
            if (toolWindow.DockPanel == null)
                DockPanel.AddContent(toolWindow);
            else
                DockPanel.RemoveContent(toolWindow);
        }

        private void ModuleContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializeDockPanel("dockpanel.config");
        }
        #region Serialization Region

        private void SerializeDockPanel(string path)
        {
            var state = DockPanel.GetDockPanelState();
            SerializerHelper.Serialize(state, path);
        }

        private void DeserializeDockPanel(string path)
        {
            DockPanelState = SerializerHelper.Deserialize<DockPanelState>(path);
            if (DockPanelState.IsNotNull())
                DockPanel.RestoreDockPanelState(DockPanelState, GetContentBySerializationKey);
        }

        private DarkDockContent GetContentBySerializationKey(string key)
        {
            foreach (var window in ToolWindows)
            {
                if (window.SerializationKey == key)
                    return window;
            }

            return null;
        }

        #endregion
        #region 主链状态
        public void HeartBeat(HeartBeatContext context)
        {
            uint walletHeight = 0;
            if (NotecaseApp.Instance.Wallet != default)
            {
                walletHeight = NotecaseApp.Instance.Wallet.WalletHeight > 0 ? NotecaseApp.Instance.Wallet.WalletHeight - 1 : 0;
            }
            this.ModuleStatusLabel.Text = LocalNode.Singleton.GetRemoteNodes().Count().ToString() + UIHelper.LocalString(" 节点", " Nodes");
            this.toolStripStatusLabel5.Text = $"{walletHeight}/{Blockchain.Singleton.Height}/{Blockchain.Singleton.HeaderHeight}";
            this.DoInvoke(() =>
            {
                TimeSpan persistence_span = DateTime.UtcNow - persistence_time;
                if (persistence_span < TimeSpan.Zero) persistence_span = TimeSpan.Zero;
                if (persistence_span > Blockchain.TimePerBlock)
                {
                    this.toolStripProgressBar.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    this.toolStripProgressBar.Value = persistence_span.Seconds;
                    this.toolStripProgressBar.Style = ProgressBarStyle.Blocks;
                }
            });
            ModuleManager.BroadCastAction(m =>
            {
                m.HeartBeat(context);
            });
        }

        public void ChangeWallet(INotecase operater)
        {
            ModuleManager.BroadCastAction(m =>
            {
                m.ChangeWallet(operater);
            });
        }

        public void OnBappEvent(BappEvent bappEvent)
        {

        }

        public void BeforeOnBlock(Block block)
        {
        }
        public void OnBlock(Block block)
        {
        }
        public void AfterOnBlock(Block block)
        {
        }
        public void OnRebuild()
        {
        }
        #endregion

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleContainer));
            SuspendLayout();
            // 
            // ModuleContainer
            // 
            ClientSize = new Size(284, 261);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ModuleContainer";
            WindowState = FormWindowState.Maximized;
            Load += ModuleContainer_Load;
            Shown += ModuleContainer_Shown;
            ResumeLayout(false);
        }

        private void ModuleContainer_Load(object sender, EventArgs e)
        {

        }
        System.Timers.Timer timer = default;
        private void ModuleContainer_Shown(object sender, EventArgs e)
        {
            if (WebStarter.Instance.NeedWebService)
            {
                timer = new System.Timers.Timer();
                // 60000ms is one minute
                timer.Interval = 60000;
                timer.Elapsed += DoWebService;
                timer.Start();
            }
        }
        private void DoWebService(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Enabled = false;
            timer.Close();
            timer.Dispose();
            WebStarter.Instance.StartWeb();
        }
    }
}
