using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using DCSoft.Drawing;
using System.Text.RegularExpressions;

namespace DCSoft.TemperatureChart
{
    [System.Runtime.InteropServices.ComVisible(false)]
    public sealed class DCGraphicsForTimeLine : DCSoft.Drawing.DCGraphics
    {
        /// <summary>
        ///  是否处于LINUX运行模式
        /// </summary>
        internal static bool TimeLineRunInLinuxMode =   DCSoft.Common.DebugHelper.IsLinuxOrUnixPlatform;
#if !DCWriterForWASM
        public DCGraphicsForTimeLine(Graphics g) : base(g)
        {

        }
#endif
        public DCGraphicsForTimeLine(DCSoft.Drawing.DCGraphics g) : base(g)
        {

        }

        public SizeF MeasureStringForTimeLine(string text, XFontValue font, int width, DrawStringFormatExt format)
        {
            if( text == null || text.Length == 0 )
            {
                return SizeF.Empty;
            }
            if (TimeLineRunInLinuxMode)
            {
                if( format.UseAdvancedDirectionVertical )
                {
                    var fontHeight = font.GetHeight(this);
                    return new SizeF(fontHeight, fontHeight * text.Length);
                }
                
            }
            return base.MeasureString(text, font, width, format);
        }

       

        /// <summary>
        /// 为时间轴而绘制文本
        /// </summary>
        /// <param name="s"></param>
        /// <param name="font"></param>
        /// <param name="c"></param>
        /// <param name="rect"></param>
        /// <param name="format"></param>
        public void DrawStringForTimeLine(
            string s,
            XFontValue font,
            Color c,
            RectangleF rect,
            DrawStringFormatExt format)
        {
            if( TimeLineRunInLinuxMode == false )
            {
                base.DrawString(s, font, c, rect, format);
                return;
            }
            lock (typeof(XFontValue))
            {
                if ( s != null && s.Length > 0)
                {
                    List<String> stringList = new List<string>();
                    foreach (char cc in s)
                    {
                        stringList.Add(cc.ToString());
                    }
                    string[] ss = stringList.ToArray();

                    //准备一个不带竖形排列的变量用于单字符绘制
                    DrawStringFormatExt format2 = format.Clone();
                    //从FormatFlags中去除DirectionVertical枚举项
                    if ((format.FormatFlags | StringFormatFlags.DirectionVertical) == format.FormatFlags)
                    {
                        format2.FormatFlags = (StringFormatFlags)((int)format2.FormatFlags - (int)StringFormatFlags.DirectionVertical);
                    }
                    if (ss != null && ss.Length > 0)
                    {
                        RectangleF rectf = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
                        float fontHeight = font.GetHeight(this);
                        var sb = GraphicsObjectBuffer.GetSolidBrush(c);
                        for (int i = 0; i < ss.Length; i++)
                        {
                            string text = ss[i];
                            rectf.Height = fontHeight;
                            if (this.MyMartrix != null)
                            {
                                var rect3 = this.MyMartrix.Transform(rectf);
                                this.DrawString(
                                    text,
                                    font.Value,
                                    sb,
                                    rect3,
                                    format2.Value);
                            }
                            else
                            {
                                this.DrawString(
                                    text,
                                    font.Value,
                                    sb,
                                    rectf,
                                    format2.Value);
                            }
                            rectf.Y = rectf.Y + rectf.Height;

                        }
                    }
                }
            }
        }
      
#if !DCWriterForWASM
        public new SizeF MeasureString(string text, XFontValue font, int width, DrawStringFormatExt format)
        {
            using (StringFormat f = format.CreateStringFormat())
            {
                if (format.UseAdvancedDirectionVertical == true
                    /*&& (f.FormatFlags | StringFormatFlags.DirectionVertical) == f.FormatFlags*/)
                {
                    SizeF size = new SizeF();

                    string[] ss = TransformingStringToArray(text, format.UseAdvancedDirectionVertical2);
                    float heightadd = font.GetHeight(this);
                    if (ss != null && ss.Length > 0)
                    {
                        for (int i = 0; i < ss.Length; i++)
                        {
                            string s = ss[i];
                            if (s.Length > 1)
                            {
                                SizeF tempsf = this.GraphisForMeasure.MeasureString(text, font.Value, width, f);
                                size.Height = size.Height + heightadd;// tempsf.Height;
                                size.Width = Math.Max(size.Width, tempsf.Width);
                            }
                            else
                            {
                                //当字符串只有一个字符时，此时字符串在竖向矩形是倒下来的所以叠加的时候宽和高对倒
                                SizeF tempsf = this.GraphisForMeasure.MeasureString(text, font.Value, width, f);
                                size.Height = size.Height + heightadd;// tempsf.Width;
                                size.Width = Math.Max(size.Width, tempsf.Height);
                            }
                        }
                    }
                    if (this.MyMartrix == null)
                    {
                        return size;
                    }
                    else
                    {
                        RectangleF rectx = new RectangleF(0, 0, size.Width, size.Height);
                        RectangleF rf = this.MyMartrix.Transform(rectx);
                        return new SizeF(rf.Width, rf.Height);
                    }
                }
                else
                {
                    return this.GraphisForMeasure.MeasureString(text, font.Value, width, f);
                }
            }
        }

