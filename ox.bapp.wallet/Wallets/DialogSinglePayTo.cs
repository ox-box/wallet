using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Wallets;
using OX.Ledger;
using OX.Wallets.Base.Wallets;
using OX.Wallets.UI.Config;
using OX.Cryptography.ECC;
using OX.SmartContract;
using OX.Network.P2P.Payloads;
using Nethereum.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OX.Bapps;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using NBitcoin.OpenAsset;

namespace OX.Wallets.Base
{
    public partial class DialogSinglePayTo : DarkDialog
    {
        public class AssetDesc
        {
            public AssetState AssetState;
            public override string ToString()
            {
                return $"{AssetState.GetName()}   /   {AssetState.AssetId.ToString()}";
            }
        }
        INotecase Operater;
        int TabSelectedIndex = 0;
        bool cancelSelected = false;
        TransferRequest TransferRequest = default;
        public DialogSinglePayTo()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("转帐", "Transfer");
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.lb_asset.Text = UIHelper.LocalString("资产:", "Asset:");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_balance.Text = UIHelper.LocalString("可用余额:", "Available Balance:");

            this.lb_native_targetAddress.Text = UIHelper.LocalString("地址:", "Address:");

            this.lb_lock_targetPubkey.Text = UIHelper.LocalString("收款公钥:", "Public Key:");
            this.cb_lockself.Text = UIHelper.LocalString("自主锁仓", "Lock Self");
            this.lb_lock_type.Text = UIHelper.LocalString("锁仓类型:", "Lock Type:");
            this.rbTime.Text = UIHelper.LocalString("解锁时间:", "Unlock Time:");
            this.rbBlock.Text = UIHelper.LocalString("解锁区块:", "Unlock Block:");

            this.lb_eth_targetAddress.Text = UIHelper.LocalString("以太坊地址:", "Eth Address:");
            this.lb_eth_lockindex.Text = UIHelper.LocalString("锁仓高度:", "Lock Height:");

            this.tab_native.Text = UIHelper.LocalString("原生转帐", "Native Transfer");
            this.tab_lock.Text = UIHelper.LocalString("锁仓转帐", "Lock Transfer");
            this.tab_eth.Text = UIHelper.LocalString("转帐到以太坊地址", "Transfer to Ethereum address");
            this.tab_native.BackColor = Colors.GreyBackground;
            this.tab_lock.BackColor = Colors.GreyBackground;
            this.tab_eth.BackColor = Colors.GreyBackground;
            this.tab_target.SelectedTabTextColor = Colors.BlueSelection;
            this.tab_target.SelectedIndex = TabSelectedIndex;
            btnOk.Enabled = false;
        }

