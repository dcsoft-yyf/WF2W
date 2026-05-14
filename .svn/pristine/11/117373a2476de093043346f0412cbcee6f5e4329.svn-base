using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using DCSoft.Common;
using DCSoft.Drawing;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    // 绘制体温单文档的代码
    public partial class TemperatureDocument
    {
        [NonSerialized]
        private bool _PrintingMode = false;
        /// <summary>
        /// 正在打印模式
        /// </summary>
        internal bool PrintingMode
        {
            get
            {
                return _PrintingMode;
            }
            set
            {
                _PrintingMode = value;
            }
        }

        /// <summary>
        /// 获得运行时的前景颜色
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private Color GetRuntimeForeColor(Color c)
        {
            //this.PrintingMode == true && this.Config.BothBlackWhenPrint
            if (this.PrintingMode == true && this.Config.BothBlackWhenPrint)
            {
                return Color.Black;
            }
            else
            {
                return c;
            }
        }

        /// <summary>
        /// 获得运行时的线条粗细
        /// </summary>
        /// <param name="lineWidth"></param>
        /// <returns></returns>
        private float GetRuntimeLineWidth(float lineWidth)
        {
            if (this.PrintingMode == true)
            {
                return lineWidth * this.Config.LineWidthZoomRateWhenPrint;
            }
            else
            {
                return lineWidth;
            }
        }

        /// <summary>
        /// 鼠标悬停的数据点
        /// </summary>
        [NonSerialized]
        private ValuePoint _MouseHoverValuePoint = null;
        /// <summary>
        /// 鼠标悬停的数据点
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        internal ValuePoint MouseHoverValuePoint
        {
            get
            {
                return _MouseHoverValuePoint;
            }
            set
            {
                _MouseHoverValuePoint = value;
            }
        }

        /// <summary>
        /// 数据点是否启用超链接显示模式
        /// </summary>
        /// <param name="vp">数据点</param>
        /// <returns>是否启用超链接显示模式</returns>
        private bool IsUseLinkVisualStyle(ValuePoint vp)
        {
            if (this.Config.LinkVisualStyle == DocumentLinkVisualStyle.None)
            {
                return false;
            }
            if (string.IsNullOrEmpty(vp.Link))
            {
                return false;
            }
            else
            {
                if (this.Config.LinkVisualStyle == DocumentLinkVisualStyle.Hover)
                {
                    return this.MouseHoverValuePoint == vp;
                }
                else if (this.Config.LinkVisualStyle == DocumentLinkVisualStyle.Always)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 最后一次绘制过程中数据点占据的边界
        /// </summary>
        [NonSerialized]
        internal Dictionary<ValuePoint, RectangleF> _LastDrawViewBounds = null;

        [NonSerialized]
        private float _LeftHeaderWidth = 0;
        /// <summary>
        /// 文档左侧标题栏宽度
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        private float LeftHeaderWidth
        {
            get
            {
                return _LeftHeaderWidth;
            }
        }
#if !DCWriterForWASM
        /// <summary>
        /// 文档左侧标题栏像素宽度
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float LeftHeaderPixelWidth
        {
            get
            {
                return GraphicsUnitConvert.Convert(this.LeftHeaderWidth, this.GraphicsUnit, GraphicsUnit.Pixel);
            }
        }
#endif
        [NonSerialized]
        private static DCStringFormat _LineTitleStringFormat = null;

        /// <summary>
        /// 绘制行标题使用的字符格式化对象
        /// </summary>
        internal DCStringFormat LineTitleStringFormat
        {
            get
            {
                if (_LineTitleStringFormat == null)
                {
                    _LineTitleStringFormat = new DCStringFormat();
                    _LineTitleStringFormat.Alignment = StringAlignment.Near;
                    _LineTitleStringFormat.LineAlignment = StringAlignment.Center;
                    _LineTitleStringFormat.FormatFlags = System.Drawing.StringFormatFlags.FitBlackBox
                            | System.Drawing.StringFormatFlags.MeasureTrailingSpaces;
                    AddNoClipForLinux(_LineTitleStringFormat);
                }
                return _LineTitleStringFormat;
            }
        }
#if !DCWriterForWASM
        internal enum DocumentDrawMode
        {
            /// <summary>
            /// 在用户界面上绘制
            /// </summary>
            UIPaint,
            /// <summary>
            /// 打印预览时绘制
            /// </summary>
            PrintPreview,
            /// <summary>
            /// 打印时绘制
            /// </summary>
            Print
        }
#endif
        [NonSerialized]
        private float _TickViewWidth = 0;
#if !DCWriterForWASM
        /// <summary>
        /// 一个最小时间刻度的宽度
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float TickViewWidth
        {
            get
            {
                return _TickViewWidth;
            }
        }
#endif
        /// <summary>
        /// 根据数值比率计算显示的Y坐标值
        /// </summary>
        /// <param name="dataGridBounds">数据网格矩形</param>
        /// <param name="rate">数值比率，从0.0到1.0</param>
        /// <returns>Y坐标值</returns>
        /// <param name="info">数据点信息</param>
        private float GetDispalyYByRate(RectangleF dataGridBounds, float rate, YAxisInfo info)
        {
            if (info == null)
            {
                return dataGridBounds.Top
                    + dataGridBounds.Height * this.Config.DataGridTopPadding
                    + dataGridBounds.Height * (1f - this.Config.DataGridTopPadding - this.Config.DataGridBottomPadding)
                        * rate;
            }
            else
            {
                return dataGridBounds.Top
                    + dataGridBounds.Height * info.RuntimeTopPadding
                    + dataGridBounds.Height * (1f - info.RuntimeTopPadding - info.RuntimeBottomPadding)
                        * rate;
            }
        }

       

        /// <summary>
        /// 绘制体温单
        /// </summary>
        /// <param name="g">画布对象</param>
        /// <param name="clipRectangle">剪切矩形</param>
        /// <param name="party">绘制部分</param>
        public float Draw2(DCGraphicsForTimeLine g, RectangleF clipRectangle, DocumentViewParty party)
        {

            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            g.SmoothingMode = SmoothingMode.HighQuality;
            /////////////////////////////////////////////////////////////////
            CheckLayoutInvalidate(g);
            lock (this)
            {
                g.PageUnit = this.GraphicsUnit;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                //this._LeftHeaderWidth = 0;
                this._LastDrawViewBounds = new Dictionary<ValuePoint, RectangleF>();
                //float tickInit = CountDown.GetTickCountFloat();
                //float tick = CountDown.GetTickCountFloat();
                clipRectangle.Inflate(2, 2);
                // 有数据的时间区间的最大值
                DateTime maxDate = this._LayoutMaxDate;
                // 有数据的时间区间的最小值
                DateTime minDate = this._LayoutStartDate;

                //tick = CountDown.GetTickCountFloat() - tick;

                //float tick2 = CountDown.GetTickCountFloat();

                //tickInit = CountDown.GetTickCountFloat() - tickInit;

                // 文本标签的字体
                XFontValue labelFont = CreateFont();

                var centerFormat = new DCStringFormat();
                centerFormat.Alignment = StringAlignment.Center;
                centerFormat.LineAlignment = StringAlignment.Center;
                centerFormat.FormatFlags = System.Drawing.StringFormatFlags.FitBlackBox
                        | System.Drawing.StringFormatFlags.MeasureTrailingSpaces
                        | StringFormatFlags.NoClip | StringFormatFlags.NoWrap;
                // 计算Y坐标轴文本占据的宽度

                // tick2 = CountDown.GetTickCountFloat() - tick2;

                // 当前区域的开始时间
                DateTime startDate = this._LayoutStartDate;
                // 刷新时间刻度
                //this.RefreshRuntimeTicks(
                //    startDate,
                //    startDate.AddDays(this.RuntimeNumOfDaysInOnePage),
                //    this._ViewMode == DocumentViewMode.Timeline ? 0 : this.RuntimeNumOfDaysInOnePage * this.Config.Ticks.Count);
                // 计算标准数据标题行高度
                float stdTitleLineHeight = g.GetFontHeight(labelFont) * 1.5f;
                // 数据标题行总高度
                float totalHeaderLineHeight = 0;
                foreach (TitleLineInfo line in this.RuntimeHeaderLines)
                {
                    SetTitleLineHeight(
                        g,
                        line,
                        stdTitleLineHeight,
                        startDate);
                    line.RefreshRuntimeHeight(g.PageUnit);
                    totalHeaderLineHeight += line.RuntimeHeight;
                }//foreach

                //float tick3 = CountDown.GetTickCountFloat();

                // 标题栏高度
                float titleHeight = 0;
                if (this.RuntimeViewMode == DocumentViewMode.Page)
                {

                    // 绘制大标题
                    XFontValue titleFont = CreateBigTitleFont();
                    {
                        titleHeight = g.GetFontHeight(titleFont) * 1.1f;
                        if (this.Config.SpecifyTitleHeight > 0)
                        {
                            titleHeight = GraphicsUnitConvert.Convert(this.Config.SpecifyTitleHeight, GraphicsUnit.Document, g.PageUnit);
                        }
                        RectangleF titleTextBounds = new RectangleF(
                            this.Left,
                            this.Top,
                            this.Width,
                            titleHeight);
                        if (string.IsNullOrEmpty(this.Title) == false)
                        {
                            if (clipRectangle.IsEmpty
                                || clipRectangle.IntersectsWith(titleTextBounds))
                            {
                                using (var format = new DCStringFormat(centerFormat)/* DrawStringFormatExt.GenericTypographic.Clone()*/)
                                {
                                    format.Alignment = StringAlignment.Center;
                                    format.LineAlignment = StringAlignment.Near;
                                    //format.FormatFlags = StringFormatFlags.NoWrap;
                                    g.DrawString(
                                        this.Title,
                                        titleFont,
                                        this.RuntimeForeColor,
                                        titleTextBounds,
                                        format);
                                }
                            }
                        }
                    }//using
                }//if

                // 绘制标题标签信息，病人基本信息
                DrawHeaderLabels(titleHeight, stdTitleLineHeight, g, clipRectangle, labelFont);
                titleHeight += this.HeaderLabels.HeaderLabelMaxHeight;

                //tick3 = CountDown.GetTickCountFloat() - tick3;

                //tick3 = CountDown.GetTickCountFloat();

                //RectangleF dataGridBounds = new RectangleF(
                //    this.Left + leftHeaderWidth,
                //    (this.Top + titleHeight + this.HeaderLines.TotalRuntimeHeight ),
                //    this.Width - leftHeaderWidth,
                //    (this.Height - totalHeaderLineHeight - totalFooterLineHeight - titleHeight));
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
                            int lineCount = this.Config.FooterDescription.Split(new string[] { "\\r\\n" }, StringSplitOptions.None).Length;
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
                        descriptionHeight += stdTitleLineHeight;
                        pageIndexText = pageIndexText.Replace("[%pageindex%]", Convert.ToString(this.RuntimePageIndex + 1));
                        pageIndexText = pageIndexText.Replace("[%pagecount%]", this.NumOfPages.ToString());
                    }
                }


                // 绘制数据标题行
                float topCount = this.Top + titleHeight;
                for (int lineIndex = 0; lineIndex < this.RuntimeHeaderLines.Count; lineIndex++)
                {

                    TitleLineInfo line = this.RuntimeHeaderLines[lineIndex];
                    //line.Top = topCount;
                    DrawTitleLine(
                        line,
                        _DataGridBounds,
                        g,
                        clipRectangle,
                        _LeftHeaderWidth,
                        party,
                        startDate,
                        maxDate);
                    topCount = topCount + line.RuntimeHeight;
                }//for

                //tick3 = CountDown.GetTickCountFloat() - tick3;

                //tick3 = CountDown.GetTickCountFloat();
                foreach (YAxisInfo info in this.Config.YAxisInfos)
                {
                    info.RuntimeTitleVisible = false;
                }
                YAxisInfo lastInfo = null;
                YAxisInfo nextInfo = null;
                for (int iCount = 0; iCount < this._TitleVisibleYAxisInfos.Count; iCount++)
                {

                    // 绘制Y刻度标尺
                    YAxisInfo info = _TitleVisibleYAxisInfos[iCount];
                    if (iCount < this._TitleVisibleYAxisInfos.Count - 1)
                    {
                        nextInfo = this._TitleVisibleYAxisInfos[iCount + 1];
                    }
                    else
                    {
                        nextInfo = null;
                    }

                    XFontValue yFont = labelFont.Clone();
                    if (info.Selected)
                    {
                        yFont.Bold = true;
                    }
                    if (info.FixTopHeightForPadding)
                    {
                        float tp = info.TopPadding;
                        if (IsNullValue(tp))
                        {
                            tp = 0;// this.Config.DataGridTopPadding;
                        }
                        if (IsNullValue(tp))
                        {
                            tp = 0;
                        }
                        float bp = info.BottomPadding;
                        if (IsNullValue(bp))
                        {
                            bp = 0;// this.Config.DataGridBottomPadding;
                        }
                        if (IsNullValue(bp))
                        {
                            bp = 0;
                        }
                        float tTop = _DataGridBounds.Top;
                        float tBottom = _DataGridBounds.Bottom;
                        if (info.MergeIntoLeft == false)
                        {
                            tTop = _DataGridBounds.Top;
                        }

                        if (lastInfo == null)
                        {
                            tBottom = _DataGridBounds.Bottom - _DataGridBounds.Height * bp;
                        }
                        else
                        {
                            if (info.MergeIntoLeft)
                            {
                                tTop = lastInfo.TitleBottom;
                            }
                            tBottom = _DataGridBounds.Bottom - _DataGridBounds.Height * bp;
                        }

                        if (nextInfo == null || nextInfo.MergeIntoLeft == false)
                        {
                            tBottom = _DataGridBounds.Bottom;
                        }
                        info.TitleTop = tTop;
                        info.TitleHeight = tBottom - info.TitleTop;
                       
                    }
                    else
                    {
                        info.TitleTop = _DataGridBounds.Top;
                        info.TitleHeight = _DataGridBounds.Height;
                    }
                    lastInfo = info;

                    info.RuntimeTitleVisible = true;
                    RectangleF titleBounds = new RectangleF(
                               info.TitleLeft,
                               info.TitleTop + GraphicsUnitConvert.Convert(2, GraphicsUnit.Pixel, g.PageUnit), //_DataGridBounds.Top 
                               info.TitleWidth,
                               stdTitleLineHeight);
                    Color bc = info.TitleBackColor;
                    if (info.ValueVisible == false)
                    {
                        bc = info.HiddenValueTitleBackColor;
                    }
                    if (bc.A != 0)
                    {
                        // 填充标题背景
                        g.FillRectangle(
                            bc,
                            info.TitleLeft + 3,
                            info.TitleTop + 3,
                            info.TitleWidth - 6,
                            info.TitleHeight - 6);
                    }
                    if (string.IsNullOrEmpty(info.Title) == false)
                    {
                        // 绘制标题文字
                        using (var f2 = new DCStringFormat(centerFormat))
                        {
                            if (info.SpecifyTitleWidth > 0)
                            {
                                // 指定了标尺宽度则自动换行
                                f2.FormatFlags = StringFormatFlags.FitBlackBox
                                            | StringFormatFlags.MeasureTrailingSpaces
                                            | StringFormatFlags.NoClip;
                            }
                            f2.Alignment = StringAlignment.Center;
                            f2.LineAlignment = StringAlignment.Near;
                            // 计算文字大小
                            SizeF size3 = g.MeasureString(
                                info.Title,
                                yFont,
                                (int)titleBounds.Width,
                                f2);
                            titleBounds.Height = size3.Height * 1.05f;
                            g.DrawString(
                                info.Title,
                                yFont,
                                GetRuntimeForeColor(info.TitleColor),
                                titleBounds,
                                f2);
                        }//using
                    }

                    titleBounds.Offset(0, titleBounds.Height);
                    if (info.ShowLegendInRule)
                    {
                        // 绘制图例
                        DrawSymbol(
                            g,
                            clipRectangle,
                            titleBounds.Left + titleBounds.Width / 2,
                            titleBounds.Top + (float)GraphicsUnitConvert.Convert(
                                this.SymbolSize,
                                GraphicsUnit.Document, g.PageUnit) / 2.0f,
                            info.SymbolStyle,
                            info.CharacterForCharSymbolStyle,
                            info.SymbolColor,
                            null,
                            float.NaN,
                            false);
                    }
                    if (string.IsNullOrEmpty(info.BottomTitle) == false)
                    {
                        // 绘制底端标题
                        using (var f2 = new DCStringFormat(centerFormat))
                        {
                            if (info.SpecifyTitleWidth > 0)
                            {
                                // 指定了标尺宽度则自动换行
                                f2.FormatFlags = StringFormatFlags.FitBlackBox
                                            | StringFormatFlags.MeasureTrailingSpaces
                                            | StringFormatFlags.NoClip;
                            }
                            f2.Alignment = StringAlignment.Center;
                            f2.LineAlignment = StringAlignment.Far;
                            RectangleF bttomTitleBounds = new RectangleF(
                                info.TitleLeft,
                                info.TitleBottom - stdTitleLineHeight,// _DataGridBounds.Bottom - stdTitleLineHeight,
                                info.TitleWidth,
                                stdTitleLineHeight);
                            g.DrawString(
                                info.BottomTitle,
                                yFont,
                                GetRuntimeForeColor(info.TitleColor),
                                bttomTitleBounds,
                                f2);
                        }//using
                    }
                    int ysplitNum = info.YSplitNum;
                    if (info.HasScales)
                    {
                        // 调整自定义刻度信息列表
                        ysplitNum = info.Scales.Count - 1;
                        info.Scales.SortByValue();
                        info.Scales.Reverse();
                    }

                    for (int numTick = 0; numTick <= ysplitNum; numTick++)
                    {
                        // 绘制Y轴刻度文本 数据刻尺
                        RectangleF infoRect = new RectangleF(
                            info.TitleLeft,
                            GetDispalyYByRate(_DataGridBounds, numTick / (float)ysplitNum, info),
                            info.TitleWidth,
                            stdTitleLineHeight);
                        float numValue = info.MaxValue -
                            (info.MaxValue - info.MinValue) * numTick / ysplitNum;
                        string text = numValue.ToString();
                        if (string.IsNullOrEmpty(info.TitleValueDispalyFormat) == false)
                        {
                            text = numValue.ToString(info.TitleValueDispalyFormat);
                        }
                        if (info.HasScales)
                        {
                            // 自定义坐标刻度
                            YAxisScaleInfo scale = info.Scales[numTick];
                            numValue = scale.Value;
                            infoRect.Y = GetDispalyYByRate(_DataGridBounds, 1f - scale.ScaleRate, info);
                            if (string.IsNullOrEmpty(scale.Text) == false)
                            {
                                // 自定义刻度文本
                                text = scale.Text;
                            }
                            else
                            {
                                text = numValue.ToString();
                            }
                        }
                        if (info.Style != YAxisInfoStyle.Value)
                        {
                            text = null;
                        }
                        if (numTick == 0)
                        {
                            // 第一行
                            if (info.RuntimeTopPadding <= 0)
                            {
                                // 由于没有空间显示第一个数值，则不显示，跳到下一个
                                continue;
                            }
                            // 向上移动半行
                            infoRect.Offset(0, -stdTitleLineHeight / 2);
                        }
                        else if (numTick == ysplitNum)
                        {
                            // 最后一行
                            if (string.IsNullOrEmpty(info.BottomTitle) == false
                                && info.RuntimeBottomPadding <= 0)
                            {
                                // 由于没有空间显示，则不显示，跳到下一个
                                continue;
                            }
                            if (nextInfo != null && nextInfo.MergeIntoLeft)
                            {
                                continue;
                            }

                            infoRect.Offset(0, -stdTitleLineHeight / 1.5f);
                            
                        }
                        else
                        {
                            // 中间的要向上移动半行
                            infoRect.Offset(0, -stdTitleLineHeight / 2);
                        }
                        if (string.IsNullOrEmpty(text) == false)
                        {
                            // 绘制文本
                            g.DrawString(
                                text,
                                yFont,
                                GetRuntimeForeColor(info.TitleColor),
                                infoRect,
                                centerFormat);
                        }
                    }//for

                    // 绘制数据标尺Y轴竖线
                    if (info.BorderVisible == true)
                    {
                        g.DrawLine(
                            this.ForePen,
                            info.TitleLeft + info.TitleWidth,
                            info.TitleTop,
                            info.TitleLeft + info.TitleWidth,
                            _DataGridBounds.Bottom);
                    }

                    //绘制数据标尺Y轴上边框
                    using (Pen p = new Pen(this.ForePen.Color, GetRuntimeLineWidth(2)))
                    {
                        g.DrawLine(
                            p,
                            info.TitleLeft,
                            info.TitleTop,
                            info.TitleLeft + info.TitleWidth,
                            info.TitleTop);
                    }
                    //绘制数据标尺Y轴下边框
                    //using (Pen p = new Pen(this.ForePen.Color, GetRuntimeLineWidth(2)))
                    {
                        g.DrawLine(
                            this.RuntimeForeColor,
                            GetRuntimeLineWidth(2),
                            info.TitleLeft,
                            _DataGridBounds.Bottom,
                            info.TitleLeft + info.TitleWidth,
                            _DataGridBounds.Bottom);
                    }


                }//for
                //tick3 = CountDown.GetTickCountFloat() - tick3;
                //tick3 = CountDown.GetTickCountFloat();
                // 绘制Y坐标轴区域大边框
                using (Pen p = new Pen(this.ForePen.Color, GetRuntimeLineWidth(2)))
                {
                    g.DrawLine(
                        p,
                        this.Left,
                        _DataGridBounds.Bottom,
                        _DataGridBounds.Left,
                        _DataGridBounds.Bottom);
                }

                //tick3 = CountDown.GetTickCountFloat() - tick3;
                //tick3 = CountDown.GetTickCountFloat();

                // 绘制背景样式的数值
                DrawBackgroundValues(
                    g,
                    clipRectangle,
                    _DataGridBounds,
                    _VisibleYAxisInfos,
                    startDate);
                // 绘制网格线
                DrawGrid2(g, _DataGridBounds, clipRectangle);
                g.DrawLine(
                    Pens.Black,
                    _DataGridBounds.Left,
                    _DataGridBounds.Top,
                    _DataGridBounds.Left,
                    _DataGridBounds.Bottom);
                // 绘制数值区域背景
                DrawValueRangeBackground(
                    g,
                    clipRectangle,
                    _DataGridBounds,
                    _VisibleYAxisInfos,
                    startDate);
                using (Pen p = new Pen(this.ForePen.Color, GetRuntimeLineWidth(2)))
                {
                    g.DrawLine(
                       p,
                       _DataGridBounds.Left,
                       _DataGridBounds.Top,
                       Math.Min(clipRectangle.Right, _DataGridBounds.Right),
                       _DataGridBounds.Top);
                }
                using (Pen p = new Pen(this.ForePen.Color, GetRuntimeLineWidth(2)))
                {
                    g.DrawLine(
                        p,
                        _DataGridBounds.Left,
                        _DataGridBounds.Bottom,
                        Math.Min(clipRectangle.Right, _DataGridBounds.Right),
                        _DataGridBounds.Bottom);
                }
                
                // 绘制背景类型数据的标题
                foreach (YAxisInfo info in _VisibleYAxisInfos)
                {

                    if (info.Style == YAxisInfoStyle.Background
                        && info.Scales != null && info.Scales.Count > 0
                        && string.IsNullOrEmpty(info.Title) == false
                        && info.ValueVisible)
                    {
                        RectangleF tilteBounds = new RectangleF(
                            _DataGridBounds.Left + 3,
                            info.GridViewTop,
                            1000,
                            info.GridViewHeight);
                        g.DrawString(
                            info.Title,
                            labelFont,
                            this.RuntimeForeColor,
                            tilteBounds,
                            StringAlignment.Near,
                            StringAlignment.Center, true);
                        //using (StringFormat format = new StringFormat())
                        //{
                        //    format.Alignment = StringAlignment.Near;
                        //    format.LineAlignment = StringAlignment.Center;
                        //    format.FormatFlags = StringFormatFlags.NoWrap;
                        //    g.DrawString(info.Title, labelFont.Value, this.ForeBrush, tilteBounds, format);
                        //}
                    }
                }

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
                foreach (YAxisInfo info in _VisibleYAxisInfos)
                {
                    if (party == DocumentViewParty.LeftHeader)
                    {
                        // 只显示左侧标题行，则退出循环
                        break;
                    }
                    if (info.ValueVisible == false)
                    {
                        // 数据点不显示
                        continue;
                    }
                    if (info.Style == YAxisInfoStyle.Background)
                    {
                        // 不处理背景样式数值
                        continue;
                    }
                    if (info.Style == YAxisInfoStyle.Text || info.Style == YAxisInfoStyle.TextInsideGrid)
                    {
                        DrawYAxisValuePoints2(
                             g,
                             _DataGridBounds,
                             clipRectangle,
                             info,
                             _VisibleYAxisInfos,
                             startDate);
                    }
                    else
                    {
                        DrawYAxisValuePoints2(
                            g,
                            _DataGridBounds,
                            clipRectangle,
                            info,
                            _VisibleYAxisInfos,
                            startDate);
                    }
                }//foreach
                g.DrawLine(
                    Pens.Black,
                    _DataGridBounds.Left,
                    _DataGridBounds.Top,
                    _DataGridBounds.Left,
                    _DataGridBounds.Bottom);

                

                if (this.RuntimeFooterLines.Count > 0)
                {
                    // 绘制页脚内容
                    //topCount = _DataGridBounds.Bottom;
                    foreach (TitleLineInfo info in this.RuntimeFooterLines)
                    {
                        //info.Top = topCount;
                        //topCount += info.RuntimeHeight;
                        DrawTitleLine(
                            info,
                            _DataGridBounds,
                            g,
                            clipRectangle,
                            _LeftHeaderWidth,
                            party,
                            startDate,
                            maxDate);
                    }
                }
                //tickFooter = CountDown.GetTickCountFloat() - tickFooter;

                //float tick6 = CountDown.GetTickCountFloat();



                // 绘制大边框
                using (Pen p = new Pen(GetRuntimeForeColor( this.Config.ForeColor), GetRuntimeLineWidth(2)))
                {
                    g.DrawRectangle(
                        p,
                        this.Left,
                        this.Top + titleHeight,
                        this.Width,
                        this.Height - titleHeight - descriptionHeight);
                }


                if (descriptionHeight > 0)
                {
                    // 绘制最下面的批注文本
                    using (var format = new DCStringFormat())
                    {
                        AddNoClipForLinux(format);
                        float topCount2 = this.Top + this.Height - descriptionHeight;
                        if (string.IsNullOrEmpty(this.Config.FooterDescription) == false)
                        {
                            // 绘制批注文本
                            format.Alignment = StringAlignment.Near;
                            format.LineAlignment = StringAlignment.Center;
                            format.FormatFlags = StringFormatFlags.NoWrap;
                            //format.Font = labelFont;
                            //format.Color = this.RuntimeForeColor;
                            //如果存在换行符，那么根据行数计算需要的高度
                            if (this.Config.FooterDescription.Contains("\\r\\n"))
                            {
                                string[] descriptionList = this.Config.FooterDescription.Split(new string[] { "\\r\\n" }, StringSplitOptions.None);
                                foreach (string desciption in descriptionList)
                                {
                                   
                                    g.DrawString(
                                        desciption,
                                        labelFont,
                                        this.RuntimeForeColor,
                                        new RectangleF(this.Left, topCount2, this.Width, stdTitleLineHeight),
                                        format);
                                   
                                    topCount2 += stdTitleLineHeight;
                                }

                            }
                            else //没有默认一行
                            {
                                //format.Left = this.Left;
                                //format.Top = topCount2;
                                //format.Width = this.Width;
                                //format.Height = stdTitleLineHeight;
                                g.DrawString(
                                        this.Config.FooterDescription,
                                        labelFont,
                                        this.RuntimeForeColor,
                                        new RectangleF(this.Left, topCount2, this.Width, stdTitleLineHeight),
                                        format);

                               
                                topCount2 += stdTitleLineHeight;
                            }
                        }
                        if (string.IsNullOrEmpty(pageIndexText) == false)
                        {
                            // 绘制页码
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                           
                            g.DrawString(
                                pageIndexText,
                                this.Config.RuntimePageIndexFont,
                                this.RuntimeForeColor,
                                new RectangleF(this.Left, topCount2, this.Width, stdTitleLineHeight),
                                format);
                            
                        }
                    }//using
                }//if
                // 绘制文本标签
                DrawLabels(g, clipRectangle);
                // 绘制图片
                DrawImages(g, clipRectangle);

                if (this.InnerBehaviorMode == DocumentBehaviorMode.DesignMode)
                {
                    // 处于设计模式
                    if (this.SelectedObject != null)
                    {
                        RectangleF sb = GetObjectBounds(this.SelectedObject);
                        if (sb.IsEmpty == false && clipRectangle.IntersectsWith(sb))
                        {
                            using (Pen p2 = new Pen(
                                GetRuntimeForeColor(this.Config.ForeColor),
                                this.PixelToDocumentUnit(2)))
                            {
                                g.DrawRectangle(
                                    p2,
                                    sb.Left,
                                    sb.Top,
                                    sb.Width,
                                    sb.Height);
                            }
                            float pw = this.PixelToDocumentUnit(6);
                            Pen p = DrawerUtil.GetSelectionPen(
                                pw,
                                true);
                            sb.Inflate(-pw / 2, -pw / 2);
                            g.DrawRectangle(p, sb.Left, sb.Top, sb.Width, sb.Height);
                        }
                    }
                }

       

                labelFont.Dispose();
                centerFormat.Dispose();
                //tick6 = CountDown.GetTickCountFloat() - tick6;
#if WINFORM || DCWriterForWinFormNET6

                if (this.EventAfterDrawDocument != null)
                {
                    System.Windows.Forms.PaintEventArgs args = new System.Windows.Forms.PaintEventArgs(
                        g.NativeGraphics,
                        new Rectangle(
                            (int)clipRectangle.X,
                            (int)clipRectangle.Y,
                            (int)clipRectangle.Width,
                            (int)clipRectangle.Height));
                    this.EventAfterDrawDocument(this, args);
                }
#endif
                return 0;
            }//lock(this)           
        }

        private static float _vpleft = 0;
        private void DrawYAxisInsideGridTextPoint(
            DCGraphicsForTimeLine g,
            YAxisInfo info,
            ValuePoint vp)
        {
            float height = this.RuntimeTicksForVerticalDataGrid[0].Width;
            float width = height;
            var rtick = this.RuntimeTicks.SafeGetItem(vp._TickIndexForDraw);
            if (rtick != null)
            {
                width = rtick.Width;
            }
            float useRectLength = Math.Min(height, width);

            XFontValue f2 = CreateFontValue(info, true);
            if (vp.Font != null)
            {
                f2.CopySettings(vp.Font);
            }
            while(f2.GetHeight(g) > useRectLength)
            {
                f2.Size = f2.Size - 0.1f;
            }

            Color txtColor = info.SymbolColor;
            if (this.PrintingMode == false && vp.TextColor != info.SymbolColor && vp.TextColor.A != 0)
            {
                txtColor = vp.TextColor;
            }

            using (var format = new DCStringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                int index = -1; //this.RuntimeTicksForVerticalDataGrid.index(vp.Top);
                for (int j = this.RuntimeTicksForVerticalDataGrid.Count - 1; j >= 0; j--)
                {
                    if (this.RuntimeTicksForVerticalDataGrid[j].Left <= vp.Top)
                    {
                        index = this.RuntimeTicksForVerticalDataGrid[j].Index;
                        break;
                    }
                }

                if (vp.Left != _vpleft)
                {
                    _vpleft = vp.Left;
                    foreach( var tick in this.RuntimeTicksForVerticalDataGrid)
                    {
                        tick.FirstTickInDate = false;
                    }
                }
                ////////////////////////////////////////

                for (int i = 0; i < vp.Text.Length; i++)
                {
                    int ii = index + i;
                    if (ii >= this.RuntimeTicksForVerticalDataGrid.Count)
                    {
                        break;
                    }

                    //如果数据点上下有重合，在此尽量错开
                    int offset = 0;
                    RuntimeTickInfo starttick = this.RuntimeTicksForVerticalDataGrid.SafeGetItem(ii + offset);
                    while (starttick != null && starttick.FirstTickInDate == true)
                    {
                        offset++;
                        starttick = this.RuntimeTicksForVerticalDataGrid.SafeGetItem(ii + offset);
                    }
                    if (starttick == null)
                    {
                        break;
                    }

                    string s = vp.Text[i].ToString();
                    float top = this.RuntimeTicksForVerticalDataGrid.GetTickLeft(ii + offset);
                    RectangleF rectf = new RectangleF(
                        vp.Left + (Math.Abs(width - useRectLength)) / 2,
                        top + (Math.Abs(height - useRectLength)) / 2,
                        width,
                        height);
                    g.DrawString(
                        s, 
                        f2, 
                        GetRuntimeForeColor(txtColor), 
                        rectf, 
                        format);
                    //使用FirstTickInDate属性临时记录该格子里是否有数据
                    starttick.FirstTickInDate = true;
                }
            }
            f2.Dispose();
        }

        

        private void DrawYAxisValuePoints2(
            DCGraphicsForTimeLine g,
            RectangleF dataGridBounds,
            RectangleF clipRectangle,
            YAxisInfo info,
            YAxisInfoList visibleYAxisInfos,
            DateTime startDate)
        {



            XFontValue labelFont = CreateFontValue(info, true);
            float textVPWidth = g.MeasureString(
                        "##",
                        labelFont,
                        10000,
                        DCStringFormat.GenericTypographic).Width;
            if (TemperatureDocument.IsNullValue(info.RedLineValue) == false)
            {
                // 存在水平红线
                float redY = info.GetDisplayY(this, dataGridBounds, info.RedLineValue);
                RectangleF rect4 = RectangleF.Intersect(dataGridBounds, clipRectangle);
                if (rect4.IsEmpty == false)
                {
                    if (this.PrintingMode == true && info.RedLinePrintVisible == false)
                    {

                    }
                    else
                    {
                        using (Pen p = new Pen(GetRuntimeForeColor(info.AlertLineColor), GetRuntimeLineWidth(info.RedLineWidth)))
                        {
                            g.DrawLine(
                                p,
                                rect4.Left,
                                redY,
                                rect4.Right,
                                redY);
                        }
                    }
                }
            }//if

            // 挨个绘制数据线
            //int minPointIndex = -1;
            ValuePointList vps1 = GetValuePointsByName(info.Name);
            //int maxPointIndex = vps1.Count - 1;
            //float tick9 = CountDown.GetTickCountFloat();
            //int outofRangeCount = 0;
            int startIndex = -1;
            int endIndex = -1;
            RectangleF clipRectangleDataGrid = RectangleF.Intersect(clipRectangle, dataGridBounds);
            // 绝对的开始序号
            int absStartIndex = -1;
            // float tick99 = CountDown.GetTickCountFloat();
            // 在收缩的时间区域中数据点横向最小间距
            float maxXDis = GraphicsUnitConvert.Convert(2, GraphicsUnit.Pixel, g.PageUnit);
            // 计算开始需要绘制数据点的大概序号，能大幅降低大循环的循环次数
            int detectTickIndex = this._RuntimeTicks.GetStartDetectIndex(dataGridBounds, clipRectangle);
            DateTime detectStartTime = this._RuntimeTicks[detectTickIndex].StartTime;
            int detectVpIndex = vps1.GetFloorIndexByTime(detectStartTime);
            detectVpIndex = Math.Max(0, detectVpIndex - 5);
            float infoRuntimeSymbolSize = info.RuntimeSymbolSize;
            for (int iCount = detectVpIndex; iCount < vps1.Count; iCount++)
            {
                ValuePoint vp = vps1[iCount];
                if (vp.Time < startDate)
                {
                    continue;
                }

                //添加一个判断
                if (vp.Time >= this._RuntimeTicks.EndTime && vp.Parent is YAxisInfo && (((YAxisInfo)vp.Parent).Style == YAxisInfoStyle.Text || ((YAxisInfo)vp.Parent).Style == YAxisInfoStyle.TextInsideGrid))
                {
                    continue;
                }

                if (vp.Time > this._RuntimeTicks.LastTime)
                {
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
                //// 获得时刻点
                //int hourTickIndex = GetHourTickIndex(vp.Time);
                //// 计算当前数据点X坐标
                //float pointX = (float)
                //    dataGridBounds.Left + tickWidth * (dayIndex * this.HoutTicksLength + hourTickIndex + 0.5f);
                // 获得时刻点
                //int hourTickIndex = this._RuntimeTicks.GetGlobalTickIndex(vp.Time , true );
                // 计算当前数据点X坐标
                float pointX = vp.Left;
                float pointY = vp.Top;
                bool match = pointX >= clipRectangleDataGrid.Left - infoRuntimeSymbolSize;
                if (info.Style == YAxisInfoStyle.Text || info.Style == YAxisInfoStyle.TextInsideGrid)
                {
                    match = pointX + textVPWidth >= clipRectangleDataGrid.Left;
                }

                if (match)
                {
                    if (startIndex < 0)
                    {
                        // 找到开始序号
                        startIndex = Math.Max(iCount - 1, 0);
                        if (info.ShadowInfo != null)
                        {
                            // 向前查找阴影起始位置
                            bool hasShadow = false;
                            int shadowCount = 0;
                            for (int iCount2 = startIndex; iCount2 >= 0; iCount2--)
                            {
                                if (vps1[iCount2].ShowShadowPoint)
                                {
                                    startIndex = iCount2;
                                    hasShadow = true;
                                    if (shadowCount++ > 4)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    if (vps1[iCount2].IsNullValue)
                                    {
                                        break;
                                    }
                                    if (iCount2 != startIndex)
                                    {
                                        startIndex = iCount2;
                                        break;
                                    }
                                    if (hasShadow)
                                    {
                                        hasShadow = false;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }//for
                        }
                    }
                    if (info.AllowInterrupt == false)
                    {
                        //  如果不允许中断数据线，则向前查找不为空的数据点
                        for (int iCount2 = iCount - 1; iCount2 >= 0; iCount2--)
                        {
                            ValuePoint vp4 = vps1[iCount2];
                            if (TemperatureDocument.IsNullValue(vp4.Value) == false)
                            {
                                startIndex = Math.Min(startIndex, iCount2);
                                break;
                            }
                        }
                    }

                }
                if (pointX > clipRectangleDataGrid.Right)
                {
                    // 超出范围,找到结束点位置
                    endIndex = iCount;
                    if (info.ShadowInfo != null)
                    {
                        // 向后查找没有阴影点的数据点
                        for (int iCount2 = endIndex; iCount2 < vps1.Count; iCount2++)
                        {
                            if (vps1[iCount2].ShadowPoint == null)
                            {
                                endIndex = iCount2;
                                break;
                            }
                            if (iCount2 - endIndex > 4)
                            {
                                endIndex = iCount2;
                                break;
                            }
                        }//for
                    }
                    if (endIndex < vps1.Count - 1)
                    {
                        endIndex++;
                    }
                    break;
                }//if
            }//for
            //tick99 = CountDown.GetTickCountFloat() - tick99;
            if (startIndex < 0)
            {
                // 没有任何要显示的数据，处理下一条折线
                return;
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }
            if (startIndex < absStartIndex)
            {
                startIndex = absStartIndex;
            }
            if (endIndex < 0)
            {
                endIndex = vps1.Count - 1;
            }


            //float tick88 = CountDown.GetTickCountFloat();
            Dictionary<float, float> textLabelTops = new Dictionary<float, float>();
            info.LastPoint = new PointF(float.NaN, float.NaN);
            info.LastValuePoint = null;
            // 最后一个数据点是否为高亮度模式
            bool lastPointHighlight = false;

            //
            List<ValuePoint> delayDrawedValuePoints = new List<ValuePoint>();

            for (int iCount = startIndex; iCount <= endIndex; iCount++)
            {
                //float tick11 = CountDown.GetTickCountFloat();
                ValuePoint vp = vps1[iCount];

                if (vp.Time < startDate || vp.Time >= this._RuntimeTicks.EndTime)
                {
                    continue;
                }
                //**********************************************************
                if (TemperatureDocument.IsNullValue(vp.Left)
                    || TemperatureDocument.IsNullValue(vp.Top))
                {
                    // 遇到数值为空
                    if (info.Style == YAxisInfoStyle.Value)
                    {
                        if (info.AllowInterrupt == false)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                int dayIndex = (vp.Time.Date - startDate).Days;
                // 获得时刻点
                //int hourTickIndex = this._RuntimeTicks.GetGlobalTickIndex(vp.Time , true );
                // 计算当前数据点X坐标
                //float pointX =  (float)dataGridBounds.Left + tickWidth * (hourTickIndex + 0.5f);
                float pointX = vp.Left;
                if (info.Style == YAxisInfoStyle.TextInsideGrid && 
                    this.RuntimeTicksForVerticalDataGrid != null && 
                    this.RuntimeTicksForVerticalDataGrid.Count > 0 &&
                    string.IsNullOrEmpty(vp.Text) == false)
                {
                    DrawYAxisInsideGridTextPoint(g, info, vp);
                }
                else
                //////////////////////////////
                if (info.Style == YAxisInfoStyle.Text || 
                    (info.Style == YAxisInfoStyle.TextInsideGrid && (this.RuntimeTicksForVerticalDataGrid == null || this.RuntimeTicksForVerticalDataGrid.Count == 0)))
                {
                    // 遇到文本类型的数值
                    if (pointX > dataGridBounds.Right - 2)
                    {
                        // 超出范围
                        break;
                    }
                    float pixelRate = (float)GraphicsUnitConvert.Convert(1.0, GraphicsUnit.Pixel, g.PageUnit);
                    float textTop = vp.Top;
                    if (vp.ImageValue != null && this.Config.ShowIcon)
                    {
                        // 显示文本图标
                        textTop += vp.Height + pixelRate * 3;
                        DrawIcon(g, vp.ImageValue, vp.Left, vp.Top);
                    }
                    if (string.IsNullOrEmpty(vp.Text) == false)
                    {
                        // 绘制竖的提示文字
                        using (var format = this.StringFormatForYAxisLabelValue(vp, DCGraphicsForTimeLine.TimeLineRunInLinuxMode).Clone())// DrawStringFormatExt.GenericDefault.Clone())// new StringFormat(StringFormat.GenericDefault))
                        {
                            //format.FormatFlags = StringFormatFlags.DirectionVertical | StringFormatFlags.NoWrap;
                            //format.UseAdvancedDirectionVertical = vp.UseAdvVerticalStyle;
                            //format.Alignment = StringAlignment.Center;
                            //format.LineAlignment = StringAlignment.Center;
                            //format.FormatFlags = StringFormatFlags.NoClip;
                            //format.Trimming = StringTrimming.None;

                            //info.Font = new XFontValue("宋体",7);
                            XFontValue f2 = CreateFontValue(info, true);
                            if (vp.Font != null)
                            {
                                f2.CopySettings(vp.Font);
                            }
                            RectangleF textBounds = new RectangleF(0, 0, 0, 0);
                            #region lidong 注释
                            //ValuePointList vplist = GetValuePointsByName(info.Name);
                            //int xNum = this.RuntimeNumOfDaysInOnePage * this.Config.Ticks.Count;
                            //int yNum = (this.GridYSplitNum+2) * 5;
                            //float CellX = dataGridBounds.Width / xNum;
                            //float CellY = dataGridBounds.Height / yNum;
                            //for (int i = 0; i < vplist.Count; i++)
                            //{
                            //    string text = vplist[i].Text;
                            //    List<string> listNum = new List<string>();
                            //    for (int a = 0; a < text.Length; a++)
                            //    {
                            //        string num = text.Substring(a, 1);
                            //        listNum.Add(num);
                            //    }
                            //    //textBounds = new RectangleF(
                            //    //    vp.Left + 3,
                            //    //    textTop + vp.ValueTextTopPadding ,
                            //    //    vp.Width,
                            //    //    vp.Top + vp.Height - textTop);
                            //    for (int j = 0; j < listNum.Count; j++)
                            //    {
                            //        textBounds = new RectangleF(
                            //        vp.Left + 3,
                            //        textTop + vp.ValueTextTopPadding + CellY * j,
                            //        vp.Width,
                            //        vp.Top + vp.Height - textTop + CellY * j);

                            //    }
                            // }
                            #endregion

                            //if (this.ViewMode == DocumentViewMode.Page)
                            //{

                            //RuntimeTickInfo tickinfo = null;
                            //float left = this.RuntimeTicks.GetXPositionForLabel(
                            //    dataGridBounds,
                            //    vp.Time,
                            //    ref tickinfo);
                            //float X = this.RuntimeTicks.GetTickLeft(tickinfo.Index + 1) - this.RuntimeTicks.GetTickLeft(tickinfo.Index);
                            //left = X <= vp.Width ? 0f : (X - vp.Width) / 2;
                            textBounds = new RectangleF(
                                vp.Left,
                                textTop + vp.ValueTextTopPadding,
                                vp.Width,
                                vp.Top + vp.Height - textTop);

                            //bool hasPreText = false;
                            //if (textLabelTops.ContainsKey(vp.Left))
                            //{
                            //    // 以前出现了相同X坐标值的文本值 
                            //    hasPreText = true;
                            //}
                            textLabelTops[vp.Left] = textBounds.Bottom;
                            float maxBottom = dataGridBounds.Bottom;// .Top + dataGridBounds.Height * 0.9f;
                            if (textBounds.Bottom > maxBottom - 2)
                            {
                                textBounds.Height = maxBottom - textBounds.Top - 2;
                            }
                            if (textBounds.Height <= 0)
                            {
                                // 高度不够，不显示了。
                                continue;
                            }
                            if (info.ValueTextBackColor.A != 0)
                            {
                                //try
                                //{
                                //竖行文字背景
                                g.FillRectangle(
                                    info.ValueTextBackColor,
                                    textBounds);
                                //}
                                //catch (Exception ext)
                                //{
                                //    System.Windows.Forms.MessageBox.Show(info.Name + " " + vps1.IndexOf(vp).ToString() + ext.ToString());
                                //}
                            }
                            //if (hasPreText)
                            {
                                //add by lidong 20160531大连医卫的需求,当两个竖行文本出现相同X坐标的时,控制中间的分割线是否显示
                                if (vp.RuntimeSeperatorLineVisible == true)
                                {
                                    g.DrawLine(
                                        GetRuntimeForeColor(this.Config.ForeColor),
                                        textBounds.Left,
                                        textBounds.Top,
                                        textBounds.Right,
                                        textBounds.Top);
                                }
                            }

                            Color txtColor = info.SymbolColor;
                            if (this.PrintingMode == false && vp != null && vp.Color != info.SymbolColor && vp.Color != Color.Transparent)
                            {
                                txtColor = vp.Color;
                            }

                            if (this.IsUseLinkVisualStyle(vp))
                            {

                                // 鼠标悬停的超链接
                                txtColor = Color.Blue;
                                g.DrawLine(
                                    Pens.Blue,
                                    textBounds.Left,
                                    textBounds.Top,
                                    textBounds.Left,
                                    textBounds.Bottom);
                                g.DrawLine(
                                    Pens.Blue,
                                    textBounds.Right - 1,
                                    textBounds.Top,
                                    textBounds.Right - 1,
                                    textBounds.Bottom);
                            }
                            //2015.10.12新加一个try catch是为了防止在设计器模式下 分页模式textBounds溢出问题
                            try
                            {
                               
                                g.DrawStringForTimeLine(vp.Text,
                                        f2,
                                        GetRuntimeForeColor(txtColor),
                                        textBounds,
                                        format);

                            }
                            catch
                            { }

                            //g.DrawRectangle(Pens.Red, textBounds.Left, textBounds.Top, textBounds.Width, textBounds.Height);
                        }//using
                    }
                    if (this._LastDrawViewBounds != null && vp.Height > 0)
                    {
                        this._LastDrawViewBounds[vp] = vp.ViewBounds;
                    }
                }//if
                else  //开始绘制数据区域的数值型数据点
                {
                    if (vp.Visible == false)
                    {
                        // 数据点不可见
                        continue;
                    }

                    float pointY = vp.Top;
                    if (float.IsNaN(pointY))
                    {
                        // 数据为空
                        if (info.AllowInterrupt || 
                            info.LastValuePoint == null || info.LastValuePoint.LineStop == DCTimeLineBooleanValue.True)
                        {
                            // 允许数据点中断
                            info.LastPoint = new PointF(float.NaN, float.NaN);
                            info.LastValuePoint = null;
                            lastPointHighlight = false;
                        }
                    }
                    else
                    {
                        //PointF point = new PointF(pointX , pointY );
                        // 超出正常数值范围，需要高亮度显示
                        bool pointHighlight = info.HighlightOutofNormalRange && info.AbNormalRangeSettings != null
                            && DCTimeLineUtils.IsOutofRange(vp.Value, info.AbNormalRangeSettings.NormalMaxValue, info.AbNormalRangeSettings.NormalMinValue);
                        //float tick78 = CountDown.GetTickCountFloat();
                        if (float.IsNaN(info.LastPoint.X) == false)
                        {
                            // 若上一个数据点存在则绘制连线
                            Color pc = info.ReverseShadowInfo != null ? info.ReverseShadowInfo.SymbolColor : info.SymbolColor;
                            if (vp.Color.A != 0)
                            {
                                pc = vp.Color;
                            }
                            else if (info.LastValuePoint != null
                                && info.LastValuePoint.Color.A != 0)
                            {
                                pc = info.LastValuePoint.Color;
                            }
                            SmoothingMode back = g.SmoothingMode;
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            float runtimeLineWidth = GetRuntimeLineWidth(info.RuntimeLineWidth);
                            if (pointHighlight || lastPointHighlight)
                            {
                                runtimeLineWidth = (float)Math.Ceiling(runtimeLineWidth * 1.5f);
                            }

                            if (info.LastValuePoint == null || info.LastValuePoint.LineStop == DCTimeLineBooleanValue.True)
                            {

                            }
                            else
                            {
                                using (Pen p2 = new Pen(GetRuntimeForeColor(pc), GetRuntimeLineWidth(runtimeLineWidth)))
                                {
                                    //if( info.LastPoint.X < 0 || info.LastPoint.Y < 0 || pointX < 0 || pointY < 0 )
                                    //{

                                    //}


                                    g.DrawLine(
                                        p2,
                                        info.LastPoint,
                                        new PointF(pointX, pointY));
                                }
                            }
                            //g.DrawLine(
                            //    GraphicsObjectBuffer.GetPen(pc),
                            //    info.LastPoint,
                            //    new PointF(pointX, pointY));
                            g.SmoothingMode = back;
                        }
                        // 绘制数据点符号
                        float runtimeSymbolSize = info.RuntimeSymbolSize;
                        if (pointHighlight)
                        {
                            runtimeSymbolSize = runtimeSymbolSize * 1.5f;
                        }

                        Color infoSymbolColor = info.ReverseShadowInfo == null ? info.SymbolColor : info.ReverseShadowInfo.SymbolColor;

                        ValuePointSymbolStyle symbolStyle = vp.HollowCovertFlag ? ValuePointSymbolStyle.HollowCicle : info.SymbolStyle;
                        float runtimesymSize = vp.HollowCovertFlag ? runtimeSymbolSize * 1.5f : runtimeSymbolSize;
                       
                        if ((info.SymbolStyle == ValuePointSymbolStyle.OpaqueHollowCicle && vp != null && vp.SpecifySymbolStyle == ValuePointSymbolStyle.Default) ||
                            (vp != null && vp.SpecifySymbolStyle == ValuePointSymbolStyle.OpaqueHollowCicle))
                        {
                            delayDrawedValuePoints.Add(vp);
                        }
                        else
                        {
                            //原来的代码
                            DrawSymbol(
                                g,
                                clipRectangle,
                                pointX,
                                pointY,
                                symbolStyle,
                                info.CharacterForCharSymbolStyle,
                                infoSymbolColor, //info.SymbolColor,
                                vp,
                                runtimesymSize,
                                false);
                        }
                        /////////////////////////////////////

                        if (vp.OutofRangeFlag == 1 && vp.SpecifySymbolStyle != ValuePointSymbolStyle.Custom)
                        {
                            DrawValueArrow(g, pointX, pointY, true, vp, info.ValueTextBackColor);
                        }
                        else if (vp.OutofRangeFlag == -1 && vp.SpecifySymbolStyle != ValuePointSymbolStyle.Custom)
                        {
                            DrawValueArrow(g, pointX, pointY, false, vp, info.ValueTextBackColor);
                        }
                        //tick78 = CountDown.GetTickCountFloat() - tick78;

                        info.LastPoint = new PointF(pointX, pointY);
                        info.LastValuePoint = vp;
                        lastPointHighlight = pointHighlight;

                        //是否核实 
                        if (vp.Verified == true)
                        {
                            DrawSymbol(
                                g,
                                clipRectangle,
                                vp.Left,
                                vp.Top - 25,
                                ValuePointSymbolStyle.V,
                                info.CharacterForCharSymbolStyle,
                                Color.Black,
                                vp,
                                runtimeSymbolSize,
                                false,
                                true);
                        }

                        if ((info.ShowPointValue == true && vp.ShowPointValue != DCTimeLineBooleanValue.False) ||
                            vp.ShowPointValue == DCTimeLineBooleanValue.True)
                        {
                            Color tempcolor = info.ColorForPointValue;
                            if (info.AbNormalRangeSettings != null && vp.Value > info.AbNormalRangeSettings.NormalMaxValue)
                            {
                                tempcolor = info.ColorForUpValue;
                            }
                            else if (info.AbNormalRangeSettings != null && vp.Value < info.AbNormalRangeSettings.NormalMinValue)
                            {
                                tempcolor = info.ColorForDownValue;
                            }
                            string txt = (vp.Text != null && vp.Text.Length > 0) ? vp.Text : vp.ValueString;
                            g.DrawString(
                                txt,
                                CreateFontValue(info, true),
                                tempcolor,
                                vp.ViewBounds.Right,
                                vp.ViewBounds.Top - 25);
                        }

                        if (info.EnableLanternValue && IsNullValue(vp.LanternValue) == false)
                        {
                            // 挂灯笼了
                            float pointY2 = info.GetDisplayY(this, dataGridBounds, vp.LanternValue);
                            Color c2 = Color.Red;
                            if (pointY2 < pointY)
                            {
                                // 灯笼在上，用蓝色
                                //c2 =GetRuntimeForeColor(Color.Blue);
                                c2 = GetRuntimeForeColor(info.LanternValueColorForUp);
                            }
                            else
                            {
                                // 灯笼在下，用红色
                                //c2 = GetRuntimeForeColor(Color.Red);
                                c2 = GetRuntimeForeColor(info.LanternValueColorForDown);
                            }
                            DrawSymbol(
                                g,
                                clipRectangle,
                                pointX,
                                pointY2,
                                info.SpecifyLanternSymbolStyle,
                                info.CharacterForLanternSymbolStyle,
                                c2,
                                vp,
                                runtimeSymbolSize,
                                true);
                            using (Pen p2 = new Pen(c2, GetRuntimeLineWidth(2)))
                            {
                                p2.DashStyle = info.LineStyleForLanternValue; 
                                g.DrawLine(p2, pointX, pointY, pointX, pointY2);
                            }
                        }
                    }
                    if (info.ShadowInfo != null && vp.ShadowPoint != null)
                    {
                        // 绘制阴影线条
                        if (vp.ShowShadowPoint)
                        {
                            // 两个点的Y坐标存在一定的偏差，可以认为没有重合，需要分叉绘制
                            pointY = vp.ShadowPoint.Top;
                            if (float.IsNaN(pointY))
                            {
                                vp.ShadowPoint = null;
                                info.ShadowInfo.LastPoint = new PointF(float.NaN, float.NaN);
                                info.ShadowInfo.LastValuePoint = null;
                            }
                            else
                            {
                                vp.ShadowPoint.Left = pointX;
                                vp.ShadowPoint.Top = pointY;

                               

                                info.ShadowInfo.LastPoint = new PointF(pointX, pointY);
                                info.ShadowInfo.LastValuePoint = vp.ShadowPoint;
                            }
                            //绘制脉搏和心率之间的连线
                            if (vp.ShowShadowPoint == true && info.VerticalLine == true
                                && vp.VerticalLine != DCTimeLineBooleanValue.False
                                && vp.ShadowPoint.VerticalLine != DCTimeLineBooleanValue.False)
                            {
                                using (var p3 = new Pen(GetRuntimeForeColor(Color.Red), GetRuntimeLineWidth(2)))
                                {
                                    g.DrawLine(
                                       p3,
                                       new PointF(vp.Left, vp.Top),
                                       new PointF(vp.ShadowPoint.Left, vp.ShadowPoint.Top)
                                       );
                                }
                            }
                        }
                        else
                        {
                            info.ShadowInfo.LastPoint = new PointF(float.NaN, float.NaN);
                            info.ShadowInfo.LastValuePoint = null;
                        }
                    }

                }
                //tick11 = CountDown.GetTickCountFloat() - tick11;
            }//foreach

            if (delayDrawedValuePoints.Count > 0)
            {
                foreach (ValuePoint vp in delayDrawedValuePoints)
                {
                    float runtimeSymbolSize = info.RuntimeSymbolSize;
                    //if (info.HighlightOutofNormalRange)
                    //{
                    //    runtimeSymbolSize = runtimeSymbolSize * 1.5f;
                    //}
                    Color infoSymbolColor = info.ReverseShadowInfo == null ? info.SymbolColor : info.ReverseShadowInfo.SymbolColor;
                    ValuePointSymbolStyle symbolStyle = vp.HollowCovertFlag ? ValuePointSymbolStyle.HollowCicle : info.SymbolStyle;
                    float runtimesymSize = vp.HollowCovertFlag ? runtimeSymbolSize * 1.5f : runtimeSymbolSize;
                    DrawSymbol( g,
                                clipRectangle,
                                vp.Left,
                                vp.Top,
                                symbolStyle,
                                info.CharacterForCharSymbolStyle,
                                infoSymbolColor, //info.SymbolColor,
                                vp,
                                runtimesymSize,
                                false);
                }
            }


            //tick88 = CountDown.GetTickCountFloat() - tick88;
            if (info.ShadowInfo != null && vps1.Count > 0)
            {


                // 数据线具有阴影数据线，准备填充阴影
                for (int iCount = startIndex; iCount <= endIndex; iCount++)
                {

                    ValuePoint vp = vps1[iCount];

                    if (vp.Time < startDate || vp.Time > this._RuntimeTicks.EndTime)
                    {
                        continue;
                    }
                    //**********************************************************

                    if (vp.ShowShadowPoint)
                    {

                        // 数据点绘制了阴影节点
                        // 查找填充区域的开始序号
                        int startShadowIndex = iCount;
                        if (startShadowIndex > startIndex)
                        {
                            ValuePoint prePoint = vps1[startShadowIndex - 1];
                            if (prePoint.IsNullValue == false
                                && prePoint.ShadowPoint != null
                                && float.IsNaN(prePoint.ShadowPoint.CenterY) == false)
                            {
                                // 修正起始序号
                                startShadowIndex--;
                            }
                            //if (vps1[startShadowIndex - 1].IsNullValue == false)
                            //{
                            //    // 修正起始序号
                            //    startShadowIndex--;
                            //}
                        }
                        // 搜索填充区域的结束序号
                        int endShadowIndex = iCount;
                        for (; endShadowIndex <= endIndex; endShadowIndex++)
                        {
                            ValuePoint vp2 = vps1[endShadowIndex];
                            //if (float.IsNaN(vp2.Top))
                            //{
                            //    endShadowIndex = endShadowIndex - 1;
                            //    break;
                            //}
                            if (vp2.ShowShadowPoint == false)
                            {
                                // 发现一个填充区域
                                if (vp2.ShadowPoint == null
                                    || float.IsNaN(vp2.ShadowPoint.CenterY))
                                {
                                    // 修正结尾序号
                                    endShadowIndex = endShadowIndex - 1;
                                }
                                break;
                            }
                            if (endShadowIndex == endIndex)
                            {
                                if (vp2.IsNullValue)
                                {
                                    endShadowIndex = endShadowIndex - 1;
                                }
                                break;
                            }
                        }//for

                        iCount = endShadowIndex;
                        if (endShadowIndex > startShadowIndex)
                        {
                            List<PointF> ps = new List<PointF>();
                            for (int index = startShadowIndex; index <= endShadowIndex; index++)
                            {
                                //如果数据点的VALUE值为空则直接跳过不处理TIMELINE353：20181116伍贻超
                                if (vps1[index].IsNullValue == false)
                                {
                                    ps.Add(new PointF(vps1[index].CenterX, vps1[index].CenterY));
                                }
                            }
                            for (int index = endShadowIndex; index >= startShadowIndex; index--)
                            {
                                ValuePoint vp2 = vps1[index];

                                //如果数据点的VALUE值为空则直接跳过不处理TIMELINE353：20181116伍贻超
                                if (vp2.IsNullValue)
                                {
                                    continue;
                                }
                                /////////////////////////////////////////////////////////////////////
                                if (vp2.ShowShadowPoint)
                                {
                                    ps.Add(new PointF(vp2.ShadowPoint.CenterX, vp2.ShadowPoint.CenterY));
                                }
                                else
                                {
                                    ps.Add(new PointF(vp2.CenterX, vp2.CenterY));
                                }
                            }//for
                            if (ps.Count > 2)
                            {
                                if (ps[0] != ps[ps.Count - 1])
                                {
                                    // 线段封闭
                                    ps.Add(ps[0]);
                                }
                            }
                            // 绘制封闭区域
                            using (Pen p = new Pen(GetRuntimeForeColor(info.SymbolColor), GetRuntimeLineWidth(2)))
                            {
                                SmoothingMode back = g.SmoothingMode;
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                g.DrawLines(
                                    p,
                                    ps.ToArray());
                                g.SmoothingMode = back;
                            }
                            // 填充阴影
                            using (HatchBrush brush = new HatchBrush(
                                    HatchStyle.LightUpwardDiagonal,
                                    GetRuntimeForeColor(info.SymbolColor),
                                    Color.Transparent))
                            {
                                if (info.ShadowPointVisible == true)
                                {
                                    g.FillPolygon(brush, ps.ToArray());
                                }

                                //g.FillPath(brush, path);
                            }

                        }//if
                    }//if
                    else
                    {
                    }
                }//for
            }//if
            //tick9 = CountDown.GetTickCountFloat() - tick9;

        }//foreach

        /// <summary>
        /// 绘制数值区域背景
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clipRectangle"></param>
        /// <param name="dataGridBounds"></param>
        /// <param name="visibleYAxisInfos"></param>
        /// <param name="startDate"></param>
        private void DrawValueRangeBackground(
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            RectangleF dataGridBounds,
            YAxisInfoList visibleYAxisInfos,
            DateTime startDate)
        {
            foreach (YAxisInfo info in visibleYAxisInfos)
            {
                bool drawNormalRange = false;
                if (info.ValueVisible)
                {
                    if (info.AbNormalRangeSettings != null && 
                        (TemperatureDocument.IsNullValue(info.AbNormalRangeSettings.NormalMaxValue) == false
                        || TemperatureDocument.IsNullValue(info.AbNormalRangeSettings.NormalMinValue) == false))
                    {
                        drawNormalRange = true;
                        foreach (YAxisInfo info2 in visibleYAxisInfos)
                        {
                            if (info2 != info && info2.ValueVisible && info2.Style == YAxisInfoStyle.Value)
                            {
                                //if (TemperatureDocument.IsNullValue(info2.NormalMaxValue) == false
                                //    || TemperatureDocument.IsNullValue(info2.NormalMinValue) == false)
                                {
                                    drawNormalRange = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (drawNormalRange == false)
                {
                    continue;
                }
                RectangleF normalBounds = dataGridBounds;

                // 绘制超出正常值范围的背景色
                if (info.AbNormalRangeSettings != null
                    && TemperatureDocument.IsNullValue(info.AbNormalRangeSettings.NormalMaxValue) == false
                    && info.AbNormalRangeSettings.NormalMaxValue > info.MinValue
                    && info.AbNormalRangeSettings.NormalMaxValue < info.MaxValue)
                {
                    // 存在正常区间最大值
                    float maxY = info.GetDisplayY(this, dataGridBounds, info.AbNormalRangeSettings.NormalMaxValue);
                    normalBounds.Height = normalBounds.Bottom - maxY;
                    normalBounds.Y = maxY;
                    if (info.AbNormalRangeSettings.OutofNormalRangeBackColor.A != 0)
                    {
                        RectangleF mr = new RectangleF(
                        dataGridBounds.Left,
                        dataGridBounds.Top,
                        dataGridBounds.Width,
                        maxY - dataGridBounds.Top);
                        RectangleF rect4 = RectangleF.Intersect(mr, clipRectangle);
                        if (rect4.IsEmpty == false)
                        {
                            g.FillRectangle(info.AbNormalRangeSettings.OutofNormalRangeBackColor,
                                    rect4);
                        }
                    }
                    if (normalBounds.Top != dataGridBounds.Top)
                    {
                        g.DrawLine(info.AbNormalRangeSettings.NormalRangeUpLineStyle,
                            normalBounds.Left,
                            normalBounds.Top,
                            normalBounds.Right,
                            normalBounds.Top);
                    }
                }
                if (TemperatureDocument.IsNullValue(info.AbNormalRangeSettings.NormalMinValue) == false
                    && info.AbNormalRangeSettings.NormalMinValue > info.MinValue
                    && info.AbNormalRangeSettings.NormalMinValue < info.MaxValue)
                {
                    // 存在正常区间最小值
                    float minY = info.GetDisplayY(this, dataGridBounds, info.AbNormalRangeSettings.NormalMinValue);
                    normalBounds.Height = minY - normalBounds.Top;
                    if (info.AbNormalRangeSettings.OutofNormalRangeBackColor.A != 0)
                    {
                        RectangleF mr = new RectangleF(
                        dataGridBounds.Left,
                        minY,
                        dataGridBounds.Width,
                        dataGridBounds.Bottom - minY);
                        RectangleF rect4 = RectangleF.Intersect(mr, clipRectangle);
                        if (rect4.IsEmpty == false)
                        {
                            g.FillRectangle(
                                info.AbNormalRangeSettings.OutofNormalRangeBackColor,
                                    rect4);
                        }
                    }
                    if (normalBounds.Bottom != dataGridBounds.Bottom)
                    {
                        g.DrawLine(info.AbNormalRangeSettings.NormalRangeDownLineStyle,
                            normalBounds.Left,
                            normalBounds.Bottom,
                            normalBounds.Right,
                            normalBounds.Bottom);
                    }
                }
                if (normalBounds.Height > 0 && info.AbNormalRangeSettings.NormalRangeBackColor.A != 0)
                {
                    // 绘制正常数值区间的背景
                    RectangleF rect4 = RectangleF.Intersect(normalBounds, clipRectangle);
                    if (rect4.IsEmpty == false)
                    {
                        g.FillRectangle(info.AbNormalRangeSettings.NormalRangeBackColor,
                            rect4);
                    }
                }
            }//foreach
        }


        /// <summary>
        /// 绘制背景样式的数值
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clipRectangle"></param>
        /// <param name="dataGridBounds"></param>
        /// <param name="visibleYAxisInfos"></param>
        /// <param name="startDate"></param>
        private void DrawBackgroundValues(
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            RectangleF dataGridBounds,
            YAxisInfoList visibleYAxisInfos,
            DateTime startDate)
        {
            // 绘制背景样式的数值
            int backgroundIndex = -1;

            foreach (YAxisInfo info in visibleYAxisInfos)
            {
                if (info.Style == YAxisInfoStyle.PartialBackground)
                {
                    // 自由排版方式绘制部分背景
                    backgroundIndex++;
                    info.GridViewTop = dataGridBounds.Bottom - dataGridBounds.Height * (backgroundIndex + 1)
                        / this.Config.GridYSplitInfo.GridYSplitNum;
                    info.GridViewHeight = dataGridBounds.Height / this.Config.GridYSplitInfo.GridYSplitNum;
                    DrawValueFreeLayout(
                        null,
                        info.Scales,
                        this.Datas.GetValuesByName(info.Name),
                        dataGridBounds,
                        info.GridViewTop,
                        info.GridViewHeight,
                        g,
                        clipRectangle,
                        startDate,
                        true);
                }//if
                else if (info.Style == YAxisInfoStyle.Background)
                {
                    // 自由排版方式绘制整体背景
                    backgroundIndex++;
                    info.GridViewTop = dataGridBounds.Top;// +dataGridBounds.Height * this.Config.DataGridTopPadding;
                    info.GridViewHeight = dataGridBounds.Height;// *(1f - this.Config.DataGridTopPadding - this.Config.DataGridBottomPadding);
                    DrawValueFreeLayout(
                        null,
                        info.Scales,
                        this.Datas.GetValuesByName(info.Name),
                        dataGridBounds,
                        info.GridViewTop,
                        info.GridViewHeight,
                        g,
                        clipRectangle,
                        startDate,
                        true);
                }
            }//foreach
        }

        private RectangleF FixLabelBoundsForAutoHeightLines(DCTimeLineLabel label)
        {
            RectangleF bounds = new RectangleF(
                    this.Left + label.Left,
                    this.Top + label.Top,
                    label.Width,
                    label.Height);
            if (this.RuntimeHeightIncrementForAutoHeightLine == 0 || label.PositionFixModeForAutoHeightLine == LabelPositionFixMode.None)
            {
                return bounds;
            }
            switch(label.PositionFixModeForAutoHeightLine)
            {
                case LabelPositionFixMode.AboveAutoHeightLine:
                    //文本框位于autoheight数据行上方，数据行在下方扩充行高，直接将文本框上移
                    bounds.Y = bounds.Y - this.RuntimeHeightIncrementForAutoHeightLine;
                    break;
                case LabelPositionFixMode.InsideAutoHeightLine:
                    //文本框位于autoheight数据行同一水平线，数据行扩充行高，文本框既要上移也要扩充高度
                    bounds.Y = bounds.Y - this.RuntimeHeightIncrementForAutoHeightLine;
                    bounds.Height = bounds.Height + this.RuntimeHeightIncrementForAutoHeightLine;
                    break;
                case LabelPositionFixMode.InsideDataGrid:
                    //文本框位于数据网格区，需要计算网格区被压缩的高度与原高度的比值，根据比值重新计算文本框在网格区的相对位置和相对大小
                    float gridRate = this.DataGridBounds.Height / (this.DataGridBounds.Height + this.RuntimeHeightIncrementForAutoHeightLine);
                    float labelToGridTopDiff = (bounds.Top - this.DataGridBounds.Top) * gridRate;
                    bounds.Y = this.DataGridBounds.Top + labelToGridTopDiff;
                    bounds.Height = bounds.Height * gridRate;
                    break;
                //default:
                //    break;
            }
            return bounds;
        }

        private void DrawLabels(DCGraphicsForTimeLine g, RectangleF clipRectangle)
        {
            if (this.RuntimeViewMode != DocumentViewMode.Page)
            {
                // 只在分页模式下显示
                return;
            }
            if (this.Config.Labels == null || this.Config.Labels.Count == 0)
            {
                // 无显示的数据
                return;
            }
            float rate = (float)GraphicsUnitConvert.GetRate(g.PageUnit, GraphicsUnit.Document);
            foreach (DCTimeLineLabel label in this.Config.Labels)
            {
                RectangleF bounds = FixLabelBoundsForAutoHeightLines(label);
                    //new RectangleF(
                    //this.Left + label.Left * rate,
                    //this.Top + label.Top * rate,
                    //label.Width * rate,
                    //label.Height * rate);
                label._LabelBounds = bounds;
                //if (clipRectangle.IntersectsWith(bounds))
                {
                    if (label.BackColor.A != 0)
                    {
                        g.FillRectangle(
                            label.BackColor,
                            bounds);
                    }
                    if (label.Image != null)
                    {
                        g.DrawImage(label.Image.Value, bounds);
                    }
                    if (label.ShowBorder == true)
                    {
                        g.DrawRectangle(
                            Pens.Black,
                            bounds);
                            //this.Left + label.Left * rate,
                            //this.Top + label.Top * rate,
                            //label.Width * rate,
                            //label.Height * rate);
                    }
                    string txt = this.Parameters.Convert(label.ParameterName, label.Text);
                    if (string.IsNullOrEmpty(txt) == false)
                    {
                        using (var format = DCStringFormat.GenericTypographic.Clone())
                        {
                            format.Alignment = label.Alignment;
                            format.LineAlignment = label.LineAlignment;
                            if (label.MultiLine == false)
                            {
                                format.FormatFlags = format.FormatFlags | StringFormatFlags.NoWrap;
                            }
                            AddNoClipForLinux(format);
                            XFontValue f = label.Font;
                            if (f == null)
                            {
                                f = new XFontValue();
                            }
                            g.DrawString(
                                txt,
                                f,
                                GetRuntimeForeColor(label.Color),
                                bounds,
                                format);
                        }
                    }
                }
            }//foreach
        }

        /// <summary>
        /// 绘制贴图
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clipRectangle"></param>
        private void DrawImages(DCGraphicsForTimeLine g, RectangleF clipRectangle)
        {
            if (this.RuntimeViewMode != DocumentViewMode.Page)
            {
                // 只在分页模式下显示
                return;
            }
            if (this.Config.Images == null || this.Config.Images.Count == 0)
            {
                return;
            }
            foreach (DCTimeLineImage img in this.Config.Images)
            {
                if (img.Image == null || img.Image.HasContent == false)
                {
                    continue;
                }
                RectangleF rect = RectangleF.Empty;
                rect.X = this.Left + GraphicsUnitConvert.Convert(img.Left, GraphicsUnit.Document, g.PageUnit);
                rect.Y = this.Top + GraphicsUnitConvert.Convert(img.Top, GraphicsUnit.Document, g.PageUnit);
                rect.Width = GraphicsUnitConvert.Convert(img.ImagePixelWidth, GraphicsUnit.Pixel, g.PageUnit);
                rect.Height = GraphicsUnitConvert.Convert(img.ImagePixelHeight, GraphicsUnit.Pixel, g.PageUnit);
                if (clipRectangle.IntersectsWith(rect))
                {
                    g.DrawImage(img.Image.Value, rect);
                }
            }
        }

        /// <summary>
        /// 获得前景色的画笔对象
        /// </summary>
        /// <returns></returns>
        private Pen ForePen
        {
            get
            {
                return GraphicsObjectBuffer.GetPen(GetRuntimeForeColor(this.Config.ForeColor));
            }
        }

        private Color RuntimeForeColor
        {
            get
            {
                return GetRuntimeForeColor(this.Config.ForeColor);
            }
        }

        ///// <summary>
        ///// 获得前景色的画刷对象
        ///// </summary>
        ///// <returns></returns>
        //private SolidBrush ForeBrush
        //{
        //    get
        //    {
        //        return GraphicsObjectBuffer.GetSolidBrush(this.GetRuntimeForeColor(this.Config.ForeColor));
        //    }
        //}

        /// <summary>
        /// 计算标题文本行的高度
        /// </summary>
        /// <param name="g">画布对象</param>
        /// <param name="line">文档行对象</param>
        /// <param name="stdTitleLineHeight">标准文档行高度</param>
        /// <param name="startDate">开始绘制日期</param>
        private void SetTitleLineHeight(
            DCGraphicsForTimeLine g,
            TitleLineInfo line,
            float stdTitleLineHeight,
            DateTime startDate)
        {
            line.Height = stdTitleLineHeight;
            DateTime endDate = startDate.AddDays(this.RuntimeNumOfDaysInOnePage);
            //float itemWidth = tickWidth * line.TimeSpan;
            if (line.RuntimeLayoutType == TitleLineLayoutType.Cascade)
            {
                ValuePointList vps = GetValuePointsByName(line.Name);
                if (vps != null && vps.Count > 0)
                {
                    DateTime lastDate = startDate.Date;
                    int maxLineNum = 1;
                    int lineNum = 0;
                    foreach (ValuePoint vp in vps)
                    {
                        if (vp.Time > endDate)
                        {
                            // 超出结束时间
                            break;
                        }
                        if (vp.Time.Date == lastDate)
                        {
                            lineNum++;
                        }
                        else
                        {
                            maxLineNum = Math.Max(maxLineNum, lineNum);
                            lineNum = 1;
                            if (vp.Time.Date > lastDate)
                            {
                                lastDate = vp.Time.Date;
                            }
                        }
                    }
                    line.Height = maxLineNum * stdTitleLineHeight;
                    line.ContentLineCount = maxLineNum;
                }
                else
                {
                    line.ContentLineCount = 1;
                    line.Height = stdTitleLineHeight;
                }
            }
            else if (line.RuntimeLayoutType == TitleLineLayoutType.Normal && line.AutoHeight)
            {
                //准备在这里处理Normal样式下的自增行高的需求，其它的不管了，先占位

                //计算宽度，硬做，只针对normal类型的标题行
                float width = this.DataGridBounds.Width / this.Config.NumOfDaysInOnePage;

                //遍历数据点解析出数据点文本长度最长的那一个
                ValuePointList vps = GetValuePointsByName(line.Name);
                string maxLengthVPText = string.Empty;
                if (vps != null && vps.Count > 0)
                {
                    DateTime lastDate = startDate.Date;
                    int maxLength = 0;
                    foreach (ValuePoint vp in vps)
                    {
                        if (vp.Time > endDate || vp.Time < startDate)
                        {
                            // 超出结束时间
                            continue;
                        }

                        if (DCGraphicsForTimeLine.TimeLineRunInLinuxMode)
                        {
                            SizeF vptextsize = g.MeasureString(vp.Text, this.Config.RuntimeFont);
                            //float num = vptextsize.Width / Width;
                            var txtWidth = g.MeasureString(vp.Text, this.Config.RuntimeFont).Width;
                            float num = txtWidth / width;
                            vp.TextSplitNumForLinux = (int)num + 1;
                        }

                        if (vp.Text != null && vp.Text.Length > maxLength)
                        {
                            maxLength = vp.Text.Length;
                            maxLengthVPText = vp.Text;
                        }
                    }
                }

                //计算文本长度最长的数据点在给定宽度下换行后的文本高度，设置为line的高度
                //line.Height = Math.Max(stdTitleLineHeight, g.MeasureString(maxLengthVPText, this.Config.RuntimeFont, (int)Width).Height);

                //SizeF sf = g.MeasureString(maxLengthVPText, this.Config.RuntimeFont);
                //float radio = sf.Width / Width;
                var txtWidth2 = g.MeasureString(maxLengthVPText, this.Config.RuntimeFont).Width;
                int radio = (int)(txtWidth2 / width);
                if (txtWidth2 <= width)
                {
                    radio = 1;
                }
                else
                {
                    radio = radio + 1;
                }
                //////////////////////////////
                line.Height = radio >= 1 ? stdTitleLineHeight * radio : stdTitleLineHeight;
            }
            if (g != null)
            {
                line.RefreshRuntimeHeight(g.PageUnit);
            }

            if (line.AutoHeight)
            {
                float hh = line.SpecifyHeight > 0 ? Math.Max(line.SpecifyHeight, stdTitleLineHeight) : stdTitleLineHeight;
                float hhdif = line.RuntimeHeight - hh;
                this.RuntimeHeightIncrementForAutoHeightLine += hhdif;
            }
        }

        internal float RuntimeHeightIncrementForAutoHeightLine = 0;

        /// <summary>
        /// 绘制一个标题数据行
        /// </summary>
        /// <param name="line"></param>
        /// <param name="dataGridBounds"></param>
        /// <param name="g"></param>
        /// <param name="clipRectangle"></param>
        /// <param name="leftHeaderWidth"></param>
        /// <param name="party"></param>
        /// <param name="startDate"></param>
        /// <param name="maxDate"></param>
        private void DrawTitleLine(
            TitleLineInfo line,
            RectangleF dataGridBounds,
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            float leftHeaderWidth,
            DocumentViewParty party,
            DateTime startDate,
            DateTime maxDate)
        {

            if (line.ValueType == TitleLineValueType.InDayIndex)
            {
                line.ValueType = TitleLineValueType.GlobalDayIndex;
            }
            //float tick = CountDown.GetTickCountFloat();

            line._LineBounds = new RectangleF(
                    this.Left,
                    line.Top,
                    this.Width,
                    line.RuntimeHeight);
            if (clipRectangle.IsEmpty == false
                && clipRectangle.IntersectsWith(line._LineBounds) == false)
            {
                // 整个对象不在剪切矩形中个，无需绘制
                return;
            }
            // 绘制横线
            g.DrawLine(
                this.ForePen,
                this.Left,
                line._LineBounds.Bottom,
                line._LineBounds.Right,
                line._LineBounds.Bottom);
            // 标题边界
            line.TitleBounds = new RectangleF(
                this.Left,
                line.Top,
                leftHeaderWidth,
                line.RuntimeHeight);
            XFontValue txtFont = CreateFontValue(line, false);
            float leftFix = 0;
            if (line.ShowExpandedHandle && this.PrintingMode == false)
            {
                // 出现展开收缩功能
                var ci = XImageValue.ConvertToBitmap( DCTimeLineImageResources.Collapse );
                var ei = XImageValue.ConvertToBitmap( DCTimeLineImageResources.Expand );
                SizeF imgSize = GraphicsUnitConvert.Convert(
                    new SizeF(ci.Width, ci.Height),
                    GraphicsUnit.Pixel,
                    g.PageUnit);
                float padding = GraphicsUnitConvert.Convert(3, GraphicsUnit.Pixel, g.PageUnit);
                int size = GraphicsUnitConvert.Convert(16, GraphicsUnit.Pixel, this.GraphicsUnit);
                float padding2 = GraphicsUnitConvert.Convert(2, GraphicsUnit.Pixel, this.GraphicsUnit);
                RectangleF ir = new RectangleF(
                    line.TitleBounds.Left + padding2,
                    line.TitleBounds.Top + (line.TitleBounds.Height - imgSize.Height) / 2,
                    imgSize.Width,
                    imgSize.Height);
                line.ExpandedHandleBounds = ir;
                var img = line.IsExpanded ? ei : ci;
                if (clipRectangle.IntersectsWith(ir))
                {
                    DrawerUtil.DrawImageUnscaledNearestNeighbor(g, img, (int)ir.Left, (int)ir.Top);
                }
                leftFix = padding2 + ir.Width + 2;
            }
            string runtimetitle = this.ViewMode == DocumentViewMode.Timeline ? line.Title : line.GetRuntimeTitleByPageIndex(this.PageIndex);
            if (string.IsNullOrEmpty(runtimetitle) == false)
            {
                // 绘制标题文本
                if (clipRectangle.IsEmpty || clipRectangle.IntersectsWith(line.TitleBounds))
                {
                    this.LineTitleStringFormat.Alignment = line.TitleAlign;
                    RectangleF txtRect = line.TitleBounds;
                    if (this.LineTitleStringFormat.Alignment == StringAlignment.Near)
                    {
                        txtRect = new RectangleF(
                            line.TitleBounds.Left + leftFix,
                            line.TitleBounds.Top,
                            line.TitleBounds.Width - leftFix,
                            line.TitleBounds.Height);
                    }
                    g.DrawString(
                        runtimetitle,
                        txtFont,
                        GetRuntimeForeColor(line.TitleColor),
                        txtRect,
                        this.LineTitleStringFormat);
                }
            }
            g.DrawRectangle(
                this.RuntimeForeColor,
                line.TitleBounds.Left,
                line.TitleBounds.Top,
                line.TitleBounds.Width,
                line.TitleBounds.Height);
            // 绘制标题的方框
            using (Pen p = new Pen(this.RuntimeForeColor, GetRuntimeLineWidth(2)))
            {
                g.DrawLine(
                    p,
                    line.TitleBounds.Left + line.TitleBounds.Width,
                    line.TitleBounds.Top,
                    line.TitleBounds.Left + line.TitleBounds.Width,
                    line.TitleBounds.Height + line.TitleBounds.Top
                    );
            }

            if (party == DocumentViewParty.LeftHeader)
            {
                // 只显示左端标题行，则不显示后续内容，继续下一个标题行
                return;
            }

            if (line.ExtendGridLineType == DCExtendGridLineType.Below)
            {
                // 绘制大竖线
                DrawDayGridLine(
                    g,
                    new RectangleF(
                        dataGridBounds.Left,
                        line.TitleBounds.Top,
                        dataGridBounds.Width,
                        line.TitleBounds.Height),
                    clipRectangle,
                    line);
            }

            var centerFormat = new DCStringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;
            centerFormat.FormatFlags = System.Drawing.StringFormatFlags.FitBlackBox
                    | System.Drawing.StringFormatFlags.MeasureTrailingSpaces
                    | StringFormatFlags.NoClip | StringFormatFlags.NoWrap;

            float dayStep = dataGridBounds.Width / this.RuntimeNumOfDaysInOnePage;
            if (this.Config.Ticks.Count > 0)
            {
                this._TickViewWidth = dayStep / this.Config.Ticks.Count;
            }
            else
            {
                this._TickViewWidth = dayStep;
            }

            if (line.ValueType == TitleLineValueType.SerialDate)
            {
                // 绘制日期系列
                DrawTitleLineForSerialDate(
                    line,
                    dataGridBounds,
                    g,
                    clipRectangle,
                    startDate,
                    centerFormat);
            }
            // 新增日期显示方式 住院日期首页第一天及跨年度第一天需写年、月、日，每页体温单的第一天及跨月份第一天需写月、日，其余只填日--宋建明
            else if (line.ValueType == TitleLineValueType.NewSerialDate)
            {
                // 绘制日期系列
                DrawTitleLineForNewSerialDate(
                    line,
                    dataGridBounds,
                    g,
                    clipRectangle,
                    startDate,
                    centerFormat);
            }

            else if (line.ValueType == TitleLineValueType.DayIndex
                || line.ValueType == TitleLineValueType.GlobalDayIndex)
            {
                // 绘制第几天数
                DrawTitleLineForDayIndex(
                    line,
                    dataGridBounds,
                    g,
                    clipRectangle,
                    startDate,
                    maxDate,
                    centerFormat);
            }//else if
            else if (line.ValueType == TitleLineValueType.HourTick)
            {

                // 绘制小时刻数
                DrawTitleLineForHourTick(
                    line,
                    dataGridBounds,
                    g,
                    clipRectangle);
            }//else
            else if (line.RuntimeLayoutType == TitleLineLayoutType.Free
                || line.RuntimeLayoutType == TitleLineLayoutType.FreeText || line.RuntimeLayoutType == TitleLineLayoutType.Slant3)
            {
                // 绘制自由排版内容
                DrawValueFreeLayout(
                    line,
                    line.Scales,
                    GetValuePointsByName(line.Name),
                    dataGridBounds,
                    line.Top + 1,
                    line.RuntimeHeight - 2,
                    g,
                    clipRectangle,
                    startDate,
                    line.ShowBackColor);
            }
            else if (line.RuntimeLayoutType == TitleLineLayoutType.Cascade
                || line.RuntimeLayoutType == TitleLineLayoutType.HorizCascade
                || line.RuntimeLayoutType == TitleLineLayoutType.Normal
                || line.RuntimeLayoutType == TitleLineLayoutType.Slant
                || line.RuntimeLayoutType == TitleLineLayoutType.Slant2
                || line.RuntimeLayoutType == TitleLineLayoutType.Fraction)
            {
                // 绘制普通内容或层叠内容
                DrawTitleLineForText(
                    line,
                    dataGridBounds,
                    g,
                    clipRectangle,
                    startDate);
            }
            if (line.ExtendGridLineType == DCExtendGridLineType.Above)
            {
                // 绘制大竖线
                DrawDayGridLine(
                    g,
                    new RectangleF(
                        dataGridBounds.Left,
                        line.TitleBounds.Top,
                        dataGridBounds.Width,
                        line.TitleBounds.Height),
                    clipRectangle,
                    line);
            }
            // 绘制标题的方框
            g.DrawRectangle(
                this.RuntimeForeColor,
                line.TitleBounds.Left,
                line.TitleBounds.Top,
                line.TitleBounds.Width,
                line.TitleBounds.Height);
            // 绘制横线
            // 上横线
            g.DrawLine(
                this.RuntimeForeColor,
                Math.Max(this.Left, clipRectangle.Left - 1),
                line.Top,
                Math.Min(this.Left + this.Width, clipRectangle.Right + 1),
                line.Top);
            // 下横线
            g.DrawLine(
                this.RuntimeForeColor,
                Math.Max(this.Left, clipRectangle.Left - 1),
                line.Top + line.RuntimeHeight,
                Math.Min(this.Left + this.Width, clipRectangle.Right + 1),
                line.Top + line.RuntimeHeight);
            centerFormat.Dispose();
            //tick = CountDown.GetTickCountFloat() - tick;
        }

       

        private void DrawDayGridLine(
            DCGraphicsForTimeLine g,
            RectangleF dataGridBounds,
            RectangleF clipRectangle,
            TitleLineInfo line)
        {
            int dn = this.RuntimeNumOfDaysInOnePage;
            float y1 = Math.Max(dataGridBounds.Top, clipRectangle.Top);
            float y2 = Math.Min(dataGridBounds.Bottom, clipRectangle.Bottom);
            //float y2 = (float)128.320282;
            if (y1 <= y2)
            {
                Color color = line.ExtendGridLineType == DCExtendGridLineType.Below ? this.RuntimeForeColor : this.Config.BigVerticalGridLineColor;

                using (Pen p = new Pen(GetRuntimeForeColor(color), this.Config.BigVerticalGridLineWidth))
                {
                    for (int iCount = 1; iCount < this._RuntimeTicks.Count; iCount++)
                    {
                        RuntimeTickInfo item = this._RuntimeTicks[iCount];
                        if (item.FirstTickInDate)// .TickIndex == 0)
                        {
                            float pos = dataGridBounds.Left + item.Left;
                            if (pos >= clipRectangle.Left && pos <= clipRectangle.Right)
                            {
                                g.DrawLine(p, pos, y1, pos, y2);
                            }
                        }
                    }
                }
            }
        }
        // 新增日期显示方式 住院日期首页第一天及跨年度第一天需写年、月、日，每页体温单的第一天及跨月份第一天需写月、日，其余只填日--宋建明
        /// <summary>
        /// 绘制日期序列
        /// </summary>
        /// <param name="line"></param>
        /// <param name="dataGridBounds"></param>
        /// <param name="g"></param>
        /// <param name="clipRectangle"></param>
        /// <param name="startDate"></param>
        /// <param name="centerFormat"></param>
        private void DrawTitleLineForNewSerialDate(
            TitleLineInfo line,
            RectangleF dataGridBounds,
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            DateTime startDate,
            DCStringFormat centerFormat)
        {
            int rndip = this.RuntimeNumOfDaysInOnePage;
            XFontValue txtFont = CreateFontValue(line, true);
            RectangleF lastBounds = RectangleF.Empty;
            string dateFormatString = this.Config.DateFormatString;
            if (string.IsNullOrEmpty(dateFormatString))
            {
                dateFormatString = "yyyy-MM-dd";
            }
            float fullWidth = g.MeasureString(
                dateFormatString,
                txtFont,
                10000,
                centerFormat).Width;
            // 是否存在历史欠账
            //bool isDue = false;
            for (int dayIndex = 0; dayIndex < rndip; dayIndex++)
            {
                DateTime dtm = startDate.AddDays(dayIndex);

                RectangleF rect2 = this._RuntimeTicks.GetDayBounds(dataGridBounds, dtm);

                if (rect2.IsEmpty)
                {
                    continue;
                }
                if (rect2.Right <= lastBounds.Right)
                {
                    continue;
                }
                else
                {
                    lastBounds = rect2;
                }
                rect2.Y = line.Top;
                rect2.Height = line.RuntimeHeight;
                if (rect2.Left > clipRectangle.Right)
                {
                    // 靠右超出剪切矩形，退出循环
                    break;
                }
                if (rect2.Left >= dataGridBounds.Right - 2)
                {
                    // 超出数据网格范围
                    break;
                }
                rect2 = FixForDataGridBounds(dataGridBounds, rect2);
                string text = null;

                if (this.PageIndex == 0 && dayIndex == 0)
                {
                    text = dtm.ToString(this.Config.DateFormatStringForFirstIndexFirstPage);
                }
                else if (dtm.Day == 1 && dtm.Month == 1)
                {
                    text = dtm.ToString(this.Config.DateFormatStringForCrossYear);
                }
                else if (dtm.Day == 1)
                {
                    text = dtm.ToString(this.Config.DateFormatStringForCrossMonth);
                }
                else if (this.PageIndex != 0 && dayIndex == 0)
                {
                    text = dtm.ToString(this.Config.DateFormatStringForFirstIndexOtherPage);
                }
                else if (this.PageIndex != 0 && dayIndex == 0)
                {
                    text = dtm.ToString(this.Config.DateFormatStringForCrossWeek);
                }
                else
                {
                    text = dtm.ToString(this.Config.DateFormatString);
                }

                if (text == null)
                {
                    // 否则只显示日数
                    text = dtm.Day.ToString(this.Config.DateFormatString);
                }
                fullWidth = g.MeasureString(text, txtFont, 10000, centerFormat).Width;
                if (rect2.Width < fullWidth)
                {
                    rect2.X = rect2.X - (fullWidth - rect2.Width) / 2;
                    rect2.Width = fullWidth;
                }
                ////////////////////////////////////////////////////////////////////////
                if (clipRectangle.IntersectsWith(rect2))
                {
                    Color tc = line == null ? this.Config.ForeColor : line.TextColor;
                    tc = line != null && line.BlankDateWhenNoData == true && this._NoDataInDocument == true ? Color.Transparent : tc;
                    g.DrawString(
                        text,
                        txtFont,
                        GetRuntimeForeColor(tc),
                        rect2,
                        centerFormat);
                }
            }//for
        }
        /// <summary>
        /// 绘制日期序列
        /// </summary>
        /// <param name="line"></param>
        /// <param name="dataGridBounds"></param>
        /// <param name="g"></param>
        /// <param name="clipRectangle"></param>
        /// <param name="startDate"></param>
        /// <param name="centerFormat"></param>
        private void DrawTitleLineForSerialDate(
            TitleLineInfo line,
            RectangleF dataGridBounds,
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            DateTime startDate,
            DCStringFormat centerFormat)
        {
            int rndip = this.RuntimeNumOfDaysInOnePage;
            XFontValue txtFont = CreateFontValue(line, true);
            RectangleF lastBounds = RectangleF.Empty;
            string dateFormatString = this.Config.DateFormatString;
            if (string.IsNullOrEmpty(dateFormatString))
            {
                dateFormatString = "yyyy-MM-dd";
            }
            float fullWidth = g.MeasureString(
                dateFormatString,
                txtFont,
                10000,
                centerFormat).Width;
            // 是否存在历史欠账
            bool isDue = false;
            for (int dayIndex = 0; dayIndex < rndip; dayIndex++)
            {
                DateTime dtm = startDate.AddDays(dayIndex);

                RectangleF rect2 = this._RuntimeTicks.GetDayBounds(dataGridBounds, dtm);
                if (rect2.IsEmpty)
                {
                    continue;
                }
                if (rect2.Right <= lastBounds.Right)
                {
                    continue;
                }
                else
                {
                    lastBounds = rect2;
                }
                rect2.Y = line.Top;
                rect2.Height = line.RuntimeHeight;
                if (rect2.Left > clipRectangle.Right)
                {
                    // 靠右超出剪切矩形，退出循环
                    break;
                }
                if (rect2.Left >= dataGridBounds.Right - 2)
                {
                    // 超出数据网格范围
                    break;
                }
                rect2 = FixForDataGridBounds(dataGridBounds, rect2);
                string text = null;
                if (dayIndex == 0
                    || dtm.Day == 1
                    || (dtm.DayOfWeek == DayOfWeek.Monday
                    && this.RuntimeViewMode == DocumentViewMode.Timeline))
                {
                    // 如果是本序列的第一栏或是本月的第一天则显示完整的日期
                    // 在时间轴模式下，星期一也全文显示
                    if (rect2.Width > fullWidth * 0.9f)
                    {
                        // 而且还要求显示宽度足够长
                        text = dtm.ToString(dateFormatString);
                        isDue = false;
                    }
                    else
                    {
                        isDue = true;
                    }
                }
                if (isDue && rect2.Width > fullWidth * 0.9f)
                {
                    // 存在历史欠账，尽量显示完整文本
                    text = dtm.ToString(dateFormatString);
                    isDue = false;
                }
                if (text == null)
                {
                    // 否则只显示日数
                    text = dtm.Day.ToString();
                }
                if (clipRectangle.IntersectsWith(rect2))
                {
                    Color tc = line == null ? this.Config.ForeColor : line.TextColor;
                    tc = line != null && line.BlankDateWhenNoData == true && this._NoDataInDocument == true ? Color.Transparent : tc;
                    g.DrawString(
                        text,
                        txtFont,
                        GetRuntimeForeColor(tc),
                        rect2,
                        centerFormat);
                }
            }//for
        }

        /// <summary>
        /// 绘制天数
        /// </summary>
        /// <param name="line">数据行对象</param>
        /// <param name="dataGridBounds">数据网格区域边界</param>
        /// <param name="centerFormat">居中显示的文字格式化对象</param>
        /// <param name="clipRectangle">剪切矩形</param>
        /// <param name="g">画布对象</param>
        /// <param name="maxDate">最大显示日期</param>
        /// <param name="startDate">开始显示日期</param>
        private void DrawTitleLineForDayIndex(
            TitleLineInfo line,
            RectangleF dataGridBounds,
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            DateTime startDate,
            DateTime maxDate,
            DCStringFormat centerFormat)
        {
            //float tickSpan = CountDown.GetTickCountFloat();
            int rndop = this.RuntimeNumOfDaysInOnePage;
            if (line._RuntimeStartDates != null)
            {
                XFontValue txtFont = CreateFontValue(line, true);
                RectangleF lastBounds = RectangleF.Empty;
                // 计算起始天的大致序号，能大幅降低大循环的循环次数
                int startIndex = this._RuntimeTicks.GetStartDetectIndex(dataGridBounds, clipRectangle);
                DateTime dtm5 = this._RuntimeTicks[startIndex].StartTime;
                startIndex = (int)dtm5.Subtract(startDate).TotalDays - 2;
                startIndex = Math.Max(startIndex, 0);
                for (int dayIndex = startIndex; dayIndex < rndop; dayIndex++)
                {
                    // 计算当前日期
                    DateTime dtm = startDate.AddDays(dayIndex);
                    if (dtm >= maxDate ||
                        (line.RuntimeEndDate != TemperatureDocument.NullDate && dtm >= line.RuntimeEndDate))//新增结束时间处理
                    {
                        // 超出最大日期，不显示
                        break;
                    }
                    if (line.EnableEndTime && IsNullDate(this._DataMaxDate) == false)
                    {
                        if (dtm > this._DataMaxDate)
                        {
                            // 超出最大数据日期
                            break;
                        }
                    }
                    // 获得时间差
                    RectangleF rect2 = this._RuntimeTicks.GetDayBounds(dataGridBounds, dtm);
                    if (rect2.Right <= lastBounds.Right)
                    {
                        // 和上一个边框重合，则忽略
                        continue;
                    }
                    else
                    {
                        lastBounds = rect2;
                    }
                    rect2.Y = line.Top;
                    rect2.Height = line.RuntimeHeight;
                    if (rect2.Left > clipRectangle.Right)
                    {
                        // 靠右超出剪切矩形，退出循环
                        break;
                    }
                    if (rect2.Left > dataGridBounds.Right - 2)
                    {
                        // 超出网格线区域
                        break;
                    }
                    rect2 = FixForDataGridBounds(dataGridBounds, rect2);
                    if (rect2.IsEmpty)
                    {
                        continue;
                    }
                    if (clipRectangle.IntersectsWith(rect2))
                    {
                        int maxDays = (line.ValueType != TitleLineValueType.GlobalDayIndex && line.MaxValueForDayIndex > 0)
                        ? line.MaxValueForDayIndex : int.MaxValue;
                        StringBuilder strText = new StringBuilder();
                        for (int iCount = line._RuntimeStartDates.Length - 1; iCount >= 0; iCount--)
                        {
                            DateTime dtm2 = line._RuntimeStartDates[iCount];
                            DateTime newdtm = dtm2.AddDays(-1);//手术当天的日期
                            TimeSpan span = dtm - dtm2;
                            TimeSpan newspan = dtm - newdtm;//当前日期与手术日期的差
                            if (span.Days > maxDays)//&& iCount != line._RuntimeStartDates.Length - 1)
                            {
                                // 超过了最大天数天
                                continue;
                            }
                            //if (line.Title == "术后天数")//add by ld 2016-05-07 南京医科大学眼科医院需求
                            //{
                            if (line.AfterOperaDaysFromZero == true)//判断是否从0开始显示
                            {
                                if (newspan.Days == 0)
                                {
                                    int dayindex = newspan.Days;
                                    if(line.AfterOperaDaysBeginOne == true)
                                    {
                                        dayindex++;
                                    }
                                    string text = Convert.ToString(dayindex);
                                    if (strText.Length > 0)
                                    {
                                        strText.Append("/");
                                    }
                                    strText.Append(text);
                                }
                            }
                            //}
                            if (span.Days >= 0)
                            {
                                int dayindex = span.Days + 1;
                                if (line.AfterOperaDaysBeginOne == true)
                                {
                                    dayindex++;
                                }
                                string text = Convert.ToString(dayindex);
                                //string text = Convert.ToString(span.Days);// edit by ld
                                if (strText.Length > 0)
                                {
                                    strText.Append("/");
                                }
                                strText.Append(text);
                            }//if
                        }//foreach
                        if (strText.Length > 0)
                        {
                            // 绘制文本
                            Color tc = line == null ? this.Config.ForeColor : line.TextColor;
                            g.DrawString(
                                strText.ToString(),
                                txtFont,
                                GetRuntimeForeColor(tc),
                                rect2,
                                centerFormat);
                        }//if
                    }
                }//for
            }//if
            //tickSpan = CountDown.GetTickCountFloat() - tickSpan;
        }

        private RectangleF FixForDataGridBounds(RectangleF dataGridBounds, RectangleF rect)
        {
            rect.X = Math.Max(rect.X, dataGridBounds.Left);
            float r = Math.Min(rect.Right, dataGridBounds.Right);
            rect.Width = r - rect.Left;
            if (rect.Width <= 0)
            {
                return RectangleF.Empty;
            }
            else
            {
                return rect;
            }
        }


        /// <summary>
        /// 绘制等间距文本数值
        /// </summary>
        /// <param name="line">数据线对象</param>
        /// <param name="dataGridBounds">数据网格区域</param>
        /// <param name="g">画布对象</param>
        /// <param name="clipRectangle">剪切矩形</param>
        /// <param name="startDate">开始时间</param>
        private void DrawTitleLineForText(
            TitleLineInfo line,
            RectangleF dataGridBounds,
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            DateTime startDate)
        {
            

            foreach (RectangleF tickRect in line._TickRectForDraw)
            {
                if (clipRectangle.IntersectsWith(tickRect))
                {
                    Color tempColor = line.TickLineVisible ? this.RuntimeForeColor : Color.Transparent;

                    // 绘制边框
                    g.DrawLine(
                        tempColor,// this.RuntimeForeColor,
                        tickRect.Left,
                        tickRect.Top,
                        tickRect.Left,
                        tickRect.Bottom);
                }
            }


            //遍历需要绘制的数据点，统一在这里进行绘制
            foreach (ValuePoint valuePoint in line._ValuePointsForDraw)
            {
                if (clipRectangle.IntersectsWith(valuePoint._OriginalBlockBounds))
                {
                    DrawTitleLineSingleTextValue(valuePoint, g);
                }
            }

        }

        /// <summary>
        /// 绘制小时时刻数
        /// </summary>
        /// <param name="line"></param>
        /// <param name="dataGridBounds"></param>
        /// <param name="g"></param>
        /// <param name="clipRectangle"></param>
        private void DrawTitleLineForHourTick(
            TitleLineInfo line,
            RectangleF dataGridBounds,
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle)
        {
            XFontValue txtFont = CreateFontValue(line, true);
            float dayStep = dataGridBounds.Width / this.RuntimeNumOfDaysInOnePage;
            // 绘制小时刻数
            float topFix = (line.RuntimeHeight - g.GetFontHeight(txtFont)) / 2;
            //float tickWidth = dataGridBounds.Width / this._RuntimeTicks.Count;
            //if (clipRectangle.IsEmpty)
            //{
            //    clipRectangle = dataGridBounds;
            //}
            //else
            //{
            //    clipRectangle = RectangleF.Intersect(clipRectangle, dataGridBounds);
            //}
            //clipRectangle = FixForDataGridBounds(dataGridBounds, clipRectangle);
            using (var format = DCStringFormat.GenericTypographic.Clone())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                format.FormatFlags = StringFormatFlags.NoWrap;
                AddNoClipForLinux(format);
                int startTickIndex = this._RuntimeTicks.GetStartDetectIndex(dataGridBounds, clipRectangle);
                for (int iCount = startTickIndex; iCount < this._RuntimeTicks.Count; iCount++)
                {
                    RuntimeTickInfo info = this._RuntimeTicks[iCount];
                    float pos = dataGridBounds.Left + info.Left;
                    if (pos + info.Width < clipRectangle.Left)
                    {
                        // 没到达剪切区域
                        continue;
                    }
                    if (pos > clipRectangle.Right)
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
                        info.Width,
                        line.RuntimeHeight);
                    RuntimeTickInfo tick = this._RuntimeTicks[iCount];
                    if (clipRectangle.IntersectsWith(tickRect))
                    {
                        if (string.IsNullOrEmpty(tick.Text) == false)
                        {
                            // 绘制时刻数
                            g.DrawString(
                                tick.Text,
                                txtFont,
                                GetRuntimeForeColor(tick.Color),
                                tickRect,
                                format);
                        }
                        //tickRect.X,
                        //tickRect.Y + topFix);
                        // 绘制边框
                        g.DrawLine(
                            this.RuntimeForeColor,
                            tickRect.Left,
                            tickRect.Top,
                            tickRect.Left,
                            tickRect.Bottom);
                        //g.DrawRectangle(
                        //    this.ForePen,
                        //    tickRect.Left,
                        //    tickRect.Top,
                        //    tickRect.Width,
                        //    tickRect.Height);
                        if (tick.TickIndex == 0)
                        {
                            Color color = line.ExtendGridLineType == DCExtendGridLineType.Below ? this.RuntimeForeColor : this.Config.BigVerticalGridLineColor;
                            g.DrawLine(
                                color,
                                tickRect.Left,
                                tickRect.Top,
                                tickRect.Left,
                                tickRect.Bottom);
                        }
                    }
                }//for
            }//using

        }
        private void AddNoClipForLinux(DCStringFormat f)
        {
            if (DCGraphicsForTimeLine.LinuxMode)
            {
                f.FormatFlags = f.FormatFlags | StringFormatFlags.NoClip;
            }
        }
#if !DCWriterForWASM
        private void AddNoClipForLinux(DrawStringFormatExt f)
        {
            if (DCGraphicsForTimeLine.LinuxMode)
            {
                f.FormatFlags = f.FormatFlags | StringFormatFlags.NoClip;
            }
        }

#endif
        /// <summary>
        /// 绘制数据行的一个以分数形式展示的文本值，支持格式"10(2/3)"显示为十又三分之二
        /// </summary>
        private void DrawTitleLineSingleTextValueFraction(
            ValuePoint vp,
            DCGraphicsForTimeLine g)
        {
            TitleLineInfo currentLine = vp.Parent as TitleLineInfo;
            bool hasinteger = vp.RuntimeText.Contains("(") == true && vp.RuntimeText.Contains(")");

            int index = vp.RuntimeText.IndexOf("/");
            string integerStr = "";
            string numeratorStr = vp.RuntimeText.Substring(0, index);//解析分子字符串
            string denominatorStr = vp.RuntimeText.Substring(index + 1, vp.RuntimeText.Length - numeratorStr.Length - 1);//解析分母字符串
            //若有整数部分直接
            if (hasinteger)
            {
                denominatorStr = denominatorStr.Substring(0, denominatorStr.Length - 1);
                int index2 = numeratorStr.IndexOf("(");
                integerStr = numeratorStr.Substring(0, index2);
                numeratorStr = numeratorStr.Substring(index2 + 1, numeratorStr.Length - integerStr.Length - 1);
            }

            XFontValue txtFont = CreateFontValue(currentLine, true, vp);//计算绘制用的字体字号
            //计算绘制用的前景色
            Color txtColor = Color.Black;
            if (currentLine == null)
            {
                txtColor = this.Config.ForeColor;
            }
            else
            {
                if (DCTimeLineUtils.IsOutofRange(vp.Value, currentLine.NormalMaxValue, currentLine.NormalMinValue))
                {
                    txtColor = currentLine.OutofNormalRangeTextColor;
                }
                else if (vp.TextColor != Color.Empty)
                {
                    txtColor = vp.TextColor;
                }
                else
                {
                    txtColor = currentLine.TextColor;
                }
            }
            txtColor = GetRuntimeForeColor(txtColor);

            SizeF sizeofInteger = g.MeasureString(integerStr, txtFont);
            if (integerStr.Length == 0)
            {
                sizeofInteger.Width = 0;
                sizeofInteger.Height = 0;
            }

            //计算分号中线的长度
            string temp = numeratorStr.Length > denominatorStr.Length ? numeratorStr : denominatorStr;
            SizeF sizef = g.MeasureString(temp, txtFont);
            float hlinewidth = Math.Min(sizef.Width, vp.ViewBounds.Width - 2);

            using (var format = DCStringFormat.GenericTypographic.Clone())
            {
                format.Alignment = StringAlignment.Center;
                TitleLineInfo line = vp.Parent as TitleLineInfo;
                if (line != null && line.AutoHeight == false)
                {
                    format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip;
                }
                format.FormatFlags = format.FormatFlags & ~StringFormatFlags.NoWrap;
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = currentLine.ValueAlign;

                if (integerStr.Length > 0)
                {
                    //绘制整数部分
                    g.DrawString(
                        integerStr,
                        txtFont,
                        txtColor,
                        new RectangleF(
                            vp.ViewBounds.Left,
                            vp.ViewBounds.Top + (vp.ViewBounds.Height - sizeofInteger.Height) / 2,
                            sizeofInteger.Width,
                            sizeofInteger.Height),
                        format);
                }

                // 绘制分子
                g.DrawString(
                    numeratorStr,
                    txtFont,
                    txtColor,
                    new RectangleF(
                        vp.ViewBounds.Left + sizeofInteger.Width,
                        vp.ViewBounds.Top,
                        vp.ViewBounds.Width - sizeofInteger.Width,
                        vp.ViewBounds.Height / 2),
                    format);
                //绘制分数中线
                g.DrawLine(
                    txtColor,
                    1,
                    vp.ViewBounds.Left + sizeofInteger.Width + (vp.ViewBounds.Width - hlinewidth - sizeofInteger.Width) / 2,
                    vp.ViewBounds.Top + vp.ViewBounds.Height / 2,
                    vp.ViewBounds.Right - (vp.ViewBounds.Width - hlinewidth - sizeofInteger.Width) / 2,
                    vp.ViewBounds.Top + vp.ViewBounds.Height / 2
                    );
                // 绘制分母
                g.DrawString(
                    denominatorStr,
                    txtFont,
                    txtColor,
                    new RectangleF(
                        vp.ViewBounds.Left + sizeofInteger.Width,
                        vp.ViewBounds.Top + vp.ViewBounds.Height / 2,
                        vp.ViewBounds.Width - sizeofInteger.Width,
                        vp.ViewBounds.Height / 2),
                    format);
            }//using
        }

        /// <summary>
        /// 绘制数据行的一个文本值
        /// </summary>
        private void DrawTitleLineSingleTextValue(
            ValuePoint vp,
            DCGraphicsForTimeLine g)
        {
            TitleLineInfo currentLine = vp.Parent as TitleLineInfo;

            Color tempcolor = this.ForePen.Color;
            if (currentLine != null && currentLine.TickLineVisible == false)
            {
                tempcolor = Color.Transparent;
            }
            ///////////////////////////////////////////////////

            // 所有的点都会绘制右边竖线，从drawtitlelinefortext函数中移过来
            g.DrawLine(
                tempcolor,
                vp._OriginalBlockBounds.Right,
                vp._OriginalBlockBounds.Top,
                vp._OriginalBlockBounds.Right,
                vp._OriginalBlockBounds.Bottom);


            // 针对斜线特殊处理，从drawtitlelinefortext函数中移过来
            if (currentLine.RuntimeLayoutType == TitleLineLayoutType.Slant
                    || currentLine.RuntimeLayoutType == TitleLineLayoutType.Slant2)
            {
                SmoothingMode back = g.SmoothingMode;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (currentLine.RuntimeLayoutType == TitleLineLayoutType.Slant)
                {
                    g.DrawLine(
                        Pens.Black,
                        vp._OriginalBlockBounds.Left,
                        vp._OriginalBlockBounds.Top,
                        vp._OriginalBlockBounds.Right,
                        vp._OriginalBlockBounds.Bottom);
                }
                else
                {
                    g.DrawLine(
                        Pens.Black,
                        vp._OriginalBlockBounds.Right,
                        vp._OriginalBlockBounds.Top,
                        vp._OriginalBlockBounds.Left,
                        vp._OriginalBlockBounds.Bottom);
                }
                g.SmoothingMode = back;
            }

            bool runtimeValueTextMultiLine = currentLine.ValueTextMultiLine || currentLine.AutoHeight;

            if (this._LastDrawViewBounds != null)
            {
                this._LastDrawViewBounds[vp] = vp.ViewBounds;
            }

            if (vp.Color.A != 0)
            {
                // 绘制背景
                using (SolidBrush b = new SolidBrush(vp.Color))
                {
                    g.FillRectangle(b, vp.ViewBounds);
                }
                g.DrawRectangle(
                    this.RuntimeForeColor,
                    vp.ViewBounds.Left,
                    vp.ViewBounds.Top,
                    vp.ViewBounds.Width,
                    vp.ViewBounds.Height);
            }
            if (currentLine.RuntimeLayoutType == TitleLineLayoutType.Cascade)
            {
                g.DrawLine(this.RuntimeForeColor, vp.ViewBounds.Left, vp.ViewBounds.Bottom, vp.ViewBounds.Right, vp.ViewBounds.Bottom);
            }
            else if (currentLine.RuntimeLayoutType == TitleLineLayoutType.HorizCascade)
            {
                g.DrawLine(this.RuntimeForeColor, vp.ViewBounds.Right, vp.ViewBounds.Top, vp.ViewBounds.Right, vp.ViewBounds.Bottom);
            }
            RuntimeTickInfo tickinfo = this._RuntimeTicks.SafeGetItem(vp._TickIndexForDraw);
            if (tickinfo != null && tickinfo.TickIndex == 0)
            {

                Color color = currentLine.ExtendGridLineType == DCExtendGridLineType.Below ? this.RuntimeForeColor : this.Config.BigVerticalGridLineColor;
                g.DrawLine(
                    color,
                    vp.ViewBounds.Left,
                    vp.ViewBounds.Top,
                    vp.ViewBounds.Left,
                    vp.ViewBounds.Bottom);
            }


            if (currentLine.RuntimeLayoutType == TitleLineLayoutType.Fraction && vp.RuntimeText.Contains("/") == true)
            {
                DrawTitleLineSingleTextValueFraction(vp, g);
                return;
            }

            string vpText = vp.RuntimeText;
            if (string.IsNullOrEmpty(vpText))
            {
                return;
            }
            XFontValue txtFont = CreateFontValue(currentLine, true, vp);
            using (var format = DCStringFormat.GenericTypographic.Clone())
            {
                format.Alignment = StringAlignment.Center;
                TitleLineInfo line = vp.Parent as TitleLineInfo;

                if (line != null && line.AutoHeight == false)
                {
                    format.FormatFlags = StringFormatFlags.NoWrap;
                }
                else
                {
                    format.FormatFlags = format.FormatFlags & ~StringFormatFlags.LineLimit;
                }
                ////////////////////////////////////////////////////////////
                
                if (runtimeValueTextMultiLine)
                {
                    // 多行文本
                    format.FormatFlags = format.FormatFlags & ~StringFormatFlags.NoWrap;
                }
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = vp.TextAlign != StringAlignment.Center ? vp.TextAlign : currentLine.ValueAlign;
                AddNoClipForLinux(format);
                //if (line.LayoutType == TitleLineLayoutType.HorizCascade)
                //{
                //    format.Alignment = StringAlignment.Near;
                //}
                if (currentLine.RuntimeLayoutType == TitleLineLayoutType.Slant)
                {
                    if (vp._TickIndexForDraw == 0)
                    {
                        format.LineAlignment = StringAlignment.Far;
                    }
                    else
                    {
                        format.LineAlignment = StringAlignment.Near;
                    }
                }
                else if (currentLine.RuntimeLayoutType == TitleLineLayoutType.Slant2)
                {
                    if (vp._TickIndexForDraw == 0)
                    {
                        format.LineAlignment = StringAlignment.Near;
                    }
                    else
                    {
                        format.LineAlignment = StringAlignment.Far;
                    }
                }
                else if (currentLine.UpAndDownTextType != UpAndDownTextType.None || vp.hasSpecifyUpDownType == true /*|| currentLine.UpAndDownText == true*/)
                {
                   
                    if (vp.UpAndDown == ValuePointUpAndDown.Up)
                    {
                        format.LineAlignment = StringAlignment.Near;
                    }
                    else if (vp.UpAndDown == ValuePointUpAndDown.Down)
                    {
                        format.LineAlignment = StringAlignment.Far;
                    }
                }//if
                Color txtColor = Color.Black;
                if (currentLine == null)
                {
                    txtColor = this.Config.ForeColor;
                }
                else
                {
                    if (DCTimeLineUtils.IsOutofRange(vp.Value, currentLine.NormalMaxValue, currentLine.NormalMinValue))
                    {
                        txtColor = currentLine.OutofNormalRangeTextColor;
                    }
                    else if (vp.TextColor != Color.Empty)
                    {
                        txtColor = vp.TextColor;
                    }
                    else
                    {
                        txtColor = currentLine.TextColor;
                    }
                }
                if (this.IsUseLinkVisualStyle(vp))
                {
                    txtColor = Color.Blue;
                    txtFont.Underline = true;
                }

                vpText = this.GetTextProcessedForLinux(vpText);

                // 绘制字符串
                g.DrawString(
                    vpText,
                    txtFont,
                    GetRuntimeForeColor(txtColor),
                    new RectangleF(
                        vp.ViewBounds.Left + 1,
                        vp.ViewBounds.Top + 1,
                        vp.ViewBounds.Width - 1,
                        vp.ViewBounds.Height - 2),
                    format);
                if (currentLine.CircleText == vpText)
                {
                    // 文字画圈
                    float circleSize = Math.Min(g.GetFontHeight(txtFont), vp.ViewBounds.Width - 2);
                    RectangleF circelBounds = new RectangleF(
                        vp.ViewBounds.Left + (vp.ViewBounds.Width - circleSize) / 2,
                        vp.ViewBounds.Top + 1,
                        circleSize,
                        circleSize);
                    if (format.LineAlignment == StringAlignment.Near)
                    {
                        circelBounds.Y = vp.ViewBounds.Top + 1;
                    }
                    else
                    {
                        circelBounds.Y = vp.ViewBounds.Bottom - circelBounds.Height - 1;
                    }
                    SmoothingMode back = g.SmoothingMode;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawEllipse(
                        currentLine == null ? this.RuntimeForeColor : currentLine.TextColor,
                        circelBounds);
                    g.SmoothingMode = back;
                }//if
            }//using
        }

        private void DrawIcon(DCGraphicsForTimeLine g, Image img, float x, float y)
        {
            float rate = (float)GraphicsUnitConvert.Convert(
                1.0,
                GraphicsUnit.Pixel,
                g.PageUnit);
            g.DrawImageUnscaled(img, (int)x, (int)y);
            //g.DrawImage( 
            //    img , 
            //    new RectangleF( 
            //        x , 
            //        y , 
            //        this.ImagePixelWidth * rate , 
            //        this.ImagePixelHeight * rate ));
        }

        /// <summary>
        /// 标题文字边界
        /// </summary>
        [NonSerialized]
        internal RectangleF _HeaderLabelBounds = RectangleF.Empty;

       



        /// <summary>
        /// 绘制病人基本信息
        /// </summary>
        private void DrawHeaderLabels(
            float bigTitleHeight,
            float titleLineHeight,
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            XFontValue labelFont)
        {
            this._HeaderLabelBounds = RectangleF.Empty;
            if (this.HeaderLabels == null || this.HeaderLabels.Count == 0)
            {
                // 无任何有效数据
                return;
            }

            

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
                // 分页视图模式
                float itemSpacing = this.Width;
                foreach (HeaderLabelInfo info in this.HeaderLabels)
                {
                    itemSpacing -= info.Width;
                }
                if (this.HeaderLabels.Count > 1)
                {
                    itemSpacing = itemSpacing / (this.HeaderLabels.Count - 1);
                }
                float leftCount = this.Left;
                //foreach (HeaderLabelInfo info in this.HeaderLabels)
                //{
                //    info.Left = leftCount;
                //    info.Top = this.Top + bigTitleHeight;
                //    leftCount += info.Width + itemSpacing;
                //}
            }
            // 绘制图形
            foreach (HeaderLabelInfo info in this.HeaderLabels)
            {
                RectangleF bounds = new RectangleF(info.Left, info.Top, info.Width, info.Height);
                if (bounds.IntersectsWith(clipRectangle))
                {
                    info.OwnerDocument = this;
                    info.Draw(g, labelFont, this.Config.ForeColor, this.RuntimeViewMode.ToString());
                }
            }//foreach
        }

        /// <summary>
        /// 绘制自由宽度数值
        /// </summary>
        /// <param name="info">数据行信息对象</param>
        /// <param name="scales">自定义刻度</param>
        /// <param name="values">数值</param>
        /// <param name="dataGridBounds">数据网格区域</param>
        /// <param name="viewTop">视图区域顶端位置</param>
        /// <param name="viewHeight">视图区域高度</param>
        /// <param name="g">图形绘制对象</param>
        /// <param name="clipRectangle">剪切矩形</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="showBackColor">显示背景色</param>
        private void DrawValueFreeLayout(
            TitleLineInfo info,
            YAxisScaleInfoList scales,
            ValuePointList values,
            RectangleF dataGridBounds,
            float viewTop,
            float viewHeight,
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            DateTime startDate,
            bool showBackColor)
        {
            if (values == null || values.Count == 0)
            {
                return;
            }
            if (g == null)
            {
                return;
            }

            //float tick = CountDown.GetTickCountFloat();
            dataGridBounds.Y = -1000;
            dataGridBounds.Height = 100000;
            ValuePoint lastValuePoint = null;
            Color bkColor = Color.Transparent;
            float dayStep = dataGridBounds.Width / this.RuntimeNumOfDaysInOnePage;
            for (int iCount = values.Count - 1; iCount >= 0; iCount--)
            {
                //if (iCount == 0)
                //{
                //}
                ValuePoint vp = values[iCount];
                XFontValue labelFont = CreateFontValue(info, true, vp);
                bkColor = vp.Color;
                string text = vp.Text;
                if (scales != null)
                {
                    YAxisScaleInfo scale = scales.GetScaleInfoByValue(vp.Value);
                    if (scale != null)
                    {
                        bkColor = scale.Color;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = scale.Text;
                        }
                    }
                }
                if (string.IsNullOrEmpty(text))
                {
                    text = vp.Value.ToString();
                }
                //int dayIndex = (vp.Time - startDate).Days;
                //int hourTickIndex = GetHourTickIndex(vp.Time);
                //vp.ViewX = dataGridBounds.Left +
                //    (dayIndex * this.HourTicks.Length + hourTickIndex + 0.5f)
                //    * (dayStep / (float)this.HourTicks.Length);
                if (this._RuntimeTicks.Count > 0)
                {
                    if (vp.Time >= this._RuntimeTicks.LastItem.EndTime
                        //伍贻超添加代码20160903：添加判断是否小于起始日期
                        || vp.Time < this._RuntimeTicks.StartTime)
                    {
                        // 超出最大时间范围 ，或小于最小时间范围
                        continue;
                    }
                }
                vp.Left = this._RuntimeTicks.GetXPosition(dataGridBounds, vp.Time);
                // vp.Left = (float)(dataGridBounds.Left + dayStep * (vp.Time - startDate).TotalDays);
                float viewWidth = 0;
                bool enableEndTime = true;
                if (info != null)
                {
                    enableEndTime = info.EnableEndTime;
                }
                if (info == null || info.RuntimeLayoutType == TitleLineLayoutType.Free || info.RuntimeLayoutType == TitleLineLayoutType.Slant3)
                {
                    if (enableEndTime && TemperatureDocument.IsNullDate(vp.EndTime) == false)
                    {
                        // 使用结束时间来算
                        viewWidth = this._RuntimeTicks.GetXPosition(dataGridBounds, vp.EndTime) - vp.Left;
                        //viewWidth = (float)(dataGridBounds.Left + dayStep * (vp.EndTime - startDate).TotalDays) - vp.Left;
                    }
                    else if (lastValuePoint == null)
                    {
                        viewWidth = dataGridBounds.Right - vp.Left;
                    }
                    else
                    {
                        viewWidth = lastValuePoint.Left - vp.Left;
                    }
                }
                else if (info != null && info.RuntimeLayoutType == TitleLineLayoutType.FreeText)
                {
                    if (lastValuePoint == null)
                    {
                        viewWidth = dataGridBounds.Right - vp.Left;
                    }
                    else
                    {
                        viewWidth = lastValuePoint.Left - vp.Left;
                    }
                }
                if (viewWidth <= 0)
                {
                    continue;
                }
                RectangleF vpBounds = new RectangleF(
                    vp.Left,
                    viewTop,
                    viewWidth,
                    viewHeight);
                RectangleF vpBoundsBack = vpBounds;
                vpBounds = RectangleF.Intersect(vpBounds, dataGridBounds);
                vp.ViewBounds = vpBounds;
                RectangleF txtBounds = vpBounds;
                RectangleF blockBounds = vpBounds;
                if (info != null && info.RuntimeLayoutType == TitleLineLayoutType.FreeText)
                {
                    float bw = GraphicsUnitConvert.Convert(info.BlockWidth, GraphicsUnit.Document, g.PageUnit);
                    txtBounds.Width -= bw + 2;
                    txtBounds.Offset(bw + 2, 0);
                    blockBounds = new RectangleF(
                        vpBounds.Left,
                        vpBounds.Top,
                        bw,
                        vpBounds.Height);
                }
                if (vpBounds.IsEmpty == false)
                {
                    RectangleF visibleBkBounds = RectangleF.Intersect(vpBounds, clipRectangle);
                    if (visibleBkBounds.IsEmpty == false)
                    {
                        if (this._LastDrawViewBounds != null)
                        {
                            this._LastDrawViewBounds[vp] = visibleBkBounds;
                        }
                        blockBounds = RectangleF.Intersect(blockBounds, clipRectangle);
                        if (showBackColor
                            && blockBounds.IsEmpty == false
                            && bkColor.A != 0
                            && bkColor.ToArgb() != Color.White.ToArgb())
                        {
                            // 背景色不是全透明或者纯白色则填充
                            using (SolidBrush brush = new SolidBrush(bkColor))
                            {
                                g.FillRectangle(brush, blockBounds);
                            }
                        }
                        if (info == null || info.TickLineVisible)
                        {
                            // 绘制色块两边的边框线
                            Color bc = System.Windows.Forms.ControlPaint.Dark(bkColor,0.5f);// DCSoft.Drawing.DrawerUtil.ColorDark(bkColor, 0.5f);// System.Windows.Forms.ControlPaint.Dark(bkColor);
                            using (Pen p = new Pen(bc))
                            {
                                if (info != null && info.RuntimeLayoutType == TitleLineLayoutType.Slant3)
                                {
                                    //绘制单元格对角斜线 201598
                                    if (text.IndexOf("/") > 0)
                                    {
                                        g.DrawLine(
                                            p,
                                            vpBounds.Right,
                                            vpBounds.Top,
                                            vpBounds.Left,
                                            vpBounds.Bottom);
                                    }


                                }

                                if (vpBoundsBack.Left >= dataGridBounds.Left
                                        && vpBoundsBack.Left <= dataGridBounds.Right)
                                {
                                    g.DrawLine(
                                        p,
                                        vpBounds.Left,
                                        vpBounds.Top,
                                        vpBounds.Left,
                                        vpBounds.Bottom);
                                }


                                if (vpBoundsBack.Right >= dataGridBounds.Left
                                    && vpBoundsBack.Right <= dataGridBounds.Right)
                                {
                                    g.DrawLine(
                                        p,
                                        blockBounds.Right,
                                        blockBounds.Top,
                                        blockBounds.Right,
                                        blockBounds.Bottom);
                                }

                            }//using
                        }//if
                        if (string.IsNullOrEmpty(text) == false)
                        {
                            // 绘制文本
                            using (var format = new DCStringFormat())
                            {
                                format.Alignment = StringAlignment.Near;
                                format.LineAlignment = StringAlignment.Center;
                                format.FormatFlags = format.FormatFlags | StringFormatFlags.NoWrap
                                    | StringFormatFlags.FitBlackBox;
                                AddNoClipForLinux(format);
                                format.Trimming = StringTrimming.None;
                                if (info != null && info.ValueTextMultiLine)
                                {
                                    // 多行文本
                                    format.FormatFlags = format.FormatFlags & ~StringFormatFlags.NoWrap;
                                }
                                if (info != null
                                    && info.RuntimeLayoutType == TitleLineLayoutType.FreeText
                                    && bkColor.A != 0)
                                {
                                }
                                else
                                {
                                    bkColor = info == null ? this.Config.ForeColor : info.TextColor;
                                }
                                if (vp.TextColor != Color.Empty)
                                {
                                    bkColor = vp.TextColor;
                                }
                                XFontValue txtFont = labelFont;
                                if (this.IsUseLinkVisualStyle(vp))
                                {
                                    bkColor = Color.Blue;
                                    txtFont = txtFont.Clone();
                                    txtFont.Underline = true;
                                }
                                //字符串分隔血压高压低压
                                if (info != null && info.RuntimeLayoutType == TitleLineLayoutType.Slant3 && text.IndexOf("/") > 0)
                                {

                                    string gaoya = text.Substring(0, text.IndexOf("/"));
                                    string diya = text.Substring(text.IndexOf("/") + 1, text.Length - text.IndexOf("/") - 1);
                                    RectangleF rfgaoya = new RectangleF(0, 0, 0, 0);
                                    RectangleF rfdiya = new RectangleF(0, 0, 0, 0);
                                    if (this.RuntimeViewMode == DocumentViewMode.Normal)
                                    {
                                        if (info.TickStep == 6)
                                        {
                                            rfgaoya = new RectangleF(vp.Left + 2, viewTop - 7, viewWidth, viewHeight);
                                            rfdiya = new RectangleF(vp.Left + 127, viewTop + 24, viewWidth - 61, viewHeight - 22);
                                        }
                                        else
                                        {

                                            rfgaoya = new RectangleF(vp.Left + 16, viewTop - 4, viewWidth, viewHeight);
                                            rfdiya = new RectangleF(vp.Left + 180, viewTop + 26, viewWidth - 61, viewHeight - 22);
                                        }
                                    }
                                    else if (this.RuntimeViewMode == DocumentViewMode.Page)
                                    {
                                        ////TickStep == 6是页眉数据行
                                        if (info.TickStep == 6)
                                        {
                                            rfgaoya = new RectangleF(vp.Left - 1, viewTop - 11, viewWidth, viewHeight);
                                            rfdiya = new RectangleF(vp.Left + 112, viewTop + 24, viewWidth - 61, viewHeight - 22);
                                        }
                                        else
                                        {
                                            rfgaoya = new RectangleF(vp.Left - 3, viewTop - 10, viewWidth, viewHeight);
                                            rfdiya = new RectangleF(vp.Left + 70, viewTop + 26, viewWidth - 61, viewHeight - 22);
                                        }
                                    }
                                    else
                                    {
                                        if (info.TickStep == 6)
                                        {
                                            rfgaoya = new RectangleF(vp.Left + 10, viewTop - 8, viewWidth, viewHeight);
                                            rfdiya = new RectangleF(vp.Left + 154, viewTop + 24, viewWidth - 61, viewHeight - 22);
                                        }
                                        else
                                        {
                                            rfgaoya = new RectangleF(vp.Left - 3, viewTop - 10, viewWidth, viewHeight);
                                            rfdiya = new RectangleF(vp.Left + 70, viewTop + 26, viewWidth - 61, viewHeight - 22);
                                        }
                                    }

                                    g.DrawString(
                                        diya,
                                        txtFont,
                                        GetRuntimeForeColor(bkColor),
                                        rfdiya,
                                        format);
                                    g.DrawString(
                                        gaoya,
                                        txtFont,
                                        GetRuntimeForeColor(bkColor),
                                        rfgaoya,
                                        format);

                                }
                                else
                                {
                                    if (vp.Superscript == true)
                                    {
                                        txtFont.Size = txtFont.Size / 3 * 2;
                                        format.LineAlignment = StringAlignment.Near;
                                    }

                                    g.DrawString(
                                            text,
                                            txtFont,
                                            GetRuntimeForeColor(bkColor),
                                            txtBounds,
                                            format);
                                }


                            }//using
                        }
                    }
                }
                lastValuePoint = vp;
            }//foreach
            //tick = CountDown.GetTickCountFloat() - tick;
        }


        /// <summary>
        /// 绘制网格线
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dataGridBounds"></param>
        /// <param name="clipRectangle">剪切矩形</param>
        private void DrawGrid2(
            DCGraphicsForTimeLine g,
            RectangleF dataGridBounds,
            RectangleF clipRectangle)
        {
            //g.FillRectangle(Brushes.Yellow, dataGridBounds);
            //return;
            int xNum = this.RuntimeNumOfDaysInOnePage * this.Config.Ticks.Count;
            int yNum = this.Config.GridYSplitInfo.GridYSplitNum * this.Config.GridYSplitInfo.GridYSpaceNum;
            //float tick = CountDown.GetTickCountFloat();
            if (clipRectangle.IsEmpty)
            {
                clipRectangle = dataGridBounds;
            }
            //RectangleF visibleBounds = RectangleF.Intersect(dataGridBounds, clipRectangle);
            if (this.Config.Zones != null)
            {
                foreach (TimeLineZoneInfo zone in this.Config.Zones)
                {
                    zone.FirstTickItem = null;
                    zone.LastTickItem = null;
                    zone.Left = 0;
                    zone.Top = dataGridBounds.Top;
                    zone.Width = 0;
                    zone.Height = dataGridBounds.Height;
                    zone.ExpandedHandleBounds = RectangleF.Empty;
                }
            }
            // 绘制竖的网格线 |||||||||||||||||||||||||||||||||||||||||||||||||||||||
            RuntimeTickInfo lastItem = this._RuntimeTicks[0];
            TimeLineZoneInfo lastZone = lastItem.Zone;
            List<float> bigVerGridPos = new List<float>();
            for (int iCount = 0; iCount < this._RuntimeTicks.Count; iCount++)
            {

                RuntimeTickInfo item = this._RuntimeTicks[iCount];
                if (item.Zone != null)
                {
                    if (item.Zone.FirstTickItem == null)
                    {
                        item.Zone.FirstTickItem = item;
                        item.Zone.Left = dataGridBounds.Left + item.Left;
                    }
                    item.Zone.LastTickItem = item;
                    item.Zone.Width = dataGridBounds.Left + item.Left - item.Zone.Left + item.Width;
                }
                if (item.Zone != lastZone || iCount == this._RuntimeTicks.Count - 1)
                {
                    Color backColor = this.Config.GridBackColor;
                    Color lineColor = GetRuntimeForeColor(this.Config.GridLineColor);
                    DashStyle lineStyle = DashStyle.Solid;
                    if (lastZone != null)
                    {
                        backColor = lastZone.BackColor;
                        lineColor = lastZone.GridLineColor;
                        lineStyle = lastZone.GridLineStyle;
                    }
                    RectangleF localBounds = new RectangleF(
                        dataGridBounds.Left + lastItem.Left,
                        dataGridBounds.Top,
                        item.Left - lastItem.Left,
                        dataGridBounds.Height);
                    int endIndex = iCount - 1;
                    RuntimeTickInfo nextItem = this._RuntimeTicks.SafeGetItem(iCount + 1);
                    if (nextItem != null && lastZone != null && nextItem.Zone != null)
                    {
                        endIndex = iCount;
                    }
                    if (item.Zone == lastZone)
                    {
                        endIndex = iCount;
                        localBounds.Width = item.Left - lastItem.Left + item.Width;
                    }
                    RectangleF visibleBounds = RectangleF.Intersect(localBounds, clipRectangle);
                    if (visibleBounds.IsEmpty)
                    {
                        lastZone = item.Zone;
                        lastItem = item;
                        continue;
                    }
                    if (backColor.A != 0)
                    {
                        // 填充背景
                        g.FillRectangle(backColor, visibleBounds);
                    }
                    using (var p = new Pen(lineColor, GetRuntimeLineWidth(1)))//这边加了后面的参数，横 竖线都加粗了
                    {
                        p.DashStyle = lineStyle;
                        int startIndex = this._RuntimeTicks.IndexOf(lastItem);
                        bool drawFirstLine = true;
                        if (startIndex > 0)
                        {
                            RuntimeTickInfo preItem = this._RuntimeTicks[startIndex - 1];
                            int zoneIndex1 = preItem.Zone == null ? -1 : preItem.Zone.ZoneIndex;
                            RuntimeTickInfo item2 = this._RuntimeTicks[startIndex];
                            int zoneIndex2 = item2.Zone == null ? -1 : item2.Zone.ZoneIndex;
                            if (zoneIndex2 >= zoneIndex1)
                            {
                                drawFirstLine = true;
                            }
                            else
                            {
                                drawFirstLine = false;
                            }
                        }
                        for (int iCount2 = startIndex; iCount2 <= endIndex; iCount2++)
                        {

                            // 绘制竖的网格线
                            RuntimeTickInfo localItem = this._RuntimeTicks[iCount2];
                            if (p.DashStyle != lineStyle)
                            {
                                p.DashStyle = lineStyle;
                            }
                            if (iCount2 > 0)
                            {
                                RuntimeTickInfo preItem = this._RuntimeTicks[iCount2 - 1];
                                if (preItem.Zone != localItem.Zone)
                                {
                                    p.DashStyle = DashStyle.Solid;
                                }
                            }
                            RuntimeTickInfo nextItem2 = this._RuntimeTicks.SafeGetItem(iCount2 + 1);
                            if (nextItem2 != null && nextItem2.Zone != localItem.Zone)
                            {
                                p.DashStyle = DashStyle.Solid;
                                g.DrawLine(
                                    p,
                                    dataGridBounds.Left + localItem.Left + localItem.Width,
                                    visibleBounds.Top,
                                    dataGridBounds.Left + localItem.Left + localItem.Width,
                                    visibleBounds.Bottom);
                                p.DashStyle = lineStyle;
                                //p.DashStyle = DashStyle.Solid;
                            }
                            //if (localItem.Zone != null 
                            //    && localItem.Zone.Name == "oper"
                            //    && nextItem2 != null 
                            //    && nextItem2.Zone != localItem.Zone)
                            //{
                            //    p.DashStyle = DashStyle.Solid;
                            //}
                            float pos = dataGridBounds.Left + localItem.Left;
                            if (pos > visibleBounds.Right + 1)
                            {
                                break;
                            }
                            if (pos + localItem.Width < visibleBounds.Left)
                            {
                                continue;
                            }
                            if (iCount2 == startIndex && drawFirstLine == false)
                            {
                            }
                            else
                            {
                                g.DrawLine(
                                    p,
                                    pos,
                                    visibleBounds.Top,
                                    pos,
                                    visibleBounds.Bottom);
                            }
                            if (iCount2 == endIndex)
                            {
                                g.DrawLine(
                                    p,
                                    pos + localItem.Width,
                                    visibleBounds.Top,
                                    pos + localItem.Width,
                                    visibleBounds.Bottom);
                            }
                            if (localItem.Zone == null && localItem.TickIndex == 0 && iCount2 != startIndex)//songjianming修改主网格线绘制
                            //if (localItem.Zone == null && localItem.TickIndex == 0)
                            {
                                // 绘制主网格竖线
                                bigVerGridPos.Add(pos);
                            }//if
                        }//for
                        // 绘制横的网格线 ==================================================
                        p.DashStyle = lineStyle;
                        // 获得红线坐标序列
                        List<float> redLinePositions = new List<float>();
                        foreach (YAxisInfo info in this.YAxisInfos)
                        {
                            if (info.Visible && info.ValueVisible)
                            {
                                if (TemperatureDocument.IsNullValue(info.RedLineValue) == false)
                                {
                                    float pos = info.GetDisplayY(this, dataGridBounds, info.RedLineValue);// rect.Top + rect.Height * (1.0f - info.GetDisplayScaleRate(info.RedLineValue));
                                    redLinePositions.Add(pos);
                                }
                            }//if
                        }//foreach
                        yNum = (int)(this.Config.GridYSplitInfo.GridYSplitNum * this.Config.GridYSplitInfo.GridYSpaceNum / (1f - this.Config.DataGridBottomPadding - this.Config.DataGridTopPadding));

                        int yNumForTop = (int)(yNum * this.Config.DataGridTopPadding);
                        int yNumForBottom = (int)(yNum * this.Config.DataGridBottomPadding);
                        float endPos = dataGridBounds.Top + dataGridBounds.Height * (1f - this.Config.DataGridBottomPadding);
                        float startPos = dataGridBounds.Top + dataGridBounds.Height * this.Config.DataGridTopPadding;

                        float dataGridHeight = dataGridBounds.Height / yNum;
                        if (this._RuntimeTicksForVerticalDataGrid == null)
                        {
                            this._RuntimeTicksForVerticalDataGrid = new RuntimeTickInfoList();
                        }
                        else
                        {
                            this._RuntimeTicksForVerticalDataGrid.Clear();
                        }
                        int tickindex = 0;
                        for (int y = 0; y <= yNum; y++)
                        {
                            float pos = dataGridBounds.Top + dataGridBounds.Height * y / yNum;
                            RuntimeTickInfo tick = new RuntimeTickInfo();
                            tick.Left = pos; //用Left记录Top
                            tick.Width = dataGridHeight;  //用Width记录高度
                            tick.Index = tickindex;
                            this._RuntimeTicksForVerticalDataGrid.Add(tick);
                            tickindex++;
                        }
                        //////////////////////////////////////

                        for (int y = 1; y <= yNum; y++)
                        {
                            float pos = dataGridBounds.Top + dataGridBounds.Height * y / yNum;
                            if (pos > visibleBounds.Bottom)
                            {
                                break;
                            }
                            if (pos < visibleBounds.Top)
                            {
                                continue;
                            }
                            //g.DrawLine(
                            //    p,
                            //    visibleBounds.Left,
                            //    pos,
                            //    visibleBounds.Right,
                            //    pos);
                            bool matchRedLine = false;
                            foreach (float v in redLinePositions)
                            {
                                if (Math.Abs(v - pos) < 0.05)
                                {
                                    matchRedLine = true;
                                    break;
                                }
                            }
                            if (matchRedLine)
                            {
                                // 命中某个红色横线位置，不处理
                                continue;
                            }

                            if (/*pos >= startPos && pos <= endPos && */y >= yNumForTop && y <= (yNum - yNumForBottom) && ((y - yNumForTop) % this.Config.GridYSplitInfo.GridYSpaceNum) == 0)
                            {
                                DashStyle back = p.DashStyle;
                                if (back != DashStyle.Solid)
                                {
                                    p.Width = DCGraphicsForTimeLine.ConvertPenWidth(this.Config.GridYSplitInfo.ThinLineWidth, GraphicsUnit.Pixel, g.PageUnit);// this.Config.GridYSplitInfo.ThinLineWidth;
                                    p.DashStyle = DashStyle.Solid;
                                    g.DrawLine(
                                        p,
                                        visibleBounds.Left,
                                        pos,
                                        visibleBounds.Right,
                                        pos);
                                    p.DashStyle = back;
                                }
                                else
                                {
                                    //System.Windows.Forms.MessageBox.Show(y.ToString());
                                    p.Width = DCGraphicsForTimeLine.ConvertPenWidth(this.Config.GridYSplitInfo.ThickLineWidth, GraphicsUnit.Pixel, g.PageUnit);
                                    g.DrawLine(
                                        p,
                                        visibleBounds.Left,
                                        pos,
                                        visibleBounds.Right,
                                        pos);

                                    //float dis = GraphicsUnitConvert.Convert(1, GraphicsUnit.Pixel , this.GraphicsUnit );
                                    //g.DrawLine(
                                    //    p,
                                    //    visibleBounds.Left,
                                    //    pos + dis ,
                                    //    visibleBounds.Right,
                                    //    pos + dis);
                                }
                            }
                            else
                            {
                                p.Width = DCGraphicsForTimeLine.ConvertPenWidth(this.Config.GridYSplitInfo.ThinLineWidth, GraphicsUnit.Pixel, g.PageUnit); //this.Config.GridYSplitInfo.ThinLineWidth;
                                g.DrawLine(
                                    p,
                                    visibleBounds.Left,
                                    pos,
                                    visibleBounds.Right,
                                    pos);
                            }


                        }//for
                    }//using
                    lastZone = item.Zone;
                    lastItem = item;
                }//if
            }//for
            if (this.Config.AllowUserCollapseZone)
            {
                if (this.Config.Zones != null)
                {
                    var ci = XImageValue.ConvertToBitmap( DCTimeLineImageResources.Collapse );
                    var ei = XImageValue.ConvertToBitmap( DCTimeLineImageResources.Expand );
                    SizeF imgSize = GraphicsUnitConvert.Convert(
                        new SizeF(ci.Width, ci.Height),
                        GraphicsUnit.Pixel,
                        g.PageUnit);
                    float padding = GraphicsUnitConvert.Convert(3, GraphicsUnit.Pixel, g.PageUnit);
                    foreach (TimeLineZoneInfo zone in this.Config.Zones)
                    {
                        if (zone.FirstTickItem != null)
                        {
                            RectangleF ir = new RectangleF(
                                dataGridBounds.Left + zone.FirstTickItem.Left + padding,
                                dataGridBounds.Top + padding,
                                imgSize.Width,
                                imgSize.Height);
                            zone.ExpandedHandleBounds = ir;
                            var img = zone.IsExpanded ? ei : ci;
                            if (clipRectangle.IntersectsWith(ir))
                            {
                                DrawerUtil.DrawImageUnscaledNearestNeighbor(g, img, (int)ir.Left, (int)ir.Top);

                                //InterpolationMode ipm = g.InterpolationMode;
                                //PixelOffsetMode pom = g.PixelOffsetMode;
                                //g.InterpolationMode = InterpolationMode.NearestNeighbor;// .NearestNeighbor;
                                //g.PixelOffsetMode = PixelOffsetMode.Half;
                                //g.DrawImageUnscaled(img, (int)ir.Left, (int)ir.Top);
                                ////g.DrawImage(img, bounds, new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
                                //g.InterpolationMode = ipm;
                                //g.PixelOffsetMode = pom;

                                //g.DrawImageUnscaled(img, (int)ir.Left, (int)ir.Top);
                                //g.DrawImage(img, ir);
                            }
                        }
                    }
                }
            }
            if (bigVerGridPos.Count > 0)
            {
                for (int iCount = 0; iCount < bigVerGridPos.Count; iCount++)
                {
                    float pos = bigVerGridPos[iCount];
                    using (Pen p = new Pen(this.Config.BigVerticalGridLineColor, this.Config.BigVerticalGridLineWidth))
                    {
                        g.DrawLine(
                            p,
                            pos,
                            dataGridBounds.Top,
                            pos,
                            dataGridBounds.Bottom);
                    }
                    //g.DrawLine(GraphicsObjectBuffer.GetPen(this.Config.BigVerticalGridLineColor),
                    //    pos,
                    //    dataGridBounds.Top,
                    //    pos,
                    //    dataGridBounds.Bottom);
                }
            }
        }

        
        /// <summary>
        /// 向上的箭头图标
        /// </summary>
        private static Bitmap bmpArrowUp = null;
        /// <summary>
        /// 向下的箭头图标
        /// </summary>
        private static Bitmap bmpArrowDown = null;

        /// <summary>
        /// 绘制带箭头的数值
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="arrowUp"></param>
        /// <param name="vp"></param>
        /// <param name="textBackcolor">文本背景色</param>
        private void DrawValueArrow(DCGraphicsForTimeLine g, float x, float y, bool arrowUp, ValuePoint vp, Color textBackcolor)
        {
            if (bmpArrowDown == null)
            {
                bmpArrowDown = XImageValue.ConvertToBitmap( DCTimeLineImageResources.ArrowDown );
                bmpArrowDown.MakeTransparent(Color.Red);
                //Console.WriteLine("rrrrrrrrrr " + Convert.ToBase64String( bmpArrowDown.Data));
                bmpArrowUp = XImageValue.ConvertToBitmap( DCTimeLineImageResources.ArrowUp );
                bmpArrowUp.MakeTransparent(Color.Red);
            }
            SizeF imgSize = GraphicsUnitConvert.Convert(
                new SizeF(bmpArrowUp.Width, bmpArrowUp.Height),
                GraphicsUnit.Pixel,
                g.PageUnit);
            float symbolSize = GraphicsUnitConvert.Convert(
                this.SymbolSize, GraphicsUnit.Document, g.PageUnit);
            string text = vp.Text;
            if (string.IsNullOrEmpty(text))
            {
                text = vp.ValueString;
            }
            XFontValue f = CreateFont();
            using (var format = new DCStringFormat())
            {
                format.FormatFlags = StringFormatFlags.NoWrap
                    | StringFormatFlags.DirectionVertical
                    | StringFormatFlags.NoClip;
                format.Trimming = StringTrimming.None;
                SizeF txtSize = g.MeasureString(text, f, 1000, format);
                if (arrowUp)
                {
                    // 在数据点的下面绘制图形
                    DrawerUtil.DrawImageUnscaledNearestNeighbor(
                        g,
                        bmpArrowUp,
                        (int)(x - imgSize.Width / 2),
                        (int)(y + symbolSize * 1));
                    RectangleF txtBounds = new RectangleF(
                        x - txtSize.Width / 2,
                        (int)(y + symbolSize * 1 + imgSize.Height * 1.1),
                        txtSize.Width + 1,
                        txtSize.Height);
                    if (textBackcolor.A != 0)
                    {
                        g.FillRectangle(textBackcolor,
                            txtBounds);
                    }
                    g.DrawString(
                        text,
                        f,
                        this.Config.ForeColor,
                        txtBounds,
                        format);
                }//if
                else
                {
                    // 在数据点的上面绘制图形
                    DrawerUtil.DrawImageUnscaledNearestNeighbor(
                        g,
                        bmpArrowDown,
                        (int)(x - imgSize.Width / 2),
                        (int)(y - symbolSize * 1 - imgSize.Height));
                    RectangleF txtBounds = new RectangleF(
                        x - txtSize.Width / 2,
                        (int)(y - symbolSize * 1.0 - imgSize.Height * 1.1 - txtSize.Height),
                        txtSize.Width + 1,
                        txtSize.Height);
                    g.FillRectangle(Color.White, txtBounds);
                    g.DrawString(
                        text,
                        f,
                        this.RuntimeForeColor,
                        txtBounds,
                        format);
                }
            }//using
        }
#if !DCWriterForWASM
        internal Bitmap CreateSymbolIcon(YAxisInfo info)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                float size = GraphicsUnitConvert.Convert(16, GraphicsUnit.Pixel, GraphicsUnit.Document);
                g.PageUnit = GraphicsUnit.Document;
                DrawSymbol(
                    new DCGraphicsForTimeLine(g),
                    new RectangleF(0, 0, size, size),
                    size / 2,
                    size / 2,
                    info.SymbolStyle,
                    info.CharacterForCharSymbolStyle,
                    info.SymbolColor,
                    null,
                    size / 2,
                    false);
                //g.DrawRectangle(Pens.Black,  0, 0, size-1, size -1);
            }
            return bmp;
        }
#endif
        /// <summary>
        /// 绘制数据点符号
        /// </summary>
        /// <param name="g">画布对象</param>
        /// <param name="clipRectangle">剪切矩形</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="style">样式</param>
        /// <param name="charForCharSymbol">当样式为字符时使用的字符变量</param>
        /// <param name="color">颜色</param>
        /// <param name="vp">数据点对象</param>
        /// <param name="specifySymbolSize">指定的图例大小</param>
        /// <param name="isForLantern">指定此数据点符号是否灯笼符号</param>
        private void DrawSymbol(
            DCGraphicsForTimeLine g,
            RectangleF clipRectangle,
            float x,
            float y,
            ValuePointSymbolStyle style,
            char charForCharSymbol, //添加一个参数用于当ValuePointSymbolStyle为字符或套圈字符时传递字符
            Color color,
            ValuePoint vp,
            float specifySymbolSize,
            bool isForLantern,
            bool isForVerified = false)
        {
            color = GetRuntimeForeColor(color);
            if (/*this.PrintingMode == false &&*/ vp != null && vp.Color != color && vp.Color != Color.Transparent)
            {
                color = vp.Color;
            }
            float ssize = GraphicsUnitConvert.Convert(
                float.IsNaN(specifySymbolSize) ? this.SymbolSize : specifySymbolSize,
                GraphicsUnit.Document,
                g.PageUnit);

            float runtimeSymbolOffsetX = 0f;
            float runtimeSymbolOffsetY = 0f;
            if (vp != null && vp.Parent is YAxisInfo)
            {
                YAxisInfo tempy = vp.Parent as YAxisInfo;
                runtimeSymbolOffsetX = vp.SymbolOffsetX == 0f ? tempy.SymbolOffsetX : vp.SymbolOffsetX;
                runtimeSymbolOffsetY = vp.SymbolOffsetY == 0f ? tempy.SymbolOffsetY : vp.SymbolOffsetY;
            }
            RectangleF rect = new RectangleF(
                x - ssize / 2 + runtimeSymbolOffsetX,
                y - ssize / 2 + runtimeSymbolOffsetY,
                ssize,
                ssize);
            float pw = DCGraphicsForTimeLine.ConvertPenWidth(2, GraphicsUnit.Pixel, g.PageUnit);

            string charstr = charForCharSymbol.ToString();
            if (vp != null && vp.SpecifySymbolStyle != ValuePointSymbolStyle.Default)
            {
                charstr = vp.CharacterForCharSymbolStyle.ToString();
            }

            //根据上面的rect调整出需要画字符的实际rect，由于同字号的不同的字符打出来的大小不同，所以得先判断大小。
            XFontValue font = new XFontValue();
            font.Name = XFontValue.DefaultFontName;// System.Drawing.SystemFonts.DefaultFont.Name;
            font.Size = 8;
            SizeF sizeofstr = g.MeasureString(charstr, font);
            RectangleF charrect = new RectangleF(
                x - sizeofstr.Width / 2,
                y - sizeofstr.Height / 2,
                sizeofstr.Width,
                sizeofstr.Height);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            if (vp != null && isForLantern == false)
            {
                vp.ViewBounds = rect;
            }
            else
            {
                //防御：创建个虚拟的VP
                vp = new ValuePoint();
                vp.ViewBounds = rect;
            }

            if (clipRectangle.IsEmpty || clipRectangle.IntersectsWith(vp.ViewBounds) == false || (vp._ConveredPoint != null && this.Config.EnableCustomValuePointSymbol))
            {
                // 不在剪切矩形范围中,或者点被覆盖重叠（先前重叠点已经绘制过了）
                return;
            }
            if (this._LastDrawViewBounds != null && vp != null)
            {
                this._LastDrawViewBounds[vp] = vp.ViewBounds;
            }
            if (vp != null && vp.SpecifySymbolStyle != ValuePointSymbolStyle.Default
                && isForVerified == false)
            {
                if (isForLantern)
                {
                    charstr = vp.CharacterForLanternSymbolStyle.ToString();
                    style = vp.SpecifyLanternSymbolStyle;
                }
                else
                {
                    charstr = vp.CharacterForCharSymbolStyle.ToString();
                    style = vp.SpecifySymbolStyle;
                }
            }

            if (vp._CoincidePoint != null && this.Config.EnableCustomValuePointSymbol == true)
            {
                //该数据点有重合点，将style设置成custom让客户自己去画
                style = ValuePointSymbolStyle.Custom;
            }

            if (vp != null && vp.CustomImage != null)
            {
                g.DrawImage(vp.CustomImage.Value, new Rectangle((int)(vp.ViewBounds.Left), (int)(vp.ViewBounds.Top), (int)(vp.CustomImage.Width * 3), (int)(vp.CustomImage.Height * 3)));
                return;
            }
            if (style == ValuePointSymbolStyle.None)
            {
                // 没有图形
                return;
            }

            //这里添加一段逻辑，将Y轴数据点的所属页面属性给填充一下
            vp._OwnerPageIndex = this.RuntimePageIndex;

            SmoothingMode back = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            switch (style)
            {
                case ValuePointSymbolStyle.SolidCicle:
                    using (SolidBrush b = new SolidBrush(color))
                    {
                        g.FillEllipse(
                            b,
                            vp.ViewBounds);
                    }
                    break;
                case ValuePointSymbolStyle.HollowCicle:
                    using (Pen p = new Pen(color, pw ))
                    {
                        g.DrawEllipse(
                             p,
                             vp.ViewBounds);

                    }
                    break;
                case ValuePointSymbolStyle.OpaqueHollowCicle:
                    using (Pen p = new Pen(color, pw ))
                    {
                        Color cc = this.Config.GridBackColor == Color.Transparent ? Color.White : this.Config.GridBackColor;
                        using (var bb = new SolidBrush(cc))
                        {
                            g.FillEllipse(
                                bb,
                                vp.ViewBounds);
                        }
                        g.DrawEllipse(
                             p,
                             vp.ViewBounds);
                    }
                    break;
                case ValuePointSymbolStyle.Cross:
                    using (Pen p = new Pen(color, pw))
                    {
                        g.DrawLine(p, vp.ViewBounds.Left, vp.ViewBounds.Top, vp.ViewBounds.Right, vp.ViewBounds.Bottom);
                        g.DrawLine(p, vp.ViewBounds.Left, vp.ViewBounds.Bottom, vp.ViewBounds.Right, vp.ViewBounds.Top);
                    }
                    break;
                case ValuePointSymbolStyle.CrossCircle:
                    using (Pen p = new Pen(color, pw / 3))
                    {
                        g.DrawLine(p, vp.ViewBounds.Left, vp.ViewBounds.Top, vp.ViewBounds.Right, vp.ViewBounds.Bottom);
                        g.DrawLine(p, vp.ViewBounds.Left, vp.ViewBounds.Bottom, vp.ViewBounds.Right, vp.ViewBounds.Top);
                        g.DrawEllipse(
                             p,
                             vp.ViewBounds);
                    }
                    break;
                case ValuePointSymbolStyle.Square:
                    {
                        // 正方形
                        using (SolidBrush b = new SolidBrush(color))
                        {
                            g.FillRectangle(b, vp.ViewBounds);
                        }
                    }
                    break;
                case ValuePointSymbolStyle.HollowSquare:
                    {
                        // 空心正方形
                        using (Pen p = new Pen(color, pw))
                        {
                            g.DrawRectangle(p, vp.ViewBounds.Left, vp.ViewBounds.Top, vp.ViewBounds.Width, vp.ViewBounds.Height);
                        }
                    }
                    break;
                case ValuePointSymbolStyle.Diamond:
                    {
                        // 菱形
                        PointF[] ps = new PointF[]
                            {
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Top ),
                                new PointF( vp.ViewBounds.Right , vp.ViewBounds.Top + vp.ViewBounds.Height /2 ),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Bottom ),
                                new PointF( vp.ViewBounds.Left , vp.ViewBounds.Top + vp.ViewBounds.Height /2 ),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Top )
                            };
                        using (SolidBrush b = new SolidBrush(color))
                        {
                            g.FillPolygon(b, ps);
                        }
                    }
                    break;
                case ValuePointSymbolStyle.HollowDiamond:
                    {
                        // 空心菱形
                        PointF[] ps = new PointF[]
                            {
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Top ),
                                new PointF( vp.ViewBounds.Right , vp.ViewBounds.Top + vp.ViewBounds.Height /2 ),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Bottom ),
                                new PointF( vp.ViewBounds.Left , vp.ViewBounds.Top + vp.ViewBounds.Height /2 ),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Top )
                            };
                        using (Pen p = new Pen(color, pw))
                        {
                            g.DrawPolygon(p, ps);
                        }
                    }
                    break;
                case ValuePointSymbolStyle.V:
                    {
                        Color colorForV = vp == null ? color : (isForVerified ? vp.VerifiedColor : color);
                        float shift = vp.ViewBounds.Width / 2;
                        float shiftForV = 0f;
                        if (vp.VerifiedAlignment == StringAlignment.Near)
                        {
                            shiftForV = shift * (-1);
                        }
                        else if (vp.VerifiedAlignment == StringAlignment.Far)
                        {
                            shiftForV = shift;
                        }

                        PointF[] ps = new PointF[]
                            {
                                new PointF( vp.ViewBounds.Left + shiftForV, vp.ViewBounds.Top ),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 + shiftForV , vp.ViewBounds.Top + vp.ViewBounds.Height / 2 ),
                                new PointF( vp.ViewBounds.Right + shiftForV , vp.ViewBounds.Top )
                            };
                        using (Pen p = new Pen(colorForV, pw))
                        {
                            g.DrawLines(p, ps);
                        }
                    }
                    break;
                case ValuePointSymbolStyle.VReversed:
                    {
                        PointF[] ps = new PointF[]
                            {
                                new PointF( vp.ViewBounds.Left , vp.ViewBounds.Bottom ),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Top + vp.ViewBounds.Height / 2 ),
                                new PointF( vp.ViewBounds.Right , vp.ViewBounds.Bottom )
                            };
                       using (Pen p = new Pen(color, pw))
                        {
                            g.DrawLines(p, ps);
                        }
                    }
                    break;

                case ValuePointSymbolStyle.HollowTriangleReversed://V空心倒三角
                    {
                        PointF[] ps = new PointF[]
                            {
                                new PointF( vp.ViewBounds.Left - vp.ViewBounds.Width / 4 , vp.ViewBounds.Top - vp.ViewBounds.Height / 4 ),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Top + vp.ViewBounds.Height / 2 ),
                                new PointF( vp.ViewBounds.Right + vp.ViewBounds.Width / 4, vp.ViewBounds.Top - vp.ViewBounds.Height / 4)
                            };
                        using (Pen p = new Pen(color, pw / 2))
                        {
                            //g.DrawLines(color, pw, ps);
                            GraphicsPath path = new GraphicsPath();
                            path.AddLines(ps);
                            path.CloseFigure();
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case ValuePointSymbolStyle.HollowTriangle://VReversed空心正三角
                    {
                        PointF[] ps = new PointF[]
                            {
                                new PointF( vp.ViewBounds.Left - vp.ViewBounds.Width / 4 , vp.ViewBounds.Bottom + vp.ViewBounds.Height / 4),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Top + vp.ViewBounds.Height / 2 ),
                                new PointF( vp.ViewBounds.Right + vp.ViewBounds.Width / 4 , vp.ViewBounds.Bottom + + vp.ViewBounds.Height / 4 )
                            };
                        using (Pen p = new Pen(color, pw/2))
                        {
                            //g.DrawLines(color, pw, ps);
                            GraphicsPath path = new GraphicsPath();
                            path.AddLines(ps);
                            path.CloseFigure();
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case ValuePointSymbolStyle.SolidTriangleReversed://V实心倒三角
                    {
                        PointF[] ps = new PointF[]
                            {
                                new PointF( vp.ViewBounds.Left - vp.ViewBounds.Width / 4 , vp.ViewBounds.Top - vp.ViewBounds.Height / 4 ),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Top + vp.ViewBounds.Height / 2 ),
                                new PointF( vp.ViewBounds.Right + vp.ViewBounds.Width / 4, vp.ViewBounds.Top - vp.ViewBounds.Height / 4)
                            };
                        //using (Pen p = new Pen(color, pw))
                        {
                            //g.DrawLines(color, pw, ps);
                            GraphicsPath path = new GraphicsPath();
                            path.AddLines(ps);
                            path.CloseFigure();
                            g.FillPath(color, path);

                        }
                    }
                    break;
                case ValuePointSymbolStyle.SolidTriangle://VReversed实心正三角
                    {
                        PointF[] ps = new PointF[]
                            {
                                new PointF( vp.ViewBounds.Left - vp.ViewBounds.Width / 4 , vp.ViewBounds.Bottom + vp.ViewBounds.Height / 4),
                                new PointF( vp.ViewBounds.Left + vp.ViewBounds.Width / 2 , vp.ViewBounds.Top + vp.ViewBounds.Height / 2 ),
                                new PointF( vp.ViewBounds.Right + vp.ViewBounds.Width / 4 , vp.ViewBounds.Bottom + + vp.ViewBounds.Height / 4 )
                            };
                        //using (Pen p = new Pen(color, pw))
                        {
                            //g.DrawLines(color, pw, ps);
                            GraphicsPath path = new GraphicsPath();
                            path.AddLines(ps);
                            path.CloseFigure();
                            g.FillPath(color, path);
                        }
                    }
                    break;



                case ValuePointSymbolStyle.Character:
                    {
                        //g.FillRectangle(new SolidBrush(Color.Yellow), charrect);

                        g.DrawString(
                            charstr,
                            font,
                            GetRuntimeForeColor(color),
                            charrect,
                            DCStringFormat.GenericTypographic
                            );
                    }
                    break;
                case ValuePointSymbolStyle.CharacterCircle:
                    {
                        //先画字符
                        g.DrawString(
                            charstr,
                            new XFontValue(),
                            GetRuntimeForeColor(color),
                            charrect,
                            DCStringFormat.GenericTypographic
                            );
                        //画套圈字符，精细调整圈的位置
                        float circleSizeWidth = Math.Min(g.GetFontHeight(font), charrect.Width);
                        float circleSizeHeight = Math.Min(g.GetFontHeight(font), charrect.Height);
                        RectangleF circelBounds = new RectangleF(
                            charrect.Left - circleSizeWidth / 4 - 1,
                            vp.ViewBounds.Top - circleSizeHeight / 4 - 1,
                            circleSizeWidth + 2,
                            circleSizeHeight + 2);
                        SmoothingMode back2 = g.SmoothingMode;
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.DrawEllipse(
                            color,
                            circelBounds);
                        g.SmoothingMode = back2;
                    }
                    break;
#if ! DCWriterForWASM
                case ValuePointSymbolStyle.Custom:
                    {
                        if (this.EventDrawValuePointSymbol != null)
                        {
                            DrawValuePointSymbolEventArgs args = new DrawValuePointSymbolEventArgs();
                            args.Document = this;
                            args.Graphic = g;
                            args.valuePoint = vp;
                            args.ReferRect = rect;
                            this.EventDrawValuePointSymbol(this, args);
                        }
                    }
                    break;
#endif
            }
            g.SmoothingMode = back;
        }
    }
}