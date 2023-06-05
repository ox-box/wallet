﻿using OX.Cryptography.ECC;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.VM;
using OX.Wallets;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI.Controls;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace OX.Wallets.Base
{
    public partial class AssetRegisterDialog : DarkDialog
    {
        INotecase Operater;
        public AssetRegisterDialog(INotecase notecase)
        {
            this.Operater = notecase;
            InitializeComponent();
            this.Text = UIHelper.LocalString("注册私营资产", "Register Private Asset");
            this.btnOk.Text=UIHelper.LocalString("注册", "Register");
            this.label1.Text = UIHelper.LocalString("资产类型:", "Asset Type:");
            this.label2.Text = UIHelper.LocalString("资产名称:", "Asset Name:");
            this.label3.Text = UIHelper.LocalString("总量限制:", "Capped:");
            this.label6.Text = UIHelper.LocalString("精度:", "Precision:");
            this.label4.Text = UIHelper.LocalString("发行者:", "Owner:");
            this.label5.Text = UIHelper.LocalString("管理者:", "Admin:");
            this.label9.Text = UIHelper.LocalString("分发:", "Issuer:");
        }

        public InvocationTransaction GetTransaction()
        {
            AssetType asset_type = (AssetType)comboBox1.SelectedItem;
            string name = string.IsNullOrWhiteSpace(textBox1.Text) ? string.Empty : $"[{{\"lang\":\"{CultureInfo.CurrentCulture.Name}\",\"name\":\"{textBox1.Text}\"}}]";
            Fixed8 amount = checkBox1.Checked ? Fixed8.Parse(textBox2.Text) : -Fixed8.Satoshi;
            byte precision = (byte)numericUpDown1.Value;
            ECPoint owner = (ECPoint)comboBox2.SelectedItem;
            UInt160 admin = comboBox3.Text.ToScriptHash();
            UInt160 issuer = comboBox4.Text.ToScriptHash();
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitSysCall("OX.Asset.Create", asset_type, name, amount, precision, owner, admin, issuer);
                return new InvocationTransaction
                {
                    Attributes = new[]
                    {
                        new TransactionAttribute
                        {
                            Usage = TransactionAttributeUsage.Script,
                            Data = Contract.CreateSignatureRedeemScript(owner).ToScriptHash().ToArray()
                        }
                    },
                    Script = sb.ToArray()
                };
            }
        }

        private void AssetRegisterDialog_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new object[] { AssetType.Share, AssetType.Token });
            comboBox2.Items.AddRange(this.Operater.Wallet.GetAccounts().Where(p => !p.WatchOnly && p.Contract.Script.IsSignatureContract()).Select(p => p.GetKey().PublicKey).ToArray());
            comboBox3.Items.AddRange(this.Operater.Wallet.GetAccounts().Where(p => !p.WatchOnly).Select(p => p.Address).ToArray());
            comboBox4.Items.AddRange(this.Operater.Wallet.GetAccounts().Where(p => !p.WatchOnly).Select(p => p.Address).ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(comboBox1.SelectedItem is AssetType assetType)) return;
            numericUpDown1.Enabled = assetType != AssetType.Share;
            if (!numericUpDown1.Enabled) numericUpDown1.Value = 0;
            CheckForm(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox1.Checked;
            CheckForm(sender, e);
        }

        private void CheckForm(object sender, EventArgs e)
        {
            bool enabled = comboBox1.SelectedIndex >= 0 &&
                              textBox1.TextLength > 0&&textBox1.Text.ToLower()!="oxc"&&textBox1.Text.ToLower()!="oxs" &&
                              (!checkBox1.Checked || textBox2.TextLength > 0) &&
                              comboBox2.SelectedIndex >= 0 &&
                              !string.IsNullOrWhiteSpace(comboBox3.Text) &&
                              !string.IsNullOrWhiteSpace(comboBox4.Text);
            if (enabled)
            {
                try
                {
                    comboBox3.Text.ToScriptHash();
                    comboBox4.Text.ToScriptHash();
                }
                catch (FormatException)
                {
                    enabled = false;
                }
            }
            this.btnOk.Enabled = enabled;
        }
    }
}
