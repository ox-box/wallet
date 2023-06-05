using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using System.Windows.Forms;
using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.Cryptography.ECC;
using OX.SmartContract;
using OX.Ledger;

namespace OX.Wallets.Base
{
    public partial class DialogTrustAssetBalance : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        #region Constructor Region
        public Module Module { get; set; }
        UInt160 SH;
        public DialogTrustAssetBalance(UInt160 sh)
        {
            this.SH = sh;
            InitializeComponent();
            this.Text = UIHelper.LocalString($"{this.SH.ToAddress()}  余额明细", $"{this.SH.ToAddress()}  Balance Details");
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



        private void DialogTrustAssetBalance_Load(object sender, System.EventArgs e)
        {
            var acts = Blockchain.Singleton.CurrentSnapshot.Accounts.GetAndChange(this.SH, () => null);
            if (acts.IsNotNull())
            {
                foreach (var b in acts.Balances)
                {
                    var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(b.Key);
                    if (assetState.IsNotNull())
                    {
                        DarkListItem item = new DarkListItem { Tag = b, Text = $"{b.Value.ToString()}           {assetState.GetName()}   /   {b.Key.ToString()}" };
                        this.darkListView1.Items.Add(item);
                    }
                }
            }
        }
    }
}
