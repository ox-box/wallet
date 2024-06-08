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
using OX.Wallets.Base;
using OX.Ledger;
using OX.VM;
using OX.Persistence;
using OX.Wallets.UI.Docking;
using OX.Wallets.Flash.State;
using OX.Wallets.Flash.Chat;
using NBitcoin.Secp256k1;

namespace OX.Wallets.Flash
{
    public class FlashMessageModule : Module
    {
        public static string WalletIndexDirectory { get; set; }
        public override string ModuleName { get { return "walletflashmessagemodule"; } }
        public override uint Index { get { return int.MaxValue - 14; } }

        protected INotecase Operater;
        protected MyTalks MyTalks;
        protected Dictionary<UInt256, UniTalk> UniTalks = new Dictionary<UInt256, UniTalk>();
        protected Dictionary<UInt256, MultiTalk> MultiTalks = new Dictionary<UInt256, MultiTalk>();
        protected StateLine StateLine;
        protected Dictionary<UInt160, SenderStateLine> SenderStateLines = new Dictionary<UInt160, SenderStateLine>();
        public FlashMessageModule(Bapp bapp) : base(bapp)
        {

        }
        public override void InitEvents() { }
        public override void InitWindows()
        {
            ToolStripMenuItem walletMenu = new ToolStripMenuItem();
            walletMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);

            walletMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            walletMenu.Name = "walletflashmessagemenu";
            walletMenu.Size = new System.Drawing.Size(39, 21);
            walletMenu.Text = UIHelper.LocalString("&闪信", "&Flash Message");

            ToolStripMenuItem flashstatemenu = new ToolStripMenuItem();
            flashstatemenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            flashstatemenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            flashstatemenu.Name = "flashstatemenu";
            flashstatemenu.Size = new System.Drawing.Size(170, 22);
            flashstatemenu.Text = UIHelper.LocalString("&闪态", "&Flash State");

            ToolStripMenuItem newFlashStateMenu = new ToolStripMenuItem();
            newFlashStateMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            newFlashStateMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            newFlashStateMenu.Name = "newFlashStateMenu";
            newFlashStateMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            newFlashStateMenu.Size = new System.Drawing.Size(170, 22);
            newFlashStateMenu.Text = UIHelper.LocalString("&更新闪态", "&Update Flash State");
            newFlashStateMenu.Click += NewFlashStateMenu_Click;
            flashstatemenu.DropDownItems.Add(newFlashStateMenu);



            ToolStripMenuItem stateFlowMenu = new ToolStripMenuItem();
            stateFlowMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            stateFlowMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            stateFlowMenu.Name = "stateFlowMenu";
            stateFlowMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            stateFlowMenu.Size = new System.Drawing.Size(170, 22);
            stateFlowMenu.Text = UIHelper.LocalString("&闪态流", "&Flash State Flow");
            stateFlowMenu.Click += StateFlowMenu_Click;
            flashstatemenu.DropDownItems.Add(stateFlowMenu);


            ToolStripMenuItem flashtalkmenu = new ToolStripMenuItem();
            flashtalkmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            flashtalkmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            flashtalkmenu.Name = "flashtalkmenu";
            flashtalkmenu.Size = new System.Drawing.Size(170, 22);
            flashtalkmenu.Text = UIHelper.LocalString("&闪聊", "&Flash Talk");

            ToolStripMenuItem mytalksMenu = new ToolStripMenuItem();
            mytalksMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            mytalksMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mytalksMenu.Name = "mytalksMenu";
            mytalksMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            mytalksMenu.Size = new System.Drawing.Size(170, 22);
            mytalksMenu.Text = UIHelper.LocalString("&我的线路", "&My Talk Line");
            mytalksMenu.Click += MytalksMenu_Click;
            flashtalkmenu.DropDownItems.Add(mytalksMenu);

            ToolStripMenuItem newTalkLineMenu = new ToolStripMenuItem();
            newTalkLineMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            newTalkLineMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            newTalkLineMenu.Name = "newTalkLineMenu";
            newTalkLineMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            newTalkLineMenu.Size = new System.Drawing.Size(170, 22);
            newTalkLineMenu.Text = UIHelper.LocalString("&创建线路", "&New talk line");
            newTalkLineMenu.Click += NewTalkLineMenu_Click;
            flashtalkmenu.DropDownItems.Add(newTalkLineMenu);


            walletMenu.DropDownItems.AddRange(new ToolStripItem[] {
                flashstatemenu,
                flashtalkmenu
                });
            Container.TopMenus.Items.AddRange(new ToolStripItem[] {
            walletMenu});
        }

