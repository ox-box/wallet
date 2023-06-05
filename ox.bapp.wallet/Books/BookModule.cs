using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI;
using OX.Network.P2P.Payloads;
using OX.Bapps;
using OX.Wallets.UI.Forms;
using System.ComponentModel.Design.Serialization;
using System.Security.Claims;
using OX.Ledger;
using OX.IO;
using OX.IO.Json;

namespace OX.Wallets.Base.Books
{
    public class BookModule : Module
    {
        public override string ModuleName { get { return "walletbookmodule"; } }
        public override uint Index { get { return int.MaxValue - 2; } }

        protected INotecase Operater;
        protected MyBooks MyBooks;
        public BookModule(Bapp bapp) : base(bapp)
        {

        }
        public override void InitEvents() { }
        public override void InitWindows()
        {
            ToolStripMenuItem walletMenu = new ToolStripMenuItem();
            walletMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);

            walletMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            walletMenu.Name = "walletBookMenu";
            walletMenu.Size = new System.Drawing.Size(39, 21);
            walletMenu.Text = UIHelper.LocalString("&书籍", "&Book");

            //reg book
            ToolStripMenuItem newBookMenu = new ToolStripMenuItem();
            newBookMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            newBookMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //newBookMenu.Image = global::Example.Icons.NewFile_6276;
            newBookMenu.Name = "newBookMenu";
            newBookMenu.ShortcutKeys = Keys.Control | Keys.R;
            newBookMenu.Size = new System.Drawing.Size(170, 22);
            newBookMenu.Text = UIHelper.LocalString("&注册书籍", "&Register Book");
            newBookMenu.Click += NewBookMenu_Click;

            //all books
            ToolStripMenuItem allBooksMenu = new ToolStripMenuItem();
            allBooksMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            allBooksMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //allBookMenu.Image = global::Example.Icons.NewFile_6276;
            allBooksMenu.Name = "allBookMenu";
            allBooksMenu.ShortcutKeys = Keys.Control | Keys.A;
            allBooksMenu.Size = new System.Drawing.Size(170, 22);
            allBooksMenu.Text = UIHelper.LocalString("&所有书籍", "&All Books");
            allBooksMenu.Click += AllBookMenu_Click;

            //my books
            ToolStripMenuItem myBooksMenu = new ToolStripMenuItem();
            myBooksMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            myBooksMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //myBooksMenu.Image = global::Example.Icons.NewFile_6276;
            myBooksMenu.Name = "myBooksMenu";
            myBooksMenu.ShortcutKeys = Keys.Control | Keys.M;
            myBooksMenu.Size = new System.Drawing.Size(170, 22);
            myBooksMenu.Text = UIHelper.LocalString("&我的书籍", "&My Books");
            myBooksMenu.Click += MyBooksMenu_Click;

            walletMenu.DropDownItems.AddRange(new ToolStripItem[] {
                newBookMenu,
                allBooksMenu,
                myBooksMenu
               });
            Container.TopMenus.Items.AddRange(new ToolStripItem[] {
            walletMenu});
        }

        private void MyBooksMenu_Click(object sender, EventArgs e)
        {
            if (MyBooks == default)
            {
                MyBooks = new MyBooks();
                MyBooks.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    MyBooks.ChangeWallet(Operater);
                Container.ToolWindows.Add(MyBooks);
            }
            Container.DockPanel.AddContent(MyBooks);
        }

        private void AllBookMenu_Click(object sender, EventArgs e)
        {
        }

        private void NewBookMenu_Click(object sender, EventArgs e)
        {
            using (CreateBook dialog = new CreateBook(Operater))
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var tx = dialog.GetTransaction(out UInt160 from);
                    if (tx.IsNotNull())
                    {
                        tx = Operater.Wallet.MakeTransaction(tx, from, from);
                        if (tx.IsNotNull())
                        {
                            Operater.SignAndSendTx(tx);
                            string msg = $"{UIHelper.LocalString("注册书籍交易已广播", "Relay register book transaction completed")}   {tx.Hash}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                        }
                    }
                }
            }
        }





        public override void OnCrossBappMessage(CrossBappMessage message)
        {

        }
        public override void OnBappEvent(BappEvent be)
        {
            if (MyBooks != default)
                MyBooks.OnBappEvent(be);
        }
        public override void HeartBeat(HeartBeatContext context)
        {
            if (MyBooks != default)
                MyBooks.HeartBeat(context);
        }
        public override void BeforeOnBlock(Block block)
        {
            if (MyBooks != default)
                MyBooks.BeforeOnBlock(block);
        }
        public override void OnBlock(Block block)
        {
            if (MyBooks != default)
                MyBooks.OnBlock(block);

        }
        public override void AfterOnBlock(Block block)
        {
            if (MyBooks != default)
                MyBooks.AfterOnBlock(block);
        }
        public override void ChangeWallet(INotecase operater)
        {
            Operater = operater;
            if (MyBooks != default)
                MyBooks.ChangeWallet(operater);
        }

        public override void OnRebuild()
        {
            if (MyBooks != default)
                MyBooks.OnRebuild();
        }
        public override void OnLoadBappModuleWalletSection(JObject bappSectionObject)
        {
        }
    }
}
