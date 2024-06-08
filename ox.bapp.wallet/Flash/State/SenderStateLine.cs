using OX.Bapps;
using OX.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;
using OX.Wallets.Base;
using OX.Cryptography.ECC;
using System.IO;
using System.Drawing.Imaging;
using OX.Wallets.Base.Wallets;
using OX.SmartContract;
using Markdig;
using OX.Wallets.Flash;
using System.Diagnostics.Metrics;
using OX.Cryptography;
using OX.Wallets.Base.Flash;
using OX.Wallets.Flash.State;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OX.Persistence;

namespace OX.Wallets.Flash.State
{
    public partial class SenderStateLine : DarkDocument, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        protected INotecase Operater;
        UInt160 Sender = default;
        bool NeedReload = false;
        List<string> fsHash = new List<string>();
        bool hasBindEvents = false;
        public uint PageIndex { get; set; } = 0;
        public uint MaxPageSize { get; set; } = 0;


        #region Constructor Region

        public SenderStateLine(UInt160 sender)
        {
            this.Sender = sender;
            InitializeComponent();
            var senderAddr = sender.ToAddress();
            var alias = senderAddr;
            if (Blockchain.Singleton.GetDomain(sender, out byte[] pdomain))
            {
                alias = System.Text.Encoding.UTF8.GetString(pdomain);
            }
            this.DockText = alias;
            this.lb_alias.Text = alias;
            this.lb_address.Text = senderAddr;
            this.bt_pre.Text = UIHelper.LocalString("上一页", "previous page");
            this.bt_next.Text = UIHelper.LocalString("下一页", "next page");
            this.bt_follow.Text = UIHelper.LocalString("关注", "Follow");
            this.bt_first.Text = UIHelper.LocalString("首页", "first page");
            this.SizeChanged += FlashState_SizeChanged;
        }



        private void FlashState_SizeChanged(object sender, EventArgs e)
        {
        }


        #endregion

        #region Event Handler Region