        private void StateFlowMenu_Click(object sender, EventArgs e)
        {
            if (StateLine == default)
            {
                StateLine = new StateLine();
                StateLine.Module = this;
            }
            Container.DockPanel.AddContent(StateLine);
            if (Operater != default && Operater.Wallet != default)
                StateLine.ChangeWallet(Operater);
        }



        private void NewFlashStateMenu_Click(object sender, EventArgs e)
        {
            new NewFlashState(this.Operater).ShowDialog();
        }

        private void NewTalkLineMenu_Click(object sender, EventArgs e)
        {
            new NewTalkLine(this.Operater).ShowDialog();
        }

        private void MytalksMenu_Click(object sender, EventArgs e)
        {
            if (MyTalks == default)
            {
                MyTalks = new MyTalks();
                MyTalks.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    MyTalks.ChangeWallet(Operater);
                Container.ToolWindows.Add(MyTalks);
            }
            Container.DockPanel.AddContent(MyTalks);
        }







        public override void OnBappEvent(BappEvent be)
        {
            if (MyTalks.IsNotNull())
            {
                MyTalks.OnBappEvent(be);
            }
            foreach (var talk in UniTalks)
            {
                talk.Value.OnBappEvent(be);
            }
            foreach (var talk in MultiTalks)
            {
                talk.Value.OnBappEvent(be);
            }
            if (StateLine.IsNotNull())
                StateLine.OnBappEvent(be);
            foreach (var line in SenderStateLines)
            {
                line.Value.OnBappEvent(be);
            }
        }

