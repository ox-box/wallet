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
using OX.Ledger;
using OX.Persistence;
using OX.Wallets.Flash.Chat;
using System.IO;
using Nethereum.Util;
using Akka.Pattern;

namespace OX.Wallets.Flash.State
{
    public partial class NewFlashState : DarkForm
    {
        public class AccountDescriptor
        {
            public WalletAccount Account;
            public override string ToString()
            {
                return Account.Address;
            }
        }
        INotecase Operater;
        public Module Module { get; set; }
        WalletAccount Local;
        byte[] ImageData = new byte[0];
        public NewFlashState(INotecase operater, WalletAccount local = default)
        {
            InitializeComponent();
            this.Operater = operater;
            Local = local;
            ofd.Filter = UIHelper.LocalString("图片|*.gif;*.jpg;*.jpeg;*.bmp;*.jfif;*.png;", "Image|*.gif;*.jpg;*.jpeg;*.bmp;*.jfif;*.png;");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            renderer.DocumentText = Markdown.ToHtml(editor.Text, pipeline);
        }

        private void tb_b_Click(object sender, EventArgs e)
        {
            string insertText = "**";
            int selectionStart = editor.SelectionStart;
            int selectionStop = editor.SelectionStart + editor.SelectionLength + insertText.Length;

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.Text = editor.Text.Insert(selectionStop, insertText);
            editor.SelectionStart = selectionStop + insertText.Length;
        }

        private void tb_i_Click(object sender, EventArgs e)
        {
            string insertText = "*";
            int selectionStart = editor.SelectionStart;
            int selectionStop = editor.SelectionStart + editor.SelectionLength + insertText.Length;

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.Text = editor.Text.Insert(selectionStop, insertText);
            editor.SelectionStart = selectionStop + insertText.Length;
        }

        private void tb_l1_Click(object sender, EventArgs e)
        {
            string insertText = "- ";
            int selectionStart = editor.GetFirstCharIndexOfCurrentLine();

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.SelectionStart = selectionStart + insertText.Length;
        }

        private void tb_l2_Click(object sender, EventArgs e)
        {
            string insertText = "1. ";
            int selectionStart = editor.GetFirstCharIndexOfCurrentLine();

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.SelectionStart = selectionStart + insertText.Length;
        }
        private void insertHeading(int level)
        {
            string insertText = "";
            for (int i = 0; i < level; i++)
            {
                insertText += '#';
            }
            insertText += " ";
            int selectionStart = editor.GetFirstCharIndexOfCurrentLine();

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.SelectionStart = selectionStart + level + 1;

        }

        private void tb_h1_Click(object sender, EventArgs e)
        {
            insertHeading(1);
        }

        private void tb_h2_Click(object sender, EventArgs e)
        {
            insertHeading(2);
        }

        private void tb_h3_Click(object sender, EventArgs e)
        {
            insertHeading(3);
        }

        private void tb_h4_Click(object sender, EventArgs e)
        {
            insertHeading(4);
        }

        private void tb_h5_Click(object sender, EventArgs e)
        {
            insertHeading(5);
        }

        private void tb_h6_Click(object sender, EventArgs e)
        {
            insertHeading(6);
        }

