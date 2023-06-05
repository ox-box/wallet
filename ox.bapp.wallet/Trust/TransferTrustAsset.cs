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
using OX.Wallets.UI.Controls;

namespace OX.Wallets.Base
{
    public partial class TransferTrustAsset : DarkForm, INotecaseTrigger, IModuleComponent
    {
        public class ScriptHashDescriptor
        {
            public UInt160 ScriptHash;
            public override string ToString()
            {
                return ScriptHash.ToAddress();
            }
        }
        public class AssetBalanceDescriptor
        {
            public string AssetName;
            public UInt256 AssetId;
            public Fixed8 Balance;
            public override string ToString()
            {
                return $"{Balance.ToString()}           {AssetName}   /   {AssetId.ToString()}";
            }
        }
        INotecase Operater;
        public Module Module { get; set; }
        ECPoint PubKey = default;
        UInt160 TrustAddress;
        AssetTrustContract AssetTrustContract;
        public TransferTrustAsset(INotecase operater, ECPoint pubkey, UInt160 trustAddress, AssetTrustContract assetTrustContract)
        {
            InitializeComponent();
            this.Operater = operater;
            this.PubKey = pubkey;
            this.TrustAddress = trustAddress;
            this.AssetTrustContract = assetTrustContract;
        }


        private void NewEvent_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("信托合约转帐", "Transfer from Trust Contract");
            this.lb_target.Text = UIHelper.LocalString("委托账户:", "Truster:");
            this.lb_amount.Text = UIHelper.LocalString("转帐金额:", "Amount:");
            this.bt_OK.Text = UIHelper.LocalString("立即转帐", "Transfer Now");
            this.bt_Close.Text = UIHelper.LocalString("关闭", "Close");
            this.lb_asset.Text = UIHelper.LocalString("资产余额:", "Asset Balance:");
            foreach (var sh in this.AssetTrustContract.Targets)
            {
                this.cbTargets.Items.Add(new ScriptHashDescriptor { ScriptHash = sh });
            }
            this.cbTargets.SelectedIndex = 0;
            var acts = Blockchain.Singleton.CurrentSnapshot.Accounts.GetAndChange(this.TrustAddress, () => null);
            if (acts.IsNotNull())
            {
                foreach (var b in acts.Balances)
                {
                    var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(b.Key);
                    if (assetState.IsNotNull())
                    {
                        this.cbAssets.Items.Add(new AssetBalanceDescriptor { AssetId = b.Key, AssetName = assetState.GetName(), Balance = b.Value });
                    }
                }
            }
            if (this.cbAssets.Items.Count > 0)
                this.cbAssets.SelectedIndex = 0;
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
        }




        private void bt_OK_Click(object sender, EventArgs e)
        {
            var provider = WalletBappProvider.Instance;
            if (provider.IsNull()) return;
            if (!Fixed8.TryParse(this.tb_amount.Text, out Fixed8 amount)) return;
            var obj = this.cbTargets.SelectedItem;
            if (obj.IsNull()) return;
            ScriptHashDescriptor shd = obj as ScriptHashDescriptor;
            obj = this.cbAssets.SelectedItem;
            if (obj.IsNull()) return;
            AssetBalanceDescriptor abd = obj as AssetBalanceDescriptor;
            if (amount > abd.Balance) return;
            var actSH = Contract.CreateSignatureRedeemScript(this.PubKey).ToScriptHash();
            var act = this.Operater.Wallet.GetAccount(actSH);
            if (act.IsNotNull())
            {
                var key = act.GetKey();
                var account = LockAssetHelper.CreateAccount(this.Operater.Wallet as OpenWallet, this.AssetTrustContract.GetContract(), key);//lock asset account have a some private key with master account
                if (account != null)
                {
                    List<UTXO> utxos = new List<UTXO>();
                    foreach (var r in provider.GetAssetTrustUTXOs(this.TrustAddress, abd.AssetId))
                    {
                        utxos.Add(new UTXO
                        {
                            Address = r.Value.ScriptHash,
                            Value = r.Value.Value.GetInternalValue(),
                            TxId = r.Key.TxId,
                            N = r.Key.N
                        });
                    }
                    List<string> excludedUtxoKeys = new List<string>();
                    if (utxos.SortSearch(amount.GetInternalValue(), excludedUtxoKeys, out UTXO[] selectedUtxos, out long remainder))
                    {
                        List<TransactionOutput> outputs = new List<TransactionOutput>();
                        outputs.Add(new TransactionOutput { AssetId = abd.AssetId, Value = amount, ScriptHash = shd.ScriptHash });
                        if (remainder > 0)
                        {
                            outputs.Add(new TransactionOutput { AssetId = abd.AssetId, Value = new Fixed8(remainder), ScriptHash = this.TrustAddress });
                        }
                        List<CoinReference> inputs = new List<CoinReference>();
                        foreach (var utxo in selectedUtxos)
                        {
                            inputs.Add(new CoinReference { PrevHash = utxo.TxId, PrevIndex = utxo.N });
                        }
                        ContractTransaction tx = new ContractTransaction
                        {
                            Attributes = new TransactionAttribute[] { new TransactionAttribute { Usage = TransactionAttributeUsage.RelatedPublicKey, Data = this.AssetTrustContract.Truster.EncodePoint(true) } },
                            Outputs = outputs.ToArray(),
                            Inputs = inputs.ToArray(),
                            Witnesses = new Witness[0]
                        };
                        tx = LockAssetHelper.Build(tx, new AvatarAccount[] { account });
                        if (tx.IsNotNull())
                        {
                            this.Operater.Wallet.ApplyTransaction(tx);
                            this.Operater.Relay(tx);
                            this.AssetTrustContract.LastTransferIndex = Blockchain.Singleton.Height;
                            if (this.Operater != default)
                            {
                                string msg = UIHelper.LocalString($"广播信托转帐交易成功  {tx.Hash}", $"Relay transfer trust asset transaction completed  {tx.Hash}");
                                //Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                                DarkMessageBox.ShowInformation(msg, "");
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
