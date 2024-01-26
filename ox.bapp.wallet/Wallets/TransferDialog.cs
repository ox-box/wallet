using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.VM;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using VMArray = OX.VM.Types.Array;
using OX.Bapps;

namespace OX.Wallets.Base
{
    public partial class TransferDialog : OX.Wallets.UI.Forms.DarkForm, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        public INotecase Operater;
        private string remark = "";

        public Fixed8 Fee => Fixed8.Parse(textBoxFee.Text);
        public UInt160 ChangeAddress => ((string)comboBoxChangeAddress.SelectedItem).ToScriptHash();
        public UInt160 FromAddress;

        public TransferDialog(INotecase operater)
        {
            this.Operater = operater;
            InitializeComponent();
            this.txOutListBox1.Operater = operater;
            textBoxFee.Text = "0";
            this.Text = UIHelper.LocalString("合并转账", "Merge Transfer");
            this.groupBox1.Text = UIHelper.LocalString("高级", "Advanced");
            this.groupBox1.ForeColor = Color.White;
            this.groupBox3.Text = UIHelper.LocalString("收款人列表", "Recipient List");
            this.groupBox3.ForeColor = Color.White;
            this.button2.Text = UIHelper.LocalString("高级", "Advanced");
            this.labelFrom.Text = UIHelper.LocalString("付款地址:", "From:");
            this.labelFrom.ForeColor = Color.White;
            this.labelFee.Text = UIHelper.LocalString("手续费:", "Fee:");
            this.labelFee.ForeColor = Color.White;
            this.labelChangeAddress.Text = UIHelper.LocalString("找零地址:", "Change Address:");
            this.labelChangeAddress.ForeColor = Color.White;
            this.button3.Text = UIHelper.LocalString("确定", "OK");
            this.button4.Text = UIHelper.LocalString("取消", "Cancel");
            comboBoxChangeAddress.Items.AddRange(this.Operater.Wallet.GetHeldAccounts().Select(p => p.Address).ToArray());
            comboBoxChangeAddress.SelectedItem = this.Operater.Wallet.GetChangeAddress().ToAddress();
            comboBoxFrom.Items.AddRange(this.Operater.Wallet.GetHeldAccounts().Select(p => p.Address).ToArray());
        }
        public static bool CostRemind(Fixed8 SystemFee, Fixed8 NetFee)
        {
            NetFeeDialog frm = new NetFeeDialog(SystemFee, NetFee);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Transaction GetTransaction()
        {
            var cOutputs = txOutListBox1.Items.Where(p => p.AssetId is UInt160).GroupBy(p => new
            {
                AssetId = (UInt160)p.AssetId,
                Account = p.ScriptHash
            }, (k, g) => new
            {
                k.AssetId,
                Value = g.Aggregate(BigInteger.Zero, (x, y) => x + y.Value.Value),
                k.Account
            }).ToArray();
            Transaction tx;
            List<TransactionAttribute> attributes = new List<TransactionAttribute>();

            if (comboBoxFrom.SelectedItem == null)
            {
                FromAddress = null;
            }
            else
            {
                FromAddress = ((string)comboBoxFrom.SelectedItem).ToScriptHash();
            }

            if (cOutputs.Length == 0)
            {
                tx = new ContractTransaction();
            }
            else
            {
                UInt160[] addresses;
                if (FromAddress != null)
                {
                    addresses = this.Operater.Wallet.GetAccounts().Where(e => e.ScriptHash.Equals(FromAddress)).Select(p => p.ScriptHash).ToArray();
                }
                else
                {
                    addresses = this.Operater.Wallet.GetAccounts().Where(e => !e.WatchOnly).Select(p => p.ScriptHash).ToArray();
                }
                HashSet<UInt160> sAttributes = new HashSet<UInt160>();
                using (ScriptBuilder sb = new ScriptBuilder())
                {
                    foreach (var output in cOutputs)
                    {
                        byte[] script;
                        using (ScriptBuilder sb2 = new ScriptBuilder())
                        {

                            foreach (UInt160 address in addresses)
                            {
                                sb2.EmitAppCall(output.AssetId, "balanceOf", address);
                            }

                            sb2.Emit(OpCode.DEPTH, OpCode.PACK);
                            script = sb2.ToArray();
                        }
                        ApplicationEngine engine = ApplicationEngine.Run(script);
                        if (engine.State.HasFlag(VMState.FAULT)) return null;
                        var balances = ((VMArray)engine.ResultStack.Pop()).AsEnumerable().Reverse().Zip(addresses, (i, a) => new
                        {
                            Account = a,
                            Value = i.GetBigInteger()
                        }).Where(p => p.Value != 0).ToArray();

                        BigInteger sum = balances.Aggregate(BigInteger.Zero, (x, y) => x + y.Value);
                        if (sum < output.Value) return null;
                        if (sum != output.Value)
                        {
                            balances = balances.OrderByDescending(p => p.Value).ToArray();
                            BigInteger amount = output.Value;
                            int i = 0;
                            while (balances[i].Value <= amount)
                                amount -= balances[i++].Value;
                            if (amount == BigInteger.Zero)
                                balances = balances.Take(i).ToArray();
                            else
                                balances = balances.Take(i).Concat(new[] { balances.Last(p => p.Value >= amount) }).ToArray();
                            sum = balances.Aggregate(BigInteger.Zero, (x, y) => x + y.Value);
                        }
                        sAttributes.UnionWith(balances.Select(p => p.Account));
                        for (int i = 0; i < balances.Length; i++)
                        {
                            BigInteger value = balances[i].Value;
                            if (i == 0)
                            {
                                BigInteger change = sum - output.Value;
                                if (change > 0) value -= change;
                            }
                            sb.EmitAppCall(output.AssetId, "transfer", balances[i].Account, output.Account, value);
                            sb.Emit(OpCode.THROWIFNOT);
                        }
                    }
                    tx = new InvocationTransaction
                    {
                        Version = 1,
                        Script = sb.ToArray()
                    };
                }
                attributes.AddRange(sAttributes.Select(p => new TransactionAttribute
                {
                    Usage = TransactionAttributeUsage.Script,
                    Data = p.ToArray()
                }));
            }
            if (!string.IsNullOrEmpty(remark))
                attributes.Add(new TransactionAttribute
                {
                    Usage = TransactionAttributeUsage.Remark,
                    Data = Encoding.UTF8.GetBytes(remark)
                });
            tx.Attributes = attributes.ToArray();
            tx.Outputs = txOutListBox1.Items.Where(p => p.AssetId is UInt256).Select(p => p.ToTxOutput()).ToArray();
            var tempOuts = tx.Outputs;
            if (tx is ContractTransaction copyTx)
            {
                copyTx.Witnesses = new Witness[0];
                copyTx = this.Operater.Wallet.MakeTransaction(copyTx, FromAddress, change_address: ChangeAddress, fee: Fee);
                if (copyTx == null) return null;
                ContractParametersContext transContext = new ContractParametersContext(copyTx);
                this.Operater.Wallet.Sign(transContext);
                if (transContext.Completed)
                {
                    copyTx.Witnesses = transContext.GetWitnesses();
                }
                if (copyTx.Size > 1024)
                {
                    Fixed8 PriorityFee = Fixed8.FromDecimal(0.001m) + Fixed8.FromDecimal(copyTx.Size * 0.00001m);
                    if (Fee > PriorityFee) PriorityFee = Fee;
                    if (!CostRemind(Fixed8.Zero, PriorityFee)) return null;
                    tx = this.Operater.Wallet.MakeTransaction(new ContractTransaction
                    {
                        Outputs = tempOuts,
                        Attributes = tx.Attributes
                    }, FromAddress, change_address: ChangeAddress, fee: PriorityFee);
                }
            }
            return tx;
        }

        private void txOutListBox1_ItemsChanged(object sender, EventArgs e)
        {
            button3.Enabled = txOutListBox1.ItemCount > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            remark = InputBox.Show(UIHelper.LocalString("在此处输入备注，该备注将记录在区块链上", "Enter remark here, which will be recorded on the blockchain"), UIHelper.LocalString("交易备注", "Transaction Remark"), remark);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            groupBox1.Visible = true;
            //this.Height = 570;
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
        public void BlockIncome(Block block) { }
        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
        }
        public void OnRebuild()
        {
        }
    }
}
