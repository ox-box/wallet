using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Cryptography;
using OX.Cryptography.ECC;
using OX.Network.P2P.Payloads;
using System.Diagnostics;
using OX.Wallets.UI.Config;

namespace OX.Wallets.Flash
{
    public partial class NewTalkLine : DarkDialog
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
        ECPoint Remote;
        public int TabSelectedIndex { get; private set; } = 0;
        public NewTalkLine(INotecase operater)
        {
            InitializeComponent();
            this.Operater = operater;
        }


        private void NewLetter_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("创建线路", "New talk line");
            this.lb_remote.Text = UIHelper.LocalString("收信公钥:", "Recipient Public Key:");
            this.lb_local.Text = UIHelper.LocalString("发信账户:", "Sender Account:");
            this.btnOk.Text = UIHelper.LocalString("创建", "Create");
            this.btnCancel.Text = UIHelper.LocalString("关闭", "Close");
            this.tab_talkline.SelectedTabTextColor = Colors.BlueSelection;
            this.tab_talkline.SelectedIndex = 0;
            this.tp_uni.Text = UIHelper.LocalString("私聊线路", "Unicast Line");
            this.tp_multi.Text = UIHelper.LocalString("群聊线路", "Multicast Line");
            this.lb_local.Text = UIHelper.LocalString("本地账户:", "Local Account:");
            this.lb_local_2.Text = UIHelper.LocalString("本地账户:", "Local Account:");
            this.lb_remote.Text = UIHelper.LocalString("对端公钥:", "Peer Public Key:");
            this.lb_remote_2.Text = UIHelper.LocalString("共享密钥:", "Share Key:");
            this.lb_name.Text = UIHelper.LocalString("线路名称:", "Line Name:");
            this.lb_name_2.Text = UIHelper.LocalString("线路名称:", "Line Name:");
            this.bt_newKey.Text = UIHelper.LocalString("新建", "New");
            this.tp_uni.BackColor = OX.Wallets.UI.Config.Colors.GreyBackground;
            this.tp_multi.BackColor = OX.Wallets.UI.Config.Colors.GreyBackground;
            initAccounts();
        }
        void initAccounts()
        {
            if (this.Operater.IsNotNull())
            {
                this.DoInvoke(() =>
                {
                    this.cbAccounts.Items.Clear();
                    this.cbAccounts_2.Items.Clear();
                    foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                    {
                        this.cbAccounts.Items.Add(new AccountDescriptor { Account = act });
                        this.cbAccounts_2.Items.Add(new AccountDescriptor { Account = act });
                    }
                    this.cbAccounts.SelectedIndex = 0;
                    this.cbAccounts_2.SelectedIndex = 0;
                });
            }
        }




        private void tb_to_TextChanged(object sender, EventArgs e)
        {
            this.Remote = default;
            ECPoint.TryParse(this.tb_remote.Text, ECCurve.Secp256r1, out this.Remote);
        }

        private void tp_uni_Click(object sender, EventArgs e)
        {

        }

        private void tab_talkline_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabSelectedIndex = this.tab_talkline.SelectedIndex;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (TabSelectedIndex == 0)
            {
                var ad = cbAccounts.SelectedItem as AccountDescriptor;
                if (ad.IsNotNull() && this.Remote.IsNotNull())
                {
                    FlashMessageProvider.Instance.SaveUnicastTalkLine(ad.Account, this.Remote, this.tb_name.Text);
                    DarkMessageBox.ShowInformation(UIHelper.LocalString("创建私聊线路成功", "Successfully created unicast line"), "");
                }
            }
            else
            {
                var ad = cbAccounts_2.SelectedItem as AccountDescriptor;
                if (ad.IsNotNull())
                {
                    try
                    {
                        var keys = this.tb_remote_2.Text.HexToBytes();
                        FlashMessageProvider.Instance.SaveMulticastTalkLine(ad.Account.ScriptHash, keys, this.tb_name_2.Text);
                        DarkMessageBox.ShowInformation(UIHelper.LocalString("创建群聊线路成功", "Successfully created multicast line"), "");
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void bt_newKey_Click(object sender, EventArgs e)
        {
            OX.Cryptography.ECDiffieHellmanHelper.CreateRandomKey(out string pk);
            this.tb_remote_2.Text = pk;
        }
    }
}
