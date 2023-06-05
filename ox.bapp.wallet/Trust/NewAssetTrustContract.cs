using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;
using OX.SmartContract;
using OX.IO;
using System.Xml;
using OX.Bapps;
using OX.Cryptography;
using System.IO;
using NBitcoin.OpenAsset;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OX.Cryptography.ECC;
using Akka.IO;
using static NBitcoin.Scripting.OutputDescriptor;
using OX.Wallets.UI.Controls;

namespace OX.Wallets.Base
{
    public partial class NewAssetTrustContract : DarkForm, INotecaseTrigger, IModuleComponent
    {
        public class AccountDescriptor
        {
            public WalletAccount Account;
            public override string ToString()
            {
                return Account.Address;
            }
        }
        INotecase Operater;
        public Module Module { get; set; }
        ECPoint Trustee = default;
        List<DarkCheckBox> cts = new List<DarkCheckBox>();
        public NewAssetTrustContract(INotecase operater)
        {
            InitializeComponent();
            this.Operater = operater;
        }


        private void NewEvent_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("创建信托合约", "New Trust Contract");
            this.lb_truster.Text = UIHelper.LocalString("委托账户:", "Truster:");
            this.bt_add.Text = UIHelper.LocalString("增加", "Add");
            this.bt_OK.Text = UIHelper.LocalString("立即创建", "Create Now");
            this.bt_Close.Text = UIHelper.LocalString("关闭", "Close");
            this.lb_trusteePubKey.Text = UIHelper.LocalString("受托公钥:", "Trustee Public Key:");
            this.lb_main_scope.Text = UIHelper.LocalString("主信托范围:", "Main Trust Scope:");
            this.lb_side_scope.Text = UIHelper.LocalString("边际信托范围:", "Side Trust Scope:");
            this.lb_balance.Text = UIHelper.LocalString("OXC余额:", "OXC Balance:");
            this.lb_amount.Text = UIHelper.LocalString("委托金额:", "Trust Amount:");
            this.lb_trustAddr.Text = UIHelper.LocalString("信托地址:", "Trust Address:");
            this.bt_copy.Text = UIHelper.LocalString("复制", "Copy");
            foreach (var act in this.Operater.Wallet.GetHeldAccounts())
            {
                this.cbAccounts.Items.Add(new AccountDescriptor { Account = act });
            }
            this.cbAccounts.SelectedIndex = 0;

            foreach (var bapp in Bapp.AllBapps)
            {
                var sps = bapp.GetSideScopes();
                if (sps.IsNotNullAndEmpty())
                {
                    foreach (var ss in sps)
                    {
                        var dcb = new DarkCheckBox { Tag = ss, Text = $"{ss.MasterAddress.ToAddress()}    {ss.Description}", Checked = false, Width = pl_side_scope.Width };
                        cts.Add(dcb);
                        this.pl_side_scope.Controls.Add(dcb);
                        dcb.CheckedChanged += Dcb_CheckedChanged;
                    }
                }
            }
        }

        private void Dcb_CheckedChanged(object sender, EventArgs e)
        {
            getTrustAddress();
        }

