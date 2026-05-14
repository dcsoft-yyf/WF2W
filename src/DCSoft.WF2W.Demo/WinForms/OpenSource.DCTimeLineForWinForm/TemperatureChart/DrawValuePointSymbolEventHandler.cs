using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DCSoft.Drawing;
using System.Drawing.Drawing2D;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间轴绘制自定义数据点图标事件的委托类型
    /// </summary>
    /// <param name="eventSender"></param>
    /// <param name="args"></param>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(true)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    [System.Runtime.InteropServices.Guid("3A78251F-92EF-4F5E-8F99-F3A0FAAABBEC")]
#endif
    public delegate void DrawValuePointSymbolEventHandler(object eventSender, DrawValuePointSymbolEventArgs args);

    /// <summary>
    /// 时间轴绘制自定义数据点图标事件的事件参数
    /// </summary>
#if !DCWriterForWASM
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
    [System.Runtime.InteropServices.ComVisible( false )]
#endif
    public partial class DrawValuePointSymbolEventArgs
    {
        public DrawValuePointSymbolEventArgs()
        {
            
        }

        private ValuePoint _valuePoint = null;
        /// <summary>
        /// 数据点对象
        /// </summary>
        public ValuePoint valuePoint
        {
            get { return _valuePoint; }
            set { _valuePoint = value; }
        }

        private TemperatureDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public TemperatureDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        private RectangleF _ReferRect = new RectangleF();
        /// <summary>
        /// 给定的数据点图标的中心点坐标
        /// </summary>
        public RectangleF ReferRect
        {
            get { return this._ReferRect; }
            set { _ReferRect = value; }
        }

        private DCGraphics _graphic = null;
        /// <summary>
        /// 绘制所需要的画刷对象
        /// </summary>
        internal DCGraphics Graphic
        {
            get { return this._graphic; }
            set { _graphic = value; }
        }

        #region 20200316:抄代码转发底层调用
        public void DrawEllipse(Color c, float lineWidth, DashStyle lineStyle, RectangleF rect)
        {
            if (this.Graphic != null)
            {
                this.Graphic.DrawEllipse(c, lineWidth, lineStyle, rect);
            }
        }

        public void DrawImage(Image img, float x, float y, float width, float height)
        {
            if (this.Graphic != null)
            {
                this.Graphic.DrawImage(img, x, y, width, height);
            }
        }

        public void DrawLine(Color c, float lineWidth, DashStyle lineStyle, PointF pt1, PointF pt2)
        {
            if (this.Graphic != null)
            {
                this.Graphic.DrawLine(c, lineWidth, lineStyle, pt1, pt2);
            }
        }

        public void DrawLines(Color lineColor, float lineWidth, DashStyle lineStyle, PointF[] points)
        {
            if (this.Graphic != null)
            {
                this.Graphic.DrawLines(lineColor, lineWidth, lineStyle, points);
            }
        }

        public void DrawPath(Color lineColor, float lineWidth, DashStyle lineStyle, GraphicsPath path)
        {
            if (this.Graphic != null)
            {
                this.Graphic.DrawPath(lineColor, lineWidth, lineStyle, path);
            }
        }

        public void DrawPie(Pen p, Rectangle rect, float startAngle, float sweepAngle)
        {
            if (this.Graphic != null)
            {
                this.Graphic.DrawPie(p, rect, startAngle, sweepAngle);
            }
        }

        public void DrawPolygon(Color c, float lineWidth, DashStyle lineStyle, PointF[] ps)
        {
            if (this.Graphic != null)
            {
                this.Graphic.DrawPolygon(c, lineWidth, lineStyle, ps);
            }
        }

        public void DrawRectangle(Pen pen, RectangleF rect)
        {
            if (this.Graphic != null)
            {
                this.Graphic.DrawRectangle(pen, rect);
            }
        }

        public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
        {
            if (this.Graphic != null && this.Graphic.NativeGraphics != null)
            {
                this.Graphic.NativeGraphics.DrawString(s, font, brush, x, y, format);
            }
        }

        public void DrawString(string s, Font font, Color txtColor, float x, float y, StringFormat format)
        {
            if (this.Graphic != null)
            {
                this.Graphic.DrawString(s, new XFontValue(font, true), txtColor, x, y, new DCStringFormat(format));
            }
        }

        public void FillEllipse(Brush brush, RectangleF rect)
        {
            if (this.Graphic != null)
            {
                this.Graphic.FillEllipse( brush , rect);
            }
        }

        public void FillPath(Brush brush, GraphicsPath path)
        {
            if (this.Graphic != null)
            {
                this.Graphic.FillPath(brush, path);
            }
        }

        public void FillPie(Brush b, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            if (this.Graphic != null)
            {
                this.Graphic.FillPie(b, x, y, width, height, startAngle, sweepAngle);
            }
        }

        public void FillPolygon(Brush brush, PointF[] points)
        {
            if (this.Graphic != null)
            {
                this.Graphic.FillPolygon(brush, points);
            }
        }

        public void FillRectangle(Brush brush, RectangleF rect)
        {
            if (this.Graphic != null)
            {
                this.Graphic.FillRectangle(brush, rect);
            }
        }
        #endregion
    }
}