        public WalletAccount Account;
        public DialogSinglePayTo(INotecase operater, WalletAccount account, UInt160 to = null) : this()
        {
            this.Operater = operater;
            this.Account = account;
            if (to.IsNotNull())
            {
                tb_native_targetAddress.Text = to.ToAddress();
                tb_native_targetAddress.ReadOnly = true;
            }
        }
        public DialogSinglePayTo(INotecase operater, TransferRequest transferRequest) : this()
        {
            this.TransferRequest = transferRequest;
            this.cancelSelected = true;
            this.Operater = operater;
            if (transferRequest.To.IsNotNull())
            {
                tb_native_targetAddress.Text = transferRequest.To.ToAddress();
                tb_native_targetAddress.ReadOnly = true;
            }
            if (transferRequest.Asset.IsNotNull())
            {
                foreach (var item in this.cb_assets.Items)
                {
                    if (item is AssetDesc assetDesc && assetDesc.AssetState.AssetId == transferRequest.Asset)
                    {
                        this.cb_assets.SelectedItem = assetDesc;
                        break;
                    }
                }
            }
            if (transferRequest.Amount != default)
            {
                tb_amount.Text = transferRequest.Amount.ToString();
            }
        }
        TransactionOutput buildTransactionOutput(out ECPoint recipient, out uint ethLockIndex)
        {
            recipient = default;
            ethLockIndex = 0;
            UInt256 AssetID = SelectedAssetID(out string AssetName);
            if (AssetID.IsNull()) return default;
            var amount = Fixed8.Parse(tb_amount.Text);
            switch (TabSelectedIndex)
            {
                case 0:
                    try
                    {
                        var sh = tb_native_targetAddress.Text.ToScriptHash();
                        return new TransactionOutput { AssetId = AssetID, Value = amount, ScriptHash = sh };
                    }
                    catch
                    {
                        return default;
                    }
                case 1:
                    try
                    {
                        recipient = ECPoint.Parse(this.tb_lock_targetPubkey.Text, ECCurve.Secp256r1);

                        return new TransactionOutput
                        {
                            AssetId = AssetID,
                            Value = amount,
                            ScriptHash = Contract.CreateSignatureRedeemScript(recipient).ToScriptHash()
                        };
                    }
                    catch
                    {
                        return default;
                    }
                case 2:
                    try
                    {
                        var ethAddress = tb_eth_targetAddress.Text;
                        ethLockIndex = uint.Parse(this.tb_eth_lockIndex.Text);
                        return new TransactionOutput { AssetId = AssetID, Value = amount, ScriptHash = ethAddress.BuildMapAddress(ethLockIndex) };
                    }
                    catch
                    {
                        return default;
                    }
            }
            return default;
        }
        public Transaction BuildTransaction()
        {
            var output = buildTransactionOutput(out ECPoint recipient, out uint ethLockIndex);
            switch (TabSelectedIndex)
            {
                case 0:
                    return new ContractTransaction { Outputs = new[] { output } };
                case 1:
                    var isTime = this.rbTime.Checked;
                    uint expiration = 0;

                    if (isTime)
                    {
                        expiration = this.dtp_time.Value.ToTimestamp();
                        if (expiration - DateTime.Now.ToTimestamp() < 150)
                        {
                            string msg = $"{UIHelper.LocalString("锁仓的时间太短", "Locking time is too short")}";
                            //Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                            return default;
                        }
                    }
                    else
                    {
                        expiration = uint.Parse(this.tb_block.Text);
                        if (expiration - Blockchain.Singleton.Height < 10)
                        {
                            string msg = $"{UIHelper.LocalString("锁仓的区块高度太低", "Locked block height is too low")}";
                            //Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                            return default;
                        }
                    }
                    LockAssetTransaction lat = new LockAssetTransaction
                    {
                        LockContract = Blockchain.LockAssetContractScriptHash,
                        IsTimeLock = isTime,
                        LockExpiration = expiration,
                        Flag = 0,
                        Recipient = recipient
                    };
                    output.ScriptHash = lat.GetContract().ScriptHash;
                    lat.Outputs = new TransactionOutput[] { output };
                    return lat;
                case 2:
                    return new EthereumMapTransaction
                    {
                        EthereumAddress = tb_eth_targetAddress.Text,
                        LockExpirationIndex = ethLockIndex,
                        EthMapContract = Blockchain.EthereumMapContractScriptHash,
                        Outputs = new TransactionOutput[] { output }
                    };
            }
            return default;
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!Fixed8.TryParse(tb_amount.Text, out Fixed8 amount))
            {
                btnOk.Enabled = false;
                return;
            }
            if (!Fixed8.TryParse(tb_balance.Text, out Fixed8 balance))
            {
                btnOk.Enabled = false;
                return;
            }
            if (amount > balance)
            {
                btnOk.Enabled = false;
                return;
            }
            if (amount == Fixed8.Zero)
            {
                btnOk.Enabled = false;
                return;
            }
            btnOk.Enabled = true;
        }


        private void RefreshBalance()
        {
            UInt256 assetid = SelectedAssetID(out string AssetName);
            if (assetid.IsNotNull())
            {
                if (this.Operater.Wallet.TryGetWalletAccountBalance(this.Account.ScriptHash, out Dictionary<UInt256, WalletAccountBalance> balances) && balances.TryGetValue(assetid, out WalletAccountBalance balance))
                {
                    tb_balance.Text = balance.AvailableBalance.ToString();
                    textBox_TextChanged(this, EventArgs.Empty);
                }
            }
        }
        private UInt256 SelectedAssetID(out string AssetName)
        {
            AssetName = string.Empty;
            var item = this.cb_assets.SelectedItem;
            if (item.IsNull()) return default;
            if (item is AssetDesc assetDesc)
            {
                return assetDesc.AssetState.AssetId;
            }
            return default;
        }

        private void PayToDialog_Load(object sender, EventArgs e)
        {
            if (this.Operater.Wallet.TryGetWalletAccountBalance(this.Account.ScriptHash, out Dictionary<UInt256, WalletAccountBalance> balances))
            {
                foreach (var balance in balances.OrderByDescending(m => m.Key == Blockchain.OXS).ThenByDescending(m => m.Key == Blockchain.OXC))
                {
                    if (this.TransferRequest.IsNull() || this.TransferRequest.Asset.Equals(balance.Key))
                    {
                        var assetState = Blockchain.Singleton.Store.GetAssets().TryGet(balance.Key);
                        if (assetState.IsNotNull())
                            this.cb_assets.Items.Add(new AssetDesc { AssetState = assetState });
                    }
                }
            }
            
            RefreshBalance();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_assets_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshBalance();
        }

        private void tab_target_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var tab = this.tab_target.SelectedTab;
            TabSelectedIndex = this.tab_target.SelectedIndex;
        }

        private void tab_target_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = cancelSelected;
        }

        private void rbBlock_CheckedChanged(object sender, EventArgs e)
        {
            this.dtp_time.Visible = this.rbTime.Checked;
            this.tb_block.Visible = this.rbBlock.Checked;
        }

        private void cb_lockself_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_lockself.Checked)
            {
                this.tb_lock_targetPubkey.ReadOnly = true;
                this.tb_lock_targetPubkey.Text = this.Account.GetKey().PublicKey.ToString();
            }
            else
            {
                this.tb_lock_targetPubkey.Text = string.Empty;
                this.lb_lock_targetAddress.Text = string.Empty;
                this.tb_lock_targetPubkey.ReadOnly = false;
            }
        }

        private void tb_lock_targetPubkey_TextChanged(object sender, EventArgs e)
        {
            this.lb_lock_targetAddress.Text = String.Empty;
            var ecp = ECPoint.Parse(tb_lock_targetPubkey.Text, ECCurve.Secp256r1);
            this.lb_lock_targetAddress.Text = Contract.CreateSignatureRedeemScript(ecp).ToScriptHash().ToAddress();
        }

        private void tb_block_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as UI.Controls.DarkTextBox;
            if (!uint.TryParse(tb.Text, out uint index))
            {
                var s = tb.Text;
                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    tb.Clear();
                    tb.AppendText(s);
                }
            }
        }

        private void tb_eth_targetAddress_TextChanged(object sender, EventArgs e)
        {
            this.lb_mapaddress.Text = string.Empty;
            if (this.tb_eth_targetAddress.Text.IsNotNullAndEmpty() && tb_eth_targetAddress.Text.IsValidEthereumAddressHexFormat())
            {
                var sh = this.tb_eth_targetAddress.Text.BuildMapAddress();
                this.lb_mapaddress.Text = sh.ToAddress();
            }
        }


    }
}
