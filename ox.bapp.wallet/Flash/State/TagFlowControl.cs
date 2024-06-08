using OX.Network.P2P.Payloads;
using OX.Wallets.UI.Config;
using OX.Wallets.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OX.Wallets.Flash.State
{
    public partial class TagFlowControl : Button
    {
        protected StateLine StateLine;
        public FlashStateTag FSTag { get; private set; }
        public TagFlowControl(StateLine stateLine, FlashStateTag tag) : base()
        {
            this.BackColor = Colors.GreySelection;
            this.AutoSize = true;
            this.StateLine = stateLine;
            this.FSTag = tag;
            this.Text = tag.ToString();
            this.Click += TagControl_Click;
            this.LostFocus += TagFlowControl_LostFocus;
        }

        private void TagFlowControl_LostFocus(object sender, EventArgs e)
        {
            //this.SpecialBorderColor = Colors.GreyBackground;
        }

        public void TagControl_Click(object sender, EventArgs e)
        {
            this.StateLine.PageIndex = 0;
            this.DoTag();
            foreach (TagFlowControl c in this.Parent.Controls)
            {
                c.BackColor = Colors.GreySelection;
            }
            this.BackColor = Color.OrangeRed;
            this.Parent.Invalidate();
        }
        public virtual void DoTag()
        {
            this.StateLine.TagKind = 2;
            this.StateLine.CurrentFunc = pageIndex =>
            {
                var range = FlashMessageProvider.Instance.GetLastRangeForTag(this.FSTag);
                if (range == uint.MaxValue)
                {
                    this.StateLine.MaxPageSize = 0;
                    return default;
                }
                this.StateLine.MaxPageSize = range;
                return FlashMessageProvider.Instance.GetCachedFlashStateRecordsForTag(this.FSTag, range - pageIndex);
            };
            this.StateLine.ReloadFlashStates();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
             var textColor = Color.White;  
            using (var b = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(b, rect);
            }
            var textOffsetX = 0;
            var textOffsetY = 0;
            using (var b = new SolidBrush(textColor))
            {
                var modRect = new Rectangle(rect.Left + textOffsetX + Padding.Left,
                                            rect.Top + textOffsetY + Padding.Top, rect.Width - Padding.Horizontal,
                                            rect.Height - Padding.Vertical);
                var stringFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                g.DrawString(Text, Font, b, modRect, stringFormat);
            }
        }
    }
    public class HotTagFlowControl : TagFlowControl
    {
        public HotTagFlowControl(StateLine stateLine) : base(stateLine, new FlashStateTag(UIHelper.LocalString("热点", "Hot")))
        {

        }
        public override void DoTag()
        {
            this.StateLine.TagKind = 1;
            this.StateLine.CurrentFunc = pageIndex =>
            {
                if (pageIndex >= 100) return default;
                this.StateLine.MaxPageSize = 100;
                return FlashMessageProvider.Instance.GetCachedHotFlashStateRecords(pageIndex);
            };
            this.StateLine.ReloadFlashStates();
        }
    }
    public class LatestTagFlowControl : TagFlowControl
    {
        public LatestTagFlowControl(StateLine stateLine) : base(stateLine, new FlashStateTag(UIHelper.LocalString("最新", "Latest")))
        {

        }
        public override void DoTag()
        {
            this.StateLine.TagKind = 0;
            this.StateLine.CurrentFunc = pageIndex =>
            {
                var range = FlashMessageProvider.Instance.GetLastRangeForSender(UInt160.Zero);
                if (range == uint.MaxValue)
                {
                    this.StateLine.MaxPageSize = 0;
                    return default;
                }
                this.StateLine.MaxPageSize = range;
                return FlashMessageProvider.Instance.GetCachedFlashStateRecordsForLatest(range - pageIndex);
            };
            this.StateLine.ReloadFlashStates();
        }
    }
}
