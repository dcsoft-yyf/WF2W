 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO ;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using DCSoft.Common;
using DCSoft.Drawing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 体温单/时间轴控件
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.ComponentModel.ToolboxItem( true  )]
    [System.Drawing.ToolboxBitmap( typeof(TemperatureControl)) ]
     
    [System.Runtime.InteropServices.ComVisible(false)]
    [DCSoft.Common.DCPublishAPI]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public partial class TemperatureControl : UserControl
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TemperatureControl()
        {
            //DCSoft.Writer.DCWriterPublish.Start();
            InitializeComponent();
            //DCSoft.Writer.DCWriterPublish.Start();
#if MWGA
            this.picLeftHeader.MWGASetUserPaint(true);
#endif
            this.pnlView.MouseMove += new MouseEventHandler(pnlView_MouseMove);

            this.pnlView.EventValuePointClick += new ValuePointClickEventHandler(pnlView_EventValuePointClick);
            this.pnlView.EventHeaderLabelClick += new EventHandler(pnlView_EventHeaderLabelClick);
            this.pnlView.MouseDoubleClick += new MouseEventHandler(pnlView_MouseDoubleClick);
            this.pnlView.MouseClick += new MouseEventHandler(pnlView_MouseClick);
            this.pnlView.EventZoneAfterCollapse += new TimeLineZoneEventHandler(pnlView_EventZoneAfterCollapse);
            this.pnlView.EventZoneAfterExpand += new TimeLineZoneEventHandler(pnlView_EventZoneAfterExpand);
            this.pnlView.EventSelectionChanged += new EventHandler(pnlView_EventSelectionChanged);
            this.pnlView.EventEditValuePoint += new EditValuePointEventHandler(pnlView_EventEditValuePoint);
            this.btnEditData.DropDownOpening += new EventHandler(btnEditData_DropDownOpening);
            this.btnDesigner.Visible = false;
            //this.pnlView.BackColor = Color.Red;
        }

        

        /// <summary>
        /// 是否显示设计器按钮
        /// </summary>
        [DefaultValue( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        [DCSoft.Common.DCPublishAPI]
        public bool DesignerButtonVisible
        {
            get
            {
                return this.btnDesigner.Visible;
            }
            set
            {
                this.btnDesigner.Visible = value;
            }
        }

        void pnlView_EventEditValuePoint(object eventSender, EditValuePointEventArgs args)
        {
            this.OnEventEditValuePoint(args);
        }

        void pnlView_EventSelectionChanged(object sender, EventArgs e)
        {
            this.picLeftHeader.Invalidate();
        }

        void pnlView_EventZoneAfterExpand(object eventSender, TimeLineZoneEventArgs args)
        {
            this.UpdateLeftHeader(false);
            this.UpdateLeftHeaderSize();
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, args, DCTimeLineControlEventMessageType.EventZoneAfterExpand));
            }
            else
            {
                if (this.EventZoneAfterExpand != null)
                {
                    this.EventZoneAfterExpand(this, args);
                }
            }
        }

        void pnlView_EventZoneAfterCollapse(object eventSender, TimeLineZoneEventArgs args)
        {
            this.UpdateLeftHeader(false);
            this.UpdateLeftHeaderSize();
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, args, DCTimeLineControlEventMessageType.EventZoneAfterCollapse));
            }
            else
            {
                if (this.EventZoneAfterCollapse != null)
                {
                    this.EventZoneAfterCollapse(this, args);
                }
            }
        }

        /// <summary>
        /// 自定义名称初始化
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static List<string> _StanderNameList = new List<string>();
        /// <summary>
        /// 自定义名称
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public List<string> StanderNameList
        {
            get
            {
                return _StanderNameList;
            }
        }
        /// <summary>
        /// 自定义名称方法
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddStanderNameList(string str)
        {
            if (str.IndexOf(",") > 0)
            {
                string[] strList = str.Split(',');
                for (int i = 0; i < strList.Length; i++)
                {
                    this.StanderNameList.Add(strList[i]);
                }
            }
            else
            {
                this.StanderNameList.Add(str);
            }
        }
        /// <summary>
        /// 自定义标题初始化
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static List<string> _StanderTitleList = new List<string>();
        /// <summary>
        /// 自定义标题
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public List<string> StanderTitleList
        {
            get
            {
                return _StanderTitleList;
            }
        }
        /// <summary>
        /// 自定义标题方法
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddStanderTitleList(string str)
        {
            if (str.IndexOf(",") > 0)
            {
                string[] strList = str.Split(',');
                for (int i = 0; i < strList.Length; i++)
                {
                    this.StanderTitleList.Add(strList[i]);
                }
            }
            else
            {
                this.StanderTitleList.Add(str);
            }
        }

        /// <summary>
        /// 声明整个控件，准备重新绘制整个界面
        /// </summary>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void InvalidateAll()
        {
            this.Invalidate(true);
        }

        /// <summary>
        /// 编辑数据点事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EditValuePointEventHandler EventEditValuePoint = null;

        internal void OnEventEditValuePoint(EditValuePointEventArgs args)
        {
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, args));
                return;
            }
            if (EventEditValuePoint != null)
            {
                EventEditValuePoint(this, args);
            }
        }

        /// <summary>
        /// 超链接点击事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event DocumentLinkClickEventHandler EventLinkClick = null;

        internal void OnEventLinkClick(DocumentLinkClickEventArgs args)
        {
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, args));
                return;
            }
            if (EventLinkClick != null)
            {
                EventLinkClick(this, args);
            }
        }
        /// <summary>
        /// 时间区域收缩事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TimeLineZoneEventHandler EventZoneAfterCollapse = null;
        /// <summary>
        /// 时间区域展开事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TimeLineZoneEventHandler EventZoneAfterExpand = null;


        void pnlView_MouseMove(object sender, MouseEventArgs e)
        {
            //移动鼠标事件处理逻辑，为节省性能尽可能的缩小引发事件的条件
            if (this.EventDocumentMouseMove != null)
            {
                DateTime dtm = DateTime.MinValue;
                float value = float.NaN;
                TitleLineInfo info = null;

                PointF p = this.InnerViewControl.ClientToView(e.X, e.Y);

                //当开关打开后，才会进行复杂计算
                if (this.EnableExtMouseMoveEvent == true &&
                    this.Document != null &&
                    this.Document.RuntimeTicks != null &&
                    this.Document.DataGridBounds != null)
                {
                    dtm = this.Document.RuntimeTicks.GetDateTimeByXPosition(
                            this.Document.DataGridBounds,
                            p.X);

                    if (this.Document.DataGridBounds.Contains(p.X, p.Y))
                    {
                        //如果坐标命中折线区，则遍历折线区的Y轴来计算数据
                        foreach (YAxisInfo yinfo in this.Document.Config.YAxisInfos)
                        {
                            if (yinfo.Selected)
                            {
                                value = yinfo.GetValueByDisplayY(this.Document, this.Document.DataGridBounds, p.Y);
                                break;
                            }
                        }
                    }
                    else if (p.Y <= this.Document.DataGridBounds.Top)
                    {
                        //如果坐标在折线区上方，则遍历页眉数据行判断
                        foreach (TitleLineInfo tinfo in this.Document.RuntimeHeaderLines)
                        {
                            if (tinfo._LineBounds != RectangleF.Empty && tinfo._LineBounds.Contains(p.X, p.Y))
                            {
                                info = tinfo;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //如果坐标在折线区下方，则遍历页脚数据行判断
                        foreach (TitleLineInfo tinfo in this.Document.RuntimeFooterLines)
                        {
                            if (tinfo._LineBounds != RectangleF.Empty && tinfo._LineBounds.Contains(p.X, p.Y))
                            {
                                info = tinfo;
                                break;
                            }
                        }
                    }
                }

                DocumentMouseMoveEventArgs args = new DocumentMouseMoveEventArgs(
                    this.Document,
                    e.Button,
                    dtm,
                    value,
                    e.Location,
                    info);

                this.OnEventDocumentMouseMove(args);
            }           
        }

        void pnlView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (pnlView.MouseDblClickEventMinTick == 0
                || Environment.TickCount > pnlView.MouseDblClickEventMinTick)
            {
                if (this.pnlView.EditMan.CurrentMode == ValuePointEditMode.None
                    && this.EventDocumentDblClick != null)
                {
                    // 触发文档双击事件
                    PointF p = this.InnerViewControl.ClientToView(e.X, e.Y);
                    ValuePoint valuePoint = this.InnerViewControl.GetValuePointByViewPosition(p.X, p.Y);
                    DateTime dtm = DateTime.MinValue;
                    float value = float.NaN;
                    TitleLineInfo info = null;
                    DCTimeLineLabel label = null;

                    if (this.Document != null &&
                        this.Document.RuntimeTicks != null &&
                        this.Document.DataGridBounds != null)
                    {
                        dtm = this.Document.RuntimeTicks.GetDateTimeByXPosition(
                            this.Document.DataGridBounds,
                            p.X);

                        foreach (DCTimeLineLabel lbl in this.DocumentConfig.Labels)
                        {
                            if (lbl._LabelBounds != RectangleF.Empty && lbl._LabelBounds.Contains(p.X, p.Y))
                            {
                                label = lbl;
                                break;
                            }
                        }

                        if (this.Document.DataGridBounds.Contains(p.X, p.Y))
                        {
                            //如果坐标命中折线区，则遍历折线区的Y轴来计算数据
                            foreach (YAxisInfo yinfo in this.Document.Config.YAxisInfos)
                            {
                                if (yinfo.Selected)
                                {
                                    value = yinfo.GetValueByDisplayY(this.Document, this.Document.DataGridBounds, p.Y);
                                    break;
                                }
                            }
                        }
                        else if (p.Y <= this.Document.DataGridBounds.Top)
                        {
                            //如果坐标在折线区上方，则遍历页眉数据行判断
                            foreach (TitleLineInfo tinfo in this.Document.RuntimeHeaderLines)
                            {
                                if (tinfo._LineBounds != RectangleF.Empty && tinfo._LineBounds.Contains(p.X, p.Y))
                                {
                                    info = tinfo;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //如果坐标在折线区下方，则遍历页脚数据行判断
                            foreach (TitleLineInfo tinfo in this.Document.RuntimeFooterLines)
                            {
                                if (tinfo._LineBounds != RectangleF.Empty && tinfo._LineBounds.Contains(p.X, p.Y))
                                {
                                    info = tinfo;
                                    break;
                                }
                            }
                        }
                    }
                    DocumentDblClickEventArgs args = new DocumentDblClickEventArgs(
                        this.Document,
                        e.Button,
                        dtm,
                        value,
                        valuePoint,
                        info,
                        label);
                    this.OnEventDocumentDblClick(args);
                }
            }
        }

        void pnlView_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.pnlView.EditMan.CurrentMode == ValuePointEditMode.None
                && this.EventDocumentClick != null)
            {
                // 触发文档单击事件
                PointF p = this.InnerViewControl.ClientToView(e.X, e.Y);
                ValuePoint valuePoint = this.InnerViewControl.GetValuePointByViewPosition(p.X, p.Y);
                DateTime dtm = DateTime.MinValue;
                float value = float.NaN;
                TitleLineInfo info = null;
                DCTimeLineLabel label = null;

                if (this.Document != null &&
                    this.Document.RuntimeTicks != null &&
                    this.Document.DataGridBounds != null)
                {
                    dtm = this.Document.RuntimeTicks.GetDateTimeByXPosition(
                        this.Document.DataGridBounds,
                        p.X);

                    foreach (DCTimeLineLabel lbl in this.DocumentConfig.Labels)
                    {
                        if (lbl._LabelBounds != RectangleF.Empty && lbl._LabelBounds.Contains(p.X, p.Y))
                        {
                            label = lbl;
                            break;
                        }
                    }

                    if (this.Document.DataGridBounds.Contains(p.X, p.Y))
                    {
                        //如果坐标命中折线区，则遍历折线区的Y轴来计算数据
                        foreach (YAxisInfo yinfo in this.Document.Config.YAxisInfos)
                        {
                            if (yinfo.Selected)
                            {
                                value = yinfo.GetValueByDisplayY(this.Document, this.Document.DataGridBounds, p.Y);
                                break;
                            }
                        }
                    }
                    else if (p.Y <= this.Document.DataGridBounds.Top)
                    {
                        //如果坐标在折线区上方，则遍历页眉数据行判断
                        foreach (TitleLineInfo tinfo in this.Document.RuntimeHeaderLines)
                        {
                            if (tinfo._LineBounds != RectangleF.Empty && tinfo._LineBounds.Contains(p.X, p.Y))
                            {
                                info = tinfo;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //如果坐标在折线区下方，则遍历页脚数据行判断
                        foreach (TitleLineInfo tinfo in this.Document.RuntimeFooterLines)
                        {
                            if (tinfo._LineBounds != RectangleF.Empty && tinfo._LineBounds.Contains(p.X, p.Y))
                            {
                                info = tinfo;
                                break;
                            }
                        }
                    }                   
                }
                DocumentClickEventArgs args = new DocumentClickEventArgs(
                    this.Document,
                    e.Button,
                    dtm,
                    value,
                    valuePoint,
                    info,
                    label);
                this.OnEventDocumentClick(args);
            }
        }

        /// <summary>
        /// 光标所在的时间值
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DateTime CaretDateTime
        {
            get
            {
                return this.pnlView.Document.CaretDateTime;
            }
            set
            {
                this.pnlView.Document.CaretDateTime = value;
            }
        }

        /// <summary>
        /// 滚动视图到最后面的数据点
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ScrollViewToLast()
        {
            Point p = this.pnlView.AutoScrollPosition;
            Point p2  = new Point( ( this.pnlView.AutoScrollMinSize.Width - this.pnlView.ClientSize.Width ) , - p.Y );
            this.pnlView.AutoScrollPosition = p2;
        }

        

        /// <summary>
        /// 内置的时间轴视图控件
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        internal TemperatureViewControl InnerViewControl
        {
            get
            {
                return this.pnlView;
            }
        }

        /// <summary>
        /// 文档行为模式
        /// </summary>
        internal DocumentBehaviorMode BehaviorMode
        {
            get
            {
                return this.DocumentViewControl.BehaviorMode ;
            }
            set
            {
                this.DocumentViewControl.BehaviorMode = value;
            }
        }

        void pnlView_EventHeaderLabelClick(object sender, EventArgs e)
        {
            
        }

        void pnlView_EventValuePointClick(object eventSender, ValuePointClickEventArgs args)
        {
            if (this.BehaviorMode == DocumentBehaviorMode.Normal)
            {
                // 触发数据点点击事件
                this.OnEventValuePointClick(args);
            }
           
        }

        /// <summary>
        /// 内部的时间轴视图控件
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        internal TemperatureViewControl DocumentViewControl
        {
            get
            {
                return this.pnlView;
            }
        }

       

        /// <summary>
        /// 总页数
        /// </summary>
        [Browsable( false )]
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int NumOfPages
        {
            get
            {
                return this.pnlView.Document.NumOfPages;
            }
        }

        /// <summary>
        /// 获取或者设置当前页码
        /// </summary>
        [ComVisible( true )]
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int PageIndex
        {
            get
            {
                return this.pnlView.Document.PageIndex;
            }
            set
            {
                if (value >= 0 && value < cboPageIndex.Items.Count)
                {
                    cboPageIndex.SelectedIndex = value;
                }
                this.pnlView.Document.PageIndex = value;
            }
        }

        private bool _FixedTimelineLeftHeader = true;
        /// <summary>
        /// 固定时间轴的左侧标题列
        /// </summary>
        [DefaultValue( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool FixedTimelineLeftHeader
        {
            get 
            {
                return _FixedTimelineLeftHeader; 
            }
            set
            {
                _FixedTimelineLeftHeader = value;
                UpdateLeftHeader( true );
            }
        }

        /// <summary>
        /// 表示为空的数值
        /// </summary>
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float NullValue
        {
            get
            {
                return TemperatureDocument.NullValue;
            }
        }

        /// <summary>
        /// 运行设计器之后触发的事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event AfterRunDesignerEventHandler EventAfterRunDesigner = null;

        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected virtual void OnEventAfterRunDesigner(AfterRunDesignerEventArgs args)
        {
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, args));
                return;
            }
            if (this.EventAfterRunDesigner != null)
            {
                if (this.IsAxControl)
                {
                    try
                    {
                        this.EventAfterRunDesigner(this, args);
                    }
                    catch (System.Exception)
                    {
                    }
                }
                else
                {
                    this.EventAfterRunDesigner(this, args);
                }
            }
        }
#if ! MWGA
        /// <summary>
        /// 运行设计器
        /// </summary>
        /// <returns>操作是否修改了文档内容</returns>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool RunDesigner()
        {
            using (frmTimeLineDesigner frm = new frmTimeLineDesigner())
            {
                frm.SourceDocument = this.Document;
                frm.SourceControl = this;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    AfterRunDesignerEventArgs args = new AfterRunDesignerEventArgs(
                        this, 
                        this.Document);
                    this.OnEventAfterRunDesigner(args);
                    this.RefreshView();
                    return true;
                }
                this.pnlView.RefreshViewWithoutRefreshDataSource();
            }
            return false;
        }
        /// <summary>
        /// 最大化运行设计器
        /// </summary>
        /// <returns></returns>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool RunDesignerMax()
        {
            using (frmTimeLineDesigner frm = new frmTimeLineDesigner())
            {
                frm.SourceDocument = this.Document;
                frm.SourceControl = this;
                frm.WindowState = FormWindowState.Maximized;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    AfterRunDesignerEventArgs args = new AfterRunDesignerEventArgs(
                        this,
                        this.Document );
                    this.OnEventAfterRunDesigner(args);
                    this.RefreshView();
                    return true;
                }
                this.pnlView.RefreshViewWithoutRefreshDataSource();
            }
            return false;
        }

