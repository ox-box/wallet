using OX.Cryptography.ECC;
using OX.IO;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using System;
using System.Linq;
using System.Windows.Forms;
using OX.Wallets.UI.Forms;

namespace OX.Wallets.Base
{
    public partial class ElectionDialog : DarkForm
    {
        OX.Wallets.Wallet Wallet;
        public ElectionDialog(OX.Wallets.Wallet wallet)
        {
            Wallet = wallet;
            InitializeComponent();
            this.Text = UIHelper.LocalString("选举", "Election");
            this.label1.Text = UIHelper.LocalString("公钥:", "Public Key:");
            this.label2.Text = UIHelper.LocalString("费用:", "Fee:");
        }

        public StateTransaction GetTransaction()
        {
            ECPoint pubkey = (ECPoint)comboBox1.SelectedItem;
            return Wallet.MakeTransaction(new StateTransaction
            {
                Version = 0,
                Descriptors = new[]
                {
                    new StateDescriptor
                    {
                        Type = StateType.Validator,
                        Key = pubkey.ToArray(),
                        Field = "Registered",
                        Value = BitConverter.GetBytes(true)
                    }
                }
            });
        }

        private void ElectionDialog_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Wallet.GetHeldAccounts().Where(p => p.Contract.Script.IsStandardContract()).Select(p => p.GetKey().PublicKey).ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                button1.Enabled = true;
                ECPoint pubkey = (ECPoint)comboBox1.SelectedItem;
                StateTransaction tx = new StateTransaction
                {
                    Version = 0,
                    Descriptors = new[]
                    {
                        new StateDescriptor
                        {
                            Type = StateType.Validator,
                            Key = pubkey.ToArray(),
                            Field = "Registered",
                            Value = BitConverter.GetBytes(true)
                        }
                    }
                };
                label3.Text = $"{tx.SystemFee} OXC";
            }
        }
    }
}