        bool refreshBlance(out AccountDescriptor ad)
        {
            ad = default;
            var item = this.cbAccounts.SelectedItem;
            if (item.IsNotNull())
            {
                ad = item as AccountDescriptor;
                this.tb_balance.Text = this.Operater.Wallet.GeAccountAvailable(ad.Account.ScriptHash, Blockchain.OXC).ToString();
                return true;
            }
            return false;
        }
        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
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
            this.Operater = operater;
        }
        public void OnRebuild()
        {
        }







        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bt_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (refreshBlance(out AccountDescriptor ad))
            {
                tryAddTarget(ad.Account.ScriptHash);
                getTrustAddress();
            }
        }

        private void tb_trusteePubkey_TextChanged(object sender, EventArgs e)
        {
            this.Trustee = default;
            this.lb_trusteeAddress.Text = string.Empty;
            try
            {
                if (ECPoint.TryParse(this.tb_trusteePubkey.Text, ECCurve.Secp256r1, out this.Trustee))
                {
                    this.lb_trusteeAddress.Text = Contract.CreateSignatureRedeemScript(this.Trustee).ToScriptHash().ToAddress();
                }
            }
            catch { }
            getTrustAddress();
        }
        void tryAddTarget(UInt160 target)
        {
            var item = this.dlv_addrs.Items.Where(m =>
            {
                var addr = m.Tag as UInt160;
                return addr == target;
            });
            if (item.IsNullOrEmpty())
            {
                this.dlv_addrs.Items.Add(new UI.Controls.DarkListItem { Text = target.ToAddress(), Tag = target });
            }
        }
        private void bt_add_Click(object sender, EventArgs e)
        {
            try
            {
                var sh = this.tb_addr.Text.ToScriptHash();
                tryAddTarget(sh);
                this.tb_addr.Clear();
            }
            catch
            {

            }
            getTrustAddress();
        }

        private void dlv_addrs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var list = this.dlv_addrs.SelectedIndices;
            if (list.IsNotNullAndEmpty() && list.Count == 1)
            {
                this.dlv_addrs.Items.RemoveAt(list[0]);
            }
        }
        void getTrustAddress()
        {
            this.tb_trustAddr.Text = string.Empty;
            var obj = this.cbAccounts.SelectedItem;
            if (obj.IsNull()) return;
            AccountDescriptor ad = obj as AccountDescriptor;
            if (this.tb_trusteePubkey.Text.IsNullOrEmpty()) return;
            if (!ECPoint.TryParse(this.tb_trusteePubkey.Text, ECCurve.Secp256r1, out ECPoint trusteePub)) return;
            var shs = this.dlv_addrs.Items.Select(m => m.Tag as UInt160).OrderBy(p => p).ToArray();
            List<UInt160> sideScopes = new List<UInt160>();
            foreach (var c in this.cts)
            {
                if (c.Checked)
                {
                    var ss = c.Tag as SideScope;
                    sideScopes.Add(ss.MasterAddress);
                }
            }
            AssetTrustTransaction att = new AssetTrustTransaction
            {
                TrustContract = Blockchain.TrustAssetContractScriptHash,
                IsMustRelateTruster = true,
                Truster = ad.Account.GetKey().PublicKey,
                Trustee = trusteePub,
                Targets = shs,
                SideScopes = sideScopes.OrderBy(p => p).ToArray()
            };
            this.tb_trustAddr.Text = att.GetContract().ScriptHash.ToAddress();
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            var obj = this.cbAccounts.SelectedItem;
            if (obj.IsNull()) return;
            AccountDescriptor ad = obj as AccountDescriptor;
            if (!Fixed8.TryParse(this.tb_balance.Text, out Fixed8 balance)) return;
            if (Fixed8.TryParse(this.tb_amount.Text, out Fixed8 amount) && amount <= balance && Trustee.IsNotNull() && this.dlv_addrs.Items.Count > 0)
            {
                var shs = this.dlv_addrs.Items.Select(m => m.Tag as UInt160).OrderBy(p => p).ToArray();
                List<UInt160> sideScopes = new List<UInt160>();
                foreach (var c in this.cts)
                {
                    if (c.Checked)
                    {
                        var ss = c.Tag as SideScope;
                        sideScopes.Add(ss.MasterAddress);
                    }
                }
                AssetTrustTransaction att = new AssetTrustTransaction
                {
                    TrustContract = Blockchain.TrustAssetContractScriptHash,
                    IsMustRelateTruster = true,
                    Truster = ad.Account.GetKey().PublicKey,
                    Trustee = this.Trustee,
                    Targets = shs,
                    SideScopes = sideScopes.OrderBy(p => p).ToArray()
                };
                TransactionOutput output = new TransactionOutput { AssetId = Blockchain.OXC, ScriptHash = att.GetContract().ScriptHash, Value = amount };
                att.Outputs = new TransactionOutput[] { output };
                att = this.Operater.Wallet.MakeTransaction(att, ad.Account.ScriptHash, ad.Account.ScriptHash);
                if (att != null)
                {
                    if (att.Inputs.Count() > 20)
                    {
                        string msg = $"{UIHelper.LocalString("交易输入项太多,请分为多次转账", "There are too many transaction input. Please transfer multiple times")}";
                        Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                        DarkMessageBox.ShowInformation(msg, "");
                        return;
                    }
                    this.Operater.SignAndSendTx(att);
                    if (this.Operater != default)
                    {
                        string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {att.Hash}";
                        //Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                        DarkMessageBox.ShowInformation(msg, "");
                        this.Close();
                    }
                }
            }
        }

        private void bt_copy_Click(object sender, EventArgs e)
        {
            var s = this.tb_trustAddr.Text;
            if (s.IsNotNullAndEmpty())
            {
                Clipboard.SetText(s);
                string msg = s + UIHelper.LocalString("  已复制", "  copied");
                //Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
        }
    }
}
