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
using OX.Wallets.Base.Wallets;

namespace OX.Wallets.Base.Events
{
    public class EventModule : Module
    {
        public override string ModuleName { get { return "walleteventmodule"; } }
        public override uint Index { get { return int.MaxValue - 8; } }

        Dictionary<string, EventList> BoardList = new Dictionary<string, EventList>();
        protected INotecase Operater;
        protected Boards Boards;
        protected FollowBoards FollowBoards;
        protected MyBoards MyBoards;
        protected MyEngraves MyEngraves;
        public EventModule(Bapp bapp) : base(bapp)
        {

        }
        public override void InitEvents() { }
        public override void InitWindows()
        {
            ToolStripMenuItem walletMenu = new ToolStripMenuItem();
            walletMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);

            walletMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            walletMenu.Name = "walletEventMenu";
            walletMenu.Size = new System.Drawing.Size(39, 21);
            walletMenu.Text = UIHelper.LocalString("&事件", "&Event");
            //manage Find Event Board
            ToolStripMenuItem boardmenu = new ToolStripMenuItem();
            boardmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            boardmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            boardmenu.Name = "boardmenu";
            boardmenu.ShortcutKeys = Keys.Control | Keys.B;
            boardmenu.Size = new System.Drawing.Size(170, 22);
            boardmenu.Text = UIHelper.LocalString("&寻找事件板", "&Find Event Board");
            boardmenu.Click += boardMenu_Click;
            //manage Create Event Board
            ToolStripMenuItem newboardmenu = new ToolStripMenuItem();
            newboardmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            newboardmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            newboardmenu.Name = "newboardmenu";
            newboardmenu.ShortcutKeys = Keys.Control | Keys.C;
            newboardmenu.Size = new System.Drawing.Size(170, 22);
            newboardmenu.Text = UIHelper.LocalString("&创建事件板", "&New Event Board");
            newboardmenu.Click += Newboardmenu_Click;
            //manage focus board
            ToolStripMenuItem focusboardmenu = new ToolStripMenuItem();
            focusboardmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            focusboardmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            focusboardmenu.Name = "focusboardmenu";
            focusboardmenu.ShortcutKeys = Keys.Control | Keys.F;
            focusboardmenu.Size = new System.Drawing.Size(170, 22);
            focusboardmenu.Text = UIHelper.LocalString("&事件板收藏夹", "&Board Favorites");
            focusboardmenu.Click += Focusboardmenu_Click;
            //my board
            ToolStripMenuItem myboardmenu = new ToolStripMenuItem();
            myboardmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            myboardmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //myboardmenu.Image = global::Example.Icons.NewFile_6276;
            myboardmenu.Name = "myboardmenu";
            myboardmenu.ShortcutKeys = Keys.Control | Keys.G;
            myboardmenu.Size = new System.Drawing.Size(170, 22);
            myboardmenu.Text = UIHelper.LocalString("&我的事件板", "&My Event Boards");
            myboardmenu.Click += Myboardmenu_Click;
            //introduce
            ToolStripMenuItem myEngravemenu = new ToolStripMenuItem();
            myEngravemenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            myEngravemenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            myEngravemenu.Name = "myEngravemenu";
            myEngravemenu.ShortcutKeys = Keys.Control | Keys.E;
            myEngravemenu.Size = new System.Drawing.Size(170, 22);
            myEngravemenu.Text = UIHelper.LocalString("&我的事件", "&My Events");
            myEngravemenu.Click += myEngraveMenu_Click;

            walletMenu.DropDownItems.AddRange(new ToolStripItem[] {
                boardmenu,
                newboardmenu,
                focusboardmenu,
                myboardmenu,
                myEngravemenu});
            Container.TopMenus.Items.AddRange(new ToolStripItem[] {
            walletMenu});
        }

        private void Myboardmenu_Click(object sender, EventArgs e)
        {
            if (MyBoards == default)
            {
                MyBoards = new MyBoards();
                MyBoards.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    MyBoards.ChangeWallet(Operater);
                Container.ToolWindows.Add(MyBoards);
            }
            Container.DockPanel.AddContent(MyBoards);
        }

