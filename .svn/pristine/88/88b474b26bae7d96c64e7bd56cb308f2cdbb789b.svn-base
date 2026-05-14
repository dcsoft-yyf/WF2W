//------------------------------------------------------------------------------
// <copyright file="GraphicsPath.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Drawing2D
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System;
    using Microsoft.Win32;
    using System.Drawing;
    using System.ComponentModel;
    using System.Drawing.Internal;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.Versioning;
    using System.Text;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public sealed class GraphicsPath : MarshalByRefObject, ICloneable, IDisposable
    {


        public static bool SVGMode = false;
        public static bool OFDMode = false;
        public GraphicsPath(PointF[] ps, byte[] types) : this(ps , types , FillMode.Alternate)
        {

        }
        private static PointF[] ToPointFArray(Point[] pts )
        {
            var pts2 = new PointF[pts.Length];
            for( var iCount = 0; iCount < pts.Length;iCount ++ )
            {
                pts2[iCount] = pts[iCount];
            }
            return pts2;
        }
        public GraphicsPath(Point[] ps, byte[] types) : this(ToPointFArray(ps), types, FillMode.Alternate)
        {

        }
        public GraphicsPath(Point[] ps, byte[] types, System.Drawing.Drawing2D.FillMode fillMode):this(ToPointFArray(ps ) , types , fillMode)
        {

        }
        public GraphicsPath(PointF[] ps, byte[] types, System.Drawing.Drawing2D.FillMode fillMode)
        {
            if (ps == null || ps.Length == 0)
            {
                throw new ArgumentNullException("ps");
            }
            if (types == null || types.Length == 0)
            {
                throw new ArgumentNullException("types");
            }
            if (ps.Length != types.Length)
            {
                throw new ArgumentException("2个数值的长度不一致");
            }
            this._String = new StringBuilder();
            if (SVGMode)
            {
                for (var iCount = 1; iCount < ps.Length; iCount++)
                {
                    var x = ps[iCount].X;
                    var y = ps[iCount].Y;
                    this.SetBounds(x, y);
                    var type = (PathPointType)types[iCount];
                    if ((type & PathPointType.Line) == PathPointType.Line)
                    {
                        if (this._String.Length == 0)
                        {
                            this._String.Append("M " + ps[iCount - 1].X + "," + ps[iCount - 1].Y);
                        }
                        this._String.Append(" L " + x + "," + y);
                        if ((type & PathPointType.CloseSubpath) == PathPointType.CloseSubpath)
                        {
                            this._String.Append(" Z");
                        }
                    }
                }
            }
            else if (OFDMode)
            {
                for (var iCount = 1; iCount < ps.Length; iCount++)
                {
                    var x = ps[iCount].X;
                    var y = ps[iCount].Y;
                    this.SetBounds(x, y);
                    var type = (PathPointType)types[iCount];
                    if ((type & PathPointType.Line) == PathPointType.Line)
                    {
                        if (this._String.Length == 0)
                        {
                            this._String.Append("M " + ps[iCount - 1].X + " " + ps[iCount - 1].Y);
                        }
                        this._String.Append(" L " + x + " " + y);
                        if ((type & PathPointType.CloseSubpath) == PathPointType.CloseSubpath)
                        {
                            this._String.Append(" C");
                        }
                    }
                }
            }
            else
            {
                this._String.Append("[null");
                for (var iCount = 0; iCount < ps.Length; iCount++)
                {
                    var x = ps[iCount].X;
                    var y = ps[iCount].Y;
                    var type = (PathPointType)types[iCount];
                    if ((type & PathPointType.Line) == PathPointType.Line)
                    {
                        if (iCount > 0)
                        {
                            this.AddLine(ps[iCount - 1].X, ps[iCount - 1].Y, x, y);
                        }
                        if ((type & PathPointType.CloseSubpath) == PathPointType.CloseSubpath)
                        {
                            if (SVGMode)
                            {
                                this._String.Append(" Z");
                            }
                            else
                            {
                                this.AddLine(x, y, ps[0].X, ps[0].Y);
                            }
                        }
                    }
                }
            }
        }

        public GraphicsPath() : this(System.Drawing.Drawing2D.FillMode.Alternate)
        {
        }

        public GraphicsPath(System.Drawing.Drawing2D.FillMode fillMode)
        {
            this._String = new StringBuilder();
            if (SVGMode == false)
            {
                this._String.Append("[null");
            }
        }

        public object Clone()
        {
            var path2 = (GraphicsPath)this.MemberwiseClone();
            if( this._String != null )
            {
                path2._String = new StringBuilder(this._String.ToString());
            }
            
            return path2;
        }

private bool _BoundsEmpty = true;
        private float _BoundsLeft = float.MinValue;
        private float _BoundsTop = float.MinValue;
        private float _BoundsRight = float.MinValue;
        private float _BoundsBottom = float.MinValue;
        private void SetBounds(float x, float y)
        {
            if (this._BoundsEmpty)
            {
                this._BoundsEmpty = false;
                this._BoundsLeft = x;
                this._BoundsTop = y;
                this._BoundsRight = x;
                this._BoundsBottom = y;
            }
            else
            {
                if (this._BoundsLeft > x) this._BoundsLeft = x;
                if (this._BoundsRight < x) this._BoundsRight = x;
                if (this._BoundsTop > y) this._BoundsTop = y;
                if (this._BoundsBottom < y) this._BoundsBottom = y;
            }
        }


        private System.Text.StringBuilder _String = null;

        public void Transform(Drawing2D.Matrix m)
        {

        }
        public bool IsVisible(float x, float y)
        {
            return false;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void CloseAllFigures()
        {
            if (SVGMode)
            {
                this._String.Append(" Z");
            }
        }
        public RectangleF GetBounds()
        {
            if (this._BoundsEmpty)
            {
                return RectangleF.Empty;
            }
            else
            {
                return new RectangleF(
                    this._BoundsLeft,
                    this._BoundsTop,
                    this._BoundsRight - this._BoundsLeft,
                    this._BoundsBottom - this._BoundsTop);
            }
        }

        private enum PathRecordType
        {
            AddArc = 0,
            AddBezier = 1,
            AddBeziers = 2,
            AddEllipse = 3,
            AddLine = 4,
            AddLines = 5,
            AddPie = 6,
            AddRectangle = 7,
            AddRectangles = 8,
            AddStringPoint = 9,
            AddStringRectangle = 10,
            AddPolygon = 11,
            AddClosedCurve = 12,
            AddCurve = 13
        }
        private void InnerAddRectangle(Rectangle rect)
        {
            this.SetBounds(rect.Left, rect.Top);
            this.SetBounds(rect.Right, rect.Bottom);
            if (OFDMode)
            {
                this._String.Append(rect.Left);
                this._String.Append(' ');
                this._String.Append(rect.Top);
                this._String.Append(' ');
                this._String.Append(rect.Width);
                this._String.Append(' ');
                this._String.Append(rect.Height);
            }
            else
            {
                this._String.Append(rect.Left);
                this._String.Append(',');
                this._String.Append(rect.Top);
                this._String.Append(',');
                this._String.Append(rect.Width);
                this._String.Append(',');
                this._String.Append(rect.Height);
            }
        }
        private void InnerAddRectangle(RectangleF rect)
        {
            this.SetBounds(rect.Left, rect.Top);
            this.SetBounds(rect.Right, rect.Bottom);
            if (OFDMode)
            {
                this._String.Append(rect.Left);
                this._String.Append(' ');
                this._String.Append(rect.Top);
                this._String.Append(' ');
                this._String.Append(rect.Width);
                this._String.Append(' ');
                this._String.Append(rect.Height);
            }
            else
            {
                this._String.Append(rect.Left);
                this._String.Append(',');
                this._String.Append(rect.Top);
                this._String.Append(',');
                this._String.Append(rect.Width);
                this._String.Append(',');
                this._String.Append(rect.Height);
            }
        }
        private void InnerAddRectangle(float x, float y, float width, float height)
        {
            this.SetBounds(x, y);
            this.SetBounds(x + width, y + height);
            if (OFDMode)
            {
                this._String.Append(x);
                this._String.Append(' ');
                this._String.Append(y);
                this._String.Append(' ');
                this._String.Append(width);
                this._String.Append(' ');
                this._String.Append(height);
            }
            else
            {
                this._String.Append(x);
                this._String.Append(',');
                this._String.Append(y);
                this._String.Append(',');
                this._String.Append(width);
                this._String.Append(',');
                this._String.Append(height);
            }
        }
        private void InnerAddPoints(PointF[] ps)
        {
            this._String.Append('[');
            for (int iCount = 0; iCount < ps.Length; iCount++)
            {
                if (iCount > 0)
                {
                    this._String.Append(',');
                }
                var p = ps[iCount];
                this.SetBounds(p.X, p.Y);
                this._String.Append(p.X.ToString());
                this._String.Append(',');
                this._String.Append(p.Y.ToString());
            }
            this._String.Append(']');
        }
        private void InnerAddRecordType(PathRecordType type)
        {
            //CheckDispose();
            //if (this._String.Length > 0 && this._String[this._String.Length - 1] != ',')
            //{
            //    this._String.Append(',');
            //}
            this._String.Append(',');
            this._String.Append(((int)type).ToString());
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void CloseFigure()
        {

        }
        public void AddArc(Rectangle rect, float startAngle, float sweepAngle)
        {
            AddArc((float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle);
        }
        public void AddArc(RectangleF rect, float startAngle, float sweepAngle)
        {
            AddArc(rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle);
        }

        public void AddArc(int x, int y, int width, int height, float startAngle, float sweepAngle)
        {
            AddArc((float)x, (float)y, (float)width, (float)height, startAngle, sweepAngle);
        }
        /// <summary>
        ///  将实际角度转换成绘制角度，由于斜视的原因，这两个角度是有区别的
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private static float TransformAngle(float angle, float boundsWidth, float boundsHeight)
        {
            double x = boundsHeight * Math.Cos(angle * Math.PI / 180);
            double y = boundsWidth * Math.Sin(angle * Math.PI / 180);
            float result = (float)(Math.Atan2(y, x) * 180 / Math.PI);
            if (result < 0)
                return result + 360;
            return result;
        }

        public static object[] BuildArcPath(
            float currentX,
            float currentY,
            float left,
            float top,
            float width,
            float height,
            float startAngle,
            float sweepAngle)
        {
            if (sweepAngle % 360 == 0)
            {
                return null;
            }
            //if( sweepAngle < 0 )
            //{
            //    var temp = sweepAngle;
            //    startAngle = startAngle + sweepAngle;
            //    sweepAngle = -sweepAngle;
            //}
            var a1 = TransformAngle(startAngle, width, height);
            var a2 = TransformAngle(startAngle + sweepAngle, width, height);
            startAngle = a1;
            sweepAngle = a2 - a1;
            var longAxis = width / 2;
            var shortAxis = height / 2;
            var cx = left + longAxis;
            var cy = top + shortAxis;
            //var newStartAngle = startAngle;// 360 - startAngle;
            var startX = (float)(cx + (longAxis * Math.Cos(startAngle * Math.PI / 180)));
            var startY = (float)(cy + (shortAxis * Math.Sin(startAngle * Math.PI / 180)));
            var newEndAngle = (startAngle + sweepAngle);
            var endX = (float)(cx + (longAxis * Math.Cos(newEndAngle * Math.PI / 180)));
            var endY = (float)(cy + (shortAxis * Math.Sin(newEndAngle * Math.PI / 180)));
            //if (endX + " " + endY == "1456.9768 908.58734")
            //{
            //    var s = 1;
            //}
            var strCode = new StringBuilder();
            strCode.Append(" A " + longAxis + " " + shortAxis + " 0 ");
            if (Math.Abs(sweepAngle) > 180)
            {
                strCode.Append(" 1 ");
            }
            else
            {
                strCode.Append(" 0 ");
            }
            if (sweepAngle < 0)
            {
                strCode.Append(" 0 ");
            }
            else
            {
                strCode.Append(" 1 ");
            }
            //if(  Math.Abs( currentX - endX ) < 2 && Math.Abs( currentY - endY) < 2 )
            //{
            //    strCode.Append(" 0 ");
            //}
            //else
            //{
            //    strCode.Append(" 1 ");
            //}
            strCode.Append(endX + " " + endY);
            return new object[] { startX, startY, strCode.ToString(), endX, endY };
        }

        public void AddArc(float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            this.SetBounds(x, y);
            this.SetBounds(x + width, y + height);

            if (SVGMode)
            {
                if (sweepAngle % 360 == 0)
                {
                    this.AddEllipse(x, y, width, height);
                    return;
                }
                var data = BuildArcPath(
                    this._CurrentX,
                    this._CurrentY,
                    x,
                    y,
                    width,
                    height,
                    startAngle, sweepAngle);
                if (data != null)
                {
                    this.AddMoveTo((float)data[0], (float)data[1]);
                    this._String.Append(data[2]);
                    this._CurrentX = (float)data[3];
                    this._CurrentY = (float)data[4];
                }
            }
            else
            {
                InnerAddRecordType(PathRecordType.AddArc);
                this._String.Append(',');
                InnerAddRectangle(x, y, width, height);
                this._String.Append(',');
                this._String.Append(CanvasGraphics.ConvertToCanvasAngle(startAngle, width, height));
                this._String.Append(',');
                this._String.Append(CanvasGraphics.ConvertToCanvasAngle(startAngle + sweepAngle, width, height));
            }
        }


        public void AddBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            this.SetBounds(x1, y1);
            this.SetBounds(x2, y2);
            this.SetBounds(x3, y3);
            this.SetBounds(x4, y4);
            if (SVGMode)
            {
                this.AddMoveTo(x1, y1);
                this._String.Append(" C " + x2 + "," + y2 + "," + x3 + "," + y3 + "," + x4 + "," + y4);
                this._CurrentX = x4;
                this._CurrentY = y4;
            }
            else
            {
                InnerAddRecordType(PathRecordType.AddBezier);
                this._String.Append(',');
                this._String.Append(x1.ToString());
                this._String.Append(',');
                this._String.Append(y1.ToString());
                this._String.Append(',');
                this._String.Append(x2.ToString());
                this._String.Append(',');
                this._String.Append(y2.ToString());
                this._String.Append(',');
                this._String.Append(x3.ToString());
                this._String.Append(',');
                this._String.Append(y3.ToString());
                this._String.Append(',');
                this._String.Append(x4.ToString());
                this._String.Append(',');
                this._String.Append(y4.ToString());
            }
        }



        public void AddEllipse(RectangleF rect)
        {
            AddEllipse(rect.Left, rect.Top, rect.Width, rect.Height);
        }
        public void AddEllipse(float x, float y, float width, float height)
        {
            if (SVGMode)
            {

            }
            else
            {
                InnerAddRecordType(PathRecordType.AddEllipse);
                this._String.Append(',');
                InnerAddRectangle(x, y, width, height);
            }
        }
        private float _CurrentX = float.MinValue;
        private float _CurrentY = float.MinValue;
        private void AddMoveTo(float x, float y)
        {
            if (Math.Abs(x - this._CurrentX) > 2
                || Math.Abs(y - this._CurrentY) > 2)
            {
                if (OFDMode)
                {
                    this._String.Append(" M " + x + " " + y);
                }
                else
                {
                    this._String.Append(" M " + x + "," + y);
                }
                this._CurrentX = x;
                this._CurrentY = y;
            }
        }
        public void AddLine(float x1, float y1, float x2, float y2)
        {
            this.SetBounds(x1, y1);
            this.SetBounds(x2, y2);
            if (SVGMode)
            {
                this.AddMoveTo(x1, y1);
                this._String.Append(" L " + x2 + "," + y2);
                this._CurrentX = x2;
                this._CurrentY = y2;
            }
            else if (OFDMode)
            {
                this.AddMoveTo(x1, y1);
                this._String.Append(" L " + x2 + " " + y2);
                this._CurrentX = x2;
                this._CurrentY = y2;
            }
            else
            {
                InnerAddRecordType(PathRecordType.AddLine);
                this._String.Append(",");
                this._String.Append(x1.ToString());
                this._String.Append(",");
                this._String.Append(y1.ToString());
                this._String.Append(",");
                this._String.Append(x2.ToString());
                this._String.Append(",");
                this._String.Append(y2.ToString());
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        public void AddLines(PointF[] points)
        {
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            if (points.Length == 0)
            {
                throw new ArgumentException(null, "points");
            }
            if (SVGMode || OFDMode)
            {
                this.AddMoveTo(points[0].X, points[0].Y);
                for (var iCount = 1; iCount < points.Length; iCount++)
                {
                    var px = points[iCount].X;
                    var py = points[iCount].Y;
                    this.SetBounds(px, py);
                    if (SVGMode)
                    {
                        this._String.Append(" L " + px + "," + py);
                    }
                    else
                    {
                        this._String.Append(" L " + px + " " + py);
                    }
                }
                this._CurrentX = points[points.Length - 1].X;
                this._CurrentY = points[points.Length - 1].Y;
            }
            else
            {
                InnerAddRecordType(PathRecordType.AddLines);
                this._String.Append(',');
                InnerAddPoints(points);
            }
        }



        public void AddPie(int x, int y, int width, int height, float startAngle, float sweepAngle)
        {
            AddPie((float)x, (float)y, (float)width, (float)height, startAngle, sweepAngle);
        }
        public void AddPie(float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            this.SetBounds(x, y);
            this.SetBounds(x + width, y + height);
            var myStr = this._String;
            if (SVGMode || OFDMode)
            {
                if (sweepAngle % 360 == 0)
                {
                    this.AddEllipse(x, y, width, height);
                    return;
                }
                var longAxis = width / 2;
                var shortAxis = height / 2;
                var cx = x + longAxis;
                var cy = y + shortAxis;
                var newStartAngle = 360 - startAngle;
                var startX = (float)(cx + (longAxis * Math.Cos(newStartAngle * Math.PI / 180)));
                var startY = (float)(cy + (shortAxis * Math.Sin(newStartAngle * Math.PI / 180)));
                var newEndAngle = -(startAngle + sweepAngle);
                var endX = (float)(cx + (longAxis * Math.Cos(newEndAngle * Math.PI / 180)));
                var endY = (float)(cy + (shortAxis * Math.Sin(newEndAngle * Math.PI / 180)));
                this.AddMoveTo(cx, cy);
                myStr.Append(" L " + startX + " " + startY);
                this.AddMoveTo(startX, startY);
                myStr.Append(" A " + longAxis + " " + shortAxis + " 0 ");
                if (SVGMode)
                {
                    if (Math.Abs(sweepAngle) > 180)
                    {
                        myStr.Append(" 1 ");
                    }
                    else
                    {
                        myStr.Append(" 0 ");
                    }
                    myStr.Append(" 0 " + endX + " " + endY);
                    myStr.Append(" L " + cx + " " + cy);
                    myStr.Append(" Z ");
                }
                else
                {
                    // 判断是否是大弧（角度差超过180度）
                    int largeArcFlag = sweepAngle % 360 <= 180 ? 0 : 1;
                    myStr.Append(" 0 ");
                    myStr.Append(largeArcFlag);
                    myStr.Append(" 1 ");
                    myStr.Append(endX + " " + endY);
                    myStr.Append(" L " + cx + " " + cy);
                    myStr.Append(" C ");
                }
                this._CurrentX = cx;
                this._CurrentY = cy;
            }
            //else if( OFDMode)
            //{
            //    if (sweepAngle % 360 == 0)
            //    {
            //        this.AddEllipse(x, y, width, height);
            //        return;
            //    }
            //    var centerX = x + width / 2.0f;
            //    var centerY = y + height / 2.0f;
            //    var radiusX = width / 2.0f;
            //    var radiusY = height / 2.0f;

            //    // 计算起点和终点坐标（角度转弧度）
            //    var startRad = startAngle * Math.PI / 180.0f;
            //    var endAngle = startAngle + sweepAngle;
            //    var endRad = endAngle * Math.PI / 180.0f;

            //    var startX = centerX + radiusX * Math.Cos(startRad);
            //    var startY = centerY + radiusY * Math.Sin(startRad);
            //    var endX = centerX + radiusX * Math.Cos(endRad);
            //    var endY = centerY + radiusY * Math.Sin(endRad);

            //    // 判断是否是大弧（角度差超过180度）
            //    int largeArcFlag = (endAngle - startAngle) % 360 <= 180 ? 0 : 1;
            //    str.Append("S 0 0 ");
            //    AddCode(str, 'M');
            //    AddValue(str, centerX);
            //    AddValue(str, centerY);
            //    AddCode(str, 'L');
            //    AddValue(str, (float)startX);
            //    AddValue(str, (float)startY);
            //    AddCode(str, 'A');
            //    AddValue(str, radiusX);
            //    AddValue(str, radiusY);
            //    str.Append(" 0 ");
            //    AddValue(str, largeArcFlag);
            //    str.Append(" 1 ");
            //    AddValue(str, (float)endX);
            //    AddValue(str, (float)endY);
            //    AddCode(str, 'Z');
            //    var txt = str.ToString();
            //    this._CurrentX = centerX;
            //    this._CurrentY = centerY;
            //    return txt;
            //}
            else
            {
                InnerAddRecordType(PathRecordType.AddPie);
                myStr.Append(',');
                InnerAddRectangle(x, y, width, height);
                myStr.Append(',');
                myStr.Append(CanvasGraphics.ConvertToCanvasAngle(startAngle, width, height));
                myStr.Append(',');
                myStr.Append(CanvasGraphics.ConvertToCanvasAngle(startAngle + sweepAngle, width, height));
            }
        }

        public void AddPolygon(PointF[] points)
        {
            if (points == null || points.Length == 0)
            {
                throw new ArgumentNullException("points");
            }
            if (points.Length == 1)
            {
                return;
            }
            var myStr = this._String;
            if (SVGMode)
            {
                myStr.Append(" M " + points[0].X + "," + points[0].Y);
                for (var iCount = 1; iCount < points.Length; iCount++)
                {
                    var px = points[iCount].X;
                    var py = points[iCount].Y;
                    this.SetBounds(px, py);
                    myStr.Append(" L " + px + "," + py);
                }
                myStr.Append(" L " + points[0].X + "," + points[0].Y);
            }
            else if (OFDMode)
            {
                myStr.Append(" M " + points[0].X + " " + points[0].Y);
                for (var iCount = 1; iCount < points.Length; iCount++)
                {
                    var px = points[iCount].X;
                    var py = points[iCount].Y;
                    this.SetBounds(px, py);
                    myStr.Append(" L " + px + " " + py);
                }
                myStr.Append(" L " + points[0].X + " " + points[0].Y);
            }
            else
            {
                InnerAddRecordType(PathRecordType.AddPolygon);
                myStr.Append(',');
                InnerAddPoints(points);
            }
        }

#if !LightWeight
        public void AddRectangle(Rectangle rect)
        {
            var myStr = this._String;
            if (SVGMode)
            {
                myStr.Append(
                    "M " + rect.Left + "," + rect.Top +
                    " h " + rect.Width +
                    " v " + rect.Height +
                    " h -" + rect.Width +
                    " v -" + rect.Height +
                    " L " + rect.Left + "," + rect.Top);
                this.SetBounds(rect.Left, rect.Top);
                this.SetBounds(rect.Right, rect.Bottom);
            }
            else if (OFDMode)
            {
                myStr.Append(" M ").Append(rect.Left).Append(' ').Append(rect.Top)
                    .Append(" L ").Append(rect.Right).Append(' ').Append(rect.Top)
                    .Append(" L ").Append(rect.Right).Append(' ').Append(rect.Bottom)
                    .Append(" L ").Append(rect.Left).Append(' ').Append(rect.Bottom)
                    .Append(" L ").Append(rect.Left).Append(' ').Append(rect.Top);
                this.SetBounds(rect.Left, rect.Top);
                this.SetBounds(rect.Right, rect.Bottom);
            }
            else
            {
                InnerAddRecordType(PathRecordType.AddRectangle);
                this._String.Append(',');
                InnerAddRectangle(rect);
            }
        }
#endif
        public void AddRectangle(RectangleF rect)
        {
            if (SVGMode)
            {
                this._String.Append(
                    "M " + rect.Left + "," + rect.Top +
                    " h " + rect.Width +
                    " v " + rect.Height +
                    " h -" + rect.Width +
                    " v -" + rect.Height +
                    " L " + rect.Left + "," + rect.Top);
                this.SetBounds(rect.Left, rect.Top);
                this.SetBounds(rect.Right, rect.Bottom);
            }
            else if (OFDMode)
            {
                this._String.Append(" M ").Append(rect.Left).Append(' ').Append(rect.Top)
                   .Append(" L ").Append(rect.Right).Append(' ').Append(rect.Top)
                   .Append(" L ").Append(rect.Right).Append(' ').Append(rect.Bottom)
                   .Append(" L ").Append(rect.Left).Append(' ').Append(rect.Bottom)
                   .Append(" L ").Append(rect.Left).Append(' ').Append(rect.Top);
                this.SetBounds(rect.Left, rect.Top);
                this.SetBounds(rect.Right, rect.Bottom);
            }
            else
            {
                InnerAddRecordType(PathRecordType.AddRectangle);
                this._String.Append(',');
                InnerAddRectangle(rect);
            }
        }

        public override string ToString()
        {
            var txt = string.Concat(this._String.ToString(), "]");
            return txt;
        }

        public string ToCanvasString()
        {
            var txt = string.Concat(this._String.ToString(), "]");
            return txt;
        }
        public string ToOFDString()
        {
            if (this._String.Length > 0)
            {
                return "S 0 0 " + this._String.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public string ToSVGString()
        {
            //Console.WriteLine(this._String.ToString());
            return this._String.ToString();
        }
        public void Dispose()
        {
            //if(this._Records != null )
            //{
            //    foreach( var item in this._Records )
            //    {
            //        item.Dispose();
            //    }
            //    this._Records.Clear();
            //    this._Records = null;
            //}
            //_Disposed = true;
        }

        //********************************************************************
        //********************************************************************
        //********************************************************************
        //********************************************************************
        //********************************************************************
        //********************************************************************
        //********************************************************************
        //********************************************************************
        //********************************************************************
        //********************************************************************
        //********************************************************************

        ///*
        // * handle to native path object
        // */
        //internal IntPtr nativePath;

        ///**
        // * Create a new path object with the default fill mode
        // */
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GraphicsPath"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Initializes a new instance of the <see cref='System.Drawing.Drawing2D.GraphicsPath'/> class with a <see cref='System.Drawing.Drawing2D.FillMode'/> of <see cref='System.Drawing.Drawing2D.FillMode.Alternate'/>
        /////       .
        /////    </para>
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //public GraphicsPath() : this(System.Drawing.Drawing2D.FillMode.Alternate) { }

        ///**
        // * Create a new path object with the specified fill mode
        // */
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GraphicsPath1"]/*' />
        ///// <devdoc>
        /////    Initializes a new instance of the <see cref='System.Drawing.Drawing2D.GraphicsPath'/> class with the specified <see cref='System.Drawing.Drawing2D.FillMode'/>.
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //public GraphicsPath(FillMode fillMode)
        //{
        //    IntPtr nativePath = IntPtr.Zero;

        //    int status = SafeNativeMethods.Gdip.GdipCreatePath(unchecked((int)fillMode), out nativePath);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);

        //    this.nativePath = nativePath;
        //}

        //// float version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GraphicsPath2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////    Initializes a new instance of the
        /////    <see cref='System.Drawing.Drawing2D.GraphicsPath'/> array with the
        /////    specified <see cref='System.Drawing.Drawing2D.GraphicsPath.PathTypes'/>
        /////    and <see cref='System.Drawing.Drawing2D.GraphicsPath.PathPoints'/> arrays.
        /////    </para>
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //public GraphicsPath(PointF[] pts, byte[] types) :
        //  this(pts, types, System.Drawing.Drawing2D.FillMode.Alternate)
        //{ }

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GraphicsPath3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Initializes a new instance of the <see cref='System.Drawing.Drawing2D.GraphicsPath'/> array with the
        /////       specified <see cref='System.Drawing.Drawing2D.GraphicsPath.PathTypes'/> and <see cref='System.Drawing.Drawing2D.GraphicsPath.PathPoints'/> arrays and with the
        /////       specified <see cref='System.Drawing.Drawing2D.FillMode'/>.
        /////    </para>
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //public GraphicsPath(PointF[] pts, byte[] types, FillMode fillMode)
        //{
        //    if (pts == null)
        //        throw new ArgumentNullException("pts");
        //    IntPtr nativePath = IntPtr.Zero;

        //    if (pts.Length != types.Length)
        //        throw SafeNativeMethods.Gdip.StatusException(SafeNativeMethods.Gdip.InvalidParameter);

        //    int count = types.Length;
        //    IntPtr ptbuf = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
        //    IntPtr typebuf = Marshal.AllocHGlobal(count);
        //    try
        //    {
        //        Marshal.Copy(types, 0, typebuf, count);

        //        int status = SafeNativeMethods.Gdip.GdipCreatePath2(new HandleRef(null, ptbuf), new HandleRef(null, typebuf), count,
        //                                             unchecked((int)fillMode), out nativePath);


        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(ptbuf);
        //        Marshal.FreeHGlobal(typebuf);
        //    }

        //    this.nativePath = nativePath;
        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GraphicsPath4"]/*' />
        ///// <devdoc>
        /////    <para>
        /////    Initializes a new instance of the
        /////    <see cref='System.Drawing.Drawing2D.GraphicsPath'/> array with the
        /////    specified <see cref='System.Drawing.Drawing2D.GraphicsPath.PathTypes'/>
        /////    and <see cref='System.Drawing.Drawing2D.GraphicsPath.PathPoints'/> arrays.
        /////    </para>
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //public GraphicsPath(Point[] pts, byte[] types) :
        //  this(pts, types, System.Drawing.Drawing2D.FillMode.Alternate)
        //{ }

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GraphicsPath5"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Initializes a new instance of the <see cref='System.Drawing.Drawing2D.GraphicsPath'/> array with the
        /////       specified <see cref='System.Drawing.Drawing2D.GraphicsPath.PathTypes'/> and <see cref='System.Drawing.Drawing2D.GraphicsPath.PathPoints'/> arrays and with the
        /////       specified <see cref='System.Drawing.Drawing2D.FillMode'/>.
        /////    </para>
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //public GraphicsPath(Point[] pts, byte[] types, FillMode fillMode)
        //{
        //    if (pts == null)
        //        throw new ArgumentNullException("pts");
        //    IntPtr nativePath = IntPtr.Zero;

        //    if (pts.Length != types.Length)
        //        throw SafeNativeMethods.Gdip.StatusException(SafeNativeMethods.Gdip.InvalidParameter);

        //    int count = types.Length;
        //    IntPtr ptbuf = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
        //    IntPtr typebuf = Marshal.AllocHGlobal(count);
        //    try
        //    {
        //        Marshal.Copy(types, 0, typebuf, count);

        //        int status = SafeNativeMethods.Gdip.GdipCreatePath2I(new HandleRef(null, ptbuf), new HandleRef(null, typebuf), count,
        //                                              unchecked((int)fillMode), out nativePath);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(ptbuf);
        //        Marshal.FreeHGlobal(typebuf);
        //    }

        //    this.nativePath = nativePath;
        //}

        ///**
        // * Make a copy of the current path object
        // */
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Clone"]/*' />
        ///// <devdoc>
        /////    Creates an exact copy of this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //public object Clone()
        //{
        //    IntPtr clonePath = IntPtr.Zero;

        //    int status = SafeNativeMethods.Gdip.GdipClonePath(new HandleRef(this, nativePath), out clonePath);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);

        //    return new GraphicsPath(clonePath, 0);

        //}

        ///**
        // * 'extra' parameter is necessary to avoid conflict with
        // * other constructor GraphicsPath(int fillmode)
        // */

        //private GraphicsPath(IntPtr nativePath, int extra)
        //{
        //    if (nativePath == IntPtr.Zero)
        //        throw new ArgumentNullException("nativePath");

        //    this.nativePath = nativePath;
        //}

        ///**
        // * Dispose of resources associated with the
        // */
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Dispose"]/*' />
        ///// <devdoc>
        /////    Eliminates resources for this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //void Dispose(bool disposing)
        //{
        //    //            if (nativePath != IntPtr.Zero) {
        //    //                 try{
        //    //#if DEBUG
        //    //                    int status = 
        //    //#endif            
        //    //                    SafeNativeMethods.Gdip.GdipDeletePath(new HandleRef(this, nativePath));
        //    //#if DEBUG
        //    //                    Debug.Assert(status == SafeNativeMethods.Gdip.Ok, "GDI+ returned an error status: " + status.ToString(CultureInfo.InvariantCulture));
        //    //#endif        
        //    //                }
        //    //                catch( Exception ex ){
        //    //                    if( ClientUtils.IsSecurityOrCriticalException( ex ) ) {
        //    //                        throw;
        //    //                    }

        //    //                    Debug.Fail( "Exception thrown during Dispose: " + ex.ToString() );
        //    //                }
        //    //                finally{
        //    //                     nativePath = IntPtr.Zero;
        //    //                }
        //    //            }
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Finalize"]/*' />
        ///// <devdoc>
        /////    Eliminates resources for this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //~GraphicsPath()
        //{
        //    Dispose(false);
        //}

        /**
         * Reset the path object to empty
         */
        /// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Reset"]/*' />
        /// <devdoc>
        ///    Empties the <see cref='System.Drawing.Drawing2D.GraphicsPath.PathPoints'/>
        ///    and <see cref='System.Drawing.Drawing2D.GraphicsPath.PathTypes'/> arrays
        ///    and sets the <see cref='System.Drawing.Drawing2D.GraphicsPath.FillMode'/> to
        ///    <see cref='System.Drawing.Drawing2D.FillMode.Alternate'/>.
        /// </devdoc>
        public void Reset()
        {
            this._String.Clear();
        }

        private FillMode _FillMode = FillMode.Alternate;
        /**
         * Get path fill mode information
         */
        /// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.FillMode"]/*' />
        /// <devdoc>
        ///    Gets or sets a <see cref='System.Drawing.Drawing2D.FillMode'/> that determines how the interiors of
        ///    shapes in this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> are filled.
        /// </devdoc>
        public FillMode FillMode
        {
            get
            {
                return this._FillMode;
            }
            set
            {
                this._FillMode = value;
            }
        }

        //private PathData _GetPathData()
        //{

        //    int ptSize = (int)Marshal.SizeOf(typeof(GPPOINTF));

        //    int numPts = PointCount;

        //    PathData pathData = new PathData();
        //    pathData.Types = new byte[numPts];

        //    IntPtr memoryPathData = Marshal.AllocHGlobal(3 * IntPtr.Size);
        //    IntPtr memoryPoints = Marshal.AllocHGlobal(checked(ptSize * numPts));
        //    try
        //    {
        //        GCHandle typesHandle = GCHandle.Alloc(pathData.Types, GCHandleType.Pinned);
        //        try
        //        {
        //            IntPtr typesPtr = typesHandle.AddrOfPinnedObject();
        //            //IntPtr typesPtr = Marshal.AddrOfArrayElement(pathData.Types, IntPtr.Zero);

        //            Marshal.StructureToPtr(numPts, memoryPathData, false);
        //            Marshal.StructureToPtr(memoryPoints, (IntPtr)((long)memoryPathData + IntPtr.Size), false);
        //            Marshal.StructureToPtr(typesPtr, (IntPtr)((long)memoryPathData + 2 * IntPtr.Size), false);

        //            int status = SafeNativeMethods.Gdip.GdipGetPathData(new HandleRef(this, nativePath), memoryPathData);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            pathData.Points = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(memoryPoints, numPts);
        //        }
        //        finally
        //        {
        //            typesHandle.Free();
        //        }
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(memoryPathData);
        //        Marshal.FreeHGlobal(memoryPoints);
        //    }

        //    return pathData;
        //}

        /// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.PathData"]/*' />
        /// <devdoc>
        ///    Gets a <see cref='System.Drawing.Drawing2D.PathData'/> object that
        ///    encapsulates both the <see cref='System.Drawing.Drawing2D.GraphicsPath.PathPoints'/> and <see cref='System.Drawing.Drawing2D.GraphicsPath.PathTypes'/> arrays of this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /// </devdoc>
        public PathData PathData
        {
            get
            {
                return null;// return _GetPathData();
            }
        }

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.StartFigure"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Starts a new figure without closing the
        /////       current figure. All subsequent points added to the path are added to this new
        /////       figure.
        /////    </para>
        ///// </devdoc>
        //public void StartFigure()
        //{
        //    //int status = SafeNativeMethods.Gdip.GdipStartPathFigure(new HandleRef(this, nativePath));

        //    //if (status != SafeNativeMethods.Gdip.Ok)
        //    //    throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public void CloseFigure()
        //{
        //    //int status = SafeNativeMethods.Gdip.GdipClosePathFigure(new HandleRef(this, nativePath));

        //    //if (status != SafeNativeMethods.Gdip.Ok)
        //    //    throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public void CloseAllFigures()
        //{
        //    int status = SafeNativeMethods.Gdip.GdipClosePathFigures(new HandleRef(this, nativePath));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.SetMarkers"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Sets a marker on this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> .
        /////    </para>
        ///// </devdoc>
        //public void SetMarkers()
        //{
        //    int status = SafeNativeMethods.Gdip.GdipSetPathMarker(new HandleRef(this, nativePath));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.ClearMarkers"]/*' />
        ///// <devdoc>
        /////    Clears all markers from this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //public void ClearMarkers()
        //{
        //    int status = SafeNativeMethods.Gdip.GdipClearPathMarkers(new HandleRef(this, nativePath));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Reverse"]/*' />
        ///// <devdoc>
        /////    Reverses the order of points in the <see cref='System.Drawing.Drawing2D.GraphicsPath.PathPoints'/> array of this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //public void Reverse()
        //{
        //    int status = SafeNativeMethods.Gdip.GdipReversePath(new HandleRef(this, nativePath));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GetLastPoint"]/*' />
        ///// <devdoc>
        /////    Gets the last point in the <see cref='System.Drawing.Drawing2D.GraphicsPath.PathPoints'/> array of this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //public PointF GetLastPoint()
        //{
        //    GPPOINTF gppt = new GPPOINTF();

        //    int status = SafeNativeMethods.Gdip.GdipGetPathLastPoint(new HandleRef(this, nativePath), gppt);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);

        //    return gppt.ToPoint();
        //}

        ///*
        // * Hit testing
        // */

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsVisible"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether the specified point is contained
        /////       within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>
        /////       .
        /////    </para>
        ///// </devdoc>
        //public bool IsVisible(float x, float y)
        //{
        //    return IsVisible(new PointF(x, y), (Graphics)null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsVisible1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether the specified point is contained
        /////       within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsVisible(PointF point)
        //{
        //    return IsVisible(point, (Graphics)null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsVisible2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether the specified point is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> in the visible clip region of the
        /////       specified <see cref='System.Drawing.Graphics'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsVisible(float x, float y, Graphics graphics)
        //{
        //    return IsVisible(new PointF(x, y), graphics);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsVisible3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether the specified point is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsVisible(PointF pt, Graphics graphics)
        //{
        //    return true;
        //    //int isVisible;

        //    //int status = SafeNativeMethods.Gdip.GdipIsVisiblePathPoint(new HandleRef(this, nativePath),
        //    //                                            pt.X,
        //    //                                            pt.Y,
        //    //                                            new HandleRef(graphics, (graphics != null) ? 
        //    //                                                graphics.NativeGraphics : IntPtr.Zero),
        //    //                                            out isVisible);

        //    //if (status != SafeNativeMethods.Gdip.Ok)
        //    //    throw SafeNativeMethods.Gdip.StatusException(status);

        //    //return isVisible != 0;
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsVisible4"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether the specified point is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> .
        /////    </para>
        ///// </devdoc>
        //public bool IsVisible(int x, int y)
        //{
        //    return IsVisible(new Point(x, y), (Graphics)null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsVisible5"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether the specified point is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsVisible(Point point)
        //{
        //    return IsVisible(point, (Graphics)null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsVisible6"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether the specified point is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> in the visible clip region of the
        /////       specified <see cref='System.Drawing.Graphics'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsVisible(int x, int y, Graphics graphics)
        //{
        //    return IsVisible(new Point(x, y), graphics);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsVisible7"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether the specified point is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsVisible(Point pt, Graphics graphics)
        //{
        //    return true;
        //    //int isVisible;

        //    //int status = SafeNativeMethods.Gdip.GdipIsVisiblePathPointI(new HandleRef(this, nativePath),
        //    //                                             pt.X,
        //    //                                             pt.Y,
        //    //                                             new HandleRef(graphics, (graphics != null) ? 
        //    //                                                 graphics.NativeGraphics : IntPtr.Zero),
        //    //                                             out isVisible);

        //    //if (status != SafeNativeMethods.Gdip.Ok)
        //    //    throw SafeNativeMethods.Gdip.StatusException(status);

        //    //return isVisible != 0;
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsOutlineVisible"]/*' />
        ///// <devdoc>
        /////    Indicates whether an outline drawn by the
        /////    specified <see cref='System.Drawing.Pen'/> at the specified location is contained
        /////    within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //public bool IsOutlineVisible(float x, float y, Pen pen)
        //{
        //    return IsOutlineVisible(new PointF(x, y), pen, (Graphics)null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsOutlineVisible1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether an outline drawn by the specified <see cref='System.Drawing.Pen'/> at the
        /////       specified location is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsOutlineVisible(PointF point, Pen pen)
        //{
        //    return IsOutlineVisible(point, pen, (Graphics)null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsOutlineVisible2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether an outline drawn by the specified <see cref='System.Drawing.Pen'/> at the
        /////       specified location is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> and within the visible clip region of
        /////       the specified <see cref='System.Drawing.Graphics'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsOutlineVisible(float x, float y, Pen pen, Graphics graphics)
        //{
        //    return IsOutlineVisible(new PointF(x, y), pen, graphics);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsOutlineVisible3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether an outline drawn by the specified
        /////    <see cref='System.Drawing.Pen'/> at the specified 
        /////       location is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> and within the visible clip region of
        /////       the specified <see cref='System.Drawing.Graphics'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsOutlineVisible(PointF pt, Pen pen, Graphics graphics)
        //{
        //    return true;
        //    //int isVisible;

        //    //if (pen == null)
        //    //    throw new ArgumentNullException("pen");

        //    //int status = SafeNativeMethods.Gdip.GdipIsOutlineVisiblePathPoint(new HandleRef(this, nativePath),
        //    //                                                   pt.X,
        //    //                                                   pt.Y,
        //    //                                                   new HandleRef(pen, pen.NativePen),
        //    //                                                   new HandleRef(graphics, (graphics != null) ? 
        //    //                                                       graphics.NativeGraphics : IntPtr.Zero),
        //    //                                                   out isVisible);

        //    //if (status != SafeNativeMethods.Gdip.Ok)
        //    //    throw SafeNativeMethods.Gdip.StatusException(status);

        //    //return isVisible != 0;
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsOutlineVisible4"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether an outline drawn by the specified <see cref='System.Drawing.Pen'/> at the
        /////       specified location is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsOutlineVisible(int x, int y, Pen pen)
        //{
        //    return IsOutlineVisible(new Point(x, y), pen, (Graphics)null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsOutlineVisible5"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether an outline drawn by the specified <see cref='System.Drawing.Pen'/> at the
        /////       specified location is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsOutlineVisible(Point point, Pen pen)
        //{
        //    return IsOutlineVisible(point, pen, (Graphics)null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsOutlineVisible6"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether an outline drawn by the specified <see cref='System.Drawing.Pen'/> at the
        /////       specified location is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> and within the visible clip region of
        /////       the specified <see cref='System.Drawing.Graphics'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsOutlineVisible(int x, int y, Pen pen, Graphics graphics)
        //{
        //    return IsOutlineVisible(new Point(x, y), pen, graphics);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.IsOutlineVisible7"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Indicates whether an outline drawn by the specified
        /////    <see cref='System.Drawing.Pen'/> at the specified 
        /////       location is contained within this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> and within the visible clip region of
        /////       the specified <see cref='System.Drawing.Graphics'/>.
        /////    </para>
        ///// </devdoc>
        //public bool IsOutlineVisible(Point pt, Pen pen, Graphics graphics)
        //{
        //    return true;
        //    //int isVisible;

        //    //if (pen == null)
        //    //    throw new ArgumentNullException("pen");

        //    //int status = SafeNativeMethods.Gdip.GdipIsOutlineVisiblePathPointI(new HandleRef(this, nativePath),
        //    //                                                    pt.X,
        //    //                                                    pt.Y,
        //    //                                                    new HandleRef(pen, pen.NativePen),
        //    //                                                    new HandleRef(graphics, (graphics != null) ?
        //    //                                                        graphics.NativeGraphics : IntPtr.Zero),
        //    //                                                    out isVisible);

        //    //if (status != SafeNativeMethods.Gdip.Ok)
        //    //    throw SafeNativeMethods.Gdip.StatusException(status);

        //    //return isVisible != 0;
        //}

        ///*
        // * Add lines to the path object
        // */
        //// float version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddLine"]/*' />
        ///// <devdoc>
        /////    Appends a line segment to this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //public void AddLine(PointF pt1, PointF pt2)
        //{
        //    AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddLine1"]/*' />
        ///// <devdoc>
        /////    Appends a line segment to this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //public void AddLine(float x1, float y1, float x2, float y2)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathLine(new HandleRef(this, nativePath), x1, y1, x2, y2);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public void AddLines(PointF[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathLine2(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }

        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddLine2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Appends a line segment to this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public void AddLine(Point pt1, Point pt2)
        //{
        //    AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddLine3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Appends a line segment to this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public void AddLine(int x1, int y1, int x2, int y2)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathLineI(new HandleRef(this, nativePath), x1, y1, x2, y2);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public void AddLines(Point[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathLine2I(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///*
        // * Add an arc to the path object
        // */
        //// float version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddArc"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Appends an elliptical arc to the current
        /////       figure.
        /////    </para>
        ///// </devdoc>
        //public void AddArc(RectangleF rect, float startAngle, float sweepAngle)
        //{
        //    AddArc(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddArc1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Appends an elliptical arc to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddArc(float x, float y, float width, float height,
        //                   float startAngle, float sweepAngle)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathArc(new HandleRef(this, nativePath), x, y, width, height,
        //                                        startAngle, sweepAngle);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddArc2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Appends an elliptical arc to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddArc(Rectangle rect, float startAngle, float sweepAngle)
        //{
        //    AddArc(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddArc3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Appends an elliptical arc to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddArc(int x, int y, int width, int height,
        //                   float startAngle, float sweepAngle)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathArcI(new HandleRef(this, nativePath), x, y, width, height,
        //                                         startAngle, sweepAngle);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///*
        //* Add Bezier curves to the path object
        //*/
        //// float version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddBezier"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a cubic Bzier curve to the current
        /////       figure.
        /////    </para>
        ///// </devdoc>
        //public void AddBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        //{
        //    AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddBezier1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a cubic Bzier curve to the current
        /////       figure.
        /////    </para>
        ///// </devdoc>
        //public void AddBezier(float x1, float y1, float x2, float y2,
        //                      float x3, float y3, float x4, float y4)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathBezier(new HandleRef(this, nativePath), x1, y1, x2, y2,
        //                                           x3, y3, x4, y4);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddBeziers"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a sequence of connected cubic Bzier
        /////       curves to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddBeziers(PointF[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathBeziers(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddBezier2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a cubic Bzier curve to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddBezier(Point pt1, Point pt2, Point pt3, Point pt4)
        //{
        //    AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddBezier3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a cubic Bzier curve to the current
        /////       figure.
        /////    </para>
        ///// </devdoc>
        //public void AddBezier(int x1, int y1, int x2, int y2,
        //                      int x3, int y3, int x4, int y4)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathBezierI(new HandleRef(this, nativePath), x1, y1, x2, y2,
        //                                            x3, y3, x4, y4);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddBeziers1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a sequence of connected cubic Bzier curves to the
        /////       current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddBeziers(params Point[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathBeziersI(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///*
        // * Add cardinal splines to the path object
        // */
        //// float version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddCurve"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a spline curve to the current figure.
        /////       A Cardinal spline curve is used because the curve travels through each of the
        /////       points in the array.
        /////    </para>
        ///// </devdoc>
        //public void AddCurve(PointF[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathCurve(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddCurve1"]/*' />
        ///// <devdoc>
        /////    Adds a spline curve to the current figure.
        ///// </devdoc>
        //public void AddCurve(PointF[] points, float tension)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathCurve2(new HandleRef(this, nativePath), new HandleRef(null, buf),
        //                                           points.Length, tension);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddCurve2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a spline curve to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddCurve(PointF[] points, int offset, int numberOfSegments,
        //                     float tension)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathCurve3(new HandleRef(this, nativePath), new HandleRef(null, buf),
        //                                           points.Length, offset,
        //                                           numberOfSegments, tension);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddCurve3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a spline curve to the current figure. A Cardinal spline curve is used
        /////       because the curve travels through each of the points in the array.
        /////    </para>
        ///// </devdoc>
        //public void AddCurve(Point[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathCurveI(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddCurve4"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a spline curve to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddCurve(Point[] points, float tension)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathCurve2I(new HandleRef(this, nativePath), new HandleRef(null, buf),
        //                                            points.Length, tension);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddCurve5"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a spline curve to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddCurve(Point[] points, int offset, int numberOfSegments,
        //                     float tension)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathCurve3I(new HandleRef(this, nativePath), new HandleRef(null, buf),
        //                                            points.Length, offset,
        //                                            numberOfSegments, tension);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        //// float version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddClosedCurve"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a closed curve to the current figure. A Cardinal spline curve is
        /////       used because the curve travels through each of the points in the array.
        /////    </para>
        ///// </devdoc>
        //public void AddClosedCurve(PointF[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathClosedCurve(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddClosedCurve1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a closed curve to the current figure. A Cardinal spline curve is
        /////       used because the curve travels through each of the points in the array.
        /////    </para>
        ///// </devdoc>
        //public void AddClosedCurve(PointF[] points, float tension)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathClosedCurve2(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length, tension);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddClosedCurve2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a closed curve to the current figure. A Cardinal spline curve is used
        /////       because the curve travels through each of the points in the array.
        /////    </para>
        ///// </devdoc>
        //public void AddClosedCurve(Point[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathClosedCurveI(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddClosedCurve3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a closed curve to the current figure. A Cardinal spline curve is used
        /////       because the curve travels through each of the points in the array.
        /////    </para>
        ///// </devdoc>
        //public void AddClosedCurve(Point[] points, float tension)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathClosedCurve2I(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length, tension);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddRectangle"]/*' />
        ///// <devdoc>
        /////    Adds a rectangle to the current figure.
        ///// </devdoc>
        //public void AddRectangle(RectangleF rect)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathRectangle(new HandleRef(this, nativePath), rect.X, rect.Y,
        //                                              rect.Width, rect.Height);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddRectangles"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a series of rectangles to the current
        /////       figure.
        /////    </para>
        ///// </devdoc>
        //public void AddRectangles(RectangleF[] rects)
        //{
        //    if (rects == null)
        //        throw new ArgumentNullException("rects");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathRectangles(new HandleRef(this, nativePath), new HandleRef(null, buf), rects.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddRectangle1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a rectangle to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddRectangle(Rectangle rect)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathRectangleI(new HandleRef(this, nativePath), rect.X, rect.Y,
        //                                               rect.Width, rect.Height);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddRectangles1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a series of rectangles to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddRectangles(Rectangle[] rects)
        //{
        //    if (rects == null)
        //        throw new ArgumentNullException("rects");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathRectanglesI(new HandleRef(this, nativePath), new HandleRef(null, buf), rects.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        //// float version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddEllipse"]/*' />
        ///// <devdoc>
        /////    Adds an ellipse to the current figure.
        ///// </devdoc>
        //public void AddEllipse(RectangleF rect)
        //{
        //    AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);
        //}

        ///**
        // * Add an ellipse to the current path
        // *
        // * !!! Need to handle the status code returned
        // *  by the native GDI+ APIs.
        // */
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddEllipse1"]/*' />
        ///// <devdoc>
        /////    Adds an ellipse to the current figure.
        ///// </devdoc>
        //public void AddEllipse(float x, float y, float width, float height)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathEllipse(new HandleRef(this, nativePath), x, y, width, height);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddEllipse2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds an ellipse to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddEllipse(Rectangle rect)
        //{
        //    AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);
        //}

        ///**
        // * Add an ellipse to the current path
        // *
        // * !!! Need to handle the status code returned
        // *  by the native GDI+ APIs.
        // */
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddEllipse3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds an ellipse to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddEllipse(int x, int y, int width, int height)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathEllipseI(new HandleRef(this, nativePath), x, y, width, height);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddPie"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds the outline of a pie shape to the
        /////       current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddPie(Rectangle rect, float startAngle, float sweepAngle)
        //{
        //    AddPie(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
        //}

        //// float version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddPie1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds the outline of a pie shape to the current
        /////       figure.
        /////    </para>
        ///// </devdoc>
        //public void AddPie(float x, float y, float width, float height,
        //                   float startAngle, float sweepAngle)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathPie(new HandleRef(this, nativePath), x, y, width, height,
        //                                        startAngle, sweepAngle);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddPie2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds the outline of a pie shape to the current
        /////       figure.
        /////    </para>
        ///// </devdoc>
        //public void AddPie(int x, int y, int width, int height,
        //                   float startAngle, float sweepAngle)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipAddPathPieI(new HandleRef(this, nativePath), x, y, width, height,
        //                                         startAngle, sweepAngle);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        //// float version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddPolygon"]/*' />
        ///// <devdoc>
        /////    Adds a polygon to the current figure.
        ///// </devdoc>
        //public void AddPolygon(PointF[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathPolygon(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        //// int version
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddPolygon1"]/*' />
        ///// <devdoc>
        /////    Adds a polygon to the current figure.
        ///// </devdoc>
        //public void AddPolygon(Point[] points)
        //{
        //    if (points == null)
        //        throw new ArgumentNullException("points");
        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipAddPathPolygonI(new HandleRef(this, nativePath), new HandleRef(null, buf), points.Length);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddPath"]/*' />
        ///// <devdoc>
        /////    Appends the specified <see cref='System.Drawing.Drawing2D.GraphicsPath'/> to this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //public void AddPath(GraphicsPath addingPath,
        //                    bool connect)
        //{
        //    if (addingPath == null)
        //        throw new ArgumentNullException("addingPath");

        //    int status = SafeNativeMethods.Gdip.GdipAddPathPath(new HandleRef(this, nativePath), new HandleRef(addingPath, addingPath.nativePath), connect);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///*
        // * Add text string to the path object
        // *
        // * @notes The final form of this API is yet to be defined.
        // * @notes What are the choices for the format parameter?
        // */

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddString"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a text string to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddString(String s, FontFamily family, int style, float emSize,
        //                      PointF origin, StringFormat format)
        //{
        //    GPRECTF rectf = new GPRECTF(origin.X, origin.Y, 0, 0);

        //    int status = SafeNativeMethods.Gdip.GdipAddPathString(new HandleRef(this, nativePath),
        //                                           s,
        //                                           s.Length,
        //                                           new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero),
        //                                           style,
        //                                           emSize,
        //                                           ref rectf,
        //                                           new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddString1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a text string to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddString(String s, FontFamily family, int style, float emSize,
        //                      Point origin, StringFormat format)
        //{
        //    GPRECT rect = new GPRECT(origin.X, origin.Y, 0, 0);

        //    int status = SafeNativeMethods.Gdip.GdipAddPathStringI(new HandleRef(this, nativePath),
        //                                            s,
        //                                            s.Length,
        //                                            new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero),
        //                                            style,
        //                                            emSize,
        //                                            ref rect,
        //                                            new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddString2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a text string to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddString(String s, FontFamily family, int style, float emSize,
        //                      RectangleF layoutRect, StringFormat format)
        //{
        //    GPRECTF rectf = new GPRECTF(layoutRect);
        //    int status = SafeNativeMethods.Gdip.GdipAddPathString(new HandleRef(this, nativePath),
        //                                           s,
        //                                           s.Length,
        //                                           new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero),
        //                                           style,
        //                                           emSize,
        //                                           ref rectf,
        //                                           new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.AddString3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Adds a text string to the current figure.
        /////    </para>
        ///// </devdoc>
        //public void AddString(String s, FontFamily family, int style, float emSize,
        //                      Rectangle layoutRect, StringFormat format)
        //{
        //    GPRECT rect = new GPRECT(layoutRect);
        //    int status = SafeNativeMethods.Gdip.GdipAddPathStringI(new HandleRef(this, nativePath),
        //                                           s,
        //                                           s.Length,
        //                                           new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero),
        //                                           style,
        //                                           emSize,
        //                                           ref rect,
        //                                           new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Transform"]/*' />
        ///// <devdoc>
        /////    Applies a transform matrix to this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///// </devdoc>
        //public void Transform(Matrix matrix)
        //{
        //    if (matrix == null)
        //        throw new ArgumentNullException("matrix");

        //    // !! Is this an optimization?  We should catch this in GdipTransformPath                                                                                       
        //    if (matrix.nativeMatrix == IntPtr.Zero)
        //        return;

        //    int status = SafeNativeMethods.Gdip.GdipTransformPath(new HandleRef(this, nativePath),
        //                                           new HandleRef(matrix, matrix.nativeMatrix));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GetBounds"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Returns a rectangle that bounds this <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /////    </para>
        ///// </devdoc>
        //public RectangleF GetBounds()
        //{
        //    return GetBounds(null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GetBounds1"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Returns a rectangle that bounds this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> when it
        /////       is transformed by the specified <see cref='System.Drawing.Drawing2D.Matrix'/>.
        /////    </para>
        ///// </devdoc>
        //public RectangleF GetBounds(Matrix matrix)
        //{
        //    return GetBounds(matrix, null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.GetBounds2"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Returns a rectangle that bounds this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> when it is
        /////       transformed by the specified <see cref='System.Drawing.Drawing2D.Matrix'/>. and drawn with the specified <see cref='System.Drawing.Pen'/>.
        /////    </para>
        ///// </devdoc>
        //public RectangleF GetBounds(Matrix matrix, Pen pen)
        //{
        //    GPRECTF gprectf = new GPRECTF();

        //    IntPtr nativeMatrix = IntPtr.Zero, nativePen = IntPtr.Zero;

        //    if (matrix != null)
        //        nativeMatrix = matrix.nativeMatrix;

        //    if (pen != null)
        //        nativePen = pen.NativePen;

        //    int status = SafeNativeMethods.Gdip.GdipGetPathWorldBounds(new HandleRef(this, nativePath),
        //                                                ref gprectf,
        //                                                new HandleRef(matrix, nativeMatrix),
        //                                                new HandleRef(pen, nativePen));

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);

        //    return gprectf.ToRectangleF();
        //}

        ///*
        // * Flatten the path object
        // */

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Flatten"]/*' />
        ///// <devdoc>
        /////    Converts each curve in this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> into a sequence of connected line
        /////    segments.
        ///// </devdoc>
        //public void Flatten()
        //{
        //    Flatten(null);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Flatten1"]/*' />
        ///// <devdoc>
        /////    Converts each curve in this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> into a sequence of connected line
        /////    segments.
        ///// </devdoc>
        //public void Flatten(Matrix matrix)
        //{
        //    Flatten(matrix, 0.25f);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Flatten2"]/*' />
        ///// <devdoc>
        /////    Converts each curve in this <see cref='System.Drawing.Drawing2D.GraphicsPath'/> into a sequence of connected line
        /////    segments.
        ///// </devdoc>
        //public void Flatten(Matrix matrix, float flatness)
        //{

        //    int status = SafeNativeMethods.Gdip.GdipFlattenPath(new HandleRef(this, nativePath),
        //                                                   new HandleRef(matrix, (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix),
        //                                                   flatness);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}


        ///**
        // * Widen the path object
        // *
        // * @notes We don't have an API yet.
        // *  Should we just take in a GeometricPen as parameter?
        // */
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Widen"]/*' />
        ///// <devdoc>
        ///// </devdoc>
        //public void Widen(Pen pen)
        //{
        //    float flatness = (float)2.0 / (float)3.0;
        //    Widen(pen, (Matrix)null, flatness);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Widen1"]/*' />
        ///// <devdoc>
        /////    <para>[To be supplied.]</para>
        ///// </devdoc>
        //public void Widen(Pen pen, Matrix matrix)
        //{
        //    float flatness = (float)2.0 / (float)3.0;
        //    Widen(pen, matrix, flatness);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Widen2"]/*' />
        ///// <devdoc>
        /////    <para>[To be supplied.]</para>
        ///// </devdoc>
        //public void Widen(Pen pen,
        //                  Matrix matrix,
        //                  float flatness)
        //{
        //    IntPtr nativeMatrix;

        //    if (matrix == null)
        //        nativeMatrix = IntPtr.Zero;
        //    else
        //        nativeMatrix = matrix.nativeMatrix;

        //    if (pen == null)
        //        throw new ArgumentNullException("pen");

        //    // 



        //    int pointCount;
        //    SafeNativeMethods.Gdip.GdipGetPointCount(new HandleRef(this, nativePath), out pointCount);

        //    if (pointCount == 0)
        //        return;

        //    int status = SafeNativeMethods.Gdip.GdipWidenPath(new HandleRef(this, nativePath),
        //                        new HandleRef(pen, pen.NativePen),
        //                        new HandleRef(matrix, nativeMatrix),
        //                        flatness);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Warp"]/*' />
        ///// <devdoc>
        /////    <para>[To be supplied.]</para>
        ///// </devdoc>
        //[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //public void Warp(PointF[] destPoints, RectangleF srcRect)
        //{ Warp(destPoints, srcRect, null); }

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Warp1"]/*' />
        ///// <devdoc>
        /////    <para>[To be supplied.]</para>
        ///// </devdoc>
        //[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix)
        //{ Warp(destPoints, srcRect, matrix, WarpMode.Perspective); }

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Warp2"]/*' />
        ///// <devdoc>
        /////    <para>[To be supplied.]</para>
        ///// </devdoc>
        //[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix,
        //                 WarpMode warpMode)
        //{ Warp(destPoints, srcRect, matrix, warpMode, 0.25f); }

        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.Warp3"]/*' />
        ///// <devdoc>
        /////    <para>[To be supplied.]</para>
        ///// </devdoc>
        //[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix,
        //                 WarpMode warpMode, float flatness)
        //{
        //    if (destPoints == null)
        //        throw new ArgumentNullException("destPoints");

        //    IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
        //    try
        //    {
        //        int status = SafeNativeMethods.Gdip.GdipWarpPath(new HandleRef(this, nativePath),
        //                                      new HandleRef(matrix, (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix),
        //                                      new HandleRef(null, buf),
        //                                      destPoints.Length,
        //                                      srcRect.X,
        //                                      srcRect.Y,
        //                                      srcRect.Width,
        //                                      srcRect.Height,
        //                                      warpMode,
        //                                      flatness);
        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buf);
        //    }
        //}

        ///**
        // * Return the number of points in the current path
        // */
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.PointCount"]/*' />
        ///// <devdoc>
        /////    <para>[To be supplied.]</para>
        ///// </devdoc>
        //public int PointCount
        //{
        //    get
        //    {
        //        int count = 0;

        //        int status = SafeNativeMethods.Gdip.GdipGetPointCount(new HandleRef(this, nativePath), out count);

        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);

        //        return count;
        //    }
        //}

        ///**
        // * Return the path point type information
        // */
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.PathTypes"]/*' />
        ///// <devdoc>
        /////    <para>[To be supplied.]</para>
        ///// </devdoc>
        //public byte[] PathTypes
        //{
        //    get
        //    {
        //        int count = PointCount;

        //        byte[] types = new byte[count];

        //        int status = SafeNativeMethods.Gdip.GdipGetPathTypes(new HandleRef(this, nativePath), types, count);

        //        if (status != SafeNativeMethods.Gdip.Ok)
        //            throw SafeNativeMethods.Gdip.StatusException(status);

        //        return types;
        //    }
        //}

        ///*
        // * Return the path point coordinate information
        // * @notes Should there be PathData that contains types[] and points[]
        // *        for get & set purposes.
        // */
        //// float points
        ///// <include file='doc\GraphicsPath.uex' path='docs/doc[@for="GraphicsPath.PathPoints"]/*' />
        ///// <devdoc>
        /////    <para>[To be supplied.]</para>
        ///// </devdoc>
        //public PointF[] PathPoints
        //{
        //    get
        //    {
        //        int count = PointCount;
        //        int size = (int)Marshal.SizeOf(typeof(GPPOINTF));
        //        IntPtr buf = Marshal.AllocHGlobal(checked(count * size));
        //        try
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipGetPathPoints(new HandleRef(this, nativePath), new HandleRef(null, buf), count);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            PointF[] points = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(buf, count);
        //            return points;
        //        }
        //        finally
        //        {
        //            Marshal.FreeHGlobal(buf);
        //        }
        //    }
        //}
    }
}
