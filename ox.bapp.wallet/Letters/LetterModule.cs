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
using OX.IO;
using OX.IO.Json;
using OX.Wallets.Letters;
using OX.Cryptography.ECC;
using OX.SmartContract;

namespace OX.Wallets.Base.Letters
{
    public class LetterModule : Module
    {
        public override string ModuleName { get { return "walletlettermodule"; } }
        public override uint Index { get { return int.MaxValue - 13; } }

        protected INotecase Operater;
        protected MyLetters MyLetters;
        Dictionary<UInt256, LetterLine> LetterLines = new Dictionary<UInt256, LetterLine>();
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
            walletMenu.Text = UIHelper.LocalString("&链邮", "&Blockchain Mail");

            ToolStripMenuItem newLetterMenu = new ToolStripMenuItem();
            newLetterMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            newLetterMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            newLetterMenu.Name = "newLetterMenu";
            newLetterMenu.ShortcutKeys = Keys.Control |Keys.Alt| Keys.N;
            newLetterMenu.Size = new System.Drawing.Size(170, 22);
            newLetterMenu.Text = UIHelper.LocalString("&写邮件", "&New Mail");
            newLetterMenu.Click += newLetterMenu_Click;

            ToolStripMenuItem inboxmenu = new ToolStripMenuItem();
            inboxmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            inboxmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            inboxmenu.Name = "inboxmenu";
            inboxmenu.ShortcutKeys = Keys.Control |Keys.Alt| Keys.B;
            inboxmenu.Size = new System.Drawing.Size(170, 22);
            inboxmenu.Text = UIHelper.LocalString("&邮箱", "&Mail Box");
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
            new NewLetter(Operater).ShowDialog();
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
            foreach (var line in this.LetterLines.Values)
            {
                line.OnBappEvent(be);
            }
        }

        public override void OnCrossBappMessage(CrossBappMessage message)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.OnCrossBappMessage(message);
            }
            foreach (var line in this.LetterLines.Values)
            {
                line.OnCrossBappMessage(message);
            }
        }
        public override void HeartBeat(HeartBeatContext context)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.HeartBeat(context);
            }
            foreach (var line in this.LetterLines.Values)
            {
                line.HeartBeat(context);
            }
        }
        public override void BeforeOnBlock(Block block)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.BeforeOnBlock(block);
            }
            foreach (var line in this.LetterLines.Values)
            {
                line.BeforeOnBlock(block);
            }
        }
        public override void OnBlock(Block block)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.OnBlock(block);
            }
            foreach (var line in this.LetterLines.Values)
            {
                line.OnBlock(block);
            }
        }
        public override void AfterOnBlock(Block block)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.AfterOnBlock(block);
            }
            foreach (var line in this.LetterLines.Values)
            {
                line.AfterOnBlock(block);
            }
        }
        public override void ChangeWallet(INotecase operater)
        {
            Operater = operater;
            if (MyLetters.IsNotNull())
            {
                MyLetters.ChangeWallet(operater);
            }
            foreach (var line in this.LetterLines.Values)
            {
                line.ChangeWallet(operater);
            }
        }
        public override void OnRebuild()
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.OnRebuild();
            }
            foreach (var line in this.LetterLines.Values)
            {
                line.OnRebuild();
            }
        }
        public override void OnFlashMessage(FlashMessage flashMessage)
        {
            if (MyLetters.IsNotNull())
            {
                MyLetters.OnFlashMessage(flashMessage);
            }
            foreach (var line in this.LetterLines.Values)
            {
                line.OnFlashMessage(flashMessage);
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
        public void OpenLetterLine(UInt256 letterLine,LetterPair pair)
        {
            if (!this.LetterLines.TryGetValue(letterLine, out LetterLine gr))
            {
                gr = new LetterLine(this, this.Operater, letterLine, pair);
                if (this.Operater != default && this.Operater.Wallet != default)
                    gr.ChangeWallet(this.Operater);
                this.LetterLines[letterLine] = gr;
            }
            if (gr.IsNotNull())
            {
                this.Container.DockPanel.AddContent(gr);
            }
        }
    }
}