        private void Newboardmenu_Click(object sender, EventArgs e)
        {
            using (CreateEventBoard dialog = new CreateEventBoard(Operater))
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
                            string msg = $"{UIHelper.LocalString("创建事件板交易已广播", "Relay event board transaction completed")}   {tx.Hash}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                        }
                    }
                }
            }
        }

        private void Focusboardmenu_Click(object sender, EventArgs e)
        {
            if (FollowBoards == default)
            {
                FollowBoards = new FollowBoards();
                FollowBoards.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    FollowBoards.ChangeWallet(Operater);
                Container.ToolWindows.Add(FollowBoards);
            }
            Container.DockPanel.AddContent(FollowBoards);
        }

        public override void OnBappEvent(BappEvent be)
        {
            if (Boards != default)
                Boards.OnBappEvent(be);
            if (FollowBoards != default)
                FollowBoards.OnBappEvent(be);
            if (MyBoards != default)
                MyBoards.OnBappEvent(be);
            if (MyEngraves != default)
                MyEngraves.OnBappEvent(be);
            foreach (var bl in BoardList.Values)
            {
                bl.OnBappEvent(be);
            }
        }



        public override void OnCrossBappMessage(CrossBappMessage message)
        {
            if (message.MessageType == 1)
            {
                var boardkey = message.Attachment as string;
                if (boardkey.IsNotNull())
                {
                    OpenEventBoard(boardkey);
                }
            }

            if (Boards != default)
                Boards.OnCrossBappMessage(message);
            if (FollowBoards != default)
                FollowBoards.OnCrossBappMessage(message);
            if (MyBoards != default)
                MyBoards.OnCrossBappMessage(message);
            if (MyEngraves != default)
                MyEngraves.OnCrossBappMessage(message);
            foreach (var bl in BoardList.Values)
            {
                bl.OnCrossBappMessage(message);
            }
        }
        public override void HeartBeat(HeartBeatContext context)
        {
            if (Boards != default)
                Boards.HeartBeat(context);
            if (FollowBoards != default)
                FollowBoards.HeartBeat(context);
            if (MyBoards != default)
                MyBoards.HeartBeat(context);
            if (MyEngraves != default)
                MyEngraves.HeartBeat(context);
            foreach (var bl in BoardList.Values)
            {
                bl.HeartBeat(context);
            }
        }
        public override void BeforeOnBlock(Block block)
        {
            if (Boards != default)
                Boards.BeforeOnBlock(block);
            if (FollowBoards != default)
                FollowBoards.BeforeOnBlock(block);
            if (MyBoards != default)
                MyBoards.BeforeOnBlock(block);
            if (MyEngraves != default)
                MyEngraves.BeforeOnBlock(block);
            foreach (var bl in BoardList.Values)
            {
                bl.BeforeOnBlock(block);
            }
        }
        public override void OnBlock(Block block)
        {
            if (Boards != default)
                Boards.OnBlock(block);
            if (FollowBoards != default)
                FollowBoards.OnBlock(block);
            if (MyBoards != default)
                MyBoards.OnBlock(block);
            if (MyEngraves != default)
                MyEngraves.OnBlock(block);
            foreach (var bl in BoardList.Values)
            {
                bl.OnBlock(block);
            }
        }
        public override void AfterOnBlock(Block block)
        {
            if (Boards != default)
                Boards.AfterOnBlock(block);
            if (FollowBoards != default)
                FollowBoards.AfterOnBlock(block);
            if (MyBoards != default)
                MyBoards.AfterOnBlock(block);
            if (MyEngraves != default)
                MyEngraves.AfterOnBlock(block);
            foreach (var bl in BoardList.Values)
            {
                bl.AfterOnBlock(block);
            }
        }
        public override void ChangeWallet(INotecase operater)
        {
            Operater = operater;
            if (Boards != default)
                Boards.ChangeWallet(operater);
            if (FollowBoards != default)
                FollowBoards.ChangeWallet(operater);
            if (MyBoards != default)
                MyBoards.ChangeWallet(operater);
            if (MyEngraves != default)
                MyEngraves.ChangeWallet(operater);
            foreach (var bl in BoardList.Values)
            {
                bl.ChangeWallet(operater);
            }
        }

        public override void OnRebuild()
        {
            if (Boards != default)
                Boards.OnRebuild();
            if (FollowBoards != default)
                FollowBoards.OnRebuild();
            if (MyBoards != default)
                MyBoards.OnRebuild();
            if (MyEngraves != default)
                MyEngraves.OnRebuild();
            foreach (var bl in BoardList.Values)
            {
                bl.OnRebuild();
            }
        }
        public override void OnLoadBappModuleWalletSection(JObject bappSectionObject)
        {
        }
        private void myEngraveMenu_Click(object sender, EventArgs e)
        {
            if (MyEngraves == default)
            {
                MyEngraves = new MyEngraves();
                MyEngraves.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    MyEngraves.ChangeWallet(Operater);
                Container.ToolWindows.Add(MyEngraves);
            }
            Container.DockPanel.AddContent(MyEngraves);
        }

        private void boardMenu_Click(object sender, EventArgs e)
        {
            if (Boards == default)
            {
                Boards = new Boards();
                Boards.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    Boards.ChangeWallet(Operater);
            }
            Container.DockPanel.AddContent(Boards);
        }
        public void OpenEvent(EngraveTx engraveTx)
        {
            using (DiggList dialog = new DiggList(this, Operater, engraveTx))
            {
                var result = dialog.ShowDialog();
            }
        }
        public void OpenEventBoard(string boardKey)
        {
            Container.DockPanel.DoInvoke(() =>
            {
                if (!BoardList.TryGetValue(boardKey, out EventList gr))
                {
                    var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
                    if (bizPlugin.IsNull()) return;
                    if (BoardKey.TryParser(boardKey, out BoardKey key))
                    {
                        var boardId = bizPlugin.GetBoard(key);
                        if (boardId.IsNull()) return;
                        var tx = Blockchain.Singleton.GetTransaction(boardId);
                        if (tx.IsNull()) return;
                        if (tx is EventTransaction et)
                        {
                            if (et.EventType == EventType.Board)
                            {
                                Board board = et.Data.AsSerializable<Board>();
                                gr = new EventList(key, board);
                                gr.Module = this;
                                if (Operater != default && Operater.Wallet != default)
                                    gr.ChangeWallet(Operater);
                                BoardList[boardKey] = gr;
                            }
                        }
                    }
                }
                if (gr.IsNotNull())
                {
                    Container.DockPanel.AddContent(gr);
                }
            });
        }
        public void PublishEvent(string boardKey)
        {
            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                if (BoardKey.TryParser(boardKey, out BoardKey key))
                {
                    var sh = bizPlugin.GetBoard(key);
                    if (sh.IsNull()) return;
                    var tx = Blockchain.Singleton.GetTransaction(sh);
                    if (tx.IsNull()) return;
                    if (tx is EventTransaction et)
                    {
                        if (et.EventType == EventType.Board)
                        {
                            var board = et.Data.AsSerializable<Board>();
                            if (board.IsNull()) return;
                            if (!board.IsOpen && !Operater.Wallet.ContainsAndHeld(et.ScriptHash)) return;
                            //Create Event
                            using (NewEvent dialog = new NewEvent(Operater, key, board, board.IsOpen ? null : et.ScriptHash))
                            {
                                var result = dialog.ShowDialog();
                                if (result == DialogResult.OK)
                                {
                                    var etx = dialog.GetTransaction(out UInt160 from);
                                    if (etx.IsNotNull())
                                    {
                                        etx = Operater.Wallet.MakeTransaction(etx, from, from);
                                        if (etx.IsNotNull())
                                        {
                                            Operater.SignAndSendTx(etx);
                                            string msg = $"{UIHelper.LocalString("创建事件交易已广播", "Relay event transaction completed")}   {etx.Hash}";
                                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = Bapp });
                                            DarkMessageBox.ShowInformation(msg, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
