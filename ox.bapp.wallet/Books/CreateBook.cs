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
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;
using OX.SmartContract;
using OX.IO;
using System.Xml;
using OX.Bapps;

namespace OX.Wallets.Base
{
    public partial class CreateBook : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        INotecase Operater;
        public Module Module { get; set; }
        public CreateBook(INotecase operater)
        {
            InitializeComponent();
            this.Operater = operater;
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("注册书籍", "Register Book");
            this.lb_from.Text = UIHelper.LocalString("作者:", "Author:");
            this.lb_name.Text = UIHelper.LocalString("注册书名:", "Register Name:");
            this.lb_StorageType.Text = UIHelper.LocalString("存储类型:", "Storage Type:");
            this.rb_onchain.Text = UIHelper.LocalString("链上存储", "Storage On Chain");
            this.rb_outchain.Text = UIHelper.LocalString("链下存储", "Storage Out Chain");
            this.btnOk.Text = UIHelper.LocalString("马上注册", "Register Now");
            initAccounts();
        }
        public BookTransaction GetTransaction(out UInt160 from)
        {
            from = this.cbAccounts.Text.ToScriptHash();
            var act = this.Operater.Wallet.GetAccount(from);
            var name = this.tb_name.Text;
            if (name.IsNullOrEmpty()) return default;
            var tx = new BookTransaction
            {
                Author = act.GetKey().PublicKey,
                BookType = BookType.Common,
                Data = System.Text.Encoding.UTF8.GetBytes(name),
                BookStorageType = this.rb_onchain.Checked ? BookStorageType.OnChain : BookStorageType.OutChain
            };
            return tx;
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
            initAccounts();
        }
        public void OnRebuild()
        {

        }
        void initAccounts()
        {
            if (this.Operater.IsNotNull())
            {
                this.DoInvoke(() =>
                {
                    this.cbAccounts.Items.Clear();
                    foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                    {
                        this.cbAccounts.Items.Add(act.Address);
                    }
                    this.cbAccounts.SelectedIndex = 0;
                });
            }
        }

    }
}
