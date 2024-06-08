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
using OX.SmartContract;
using Newtonsoft.Json.Serialization;

namespace OX.Wallets.Flash.State
{
    public partial class FlashStateDetail : DarkForm
    {
        public class CommentNode
        {
            public string SenderAddressSplit;
            public string SenderName;
            public string ReplyName;
            public UInt256 Hash;
            public UInt256 ParentHash;
            public FlashStateCommentValue FSCV;
            public List<CommentNode> Sub = new List<CommentNode>();
        }

        INotecase Operater;
        public FlashMessageModule Module { get; set; }
        UInt256 FlashStateHash;
        bool hasBindEvents = false;
        List<string> fsHash = new List<string>();
        List<string> senderHash = new List<string>();
        IEnumerable<KeyValuePair<FlashStateCommentKey, FlashStateCommentValue>> cms;
        public FlashStateDetail(INotecase operater, FlashMessageModule moduel, UInt256 flashStateHash)
        {
            InitializeComponent();
            this.Operater = operater;
            this.Module = moduel;
            this.FlashStateHash = flashStateHash;
            this.bt_ok.Text = UIHelper.LocalString("刷新", "Refresh");
            this.bt_close.Text = UIHelper.LocalString("关闭", "Close");
        }

        private void NewLetter_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("闪态详情", "Flash State Details");
            reload();
        }
        void reload()
        {
            fsHash.Clear();
            senderHash.Clear();
            hasBindEvents = false;
            var fsr = FlashMessageProvider.Instance.GetFlashStatRecord(this.FlashStateHash);
            if (fsr.IsNotNull())
            {
                Random rd = new Random();
                string html = "<html><body style='background-color: rgb(60, 63, 65);width:99%; '>";
                html += "<div style='border-radius:5px;border:1px solid #ffffff;background-color:#ffffff;width:100%;padding:10px;margin-bottom:0px;'>";
                var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                if (fsr.FlashState.TextData.IsNotNullAndEmpty() && fsr.FlashState.TextData.Length > 1)
                {
                    var str = System.Text.Encoding.UTF8.GetString(fsr.FlashState.TextData);
                    if (str.IsNotNullAndEmpty())
                        html += Markdown.ToHtml(str, pipeline);
                }
                if (fsr.FlashState.ImageData.IsNotNullAndEmpty() && fsr.FlashState.ImageData.Length > 1)
                {
                    var base64String = Convert.ToBase64String(fsr.FlashState.ImageData);
                    html += $"<img src='data:image/jpg;base64,{base64String}'/>";
                }
                html += $"<p style='vertical-align:middle;width:99%;font-size:14px;color:#c0c0c0;margin-top:0px;'>";
                foreach (var tag in fsr.FlashState.Tags)
                {
                    html += $"[<a style='margin-left:5px;'>{tag.ToString()}</a>]";
                }
                html += $"</p>";
                html += "</div>";
                var senderSH = fsr.FlashState.Author;
                var sender = senderSH.ToAddress();
                var alias = sender;
                sender = $"{sender}-{rd.Next()}";
                if (!senderHash.Contains(sender))
                    senderHash.Add(sender);
                if (Blockchain.Singleton.GetDomain(senderSH, out byte[] senderdomain))
                {
                    alias = System.Text.Encoding.UTF8.GetString(senderdomain);
                }
                var time = fsr.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                var flag = $"{time}     &nbsp&nbsp&nbsp&nbsp           <a style='height:16px;color:#c0c0c0;font-size:14px;margin-top:5px;' id=\"{sender}\" href=\"#\">{alias}</a> &nbsp&nbsp&nbsp&nbsp<a style='height:16px;color:#c0c0c0;font-size:14px;margin-top:5px;' id=\"_comment\" href=\"#\">{UIHelper.LocalString("评论", "Comment")}</a>";
                html += $"<p style='vertical-align:middle;width:99%;font-size:14px;color:#c0c0c0;margin-top:0px;'>{flag}</p>";

                cms = FlashMessageProvider.Instance.GetFlashStateComments(this.FlashStateHash);
                if (cms.IsNotNullAndEmpty())
                {
                    html += "<div style='background-color:#ffffff;border-top:1px solid  rgb(60, 63, 65);width:99%;padding:20px;'>";
                    Dictionary<UInt256, CommentNode> nodes = new Dictionary<UInt256, CommentNode>();
                    foreach (var c in cms)
                    {
                        if (c.Key.ParentCommentHash == UInt256.Zero)
                        {


                            var addr = $"{c.Value.Sender.ToAddress()}-{rd.Next()}";
                            if (!senderHash.Contains(addr))
                                senderHash.Add(addr);
                            var domainName = c.Value.Sender.ToAddress();
                            if (Blockchain.Singleton.GetDomain(c.Value.Sender, out byte[] domain))
                            {
                                domainName = System.Text.Encoding.UTF8.GetString(domain);
                            }
                            nodes[c.Key.CommentHash] = new CommentNode { SenderAddressSplit = addr, SenderName = domainName, ReplyName = string.Empty, Hash = c.Key.CommentHash, ParentHash = UInt256.Zero, FSCV = c.Value };

                        }
                    }
                    foreach (var c in cms)
                    {
                        if (c.Key.ParentCommentHash != UInt256.Zero)
                        {
                            var pdomainName = string.Empty;
                            var pCs = cms.Where(m => m.Key.CommentHash == c.Key.ParentCommentHash);
                            if (pCs.IsNotNullAndEmpty())
                            {
                                var psender = pCs.FirstOrDefault().Value.Sender;
                                pdomainName = psender.ToAddress();
                                if (Blockchain.Singleton.GetDomain(psender, out byte[] pdomain))
                                {
                                    pdomainName = System.Text.Encoding.UTF8.GetString(pdomain);
                                }
                            }


                            var addr = $"{c.Value.Sender.ToAddress()}-{rd.Next()}";
                            if (!senderHash.Contains(addr))
                                senderHash.Add(addr);
                            var domainName = c.Value.Sender.ToAddress();
                            if (Blockchain.Singleton.GetDomain(c.Value.Sender, out byte[] domain))
                            {
                                domainName = System.Text.Encoding.UTF8.GetString(domain);
                            }
                            if (SearchRoot(cms, c.Key.ParentCommentHash, out UInt256 rootHahs) &&
                                nodes.TryGetValue(rootHahs, out CommentNode pNode))
                            {
                                pNode.Sub.Add(new CommentNode { SenderAddressSplit = addr, SenderName = domainName, ReplyName = pdomainName, Hash = c.Key.CommentHash, ParentHash = UInt256.Zero, FSCV = c.Value });
                            }

                        }
                    }
                    foreach (var n in nodes.OrderBy(m => m.Value.FSCV.Timestamp))
                    {
                        html += $"<p style='padding:0;margin:15px 0px 0px 0px;'><a style='font-size:16px;color:black;padding:0px;margin:0px;'>{System.Text.Encoding.UTF8.GetString(n.Value.FSCV.Data)}</a>";
                        html += $"<br/><span style='font-size:14px;color:#c0c0c0;padding:0px;margin:0px;'><a style='height:16px;color:#c0c0c0;font-size:14px;margin-top:5px;' id=\"{n.Value.SenderAddressSplit}\" href=\"#\">{n.Value.SenderName}</a>&nbsp&nbsp&nbsp&nbsp{n.Value.FSCV.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss")}&nbsp&nbsp<a style='height:16px;color:#c0c0c0;font-size:14px;padding:0px;margin:0px;' id=\"{n.Value.Hash.ToString()}\" href=\"#\">reply</a></span></p>";
                        fsHash.Add(n.Value.Hash.ToString());
                        foreach (var sn in n.Value.Sub.OrderBy(m => m.FSCV.Timestamp))
                        {
                            var rps = UIHelper.LocalString($"&nbsp&nbsp回复&nbsp&nbsp{sn.ReplyName}", $"&nbsp&nbsp reply &nbsp&nbsp{sn.ReplyName}");
                            html += $"<p style='padding:0;margin:8px 0px 0px 30px;'><a style='font-size:16px;color:black;padding:0px;margin:0px;'>{System.Text.Encoding.UTF8.GetString(sn.FSCV.Data)}</a>";
                            html += $"<br/><span style='font-size:14px;color:#c0c0c0;padding:0px;margin:0px;'><a style='height:16px;color:#c0c0c0;font-size:14px;margin-top:5px;' id=\"{sn.SenderAddressSplit}\" href=\"#\">{sn.SenderName}</a>{rps}&nbsp&nbsp&nbsp&nbsp{n.Value.FSCV.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss")}&nbsp&nbsp<a style='height:16px;color:#c0c0c0;font-size:14px;padding:0px;margin:0px;' id=\"{sn.Hash.ToString()}\" href=\"#\">reply</a></span></p>";
                            fsHash.Add(sn.Hash.ToString());
                        }
                    }
                    html += "</div>";
                }
                this.renderer.DocumentText = html + "</body></html>";
            }
        }
        private void btnRetry_Click(object sender, EventArgs e)
        {

        }

        private void renderer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!hasBindEvents)
            {
                renderer.Document.GetElementById("_comment").Click += FlashStateDetail_Click;
                foreach (var fsh in fsHash)
                {
                    renderer.Document.GetElementById(fsh).Click += Comment_Click;
                }
                foreach (var fssender in senderHash)
                {
                    renderer.Document.GetElementById(fssender).Click += FSSender_Click1;
                }
                hasBindEvents = true;
            }
        }
        private void FSSender_Click1(object sender, HtmlElementEventArgs e)
        {
            HtmlElement he = sender as HtmlElement;
            this.Module.OpenSenderHome(he.Id.Split('-')[0]);
            this.Close();
        }
        private void Comment_Click(object sender, HtmlElementEventArgs e)
        {
            HtmlElement he = sender as HtmlElement;
            var fsHashStr = UInt256.Parse(he.Id);
            new NewFlashStateComment(this.Operater, this.FlashStateHash, fsHashStr).ShowDialog();
        }
        private void FlashStateDetail_Click(object sender, HtmlElementEventArgs e)
        {
            new NewFlashStateComment(this.Operater, this.FlashStateHash).ShowDialog();
        }
        public bool SearchRoot(IEnumerable<KeyValuePair<FlashStateCommentKey, FlashStateCommentValue>> pool, UInt256 phash, out UInt256 rootHash)
        {
            var p = pool.FirstOrDefault(m => m.Key.CommentHash == phash);
            if (p.Equals(new KeyValuePair<FlashStateCommentKey, FlashStateCommentValue>()))
            {
                rootHash = default;
                return false;
            }
            rootHash = p.Key.CommentHash;
            if (p.Key.ParentCommentHash == UInt256.Zero) return true;
            return SearchRoot(pool, p.Key.ParentCommentHash, out rootHash);
        }



        private void btnOk_Click(object sender, EventArgs e)
        {
            reload();
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
