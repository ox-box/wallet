using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using System.Windows.Forms;
using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.Ledger;
using System.Linq;
using System;
using OX.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Generic;
using Nethereum.Model;

namespace OX.Wallets.Base
{
    public partial class DialogSlotDetails : DarkDialog
    {
        #region Constructor Region
        INotecase Operator;
        public DialogSlotDetails()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("插槽详情", "Slot Details");
            this.darkLabel1.Text = UIHelper.LocalString("关闭投票:", "Off Voting:");
            this.darkLabel2.Text = UIHelper.LocalString("投票高度:", "Vote Height:");
            this.bt_doVote.Text = UIHelper.LocalString("投票", "Vote");
            btnOk.Text = UIHelper.LocalString("关闭", "Close");
        }
        public DialogSlotDetails(INotecase notecase) : this()
        {
            this.Operator = notecase;
        }
        AccountState Slot = default;
        #endregion

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {

        }

        private void btnYes_Click(object sender, System.EventArgs e)
        {

        }

        private void btnNo_Click(object sender, System.EventArgs e)
        {

        }

        private void btnAbort_Click(object sender, System.EventArgs e)
        {

        }

        private void btnRetry_Click(object sender, System.EventArgs e)
        {

        }

        private void btnIgnore_Click(object sender, System.EventArgs e)
        {

        }

        private void DialogNativeAsset_Load(object sender, System.EventArgs e)
        {
            foreach (var ats in Blockchain.Singleton.GetAllValidSlots())
            {
                string title = string.Empty;
                if (ats.SlotMark.IsNotNullAndEmpty())
                {
                    try
                    {

                        var mark = ats.SlotMark.AsSerializable<SlotMark>();
                        if (mark.IsNotNull())
                        {
                            title = UIHelper.LocalString(System.Text.Encoding.UTF8.GetString(mark.CnTitle), System.Text.Encoding.UTF8.GetString(mark.EnTitle));
                        }

                    }
                    catch
                    {

                    }
                }
                var s = UIHelper.LocalString($"{ats.ScriptHash.ToAddress()}      {title}       {ats.SlotExpire}到期", $"{ats.ScriptHash.ToAddress()}      {title}       Due {ats.SlotExpire}");
                this.lv_slots.Items.Add(new DarkListItem(s) { Tag = ats });
            }
        }

        private void lv_slots_Click(object sender, EventArgs e)
        {

        }

        private void lv_slots_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void lv_votes_SelectedIndicesChanged(object sender, EventArgs e)
        {
            this.darkTextBox1.Text = "0";
            var itemid = this.lv_votes.SelectedIndices?.FirstOrDefault();
            if (itemid.HasValue && this.lv_votes.Items.IsNotNullAndEmpty())
            {
                var item = this.lv_votes.Items[itemid.Value];
                var vote = (KeyValuePair<uint, Fixed8>)item.Tag;
                this.darkTextBox1.Text = vote.Key.ToString();
            }
        }

        private void bt_doVote_Click(object sender, EventArgs e)
        {
            if (uint.TryParse(this.darkTextBox1.Text, out uint index))
            {
                if (index % 10000 == 0 && index > Blockchain.Singleton.HeaderHeight && this.Slot.IsNotNull())
                {
                    using (DialogLockVoteSlot dialog = new DialogLockVoteSlot(this.Operator, this.Slot.ScriptHash, index))
                    {
                        if (dialog.ShowDialog() != DialogResult.OK) return;
                        var tx = dialog.BuildTransaction(out WalletAccount account);
                        if (tx.IsNotNull())
                        {
                            this.Operator.Wallet.MixBuildAndRelaySingleOutputTransaction(tx, account.ScriptHash, tx2 =>
                            {
                                string msg = $"{UIHelper.LocalString("锁仓投票交易已广播", "Relay lock vote transaction completed")}   {tx2.Hash}";
                                DarkMessageBox.ShowInformation(msg, "");
                                this.Close();
                            });
                        }
                    }
                }
            }
        }

        private void darkTextBox1_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as UI.Controls.DarkTextBox;
            if (!uint.TryParse(tb.Text, out uint index))
            {
                var s = tb.Text;
                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    tb.Clear();
                    tb.AppendText(s);
                }
            }
        }

        private void lv_slots_SelectedIndicesChanged(object sender, EventArgs e)
        {
            var itemid = this.lv_slots.SelectedIndices?.FirstOrDefault();
            if (itemid.HasValue)
            {
                this.lv_votes.Items.Clear();
                var item = this.lv_slots.Items[itemid.Value];
                Slot = item.Tag as AccountState;

                var voteList = Blockchain.Singleton.CurrentSnapshot.SlotOffVoteList.TryGet(Slot.ScriptHash);
                if (voteList.IsNotNull() && voteList.Votes.IsNotNullAndEmpty())
                {
                    foreach (var vote in voteList.Votes.OrderByDescending(m => m.Key))
                    {
                        var itm = new DarkListItem($"{vote.Key}         {vote.Value} OXS");
                        itm.Tag = vote;
                        this.lv_votes.Items.Add(itm);
                    }
                }
            }
        }
    }
}