        private void NewLetter_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("更新闪态", "Update Fash State");
            this.lb_from.Text = UIHelper.LocalString("账户:", "Account:");
            this.bt_ok.Text = UIHelper.LocalString("更新", "Update");
            this.bt_cancel.Text = UIHelper.LocalString("关闭", "Close");
            this.lb_tags.Text = UIHelper.LocalString("标签:", "Tags:");
            initAccounts();
            check();
        }
        void initAccounts()
        {
            if (this.Operater.IsNotNull())
            {
                this.DoInvoke(() =>
                {
                    this.cbAccounts.Items.Clear();
                    if (Local.IsNotNull())
                    {
                        this.cbAccounts.Items.Add(new AccountDescriptor { Account = Local });
                    }
                    else
                    {
                        foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                        {
                            if (this.Local.IsNull())
                                this.Local = act;
                            this.cbAccounts.Items.Add(new AccountDescriptor { Account = act });
                        }
                    }
                    this.cbAccounts.SelectedIndex = 0;


                });
            }
        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bt_ok_Click(object sender, EventArgs e)
        {
            try
            {
                var ad = this.cbAccounts.SelectedItem as AccountDescriptor;
                if (ad.IsNotNull())
                {
                    var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(ad.Account.ScriptHash);
                    if (accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out uint _))
                    {
                        byte[] textData = new byte[0];
                        var text = this.editor.Text;
                        if (text.IsNotAnEmptyAddress())
                            textData = System.Text.Encoding.UTF8.GetBytes(text);
                        var tcs = this.GetTagControls();
                        FlashStateTag[] tags = default;
                        if (tcs.IsNotNullAndEmpty())
                        {
                            tags = tcs.Select(m => new FlashStateTag(m.TagText)).ToArray();
                        }
                        var fs = new FlashState(ad.Account.ScriptHash, Blockchain.Singleton.HeaderHeight, textData, this.ImageData, tags);
                        this.Operater.SignAndSendFlashMessage(fs);
                        this.Close();
                    }
                }
            }
            catch
            {

            }
        }

        private void pb_Paint(object sender, PaintEventArgs e)
        {
            if (sender is PictureBox box && box.Image.IsNull())
            {
                string text = UIHelper.LocalString("点击添加图片", "Click to add image");
                Font font = new Font("Arial", 12);
                Brush brush = new SolidBrush(Color.White);
                e.Graphics.DrawString(text, font, brush, new PointF(50, 50));
            }
        }

        private void pb_Click(object sender, EventArgs e)
        {
            ofd.Reset();
            ofd.Multiselect = false;
            ofd.Filter = UIHelper.LocalString("图片|*.gif;*.jpg;*.jpeg;*.bmp;*.jfif;*.png;", "Image|*.gif;*.jpg;*.jpeg;*.bmp;*.jfif;*.png;");
            var result = ofd.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selected = ofd.FileName;
                if (ImageCompressHelper.CompressImage(selected, FlashState.MaxImageDataSize, out byte[] bs))
                {
                    this.ImageData = bs;
                    this.pb.Image = Bitmap.FromStream(new MemoryStream(bs));
                }
            }
        }
        void check()
        {
            var ad = this.cbAccounts.SelectedItem as AccountDescriptor;
            if (ad.IsNull())
            {
                this.bt_ok.Enabled = false;
                return;
            }
            var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(ad.Account.ScriptHash);
            this.bt_ok.Enabled = accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out uint _);
        }

        private void cbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            check();
        }

        private void tb_tag_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                var tag = textBox.Text;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (tag.IsNotNullAndEmpty() && !ContainTag(tag, out IEnumerable<TagControl> tcs) && tcs.Count() < FlashState.MaxTagsNumber)
                    {
                        this.pl_tags.Controls.Add(new TagControl(tag) { Height = this.pl_tags.Height, AutoSize = true });
                        textBox.Clear();
                    }
                    e.Handled = true;
                    return;
                }
                FlashStateTag fst = new FlashStateTag(tag);
                if (e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete && fst.Size >= FlashState.MaxTagSize - 3)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ' ')
            {
                e.Handled = false;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar >= 0x4e00 && e.KeyChar <= 0x9fa5)
            {
                e.Handled = false;
            }
            else if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        IEnumerable<TagControl> GetTagControls()
        {
            foreach (var c in this.pl_tags.Controls)
            {
                if (c is TagControl tc) yield return tc;
            }
        }
        bool ContainTag(string tag, out IEnumerable<TagControl> tcs)
        {
            tcs = GetTagControls();
            if (tcs.IsNullOrEmpty()) return false;
            return tcs.Select(m => m.TagText).Contains(tag);
        }
    }
}
