using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Cryptography;
using OX.Cryptography.ECC;
using OX.Network.P2P.Payloads;
using System.Diagnostics;
using OX.Wallets.UI.Config;
using OX.Ledger;
using static System.Net.Mime.MediaTypeNames;

namespace OX.Wallets.Flash
{
    public partial class InviteMulticast : DarkDialog
    {
        string Label;
        public InviteMulticast(string label)
        {
            Label = label;
            InitializeComponent();
        }

        public IEnumerable<ECPoint> GetPublics()
        {
            foreach (var item in this.lv_pubs.Items)
            {
                yield return item.Tag as ECPoint;
            }
        }
        private void InviteMulticast_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("邀请群聊", "Invite Multicast");
            this.lb_linename.Text = UIHelper.LocalString($"线路名称:{Label}", $"Talk Line Name:{Label}");
            this.lb_pubkey.Text = UIHelper.LocalString($"受邀人公钥:", $"Invitee Public Key:");
            this.bt_add.Text = UIHelper.LocalString("增加", "Append");
            this.bt_remove.Text = UIHelper.LocalString("删除", "Remove");
            this.btnOk.Text = UIHelper.LocalString("邀请", "Invite");
            this.btnCancel.Text = UIHelper.LocalString("关闭", "Close");
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            if (ECPoint.TryParse(this.tb_pubkey.Text, ECCurve.Secp256r1, out ECPoint pubkey))
            {
                var item = new DarkListItem(this.tb_pubkey.Text);
                item.Tag = pubkey;
                this.lv_pubs.Items.Insert(0, item);
            }
            bt_add.Enabled = this.lv_pubs.Items.Count <= 40;
        }

        private void bt_remove_Click(object sender, EventArgs e)
        {
            var ids = this.lv_pubs.SelectedIndices;
            if (ids != default && ids.Count == 1)
            {
                var index = ids.FirstOrDefault();
                var obj = this.lv_pubs.Items[index];
                this.lv_pubs.Items.Remove(obj);
            }
            bt_add.Enabled = this.lv_pubs.Items.Count <= 40;
        }
    }
}