        public override void Close()
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

        }

        public void BeforeOnBlock(Block block)
        {
            if (NeedReload)
            {
                NeedReload = false;
                InitLoadLatest();
            }
        }
        public void OnBlock(Block block) { }

        public void AfterOnBlock(Block block)
        {

        }
        public void ChangeWallet(INotecase operater)
        {
            if (operater.IsNull()) return;
            this.Operater = operater;
            InitLoadLatest();
        }
        public void OnRebuild() { }
        public void OnFlashMessage(FlashMessage flashMessage)
        {
            if (FlashMemoryHelper.FlashHashs.Contains(flashMessage.Hash))
            {
                FlashMemoryHelper.FlashHashs.Enqueue(flashMessage.Hash);
                switch (flashMessage.Type)
                {
                    case FlashMessageType.FlashState:
                        FlashState fs = flashMessage as FlashState;
                        if (fs.IsNotNull() && (FlashMessageProvider.Instance.LogFilter.IsNull() || FlashMessageProvider.Instance.LogFilter.StateInputFilter(fs)))
                        {
                            if (fs.Author == this.Sender)
                                NeedReload = true;
                        }
                        break;
                    case FlashMessageType.FlashStateComment:
                        FlashStateComment fsc = flashMessage as FlashStateComment;
                        if (fsc.IsNotNull() && (FlashMessageProvider.Instance.LogFilter.IsNull() || FlashMessageProvider.Instance.LogFilter.CommentInputFilter(fsc)))
                        {
                        }
                        break;
                }
            }
        }
        #endregion


        public void InitLoadLatest()
        {
            ReloadFlashStates();
        }

        public void ReloadFlashStates()
        {

            this.DoInvoke(() =>
            {
                string html = "<html><body style='background-color: rgb(60, 63, 65);width:100%; '>";
                this.fsHash.Clear();
                var fss = this.GetRecords(this.PageIndex);
                if (fss.IsNotNullAndEmpty())
                {
                    foreach (var fsh in fss)
                    {
                        html += "<div style='border-radius:5px;border:1px solid #ffffff;background-color:#ffffff;width:100%;padding:10px;margin-bottom:0px;'>";
                        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                        if (fsh.FlashState.TextData.IsNotNullAndEmpty() && fsh.FlashState.TextData.Length > 1)
                        {
                            var str = System.Text.Encoding.UTF8.GetString(fsh.FlashState.TextData);
                            if (str.IsNotNullAndEmpty())
                                html += Markdown.ToHtml(str, pipeline);
                        }
                        if (fsh.FlashState.ImageData.IsNotNullAndEmpty() && fsh.FlashState.ImageData.Length > 1)
                        {
                            var base64String = Convert.ToBase64String(fsh.FlashState.ImageData);
                            html += $"<img src='data:image/jpg;base64,{base64String}'/>";
                        }
                        html += $"<p style='vertical-align:middle;width:99%;font-size:14px;color:#c0c0c0;margin-top:0px;'>";
                        foreach (var tag in fsh.FlashState.Tags)
                        {
                            html += $"[<a style='margin-left:5px;'>{tag.ToString()}</a>]";
                        }
                        html += $"</p>";
                        html += "</div>";

                        var time = fsh.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                        var flag = $"{time}&nbsp&nbsp&nbsp&nbsp<a style='height:16px;color:#c0c0c0;font-size:14px;margin-top:5px;' id=\"{fsh.FlashState.Hash.ToString()}\" href=\"#\">{UIHelper.LocalString("详情", "Details")}<svg t=\"1712984589295\" class=\"icon\" viewBox=\"0 0 1024 1024\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" p-id=\"1128\" width=\"48\" height=\"48\"><path d=\"M856.15 316.42c-13.02-64.46-73.18-114.96-149.98-125.89-126.84-18.04-256.39-18.04-383.23 0-76.8 10.93-136.97 61.42-149.98 125.88-21.5 106.46-21.5 215.2 0 321.66 10.16 50.34 49.08 92.16 102.22 113.27a58.086 58.086 0 0 1 34.14 37.03c3.33 10.92 7.13 22.47 11.47 34.52 9.99 27.78 44.57 37.06 67.14 18.03 14.58-12.29 31.78-27.82 50.31-46.61a57.95 57.95 0 0 1 42.66-17.25c75.29 1.86 150.75-2.51 225.27-13.11 76.8-10.93 136.96-61.42 149.98-125.88 21.5-106.46 21.5-215.2 0-321.66z\" fill=\"#333333\" p-id=\"1129\"></path><path d=\"M517 596.45c-70.1 0-140.2-3.35-209.8-10.06-13.19-1.27-22.86-13-21.59-26.19 1.27-13.19 13-22.86 26.19-21.59 136.16 13.12 274.24 13.12 410.39 0 13.19-1.27 24.92 8.39 26.19 21.59 1.27 13.19-8.39 24.92-21.59 26.19-69.61 6.71-139.7 10.06-209.8 10.06zM517.39 446.47c-70.14 0-140.61-3.37-210.19-10.08-13.19-1.27-22.86-13-21.59-26.19 1.27-13.19 13-22.86 26.19-21.59 86.26 8.31 173.92 11.39 260.55 9.15 13.24-0.36 24.27 10.12 24.61 23.37 0.34 13.25-10.12 24.27-23.37 24.61-18.69 0.48-37.45 0.73-56.21 0.73z\" fill=\"#FFFFFF\" p-id=\"1130\"></path></svg></a>";
                        html += $"<p style='vertical-align:middle;width:99%;font-size:14px;color:#c0c0c0;margin-top:0px;'>{flag}</p>";

                        fsHash.Add(fsh.FlashState.Hash.ToString());
                    }
                }
                this.renderer.DocumentText = html + "</body></html>";
                hasBindEvents = false;
                this.bt_pre.Enabled = this.PageIndex > 0;
                this.bt_next.Enabled = this.PageIndex < MaxPageSize;
                this.bt_first.Enabled = this.PageIndex != 0;
            });

        }

        IOrderedEnumerable<FlashStateRecord> GetRecords(uint pageIndex)
        {
            var range = FlashMessageProvider.Instance.GetLastRangeForSender(this.Sender);
            if (range == uint.MaxValue)
            {
                this.MaxPageSize = 0;
                return default;
            }
            this.MaxPageSize = range;
            return FlashMessageProvider.Instance.GetCachedFlashStateRecordsForSender(this.Sender, range - pageIndex);
        }
        private void renderer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!hasBindEvents)
            {
                foreach (var fsh in fsHash)
                {
                    renderer.Document.GetElementById(fsh).Click += FSDetail_Click;
                }
                hasBindEvents = true;
            }
        }


        private void FSDetail_Click(object sender, HtmlElementEventArgs e)
        {
            HtmlElement he = sender as HtmlElement;
            var fsHashStr = UInt256.Parse(he.Id);
            new FlashStateDetail(this.Operater, this.Module as FlashMessageModule, fsHashStr).ShowDialog();
        }



        private void bt_pre_Click(object sender, EventArgs e)
        {
            this.PageIndex--;
            ReloadFlashStates();
        }

        private void bt_next_Click(object sender, EventArgs e)
        {
            this.PageIndex++;
            ReloadFlashStates();
        }

        private void bt_follow_Click(object sender, EventArgs e)
        {

        }

        private void bt_first_Click(object sender, EventArgs e)
        {
            this.PageIndex = 0;
            ReloadFlashStates();
        }
    }
}
