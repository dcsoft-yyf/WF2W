using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DCSoft.TemperatureChart
{
    [System.Runtime.InteropServices.ComVisible( false )]
    internal class EditValuePointManager
    {
        public EditValuePointManager( )
        {
        }

        private TemperatureControl _Control = null;

        public TemperatureControl Control
        {
            get
            {
                return _Control; 
            }
            set
            {
                _Control = value;
                if (_Control != null)
                {
                    _ViewControl = this._Control.InnerViewControl;
                }
            }
        }
        private TemperatureViewControl _ViewControl = null;

        public TemperatureViewControl ViewControl
        {
            get { return _ViewControl; }
            set { _ViewControl = value; }
        }

        public bool Enabled
        {
            get
            {
                // 控件不只读而且文档允许修改数值
                return this.ViewControl.Document.Config.Readonly == false
                    || this.ViewControl.Document.Config.EditValuePointMode != EditValuePointEventHandleMode.None ;
            }
        }

        /// <summary>
        /// 取消编辑数值操作
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Cancel()
        {
            if (this.CurrentMode != ValuePointEditMode.None)
            {
                this._CurrentMode = ValuePointEditMode.None;
                this.ViewControl.Cursor = Cursors.Arrow;
                if (this._YaxisInfoForInserting != null)
                {
                    _YaxisInfoForInserting.Selected = false;
                    _YaxisInfoForInserting = null;
                    this._Control.Invalidate(true);
                    this._ViewControl.ShowToolTip( null , false );
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 开始删除数据点
        /// </summary>
        public void BeginDeleteValuePoint()
        {
            if (this.Enabled == false)
            {
                return;
            }
            this._CurrentMode = ValuePointEditMode.DeleteValuePoint;
            if (this._YaxisInfoForInserting != null)
            {
                _YaxisInfoForInserting.Selected = false;
                _YaxisInfoForInserting = null;
                this._Control.Invalidate(true);
            }
        }

        //public void BeginDragValuePointFree()
        //{
        //    if (this.Enabled == false)
        //    {
        //        return;
        //    }
        //    this._CurrentMode = ValuePointEditMode.DragValuePointFree;
        //    if (this._YaxisInfoForInserting != null)
        //    {
        //        this._YaxisInfoForInserting.Selected = false;
        //    }
        //    this._YaxisInfoForInserting = null;
        //    this._Control.Invalidate(true);
        //}

        public void BeginDragValuePointFixDate()
        {
            if (this.Enabled == false)
            {
                return;
            }
            this._CurrentMode = ValuePointEditMode.DragValuePointFixDate;
            if (this._YaxisInfoForInserting != null)
            {
                this._YaxisInfoForInserting.Selected = false;
            }
            this._YaxisInfoForInserting = null;
            this._Control.Invalidate(true);
        }


        private YAxisInfo _YaxisInfoForInserting = null;
        /// <summary>
        /// 当前拖拽的数据点对象
        /// </summary>
        private ValuePoint _CurrentDragValuePoint = null;
        
        /// <summary>
        /// 开始新增数据点
        /// </summary>
        /// <param name="info"></param>
        public void BeginInsertValuePoint(YAxisInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            if (this.Enabled == false)
            {
                return;
            }
            TemperatureDocument document = this._ViewControl.Document;
            if (document.Config.YAxisInfos.Contains(info) == false)
            {
                throw new ArgumentOutOfRangeException("Info:" + info.Name);
            }
            foreach (YAxisInfo item in document.Config.YAxisInfos)
            {
                item.Selected = false;
            }
            info.ValueVisible = true;
            info.Selected = true;
            this._CurrentMode = ValuePointEditMode.InsertValuePoint;
            this._Control.Invalidate(true);
            _YaxisInfoForInserting = info;
        }

        /// <summary>
        /// 当前是否在插入数据点
        /// </summary>
        private bool IsInsertingValuePoint
        {
            get
            {
                return this._CurrentMode == ValuePointEditMode.InsertValuePoint 
                    && _YaxisInfoForInserting != null;
            }
        }

        public bool IsInsertingValuePointFor(YAxisInfo info)
        {
            return this._CurrentMode == ValuePointEditMode.InsertValuePoint
                && this._YaxisInfoForInserting == info;
        }

        public bool HandleMouseMove(object sender, MouseEventArgs args)
        {
            if (this.Enabled == false)
            {
                return false;
            }
            Control ctl = sender as Control;
            switch (this._CurrentMode)
            {
                case ValuePointEditMode.None :
                    // 无操作
                    break;
                case ValuePointEditMode.InsertValuePoint:
                    {
                        // 插入数据点
                        if (ctl != null)
                        {
                            ctl.Cursor = PointerNewRecord;
                        }
                        ValuePoint newVP = CreateValuePointByClientPosition(this._YaxisInfoForInserting, args.X, args.Y);
                        string toolTipText = null;
                        if (newVP != null)
                        {
                            toolTipText = string.Format(
                                    DCTimeLineStrings.NewValuePoint_Name, this._YaxisInfoForInserting.Title)
                                    + Environment.NewLine + DCTimeLineStrings.Time + ":" +
                                        newVP.Time.ToString( DCTimeLineUtils.GetDateTimeFormatString( this._YaxisInfoForInserting.InputTimePrecision ))
                                    + Environment.NewLine + DCTimeLineStrings.Value + ":" +  newVP.Value;
                        }
                        this.ViewControl.ShowToolTip(toolTipText, true);
                    }
                    return true;
                case ValuePointEditMode.DeleteValuePoint :
                    // 删除数据点
                    if (ctl != null)
                    {
                        PointF p = this.ViewControl.ClientToView(args.X, args.Y);
                        ValuePoint vp = this.ViewControl.GetValuePointByViewPosition( p.X , p.Y );
                        if (vp == null)
                        {
                            return false;
                        }
                        else
                        {
                            ctl.Cursor = PointerDeleteRecord;
                            return true;
                        }
                    }
                    return true;
                case ValuePointEditMode.EditValuePoint :
                    // 编辑数据点
                    return true;
                case ValuePointEditMode.DragValuePointFree :
                case ValuePointEditMode.DragValuePointFixDate:
                    {
                        // 拖拽数据点
                        if (this._CurrentDragValuePoint != null && args.Button == MouseButtons.Left)
                        {
                            // 正在拖拽数据点
                            if (ctl != null)
                            {
                                ctl.Cursor = PointerDragRecord ;
                            }
                            YAxisInfo info = (YAxisInfo)this._CurrentDragValuePoint.Parent;
                            if (info == null)
                            {
                                return true;
                            }
                            ValuePoint newVP = CreateValuePointByClientPosition( info , args.X, args.Y);
                            //TemperatureDocument document = this.ViewControl.Document;
                            string toolTipText = null;
                            if (newVP != null)
                            {
                                DateTime disTime = _CurrentDragValuePoint.Time;
                                if (this.CurrentMode == ValuePointEditMode.DragValuePointFree)
                                {
                                    disTime = newVP.Time;
                                }
                                toolTipText = string.Format(
                                    DCTimeLineStrings.DragValuePoint_Name, info.Title)
                                    + Environment.NewLine + DCTimeLineStrings.Time + ":" + disTime.ToString( 
                                        DCTimeLineUtils.GetDateTimeFormatString( info.InputTimePrecision ))
                                    + Environment.NewLine + DCTimeLineStrings.Value + ":" + newVP.Value;
                            }
                            this.ViewControl.ShowToolTip(toolTipText, true);
                        }
                        else
                        {
                            if (ctl != null)
                            {
                                ctl.Cursor = Cursors.Arrow;
                            }
                            return false;
                        }
                    }
                    return true;
            }
            return false;
        }

        private Point _DragStartPosition = Point.Empty;

        public bool HandleMouseDown(object sender, MouseEventArgs args)
        {
            if (this.Enabled == false)
            {
                return false ;
            }
            if (this.CurrentMode == ValuePointEditMode.DeleteValuePoint)
            {
                // 删除数据点
                TemperatureDocument document = this.ViewControl.Document;
                PointF p = this.ViewControl.ClientToView(args.X, args.Y);
                ValuePoint vp = this.ViewControl.GetValuePointByViewPosition(p.X, p.Y);
                if (vp != null)
                {                    
                    EditValuePointEventArgs args2 = new EditValuePointEventArgs(
                        this._Control, 
                        document , 
                        vp, 
                        EditValuePointMode.Delete);
                    args2.Result = true;
                    this.ViewControl.OnEventEditValuePoint(args2);
                    if (args2.Result)
                    {
                        // 删除数据点
                        ValuePointList list = vp.OwnerList;
                        list.Remove(vp);
                        document.Modified = true;
                        this.ViewControl.Invalidate();
                    }
                    return true;
                }
                return false;
            }
            else if (this.CurrentMode == ValuePointEditMode.InsertValuePoint)
            {
                TemperatureDocument document = this.ViewControl.Document;
                if (this._YaxisInfoForInserting != null)
                {
                    ValuePoint newVP = CreateValuePointByClientPosition(this._YaxisInfoForInserting, args.X, args.Y);
                    if (newVP != null)
                    {
                        newVP.Time = DCTimeLineUtils.FormatDateTime(newVP.Time, this._YaxisInfoForInserting.InputTimePrecision);
                        newVP.Parent = this._YaxisInfoForInserting;
                        EditValuePointEventArgs args2 = new EditValuePointEventArgs(
                            this._Control,
                            document,
                            newVP,
                            EditValuePointMode.Insert);
                        args2.Result = true;
                        if (this.ViewControl != null)
                        {
                            this.ViewControl.OnEventEditValuePoint(args2);
                        }
                        if (args2.Result)
                        {
                            // 操作成功
                            ValuePointList list = document.GetValuePointsByName(
                                this._YaxisInfoForInserting.Name);
                            list.Add(newVP);
                            list.SortByTime();
                            document.Modified = true;
                            this.ViewControl.Invalidate();
                            this.ViewControl.RefreshViewWithoutRefreshDataSource();
                        }
                        return true;
                    }
                }
                // 检索文本数据行
                PointF p = this.ViewControl.ClientToView(args.X, args.Y);
                foreach (TitleLineInfo line in document.Config.GetAllTitleLines())
                {
                    RectangleF dataBounds = new RectangleF(
                        document.DataGridBounds.Left, 
                        line.Top, 
                        document.DataGridBounds.Width, 
                        line.Height);
                    if (dataBounds.Contains(p))
                    {
                        ValuePointList list = document.GetValuePointsByName(
                                    line.Name);
                        DateTime dtm = document.RuntimeTicks.GetDateTimeByXPosition(document.DataGridBounds, p.X);
                        ValuePoint newValue = new ValuePoint();
                        newValue.Time = DCTimeLineUtils.FormatDateTime( dtm , line.InputTimePrecision );
                        if (list.IsTextMode( line ))
                        {
                            newValue.Text = "-";
                        }
                        else
                        {
                            newValue.Value = 0;
                        }
                        newValue.Value = 0;
                        newValue.Parent = line;
                        EditValuePointEventArgs args2 = new EditValuePointEventArgs(
                           this._Control,
                           document,
                           newValue,
                           EditValuePointMode.Insert);
                        args2.Result = true;
                        if (this.ViewControl != null)
                        {
                            this.ViewControl.OnEventEditValuePoint(args2);
                        }
                        if (args2.Result)
                        {
                            // 操作成功
                            list.Add(newValue);
                            list.SortByTime();
                            document.Modified = true;
                            this.ViewControl.Invalidate();
                            this.ViewControl.RefreshViewWithoutRefreshDataSource();
                        }
                        break;
                    }//if
                }//foreach
                return true;
            }
            else if (this.CurrentMode == ValuePointEditMode.DragValuePointFree
                || this.CurrentMode == ValuePointEditMode.DragValuePointFixDate)
            {
                // 开始拖拽数据点
                if (args.Button == MouseButtons.Left)
                {
                    TemperatureDocument document = this.ViewControl.Document;
                    PointF p = this.ViewControl.ClientToView(args.X, args.Y);
                    if (document.DataGridBounds.Contains(p.X, p.Y))
                    {
                        ValuePoint vp = this.ViewControl.GetValuePointByViewPosition(p.X, p.Y);
                        if (vp != null)
                        {
                            // 找到数据点，开始拖拽
                            YAxisInfo info = vp.Parent as YAxisInfo;
                            if (info != null && info.Style == YAxisInfoStyle.Value)
                            {
                                _CurrentDragValuePoint = vp;
                                if (this.CurrentMode == ValuePointEditMode.DragValuePointFixDate)
                                {
                                    // 固定时间，则限制鼠标移动范围
                                    Point p2 = this.ViewControl.ViewToClient(0, document.DataGridBounds.Top + document.DataGridBounds.Height * info.RuntimeTopPadding );
                                    Point p3 = this.ViewControl.ViewToClient(0, document.DataGridBounds.Bottom - document.DataGridBounds.Height * info.RuntimeBottomPadding );
                                    Rectangle rect = new Rectangle(
                                        args.X,
                                        p2.Y,
                                        2,
                                        p3.Y - p2.Y);
                                    rect.Location = this.ViewControl.PointToScreen(rect.Location);
                                    Cursor.Clip = rect;
                                    _DragStartPosition = new Point(args.X, args.Y);
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 处理鼠标按键松开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool HandleMouseUp(object sender , MouseEventArgs args)
        {
            if (this.Enabled == false)
            {
                return false ;
            }
            Cursor.Clip = Rectangle.Empty;
            if (this.CurrentMode == ValuePointEditMode.DragValuePointFixDate
                || this.CurrentMode == ValuePointEditMode.DragValuePointFree)
            {
                if (_CurrentDragValuePoint != null)
                {
                    // 结束拖拽数据点
                    if (Math.Abs(_DragStartPosition.X - args.X) < SystemInformation.DragSize.Width
                        && Math.Abs(_DragStartPosition.Y - args.Y) < SystemInformation.DragSize.Height)
                    {
                        // 拖动的距离太短了，不认为是拖拽操作
                        return true ;
                    }
                    ValuePoint newVP = CreateValuePointByClientPosition((YAxisInfo)_CurrentDragValuePoint.Parent, args.X, args.Y);
                    TemperatureDocument document = this.ViewControl.Document;
                    if (newVP != null)
                    {
                        ValuePoint vp2 = _CurrentDragValuePoint.Clone();
                        if (this.CurrentMode == ValuePointEditMode.DragValuePointFree)
                        {
                            vp2.Time = newVP.Time;
                        }
                        vp2.Value = newVP.Value;
                        EditValuePointEventArgs args3 = new EditValuePointEventArgs(this._Control, document, vp2, EditValuePointMode.Update);
                        this.ViewControl.OnEventEditValuePoint(args3);
                        if (args3.Result)
                        {
                            _CurrentDragValuePoint.Time = vp2.Time;
                            _CurrentDragValuePoint.Value = vp2.Value;
                            this.ViewControl.Invalidate();
                            document.Modified = true;
                            this.ViewControl.Cursor = Cursors.Arrow;
                            this.ViewControl.RefreshViewWithoutRefreshDataSource();
                        }
                        this.ViewControl.ShowToolTip(null, false);
                        _CurrentDragValuePoint = null;
                    }
                }
                return true;
            }
            if (this.CurrentMode == ValuePointEditMode.None)
            {
                return false;
            }
            return true ;
        }

        public bool HandleKeyDown(KeyEventArgs args)
        {
            if (this.Enabled == false)
            {
                return false ;
            }
            if (args.KeyCode == Keys.Escape)
            {
                if (this.Cancel())
                {
                    return true;
                }
            }
            return false;
        }

        private ValuePoint CreateValuePointByClientPosition(YAxisInfo info, int x, int y)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            TemperatureDocument document = this.ViewControl.Document;
            PointF p = this.ViewControl.ClientToView(x, y);
            if (document.DataGridBounds.Contains(p.X,p.Y))
            {
                DateTime dtm = document.RuntimeTicks.GetDateTimeByXPosition(
                    document.DataGridBounds,
                    p.X);
                if (TemperatureDocument.IsNullDate(dtm))
                {
                    return null;
                }
                float v = info.GetValueByDisplayY(
                    document,
                    document.DataGridBounds,
                    p.Y);
                if (TemperatureDocument.IsNullValue(v))
                {
                    return null;
                }
                ValuePoint vp = new ValuePoint();
                vp.Time = dtm;
                vp.Value = v;
                vp.Parent = info;
                return vp;
            }
            return null;
        }
        ///// <summary>
        ///// 结束新增数据点操作
        ///// </summary>
        //public void EndInsertValuePoint()
        //{
        //    this._CurrentMode = ValuePointEditMode.None;
        //    this._YaxisInfoForInserting = null;
        //}


        private static Cursor _PointerNewRecord = null;
        /// <summary>
        /// 新增数据点使用的鼠标光标对象
        /// </summary>
        public static Cursor PointerNewRecord
        {
            get
            {
                if (_PointerNewRecord == null)
                {
                    System.IO.Stream stream = typeof(DCTimeLineUtils).Assembly.GetManifestResourceStream(
                        "DCSoft.TemperatureChart.Images.PointerNewRecord.cur");
                    _PointerNewRecord = new Cursor( stream );
                }
                return _PointerNewRecord;
            }
        }

        private static Cursor _PointerDragRecord = null;
        /// <summary>
        /// 新增数据点使用的鼠标光标对象
        /// </summary>
        public static Cursor PointerDragRecord
        {
            get
            {
                if (_PointerDragRecord == null)
                {
                    System.IO.Stream stream = typeof(DCTimeLineUtils).Assembly.GetManifestResourceStream(
                        "DCSoft.TemperatureChart.Images.PointerDragRecord.cur");
                    _PointerDragRecord = new Cursor(stream);
                }
                return _PointerDragRecord;
            }
        }

        private static Cursor _PointerDeleteRecord = null;
        /// <summary>
        /// 新增数据点使用的鼠标光标对象
        /// </summary>
        public static Cursor PointerDeleteRecord
        {
            get
            {
                if (_PointerDeleteRecord == null)
                {
                    System.IO.Stream stream = typeof(DCTimeLineUtils).Assembly.GetManifestResourceStream(
                        "DCSoft.TemperatureChart.Images.PointerDeleteRecord.cur");
                    _PointerDeleteRecord = new Cursor( stream );
                }
                return _PointerDeleteRecord;
            }
        }
        /// <summary>
        /// 当前编辑模式
        /// </summary>
        private ValuePointEditMode _CurrentMode = ValuePointEditMode.None;
        /// <summary>
        /// 当前编辑模式
        /// </summary>
        internal ValuePointEditMode CurrentMode
        {
            get
            {
                return _CurrentMode; 
            }
            //set
            //{
            //    _CurrentMode = value; 
            //}
        }

        public bool IsDeleteValuePointMode
        {
            get
            {
                return _CurrentMode == ValuePointEditMode.DeleteValuePoint;
            }
        }

    }


    internal enum ValuePointEditMode
    {
        /// <summary>
        /// 无任何模式
        /// </summary>
        None,
        /// <summary>
        /// 插入数据点
        /// </summary>
        InsertValuePoint,
        /// <summary>
        /// 删除数据点
        /// </summary>
        DeleteValuePoint,
        /// <summary>
        /// 编辑数据点
        /// </summary>
        EditValuePoint,
        /// <summary>
        /// 自由拖拽方式来修改数据点的时间和数值
        /// </summary>
        DragValuePointFree,
        /// <summary>
        /// 以受限拖拽模式来修改数据点的数值,不修改数据时间
        /// </summary>
        DragValuePointFixDate

    }
}