        public new void DrawString(
           string s,
           XFontValue font,
           Color c,
           RectangleF rect,
           DrawStringFormatExt format)
        {
            lock (typeof(XFontValue))
            {
                if (NativeGraphics != null)
                {
                    if (format.UseAdvancedDirectionVertical == true)
                    {
                        string[] ss = TransformingStringToArray(s, format.UseAdvancedDirectionVertical2);

                        //准备一个不带竖形排列的变量用于单字符绘制
                        DrawStringFormatExt format2 = format.Clone();
                        //从FormatFlags中去除DirectionVertical枚举项
                        if ((format.FormatFlags | StringFormatFlags.DirectionVertical) == format.FormatFlags)
                        {
                            format2.FormatFlags = (StringFormatFlags)((int)format2.FormatFlags - (int)StringFormatFlags.DirectionVertical);
                        }

                        //MeasureSizeForLinux measureHelper = GetMeasureSizeForLinux(font, format);

                        if (ss != null && ss.Length > 0)
                        {
                            RectangleF rectf = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
                            Regex pattern = new Regex("[^0-9a-zA-Z]");
                            for (int i = 0; i < ss.Length; i++)
                            {
                                //在这里判断数组中的每一个解析出来的字符串，若字符串为单字符，则横向绘制不使用竖向模式。
                                //每绘制完一个字符串在绘制下一个字符串时需要重新计算坐标
                                //20181213备注：竖向绘制英文字符在X轴上会有偏移，微调下，但不能影响中文字符
                                string text = ss[i];
                                char firstchar = text[0];
                                bool isEngishChar = pattern.IsMatch(firstchar.ToString()) == true;
                                float verticalAdd = font.GetHeight(this.GraphisForMeasure);// isEngishChar ? (measureHelper.EngHeight + measureHelper.EngTopBottomPadding) : (measureHelper.ChnHeight + measureHelper.ChnTopBottomPadding);

                               
                                {
                                    SizeF sf = this.GraphisForMeasure.MeasureString(text, font.Value, (int)rect.Width, format2.Value);
                                    rectf.Height = sf.Height;
                                    if (this.MyMartrix != null)
                                    {
                                        var rect3 = this.MyMartrix.Transform(rectf);
                                        this.GraphisForMeasure.DrawString(
                                            text,
                                            font.Value,
                                            GraphicsObjectBuffer.GetSolidBrush(c),
                                            rect3,
                                            format2.Value);
                                    }
                                    else
                                    {
                                        this.GraphisForMeasure.DrawString(
                                            text,
                                            font.Value,
                                            GraphicsObjectBuffer.GetSolidBrush(c),
                                            rectf,
                                            format2.Value);
                                    }
                                    rectf.Y = rectf.Y + verticalAdd;
                                }

                            }
                        }
                    }
                    else
                    {
                        if (this.MyMartrix == null)
                        {
                            this.GraphisForMeasure.DrawString(
                                s,
                                font.Value,
                                GraphicsObjectBuffer.GetSolidBrush(c),
                                rect,
                                format.Value);
                        }
                        else
                        {
                            var rect3 = this.MyMartrix.Transform(rect);
                            this.GraphisForMeasure.DrawString(
                                s,
                                font.Value,
                                GraphicsObjectBuffer.GetSolidBrush(c),
                                rect3,
                                format.Value);
                        }
                    }
                }
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(s);
            }
#endif
        }
#endif

#if !DCWriterForWASM
        /// <summary>
        /// 将一个完整的字符串进行分析，解析出其中连续的非ASCII字符，以及连续的ASCII字符，以及单个的ASCII字符，按原顺序拆成字符串数组并返回
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>转换后的字符串数组</returns>
        private string[] TransformingStringToArray(string source, bool useSpecificSplitChar = false)
        {
            if (String.IsNullOrEmpty(source) == false)
            {
                List<String> stringList = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();

                if ( TimeLineRunInLinuxMode )
                {
                    foreach (char c in source)
                    {
                        stringList.Add(c.ToString());
                    }
                    return stringList.ToArray();
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                if (useSpecificSplitChar == true)
                {
                    char splitChar = '^';   //使用^字符做特殊分割识别字符
                    return source.Split(splitChar);
                }

                //暂时先不考虑其它ASCII字符了
                Regex pattern = new Regex("[^0-9a-zA-Z]");
                bool ispreascii = false;
                for (int i = 0; i < source.Length; i++)
                {
                    bool islast = i == source.Length - 1 ? true : false;
                    char c = source[i];

                    if (pattern.IsMatch(c.ToString()))
                    {
                        //非ASCII字符
                        stringBuilder.Append(c);
                        ispreascii = false;
                        if (islast)
                        {
                            stringList.Add(stringBuilder.ToString());
                        }
                    }
                    else
                    {
                        //ASCII字符
                        if (ispreascii == false &&

                            (islast == true || (pattern.IsMatch(source[i + 1].ToString()) == true)))
                        {
                            //出现孤立的ASCII字符
                            stringList.Add(stringBuilder.ToString());
                            stringBuilder = new StringBuilder();
                            stringList.Add(source[i].ToString());
                            ispreascii = true;
                        }
                        else
                        {
                            //虽然是ASCII字符但是是连续的
                            stringBuilder.Append(c);
                            ispreascii = true;
                            if (islast)
                            {
                                stringList.Add(stringBuilder.ToString());
                            }
                        }
                    }
                }

                return stringList.ToArray();
            }
            else
            {
                return null;
            }
        }
       
#endif
    }

    internal class MeasureSizeForLinux
    {
        public MeasureSizeForLinux()
        {
            EngLeftRightPadding = float.NaN;
            EngTopBottomPadding = float.NaN;
            EngWidth = float.NaN;
            EngHeight = float.NaN;
            ChnLeftRightPadding = float.NaN;
            ChnTopBottomPadding = float.NaN;
            ChnWidth = float.NaN;
            ChnHeight = float.NaN;
        }

        public float EngLeftRightPadding { get; set; }
        public float EngTopBottomPadding { get; set; }
        public float EngWidth { get; set; }
        public float EngHeight { get; set; }
        public float ChnLeftRightPadding { get; set; }
        public float ChnTopBottomPadding { get; set; }
        public float ChnWidth { get; set; }
        public float ChnHeight { get; set; }
    }
}
