using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using OX.Wallets;
using OX.Wallets.UI.Controls;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OX.Wallets.Base
{
    public partial class StockControl : ContainerControl
    {
        INotecase Operater;
        bool IsHour = false;
        List<NFTSellValue> Data = new List<NFTSellValue>();
        List<NFTSellValue> displayData = new List<NFTSellValue>();
        public StockControl(INotecase notecase)
        {
            this.Operater = notecase;
            InitializeComponent();
            //加载事件
            this.HandleCreated += StockControl_HandleCreated;
            this.SizeChanged += StockControl_SizeChanged;
            this.Paint += StockControl_Paint;
            this.PreviewKeyDown += StockControl_PreviewKeyDown;
            this.KeyDown += StockControl_KeyDown;
            this.KeyUp += StockControl_KeyUp;
            this.MouseMove += StockControl_MouseMove;
            this.MouseUp += StockControl_MouseUp;
            this.MouseDown += StockControl_MouseDown;
            this.MouseWheel += StockControl_MouseWheel;
            this.MouseEnter += StockControl_MouseEnter;
        }

        private void StockControl_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }
        #region fields
        private DateTime lastMouseMoveTime = DateTime.Now;
        private object refresh_lock = new object();
        [Browsable(true)]
        public bool ShowLeftScale { get; set; } = true;
        [Browsable(true)]
        public bool ShowRightScale { get; set; } = true;

        [Browsable(true)]
        public int RightPixSpace { get; set; }
        [Browsable(true)]
        public int RightOrderSpace { get; set; }
        [Browsable(true)]
        public int AxisSpace { get; set; } = 50;
        [Browsable(true)]
        public int AreaSplit { get; set; } = 60;

        [Browsable(false)]
        public bool ShowCrossHair { get; set; } = false;


        public int FirstRecord { get; set; }
        public Color GridColor { get; set; } = Color.Gray;
        public Color YanColor { get; set; } = Color.Red;
        public Color YinColor { get; set; } = Color.Green;
        public Color CrossHairColor { get; set; } = Color.White;
        public Pen GridPen { get; set; } = new Pen(Color.Gray);
        public Pen YanPen { get; set; } = new Pen(Color.Red);
        public Pen YinPen { get; set; } = new Pen(Color.Green);
        public Pen CrossHair_Pen { get; set; } = new Pen(Color.White);
        public Brush YanBrush = new SolidBrush(Color.Red);
        public Brush YinBrush = new SolidBrush(Color.Green);
        public Brush GridBrush = new SolidBrush(Color.Gray);
        public Brush Xtip_Brush { get; set; } = new SolidBrush(Color.FromArgb(100, Color.Red));
        private Pen XTipFont_Pen { get; set; } = new Pen(Color.White);

        public Font StockFont { get; set; } = new Font("New Times Roman", 9);
        ~StockControl()
        {
            if (this.GridPen != default) this.GridPen.Dispose();
            if (this.YanPen != default) this.YanPen.Dispose();
            if (this.YinPen != default) this.YinPen.Dispose();
            if (this.CrossHair_Pen != default) this.CrossHair_Pen.Dispose();
            if (this.YanBrush != default) this.YanBrush.Dispose();
            if (this.YinBrush != default) this.YinBrush.Dispose();
            if (this.StockFont != default) this.StockFont.Dispose();
            if (this.GridBrush != default) this.GridBrush.Dispose();
            if (this.Xtip_Brush != default) this.Xtip_Brush.Dispose();
            if (this.XTipFont_Pen != default) this.XTipFont_Pen.Dispose();
        }
        #endregion
        #region events
        private void StockControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomIn(2);
            }
            else
            {
                ZoomOut(2);
            }
            this.RefreshGraph(false);
            this.Focus();
        }

        private void StockControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1)
                {

                }
                else if (e.Clicks == 2)
                {
                    //双击显示或隐藏十字线
                    this.ShowCrossHair = !this.ShowCrossHair;
                    RefreshGraph(false);
                }
            }
        }

        private void Sm_Click1(object sender, EventArgs e)
        {
            IsHour = !IsHour;
            ReloadInitData();
        }

        private void Sm_Click(object sender, EventArgs e)
        {
        }

        private void StockControl_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void StockControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastMouseMoveTime.AddTicks(100000) < DateTime.Now)
            {
                RefreshGraph(false);
            }
            lastMouseMoveTime = DateTime.Now;
        }

        private void StockControl_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void StockControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void StockControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyData == Keys.Left)//正常左移动为1步长
            {
                if (this.FirstRecord > 0)
                    this.FirstRecord--;
            }
            if (e.KeyData == Keys.Right)//正常右移动为1步长
            {
                var w = this.DisplayRectangle.Width - this.RightPixSpace - this.RightOrderSpace;
                var count = w / this.AxisSpace;
                if (count <= this.displayData.Count)
                {
                    if (this.FirstRecord < this.Data.Count - 1)
                        this.FirstRecord++;
                }
            }

            if (e.Control && e.KeyCode == Keys.Left)//组合Ctrl左移动为50步长
            {
                if (this.FirstRecord >= 50)
                    this.FirstRecord -= 50;
                else
                    this.FirstRecord = 0;
            }

            if (e.Control && e.KeyCode == Keys.Right)//组合Ctrl右移动为50步长
            {
                var w = this.DisplayRectangle.Width - this.RightPixSpace - this.RightOrderSpace;
                var count = w / this.AxisSpace;
                if (count <= this.displayData.Count)
                {
                    if (this.FirstRecord + 50 < this.Data.Count - 1)
                        this.FirstRecord += 50;
                    else
                        this.FirstRecord = this.Data.Count - 1;
                }
            }
            if (e.KeyData == Keys.Up)
            {
                this.ZoomIn(1);
            }
            if (e.KeyData == Keys.Down)
            {
                this.ZoomOut(1);
            }
            RefreshGraph(false);
            this.Focus();
        }

        private void StockControl_Paint(object sender, PaintEventArgs e)
        {
            DrawGraph();
        }

        private void StockControl_SizeChanged(object sender, EventArgs e)
        {
            if (this.Size.Width != 0 && this.Size.Height != 0)
            {
                RefreshGraph(false);
            }
        }

        private void StockControl_HandleCreated(object sender, EventArgs e)
        {

        }
        #endregion
        #region methods

        /// 重置为空的图像
        /// </summary>
        public void ResetNullGraph()
        {
            this.AxisSpace = 50;
            this.RightPixSpace = 0;
            this.ShowLeftScale = false;
            this.ShowRightScale = false;
        }
        public void Init(IEnumerable<KeyValuePair<NFTSellKey, NFTSellValue>> data)
        {
            if (data.IsNotNullAndEmpty())
                this.Data = data.Select(m => m.Value).OrderBy(m => m.Time).ToList();
            ReloadInitData();
        }
        void ReloadInitData()
        {
            RefreshGraph(true);
        }



        public void RefreshGraph(bool InitFirst)
        {
            ResetFirstRecord(InitFirst);
            DrawGraph();
        }

        public void ResetFirstRecord(bool InitFirst)
        {
            var dc = this.Data.Count;
            if (dc == 0) return;
            if (dc < this.FirstRecord)
            {
                this.FirstRecord = dc - 1;
            };
            var w = this.DisplayRectangle.Width - this.RightPixSpace - this.RightOrderSpace;
            if (w <= 0) return;
            var count = w / this.AxisSpace;

            if (InitFirst)
            {
                if (count > dc)
                {
                    this.FirstRecord = 0;
                    displayData = this.Data.Take(new Range(new Index(this.FirstRecord), new Index(dc))).OrderBy(m => m.Time).ToList();
                }
                else
                {
                    this.FirstRecord = dc - count;
                    displayData = this.Data.Take(new Range(new Index(this.FirstRecord), new Index(dc))).OrderBy(m => m.Time).ToList();
                }
            }
            else
            {
                var c = count + this.FirstRecord;
                if (c > dc) c = dc;
                displayData = this.Data.Take(new Range(new Index(this.FirstRecord), new Index(c))).OrderBy(m => m.Time).ToList();
            }
        }

        public void DrawGraph()
        {
            PaintGraph(this.DisplayRectangle);
        }

        public void PaintGraph(Rectangle drawRectangle)
        {
            lock (refresh_lock)
            {
                BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
                BufferedGraphics myBuffer = currentContext.Allocate(this.CreateGraphics(), drawRectangle);
                Graphics g = myBuffer.Graphics;
                if (this.displayData.IsNotNullAndEmpty())
                    DrawScale(g);
                myBuffer.Render();
                myBuffer.Dispose();
            }
        }
        public void DrawScale(Graphics g)
        {
            var H = this.DisplayRectangle.Height - this.AreaSplit * 2;
            var maxAmount = (decimal)this.displayData.OrderByDescending(m => m.Amount).FirstOrDefault().Amount;
            var minAmount = (decimal)this.displayData.OrderBy(m => m.Amount).FirstOrDefault().Amount;
            var sp = maxAmount - minAmount;
            if (sp == 0) return;
            var mp = H / sp;
            int left = 0;
            Point prePoint = default;
            foreach (var data in this.displayData.OrderBy(m => m.Time))
            {
                var amt = (decimal)data.Amount;
                var pheight = H - mp * (amt - minAmount) + this.AreaSplit;
                left += AxisSpace;
                var p = new Point(left, (int)pheight);
                if (prePoint != default)
                {
                    g.DrawLine(this.GridPen, prePoint, p);
                }
                string s = amt.ToString("f4");
                Brush bBrush = new SolidBrush(this.CrossHairColor);
                g.DrawString(s, this.StockFont, bBrush, new PointF(p.X + 10, p.Y + 10));
                prePoint = p;
            }
        }


        /// <summary>
        /// 放大
        /// </summary>
        /// <param name="step"></param>
        private void ZoomIn(int step)
        {
            if (this.AxisSpace < 200)
            {
                this.AxisSpace += step;
                this.ResetFirstRecord(false);
            }
        }

        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="step"></param>
        private void ZoomOut(int step)
        {
            if (this.AxisSpace > 5)
            {
                this.AxisSpace -= step;
                this.ResetFirstRecord(false);
            }
        }


        #endregion
    }
}