        public override void OnCrossBappMessage(CrossBappMessage message)
        {
            if (MyTalks.IsNotNull())
            {
                MyTalks.OnCrossBappMessage(message);
            }
            foreach (var talk in UniTalks)
            {
                talk.Value.OnCrossBappMessage(message);
            }
            foreach (var talk in MultiTalks)
            {
                talk.Value.OnCrossBappMessage(message);
            }
            if (StateLine.IsNotNull())
                StateLine.OnCrossBappMessage(message);
            foreach (var line in SenderStateLines)
            {
                line.Value.OnCrossBappMessage(message);
            }
        }
        public override void HeartBeat(HeartBeatContext context)
        {
            if (MyTalks.IsNotNull())
            {
                MyTalks.HeartBeat(context);
            }
            foreach (var talk in UniTalks)
            {
                talk.Value.HeartBeat(context);
            }
            foreach (var talk in MultiTalks)
            {
                talk.Value.HeartBeat(context);
            }
            if (StateLine.IsNotNull())
                StateLine.HeartBeat(context);
            foreach (var line in SenderStateLines)
            {
                line.Value.HeartBeat(context);
            }
        }
        public override void BeforeOnBlock(Block block)
        {
            if (MyTalks.IsNotNull())
            {
                MyTalks.BeforeOnBlock(block);
            }
            foreach (var talk in UniTalks)
            {
                talk.Value.BeforeOnBlock(block);
            }
            foreach (var talk in MultiTalks)
            {
                talk.Value.BeforeOnBlock(block);
            }
            if (StateLine.IsNotNull())
                StateLine.BeforeOnBlock(block);
            foreach (var line in SenderStateLines)
            {
                line.Value.BeforeOnBlock(block);
            }
        }
        public override void OnBlock(Block block)
        {
            if (MyTalks.IsNotNull())
            {
                MyTalks.OnBlock(block);
            }
            foreach (var talk in UniTalks)
            {
                talk.Value.OnBlock(block);
            }
            foreach (var talk in MultiTalks)
            {
                talk.Value.OnBlock(block);
            }
            if (StateLine.IsNotNull())
                StateLine.OnBlock(block);
            foreach (var line in SenderStateLines)
            {
                line.Value.OnBlock(block);
            }
        }
        public override void AfterOnBlock(Block block)
        {
            if (MyTalks.IsNotNull())
            {
                MyTalks.AfterOnBlock(block);
            }
            foreach (var talk in UniTalks)
            {
                talk.Value.AfterOnBlock(block);
            }
            foreach (var talk in MultiTalks)
            {
                talk.Value.AfterOnBlock(block);
            }
            if (StateLine.IsNotNull())
                StateLine.AfterOnBlock(block);
            foreach (var line in SenderStateLines)
            {
                line.Value.AfterOnBlock(block);
            }
        }
        public override void ChangeWallet(INotecase operater)
        {
            Operater = operater;
            if (MyTalks.IsNotNull())
            {
                MyTalks.ChangeWallet(operater);
            }
            foreach (var talk in UniTalks)
            {
                talk.Value.ChangeWallet(operater);
            }
            foreach (var talk in MultiTalks)
            {
                talk.Value.ChangeWallet(operater);
            }
            if (StateLine.IsNotNull())
                StateLine.ChangeWallet(operater);
            foreach (var line in SenderStateLines)
            {
                line.Value.ChangeWallet(operater);
            }
        }
        public override void OnRebuild()
        {
            if (MyTalks.IsNotNull())
            {
                MyTalks.OnRebuild();
            }
            foreach (var talk in UniTalks)
            {
                talk.Value.OnRebuild();
            }
            foreach (var talk in MultiTalks)
            {
                talk.Value.OnRebuild();
            }
            if (StateLine.IsNotNull())
                StateLine.OnRebuild();
            foreach (var line in SenderStateLines)
            {
                line.Value.OnRebuild();
            }
        }
        public override void OnFlashMessage(FlashMessage flashMessage)
        {
            if (MyTalks.IsNotNull())
            {
                MyTalks.OnFlashMessage(flashMessage);
            }
            foreach (var talk in UniTalks)
            {
                talk.Value.OnFlashMessage(flashMessage);
            }
            foreach (var talk in MultiTalks)
            {
                talk.Value.OnFlashMessage(flashMessage);
            }
            if (StateLine.IsNotNull())
                StateLine.OnFlashMessage(flashMessage);
            foreach (var line in SenderStateLines)
            {
                line.Value.OnFlashMessage(flashMessage);
            }
        }
        public override void OnLoadBappModuleWalletSection(JObject bappSectionObject)
        {
        }
        public void OpenUnicastTalkLine(Tuple<UInt256, UnicastTalkLineValue> t)
        {
            if (!this.UniTalks.TryGetValue(t.Item1, out UniTalk gr))
            {
                gr = new UniTalk(this.Operater, t);
                gr.Module = this;
                if (this.Operater != default && this.Operater.Wallet != default)
                    gr.ChangeWallet(this.Operater);
                this.UniTalks[t.Item1] = gr;
            }
            if (gr.IsNotNull())
            {
                this.Container.DockPanel.AddContent(gr);
            }
        }
        public void OpenMulticastTalkLine(Tuple<UInt256, MulticastTalkLineValue> t)
        {
            if (!this.MultiTalks.TryGetValue(t.Item1, out MultiTalk gr))
            {
                gr = new MultiTalk(this.Operater, t);
                gr.Module = this;
                if (this.Operater != default && this.Operater.Wallet != default)
                    gr.ChangeWallet(this.Operater);
                this.MultiTalks[t.Item1] = gr;
            }
            if (gr.IsNotNull())
            {
                this.Container.DockPanel.AddContent(gr);
            }
        }
        public void ClearMulticastTalkLineRecords(Tuple<UInt256, MulticastTalkLineValue> t)
        {
            if (this.MultiTalks.TryGetValue(t.Item1, out MultiTalk gr))
            {
                gr.ClearRecords();
            }

        }
        public void ModifyMulticastTalkLineLabel(Tuple<UInt256, MulticastTalkLineValue> t)
        {
            if (this.MultiTalks.TryGetValue(t.Item1, out MultiTalk gr))
            {
                gr.ResetLabel(t.Item2.Label);
            }

        }
        public void ModifyUnicastTalkLineLabel(Tuple<UInt256, UnicastTalkLineValue> t)
        {
            if (this.UniTalks.TryGetValue(t.Item1, out UniTalk gr))
            {
                gr.ResetLabel(t.Item2.Label);
            }

        }
        public void CloseMulticastTalkLine(Tuple<UInt256, MulticastTalkLineValue> t)
        {
            if (this.MultiTalks.TryGetValue(t.Item1, out MultiTalk gr))
            {
                gr.DoClose();
                this.MultiTalks.Remove(t.Item1);
            }

        }
        public void ClearUnicastTalkLineRecords(Tuple<UInt256, UnicastTalkLineValue> t)
        {
            if (this.UniTalks.TryGetValue(t.Item1, out UniTalk gr))
            {
                gr.ClearRecords();
            }

        }
        public void CloseUnicastTalkLine(Tuple<UInt256, UnicastTalkLineValue> t)
        {
            if (this.UniTalks.TryGetValue(t.Item1, out UniTalk gr))
            {
                gr.DoClose();
                this.UniTalks.Remove(t.Item1);
            }

        }
        public void OpenSenderHome(string address)
        {
            var sh = address.ToScriptHash();
            if (!this.SenderStateLines.TryGetValue(sh, out SenderStateLine gr))
            {
                gr = new SenderStateLine(sh);
                gr.Module = this;
                this.SenderStateLines[sh] = gr;
            }
            if (gr.IsNotNull())
            {
                this.Container.DockPanel.AddContent(gr);
                if (this.Operater != default && this.Operater.Wallet != default)
                    gr.ChangeWallet(this.Operater);
            }
        }
    }
}