#endif

        private bool _EnabledControlEvent = true;
        /// <summary>
        /// 是否允许控件的事件
        /// </summary>
        /// <remarks>
        /// 如果本属性为false，则不触发任何编辑器的事件，不过System.Windows.Forms.Control中定义的事件仍然会触发。
        /// </remarks>
        [DefaultValue(true)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCPublishAPI]
        public bool EnabledControlEvent
        {
            get
            {
                return _EnabledControlEvent;
            }
            set
            {
                _EnabledControlEvent = value;
            }
        }

        //#region EventGetServerTime



        /// <summary>
        /// 是否启用事件消息机制
        /// </summary>
        internal bool EnabledEventMessage
        {
            get
            {
                return !this.EnabledControlEvent;
            }
        }

        private DCTimeLineControlEventMessageManager _EventMessages = new DCTimeLineControlEventMessageManager();

        

        /// <summary>
        /// 清空内部的编辑器控件事件消息队列
        /// </summary>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
         
        public void ClearEventMessage()
        {
            //this._LastEventMessage = null;
            if (_EventMessages != null)
            {
                this._EventMessages.Clear();
                //_EventMessages.Clear();
            }
        }
        /// <summary>
        /// 获得一个编辑器控件事件消息.只有当控件的EnabledControlEvent=false时，本函数才有效。
        /// </summary>
        /// <returns></returns>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCPublishAPI]
        public DCTimeLineControlEventMessage GetEventMessage()
        {
            if (this._EventMessages != null)
            {
                this._EventMessages.ClearLastEventMessage();
            }
            //this._LastEventMessage = null;
            if (this.EnabledControlEvent)
            {
                return null;
            }
            if (this.IsHandleCreated && this._EventMessages != null)
            {
                DCTimeLineControlEventMessage info = this._EventMessages.GetEventMessage();
                return info;

               
            }
            return null;
        }

        //private WriterControlEventMessage _LastEventMessage = null;
        /// <summary>
        /// 最后一次获得的事件消息对象
        /// </summary>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
         
        public DCTimeLineControlEventMessage LastEventMessage
        {
            get
            {
                if (this._EventMessages != null)
                {
                    return this._EventMessages.LastEventMessage;
                }
                else
                {
                    return null;
                }
                //return _LastEventMessage; 
            }
        }

        /// <summary>
        /// 发送事件消息
        /// </summary>
        /// <param name="msg"></param>
        internal void SendEventMessage(DCTimeLineControlEventMessage msg)
        {
            PostEventMessage(msg);
        }
        /// <summary>
        /// 发送事件消息
        /// </summary>
        /// <param name="msg"></param>
        internal void PostEventMessage(DCTimeLineControlEventMessage msg)
        {
            if (this.IsHandleCreated && this._EventMessages != null)
            {
                this._EventMessages.AddMessage(msg);
            }
        }

        /// <summary>
        /// 文档双击事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event DocumentDblClickEventHandler EventDocumentDblClick = null;
        private void OnEventDocumentDblClick(DocumentDblClickEventArgs args)
        {
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, args));
                return;
            }
            if (this.EventDocumentDblClick != null)
            {
                if (this.IsAxControl)
                {
                    try
                    {
                        this.EventDocumentDblClick(this, args);
                    }
                    catch (System.Exception)
                    {
                    }
                }
                else
                {
                    this.EventDocumentDblClick(this, args);
                }
            }
        }


        /// <summary>
        /// 文档双击事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event DocumentMouseMoveEventHandler EventDocumentMouseMove = null;
        private void OnEventDocumentMouseMove(DocumentMouseMoveEventArgs args)
        {
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, args));
                return;
            }
            if (this.EventDocumentMouseMove != null)
            {
                if (this.IsAxControl)
                {
                    try
                    {
                        this.EventDocumentMouseMove(this, args);
                    }
                    catch (System.Exception)
                    {
                    }
                }
                else
                {
                    this.EventDocumentMouseMove(this, args);
                }
            }
        }


        /// <summary>
        /// 文档单击事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event DocumentClickEventHandler EventDocumentClick = null;
        private void OnEventDocumentClick(DocumentClickEventArgs args)
        {
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, args));
                return;
            }
            if (this.EventDocumentClick != null)
            {
                if (this.IsAxControl)
                {
                    try
                    {
                        this.EventDocumentClick(this, args);
                    }
                    catch (System.Exception)
                    {
                    }
                }
                else
                {
                    this.EventDocumentClick(this, args);
                }
            }
        }



        /// <summary>
        /// 鼠标点击数据点事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event ValuePointClickEventHandler EventValuePointClick = null;
        private void OnEventValuePointClick(ValuePointClickEventArgs args)
        {
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, args));
                return;
            }
            if (this.EventValuePointClick != null)
            {
                if (this.IsAxControl)
                {
                    try
                    {
                        this.EventValuePointClick(this, args);
                    }
                    catch (System.Exception)
                    {
                    }
                }
                else
                {
                    this.EventValuePointClick(this, args);
                }
            }
        }

        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [System.Runtime.InteropServices.ComVisible(false)]
        public delegate void SelectPageIndexEventHander(object sender, SelectPageIndexChangeArgs e);
        /// <summary>
        /// 事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event SelectPageIndexEventHander SelectPageIndexChanged;

        /// <summary>
        /// 内置工具栏的换页事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event SelectPageIndexEventHander EventSelectPageIndexChanged = null;

        private void OnSelectPageIndexChanged(SelectPageIndexChangeArgs e)
        {
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, e));
                return;
            }
            if (SelectPageIndexChanged!=null)
            {
                SelectPageIndexChanged(this,e);
            }
            if (EventSelectPageIndexChanged != null)
            {
                EventSelectPageIndexChanged(this, e);
            }
        }
        /// <summary>
        /// 控件加载时的处理
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.DesignMode == false)
            {
              
                pnlView.ShowCrossLine = btnCrossLine.Checked;
                if (picLeftHeader.Visible)
                {
                    this.OnResize(null);
                }
            }
        }
        /// <summary>
        /// 文档配置信息XML字符串
        /// </summary>
        [Browsable( false )]
        [System.Runtime.InteropServices.ComVisible(true)]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DocumentConfigXml
        {
            get
            {
                return this.Document.ConfigXml;
            }
            set
            {
                this.Document.ConfigXml = value;
                //this.RefreshView();
            }
        }

        /// <summary>
        /// 文档配置对象
        /// </summary>
        [Browsable(false)]
        [System.Runtime.InteropServices.ComVisible(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocumentConfig DocumentConfig
        {
            get
            {
                return this.Document.Config;
            }
            set
            {
                this.Document.Config = value;
            }
        }

        /// <summary>
        /// 编辑数据模式
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Obsolete("本属性已废除，无任何效果。")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool EditValueMode
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
        /// <summary>
        /// 设置、获得包含文档数据的XML字符串
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ComVisible(true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string XMLText
        {
            get
            {
                return this.Document.XMLText;
            }
            set
            {
                this.Document.XMLText = value;
                this.RefreshViewWithoutRefreshDataSource();
            }
        }

        ///// <summary>
        ///// 设置、获得包含文档数据的带缩进的XML字符串
        ///// </summary>
        ////[Browsable(false)]
        ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        ////[ComVisible(true)]
        ////[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        ////public string XMLTextIndented
        ////{
        ////    get
        ////    {
        ////        return this.Document.XMLTextIndented;
        ////    }
        ////    set
        ////    {
        ////        this.Document.XMLTextIndented = value;
        ////        this.RefreshViewWithoutRefreshDataSource();
        ////    }
        ////}

        /// <summary>
        /// 清除数据
        /// </summary>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ClearData()
        {
            this.Document.ClearData();
            this.Document.Parameters.Clear();
        }

        /// <summary>
        /// 修改指定时间区域的范围
        /// </summary>
        /// <param name="zoneName">时间区域名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>操作是否修改了数据</returns>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool SetTimeLineZoneRange(string zoneName, DateTime startTime, DateTime endTime)
        {
            return this.Document.SetTimeLineZoneRange(zoneName, startTime, endTime);
        }

        /// <summary>
        /// 设置指定时间区域中的数据点颜色
        /// </summary>
        /// <param name="zoneName">时间区域名称</param>
        /// <param name="valueName">数据序列名称</param>
        /// <param name="colorValue">颜色值，比如"#ff00ff"</param>
        /// <returns>操作修改的数据点个数</returns>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int SetSymbolStyleByTimeZone(
            string zoneName,
            string valueName,
            string colorValue)
        {
            return this.Document.SetSymbolStyleByTimeZone(zoneName, valueName, colorValue);
        }

        /// <summary>
        /// 设置指定时间区域中的数据点样式
        /// </summary>
        /// <param name="zoneName">时间区域名称</param>
        /// <param name="valueName">数据序列名称</param>
        /// <param name="style">新的数据点图标样式</param>
        /// <returns>操作修改的数据点个数</returns>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int SetSymbolStyleByTimeZone(
            string zoneName,
            string valueName,
            ValuePointSymbolStyle style)
        {
            return this.Document.SetSymbolStyleByTimeZone(zoneName, valueName, style);
        }

        /// <summary>
        /// 设置页眉标题文本
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        [System.Runtime.InteropServices.ComVisible( true )]
        //[System.Obsolete("请使用SetParameterValue( name , value )，标签元素要事先设置ParameterName属性值。")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetHeaderLableValue(string title, string text)
        {
            this.Document.SetHeaderLableValue(title, text);
        }

        /// <summary>
        /// 设置文档参数值
        /// </summary>
        /// <param name="pName">参数名</param>
        /// <param name="pValue">参数值</param>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetParameterValue(string pName, string pValue)
        {
            this.Document.SetParameterValue(pName, pValue);
        }

        /// <summary>
        /// 根据序号设置页眉标题文本
        /// </summary>
        /// <param name="index">从0开始计算的序号</param>
        /// <param name="text">文本</param>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetHeaderLableValueByIndex(int index, string text)
        {
            this.Document.SetHeaderLableValueByIndex(index, text);
        }

        /// <summary>
        /// 创建一个数据点对象实例
        /// </summary>
        /// <returns>创建的对象实例</returns>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint CreateValuePoint()
        {
            return new ValuePoint();
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列的名称</param>
        /// <param name="point">数据点</param>
        /// <param name="covermode">覆盖模式设为true则当插入同时间点的数据点时，删除之前已存在的同时间点的数据点</param>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPoint(string name, ValuePoint point, bool covermode = false)
        {
            this.Document.AddPoint(name, point, covermode);
        }


        /// <summary>
        /// 删除数据点
        /// </summary>
        /// <param name="IsInterrupt">是否在此处制作断点</param>
        /// <param name="point">数据点</param>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DeletePoint(ValuePoint point, bool IsInterrupt)
        {
            if (point == null)
            {
                return;
            }
            if (IsInterrupt)
            {
                point = new ValuePoint();
                point.Left = point.Top = point.Value = float.NaN;
            }
            else
            {
                point.OwnerList.Remove(point);
            }
        }



        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="Value">数值</param>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeValue(string name, DateTime dtm, float Value)
        {
            this.Document.AddPointByTimeValue(name, dtm, Value);
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="text">数值</param>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeText(string name, DateTime dtm, string text)
        {
            this.Document.AddPointByTimeText(name, dtm, text);
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="text">文本</param>
        /// <param name="Value">数值</param>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeTextValue(string name, DateTime dtm, string text, float Value)
        {
            this.Document.AddPointByTimeTextValue(name , dtm, text, Value);
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="text">文本</param>
        /// <param name="htmlColorValue">HTML格式的颜色值</param>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeTextColor(string name, DateTime dtm, string text, string htmlColorValue)
        {
            this.Document.AddPointByTimeTextColor(name, dtm, text, htmlColorValue);
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="Value">数值</param>
        /// <param name="landernValue">灯笼数值</param>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeValueLandernValue(
            string name,
            DateTime dtm,
            float Value,
            float landernValue)
        {
            this.Document.AddPointByTimeValueLandernValue(name, dtm, Value, landernValue);
        }

        /// <summary>
        /// 是否显示提示文本 
        /// </summary>
        [DefaultValue( true )]
        [Category("Behavior")]
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ShowTooltip
        {
            get
            {
                return pnlView.Document.Config.ShowTooltip;
            }
            set
            {
                pnlView.Document.Config.ShowTooltip = value;
            }
        }

        /// <summary>
        /// 是否启用增强型的鼠标移动事件处理逻辑 
        /// </summary>
        [DefaultValue(false)]
        [Category("Behavior")]
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool EnableExtMouseMoveEvent
        {
            get
            {
                return pnlView.Document.Config.EnableExtMouseMoveEvent;
            }
            set
            {
                pnlView.Document.Config.EnableExtMouseMoveEvent = value;
            }
        }

        /// <summary>
        /// 是否显示十字线 
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ShowCrossLine
        {
            get
            {
                return pnlView.ShowCrossLine;
            }
            set
            {
                pnlView.ShowCrossLine = value;
            }
        }

        /// <summary>
        /// 是否显示工具条
        /// </summary>
        [DefaultValue( true )]
        [Category("Layout")]
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ToolbarVisible
        {
            get
            {
                return myToolStrip.Visible;
            }
            set
            {
                myToolStrip.Visible = value;
            }
        }

        /// <summary>
        /// 文档视图模式
        /// </summary>
        [DefaultValue(DocumentViewMode.Timeline)]
        [Category("Layout")]
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DocumentViewMode ViewMode
        {
            get
            {
                return pnlView.ViewMode;
            }
            set
            {
                pnlView.ViewMode = value;
              
            }
        }
         

        /// <summary>
        /// 文档对象
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocument Document
        {
            get
            {
                return pnlView.Document;
            }
            set
            {
                pnlView.Document = value;
            }
        }
         
         
        /// <summary>
        /// 页面设置
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DocumentPageSettings PageSettings
        {
            get
            {
                return pnlView.Document.Config.PageSettings ; 
            }
            set
            {
                pnlView.Document.Config.PageSettings = value;
            }
        }

        //private Color _PageBackColor = Color.White;
        /// <summary>
        /// 页面背景色
        /// </summary>
        [DefaultColorValue("White")]
        [Category("Appearance")]
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color PageBackColor
        {
            get
            {
                return pnlView.Document.Config.PageBackColor ; 
            }
            set
            {
                pnlView.Document.Config.PageBackColor = value;
                picLeftHeader.BackColor = value;
            }
        }

        /// <summary>
        /// 更新文档视图
        /// </summary>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RefreshView()
        {
            bool back = picLeftHeader.Visible;
            picLeftHeader.Visible = false;
            pnlView.RefreshView();
            picLeftHeader.Visible = back;
            UpdateLeftHeader( true );
            if (this.Document.Config.Zones != null && this.Document.Config.Zones.Count > 0)
            {
                // 出现自定义的时间区域，则只能使用时间轴模式
                btnWidelyViewMode_Click(null, null);
            }
            UpdatePageIndex();
            UpdateToolbar();
            UpdateEditMenuItems();
            this.pnlView.EditMan.Cancel();
        }

        /// <summary>
        ///不刷新数据源的更新文档视图
        /// </summary>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RefreshViewWithoutRefreshDataSource()
        {
            pnlView.RefreshViewWithoutRefreshDataSource();
            UpdateLeftHeader(true);
            if (this.Document.Config.Zones != null && this.Document.Config.Zones.Count > 0)
            {
                // 出现自定义的时间区域，则只能使用时间轴模式
                if (this.Document.RuntimeViewMode != DocumentViewMode.Timeline)
                {
                    btnWidelyViewMode_Click(null, null);
                }
            }
            UpdatePageIndex();
            UpdateEditMenuItems();
            this.pnlView.EditMan.Cancel();
        }

        private void UpdateEditMenuItems()
        {
            btnEditData.Visible = this.Document.Config.EditValuePointMode != EditValuePointEventHandleMode.None ;
            btnEditData.DropDownItems.Clear();
            ToolStripMenuItem cancelItem = new ToolStripMenuItem();
            cancelItem.Text = DCTimeLineStrings.CancelEditValuePoint;
            cancelItem.Click += new EventHandler(CancelEditValuePointMenuItem_Click);
            btnEditData.DropDownItems.Add(cancelItem);
            btnEditData.DropDownItems.Add(new ToolStripSeparator());
            foreach (YAxisInfo info in this.Document.VisibleYAxisInfos)
            {
                if (info.Style == YAxisInfoStyle.Value)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = string.Format(
                        DCTimeLineStrings.NewValuePoint_Name,
                        info.Title);
                    item.Click += new EventHandler(NewValuePointMenuItem_Click);
                    item.Tag = info;
                    item.Image = this.Document.CreateSymbolIcon(info);
                    btnEditData.DropDownItems.Add(item);
                }
            }
            btnEditData.DropDownItems.Add(new ToolStripSeparator());

            _MenuItemDeleteValuePoint = new ToolStripMenuItem();
            _MenuItemDeleteValuePoint.Text = DCTimeLineStrings.DeleteValuePoint;
            _MenuItemDeleteValuePoint.Click += new EventHandler(DeleteValuePointMenuItem_Click);
            btnEditData.DropDownItems.Add(_MenuItemDeleteValuePoint);

            _MenuItemDragValuePointFixedTime = new ToolStripMenuItem();
            _MenuItemDragValuePointFixedTime.Text = DCTimeLineStrings.DragValuePointFixedTime;
            _MenuItemDragValuePointFixedTime.Click += new EventHandler(_MenuItemDragValuePointFixedTime_Click);
            btnEditData.DropDownItems.Add(_MenuItemDragValuePointFixedTime);
        }
        /// <summary>
        /// 拖拽数据点方法
        /// </summary>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void BeginDragValuePointFixDate()
        {
            this.InnerViewControl.EditMan.BeginDragValuePointFixDate();
        }
        /// <summary>
        /// 拖拽数据点事件
        /// </summary>
        private void _MenuItemDragValuePointFixedTime_Click(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            BeginDragValuePointFixDate();
#endif
        }

        private ToolStripMenuItem _MenuItemDragValuePointFixedTime = null;
        private ToolStripMenuItem _MenuItemDeleteValuePoint = null;

        private void CancelEditValuePointMenuItem_Click(object sender, EventArgs e)
        {
            CancelEditValuePoint();
        }

        /// <summary>
        /// 结束编辑数据点
        /// </summary>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void CancelEditValuePoint()
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else
            this.InnerViewControl.EditMan.Cancel();
#endif
        }

        private void DeleteValuePointMenuItem_Click(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else
            BeginDeleteValuePoint();
#endif
        }
        /// <summary>
        /// 删除数据点
        /// </summary>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public void BeginDeleteValuePoint()
        {
            this.InnerViewControl.EditMan.BeginDeleteValuePoint();
        }

        private void NewValuePointMenuItem_Click(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            this.InnerViewControl.EditMan.BeginInsertValuePoint(item.Tag as YAxisInfo);
#endif
        }

        /// <summary>
        /// 开始为指定的数据序列插入数据点
        /// </summary>
        /// <param name="yaxisInfoName">数据序列名称</param>
        /// <returns>操作是否成功</returns>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool BeginInsertValuePointFor(string yaxisInfoName)
        {
            YAxisInfo info = this.Document.Config.YAxisInfos.GetItemByName(yaxisInfoName);
            if (info != null)
            {
                this.InnerViewControl.EditMan.BeginInsertValuePoint(info);
                return true;
            }
            return false;
        }


        private void btnEditData_DropDownOpening(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            foreach (object item in btnEditData.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem mItem = (ToolStripMenuItem)item;
                    if (mItem.Tag is YAxisInfo)
                    {
                        YAxisInfo ya = (YAxisInfo)mItem.Tag;
                        mItem.Checked = this.InnerViewControl.EditMan.IsInsertingValuePointFor(ya);
                    }
                }
            }
            if (_MenuItemDeleteValuePoint != null && _MenuItemDeleteValuePoint.IsDisposed == false)
            {
                _MenuItemDeleteValuePoint.Checked = this.InnerViewControl.EditMan.CurrentMode == ValuePointEditMode.DeleteValuePoint ;
            }
            if (_MenuItemDragValuePointFixedTime != null && _MenuItemDragValuePointFixedTime.IsDisposed == false)
            {
                _MenuItemDragValuePointFixedTime.Checked = this.InnerViewControl.EditMan.CurrentMode == ValuePointEditMode.DragValuePointFixDate;
            }
#endif
        }

        /// <summary>
        /// 刷新文档内容后的事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandler EventAfterRefreshView = null;

        protected virtual bool IsAxControl
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 刷新文档内容后的处理
        /// </summary>
        internal virtual void OnAfterRefreshView(PaintEventArgs e)
        {
            if (this.EnabledControlEvent == false)
            {
                this.SendEventMessage(new DCTimeLineControlEventMessage(this, DCTimeLineControlEventMessageType.AfterRefreshView));
                return;
            }
            if (EventAfterRefreshView != null)
            {
                if (this.IsAxControl)
                {
                    try
                    {
                        EventAfterRefreshView(this, e);
                    }
                    catch (System.Exception ext)
                    {
                        //DCConsole.Default.WriteLineError(ext.ToString());
                    }
                }
                else
                {
                    this.EventAfterRefreshView(this, e);
                }
            }
        }

        /// <summary>
        /// 更新控件状态
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void UpdateState()
        {
            pnlView.UpdateState();
        }
          
        private void cboPageIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            OnSelectPageIndexChanged(new SelectPageIndexChangeArgs(cboPageIndex.SelectedIndex));
            pnlView.CurrentPageIndex = cboPageIndex.SelectedIndex;
            pnlView.Invalidate();
#endif
        }

        private void btnPrintCurrentPage_Click(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            pnlView.PrintDocument(pnlView.CurrentPageIndex, null);
#endif
        }
        
        private void btnPrintAll_Click(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            pnlView.PrintDocument(-1, null);
#endif
        }

        /// <summary>
        /// 打印当前页
        /// </summary>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void PrintCurrentPage(PrinterSettings ps = null)
        {
            if (pnlView.ViewMode == DocumentViewMode.Normal || pnlView.ViewMode == DocumentViewMode.Page)
            {
                pnlView.PrintDocument(pnlView.CurrentPageIndex, ps);
            }
        }

        /// <summary>
        /// 打印指定页
        /// </summary>
        /// <param name="pageIndex">从0开始计算的页码</param>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void PrintDocumentSpecifyPageIndex(int pageIndex, PrinterSettings ps = null)
        {
            pnlView.PrintDocument(pageIndex - 1, ps);
        }
        /// <summary>
        /// 批量打印指定页
        /// </summary>
        /// <param name="pageIndex"></param>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void PrintDocumentPageIndex(string pageIndex , PrinterSettings ps = null)
        {
           

            if(pageIndex == null  || pageIndex.Length == 0)
            {
                return;
            }
            List<int> pageIndexes = new List<int>();
            int tempi = int.MinValue;
            int tempi2 = int.MinValue;

            string[] indexgroup = pageIndex.Split(',');
            foreach (string str in indexgroup)
            {
                string[] group2 = str.Split('-');
                if (group2.Length == 2 &&
                    int.TryParse(group2[0], out tempi) == true &&
                    int.TryParse(group2[1], out tempi2) == true)
                {
                    //这里还有逻辑要处理 3-6这样的情况解析成3456
                    int num = tempi < tempi2 ? 1 : -1;
                    while ((tempi <= tempi2 && num == 1) || (tempi >= tempi2 && num == -1))
                    {
                        if(pageIndexes.Contains(tempi) == false)
                        {
                            pageIndexes.Add(tempi);
                        }
                        tempi += num;
                    }
                }
                else if (group2.Length == 1 &&
                    int.TryParse(group2[0], out tempi) == true &&
                    pageIndexes.Contains(tempi) == false)
                {
                    pageIndexes.Add(tempi);
                }
            }

            if (pageIndexes.Count > 0)
            {
                pageIndexes.Sort();
                try
                {
                    List<int> usedPagfeIndexes = new List<int>();
                    foreach (int i in pageIndexes)
                    {
                        usedPagfeIndexes.Add(i - 1);
                    }
                    ///////////////////////////////////////
                    pnlView.PrintDocument(usedPagfeIndexes, ps);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        /// <summary>
        /// 打印所有内容
        /// </summary>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void PrintDocument(PrinterSettings ps = null)
        {
            pnlView.PrintDocument(-1, ps);
        }

        /// <summary>
        /// 从文本读取器中加载文档
        /// </summary>
        /// <param name="reader">文本读取器</param>
        [ComVisible( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void LoadDocument(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            TemperatureDocument doc = new TemperatureDocument();
            doc.Load(reader);
            LoadDocument(doc);
        }

        /// <summary>
        /// 从文件流中加载文档
        /// </summary>
        /// <param name="stream">文件流对象</param>
        [ComVisible(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void LoadDocument(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            TemperatureDocument doc = new TemperatureDocument();
            doc.Load(stream);
            LoadDocument(doc);
        }

        private void LoadDocument(TemperatureDocument doc)
        {
            pnlView.LoadDocument(doc);
            UpdateToolbar();
            UpdateEditMenuItems();
            this.Invalidate(true);
        }
        /// <summary>
        /// 从文件中加载文档
        /// </summary>
        /// <param name="fileName">文件名</param>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void LoadDocumentFromFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            if (System.IO.File.Exists(fileName) == false)
            {
                throw new System.IO.FileNotFoundException(fileName);
            }
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            TemperatureDocument doc = new TemperatureDocument();
            doc.LoadFromFile(fileName);
            LoadDocument(doc);
#endif
        }
        /// <summary>
        /// 从字符串中加载文档
        /// </summary>
        /// <param name="xml">字符串</param>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void LoadDocumentFormString(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException("xml");
            }
            TemperatureDocument doc = new TemperatureDocument();
            doc.LoadFromString(xml);
            LoadDocument(doc);
        }

        /// <summary>
        /// 保存文件到流中
        /// </summary>
        /// <param name="stream">文件流对象</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [ComVisible(false)]
        public void SaveDocument(Stream stream)
        {
            //this.SaveDocument(stream);
            this.Document.Save(stream);//20150430刘帅修改，调用方法写错。
        }

        /// <summary>
        /// 保存文件到文本书写器中
        /// </summary>
        /// <param name="writer">文本书写器</param>
        [ComVisible( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SaveDocument(TextWriter writer)
        {
            this.Document.Save(writer);
        }

        /// <summary>
        /// 保存文档到字符串中
        /// </summary>
        /// <returns>生成的字符串</returns>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SaveDocumentToString()
        {
            return this.Document.SaveToString();
        }

        /// <summary>
        /// 保存文档到文件中
        /// </summary>
        /// <param name="fileName">文件名</param>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SaveDocumentToFile(string fileName)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            this.Document.SaveToFile(fileName);
#endif
        }

        /// <summary>
        /// 保存数据HTML文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SaveDataHtmlToFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            using (System.IO.FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                SaveDataHtmlToStream(stream);
            }
#endif
        }
        
        /// <summary>
        /// 保存数据到HTML文档流中
        /// </summary>
        /// <param name="stream">文件流</param>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SaveDataHtmlToStream(System.IO.Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            this.Document.SaveDataHtml(stream);
        }

        private void UpdatePageIndex()
        {
            if (cboPageIndex.Items.Count != pnlView.Document.NumOfPages)
            {
                cboPageIndex.Items.Clear();
                for (int iCount = 0; iCount < pnlView.Document.NumOfPages; iCount++)
                {
                    cboPageIndex.Items.Add(Convert.ToString(iCount + 1));
                }
            }
        }
         
        private void btnViewMode_DropDownOpening(object sender, EventArgs e)
        {
            btnPageViewMode.Checked = pnlView.ViewMode == DocumentViewMode.Page;
            btnNormalViewMode.Checked = pnlView.ViewMode == DocumentViewMode.Normal;
            btnWidelyViewMode.Checked = pnlView.ViewMode == DocumentViewMode.Timeline;
        }

        private bool CheckTimeZoneExisted()
        {
            if (this.Document != null && this.Document.Config.HasTimeLineZones)
            {
                MessageBox.Show(
                    this,
                    DCTimeLineStrings.PromptExistTimeZone,
                    DCTimeLineStrings.SystemAlert,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void btnNormalViewMode_Click(object sender, EventArgs e)
        {
            if (CheckTimeZoneExisted() == false)
            {
                return;
            }
            this.ViewMode = DocumentViewMode.Normal;
            this.RefreshViewWithoutRefreshDataSource();
            UpdateToolbar();
        }

        private void btnPageViewMode_Click_1(object sender, EventArgs e)
        {
            if (CheckTimeZoneExisted() == false)
            {
                return;
            }
            this.ViewMode = DocumentViewMode.Page;
            this.RefreshViewWithoutRefreshDataSource();
            UpdateToolbar();
        }

        private void btnWidelyViewMode_Click(object sender, EventArgs e)
        {
            this.ViewMode = DocumentViewMode.Timeline;
            this.RefreshViewWithoutRefreshDataSource();
            UpdateToolbar();
        }

        private void UpdateToolbar()
        {
            cboPageIndex.Enabled = pnlView.ViewMode == DocumentViewMode.Page || pnlView.ViewMode == DocumentViewMode.Normal;
            //btnPrintAll.Enabled = pnlView.ViewMode == DocumentViewMode.Page || pnlView.ViewMode == DocumentViewMode.Normal;
            btnPrintCurrentPage.Enabled = pnlView.ViewMode == DocumentViewMode.Page || pnlView.ViewMode == DocumentViewMode.Normal;
            btnPrintAll.Enabled = btnPrintCurrentPage.Enabled;
            //btnEditValue.Checked = this.EditValueMode;
            UpdatePageIndex();
            UpdateLeftHeader(true);
        }
        private void UpdateLeftHeader( bool raiseOnResize )
        {
            bool showHeader = pnlView.ViewMode == DocumentViewMode.Timeline;
            if (this.FixedTimelineLeftHeader == false)
            {
                showHeader = false;
            }
            if (showHeader)
            {
                pnlView.Document.UpdateLeftHeaderWidth();
                if (pnlView.Document.LeftHeaderPixelWidth <= 0)
                {
                    showHeader = false;
                }
            }
            if( picLeftHeader.Visible != showHeader )
            {
                picLeftHeader.Visible = showHeader ;
            }
            if (showHeader)
            {
                picLeftHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
              
                pnlView.Refresh();
                picLeftHeader.Width = (int)Math.Ceiling(pnlView.Document.LeftHeaderPixelWidth) + 1;
                picLeftHeader.Height = pnlView.ClientSize.Height - SystemInformation.HorizontalScrollBarHeight;
                picLeftHeader.Invalidate();
                if (raiseOnResize)
                {
                    OnResize(EventArgs.Empty);
                }
            }
        }
         
        /// <summary>
        /// 处理控件大小改变事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (picLeftHeader.Visible)
            {
                if (this.Width > 10 && this.Height > 10)
                {
                    UpdateLeftHeaderSize();
                    var tmr = new System.Windows.Forms.Timer();
                    tmr.Interval = 100;
                    tmr.Tick += new EventHandler(tmr_Tick);
                    tmr.Start();
                }
            }
            if (this.pnlView.ViewMode == DocumentViewMode.Timeline
                || this.pnlView.ViewMode == DocumentViewMode.Normal)
            {

            }
        }

        private void UpdateLeftHeaderSize()
        {
            picLeftHeader.BackColor = pnlView.BackColor;
            Rectangle pb = new Rectangle(pnlView.Left + SystemInformation.Border3DSize.Width,
                pnlView.Top + SystemInformation.Border3DSize.Height,
                picLeftHeader.Width,
                pnlView.Height - SystemInformation.Border3DSize.Height * 2 - SystemInformation.HorizontalScrollBarHeight);
            if (picLeftHeader.Bounds != pb)
            {
                picLeftHeader.Bounds = pb;
                picLeftHeader.Invalidate();
            }
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            var tmr = (System.Windows.Forms.Timer)sender;
            tmr.Dispose();
            UpdateLeftHeaderSize();
        }
        /// <summary>
        /// 处理键盘按下事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            this.InnerViewControl.EditMan.HandleKeyDown(e);
        }

        /// <summary>
        /// 显示关于对话框
        /// </summary>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AboutControl()
        {
            pnlView.AboutControl();
        }

        private void btnCrossLine_Click(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            this.ShowCrossLine = btnCrossLine.Checked;
#endif
        }

        private void picLeftHeader_Paint(object sender, PaintEventArgs e)
        {
            if (pnlView.Document != null)
            {
                pnlView.Document.PrintingMode = false;
                e.Graphics.FillRectangle(
                    DCSoft.Drawing.GraphicsObjectBuffer.GetSolidBrush(pnlView.RuntimePageBackColor), 
                    e.ClipRectangle);
                RectangleF clipRect = DCSoft.Drawing.GraphicsUnitConvert.Convert(
                    e.ClipRectangle, 
                    GraphicsUnit.Pixel, 
                    this.Document.GraphicsUnit);
                e.Graphics.PageUnit = pnlView.Document.GraphicsUnit;
                e.Graphics.ResetClip();// XP系统下有个BUG，在此无限扩大剪切区域
                pnlView.Document.Draw2(
                    new  DCGraphicsForTimeLine( e.Graphics ), 
                    clipRect, 
                    DocumentViewParty.LeftHeader );
                //e.Graphics.ResetClip();
                //e.Graphics.ResetTransform();
                //DrawerUtil.DrawDebugRectangle(e.Graphics, new RectangleF(0, 0, pnlView.Document.PixelToDocumentUnit(pnlView.Document.LeftHeaderPixelWidth), pnlView.Document.Height), Color.Red);
                //DrawerUtil.DrawDebugRectangle(e.Graphics, new RectangleF( clipRect.Left + 6 , clipRect.Top + 6 , clipRect.Width - 20, clipRect.Height - 20 ) , Color.Blue);
            }
        }

        private void picLeftHeader_MouseClick(object sender, MouseEventArgs e)
        {
            if (pnlView.HandleMouseClickForLeftHeader(
                e.X + this.pnlView.AutoScrollPosition.X , 
                e.Y + this.pnlView.AutoScrollPosition.Y))
            {
                picLeftHeader.Invalidate();
                WinForms.WinFormUtils.RunOnceDelay(
                    delegate(object sender2, EventArgs e2) 
                    { picLeftHeader.Invalidate(); }, 
                    100);
            }
        }

        /// <summary>
        /// 编辑数据按钮是否可见
        /// </summary>
        [DefaultValue( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool EditValueButtonVisible
        {
            get
            {
                return this.btnEditValue.Visible;
            }
            set
            {
                this.btnEditValue.Visible = value;
            }
        }

        private void btnEditValue_Click(object sender, EventArgs e)
        {
            //this.DocumentViewControl.BehaviorMode = btnEditValue.Checked ? DocumentBehaviorMode.EditValueMode : DocumentBehaviorMode.Normal;
        }

        private void tsbImportImg_Click(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else

            if (this.Document != null)
            {
                ExportImage(null, this.Document.PageIndex);
            }
#endif
        }

       
        /// <summary>
        /// 页面视图下导出指定页面的文档图片并保存到指定地址
        /// </summary>
        /// <param name="SavePath">指定导出图片要保存的位置</param>
        /// <param name="PageIndex"></param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [ComVisible(true)]
        public bool ExportImg(string SavePath, int PageIndex)
        {
            if (string.IsNullOrEmpty(SavePath) == false)
            {
                ExportImage(SavePath, PageIndex);
                return true;
            }
            return false;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        private bool ExportImage(string path,int pageindex)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else
            if (this.ViewMode == DocumentViewMode.Page)
            {
                using (Bitmap bmp = pnlView.Document.CreatePageBmp(pageindex))
                {
                   
                    if (string.IsNullOrEmpty(path))
                    {
                        using (SaveFileDialog sfd = new SaveFileDialog())
                        {
                            sfd.InitialDirectory = Application.StartupPath;
                            sfd.Filter = "jpg文件(*.jpg)|*.jpg|png文件(*.png)|*.png";
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                if (string.IsNullOrEmpty(sfd.FileName) == false)
                                {
                                    bmp.Save(
                                        sfd.FileName,
                                        System.Drawing.Imaging.ImageFormat.Jpeg
                                        );

                                    MessageBox.Show("保存成功");
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        bmp.Save(path,System.Drawing.Imaging.ImageFormat.Jpeg);
                        return true;
                    }
                }
            }
            else
            {
                //MessageBox.Show("此功能仅限分页模式下使用");
                //return false;
                using (Bitmap bmp = pnlView.Document.CreateFullContentBmp())
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        using (SaveFileDialog sfd = new SaveFileDialog())
                        {
                            sfd.InitialDirectory = Application.StartupPath;
                            sfd.Filter = "jpg文件(*.jpg)|*.jpg|png文件(*.png)|*.png";
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                if (string.IsNullOrEmpty(sfd.FileName) == false)
                                {
                                    bmp.Save(
                                        sfd.FileName,
                                        System.Drawing.Imaging.ImageFormat.Jpeg
                                        );

                                    MessageBox.Show("保存成功");
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return true;
                    }
                }

            }
#endif
            return false;
        }

        private void btnDesigner_Click(object sender, EventArgs e)
        {
#if !MWGA
            this.RunDesigner();
#endif
        }

    }




    /// <summary>
    /// 页码改变事件
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public partial class SelectPageIndexChangeArgs
    {
        private int _PageIndex;
        /// <summary>
        /// 页码改变数据
        /// </summary>
        /// <param name="pageIndex"></param>
        public SelectPageIndexChangeArgs(int pageIndex)
        {
            _PageIndex = pageIndex;
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get { return _PageIndex; }
        }
    }
}
