using OX.Wallets.UI.Config;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Forms;
using OX.Bapps;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.NEP6;
using System.Drawing.Imaging;
using OX.Wallets.UI;
using OX.Persistence;
using OX.IO;
using System.Collections;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Wallets;
using OX.SmartContract;
using System.ComponentModel.Design.Serialization;
using OX.Wallets.Flash.Chat;
using System.Threading;
using OX.Wallets.Eths;

namespace OX.Wallets.Flash.Chat
{
    public partial class UniTalk : DarkDocument, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        protected INotecase Operater;
        Tuple<UInt256, UnicastTalkLineValue> TP;

        UnicastChatbox chatbox;
        UnicastChatboxInfo cbi;
        #region Constructor Region

        public UniTalk()
        {
            InitializeComponent();
        }

        public UniTalk(INotecase notecase, Tuple<UInt256, UnicastTalkLineValue> tp)
            : this()
        {
            this.Operater = notecase;
            this.TP = tp;
            var name = tp.Item2.Label;
            var remoteAddress = Contract.CreateSignatureRedeemScript(tp.Item2.Remote).ToScriptHash().ToAddress();
            var localAddres = tp.Item2.Local.ToAddress();
            if (name.IsNullOrEmpty())
                name = remoteAddress.Omit();
            this.DockText = name;
            this.SizeChanged += UniTalk_SizeChanged;

            var walletAccount = this.Operater.Wallet.GetAccount(tp.Item2.Local);
            if (walletAccount.IsNotNull() && !walletAccount.WatchOnly)
            {
                 cbi = new UnicastChatboxInfo();
                cbi.TP = tp;
                cbi.LocalAccount = walletAccount;
                cbi.TalkLabel = name;
                cbi.LocalAddress = localAddres;
                cbi.RemoteAddress = remoteAddress;
                cbi.TalkName = $"{remoteAddress}      <=>      {localAddres}";
                cbi.ChatPlaceholder = UIHelper.LocalString("请输入信息...", "Please enter a message...");
                chatbox = new UnicastChatbox(this.Operater, cbi);
                chatbox.Name = "chat_box";
                chatbox.Dock = DockStyle.Fill;
                this.Controls.Add(chatbox);
            }

        }








        private void UniTalk_SizeChanged(object sender, EventArgs e)
        {
        }



        #endregion

        #region Event Handler Region

        public override void Close()
        {
            var result = DarkMessageBox.ShowWarning(UIHelper.LocalString($"确定要退出线路吗?", $"Are you sure you want to exit the talk line?"), UIHelper.LocalString("退出线路", "exit talk line"), DarkDialogButton.YesNo);
            if (result == DialogResult.No)
                return;
            base.Close();
        }
        public  void DoClose()
        {
            base.Close();
        }
        #endregion


        #region IBlockChainTrigger

        public void OnBappEvent(BappEvent be)
        {

        }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void HeartBeat(HeartBeatContext context)
        {
            this.chatbox.HeartBeat(context);
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
        public void OnFlashMessage(FlashMessage flashMessage)
        {
            if (chatbox.IsNotNull())
            {
                chatbox.OnFlashMessage(flashMessage);
            }
        }
        public virtual void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            reload();
        }
        public void OnRebuild()
        {
        }
        #endregion

        void reload()
        {

        }



        private void UniTalk_Enter(object sender, EventArgs e)
        {
            this.chatbox.ReloadOldRecords();

        }
        public void ClearRecords()
        {
            this.chatbox.ClearRecords();
        }
        public void ResetLabel(string label)
        {
            this.DockText = label;
            cbi.TalkLabel = label;
            this.chatbox.ResetLabel();
        }
    }
}
