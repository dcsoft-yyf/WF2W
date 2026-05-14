using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows.Forms;
using DCSoft.Common;
using DCSoft.Drawing;
using DCSoft.WinForms.Native;
using System.Reflection;
using DCSoft.Printing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 体温单视图控件
    /// </summary>
    [System.ComponentModel.ToolboxItem( false )]
    [System.Runtime.InteropServices.ComVisible( false )]
     
    [DCSoft.Common.DCPublishAPI]
    internal class TemperatureViewControl : System.Windows.Forms.UserControl 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TemperatureViewControl()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
        }
         

#region 操作光标的代码群 ****************************


        private bool _CaretCreated = false; // 光标已经创建标志
        /// <summary>
        /// 光标已经创建标志
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        private bool CaretCreated
        {
            get
            {
                return _CaretCreated;
            }
        }

        //private DateTime _CaretDateTime = TemperatureDocument.NullDate;
        ///// <summary>
        ///// 光标所在的时间值
        ///// </summary>
        //[Browsable( false )]
        //[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        //public DateTime CaretDateTime
        //{
        //    get
        //    {
        //        return _CaretDateTime; 
        //    }
        //    set
        //    {
        //        _CaretDateTime = value;
        //        if (this.IsHandleCreated)
        //        {
        //            this.UpdateCaret();
        //        }
        //    }
        //}
        /// <summary>
        /// 光标控制对象
        /// </summary>
        private Win32Caret _Caret = null;
        /// <summary>
        /// 光标控制对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        private Win32Caret Caret
        {
            get
            {
                if (_Caret == null)
                {
                    _Caret = new Win32Caret(this);
                }
                return _Caret;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            UpdateCaret();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
        }
        
        /// <summary>
        /// 更新光标位置
        /// </summary>
        public void UpdateCaret()
        {
            if (this.IsHandleCreated)
            {
                if (TemperatureDocument.IsNullDate(this.Document.CaretDateTime) == false)
                {
                    if (this.Document.DataGridBounds.Height > 0)
                    {
                        if (this.Document.CaretDateTime >= this.Document.RuntimeTicks.StartTime
                            && this.Document.CaretDateTime <= this.Document.RuntimeTicks.EndTime)
                        {
                            this._CaretCreated = this.Caret.Create(
                                0,
                                5,
                                (int)this.Document.DocumentUnitToPixel(
                                this.Document.DataGridBounds.Height));
                            float x = this.Document.RuntimeTicks.GetXPosition(
                                this.Document.DataGridBounds,
                                this.Document.CaretDateTime);
                            float y = this.Document.DataGridBounds.Top;
                            Point p = this.ViewToClient(x, y);
                            this.Caret.SetPos(p.X , p.Y );
                            this.Caret.Show();
                        }
                        else
                        {
                            // 当前光标时间超出刻度时间范围
                            this.Caret.Hide();
                        }
                    }
                }
                else
                {
                    if (this.CaretCreated)
                    {
                        this.Caret.Hide();
                    }
                }
            }
        }

#endregion

        /// <summary>
        /// 控件所属的时间轴控件对象
        /// </summary>
        private TemperatureControl OwnerControl
        {
            get
            {
                return this.Parent as TemperatureControl ;
            }
        }

        private DocumentBehaviorMode _BehaviorMode = DocumentBehaviorMode.Normal;
        /// <summary>
        /// 文档行为模式
        /// </summary>
        internal DocumentBehaviorMode BehaviorMode
        {
            get
            {
                return _BehaviorMode;
            }
            set
            {
                _BehaviorMode = value;
            }
        }

       


        private bool _AllowMouseDragScroll = true;
        /// <summary>
        /// 允许鼠标拖拽滚动
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool AllowMouseDragScroll
        {
            get
            {
                return _AllowMouseDragScroll;
            }
            set
            {
                _AllowMouseDragScroll = value;
            }
        }


        private bool _ShowCrossLine = false ;
        /// <summary>
        /// 是否显示交叉十字线
        /// </summary>
        [DefaultValue( false  )]
        [Category("Behavior")]
        public bool ShowCrossLine
        {
            get
            {
                return _ShowCrossLine; 
            }
            set
            {
                if (_ShowCrossLine != value)
                {
                    _ShowCrossLine = value;
                    if (this.IsHandleCreated)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        

        private DocumentViewMode _ViewMode = DocumentViewMode.Page;
        /// <summary>
        /// 文档视图模式
        /// </summary>
        [DefaultValue( DocumentViewMode.Page )]
        [Category("Layout")]
        public DocumentViewMode ViewMode
        {
            get
            {
                return _ViewMode; 
            }
            set
            {
                if (_ViewMode != value)
                {
                    DrawCrossLine();
                    _CrossLinePoint = Point.Empty;
                    _ViewMode = value;
                    if (this.Document != null)
                    {
                        this.Document.ViewMode = value;
                        this.Document.LayoutInvalidate = true;
                    }
                    //if (this.IsHandleCreated)
                    //{
                    //    RefreshViewWithoutRefreshDataSource();
                    //}
                }
            }
        }
         
        private int _CurrentPageIndex = 0;
        /// <summary>
        /// 从0开始计算的当前页号
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public int CurrentPageIndex
        {
            get
            {
                return _CurrentPageIndex; 
            }
            set
            {
                if (_CurrentPageIndex != value)
                {
                    _CurrentPageIndex = value;
                    this.Document.LayoutInvalidate = true;
                }
            }
        }

        private TemperatureDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TemperatureDocument Document
        {
            get
            {
                if (_Document == null)
                {
                    _Document = new TemperatureDocument();
                }
                _Document.InnerBehaviorMode = this.BehaviorMode;
                return _Document;
            }
            set
            {
                _Document = value;
                if (_Document != null)
                {
                    _Document.LayoutInvalidate = true;
                }
                //if (this.IsHandleCreated) 
                //{
                //    UpdateViewSize();
                //}
            }
        }


       

        /// <summary>
        /// 创建运行时使用的页面设置对象
        /// </summary>
        /// <returns></returns>
        internal XPageSettings CreateRuntimePageSettings()
        {
            //PageSettings ps = new PageSettings();
            XPageSettings ps = new XPageSettings();
            if (this.Document != null && this.Document.Config.PageSettings != null)
            {
                //this.Document.Config.PageSettings.WriteTo(ps);
                ps.PaperSize = new PaperSize(
                    this.Document.Config.PageSettings.PaperSizeName,
                    this.Document.Config.PageSettings.PaperWidth,
                    this.Document.Config.PageSettings.PaperHeight);
                ps.Landscape = this.Document.Config.PageSettings.Landscape;
                ps.Margins = new Margins(
                    this.Document.Config.PageSettings.LeftMargin,
                    this.Document.Config.PageSettings.RightMargin,
                    this.Document.Config.PageSettings.TopMargin,
                    this.Document.Config.PageSettings.BottomMargin);
            }
            return ps;
        }

        

        /// <summary>
        /// 运行时使用的页面背景色
        /// </summary>
        internal Color RuntimePageBackColor
        {
            get
            {
                if (this.Document != null)
                {
                    Color c = this.Document.Config.PageBackColor;
                    if (c.A != 0)
                    {
                        return c;
                    }
                }
                return Color.White;
            }
        }
        /// <summary>
        /// 数据点点击事件
        /// </summary>
        public event ValuePointClickEventHandler EventValuePointClick = null;

        /// <summary>
        /// 控件加载时的处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.IsDesignMode == false)
            {
                this.UpdateState();
                this.UpdateViewSize();
            }
        }

        /// <summary>
        /// 改变控件大小时的处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode == false)
            {
                this.Invalidate();
                UpdateViewSize();
            }
        }


        /// <summary>
        /// 更新文档视图
        /// </summary>
        public void RefreshView()
        {
            try
            {
                //this.Refresh();
                this.DrawWaittingMessage();
                //float tick = CountDown.GetTickCountFloat();
                this.Document.RefreshDataSource();
                UpdateState();
                //tick = CountDown.GetTickCountFloat() - tick;
                //tick = CountDown.GetTickCountFloat();
                UpdateViewSize();
                //tick = CountDown.GetTickCountFloat() - tick;
                //DrawCrossLine();
                _CrossLinePoint = Point.Empty;
                this.Invalidate();
                //this.EditMan.Cancel();
            }
            finally
            {
            }
        }

        /// <summary>
        /// 不刷新数据源的更新文档视图
        /// </summary>
        public void RefreshViewWithoutRefreshDataSource()
        {
           // float tick = CountDown.GetTickCountFloat();
            this.DrawWaittingMessage();
            UpdateState();
            //tick = CountDown.GetTickCountFloat() - tick;
           // tick = CountDown.GetTickCountFloat();
            UpdateViewSize();
            //tick = CountDown.GetTickCountFloat() - tick;
            //DrawCrossLine();
            _CrossLinePoint = Point.Empty;
            this.Invalidate();
            //this.EditMan.Cancel();
        }

        /// <summary>
        /// 更新控件状态
        /// </summary>
        public void UpdateState()
        {
            if (this.Document != null)
            {
                //this.Document.NumOfDaysInOnePage = 7;
                DateTime max = TemperatureDocument.NullDate;
                DateTime min = TemperatureDocument.NullDate;
                this.Document.ViewMode = this.ViewMode;
                //this.Document.UpdateDOMState();
                this.Document.UpdateNumOfPage(out max, out min);
                //if ( this.ViewMode == DocumentViewMode.Widely )
                //{
                //    this.Document.NumOfDaysInOnePage = (int)max.Subtract(min).TotalDays;
                //}
            }
        }

        private bool _AutoHeightWhenWidelyViewMode = true ;
        /// <summary>
        /// 当处于大宽度模式时是否为自动高度
        /// </summary>
        [DefaultValue( true )]
        [Category("Layout")]
        public bool AutoHeightWhenWidelyViewMode
        {
            get
            {
                return _AutoHeightWhenWidelyViewMode; 
            }
            set
            {
                if (this._AutoHeightWhenWidelyViewMode != value)
                {
                    _AutoHeightWhenWidelyViewMode = value;
                    if (this.IsHandleCreated)
                    {
                        this.RefreshViewWithoutRefreshDataSource();
                    }
                }
            }
        }

        private void AfterDocumentExecuteLayout(object sender, EventArgs args)
        {
            this.UpdateCaret();
        }

        private RectangleF _PageViewBounds = RectangleF.Empty;
        //private PointF _NativeSourcePosition = new PointF(0, 0);
        internal void UpdateViewSize()
        {
            if (this.IsHandleCreated
                && this.Document != null
                && this.IsDesignMode == false)
            {
                this.Document.HandlerAfterExecuteLayout = new EventHandler(this.AfterDocumentExecuteLayout ); 
                if (this.ViewMode == DocumentViewMode.Page)
                {
                    // 页面视图模式
                    this.ViewTransform = new SimpleRectangleTransform();
                    // 计算页面框架视图边界
                    _PageViewBounds = this.Document.LayoutForPageView(this.CreateRuntimePageSettings());
 
                    this.ViewTransform.DescRectF = _PageViewBounds;
                    //this.ViewTransform.DescRectF = new RectangleF(
                    //    this.Document.Left, 
                    //    this.Document.Top, 
                    //    this.Document.Width, 
                    //    this.Document.Height);

                    RectangleF clientBounds = GraphicsUnitConvert.Convert(
                        _PageViewBounds,
                        this.Document.GraphicsUnit,
                        GraphicsUnit.Pixel);
                    clientBounds.X = 10;
                    clientBounds.Y = 10;

                    if (clientBounds.Width + 20 < this.ClientSize.Width)
                    {
                        clientBounds.X = 10 + (this.ClientSize.Width - clientBounds.Width) / 2;
                    }
                    this.ViewTransform.SourceRectF = clientBounds;
                    //this.ViewTransform.OffsetSourceRectF(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
                    //this._NativeSourcePosition = this.ViewTransform.SourceRectF.Location;
                    Size viewSize = new Size(
                        (int)clientBounds.Right + 10,
                        (int)clientBounds.Bottom + 10);
                    using (Graphics g = this.CreateGraphics())
                    {
                        this.Document.ExecuteLayout(new DCGraphicsForTimeLine(g));
                    }
                    if (this.AutoScrollMinSize != viewSize)
                    {
                        this.AutoScrollMinSize = viewSize;
                    }
                }
                else if (this.ViewMode == DocumentViewMode.Normal)
                {
                    // 普通视图模式
                    this.AutoScrollMinSize = new System.Drawing.Size(1, 1);
                    this.Document.Left = 0;
                    this.Document.Top = 0;
                    this.Document.Width = GraphicsUnitConvert.Convert( 
                        this.ClientSize.Width , 
                        GraphicsUnit.Pixel , 
                        this.Document.GraphicsUnit );
                    this.Document.Height = GraphicsUnitConvert.Convert( 
                        this.ClientSize.Height , 
                        GraphicsUnit.Pixel , 
                        this.Document.GraphicsUnit );
                    this.ViewTransform = new SimpleRectangleTransform();
                    this.ViewTransform.DescRectF = new RectangleF(
                        this.Document.Left, 
                        this.Document.Top, 
                        this.Document.Width, 
                        this.Document.Height);
                    this.ViewTransform.SourceRectF = new RectangleF(
                        0, 
                        0, 
                        this.ClientSize.Width, 
                        this.ClientSize.Height);
                    //this._NativeSourcePosition = this.ViewTransform.SourceRectF.Location;
                    using (Graphics g = this.CreateGraphics())
                    {
                        this.Document.ExecuteLayout(new DCGraphicsForTimeLine(g));
                    }
                }
                else if (this.ViewMode == DocumentViewMode.Timeline)
                {
                    // 时间轴视图模式
                    SizeF size = SizeF.Empty;
                    using (Graphics g = this.CreateGraphics())
                    {
                        RectangleF newDocumentBounds = RectangleF.Empty;
                        this.DrawWaittingMessage();
                        g.PageUnit = this.Document.GraphicsUnit;
                        size = this.Document.GetPreferedSizeForTimeLineViewMode(g);
                        newDocumentBounds.X = 0;
                        newDocumentBounds.Y = 0;
                        newDocumentBounds.Width = (int)size.Width;
                        newDocumentBounds.Height = (int)size.Height;
                        SizeF viewSize = GraphicsUnitConvert.Convert(
                            newDocumentBounds.Size, 
                            this.Document.GraphicsUnit, 
                            GraphicsUnit.Pixel);
                        if (this.AutoHeightWhenWidelyViewMode)
                        {
                            newDocumentBounds.X = 0;
                            newDocumentBounds.Y = 0;
                            newDocumentBounds.Height = GraphicsUnitConvert.Convert(
                                this.ClientSize.Height - 1 ,
                                GraphicsUnit.Pixel , 
                                this.Document.GraphicsUnit );
                            viewSize.Height = this.ClientSize.Height;
                        }
                        this.Document.Bounds = newDocumentBounds;
                        this.ViewTransform = new SimpleRectangleTransform();
                        this.ViewTransform.DescRectF = newDocumentBounds;
                        this.ViewTransform.SourceRectF = new RectangleF(0, 0, viewSize.Width, viewSize.Height);
                        //this._NativeSourcePosition = this.ViewTransform.SourceRectF.Location;
                        if (this.AutoScrollMinSize != viewSize)
                        {
                            //this.AutoScrollMinSize = new Size(1, 1);
                            this.AutoScrollMinSize = new Size((int)viewSize.Width, 1);
                        }
                        // 执行文档内容排版
                        //this.Document.ExecuteLayout(new DCGraphics(g));
                    }
                }
                UpdateTransformSourceRectForAutoScrollPosition();
                this.UpdateCaret();
            }
        }

        private void UpdateTransformSourceRectForAutoScrollPosition()
        {
            Point p = this.AutoScrollPosition;
            //var rect = this.GetType().GetField("displayRect", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            this.ViewTransform.SourceRectOffset = new PointF(p.X, p.Y);

            //RectangleF rect = this.ViewTransform.SourceRectF;
            //rect.X = this._NativeSourcePosition.X + this.AutoScrollPosition.X;
            //rect.Y = this._NativeSourcePosition.Y + this.AutoScrollPosition.Y;
            //this.ViewTransform.SourceRectF = rect;
        }

        /// <summary>
        /// 将视图坐标转换为控件客户区中的坐标
        /// </summary>
        /// <param name="x">X坐标值</param>
        /// <param name="y">Y坐标值</param>
        /// <returns>转换后的坐标</returns>
        internal Point ViewToClient(float x, float y)
        {
            PointF p = this.ViewTransform.UnTransformPointF( x, y );
            //p.X = p.X + this.AutoScrollPosition.X;
            //p.Y = p.Y + this.AutoScrollPosition.Y;
            return new Point((int)p.X, (int)p.Y);
        }

        /// <summary>
        /// 将控件客户区中的坐标转换为文档视图坐标
        /// </summary>
        /// <param name="x">X坐标值</param>
        /// <param name="y">Y坐标值</param>
        /// <returns>转换后的坐标</returns>
        internal PointF ClientToView(int x, int y)
        {
            PointF p = this.ViewTransform.TransformPointF(
                x ,
                y );
            return p;
        }

        /// <summary>
        /// 绘制等待信息
        /// </summary>
        private void DrawWaittingMessage()
        {
            //int dataCount = 0;
            //if (this.Document != null)
            //{
            //    foreach (DocumentData data in this.Document.Datas)
            //    {
            //        dataCount += data.Values.Count;
            //    }
            //}
            //if (dataCount > 100000)
            //{
            //    // 数据点很多
            //    WaittingUIManager.SetMessage(this, DCTimeLineStrings.Watting );
            //}
        }

        private bool IsDesignMode
        {
            get
            {
                if (this.DesignMode)
                {
                    return true;
                }
                Control ctl = this.Parent ;
                while (ctl != null)
                {
                    if (ctl.Site != null && ctl.Site.DesignMode)
                    {
                        return true;
                    }
                    ctl = ctl.Parent;
                }
                return false;
            }
        }

       

        private DocumentViewParty _ViewParty = DocumentViewParty.Both;
        /// <summary>
        /// 文档显示部分
        /// </summary>
        [DefaultValue( DocumentViewParty.Both )]
        internal DocumentViewParty ViewParty
        {
            get
            {
                return _ViewParty; 
            }
            set
            {
                _ViewParty = value; 
            }
        }

        private SimpleRectangleTransform _ViewTransform = null;
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        internal SimpleRectangleTransform ViewTransform
        {
            get
            {
                if (_ViewTransform == null)
                {
                    _ViewTransform = new SimpleRectangleTransform();
                }
                return _ViewTransform; 
            }
            set
            {
                _ViewTransform = value; 
            }
        }


        //private string _WaittingMessage = null;
        ///// <summary>
        ///// 表示等待的提示文本
        ///// </summary>
        //internal string WaittingMessage
        //{
        //    get { return _WaittingMessage; }
        //    set { _WaittingMessage = value; }
        //}

        /// <summary>
        /// 绘制控件内容
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            
            if (this.IsDesignMode)
            {
                // 处于设计模式
                string txt = this.GetType().Name + ":" + this.Name
                    + Environment.NewLine + "Version:" + this.ProductVersion
                    + Environment.NewLine + _AboutText ;
                using (var format = new DCStringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(
                        txt,
                        this.Font,
                        DCBrushes.Black,
                        new Rectangle(
                            0, 
                            0, 
                            this.ClientSize.Width,
                            this.ClientSize.Height),
                        format.Value());
                }
                return;
            }
            InnerOnPaint(e);

            if (this.OwnerControl != null)
            {
                this.OwnerControl.OnAfterRefreshView(e);
            }
        }

        private void InnerOnPaint( PaintEventArgs e )
        {
            //if (DCSoft.Common.StackTraceHelper.IsOverrideOnPaint(this.GetType()))
            //{
            //    // 控件的OnPaint()函数被重载了，不再绘制
            //    return;
            //}
             
            try
            {
                this.DrawCrossLine();
                if (this.Document != null)
                {
                    this.Document.PrintingMode = false;
                    //RectangleF rect = this.ViewTransform.SourceRectF;
                    //rect.X = this.AutoScrollPosition.X ;
                    //rect.Y = this.AutoScrollPosition.Y;
                    //this.ViewTransform.SourceRectF = rect;
                    UpdateTransformSourceRectForAutoScrollPosition();

                    this.Document.InnerBehaviorMode = this.BehaviorMode;
                    RectangleF clipRectangle = (RectangleF)e.ClipRectangle;
                    clipRectangle = this.ViewTransform.TransformRectangleF(clipRectangle);
                    e.Graphics.PageUnit = this.Document.GraphicsUnit;
                    PointF lp = this.ClientToView(0, 0);
                    e.Graphics.TranslateTransform(-lp.X, -lp.Y);
                    this.Document.ViewMode = this.ViewMode;
                    if (this.ViewMode == DocumentViewMode.Page)
                    {
                        // 页面视图模式
                        // 绘制页面边框
                        clipRectangle.Inflate(1, 1);
                        if (this.Document.Config.BackColor.A != 0)
                        {
                            // 填充背景色
                            e.Graphics.FillRectangle(
                                GraphicsObjectBuffer.GetSolidBrush(this.Document.Config.BackColor),
                                clipRectangle);
                        }
                        RectangleF pb2 = RectangleF.Intersect(this._PageViewBounds, clipRectangle);
                        if (pb2.IsEmpty)
                        {
                            return;
                        }
                        //float tick2 = DCSoft.Common.CountDown.GetTickCountFloat();
                        // 填充页面背景色
                        e.Graphics.FillRectangle(
                            GraphicsObjectBuffer.GetSolidBrush(this.RuntimePageBackColor),
                            pb2);

                        e.Graphics.DrawRectangle(
                            Pens.Black,
                            _PageViewBounds.Left,
                            _PageViewBounds.Top,
                            _PageViewBounds.Width,
                            _PageViewBounds.Height);

                        //tick2 = DCSoft.Common.CountDown.GetTickCountFloat() - tick2;
                        //tick2 = DCSoft.Common.CountDown.GetTickCountFloat() - tick2;
                        // 绘制文档
                        if (clipRectangle.IntersectsWith(_Document.Bounds))
                        {
                            //this.Document.UpdateState();
                            this.Document.PageIndex = this.CurrentPageIndex;
                            //tick2 = DCSoft.Common.CountDown.GetTickCountFloat() - tick2;
                            //float tick = DCSoft.Common.CountDown.GetTickCountFloat();
                            this.Document.Draw2(
                                new DCGraphicsForTimeLine(e.Graphics),
                                clipRectangle,
                                DocumentViewParty.Both);
                            //tick = DCSoft.Common.CountDown.GetTickCountFloat() - tick;
                        }
                        //tick2 = DCSoft.Common.CountDown.GetTickCountFloat() - tick2;
                        //PageTitleInfo info = (PageTitleInfo)TemperatureDocument.CreatePageTitleInfo( this.Document );
                        //if (info != null)
                        //{
                        //    info.Draw(new DCGraphics( e.Graphics), _PageViewBounds, true, e.ClipRectangle);
                        //}
                    }
                    else
                    {
                        if (clipRectangle.IntersectsWith(this.Document.Bounds))
                        {

                            // 填充页面背景色
                            e.Graphics.FillRectangle(
                                GraphicsObjectBuffer.GetSolidBrush(this.RuntimePageBackColor),
                                clipRectangle);

                            //using (SolidBrush b = new SolidBrush(this.PageBackColor))
                            //{
                            //    e.Graphics.FillRectangle(b, clipRectangle);
                            //}
                            //this.Document.UpdateState();
                            this.Document.PageIndex = this.CurrentPageIndex;
                            //float tick = DCSoft.Common.CountDown.GetTickCountFloat();
                            this.Document.Draw2(
                                new DCGraphicsForTimeLine(e.Graphics),
                                clipRectangle,
                                this.ViewParty);
                           // tick = DCSoft.Common.CountDown.GetTickCountFloat() - tick;
                        }
                    }
                }
                this.DrawCrossLine();
            }
            catch (Exception ext)
            {
                Console.WriteLine(ext.ToString());
                using( var format = new DCStringFormat())
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString( 
                        ext.ToString() , 
                        this.Font ,
                        DCBrushes.Red ,
                        new RectangleF( 0 , 0 , this.ClientSize.Width , this.ClientSize.Height ),
                        format.Value());
                }
                //System.Console.WriteLine(ext.ToString());
            }
        }
        /// <summary>
        /// 打印文档
        /// </summary>
        /// <param name="specifyPageIndex">从0开始计算的指定页码</param>
        /// <param name="ps">指定的打印机设置对象</param>
        public void PrintDocument( int specifyPageIndex, PrinterSettings ps)

        {
            PrinterSettings printerSettings = ps;
            if(printerSettings  == null)
            {
                printerSettings = new PrinterSettings();
                //using (PrintDialog dlg = new PrintDialog())
                //{
                //    dlg.AllowCurrentPage = false;
                //    dlg.AllowSelection = false;
                //    dlg.AllowSomePages = false;
                //    //dlg.UseEXDialog = true;
                //    if (dlg.ShowDialog(this) == DialogResult.OK)
                //    {
                //        printerSettings = dlg.PrinterSettings.Clone() as PrinterSettings;
                //    }
                //}
            }
            if(printerSettings != null)
            {
                using (TemperaturePrintDocument pd = new TemperaturePrintDocument(
                            this.Document))
                {
                    if (specifyPageIndex >= 0)
                    {
                        // 指定打印页码
                        pd.SpecifyPageIndex = specifyPageIndex;
                    }
                    pd.PrinterSettings = printerSettings;
                    pd.Print();
                }
            }          
        }

        /// <summary>
        /// 打印文档
        /// </summary>
        /// <param name="specifyPageIndex">从0开始计算的指定页码</param>
        /// <param name="ps">指定的打印机设置对象</param>
        public void PrintDocument(List<int> specifyPageIndex, PrinterSettings ps)
        {
#if !WF2W

            PrinterSettings printerSettings = ps;
            if (printerSettings == null)
            {
                using (PrintDialog dlg = new PrintDialog())
                {
                    dlg.AllowCurrentPage = false;
                    dlg.AllowSelection = false;
                    dlg.AllowSomePages = false;
                    //dlg.UseEXDialog = true;
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        printerSettings = dlg.PrinterSettings.Clone() as PrinterSettings;
                    }
                }
            }
            if (printerSettings != null)
            {
                using (TemperaturePrintDocument pd = new TemperaturePrintDocument(
                            this.Document))
                {
                    if (specifyPageIndex.Count >= 0)
                    {
                        // 指定打印页码
                        pd.SpecifyPageIndexes = specifyPageIndex;
                    }
                    pd.PrinterSettings = printerSettings;
                    pd.Print();
                }
            }
