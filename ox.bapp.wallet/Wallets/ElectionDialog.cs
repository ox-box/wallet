using OX.Cryptography.ECC;
using OX.IO;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using System;
using System.Linq;
using System.Windows.Forms;
using OX.Wallets.UI.Forms;
using Nethereum.Model;

namespace OX.Wallets.Base
{
    public partial class ElectionDialog : DarkDialog
    {
        WalletAccount Account;
        StateTransaction TX;
        public ElectionDialog(WalletAccount account)
        {
            Account = account;
            InitializeComponent();

        }

        public StateTransaction GetTransaction()
        {
            return TX;
        }

        private void ElectionDialog_Load(object sender, EventArgs e)
        {
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.btnCancel.Text= UIHelper.LocalString("取消", "Cancel");
            this.TX = new StateTransaction
            {
                Version = 0,
                Descriptors = new[]
                  {
                    new StateDescriptor
                    {
                        Type = StateType.Validator,
                        Key = this.Account.GetKey().PublicKey.ToArray(),
                        Field = "Registered",
                        Value = BitConverter.GetBytes(true)
                    }
                }
            };
            this.Text = UIHelper.LocalString("选举", "Election");
            this.lb_pubkey.Text = UIHelper.LocalString($"公钥:    {this.Account.GetKey().PublicKey.ToString()}", $"public key:    {this.Account.GetKey().PublicKey.ToString()}");
            this.label3.Text = UIHelper.LocalString($"费用:    {this.TX.SystemFee} OXC", $"Fee:    {this.TX.SystemFee} OXC");
        }
    }
}
