using OX.Cryptography.ECC;
using OX.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using System.Linq;
using System.Windows.Forms;
using OX.Wallets.UI.Forms;

namespace OX.Wallets.Base
{
    internal partial class VotingDialog : DarkForm
    {
        private UInt160 script_hash;
        OX.Wallets.Wallet Wallet;
        public StateTransaction GetTransaction()
        {
            return Wallet.MakeTransaction(new StateTransaction
            {
                Version = 0,
                Descriptors = new[]
                {
                    new StateDescriptor
                    {
                        Type = StateType.Account,
                        Key = script_hash.ToArray(),
                        Field = "Votes",
                        Value = textBox1.Lines.Select(p => ECPoint.Parse(p, ECCurve.Secp256r1)).ToArray().ToByteArray()
                    }
                }
            });
        }

        public VotingDialog(OX.Wallets.Wallet wallet, UInt160 script_hash)
        {
            InitializeComponent();
            this.Wallet = wallet;
            this.script_hash = script_hash;
            AccountState account = Blockchain.Singleton.Store.GetAccounts().TryGet(script_hash);
            label1.Text = script_hash.ToAddress();
            textBox1.Lines = account.Votes.Select(p => p.ToString()).ToArray();
            this.Text = UIHelper.LocalString("投票", "Voting");
            this.groupBox1.Text = UIHelper.LocalString("候选人", "Candidates");
            this.button1.Text = UIHelper.LocalString("确定", "OK");
            this.button2.Text = UIHelper.LocalString("取消", "Cancel");
        }
    }
}
