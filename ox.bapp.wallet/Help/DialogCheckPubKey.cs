using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using System.Windows.Forms;
using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.Cryptography.ECC;
using OX.SmartContract;

namespace OX.Wallets.Base
{
    public partial class DialogCheckPubKey : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        #region Constructor Region
        public Module Module { get; set; }
        public DialogCheckPubKey()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("公钥查验", "Public Key Check");
            this.lb_pubkey.Text = UIHelper.LocalString("公钥:", "Public Key:");
            this.lb_addr.Text = UIHelper.LocalString("地址:", "Address:");
            this.bt_copy.Text = UIHelper.LocalString("复制", "Copy");
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

        private void tb_pubkey_TextChanged(object sender, System.EventArgs e)
        {
            this.tb_addr.Text = string.Empty;
            try
            {
                if (ECPoint.TryParse(this.tb_pubkey.Text, ECCurve.Secp256r1, out ECPoint pubkey))
                {
                    this.tb_addr.Text = Contract.CreateSignatureRedeemScript(pubkey).ToScriptHash().ToAddress();
                }
            }
            catch { }
        }

        private void bt_copy_Click(object sender, System.EventArgs e)
        {
            string s = this.tb_addr.Text;
            Clipboard.SetText(s);
            string msg = s + UIHelper.LocalString("  已复制", "  copied");
            DarkMessageBox.ShowInformation(msg, "");
        }
    }
}