#endif
        }


        internal void LoadDocument(TemperatureDocument doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc");
            }
            this.Document = doc;
            if (this.IsHandleCreated)
            {
                this.UpdateState();
                //this.UpdateViewSize();
                this.Document.LayoutInvalidate = true;
                this.UpdateViewSize();
                this.Invalidate();
                this.EditMan.Cancel();
            }
        }

       

        private int _MouseDblClickEventMinTick = 0;
        /// <summary>
        /// 触发鼠标双击事件的最小机器时刻数
        /// </summary>
        internal int MouseDblClickEventMinTick
        {
            get
            {
                return _MouseDblClickEventMinTick; 
            }
        }

        private EditValuePointManager _EditMan = null;
        /// <summary>
        /// 编辑数据管理器
        /// </summary>
        internal EditValuePointManager EditMan
        {
            get
            {
                if (_EditMan == null)
                {
                    _EditMan = new EditValuePointManager();
                    _EditMan.Control = this.Parent as TemperatureControl;
                    _EditMan.ViewControl = this;
                }
                return _EditMan; 
            }
        }

        /// <summary>
        /// 编辑数据点事件
        /// </summary>
        public event EditValuePointEventHandler EventEditValuePoint = null;
        internal void OnEventEditValuePoint(EditValuePointEventArgs args)
        {
            if (this._Tooltip != null)
            {
                this._Tooltip.SetToolTip(this, null);
            }
            switch (this.Document.Config.EditValuePointMode)
            {
                case EditValuePointEventHandleMode.None :
                    // 不允许编辑数值
                    args.Result = false;
                    break;
                case EditValuePointEventHandleMode.Program :
                    // 用户编程模式，触发事件
                    if (EventEditValuePoint != null)
                    {
                        EventEditValuePoint(this, args);
                    }
                    break;
                case EditValuePointEventHandleMode.Silent :
                    // 静默模式
                    args.Result = true;
                    break;
                case EditValuePointEventHandleMode.OwnedUI:
                    {
                        // 使用内置UI模式
                        switch (args.EditMode)
                        {
                            case EditValuePointMode.Update:
                                {
#if !WF2W
                                    // 处理修改数据点的事件
                                    using (dlgEditSingleValue dlg = new dlgEditSingleValue())
                                    {
                                        dlg.Text = string.Format(DCTimeLineStrings.EditValuePoint_Title, args.SerialTitle);
                                        if (args.YAxisInfo != null)
                                        {
                                            dlg.InputTimePrecision = args.YAxisInfo.InputTimePrecision;
                                        }
                                        else if (args.TitleLineInfo != null)
                                        {
                                            dlg.InputTimePrecision = args.TitleLineInfo.InputTimePrecision;
                                        }
                                        dlg.InputTitle = args.SerialTitle;
                                        dlg.InputTime = args.ValuePoint.Time;
                                        dlg.EnableInputTime = false ;
                                        dlg.InputValue = args.ValuePoint.ValueString;
                                        dlg.Tag = args.ValuePoint;
                                        dlg.EventOKButtonClick += new CancelEventHandler(dlg_EventOKButtonClick);
                                        if (dlg.ShowDialog(this) == DialogResult.OK)
                                        {
                                            float v = 0;
                                            if (float.TryParse(dlg.InputValue, out v))
                                            {
                                                args.ValuePoint.Value = v;
                                            }
                                            args.Result = true;
                                        }
                                        else
                                        {
                                            args.Result = false;
                                        }
                                    }
#endif
                                }
                                break;
                            case EditValuePointMode.Delete:
                                {
                                    // 处理删除数据点的事件
                                    string msg = string.Format(
                                        DCTimeLineStrings.PromptDeleteValuePoint_Title_Time_Value,
                                        args.SerialTitle,
                                        args.ValuePoint.Time ,
                                        args.ValuePoint.RuntimeText);
                                    if (MessageBox.Show(
                                        this,
                                        msg,
                                        DCTimeLineStrings.SystemAlert,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        args.Result = true;
                                    }
                                    else
                                    {
                                        args.Result = false;
                                    }
                                }
                                break;
                            case EditValuePointMode.Insert:
                                {
                                    // 处理新增数据点的事件
#if !WF2W
                                    using (dlgEditSingleValue dlg = new dlgEditSingleValue())
                                    {
                                        dlg.Text = string.Format(DCTimeLineStrings.NewValuePoint_Name, args.SerialTitle);
                                        if (args.YAxisInfo != null)
                                        {
                                            dlg.InputTimePrecision = args.YAxisInfo.InputTimePrecision;
                                        }
                                        else if (args.TitleLineInfo != null)
                                        {
                                            dlg.InputTimePrecision = args.TitleLineInfo.InputTimePrecision;
                                        }
                                        dlg.InputTitle = args.SerialTitle;
                                        dlg.InputTime = args.ValuePoint.Time;
                                        dlg.EnableInputTime = true;
                                        dlg.InputValue = args.ValuePoint.ValueString;
                                        dlg.Tag = args.ValuePoint;
                                        dlg.EventOKButtonClick += new CancelEventHandler(dlg_EventOKButtonClick);
                                        if (dlg.ShowDialog(this) == DialogResult.OK)
                                        {
                                            args.ValuePoint.Time = dlg.InputTime;
                                            float v = 0;
                                            if (float.TryParse(dlg.InputValue, out v))
                                            {
                                                args.ValuePoint.Value = v;
                                            }
                                            args.Result = true;
                                        }
                                        else
                                        {
                                            args.Result = false;
                                        }
                                    }
#endif
                                }
                                break;
                        }
                    }
                    break;
            }
            
        }

        void dlg_EventOKButtonClick(object sender, CancelEventArgs e)
        {
#if !WF2W
            dlgEditSingleValue dlg = (dlgEditSingleValue)sender;
            ValuePoint vp = (ValuePoint)dlg.Tag;
            string msg = null;
            float v = float.NaN;
            if ( vp.Parent is YAxisInfo )
            {
                YAxisInfo ya = (YAxisInfo)vp.Parent;
                v = DCTimeLineUtils.ParseInputValue(
                    dlg.InputValue,
                    ya.MaxValue,
                    ya.MinValue,
                    ref msg,
                    true,
                    ya.AllowOutofRange);
                if (string.IsNullOrEmpty(msg) == false)
                {
                    MessageBox.Show(dlg, msg, dlg.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                dlg.ResultValue = v;
            }
#endif
        }

        /// <summary>
        /// 时间区域收缩事件
        /// </summary>
        public event TimeLineZoneEventHandler EventZoneAfterCollapse = null;
        /// <summary>
        /// 时间区域展开事件
        /// </summary>
        public event TimeLineZoneEventHandler EventZoneAfterExpand = null;
        /// <summary>
        /// 文档选择的内容发生改变事件
        /// </summary>
        public event EventHandler EventSelectionChanged = null;

        private Point _LastPoint = Point.Empty;
        private Point _MouseDownPoint = Point.Empty;
        /// <summary>
        /// 处理鼠标按键按下事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _MouseDownPoint = new Point(e.X, e.Y);
            HideCrossLine();
            if (this.EditMan.HandleMouseDown(this, e))
            {
                return;
            }
            //DrawCrossLine();
            //_CrossLinePoint = Point.Empty;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _LastPoint = new Point(e.X, e.Y);
            }
            
            if (e.Button == System.Windows.Forms.MouseButtons.Left 
                && this.Document.Config.AllowUserCollapseZone 
                && this.Document.Config.Zones != null )
            {
                // 试图进行时间区域缩放
                PointF viewP = this.ClientToView(e.X, e.Y);
                foreach (TimeLineZoneInfo zone in this.Document.Config.Zones)
                {
                    if (zone.ExpandedHandleBounds.Contains( viewP.X , viewP.Y ))
                    {
                        zone.IsExpanded = !zone.IsExpanded;
                        this.UpdateViewSize();
                        this.Invalidate();
                        if (zone.IsExpanded)
                        {
                            if (this.EventZoneAfterExpand != null)
                            {
                                TimeLineZoneEventArgs args2 = new TimeLineZoneEventArgs(this.Document, zone);
                                this.EventZoneAfterExpand(this, args2);
                            }
                        }
                        else
                        {
                            if (this.EventZoneAfterCollapse != null)
                            {
                                TimeLineZoneEventArgs args2 = new TimeLineZoneEventArgs(this.Document, zone);
                                this.EventZoneAfterCollapse(this, args2);
                            }
                        }
                        // 在随后的300毫秒钟不触发鼠标双击事件
                        this._MouseDblClickEventMinTick = Environment.TickCount + 300;
                        return;
                    }
                }
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left
                && this.BehaviorMode == DocumentBehaviorMode.DesignMode )
            {
                PointF viewP = this.ClientToView(e.X, e.Y);

                // 处于设计模式
                object obj = this.Document.GetObject( viewP.X , viewP.Y );
                if (obj == null)
                {
                    obj = this.Document;
                }
                if (obj != this.Document.SelectedObject)
                {
                    this.Document.SelectedObject = obj;
                    this.Invalidate();
                    if (EventSelectedObjectChanged != null)
                    {
                        // 触发当前对象发生改变事件
                        EventSelectedObjectChanged(this, EventArgs.Empty);
                    }
                }
            }
        }



        private Point _CrossLinePoint = Point.Empty;
        /// <summary>
        /// 绘制十字交叉线
        /// </summary>
        private void DrawCrossLine()
        {
#if !DCWriterForWASM
            if (Environment.TickCount - this._LastScrollTick > 100)
            {
                if (this.ShowCrossLine && _CrossLinePoint.IsEmpty == false)
                {
                    SimpleReversibleDrawer.DrawReversibleLine(
                                    this.Handle,
                                    0,
                                    _CrossLinePoint.Y,
                                    this.ClientSize.Width,
                                    _CrossLinePoint.Y);
                    SimpleReversibleDrawer.DrawReversibleLine(
                        this.Handle,
                        _CrossLinePoint.X,
                        0,
                        _CrossLinePoint.X,
                        this.ClientSize.Height);
                }
            }
#endif
        }

        /// <summary>
        /// 隐藏十字交叉线
        /// </summary>
        private void HideCrossLine()
        {
            if (_CrossLinePoint.IsEmpty == false)
            {
                DrawCrossLine();
                _CrossLinePoint = Point.Empty;
            }
        }
        /// <summary>
        /// 处理按键状态
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return false ;
        }

        /// <summary>
        /// 处理按键状态
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            return false ;
        }
        /// <summary>
        /// 处理键盘事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Handled )
            {
                return;
            }
            if (this.EditMan.HandleKeyDown(e))
            {
                return;
            }
            HandleKeyDown(e);
        }

        internal void HandleKeyDown( KeyEventArgs e )
        {
            if (this.ViewMode == DocumentViewMode.Timeline)
            {
                Point p = new Point( - this.AutoScrollPosition.X, - this.AutoScrollPosition.Y);
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                {
                    p.Offset(-(int)this.Document.TickViewWidth, 0);
                    this.HideCrossLine();
                    this.AutoScrollPosition = p;
                }
                else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
                {
                    p.Offset((int) this.Document.TickViewWidth , 0);
                    this.HideCrossLine();
                    this.AutoScrollPosition = p;
                }
                else if (e.KeyCode == Keys.PageUp)
                {
                    int step = (int)(this.ClientSize.Width - this.Document.LeftHeaderPixelWidth - 20);
                    if (step < 40)
                    {
                        step = 40;
                    }
                    p.Offset( - step , 0);
                    this.HideCrossLine();
                    this.AutoScrollPosition = p;
                }
                else if (e.KeyCode == Keys.PageDown)
                {
                    int step = (int)(this.ClientSize.Width - this.Document.LeftHeaderPixelWidth - 20);
                    if (step < 40)
                    {
                        step = 40;
                    }
                    p.Offset(step, 0);
                    this.HideCrossLine();
                    this.AutoScrollPosition = p;
                }
                else if (e.KeyCode == Keys.Home)
                {
                    p = new Point(0, 0);
                    this.HideCrossLine();
                    this.AutoScrollPosition = p;
                }
                else if (e.KeyCode == Keys.End)
                {
                    p = new Point(this.AutoScrollMinSize.Width, 0);
                    this.HideCrossLine();
                    this.AutoScrollPosition = p;
                }
            }
        }

        /// <summary>
        /// 处理鼠标光标离开事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            DrawCrossLine();
            _CrossLinePoint = Point.Empty;
            this.SetMouseHoverValuePoint(null);
        }

        /// <summary>
        /// 处理鼠标滚轮事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            DrawCrossLine();
            _CrossLinePoint = Point.Empty;
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// 处理鼠标点击事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            
        }

        private void SetMouseHoverValuePoint(ValuePoint newPoint )
        {
            if (this.Document.MouseHoverValuePoint != newPoint)
            {
                if (this.Document.MouseHoverValuePoint != null)
                {
                    if (this.Document.Config.LinkVisualStyle == DocumentLinkVisualStyle.Hover)
                    {
                        if (string.IsNullOrEmpty(this.Document.MouseHoverValuePoint.Link) == false)
                        {
                            this.InvalidateValuePoint(this.Document.MouseHoverValuePoint);
                        }
                    }
                }
                this.Document.MouseHoverValuePoint = newPoint;
                if (newPoint != null)
                {
                    if (this.Document.Config.LinkVisualStyle == DocumentLinkVisualStyle.Hover)
                    {
                        if (string.IsNullOrEmpty(newPoint.Link) == false)
                        {
                            this.InvalidateValuePoint(newPoint);
                        }
                    }
                }
            }
        }

        private void InvalidateValuePoint(ValuePoint vp)
        {
            if (vp != null)
            {
                RectangleF bounds = this.ViewTransform.UnTransformRectangleF(vp.ViewBounds);
                this.Invalidate(
                    new Rectangle(
                        (int)bounds.Left - 2, 
                        (int)bounds.Top - 2 , 
                        (int)bounds.Width + 4, 
                        (int)bounds.Height + 4 ));
            }
        }
        /// <summary>
        /// 处理鼠标移动事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.EditMan.HandleMouseMove(this, e))
            {
                return;
            }
            ValuePoint newHoverPoint = null;
            if (e.Button == System.Windows.Forms.MouseButtons.Left && _LastPoint.IsEmpty == false)
            {
                if (this.AllowMouseDragScroll)
                {
                    // 鼠标拖拽操作
                    Point p = this.AutoScrollPosition;
                    p.Offset(e.X - _LastPoint.X, e.Y - _LastPoint.Y);
                    _LastPoint = new Point(e.X, e.Y);
                    this.AutoScrollPosition = new Point(-p.X, -p.Y);
                }
            }
            else
            {
                if (this.ShowCrossLine)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.None && e.Delta == 0)
                    {
                        DrawCrossLine();
                        _CrossLinePoint = new Point(e.X, e.Y);
                        DrawCrossLine();
                    }
                }
                if (this.Document.Config.ShowTooltip)
                {
                    // 显示提示文本
                    if (_Tooltip == null)
                    {
                        _Tooltip = new ToolTip();
                    }
                    if (string.IsNullOrEmpty(this.Document.Config.TitleForToolTip))
                    {
                        _Tooltip.ToolTipTitle = String.Empty;//DCTimeLineStrings.SystemAlert;
                    }
                    else
                    {
                        _Tooltip.ToolTipTitle = this.Document.Config.TitleForToolTip;
                    }
                    string txt = null;
                    PointF viewP = this.ClientToView(e.X, e.Y);
                    TitleLineInfoList allTitleLines = new TitleLineInfoList();
                    allTitleLines.AddRange(this.Document.RuntimeHeaderLines);
                    allTitleLines.AddRange(this.Document.RuntimeFooterLines);
                    
                    if (this.Document.Config.DebugMode)
                    {
                        // 处于调试模式
                        foreach (TitleLineInfo line in allTitleLines)
                        {
                            if (line.ValueType == TitleLineValueType.HourTick )
                            {
                                RectangleF detectBounds = new RectangleF(
                                    this.Document.DataGridBounds.Left,
                                    line.Top,
                                    this.Document.DataGridBounds.Width,
                                    line.Height);
                                if ( detectBounds.Contains( viewP ))
                                {
                                    // 命中一个时刻序列列表
                                    foreach (RuntimeTickInfo tick in this.Document.RuntimeTicks)
                                    {
                                        if (tick.Left <= viewP.X - detectBounds.Left 
                                            && viewP.X - detectBounds.Left  <= tick.Left + tick.Width)
                                        {
                                            // 命中一个时刻
                                            txt = "Index   :" + this.Document.RuntimeTicks.IndexOf(tick)
                                                + Environment.NewLine + "Left    :" + tick.Left
                                                + Environment.NewLine + "Width   :" + tick.Width
                                                + Environment.NewLine + "Right   :" + Convert.ToString(tick.Left + tick.Width)
                                                + Environment.NewLine + "开始时间:" + tick.StartTime.ToString("yyyy-MM-dd HH:mm:ss")
                                                + Environment.NewLine + "结束时间:" + tick.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (txt == null)
                    {
                        foreach (TitleLineInfo line in allTitleLines)
                        {
                            if (line.ShowExpandedHandle)
                            {
                                if (line.ExpandedHandleBounds.Contains(viewP))
                                {
                                    txt = DCTimeLineStrings.PromptExpandedCollapseTitleLine;
                                    break;
                                }
                            }
                        }
                    }
                    if( txt == null )
                    {
                        if (this.Document.Config.AllowUserCollapseZone && this.Document.Config.Zones != null)
                        {
                            foreach (TimeLineZoneInfo zone in this.Document.Config.Zones)
                            {
                                if (zone.ExpandedHandleBounds.IsEmpty == false
                                    && zone.ExpandedHandleBounds.Contains(viewP.X, viewP.Y))
                                {
                                    txt = DCTimeLineStrings.PromptExpandedCollapseZone;
                                    break;
                                }
                            }//foreach
                        }
                    }
                    this.Cursor = Cursors.Arrow;
                    if (txt == null)
                    {
                        ValuePoint vp = GetValuePointByViewPosition(viewP.X, viewP.Y);
                        newHoverPoint = vp;
                        if (vp != null)
                        {
                            // 鼠标在某个数据点上面
                            // 获得提示文本
                            //if (string.IsNullOrEmpty(vp.Title) == false)
                            //{
                            //    txt = vp.Title;
                            //}
                            //else if (string.IsNullOrEmpty(vp.Link) == false)
                            //{
                            //    txt = vp.Link;
                            //}
                            //else
                            {
                                txt = vp.RuntimeTitle;
                            }
                            if (this.Document.Config.DebugMode)
                            {
                                txt = txt + Environment.NewLine + "Bounds:" + vp.ViewBounds.ToString()
                                    + Environment.NewLine + "PIndex:" + vp.InstanceIndex 
                                    + Environment.NewLine + "Time:" + vp.Time.ToString("yyyy-MM-dd HH:mm:ss")
                                    + Environment.NewLine + "Text:" + vp.Text
                                    + Environment.NewLine + "Value:" + vp.Value.ToString();
                            }
                            if( string.IsNullOrEmpty( vp.Link ) == false )
                            {
                                if (this.EditMan == null
                                    || this.EditMan.Enabled == false 
                                    || this.EditMan.CurrentMode == ValuePointEditMode.None )
                                {
                                    // 如果设置了超链接则设置鼠标光标形状
                                    this.Cursor = Cursors.Hand;
                                }
                            }
                        }
                    }
                    if (_Tooltip.GetToolTip(this) != txt)
                    {
                        _Tooltip.SetToolTip(this, txt);
                    }
                    
                }
            }
            SetMouseHoverValuePoint(newHoverPoint);
        }

        internal void ShowToolTip(string msg , bool showAlways )
        {
            if (_Tooltip == null)
            {
                _Tooltip = new ToolTip();
            }
            if (string.IsNullOrEmpty(this.Document.Config.TitleForToolTip))
            {
                _Tooltip.ToolTipTitle = DCTimeLineStrings.SystemAlert;
            }
            else
            {
                _Tooltip.ToolTipTitle = this.Document.Config.TitleForToolTip;
            }
            if (string.IsNullOrEmpty(msg))
            {
                _Tooltip.SetToolTip(this, null);
            }
            else
            {
                if (_Tooltip.GetToolTip(this) != msg)
                {
                    _Tooltip.ShowAlways = showAlways;
                    _Tooltip.SetToolTip(this, msg);
                }
            }
        }

        private long _LastScrollTick = 0;
        /// <summary>
        /// 处理控件视图滚动事件
        /// </summary>
        /// <param name="se"></param>
        protected override void OnScroll(ScrollEventArgs se)
        {
            HideCrossLine();
            this._LastScrollTick = Environment.TickCount;
            base.OnScroll(se);
            //RectangleF rect = this.ViewTransform.SourceRectF;
            //rect.X = this.AutoScrollPosition.X;
            //rect.Y = this.AutoScrollPosition.Y;
            //this.ViewTransform.SourceRectF = rect;
        }

        
        internal ValuePoint GetValuePointByViewPosition(float x, float y)
        {
            lock (this.Document)
            {
                for( int iCount = this.Document.Config.YAxisInfos.Count -1 ; iCount >=0 ; iCount --)
                {
                    YAxisInfo ya = this.Document.Config.YAxisInfos[iCount];
                    if (ya.Visible == false || ya.ValueVisible == false)
                    {
                        continue;
                    }
                    ValuePointList values = this.Document.Datas.GetValuesByName(ya.Name);
                    if (ya.Style == YAxisInfoStyle.Text || ya.Style == YAxisInfoStyle.TextInsideGrid)
                    {
                        foreach (ValuePoint vp in values)
                        {
                            if (vp.ViewBounds.Contains(x, y))
                            {
                                return vp;
                            }
                        }
                    }
                    else if (ya.Style == YAxisInfoStyle.Value)
                    {
                        foreach (ValuePoint vp in values)
                        {
                            // 判断数据点
                            if (vp.ViewBounds.Contains(x, y))
                            {
                                return vp;
                            }
                            if (vp.ShadowPoint != null && vp.ShowShadowPoint)
                            {
                                // 判断影子数据点
                                if (vp.ShadowPoint.ViewBounds.Contains(x, y))
                                {
                                    return vp.ShadowPoint;
                                }
                            }
                        }//foreach
                    }
                }//foreach
                foreach (TitleLineInfo line in this.Document.RuntimeFooterLines)
                {
                    ValuePointList values = this.Document.Datas.GetValuesByName(line.Name);
                    foreach (ValuePoint vp in values)
                    {
                        if (vp.ViewBounds.Contains(x, y))
                        {
                            return vp;
                        }
                    }
                }
                foreach (TitleLineInfo line in this.Document.RuntimeHeaderLines)
                {
                    ValuePointList values = this.Document.Datas.GetValuesByName(line.Name);
                    foreach (ValuePoint vp in values)
                    {
                        if (vp.ViewBounds.Contains(x, y))
                        {
                            return vp;
                        }
                    }
                }
            }//lock
            return null;
        }


        private ToolTip _Tooltip = null;

        /// <summary>
        /// 被选择的对象发生改变事件
        /// </summary>
        public event EventHandler EventSelectedObjectChanged = null;

        /// <summary>
        /// 处理鼠标按键松开事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.EditMan.HandleMouseUp(this, e))
            {
                return;
            }
            _LastPoint = Point.Empty;
            if (Math.Abs(e.X - _MouseDownPoint.X) < System.Windows.Forms.SystemInformation.DragSize.Width
                && Math.Abs(e.Y - _MouseDownPoint.Y) < System.Windows.Forms.SystemInformation.DragSize.Height)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    PointF viewP = this.ClientToView(e.X, e.Y);
                    if (this.BehaviorMode != DocumentBehaviorMode.DesignMode)
                    {
                        if (this.HandleMouseClickForLeftHeader(e.X, e.Y))
                        {
                            return;
                        }
                       
                    }
                    if (this.Document.InnerBehaviorMode != DocumentBehaviorMode.DesignMode)
                    {
                        // 不处于设计模式
                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            ValuePoint vp = GetValuePointByViewPosition(viewP.X, viewP.Y);
                            if (vp != null)
                            {
                                if (EventValuePointClick != null)
                                {
                                    // 触发事件
                                    ValuePointClickEventArgs args = new ValuePointClickEventArgs(vp);
                                    EventValuePointClick(this, args);
                                }
                            }
                            if (vp != null && string.IsNullOrEmpty(vp.Link) == false)
                            {
                                // 触发超链接事件
                                DocumentLinkClickEventArgs args2 = new DocumentLinkClickEventArgs(
                                    this.OwnerControl, 
                                    this.Document, 
                                    vp, 
                                    vp.Link, 
                                    vp.LinkTarget);
                                    this.OwnerControl.OnEventLinkClick(args2);
                                return;
                            }
                        }
                    }
                    if (this.HandleMouseEventForSelection(e))
                    {
                        // 用户选择了数据点
                        return;
                    }
                    

                   
                    if (this.BehaviorMode != DocumentBehaviorMode.DesignMode)
                    {
                        if (EventHeaderLabelClick != null)
                        {
                            object obj = this.Document.GetObject(viewP.X, viewP.Y);
                            if (obj is HeaderLabelInfo)
                            {
                                EventHeaderLabelClick(obj, null);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 为设置选择线条状态而处理鼠标事件
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>是否处理了事件而无需后续处理</returns>
        private bool HandleMouseEventForSelection(MouseEventArgs args)
        {
            PointF viewP = this.ClientToView(args.X, args.Y);
            TemperatureDocument document = this.Document;
            if (document.DataGridBounds.Contains(viewP) == false)
            {
                return false;
            }
            YAxisInfo matchInfo = null;
            for (int iCount = this.Document.VisibleYAxisInfos.Count - 1; iCount >= 0; iCount--)
            {
                YAxisInfo info = this.Document.VisibleYAxisInfos[iCount];
                if (info.Style != YAxisInfoStyle.Value || info.ValueVisible == false )
                {
                    continue;
                }
                ValuePointList vps = document.GetValuePointsByName(info.Name);
                if (vps != null && vps.Count > 0)
                {
                    DateTime dtm = this.Document.RuntimeTicks.GetDateTimeByXPosition(document.DataGridBounds, viewP.X);
                    if (TemperatureDocument.IsNullDate(dtm) == false)
                    {
                        // 首先缩小查找范围
                        int index = vps.GetFloorIndexByTime(dtm);
                        int startIndex = Math.Max(0, index - 10);
                        int endIndex = Math.Max( vps.Count -1 , index + 10);
                        PointF lastPointF = PointF.Empty;
                        for (int vpIndex = startIndex; vpIndex < vps.Count; vpIndex++)
                        {
                            ValuePoint vp = vps[vpIndex];
                            RectangleF vpBounds = vp.ViewBounds;
                            if (vpBounds.IsEmpty)
                            {
                                // 遇到空数据点
                                lastPointF = PointF.Empty;
                                continue;
                            }
                            if (vpBounds.Contains(viewP))
                            {
                                // 直接命中数据点
                                matchInfo = info;
                                break;
                            }
                            PointF p = new PointF(vpBounds.Left + vpBounds.Width / 2, vpBounds.Top + vpBounds.Height / 2);
                            if (lastPointF.IsEmpty == false)
                            {
                                // 判断点是否命中两个点的连线
                                double dis = MathCommon.DistanceToLine( 
                                    p.X  ,
                                    p.Y , 
                                    lastPointF.X , 
                                    lastPointF.Y , 
                                    viewP.X ,
                                    viewP.Y , 
                                    true );
                                if (dis >= 0 && dis <= vpBounds.Height / 2)
                                {
                                    // 如果点和两个点的线段非常近，则认为是命中了
                                    matchInfo = info;
                                    break;
                                }
                            }
                            lastPointF = p;
                        }
                    }
                }
                if (matchInfo != null)
                {
                    break;
                }
            }//for
            if (matchInfo == null)
            {
                return false;
            }
            else
            {
                if (document.Config.SelectionMode == DCTimeLineSelectionMode.MultiSelec)
                {
                    // 多选模式
                    matchInfo.Selected = !matchInfo.Selected;
                }
                else if( document.Config.SelectionMode == DCTimeLineSelectionMode.SingleSelect )
                {
                    // 单选模式
                    matchInfo.Selected = !matchInfo.Selected;
                    foreach (YAxisInfo info in document.Config.YAxisInfos)
                    {
                        if (info != matchInfo)
                        {
                            info.Selected = false;
                        }
                    }
                }
                this.Invalidate();
                if (this.EventSelectionChanged != null)
                {
                    this.EventSelectionChanged(this, null);
                }
                return true;
            }
        }

        internal bool HandleMouseClickForLeftHeader(int x, int y)
        {
            PointF viewP = this.ClientToView(x, y);
             
            // 试图进行标题数据行缩放
            TitleLineInfoList list = new TitleLineInfoList();
            list.AddRange(this.Document.RuntimeHeaderLines);
            list.AddRange(this.Document.RuntimeFooterLines);
            foreach (TitleLineInfo line in list)
            {
                if ( line.ShowExpandedHandle)
                {
                    if (line.ExpandedHandleBounds.Contains(viewP.X, viewP.Y))
                    {
                        line.IsExpanded = !line.IsExpanded;
                        this.Document.LayoutInvalidate = true;
                        this.UpdateViewSize();
                        this.Invalidate();
                        // 在随后的300毫秒钟不触发鼠标双击事件
                        this._MouseDblClickEventMinTick = Environment.TickCount + 300;
                        return true ;
                    }
                }
            }
             
            foreach (YAxisInfo info in this.Document.YAxisInfos)
            {
                if (info.RuntimeTitleVisible && info.ClickToHide)
                {
                    RectangleF rect = new RectangleF(info.TitleLeft,
                        info.TitleTop,
                        info.TitleWidth,
                        info.TitleHeight);
                    if (rect.Contains( viewP.X , viewP.Y))
                    {
                        info.ValueVisible = !info.ValueVisible;
                        //info.Selected = !info.Selected;//撤销修改，让客户自己设置完selected
                        this.Document.UpdateVisibleYAxisInfos();
                        this.Invalidate();
                        return true;
                    }
                }//if
            }//foreach
            if (EventHeaderLabelClick != null)
            {
                object obj = this.Document.GetObject( viewP.X , viewP.Y );
                if (obj is HeaderLabelInfo)
                {
                    EventHeaderLabelClick(obj, null);
                }
            }
            return false;
        }

        internal event EventHandler EventHeaderLabelClick = null;

        /// <summary>
        /// 销毁控件
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (_Tooltip != null)
            {
                _Tooltip.Dispose();
                _Tooltip = null;
            }

            base.Dispose(disposing);
        }
        /// <summary>
        /// 显示关于本控件的对话框
        /// </summary>
        public void AboutControl()
        {
            MessageBox.Show(this, _AboutText ,"关于...");
        }

        private string _AboutText = "都昌时间轴控件(DCTimeline)，是南京都昌信息科技有限公司开发的医疗行业使用的时间轴控件，适用于.NET平台。南京都昌信息科技有限公司专业研发医学数据可视化技术，并自主研发了电子病历编辑器软件、时间轴控件等等，公司网址 www.dcwriter.cn。";
    }
}
