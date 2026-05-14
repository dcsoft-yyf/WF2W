using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using DCSoft.Common;
using DCSoft.Drawing;
using System.ComponentModel ;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    // 文档内容排版
    partial class TemperatureDocument
    {
        internal float PixelToDocumentUnit(float v)
        {
            return GraphicsUnitConvert.Convert(v, GraphicsUnit.Pixel, this.GraphicsUnit);
        }
#if ! DCWriterForWASM
        internal float DocumentUnitToPixel(float v)
        {
            return GraphicsUnitConvert.Convert(v, this.GraphicsUnit, GraphicsUnit.Pixel);
        }
#endif
        internal GraphicsUnit GraphicsUnit
        {
            get
            {
                return System.Drawing.GraphicsUnit.Document;
            }
        }

        /// <summary>
        /// 数据网格区域边界
        /// </summary>
        [NonSerialized]
        private RectangleF _DataGridBounds = RectangleF.Empty;
        /// <summary>
        /// 数据网格区域
        /// </summary>
        internal RectangleF DataGridBounds
        {
            get
            {
                return _DataGridBounds; 
            }
        }

        /// <summary>
        /// 布局时使用的起始时间
        /// </summary>
        [NonSerialized]
        private DateTime _LayoutStartDate = DateTime.MinValue;
        /// <summary>
        /// 布局时使用的最大时间
        /// </summary>
        [NonSerialized]
        private DateTime _LayoutMaxDate = DateTime.MinValue;
        [NonSerialized]
        private float _StdTitleLineHeight = 0;
        /// <summary>
        /// 可见的Y刻度尺信息对象列表
        /// </summary>
        [NonSerialized]
        private YAxisInfoList _VisibleYAxisInfos = new YAxisInfoList();
#if !DCWriterForWASM
        /// <summary>
        /// 可见的Y刻度尺信息对象列表
        /// </summary>
        internal YAxisInfoList VisibleYAxisInfos
        {
            get
            {
                return _VisibleYAxisInfos; 
            }
            set
            {
                _VisibleYAxisInfos = value; 
            }
        }
#endif
        /// <summary>
        /// 标题可见的Y刻度尺信息列表
        /// </summary>
        [NonSerialized]
        private YAxisInfoList _TitleVisibleYAxisInfos = new YAxisInfoList();
        [NonSerialized]
        private bool _LayoutInvalidate = true ;
        /// <summary>
        /// 文档内容布局无效，需要重新计算布局
        /// </summary>
        internal bool LayoutInvalidate
        {
            get
            {
                return _LayoutInvalidate; 
            }
            set
            {
                if (_LayoutInvalidate != value)
                {
                    _LayoutInvalidate = value;
                    if (_LayoutInvalidate)
                    {
                        this._RuntimeFooterLines = null;
                        this._RuntimeHeaderLines = null;
                    }
                }
            }
        }

        private void CheckLayoutInvalidate(DCGraphicsForTimeLine g)
        {
            if (this.LayoutInvalidate)
            {
                this.LayoutInvalidate = false;
                ExecuteLayout( g );
            }
        }

        /// <summary>
        /// 更新可见数据序列列表
        /// </summary>
        internal void UpdateVisibleYAxisInfos()
        {
            if (this.InnerBehaviorMode == DocumentBehaviorMode.DesignMode)
            {
                _VisibleYAxisInfos = this.YAxisInfos;
            }
            else
            {
                _VisibleYAxisInfos = new YAxisInfoList();
                foreach (YAxisInfo info in this.YAxisInfos)
                {
                    if (info.Visible == false)
                    {
                        continue;
                    }
                    bool macth = false;
                    foreach (YAxisInfo info2 in this.YAxisInfos)
                    {
                        if (info2.ShadowName == info.Name)
                        {
                            if (info2.Visible == false)
                            {
                                macth = true;
                            }
                        }
                    }
                    if (macth == false)
                    {
                        _VisibleYAxisInfos.Add(info);
                    }
                }//foreach
            }
        }

        private void UpdateStartEndDateForDayIndexTitleLine(TitleLineInfo info, DateTime minDate)
        {
            List<DateTime> runtimeStartDates = new List<DateTime>();
            if (info.ValueType == TitleLineValueType.DayIndex)
            {
                // 确认天数序列的开始计数时间
                if (TemperatureDocument.IsNullDate(info.StartDate) == false)
                {
                    runtimeStartDates.Add(info.StartDate);
                }
                else if (string.IsNullOrEmpty(info.StartDateKeyword) == false)
                {
                    foreach (YAxisInfo ya in _VisibleYAxisInfos)
                    {
                        if (ya.Style == YAxisInfoStyle.Text || ya.Style == YAxisInfoStyle.TextInsideGrid)
                        {
                            string[] KeyWords = new string[] { info.StartDateKeyword };
                            string endKeyWord = info.EndDateKeyword;
                            // 找到文本标记数据线
                            foreach (ValuePoint v in GetValuePointsByName(ya.Name))
                            {
                                //新增在此处记录DAYINDEX数据行的结束时间
                                if (v.Text != null &&
                                    v.Text.Length > 0 &&
                                    endKeyWord != null &&
                                    endKeyWord.Length > 0 &&
                                    v.Text.IndexOf(endKeyWord) >= 0)
                                {
                                    DateTime endt = v.Time.Date.AddDays(1);
                                    if (info.RuntimeEndDate == TemperatureDocument.NullDate || info.RuntimeEndDate > endt)
                                    {
                                        info.RuntimeEndDate = endt;
                                    }
                                }
                                //////////////////////////////////////////

                                if (info.StartDateKeyword.IndexOf(",") > 0)
                                {
                                    KeyWords = info.StartDateKeyword.Split(',');
                                }
                                for (int i = 0; i < KeyWords.Length; i++)
                                {
                                    if (v.Text != null && v.Text.Length > 0 && v.Text.IndexOf(KeyWords[i]) >= 0)
                                    {
                                        DateTime dtm = v.Time.Date.AddDays(1);
                                        if (runtimeStartDates.Count == 0
                                            || runtimeStartDates[runtimeStartDates.Count - 1] != dtm)
                                        {
                                            runtimeStartDates.Add(dtm);
                                        }
                                    }

                                }

                            }//foreach
                            break;
                        }
                    }//foreach
                }
            }
            else if (info.ValueType == TitleLineValueType.GlobalDayIndex)
            {
                runtimeStartDates.Add(minDate);
                //line.RuntimeStartDate = minDate;
            }
            if (runtimeStartDates.Count > 0)
            {
                if (info.PreserveStartKeywordOrder  )
                {
                    runtimeStartDates.Reverse();
                }
                /////////////////////////////
                info._RuntimeStartDates = runtimeStartDates.ToArray();
            }
            else
            {
                info._RuntimeStartDates = null;
            }
        }

        /// <summary>
        /// 执行文档排版完成后执行的委托对象
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        [System.ComponentModel.Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public EventHandler HandlerAfterExecuteLayout = null;

        

        /// <summary>
        /// 执行文档内容排版布局
        /// </summary>
        /// <param name="g">画布对象</param>
        internal float ExecuteLayout(DCGraphicsForTimeLine g )
        {
            
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            lock (this)
            {
                g.PageUnit = this.GraphicsUnit;
                this._RuntimeHeaderLines = null;
                this._RuntimeFooterLines = null;
                this._LayoutInvalidate = false;
                this._LeftHeaderWidth = 0;
                //float tickInit = CountDown.GetTickCountFloat();
                //float tick = CountDown.GetTickCountFloat();
                //float totalTick = tick;
                this.Config.CheckDefaultTicks();
                // 有数据的时间区间的最大值
                _LayoutMaxDate = TemperatureDocument.NullDate;
                // 有数据的时间区间的最小值
                DateTime minDate = TemperatureDocument.NullDate;
                this.UpdateNumOfPage(out _LayoutMaxDate, out minDate);
                //this.RefreshRuntimeTicks(minDate, _LayoutMaxDate, 0);


                if (this.Config != null && this.Config.IllegalTextEndCharForLinux != null && this.Config.IllegalTextEndCharForLinux.Length > 0)
                {
                    this._IllegalTextEndCharForLinux = new List<char>();
                    string tt = this.Config.IllegalTextEndCharForLinux;
                    foreach(char c in tt)
                    {
                        this._IllegalTextEndCharForLinux.Add(c);
                    }
                }

                // 当前区域的开始时间
                this._LayoutStartDate = minDate.AddDays(
                    this.RuntimePageIndex * this.RuntimeNumOfDaysInOnePage);
                this._LayoutMaxDate = this._LayoutStartDate.AddDays(this.RuntimeNumOfDaysInOnePage);
                // 刷新时间刻度
                this.RefreshRuntimeTicks(
                    _LayoutStartDate,
                    _LayoutMaxDate,// _LayoutStartDate.AddDays(this.RuntimeNumOfDaysInOnePage),
                    0/*this.RuntimeViewMode == DocumentViewMode.Timeline ? 0 : this.RuntimeNumOfDaysInOnePage * this.Config.Ticks.Count*/);

                this.RefreshRuntimeTicks(
                    _LayoutStartDate,
                    _LayoutMaxDate,// _LayoutStartDate.AddDays(this.RuntimeNumOfDaysInOnePage),
                    0/*this.RuntimeViewMode == DocumentViewMode.Timeline ? 0 : this.RuntimeNumOfDaysInOnePage * 6*/,
                    true);
                //////////////////////////////////////////////////////////////////////////////////////////////////////

                //tick = CountDown.GetTickCountFloat() - tick;

                //float tick2 = CountDown.GetTickCountFloat();
                // 重置数据序列的状态
                this._NoDataInDocument = true;
                foreach (DocumentData data in this.Datas)
                {
                    foreach (ValuePoint vp in data.Values)
                    {
                        this._NoDataInDocument = false;
                        vp.OwnerList = data.Values;
                        vp.Visible = false;
                        vp.Left = float.NaN;
                        vp.Top = float.NaN;
                        vp.ViewBounds = Rectangle.Empty;
                        vp.HollowCovertFlag = false;
                        vp._OwnerPageIndex = -1;

                        //if (vp.UpAndDown != ValuePointUpAndDown.None)
                        //{
                        //    vp.hasSpecifyUpDownType = true;
                        //}
                        vp.UpAndDown = ValuePointUpAndDown.None;
                    }
                }
                // 获得所有可见的数据轴信息
                UpdateVisibleYAxisInfos();

                //遍历标题行，初始化标题行的运行时使用的开始计算的日期
                TitleLineInfo[] lines = this.HeaderLines.ToArray();
                //foreach (TitleLineInfo line in this.RuntimeHeaderLines)
                for (int index = 0; index < lines.Length; index++)
                {
                    TitleLineInfo line = lines[index];
                    line.RuntimeVisible = line.Visible;
                    if (line.RuntimeVisible == false)
                    {
                        continue;
                    }
                    //如果没有一个valuepoint的时间落到当前标题行的时间区域内，则说明当前页该标题行没有数据点
                    bool _hasValuePointInCurrentPage = false;
                    ValuePointList points = this.GetValuePointsByName(line.Name);
                    foreach (ValuePoint vp in points)
                    {
                        //vp.Left = float.NaN;
                        //vp.Top = float.NaN;
                        //vp.Width = 0;
                        //vp.Height = 0;
                        vp.Parent = line;

                        if (this.RuntimeTicks.Count > 0 &&
                            vp.Time >= this.RuntimeTicks[0].StartTime &&
                            vp.Time < this.RuntimeTicks[this.RuntimeTicks.Count - 1].EndTime)
                        {
                            _hasValuePointInCurrentPage = true; //标记当前页该标题行有数据点
                        }
                    }
                    if (this._ViewMode == DocumentViewMode.Page &&
                        _hasValuePointInCurrentPage == false &&
                        line.VisibleWhenNoValuePoint == false &&
                        line.RuntimeVisible == true)
                    {
                        line.RuntimeVisible = false;
                        continue;
                    }
                    //////////////////////////////////////////////////////////////////////

                    line.Top = 0;
                    line.Height = 0;
                    if (line.ValueType == TitleLineValueType.InDayIndex)
                    {
                        line.ValueType = TitleLineValueType.GlobalDayIndex;
                    }

                    UpdateStartEndDateForDayIndexTitleLine(line, minDate);

                    line.RefreshRuntimeHeight(g.PageUnit);
                    
                }//foreach


                // 清除阴影线条状态
                foreach (YAxisInfo ya in this._VisibleYAxisInfos)
                {
                    ya.ShadowInfo = null;
                    foreach (ValuePoint vp in GetValuePointsByName(ya.Name))
                    {
                        vp.ShadowPoint = null;
                        vp.Parent = ya;
                    }
                }

                TitleLineInfo[] footlines = this.FooterLines.ToArray();
                for (int index = 0; index < footlines.Length; index++)
                //foreach (TitleLineInfo line in this.RuntimeFooterLines)
                {
                    TitleLineInfo line = footlines[index];
                    line.RuntimeVisible = line.Visible;
                    if (line.RuntimeVisible == false)
                    {
                        continue;
                    }

                    UpdateStartEndDateForDayIndexTitleLine(line, minDate);

                    //如果没有一个valuepoint的时间落到当前标题行的时间区域内，则说明当前页该标题行没有数据点
                    bool _hasValuePointInCurrentPage = false;
                    ValuePointList points = this.GetValuePointsByName(line.Name);
                    foreach (ValuePoint vp in points)
                    {
                        vp.Parent = line;

                        if (this.RuntimeTicks.Count > 0 &&
                            vp.Time >= this.RuntimeTicks[0].StartTime &&
                            vp.Time < this.RuntimeTicks[this.RuntimeTicks.Count - 1].EndTime)
                        {
                            _hasValuePointInCurrentPage = true; //标记当前页该标题行有数据点
                        }
                    }
                    if (this._ViewMode == DocumentViewMode.Page &&
                        _hasValuePointInCurrentPage == false &&
                        line.VisibleWhenNoValuePoint == false &&
                        line.RuntimeVisible == true)
                    {
                        line.RuntimeVisible = false;
                        continue;
                    }

                    line.RefreshRuntimeHeight(g.PageUnit);
                }


                //tick2 = CountDown.GetTickCountFloat() - tick2;

                //tick2 = CountDown.GetTickCountFloat();
                // 获得要绘制的Y坐标轴内容
                this._TitleVisibleYAxisInfos = new YAxisInfoList();
                foreach (YAxisInfo ya in this._VisibleYAxisInfos)
                {
                    ya.UpdateRuntimePadding(this);
                    if (string.IsNullOrEmpty(ya.Name) == false)
                    {
                        bool isShadow = false;
                        foreach (YAxisInfo info2 in _VisibleYAxisInfos)
                        {
                            if (ya.Name == info2.ShadowName && ya != info2)
                            {
                                //float tick7 = CountDown.GetTickCountFloat();
                                info2.ShadowInfo = ya;
                                ya.ReverseShadowInfo = info2;
                                // 搜索数据点对应的阴影数据点
                                ValuePointList list1 = GetValuePointsByName(info2.Name);
                                ValuePointList list2 = GetValuePointsByName(ya.Name);
                                int index = 0;
                                foreach (ValuePoint vp in list1)
                                {
                                    for (int iCount = index; iCount < list2.Count; iCount++)
                                    {
                                        ValuePoint vp2 = list2[iCount];
                                        TimeSpan ts = vp.Time.Subtract(vp2.Time);
                                        if (Math.Abs(ts.TotalSeconds) < this.ShadowPointDetectSeconds)
                                        {
                                            // 两个数据点时间差小于检测值，则认为是阴影数据点
                                            vp.ShadowPoint = vp2;
                                            vp2.ReverseShadowPoint = vp;
                                            index = iCount;
                                            break;
                                        }
                                    }//for
                                }//foreach
                                //tick7 = CountDown.GetTickCountFloat() - tick7;
                                isShadow = true;
                                break;
                            }//if
                        }//foreach
                        if (isShadow)
                        {
                            continue;
                        }
                    }//if
                    if (ya.Style == YAxisInfoStyle.Value && ya.TitleVisible )
                    {
                        // 获得数据线，去掉非数据线
                        this._TitleVisibleYAxisInfos.Add(ya);
                    }
                }//foreach
                if (this.InnerBehaviorMode == DocumentBehaviorMode.DesignMode)
                {
                    // 设计模式下无条件的显示所有标尺
                    this._TitleVisibleYAxisInfos = this.YAxisInfos;
                }
                //tickInit = CountDown.GetTickCountFloat() - tickInit;

                // 文本标签的字体
                XFontValue labelFont = CreateFont();

               
                foreach (YAxisInfo info in this.YAxisInfos)
                {
                    info.TitleWidth = 0;
                    info.TitleWidth = 0;
                }
                // 计算Y坐标轴文本占据的宽度
                float totalYAxisWidth = 0;
                if (this._TitleVisibleYAxisInfos.Count > 0)
                {
                    this._TitleVisibleYAxisInfos[0].MergeIntoLeft = false;
                }
                YAxisInfoList handlesInfos = new YAxisInfoList();
                foreach (YAxisInfo info in this._TitleVisibleYAxisInfos)
                {
                    info.FixTopHeightForPadding = false;
                    if (info.SpecifyTitleWidth == 0)
                    {
                        // 自动计算宽度
                        string title = info.Title;
                        if (string.IsNullOrEmpty(title))
                        {
                            title = "HHHH";
                        }
                        SizeF size = g.MeasureString(title, labelFont);
                        if (string.IsNullOrEmpty(info.BottomTitle) == false)
                        {
                            SizeF size2 = g.MeasureString(info.BottomTitle, labelFont);
                            size.Width = Math.Max(size.Width, size2.Width);
                        }
                        info.TitleWidth = size.Width * 1.1f;
                    }
                    else
                    {
                        // 使用指定宽度
                        info.TitleWidth = info.SpecifyTitleWidth;
                    }
                    if (info.MergeIntoLeft )
                    {
                        // 靠左合并了
                        YAxisInfo last = handlesInfos.LastInfo;
                        last.FixTopHeightForPadding = true;
                        info.FixTopHeightForPadding = true;
                        info.TitleLeft = last.TitleLeft;
                        info.MergeInfo = last;
                        float w = Math.Max(info.TitleWidth, last.TitleWidth);
                        last.TitleWidth = w;
                        info.TitleWidth = w;

                    }
                    else
                    {
                        info.TitleLeft = handlesInfos.TotalTitleWidth + this.Left ;
                    }
                    handlesInfos.Add(info);
                }//foreach
                totalYAxisWidth = handlesInfos.TotalTitleWidth;

                //tick2 = CountDown.GetTickCountFloat() - tick2;

                // 根据标题行标题文本的宽度来设置左边缘区域宽度
                List<TitleLineInfo> titleInfos = new List<TitleLineInfo>();
                foreach (TitleLineInfo info in this.RuntimeHeaderLines)
                {
                    titleInfos.Add(info);
                }
                foreach (TitleLineInfo info in this.RuntimeFooterLines)
                {
                    titleInfos.Add(info);
                }
                float leftHeaderWidth = totalYAxisWidth;
                foreach (TitleLineInfo info in titleInfos)
                {
                    if (info.SpecifyTitleWidth <= 0)
                    {
                        string runtimetitle = this.ViewMode == DocumentViewMode.Timeline ? info.Title : info.GetRuntimeTitleByPageIndex(this.PageIndex);
                        if (string.IsNullOrEmpty(runtimetitle) == false)
                        {
                            SizeF size = g.MeasureString(runtimetitle, labelFont);
                            leftHeaderWidth = Math.Max(leftHeaderWidth, size.Width);
                        }
                    }
                    else
                    {
                        leftHeaderWidth = Math.Max(leftHeaderWidth, info.SpecifyTitleWidth);
                    }
                }//foreach

                if (leftHeaderWidth > totalYAxisWidth)
                {
                    // 左标题宽度大于Y轴刻度尺总宽度，则修正Y轴刻度尺的宽度
                    float leftCount = this.Left;

                    foreach (YAxisInfo info in this._TitleVisibleYAxisInfos)
                    {
                        info.TitleLeft = leftCount;
                        info.TitleWidth += (leftHeaderWidth - totalYAxisWidth) / this._TitleVisibleYAxisInfos.HorizontalYAxisCount;
                        if (info.MergeIntoLeft)
                        {
                            // 靠左合并了
                            info.TitleLeft = info.MergeInfo.TitleLeft;
                            info.TitleWidth = info.MergeInfo.TitleWidth;
                        }
                        else
                        {
                            leftCount += info.TitleWidth;
                        }
                    }
                }
                this._LeftHeaderWidth = leftHeaderWidth;
                //tick2 = CountDown.GetTickCountFloat() - tick2;

                this._DataGridBounds = RectangleF.Empty;
                this._DataGridBounds.X = this.Left + leftHeaderWidth;
                this._DataGridBounds.Width = this.Width - leftHeaderWidth;
              
                // 绘制标题行网格竖线
                float dayStep = _DataGridBounds.Width / this.RuntimeNumOfDaysInOnePage;
                if (this.Config.Ticks.Count > 0)
                {
                    this._TickViewWidth = dayStep / this.Config.Ticks.Count;
                }
                else
                {
                    this._TickViewWidth = dayStep;
                }


                // 计算标准数据标题行高度
                this._StdTitleLineHeight = g.GetFontHeight(labelFont) * 1.5f;
                // 初始化页眉标签的高度，默认情况下为一行
                this.HeaderLabels.HeaderLabelMaxHeight = this._StdTitleLineHeight;
                // 数据标题行总高度
                float totalHeaderLineHeight = 0;
                foreach (TitleLineInfo line in this.RuntimeHeaderLines)
                {
                    SetTitleLineHeight(
                        g,
                        line,
                        this._StdTitleLineHeight,
                        this._LayoutStartDate );
                    line.RefreshRuntimeHeight(g.PageUnit);

                    totalHeaderLineHeight += line.RuntimeHeight;
                }//foreach

                //float tick3 = CountDown.GetTickCountFloat();

                // 标题栏高度
                float titleHeight = 0;
                if (this.RuntimeViewMode == DocumentViewMode.Page)
                {
                    if (this.Config.SpecifyTitleHeight > 0)
                    {
                        titleHeight = GraphicsUnitConvert.Convert(
                            this.Config.SpecifyTitleHeight,
                            GraphicsUnit.Document,
                            g.PageUnit);
                    }
                    else
                    {
                        //if (string.IsNullOrEmpty(this.Title) == false)
                        {
                            // 绘制大标题
                            XFontValue titleFont = CreateBigTitleFont();
                            titleHeight = g.GetFontHeight(titleFont) * 1.1f;
                            RectangleF titleTextBounds = new RectangleF(
                                this.Left,
                                this.Top,
                                this.Width,
                                titleHeight);
                            //g.DrawString(
                            //    this.Title,
                            //    titleFont,
                            //    this.ForeBrush,
                            //    titleTextBounds,
                            //    centerFormat);
                        }//if
                    }
                }

                // 绘制标题标签信息
                LayoutHeaderLabels(
                    titleHeight,
                    _StdTitleLineHeight,
                    g,
                    labelFont);

                //titleHeight += _StdTitleLineHeight;
                titleHeight += this.HeaderLabels.HeaderLabelMaxHeight;//这一步会出问题，感觉是整个大框的计算出了问题了。

                //tick3 = CountDown.GetTickCountFloat() - tick3;
                // 页尾行总高度
                float totalFooterLineHeight = 0;
                foreach (TitleLineInfo line in this.RuntimeFooterLines)
                {
                    SetTitleLineHeight(
                        g,
                        line,
                        this._StdTitleLineHeight,
                        this._LayoutStartDate );
                    totalFooterLineHeight += line.RuntimeHeight;
                }//foreach

                //tick3 = CountDown.GetTickCountFloat();
                //this._LeftHeaderWidth = leftHeaderWidth + 1;
                SetRuntimeTicksWidth();
                SetRuntimeTicksWidth(true); 
                // 数据区域网格边界矩形
                this._DataGridBounds.Y = this.Top + titleHeight + this.RuntimeHeaderLines.TotalRuntimeHeight;
                this._DataGridBounds.Height = this.Height - totalHeaderLineHeight - totalFooterLineHeight - titleHeight;

               
                float descriptionHeight = 0;
                string pageIndexText = null;
                if (this.RuntimeViewMode == DocumentViewMode.Page)
                {
                    // 当分页显示模式下，获得最下面显示的文本内容
                    if (string.IsNullOrEmpty(this.Config.FooterDescription) == false)
                    {
                        //如果存在换行符，那么根据行数计算需要的高度
                        if (this.Config.FooterDescription.Contains("\\r\\n"))
                        {
                            int lineCount = this.Config.FooterDescription.Split(new string[] { "\\r\\n" },StringSplitOptions.None).Length;
                            descriptionHeight = _StdTitleLineHeight * lineCount;
                        }
                        else //没有默认一行
                        {
                            descriptionHeight = _StdTitleLineHeight;
                        }
                    }
                    pageIndexText = this.Config.PageIndexText;
                    if (string.IsNullOrEmpty(pageIndexText) == false)
                    {
                        descriptionHeight += _StdTitleLineHeight;
                        pageIndexText = pageIndexText.Replace("[%pageindex%]", Convert.ToString(this.RuntimePageIndex + 1));
                        pageIndexText = pageIndexText.Replace("[%pagecount%]", this.NumOfPages.ToString());
                    }
                    this._DataGridBounds.Height -= descriptionHeight;
                }


                // 计算数据标题行
                float topCount = this.Top + titleHeight;
                for (int lineIndex = 0; lineIndex < this.RuntimeHeaderLines.Count; lineIndex++)
                {
                    TitleLineInfo line = this.RuntimeHeaderLines[lineIndex];
                    line.Top = topCount;
                    line.RefreshRuntimeHeight(g.PageUnit);
                    topCount = topCount + line.RuntimeHeight;

                    //在这里提前对line中的数据点进行布局，代码移植源于DrawTitleLineForText
                    line._ValuePointsForDraw = new List<ValuePoint>();
                    line._TickRectForDraw = new List<RectangleF>();
                    if (line.RuntimeLayoutType == TitleLineLayoutType.Cascade
                        || line.RuntimeLayoutType == TitleLineLayoutType.HorizCascade
                        || line.RuntimeLayoutType == TitleLineLayoutType.Normal
                        || line.RuntimeLayoutType == TitleLineLayoutType.Slant
                        || line.RuntimeLayoutType == TitleLineLayoutType.Slant2
                        || line.RuntimeLayoutType == TitleLineLayoutType.Fraction)
                    {
                        LayoutTitleLineForText(
                            line,
                            this.DataGridBounds,
                            g,
                            new RectangleF(0, 0, 10000, 10000),
                            this._LayoutStartDate);
                    }
                }//for

                //tick3 = CountDown.GetTickCountFloat() - tick3;

                //tick3 = CountDown.GetTickCountFloat();
                foreach (YAxisInfo info in this.Config.YAxisInfos)
                {
                    info.RuntimeTitleVisible = false;
                }
                for (int iCount = 0; iCount < this._TitleVisibleYAxisInfos.Count; iCount++)
                {
                    YAxisInfo info = _TitleVisibleYAxisInfos[iCount];
                    info.TitleTop = this._DataGridBounds.Top;
                    info.TitleHeight = this._DataGridBounds.Height;
                    info.RuntimeTitleVisible = true;
                }//for
                //tick3 = CountDown.GetTickCountFloat() - tick3;

                // 清空状态
                foreach (YAxisInfo info in _VisibleYAxisInfos)
                {
                    info.LastPoint = new PointF(float.NaN, float.NaN);
                    info.LastValuePoint = null;
                }

                //float tickPoint = CountDown.GetTickCountFloat();
                DateTime clipStartDate = DateTime.MinValue;
                DateTime clipEndDate = DateTime.MaxValue;

                //float tickWidth = dataGridBounds.Width / this._RuntimeTicks.Count;// (this.RuntimeNumOfDaysInOnePage * this.HoutTicksLength);
                float textVPWidth = g.MeasureString(
                        "##",
                        labelFont,
                        10000,
                        DCStringFormat.GenericTypographic).Width;
                // 绘制数据点和线
                foreach (YAxisInfo info in this.YAxisInfos)//  _VisibleYAxisInfos)
                {
                    //if (info.ValueVisible == false)
                    //{
                    //    // 数据点不显示
                    //    continue;
                    //}
                    if (info.Style == YAxisInfoStyle.Background)
                    {
                        // 不处理背景样式数值
                        continue;
                    }
                    LayoutYAxisValuePoints(
                        g,
                        this._DataGridBounds,
                        info,
                        this._VisibleYAxisInfos,
                        this._LayoutStartDate);
                }//foreach

                //tickPoint = CountDown.GetTickCountFloat() - tickPoint;

                //float tickFooter = CountDown.GetTickCountFloat();
                if (this.RuntimeFooterLines.Count > 0)
                {
                    // 绘制页脚内容
                    topCount = _DataGridBounds.Bottom;
                    foreach (TitleLineInfo info in this.RuntimeFooterLines)
                    {
                        info.Top = topCount;
                        topCount += info.RuntimeHeight;

                        //在这里提前对line中的数据点进行布局，代码移植源于DrawTitleLineForText
                        info._ValuePointsForDraw = new List<ValuePoint>();
                        info._TickRectForDraw = new List<RectangleF>();
                        if (info.RuntimeLayoutType == TitleLineLayoutType.Cascade
                            || info.RuntimeLayoutType == TitleLineLayoutType.HorizCascade
                            || info.RuntimeLayoutType == TitleLineLayoutType.Normal
                            || info.RuntimeLayoutType == TitleLineLayoutType.Slant
                            || info.RuntimeLayoutType == TitleLineLayoutType.Slant2
                            || info.RuntimeLayoutType == TitleLineLayoutType.Fraction)
                        {
                            LayoutTitleLineForText(
                                info,
                                this.DataGridBounds,
                                g,
                                new RectangleF(0, 0, 10000, 10000),
                                this._LayoutStartDate);
                        }
                    }
                }
                //tickFooter = CountDown.GetTickCountFloat() - tickFooter;

                //float tick6 = CountDown.GetTickCountFloat();
                labelFont.Dispose();
                //tick6 = CountDown.GetTickCountFloat() - tick6;
                //totalTick = CountDown.GetTickCountFloat() - totalTick;
                if (HandlerAfterExecuteLayout != null)
                {
                    HandlerAfterExecuteLayout(this, null);
                }
                return 0;
            }//lock(this)
        }

        /// <summary>
        /// 计算文本类数据标题行中需要绘制的数据点的信息
        /// </summary>
        /// <param name="line"></param>
        /// <param name="dataGridBounds"></param>
        /// <param name="g"></param>
        /// <param name="clipRectangle"></param>
        /// <param name="startDate"></param>
        private void LayoutTitleLineForText(
            TitleLineInfo line,
            RectangleF dataGridBounds,
            DCGraphics g,
            RectangleF clipRectangle,
            DateTime startDate)
        {
            //实验
            RectangleF clipRectangle2 = clipRectangle;
            //RectangleF clipRectangle2 = g.GraphisForMeasure.ClipBounds;
            /////////////////////////////////////////////////////////

            //为避免出现画线错误还是使用均匀刻度
            RuntimeTickInfoList usedRuntimeTicks = line.LayoutType == TitleLineLayoutType.Normal && line.TickStep == 1 ? this._RuntimeTicks : this._DefaultRuntimeTicks;

            int rtDaysInOnePage = this.RuntimeNumOfDaysInOnePage;
            ValuePointList vps = GetValuePointsByName(line.Name);

            //如果Line数据为上下交替以数据文本上下，此时需要优先处理集合，去除空节点，设置上下标识。
            int emptyPoint = 0;
            if (line.UpAndDownTextType == UpAndDownTextType.ShowByText)
            {
                for (int i = 0; i < vps.Count; i++)
                {
                    //存在空点，记录下来不参加排序
                    if (string.IsNullOrEmpty(vps[i].RuntimeText))
                    {
                        emptyPoint++;
                    }

                    ValuePoint vp = vps[i];
                    if ((i - emptyPoint) % 2 == 0)
                    {
                        vp.UpAndDown = vp.hasSpecifyUpDownType == false ? ValuePointUpAndDown.Up : vp.UpAndDown;
                    }
                    else
                    {
                        vp.UpAndDown = vp.hasSpecifyUpDownType == false ? ValuePointUpAndDown.Down : vp.UpAndDown;
                    }

                }
            }
            //float tickSpan = CountDown.GetTickCountFloat();
            int totalTickCount = usedRuntimeTicks.Count;// rtDaysInOnePage * this.Config.Ticks.Count;
            float tickWidth = dataGridBounds.Width / usedRuntimeTicks.Count;
            string[] loopTexts = null;
            if (string.IsNullOrEmpty(line.LoopTextList) == false)
            {
                loopTexts = line.LoopTextList.Split(',');
            }
            int tickStep = 1;
            int pointPosition = -1;

            // 计算遍历的起始序号，能大幅降低大循环的循环次数
            int startTickIndex = usedRuntimeTicks.GetStartDetectIndex(dataGridBounds, clipRectangle2);
            if (line.TickStep > 1)
            {
                int ltCount = 1;
                if (loopTexts != null && loopTexts.Length > 0)
                {
                    ltCount = loopTexts.Length;
                }
                if (line.ValueType == TitleLineValueType.TickText)
                {
                    startTickIndex = startTickIndex - startTickIndex % (line.TickStep * ltCount);
                }
                else
                {
                    float timeStep = line.TickStep;
                    DCTimeUnit stepUnit = this.Config.TickUnit;
                    if (this.Config.TickUnit == DCTimeUnit.Hour)
                    {
                        timeStep = line.TickStep * 24.0f / this.Config.Ticks.Count;
                        if (timeStep == 24)
                        {
                            timeStep = 1;
                            stepUnit = DCTimeUnit.Day;
                        }
                    }
                    if (stepUnit == DCTimeUnit.Day)
                    {
                        // 必须为整天零时开始
                        DateTime dtm = usedRuntimeTicks[startTickIndex].StartTime.Date;
                        for (int iCount = startTickIndex - 1; iCount >= 0; iCount--)
                        {
                            if (usedRuntimeTicks[iCount].StartTime.Day < dtm.Day)
                            {
                                startTickIndex = iCount + 1;
                            }
                        }
                    }
                }
            }

            for (int tickIndex = startTickIndex;
                tickIndex < totalTickCount;
                tickIndex += tickStep)
            {
                if (tickIndex == totalTickCount - 1)
                {

                }
                DateTime nextTime = usedRuntimeTicks[tickIndex].StartTime;

                //伍贻超修改20170703：原备注的代码改成最下面一句，不管刻度是否均匀，tickStep始终均匀增加
                int nextTickIndex = tickIndex + 1;
                if (line.ValueType == TitleLineValueType.TickText && line.TickStep == 1)
                {
                    nextTickIndex = tickIndex + 1;
                }
                else
                {
                    float timeStep = line.TickStep;
                    DCTimeUnit stepUnit = this.Config.TickUnit;
                    if (this.Config.TickUnit == DCTimeUnit.Hour)
                    {
                        timeStep = line.TickStep * 24.0f / this.Config.Ticks.Count;
                        if (timeStep == 24)
                        {
                            timeStep = 1;
                            stepUnit = DCTimeUnit.Day;
                        }
                    }
                    nextTime = TimeLineUtils.AddTime(nextTime, timeStep, stepUnit, true);
                    nextTickIndex = usedRuntimeTicks.GetGlobalTickIndex(nextTime, false, true);
                }
                tickStep = nextTickIndex - tickIndex;
                //tickStep = line.TickStep; 
                //***************************************************************************************

                if (tickStep <= 0)
                {
                    tickStep = 1;
                }
                pointPosition++;
                RectangleF blockBounds = new RectangleF(
                    dataGridBounds.Left + usedRuntimeTicks.GetTickLeft(tickIndex),
                    line.Top,
                    0,
                    line.RuntimeHeight);
                blockBounds.Width = usedRuntimeTicks.GetTickLeft(tickIndex + tickStep) - usedRuntimeTicks.GetTickLeft(tickIndex);
                if (blockBounds.Left > clipRectangle2.Right - 2 && this.ViewMode != DocumentViewMode.Timeline)
                {
                    // 超出网格线右边范围，退出循环
                    //float tickSpan3 = CountDown.GetTickCountFloat() - tickSpan;
                    break;
                }
                blockBounds = FixForDataGridBounds(dataGridBounds, blockBounds);
                if (clipRectangle2.IntersectsWith(blockBounds) == false && this.ViewMode != DocumentViewMode.Timeline)
                {
                    // 不在剪切矩形中，下一个
                    continue;
                }
                //float tickSpan2 = CountDown.GetTickCountFloat() - tickSpan;

                ValuePointList blockPoints = null;



                if (loopTexts != null && loopTexts.Length > 0)
                {
                    // 绘制循环文本
                    if (line.RuntimeLayoutType == TitleLineLayoutType.Cascade
                        || line.RuntimeLayoutType == TitleLineLayoutType.HorizCascade)
                    {
                        // 层叠模式
                        blockPoints = new ValuePointList();
                        foreach (string txt in loopTexts)
                        {
                            ValuePoint vp = new ValuePoint();
                            vp.Text = txt;
                            vp.Parent = line;
                            vp._OriginalBlockBounds = blockBounds;
                            blockPoints.Add(vp);
                        }
                    }
                    else
                    {
                        ValuePoint vp = new ValuePoint();
                        vp.Text = loopTexts[pointPosition % loopTexts.Length];
                        vp.Parent = line;
                        vp._TickIndexForDraw = tickIndex;
                        vp.ViewBounds = blockBounds;
                        vp._OriginalBlockBounds = blockBounds;
                        line._ValuePointsForDraw.Add(vp);

                        continue;
                    }
                }
                else
                {
                    blockPoints = new ValuePointList();
                    // 当前天的序号
                    //float dayIndex = (int)Math.Floor((float)tickIndex / this.Config.Ticks.Count);
                    // 当前天中的时刻序号
                    //int tickIndexInDay = tickIndex % this.Config.Ticks.Count;
                    // 计算开始时间
                    DateTime blockStartDate = usedRuntimeTicks.GetMinDateForDetect(tickIndex);
                    // 计算结束时间
                    DateTime blockEndDate = usedRuntimeTicks.GetMaxDateForDetect(tickIndex + tickStep - 1);

                    if (blockEndDate.Day != blockStartDate.Day)
                    {
                        blockEndDate = blockStartDate.Date.AddDays(1);
                    }
                    //DateTime blockStartDate =  startDate.AddHours(
                    //    dayIndex * 24 + GetMinHourForHourTick(tickIndexInDay));
                    // 计算结束时间
                    //DateTime blockEndDate = startDate.AddHours(
                    //    dayIndex * 24 + GetMinHourForHourTick(tickIndexInDay + line.TickStep));
                    if (vps.Count > 0)
                    {
                        int vpIndex = vps.GetFloorIndexByTime(blockStartDate);
                        if (vpIndex < 0)
                        {
                            vpIndex = 0;
                        }
                        if (vpIndex >= 0)
                        {
                            for (int iCount2 = vpIndex; iCount2 < vps.Count; iCount2++)
                            {
                                ValuePoint vp = vps[iCount2];
                                if (vp.Time >= blockStartDate)
                                {
                                    // 超过了起始时间
                                    if (vp.Time >= blockEndDate)
                                    {
                                        // 超出了结束时间
                                        break;
                                    }
                                    // 命中,输出文本
                                    if (string.IsNullOrEmpty(vp.RuntimeText) == false)
                                    {
                                        blockPoints.Add(vp);
                                        if (line.RuntimeLayoutType == TitleLineLayoutType.Normal)
                                        {
                                            // 普通排版模式，则只显示第一个数据点
                                            break;
                                        }
                                    }
                                }
                            }//for
                        }
                    }
                }
                if (line.RuntimeLayoutType == TitleLineLayoutType.Normal
                    || line.RuntimeLayoutType == TitleLineLayoutType.Fraction)
                {
                    if (blockPoints.Count > 0)
                    {
                        blockPoints[0]._OriginalBlockBounds = blockBounds;
                        blockPoints[0].ViewBounds = blockBounds;
                        blockPoints[0]._TickIndexForDraw = tickIndex;
                        line._ValuePointsForDraw.Add(blockPoints[0]);
                    }
                }
                else if (line.RuntimeLayoutType == TitleLineLayoutType.Slant
                    || line.RuntimeLayoutType == TitleLineLayoutType.Slant2)
                {
                    if (blockPoints.Count > 0)
                    {
                        blockPoints[0]._OriginalBlockBounds = blockBounds;
                        blockPoints[0].ViewBounds = new RectangleF(
                                blockBounds.Left,
                                blockBounds.Top,
                                blockBounds.Width / 2,
                                blockBounds.Height);
                        blockPoints[0]._TickIndexForDraw = 0;
                        line._ValuePointsForDraw.Add(blockPoints[0]);
                    }
                    if (blockPoints.Count > 1)
                    {
                        blockPoints[1]._OriginalBlockBounds = blockBounds;
                        blockPoints[1].ViewBounds = new RectangleF(
                                blockBounds.Left + blockBounds.Width / 2,
                                blockBounds.Top,
                                blockBounds.Width / 2,
                                blockBounds.Height);
                        blockPoints[1]._TickIndexForDraw = 1;
                        line._ValuePointsForDraw.Add(blockPoints[1]);
                    }

                }
                else if (line.RuntimeLayoutType == TitleLineLayoutType.Cascade)
                {
                    if (blockPoints.Count > 0)
                    {
                        // 绘制垂直层叠文本
                        for (int iCount = 0; iCount < blockPoints.Count; iCount++)
                        {
                            blockPoints[iCount]._OriginalBlockBounds = blockBounds;
                            blockPoints[iCount].ViewBounds = new RectangleF(
                                    blockBounds.Left,
                                    blockBounds.Top + iCount * blockBounds.Height / blockPoints.Count,
                                    blockBounds.Width,
                                    blockBounds.Height / blockPoints.Count);
                            blockPoints[iCount]._TickIndexForDraw = tickIndex;
                            line._ValuePointsForDraw.Add(blockPoints[iCount]);
                        }//foreach
                    }
                }
                else if (line.RuntimeLayoutType == TitleLineLayoutType.HorizCascade)
                {
                    if (blockPoints.Count > 0)
                    {
                        // 绘制水平层叠文本
                        for (int iCount = 0; iCount < blockPoints.Count; iCount++)
                        {
                            blockPoints[iCount]._OriginalBlockBounds = blockBounds;
                            blockPoints[iCount].ViewBounds = new RectangleF(
                                    blockBounds.Left + iCount * blockBounds.Width / blockPoints.Count,
                                    blockBounds.Top,
                                    blockBounds.Width / blockPoints.Count,
                                    blockBounds.Height);
                            blockPoints[iCount]._TickIndexForDraw = tickIndex;
                            line._ValuePointsForDraw.Add(blockPoints[iCount]);
                        }//foreach
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////

                RuntimeTickInfo info = usedRuntimeTicks[tickIndex];
                float pos = dataGridBounds.Left + info.Left;
                if ((pos + info.Width < clipRectangle2.Left) && this.ViewMode != DocumentViewMode.Timeline)
                {
                    // 没到达剪切区域
                    continue;
                }
                if ((pos > clipRectangle2.Right) && this.ViewMode != DocumentViewMode.Timeline)
                {
                    // 超出剪切区域，退出
                    break;
                }

                if (pos >= dataGridBounds.Right - 2)
                {
                    break;
                }
                // 计算边界
                RectangleF tickRect = new RectangleF(
                    pos,
                    line.Top,
                    info.Width * line.TickStep,
                    line.RuntimeHeight);
                RuntimeTickInfo tick = usedRuntimeTicks[tickIndex];
                if ((clipRectangle2.IntersectsWith(tickRect) == false) && this.ViewMode != DocumentViewMode.Timeline)
                {

                }
                else
                {
                    line._TickRectForDraw.Add(tickRect);
                }
                ////////////////////////////////////////////////////////////////////////

                if (line.UpAndDownTextType == UpAndDownTextType.ShowByTick)
                {
                    foreach (ValuePoint vp in line._ValuePointsForDraw)
                    {
                        if ((vp._TickIndexForDraw / line.TickStep) % 2 == 0)
                        {
                            vp.UpAndDown = vp.hasSpecifyUpDownType == false ? ValuePointUpAndDown.Up : vp.UpAndDown; //ValuePointUpAndDown.Up;
                        }
                        else
                        {
                            vp.UpAndDown = vp.hasSpecifyUpDownType == false ? ValuePointUpAndDown.Down : vp.UpAndDown; //ValuePointUpAndDown.Down;
                        }
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////
            }//for

            if (line.ForceUpWhenPageFirstPoint == true &&
                line.UpAndDownTextType != UpAndDownTextType.None &&
                line._ValuePointsForDraw.Count > 0 &&
                line._ValuePointsForDraw[0].UpAndDown != ValuePointUpAndDown.Up)
            {
                foreach(ValuePoint vp in line._ValuePointsForDraw)
                {
                    if (vp.UpAndDown == ValuePointUpAndDown.Up && vp.hasSpecifyUpDownType == false)
                    {
                        vp.UpAndDown = ValuePointUpAndDown.Down;
                    }
                    else if (vp.UpAndDown == ValuePointUpAndDown.Down && vp.hasSpecifyUpDownType == false)
                    {
                        vp.UpAndDown = ValuePointUpAndDown.Up;
                    }
                }
            }
            //tickSpan = CountDown.GetTickCountFloat() - tickSpan;
        }

        /// <summary>
        /// 绘制病人基本信息
        /// </summary>
        private void LayoutHeaderLabels(
            float bigTitleHeight,
            float titleLineHeight,
            DCGraphics g,
            XFontValue labelFont)
        {
            this._HeaderLabelBounds = RectangleF.Empty;
            if (this.HeaderLabels == null || this.HeaderLabels.Count == 0)
            {
                // 无任何有效数据
                return;
            }
            System.Collections.Generic.Dictionary<int, List<HeaderLabelInfo>> dir = new Dictionary<int, List<HeaderLabelInfo>>();
            // 计算大小
            foreach (HeaderLabelInfo info in this.HeaderLabels)
            {
                info.OwnerDocument = this;
                info.RefreshSize(g, labelFont);
                info.Height = Math.Max(info.Height, titleLineHeight);

                //this.HeaderLabels.HeaderLabelMaxHeight= Math.Max(this.HeaderLabels.HeaderLabelMaxHeight, info.Height);
                if (dir.ContainsKey(info.GroupIndex) == true)
                {
                    List<HeaderLabelInfo> list = null;
                    if (dir.TryGetValue(info.GroupIndex, out list) == true)
                    {
                        if (list.Contains(info) == false)
                        {
                            list.Add(info);
                        }
                    }
                    else
                    {
                        list = new List<HeaderLabelInfo>();
                        list.Add(info);
                    }
                }
                else
                {
                    List<HeaderLabelInfo> list = new List<HeaderLabelInfo>();
                    list.Add(info);
                    dir.Add(info.GroupIndex, list);
                }
            }
            //排个序先
            List<int> ints = new List<int>();
            foreach(var key in dir.Keys)
            {
                ints.Add(key);
            }
            ints.Sort();
            ///////////////////////////////////////////////////////


            // 进行内容排版
            if (this.RuntimeViewMode == DocumentViewMode.Timeline
                || this.RuntimeViewMode == DocumentViewMode.Normal)
            {
                // 时间轴或普通视图模式
                float leftCount = this.Left;
                foreach (HeaderLabelInfo info in this.HeaderLabels)
                {
                    info.Left = leftCount;
                    info.Top = this.Top + bigTitleHeight;
                    leftCount += info.Width + titleLineHeight;
                }
            }
            else
            {
                float topOffset = 0f;
                for (int i = 0; i < ints.Count; i++)
                {
                    int currentindex = ints[i];
                    List<HeaderLabelInfo> infos = null;
                    if (dir.TryGetValue(currentindex, out infos) == false || infos == null || infos.Count == 0)
                    {
                        continue;
                    }
                    float currentInfosMaxHeight = 0;
                    float itemSpacing = this.Width;
                    foreach (HeaderLabelInfo info in infos)
                    {
                        itemSpacing -= info.Width;
                        currentInfosMaxHeight = Math.Max(currentInfosMaxHeight, info.Height);
                    }
                    if (infos.Count > 1)
                    {
                        itemSpacing = itemSpacing / (infos.Count - 1);
                    }
                    float leftCount = this.Left;
                    foreach (HeaderLabelInfo info in infos)
                    {
                        info.Left = leftCount;
                        info.Top = this.Top + bigTitleHeight + topOffset;
                        leftCount += info.Width + itemSpacing;
                    }

                    topOffset = topOffset + currentInfosMaxHeight;
                }
                this.HeaderLabels.HeaderLabelMaxHeight = Math.Max(this.HeaderLabels.HeaderLabelMaxHeight, topOffset);

            }
        }


        private void LayoutYAxisValuePoints(
            DCGraphicsForTimeLine g,
            RectangleF dataGridBounds,
            YAxisInfo info,
            YAxisInfoList visibleYAxisInfos,
            DateTime startDate)
        {
            //float tickSpan = CountDown.GetTickCountFloat();
            if (info.Style == YAxisInfoStyle.Value
                && string.IsNullOrEmpty( info.HollowCovertTargetName ) == false )
            {
                // 设置空心覆盖标记
                ValuePointList vps = GetValuePointsByName(info.Name);

                string[] covertnames = info.HollowCovertTargetName.Split(',');
                foreach (string infoName in covertnames)
                {
                    //原来的代码
                    //float tick999 = CountDown.GetTickCountFloat();
                    YAxisInfo hollowCovertTargetInfo = visibleYAxisInfos.GetItemByName(infoName);
                    ValuePointList hollowCovertTargetPoints = GetValuePointsByName(infoName); ;
                    if (hollowCovertTargetInfo != null
                        && hollowCovertTargetPoints != null
                        && hollowCovertTargetPoints.Count > 0)
                    {
                        float maxDis = GraphicsUnitConvert.Convert(2, GraphicsUnit.Pixel, this.GraphicsUnit);
                        foreach (ValuePoint vp in vps)
                        {
                            // 设置空心覆盖标记
                            if(vp.HollowCovertFlag == true)
                            {
                                //之前设置过覆盖标记，表示该数据点一定会被绘制成外圈样式，此处就不判断了
                                continue;
                            }

                            vp.HollowCovertFlag = false;
                            //RuntimeTickInfo currentTickInfo = null;
                            //float pointX = dataGridBounds.Left + this._RuntimeTicks.GetValuePointXPosition(
                            //    vp.Time,
                            //    ref currentTickInfo);
                            float pointY = info.GetDisplayY(this, dataGridBounds, vp.Value);
                            if (float.IsNaN(pointY) == false)
                            {
                                // 检查空心覆盖标记
                                ValuePoint nvp = hollowCovertTargetPoints.GetNearestPoint(vp.Time, 1000);
                                if (nvp != null)
                                {
                                    float top2 = hollowCovertTargetInfo.GetDisplayY(this, dataGridBounds, nvp.Value);
                                    if (float.IsNaN(top2) == false)
                                    {
                                        if (Math.Abs(top2 - pointY) < maxDis)
                                        {
                                            vp.HollowCovertFlag = true;
                                            if (vp._ConveredPoint == null)
                                            {
                                                vp._CoincidePoint = nvp;
                                            }
                                            if (nvp._CoincidePoint == null)
                                            {
                                                nvp._ConveredPoint = vp;
                                            }
                                            
                                        }
                                    }
                                }//if
                            }
                        }//foreach
                    }
                }
                //tick999 = CountDown.GetTickCountFloat() - tick999;
            }

            XFontValue labelFont = CreateFontValue(info,true);
            float textVPWidth = g.MeasureString(
                        "##",
                        labelFont,
                        10000,
                        DCStringFormat.GenericTypographic).Width;

            // 挨个绘制数据线
            //int minPointIndex = -1;
            ValuePointList vps1 = GetValuePointsByName(info.Name);
            //int maxPointIndex = vps1.Count - 1;
            //float tick9 = CountDown.GetTickCountFloat();
            //int outofRangeCount = 0;
            int startIndex = -1;
            int endIndex = -1;
            // 绝对的开始序号
            int absStartIndex = -1;
            //float tick99 = CountDown.GetTickCountFloat();
            // 在收缩的时间区域中数据点横向最小间距
            float maxXDis = GraphicsUnitConvert.Convert(2, GraphicsUnit.Pixel, g.PageUnit);
            if (this.RuntimeViewMode == DocumentViewMode.Timeline)
            {
                // 处于时间轴模式，对所有数据点进行排版
                startIndex = 0;
                endIndex = vps1.Count - 1;
            }
            else
            {
                int vpsCount = vps1.Count;
                int loopCount = 0;
                for (int iCount = 0; iCount < vpsCount; iCount++)
                {
                    loopCount++;
                    ValuePoint vp = vps1[iCount];
                    if (vp.Time < startDate)
                    {
                        vp.Visible = false;
                        startIndex = iCount + 1;
                        continue;
                    }
                    vp.Visible = true;
                    if (vp.Time > this._RuntimeTicks.LastTime)
                    {
                        vp.Visible = false;
                        if (startIndex < 0)
                        {
                            break;
                        }
                        if (endIndex < 0)
                        {
                            if (vp.Time > this._RuntimeTicks.EndTime)
                            {
                                endIndex = Math.Max(0, iCount - 1);
                            }
                            else
                            {
                                endIndex = iCount;
                            }
                        }
                        if (startIndex < 0)
                        {
                            startIndex = Math.Max(0, endIndex - 1);
                        }
                        break;
                    }
                    if (absStartIndex < 0)
                    {
                        absStartIndex = iCount;
                    }
                    if (startIndex < 0)
                    {
                        // 找到开始序号
                        startIndex = Math.Max(iCount - 1, 0);
                    }
                }//for
                //tick99 = CountDown.GetTickCountFloat() - tick99;
                //tick99 = CountDown.GetTickCountFloat();
                if (startIndex >= 0 || endIndex >= 0)
                {
                    if (startIndex < 0)
                    {
                        startIndex = 0;
                    }
                    if (endIndex < 0)
                    {
                        endIndex = vps1.Count - 1;
                    }
                    // 扩大范围
                    if (info.Style != YAxisInfoStyle.Text || info.Style == YAxisInfoStyle.TextInsideGrid)
                    {
                        startIndex = Math.Max(startIndex - 10, 0);
                        endIndex = Math.Min(endIndex + 10, vps1.Count - 1);
                    }
                }
            }
            Dictionary<float, float> textLabelTops = new Dictionary<float, float>();
            float maxTextRight = 0;

            //当文本数据点在刻度和Y轴出现重合时能正确的错开位置
            ValuePointList runtimeVps = new ValuePointList();
            for (int iCount = startIndex; iCount >= 0 && iCount <= endIndex; iCount++)
            {
                //float tick11 = CountDown.GetTickCountFloat();
                ValuePoint vp = vps1[iCount];
                int dayIndex = (vp.Time.Date - startDate).Days;

                // 获得时刻点
                //int hourTickIndex = this._RuntimeTicks.GetGlobalTickIndex(vp.Time , true );
                // 计算当前数据点X坐标
                //float pointX =  (float)dataGridBounds.Left + tickWidth * (hourTickIndex + 0.5f);
                RuntimeTickInfo currentTickInfo = null;
                float pointX = dataGridBounds.Left + this._RuntimeTicks.GetValuePointXPosition(
                    vp.Time,
                    ref currentTickInfo,
                    this.Config.EnableDataGridLinearAxisMode);
                vp.Left = pointX;
                runtimeVps.Add(vp);
            }
            runtimeVps.SortByLeftValue();
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            foreach(ValuePoint v in runtimeVps)
            //for (int iCount = startIndex ; iCount >= 0 && iCount <= endIndex ; iCount++)
            {
                //float tick11 = CountDown.GetTickCountFloat();
                ValuePoint vp = v;//vps1[iCount];
                int dayIndex = (vp.Time.Date - startDate).Days;
                 
                // 获得时刻点
                //int hourTickIndex = this._RuntimeTicks.GetGlobalTickIndex(vp.Time , true );
                // 计算当前数据点X坐标
                //float pointX =  (float)dataGridBounds.Left + tickWidth * (hourTickIndex + 0.5f);
                RuntimeTickInfo currentTickInfo = null;

                //float pointX = dataGridBounds.Left + this._RuntimeTicks.GetValuePointXPosition(
                //    vp.Time,
                //    ref currentTickInfo);
                //vp.Left = pointX;
                float pointX = vp.Left;
                ////////////////////////////////////////////////////////////////

                if (info.Style == YAxisInfoStyle.Text || info.Style == YAxisInfoStyle.TextInsideGrid)
                {
                    //wyc20260109:修复DCWRITER_CS-1055
                    if (vp.Time < startDate)
                    {
                        continue;
                    }
                    // 遇到文本类型的数值
                    RuntimeTickInfo tickInfo = null;
                    pointX = this._RuntimeTicks.GetXPositionForLabel(
                        dataGridBounds,
                        vp.Time,
                        ref tickInfo);
                    //pointX = Math.Max(pointX, _DataGridBounds.Left + this.PixelToDocumentUnit( 2 ) );
                    //pointX = Math.Max(maxTextRight, pointX);
                    if (vp.Left > dataGridBounds.Right - 2)
                    {
                        // 超出范围
                        break;
                    }
                    vp.Visible = true;
                    float pixelRate = (float)GraphicsUnitConvert.Convert(1.0, GraphicsUnit.Pixel, g.PageUnit);
                    vp.Left = pointX;// -cs.Width / 2;

                    
                    vp.Top = info.GetDisplayY(this, dataGridBounds, vp.Value);
                    ////////////////////////////////////////////////////////////////////////////
                    foreach (float key in textLabelTops.Keys)
                    {
                        float dis = Math.Abs(key - vp.Left);
                        if (dis < textVPWidth / 2)
                        {
                            vp.Top = Math.Max(textLabelTops[key] + 3, vp.Top);
                            if (vp.Parent is YAxisInfo &&
                                ((YAxisInfo)vp.Parent).SeparatorLineVisible &&
                                vp.RuntimeSeperatorLineVisible == false)
                            {
                                vp.RuntimeSeperatorLineVisible = true;
                            }
                            vp.Left = key;
                            break;
                        }
                    }
                    if (textLabelTops.ContainsKey(vp.Left))
                    {
                        vp.Top = Math.Max(textLabelTops[vp.Left] + 3, vp.Top);
                        if (vp.Parent is YAxisInfo &&
                                ((YAxisInfo)vp.Parent).SeparatorLineVisible &&
                                vp.RuntimeSeperatorLineVisible == false)
                        {
                            vp.RuntimeSeperatorLineVisible = true;
                        }
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////
                    vp.Height = 0;
                    vp.Width = textVPWidth;

                    if(vp.UseAdvVerticalStyle2 && vp.UseAdvVerticalStyle)
                    {
                        string[] txts = vp.Text.Split('^');
                        if (txts.Length > 0)
                        {
                            int max = 0;
                            foreach (string txt in txts)
                            {
                                if (txt.Length > max)
                                {
                                    max = txt.Length;
                                }
                            }
                            float w = g.MeasureString("口",labelFont,10000, DCStringFormat.GenericTypographic).Width;
                            vp.Width = w * max;
                        }
                    }
                    ///////////////////////////////////////////////////////

                    float textTop = vp.Top;
                    if (vp.ImageValue != null && this.Config.ShowIcon)
                    {
                        // 显示文本图标
                        vp.Height = this.ImagePixelHeight * pixelRate;
                        textTop += vp.Height + pixelRate * 3;
                    }
                    if (string.IsNullOrEmpty(vp.Text) == false)
                    {
                        // 计算竖的提示文字
                        XFontValue f2 = CreateFontValue(info,true);
                        if (vp.Font != null)
                        {
                            f2.CopySettings(vp.Font);
                        }
                        SizeF cs = g.MeasureStringForTimeLine(
                            vp.Text,
                            f2,
                            (int)textVPWidth,
                            this.StringFormatForYAxisLabelValue(vp, DCGraphicsForTimeLine.TimeLineRunInLinuxMode));
                        if (info.MaxTextDisplayLength > 0 && cs.Height > info.MaxTextDisplayLength)
                        {
                            // 设置了文本最大显示长度
                            cs.Height = info.MaxTextDisplayLength;
                        }
                        vp.Height += cs.Height;
                        RectangleF textBounds = new RectangleF(
                            vp.Left,
                            textTop,
                            vp.Width,
                            cs.Height);
                        //bool hasPreText = false;
                        if (textLabelTops.ContainsKey(vp.Left))
                        {
                            // 以前出现了相同X坐标值的文本值 
                            //hasPreText = true;
                        }
                        else
                        {
                            textBounds.X = Math.Max(textBounds.Left, maxTextRight + 4 );
                            vp.Left = textBounds.X;
                            maxTextRight = Math.Max( maxTextRight, textBounds.Right );
                        }
                        textLabelTops[vp.Left] = textBounds.Bottom;
                        float maxBottom = dataGridBounds.Bottom;// .Top + dataGridBounds.Height * 0.9f;
                        if (textBounds.Bottom > maxBottom - 2)
                        {
                            textBounds.Height = maxBottom - textBounds.Top - 2;
                        }
                        vp.ViewBounds = new RectangleF(vp.Left, vp.Top, vp.Width, vp.Height);
                        if (textBounds.Height <= 0)
                        {
                            // 高度不够，不显示了。
                            continue;
                        }
                    }
                }//if
                else
                {
                    if (currentTickInfo != null && currentTickInfo.Zone != null && currentTickInfo.Zone.IsExpanded == false)
                    {
                        // 当前时刻所在的时间区域是收缩的，则进行X偏移量最小值判断
                        if (float.IsNaN(info.LastPoint.X) == false)
                        {
                            float dis = pointX - info.LastPoint.X;
                            if (dis < maxXDis)
                            {
                                // 横向偏移量距离太短，不绘制本节点
                                vp.Visible = false;
                                continue;
                            }
                        }
                    }
                    vp.Visible = true;
                    float pointY = info.GetDisplayY(this, dataGridBounds, vp.Value);
                    vp.Top = pointY;
                    if (float.IsNaN(pointY))
                    {
                        // 数据为空
                        info.LastPoint = new PointF(float.NaN, float.NaN);
                        info.LastValuePoint = null;
                        vp.Left = float.NaN;
                        vp.Top = float.NaN;
                    }
                    else
                    {
                      

                        info.LastPoint = new PointF(pointX, pointY);
                        info.LastValuePoint = vp;
                    }
                }
                //tick11 = CountDown.GetTickCountFloat() - tick11;
            }//for
            //tick99 = CountDown.GetTickCountFloat() - tick99;
            //tickSpan = CountDown.GetTickCountFloat() - tickSpan;
        }

      

        [NonSerialized]
        private static DrawStringFormatExt _StringFormatForYAxisLabelValue = null;
        /// <summary>
        /// 绘制坐标网格中文本标签使用的格式化对象
        /// </summary>
        private DrawStringFormatExt StringFormatForYAxisLabelValue(ValuePoint vp, bool linuxmode)
        {
            if (_StringFormatForYAxisLabelValue == null)
            {
                _StringFormatForYAxisLabelValue = DrawStringFormatExt.GenericDefault.Clone();
                _StringFormatForYAxisLabelValue.FormatFlags = StringFormatFlags.NoClip;
                _StringFormatForYAxisLabelValue.Alignment = StringAlignment.Center;
                _StringFormatForYAxisLabelValue.LineAlignment = StringAlignment.Center;
                //_StringFormatForYAxisLabelValue.Trimming = StringTrimming.None;
            }
            _StringFormatForYAxisLabelValue.UseAdvancedDirectionVertical = linuxmode ? true : vp.UseAdvVerticalStyle;// (vp.Parent is YAxisInfo) && (vp.UseAdvVerticalStyle == true) && (((YAxisInfo)vp.Parent).Style == YAxisInfoStyle.Text);           
            if (vp.UseAdvVerticalStyle2 == true)
            {
                _StringFormatForYAxisLabelValue.UseAdvancedDirectionVertical2 = vp.UseAdvVerticalStyle2;
                _StringFormatForYAxisLabelValue.Alignment = StringAlignment.Near;
            }
            return _StringFormatForYAxisLabelValue;
        }

    }
}
