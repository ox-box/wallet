using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using System.Windows.Forms;
using OX.Bapps;
using OX.Network.P2P.Payloads;

namespace OX.Wallets.Base
{
    public partial class DialogAbout : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        #region Constructor Region
        public Module Module { get; set; }
        public DialogAbout()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("关于", "About");
            this.darkLabel1.Text = UIHelper.LocalString("OX-ECO 钱包 OX BOX", "OX-ECO Wallet OX BOX");
            this.darkLabel2.Text = UIHelper.LocalString("基于区块链技术的去中心化价值生态", "Decentralized value ecology based on blockchain technology");
            var kernelVersion = OX.Bapps.Bapp.KernelVersion;
            var appVersion = typeof(OpenWallet).Assembly.GetName().Version.ToString(3);

            lblVersion.Text = UIHelper.LocalString($"应用版本: {appVersion}         内核版本:{kernelVersion}", $"Application Version: {appVersion}         Kernel Version:{kernelVersion}");
            btnOk.Text = UIHelper.LocalString("关闭", "Close");
        }

        #endregion
        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void HeartBeat(HeartBeatContext context)
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
        public void ChangeWallet(INotecase operater)
        {

        }
        public void OnRebuild()
        {
        }
    }
}
