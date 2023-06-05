using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI;
using OX.Network.P2P.Payloads;
using OX.Bapps;
using OX.Wallets.NEP6;
using System.ComponentModel.Design.Serialization;
using System.Security.Claims;
using OX.Wallets.UI.Forms;
using System.Security.Cryptography;
using OX.IO;
using OX.IO.Json;

namespace OX.Wallets.Base.Letters
{
    public class LetterModule : Module
    {
        public override string ModuleName { get { return "walletlettermodule"; } }
        public override uint Index { get { return int.MaxValue - 9; } }

        protected INotecase Operater;
        protected MyLetters MyLetters;
        public LetterModule(Bapp bapp) : base(bapp)
        {

        }
        public override void InitEvents() { }
        public override void InitWindows()
        {
            ToolStripMenuItem walletMenu = new ToolStripMenuItem();
            walletMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);

            walletMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            walletMenu.Name = "walletletterMenu";
            walletMenu.Size = new System.Drawing.Size(39, 21);
            walletMenu.Text = UIHelper.LocalString("&私信", "&Letter");
            //signature
            ToolStripMenuItem newLetterMenu = new ToolStripMenuItem();
            newLetterMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            newLetterMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //isingMenu.Image = global::Example.Icons.NewFile_6276;
            newLetterMenu.Name = "newLetterMenu";
            newLetterMenu.ShortcutKeys = Keys.Control | Keys.N;
            newLetterMenu.Size = new System.Drawing.Size(170, 22);
            newLetterMenu.Text = UIHelper.LocalString("&写信", "&New Letter");
            newLetterMenu.Click += newLetterMenu_Click;
            //introduce
            ToolStripMenuItem inboxmenu = new ToolStripMenuItem();
            inboxmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            inboxmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            inboxmenu.Name = "inboxmenu";
            inboxmenu.ShortcutKeys = Keys.Control | Keys.I;
            inboxmenu.Size = new System.Drawing.Size(170, 22);
            inboxmenu.Text = UIHelper.LocalString("&收件箱", "&Inbox");
            inboxmenu.Click += inboxMenu_Click;


            walletMenu.DropDownItems.AddRange(new ToolStripItem[] {
                newLetterMenu,
                inboxmenu
                });
            Container.TopMenus.Items.AddRange(new ToolStripItem[] {
            walletMenu});
        }

        private void newLetterMenu_Click(object sender, EventArgs e)
        {
            new NewLetterDialog(Operater).ShowDialog();
        }

        private void outboxmenu_Click(object sender, EventArgs e)
        {
        }

        public override void OnBappEvent(BappEvent be)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.OnBappEvent(be);
            }
        }

        public override void OnCrossBappMessage(CrossBappMessage message)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.OnCrossBappMessage(message);
            }
        }
        public override void HeartBeat(HeartBeatContext context)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.HeartBeat(context);
            }
        }
        public override void BeforeOnBlock(Block block)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.BeforeOnBlock(block);
            }
        }
        public override void OnBlock(Block block)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.OnBlock(block);
            }
        }
        public override void AfterOnBlock(Block block)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.AfterOnBlock(block);
            }
        }
        public override void ChangeWallet(INotecase operater)
        {
            Operater = operater;
            if (MyLetters.IsNotNull())
            {
                MyLetters.ChangeWallet(operater);
            }
        }
        public override void OnRebuild()
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.OnRebuild();
            }
        }
        public override void OnLoadBappModuleWalletSection(JObject bappSectionObject)
        {
        }
      

        private void inboxMenu_Click(object sender, EventArgs e)
        {
            if (MyLetters == default)
            {
                MyLetters = new MyLetters();
                MyLetters.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    MyLetters.ChangeWallet(Operater);
                Container.ToolWindows.Add(MyLetters);
            }
            Container.DockPanel.AddContent(MyLetters);
        }
    }
}
