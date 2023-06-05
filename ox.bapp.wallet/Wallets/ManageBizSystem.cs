using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using System.Windows.Forms;
using OX.Bapps;
using OX.Network.P2P.Payloads;

namespace OX.Wallets.Base
{
    public partial class ManageBizSystem : DarkForm, INotecaseTrigger, IModuleComponent
    {
        #region Constructor Region
        public Module Module { get; set; }
        public ManageBizSystem()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("管理业务系统", "Manage Business System");
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
