//------------------------------------------------------------------------------
// <copyright file="Graphics.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

//#define FINALIZATION_WATCH

namespace System.Drawing
{
    using DCSoft;
    using System.Text;
    using System;

    using System.Runtime.InteropServices;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System;
    using Microsoft.Win32;
    using System.Security;
    using System.Security.Permissions;
    using System.Drawing.Internal;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.Drawing.Drawing2D;
    using System.Runtime.ConstrainedExecution;
    using System.Globalization;
    using System.Runtime.Versioning;

    internal class MyBinaryBuilder : IDisposable
    {
        public static readonly byte[] GlobalBufferForPackage = new byte[1024 * 5];

        private static byte[] _GlobalBuffer = new byte[1024 * 50];
        internal static byte[] Ref_GlobalBuffer = _GlobalBuffer;
        public MyBinaryBuilder()
        {
            if (_GlobalBuffer == null)
            {
                this._Buffer = new byte[1024];
            }
            else
            {
                this._Buffer = _GlobalBuffer;
                _GlobalBuffer = null;
            }
            this._BufferLength = this._Buffer.Length;
        }

        public MyBinaryBuilder(byte[] bsBuffer)
        {
            if (bsBuffer == null || bsBuffer.Length == 0)
            {
                throw new ArgumentNullException("bsBuffer");
            }
            this._Buffer = bsBuffer;
            this._BufferLength = this._Buffer.Length;
        }

        private int _BufferLength;
        internal byte[] _Buffer;
        public void Dispose()
        {
            if (this._Buffer == Ref_GlobalBuffer)
            {
                _GlobalBuffer = this._Buffer;
            }
            this._BufferLength = 0;
            this._Buffer = null;
        }
        internal int _Length;
        public int Length
        {
            get
            {
                return this._Length;
            }
        }

        private void EnsureCapacity(int newSize)
        {
            //if( this._Length > 169)
            //{
            //    Console.WriteLine(this._Length.ToString());
            //}
            if (this._BufferLength < newSize)
            {
                var temp = new byte[(int)(newSize * 1.5)];
                System.Buffer.BlockCopy(this._Buffer, 0, temp, 0, this._Length);
                if (this._Buffer == Ref_GlobalBuffer)
                {
                    Ref_GlobalBuffer = temp;
                }
                this._Buffer = temp;
                this._BufferLength = temp.Length;

                //if (_SizeList.ContainsKey( temp.Length))
                //{
                //    _SizeList[temp.Length]++;
                //}
                //else
                //{
                //    _SizeList[temp.Length] = 1;
                //}


            }
        }
        public void AppendRecordType(GraphicsRecordType type)
        {
            //this.AppendByte((byte)0xff);
            //this.AppendByte((byte)0xff);
            //if (WriterViewControl._ForPaintDataMD5String)
            //{
            //    this.AppendString(type.ToString());
            //}
            //else
            //{
            this.AppendByte((byte)type);
            //}
        }
        public void AppendByteArray(byte[] bs)
        {
            if (bs == null || bs.Length == 0)
            {
                EnsureCapacity(this._Length + 2);
                this._Buffer[this._Length++] = 0;
                this._Buffer[this._Length++] = 0;
            }
            else
            {
                EnsureCapacity(this._Length + bs.Length + 4);
                this.AppendInt32(bs.Length);
                //this._Buffer[this._Length++] = (byte)(bs.Length >> 8);
                //this._Buffer[this._Length++] = (byte)(bs.Length & 0xff);
                System.Buffer.BlockCopy(bs, 0, this._Buffer, this._Length, bs.Length);
                this._Length += bs.Length;
            }
        }
        public void AppendBoolean(bool v)
        {
            this.AppendByte(v ? (byte)1 : (byte)0);
        }
        public void AppendByte(byte b)
        {
            if (this._Length == this._BufferLength)
            {
                EnsureCapacity(this._Length + 1);
            }
            this._Buffer[this._Length++] = b;
        }

        //public void AppendLine()
        //{
        //    EnsureCapacity(this._Length + 1);
        //    this._Buffer[this._Length++] = '\n';
        //}
        private System.Text.Encoding _UTF8 = System.Text.Encoding.UTF8;

        [ThreadStatic]
        private static byte[] _TextBuffer = new byte[1024];
        public void AppendString(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                // 空字符串
                EnsureCapacity(this._Length + 4);
                this._Buffer[this._Length] = 0;
                this._Buffer[this._Length + 1] = 0;
                this._Buffer[this._Length + 2] = 0;
                this._Buffer[this._Length + 3] = 0;
                this._Length += 4;
                return;
            }
            if (_TextBuffer == null || _TextBuffer.Length < txt.Length * 3)
            {
                _TextBuffer = new byte[txt.Length * 3];
            }
            var len = _UTF8.GetBytes(txt, 0, txt.Length, _TextBuffer, 0);
            EnsureCapacity(this._Length + len + 4);
            this.AppendInt32(len);
            //this._Buffer[this._Length++] = (byte)(len >> 8);
            //this._Buffer[this._Length++] = (byte)(len & 0xff);
            System.Buffer.BlockCopy(_TextBuffer, 0, this._Buffer, this._Length, len);
            this._Length += len;
        }

        public void AppendBinaryBuilder(MyBinaryBuilder str)
        {
            EnsureCapacity(this._Length + str._Length);
            Array.Copy(str._Buffer, 0, this._Buffer, this._Length, str._Length);
            this._Length += str._Length;
        }

        public void AppendInt32(int v)
        {
            var len = this._Length;
            if (this._BufferLength < len + 4)
            {
                EnsureCapacity(len + 4);
            }
            var buffer = this._Buffer;
            buffer[len] = (byte)(v >> 24);
            buffer[len + 1] = (byte)(v >> 16);
            buffer[len + 2] = (byte)(v >> 8);
            buffer[len + 3] = (byte)(v);
            this._Length = len + 4;
        }
        public void Append2Int32(int v1, int v2)
        {
            var len = this._Length;
            if (this._BufferLength < len + 8)
            {
                EnsureCapacity(len + 8);
            }
            var buffer = this._Buffer;
            buffer[len] = (byte)(v1 >> 24);
            buffer[len + 1] = (byte)(v1 >> 16);
            buffer[len + 2] = (byte)(v1 >> 8);
            buffer[len + 3] = (byte)(v1);

            buffer[len + 4] = (byte)(v2 >> 24);
            buffer[len + 5] = (byte)(v2 >> 16);
            buffer[len + 6] = (byte)(v2 >> 8);
            buffer[len + 7] = (byte)(v2);

            this._Length = len + 8;
        }
        public void Append4Int32(int v1, int v2, int v3, int v4)
        {
            var len = this._Length;
            if (this._BufferLength < len + 16)
            {
                EnsureCapacity(len + 16);
            }
            var buffer = this._Buffer;
            buffer[len] = (byte)(v1 >> 24);
            buffer[len + 1] = (byte)(v1 >> 16);
            buffer[len + 2] = (byte)(v1 >> 8);
            buffer[len + 3] = (byte)(v1);

            buffer[len + 4] = (byte)(v2 >> 24);
            buffer[len + 5] = (byte)(v2 >> 16);
            buffer[len + 6] = (byte)(v2 >> 8);
            buffer[len + 7] = (byte)(v2);

            buffer[len + 8] = (byte)(v3 >> 24);
            buffer[len + 9] = (byte)(v3 >> 16);
            buffer[len + 10] = (byte)(v3 >> 8);
            buffer[len + 11] = (byte)(v3);

            buffer[len + 12] = (byte)(v4 >> 24);
            buffer[len + 13] = (byte)(v4 >> 16);
            buffer[len + 14] = (byte)(v4 >> 8);
            buffer[len + 15] = (byte)(v4);

            this._Length = len + 16;
        }


        public void AppendSingle(float v)
        {
            var intv = System.BitConverter.SingleToInt32Bits(v);
            AppendInt32(intv);
        }

        public void AppendInt16(short v)
        {
            var len = this._Length;
            EnsureCapacity(len + 2);
            this._Buffer[len] = (byte)((v >> 8) & 0xff);
            this._Buffer[len + 1] = (byte)(v & 0xff);
            this._Length = len + 2;
        }
        public byte[] ToByteArray()
        {
            //if (this._Length == this._Buffer.Length)
            //{
            //    return this._Buffer;
            //}
            //else
            {
                var bs = new byte[this._Length];
                System.Buffer.BlockCopy(this._Buffer, 0, bs, 0, this._Length);
                return bs;
            }
        }

        public override string ToString()
        {
            return "Bytes " + this._Length;
        }
        //public bool NeedAddSpliter()
        //{
        //    return this._Length > 0 && this._Buffer[this._Length - 1] != ',';
        //}
    }
    public enum GraphicsTextBaseline
    {
        Top = 0,
        Middle = 1,
        Bottom = 2,
        hanging = 3
    }
    internal enum GraphicsRecordType
    {
        First = 0,
        SetCurrentFont = 1,
        SetCurrentBrush = 2,
        SetCurrentPen = 3,
        AddPage = 4,
        DrawLine = 5,
        DrawString = 6,
        DrawChar = 7,
        DrawRoundRectangle = 8,
        DrawRectangle = 9,
        FillRectangle = 10,
        DrawEllipse = 11,
        FillEllipse = 12,
        CheckVersion = 13,
        SetTransform = 14,
        DrawImageExt = 15,
        Save = 16,
        Restore = 17,
        FontTable = 18,
        ColorTable = 19,
        PenTable = 20,
        DrawImageXY = 21,
        DrawImage = 22,
        SetPageUnit = 23,
        TranslateTransform = 24,
        RotateTransform = 25,
        ScaleTransform = 26,
        SetClip = 27,
        ResetClip = 28,
        ImageTable = 29,
        BrushTable = 30,
        DrawLines = 31,
        UpdateClearRectangle = 32,
        FillPolygon = 33,
        FillPie = 34,
        DrawPie = 35,
        DrawPath = 36,
        FillPath = 37,
        DrawImageXYStdImageIndex = 38,
        DrawImageStdImageIndex = 39,
        ClearRect = 40,
        SetImageSmoothing = 41,
        SetTextBaseline = 42,
        FillRoundRectangle = 43,
        FillMatrix = 44,
        FillRectangleFloat = 45
    }
    internal class CanvasGraphics : MarshalByRefObject, IDisposable, IDeviceContext
    {

        public CanvasGraphics(Graphics g)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }
            this._BinaryData = new MyBinaryBuilder();
            this._Owner = g;
        }
        private Graphics _Owner = null;
        //public Graphics(char[] buffer )
        //{
        //    this._String = new MyStringBuilder();
        //}
        //public char[] GetInnerDataBuffer()
        //{
        //    return this._String?.GetNativeBuffer();
        //}
        private float _AbsoluteOffsetX;
        private float _AbsoluteOffsetY;
        /// <summary>
        /// 设置绝对化的坐标偏移量
        /// </summary>
        /// <param name="x">X偏移量</param>
        /// <param name="y">Y偏移量</param>
        internal void SetAbsoluteOffset(float x, float y)
        {
            this._AbsoluteOffsetX = x;
            this._AbsoluteOffsetY = y;
        }
        private float _ZoomRate = 1f;
        /// <summary>
        /// 缩放比率
        /// </summary>
        internal float ZoomRate
        {
            get
            {
                return this._ZoomRate;
            }
            set
            {
                this._ZoomRate = value;
                this._FontTable.ZoomRate = value;
            }
        }
        internal RectangleF _ClipBounds = RectangleF.Empty;
        public RectangleF ClipBounds
        {
            get
            {
                CheckDispose();
                return this._ClipBounds;
            }
        }

        public const float _DpiX = 96f;
        public const float _DpiY = 96f;

        public float DpiX
        {
            get
            {
                return _DpiX;
            }
        }
        public float DpiY
        {
            get
            {
                return _DpiY;
            }
        }

        private InterpolationMode _InterpolationMode = InterpolationMode.High;
        public InterpolationMode InterpolationMode
        {
            get
            {
                CheckDispose();
                return this._InterpolationMode;
            }
            set
            {
                CheckDispose();
                this._InterpolationMode = value;
            }
        }
        //public bool IsClipEmpty
        //{
        //    get
        //    {
        //        return this._ClipBounds.IsEmpty;
        //    }
        //}

        //public bool IsVisibleClipEmpty
        //{
        //    get
        //    {
        //        bool result;
        //        SafeNativeMethods.Gdip.CheckStatus(SafeNativeMethods.Gdip.GdipIsVisibleClipEmpty(new HandleRef(this, NativeGraphics), out result));
        //        return result;
        //    }
        //}
        //private float _PageScale = 1;
        //public float PageScale
        //{
        //    get
        //    {
        //        CheckDispose();
        //        return this._PageScale;
        //    }
        //    set
        //    {
        //        if (value <= 0f || value > 1E+09f)
        //        {
        //            throw new ArgumentException("GdiplusInvalidParameter");
        //        }
        //        CheckDispose();
        //        this._PageScale = value;
        //    }
        //}
        private GraphicsUnit _PageUnit = GraphicsUnit.Pixel;
        public GraphicsUnit PageUnit
        {
            get
            {
                return this._PageUnit;
            }
            set
            {
                if (this._PageUnit != value)
                {
                    CheckDispose();
                    this._PageUnit = value;
                    this._BinaryData.AppendRecordType(GraphicsRecordType.SetPageUnit);
                    this._BinaryData.AppendByte((byte)value);
                    /*  World : 0, Display : 1, Pixel : 2, Point : 3, Inch : 4, Document : 5, Millimeter : 6, */
                    switch (value)
                    {
                        case GraphicsUnit.World: this._PageUnitRate = 1; break;
                        case GraphicsUnit.Display: this._PageUnitRate = 1; break;
                        case GraphicsUnit.Pixel: this._PageUnitRate = 1; break;
                        case GraphicsUnit.Point: this._PageUnitRate = 1.3333333333f; break;
                        case GraphicsUnit.Inch: this._PageUnitRate = 96; break;
                        case GraphicsUnit.Document: this._PageUnitRate = 0.32f; break;
                        case GraphicsUnit.Millimeter: this._PageUnitRate = 3.77952744641642f; break;
                        default: throw new Exception("不支持的单位:" + value);
                    }
                }
            }
        }

        private float _PageUnitRate = 1;

        private PixelOffsetMode _PixelOffsetMode = Drawing2D.PixelOffsetMode.Default;
        public PixelOffsetMode PixelOffsetMode
        {
            get
            {
                CheckDispose();
                return this._PixelOffsetMode;
            }
            set
            {
                CheckDispose();
                this._PixelOffsetMode = value;
            }
        }

        //public Point RenderingOrigin
        //{
        //    get
        //    {
        //        int x;
        //        int y;
        //        SafeNativeMethods.Gdip.CheckStatus(SafeNativeMethods.Gdip.GdipGetRenderingOrigin(new HandleRef(this, NativeGraphics), out x, out y));
        //        return new Point(x, y);
        //    }
        //    set
        //    {
        //        SafeNativeMethods.Gdip.CheckStatus(SafeNativeMethods.Gdip.GdipSetRenderingOrigin(new HandleRef(this, NativeGraphics), value.X, value.Y));
        //    }
        //}

        private SmoothingMode _SmoothingMode = SmoothingMode.HighQuality;
        public SmoothingMode SmoothingMode
        {
            get
            {
                CheckDispose();
                return this._SmoothingMode;
            }
            set
            {
                CheckDispose();
                this._SmoothingMode = value;
            }
        }
        //private int _TextContrast = 0;
        //public int TextContrast
        //{
        //    get
        //    {
        //        CheckDispose();
        //        return this._TextContrast;
        //    }
        //    set
        //    {
        //        CheckDispose();
        //        this._TextContrast = value;
        //    }
        //}

        private TextRenderingHint _TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        public TextRenderingHint TextRenderingHint
        {
            get
            {
                CheckDispose();
                return this._TextRenderingHint;
            }
            set
            {
                CheckDispose();
                this._TextRenderingHint = value;
            }
        }

        private Matrix _Transform = new Matrix();
        public Matrix Transform
        {
            get
            {
                CheckDispose();
                return this._Transform;
            }
            set
            {
                CheckDispose();
                if (value == null)
                {
                    if (this._Transform.IsIdentity == false)
                    {
                        this._Transform.Reset();
                        this.WriteTransformData();
                    }
                }
                else
                {
                    if (this._Transform.EqualsValue(value) == false)
                    {
                        this._Transform = value;
                        this.WriteTransformData();
                    }
                }
            }
        }

        private void WriteTransformData()
        {
            this._BinaryData.AppendRecordType(GraphicsRecordType.SetTransform);
            var transform = this._Transform;
            //var es = this._Transform.Elements;
            var dx = transform.OffsetX * this._PageUnitRate * this._ZoomRate;
            var dy = transform.OffsetY * this._PageUnitRate * this._ZoomRate;
            var es = transform.Elements;
            this._BinaryData.AppendSingle(es[0]);
            this._BinaryData.AppendSingle(es[1]);
            this._BinaryData.AppendSingle(es[2]);
            this._BinaryData.AppendSingle(es[3]);
            this._BinaryData.AppendSingle(dx);
            this._BinaryData.AppendSingle(dy);

        }
        public void ApplyTransformData()
        {
            this._BinaryData.AppendRecordType(GraphicsRecordType.SetTransform);
            var transform = this._Transform;
            var es = transform.Elements;
            this._BinaryData.AppendSingle(es[0]);
            this._BinaryData.AppendSingle(es[1]);
            this._BinaryData.AppendSingle(es[2]);
            this._BinaryData.AppendSingle(es[3]);
            this._BinaryData.AppendSingle(this._AbsoluteOffsetX * (1 - es[0]) * this._ZoomRate + es[4] * this._PageUnitRate * this._ZoomRate);
            this._BinaryData.AppendSingle(this._AbsoluteOffsetY * (1 - es[3]) * this._ZoomRate + es[5] * this._PageUnitRate * this._ZoomRate);
        }
        public void ResetClip()
        {
            this._ClipBounds = RectangleF.Empty;
            this._BinaryData.AppendRecordType(GraphicsRecordType.ResetClip);
            //Console.WriteLine("ResetClip");
        }
        public void TranslateClip(float x, float y)
        {
            throw new NotSupportedException();
        }
        public Rectangle VisibleClipBounds
        {
            get { throw new NotSupportedException(); }
        }
        public void SetClip(Region region)
        {
            throw new NotSupportedException();
        }
        public void SetClip(Region region, CombineMode mode)
        {
            throw new NotSupportedException();
        }
        public void SetClip(RectangleF rect)
        {
            this._ClipBounds = rect;
            this._BinaryData.AppendRecordType(GraphicsRecordType.SetClip);
            this.AppendRectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }
        public void ResetTransform()
        {
            this.Transform = null;
        }
        public void TranslateTransform(float x, float y)
        {
            this._Transform.Translate(x, y);
            this.WriteTransformData();
        }
        public void TranslateTransform(float x, float y, MatrixOrder order)
        {
            this._Transform.Translate(x, y, order);
            this.WriteTransformData();
        }
        public void ScaleTransform(float sx, float sy)
        {
            if (sx != 1 || sy != 1)
            {
                this._Transform.Scale(sx, sy);
                if (sx != 1 || sy != 1)
                {
                    this.ApplyTransformData();
                }
                else
                {
                    this.WriteTransformData();
                }
            }
        }
        /// <summary>
        /// 旋转指定的角度，单位是角度
        /// </summary>
        /// <param name="angle">角度值</param>
        public void RotateTransform(float angle)
        {
            this._BinaryData.AppendRecordType(GraphicsRecordType.RotateTransform);
            this._BinaryData.AppendSingle((float)(angle * Math.PI / 180));
            this._Transform.Rotate(angle);
        }

        #region DrawRectangle
        public void DrawRectangle(Pen pen, Rectangle rect)
        {
            DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void DrawRectangle(Pen pen, float x, float y, float width, float height, float radius = 0)
        {
            if (pen == null)
            {
                throw new ArgumentNullException("pen");
            }
            if (this._ClipBounds.IsEmpty == false)
            {
                if (x + width < this._ClipBounds.Left
                    || x > this._ClipBounds.Right
                    || y + height < this._ClipBounds.Top
                    || y > this._ClipBounds.Bottom)
                {
                    // 完全在裁剪区域外，不进行绘制
                    return;
                }
            }
            var strIndex = this._PenTable.GetIndex(pen);
            if (radius > 0)
            {
                this._BinaryData.AppendRecordType(GraphicsRecordType.DrawRoundRectangle);
                this._BinaryData.AppendInt16(strIndex);
                this._BinaryData.AppendInt16((short)(radius * this._PageUnitRate));
                this.AppendRectangle(x, y, width, height);
            }
            else
            {
                this._BinaryData.AppendRecordType(GraphicsRecordType.DrawRectangle);
                this._BinaryData.AppendInt16(strIndex);
                this.AppendRectangle(x, y, width, height);
            }
        }
        public void DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            DrawRectangle(pen, (float)x, (float)y, (float)width, (float)height);
        }
        #endregion
        #region DrawEllipse
        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            if (pen == null)
            {
                throw new ArgumentNullException("pen");
            }
            var strIndex = this._PenTable.GetIndex(pen);
            this._BinaryData.AppendRecordType(GraphicsRecordType.DrawEllipse);
            this._BinaryData.AppendInt16(strIndex);// this._String.Append(strIndex);
            this.AppendRectangle(x, y, width, height);
        }

        #endregion

        #region DrawPie

        public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            if (pen == null)
            {
                throw new ArgumentNullException("pen");
            }
            var pi = this._PenTable.GetIndex(pen);
            this._BinaryData.AppendRecordType(GraphicsRecordType.DrawPie);
            this._BinaryData.AppendInt16(pi);
            this.AppendRectangle(x, y, width, height);
            this._BinaryData.AppendSingle(ConvertToCanvasAngle(startAngle, width, height));
            this._BinaryData.AppendSingle(ConvertToCanvasAngle(startAngle + sweepAngle, width, height));
        }
        public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        {
            DrawPie(pen, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle);
        }

        #endregion
        public void DrawPolygon(Pen pen, PointF[] points)
        {
            if (pen == null)
            {
                throw new ArgumentNullException("pen");
            }
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
        }
        public void DrawPath(Pen pen, GraphicsPath path)
        {
            if (pen == null)
            {
                throw new ArgumentNullException("pen");
            }
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            this._BinaryData.AppendRecordType(GraphicsRecordType.DrawPath);
            var pindex = this._PenTable.GetIndex(pen);
            this._BinaryData.AppendInt16(pindex);
            this._BinaryData.AppendSingle(this._ZoomRate * this._PageUnitRate);
            this._BinaryData.AppendSingle(this._AbsoluteOffsetX * this._ZoomRate);
            this._BinaryData.AppendSingle(this._AbsoluteOffsetY * this._ZoomRate);
            this._BinaryData.AppendString(path.ToCanvasString());
        }


        public void Clear(Color color)
        {
        }

        #region FillRectangle

        //public void FillRectangle(Brush brush, RectangleF rect)
        //{
        //    FillRectangle(
        //        brush, 
        //        (float)rect.X, 
        //        (float)rect.Y,
        //        (float)rect.Width,
        //        (float)rect.Height);
        //}
        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }

            var vIndex = this._BrushTable.GetIndex(brush);
            this._BinaryData.AppendRecordType(GraphicsRecordType.FillRectangle);
            this._BinaryData.AppendInt16(vIndex);// this._String.Append(strIndex);
            this.AppendRectangle(x, y, width, height);
        }

        public void FillRectangle(Brush brush, Rectangle rect)
        {
            FillRectangle(brush, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }

        #endregion

        public void FillPolygon(Brush brush, PointF[] points)
        {
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            if (points.Length > 0)
            {
                var intIndex = this._BrushTable.GetIndex(brush);
                this._BinaryData.AppendRecordType(GraphicsRecordType.FillPolygon);
                this._BinaryData.AppendInt16(intIndex);
                this.AppendPoints(points);
            }
        }
        public void FillPolygon(Brush brush, Point[] points)
        {
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            if (points.Length > 0)
            {
                var intIndex = this._BrushTable.GetIndex(brush);
                this._BinaryData.AppendRecordType(GraphicsRecordType.FillPolygon);
                this._BinaryData.AppendInt16(intIndex);
                this.AppendPoints(points);
            }
        }

        #region FillEllipse
        public void FillEllipse(Brush brush, RectangleF rect)
        {
            FillEllipse(brush, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }
        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            CheckDispose();
            var strIndex = this._BrushTable.GetIndex(brush);
            this._BinaryData.AppendRecordType(GraphicsRecordType.FillEllipse);
            this._BinaryData.AppendInt16(strIndex);
            AppendRectangle(x, y, width, height);
        }


        #endregion
        public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            this._BinaryData.AppendRecordType(GraphicsRecordType.FillPie);
            var bi = this._BrushTable.GetIndex(brush);
            this._BinaryData.AppendInt16(bi);
            AppendRectangle(x, y, width, height);
            this._BinaryData.AppendSingle(ConvertToCanvasAngle(startAngle, width, height));
            this._BinaryData.AppendSingle(ConvertToCanvasAngle(startAngle + sweepAngle, width, height));
        }
        private static readonly float _AngleRate = (float)(Math.PI / 180);
        /// <summary>
        /// 将角度转换为CANVAS绘图用的弧度值
        /// </summary>
        /// <param name="angle">角度值</param>
        /// <param name="width">椭圆宽度</param>
        /// <param name="height">椭圆高度</param>
        /// <returns>CANVAS弧度</returns>
        internal static float ConvertToCanvasAngle(float angle, float width, float height)
        {
            if (width == height || ((angle % 90) == 0))
            {
                return angle * _AngleRate;
            }
            else
            {
                //var index = (int)Math.Floor(angle / 90.0) % 4;
                //if (index < 0)
                //{
                //    index += 4;
                //}
                //var oldAngle = angle;
                //return (float)(angle * _AngleRate);
                angle = angle * _AngleRate;
                var tan = Math.Tan(angle);
                var tan2 = tan * width / height;
                var angle2 = Math.Atan(tan2);
                var dis2 = angle2 - Math.Atan(tan);

                var result = angle + dis2;// index == 0 || index == 2 ? angle + dis2 : angle - dis2 ;
                //var newAngle3 = result / _AngleRate;
                return (float)result;

                //var newAngle = angle2 / _AngleRate;
                //if( angle > 0 && angle2 < 0 )
                //{
                //    angle2 += Math.PI;
                //}
                //else if( angle < 0 && angle2 > 0 )
                //{
                //    angle2 -= Math.PI;
                //}
                //var dis = angle - angle2;
                //if(Math.Abs( dis ) > 0.3)
                //{
                //    dis = 0;
                //}
                //return (float)angle2;// (float)(angle2 * _AngleRate);
            }
        }
        #region DrawString
        //public void DrawString(string s, Font font, Brush brush, float x, float y)
        //{
        //    DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), null);
        //}
        //public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
        //{
        //    DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), format);
        //}

        public void DrawString(
            string txt,
            Font font,
            Brush brush,
            float layoutRectangleLeft,
            float layoutRectangleWidth,
            float layoutRectangleHeight,
            float layoutRectangleTop,
            StringFormat format)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return;
            }
            var isVertical = format != null && (format.FormatFlags & StringFormatFlags.DirectionVertical) == StringFormatFlags.DirectionVertical;
            //if (txt.Length > 10 && ((SolidBrush)brush).ColorARGB == Color.Red.ToArgb())
            //{
            //    return;
            //}
            if( txt.IndexOf("孙思邈") >=0)
            {
                var s = 1;
            }
            SetCurrentBrush(brush);
            SetCurrentFont(font);
            var temp1 = layoutRectangleWidth;
            var temp2 = layoutRectangleHeight;
            var temp3 = layoutRectangleTop;
            layoutRectangleTop = temp1;
            layoutRectangleWidth = temp2;
            layoutRectangleHeight = temp3;
            var fm = font.FontFamily;
            var topFixRate = fm == null ? 0 : fm.FixRateForCanvasDraw;
            if (layoutRectangleWidth == 0
                && layoutRectangleHeight == 0
                && txt.Length == 1
                && isVertical == false)
            {
                // 快速绘制单个字符
                _BinaryData.AppendRecordType(GraphicsRecordType.DrawChar);
                this._BinaryData.AppendInt32((int)txt[0]);
                var fh = font.GetHeight(this._Owner);
                AppendPoint(layoutRectangleLeft, layoutRectangleTop + fh * topFixRate);
                if (font.Strikeout)
                {
                    // 删除线
                    var fw = this.MeasureString(txt, font).Width;
                    var linetop = layoutRectangleTop + fh * topFixRate;
                    this.DrawLine(
                        new Pen(brush, 1),
                        layoutRectangleLeft,
                        linetop,
                        layoutRectangleLeft + fw,
                        linetop);
                    //var fw = DCSoft.Writer.Dom.CharacterMeasurer.MeasureStringUseTrueTypeFont
                }
            }
            else
            {
                if (txt.Length == 1 && isVertical == false)
                {
                    // 只显示一个字符
                    if (format == null
                        || (format.Alignment == StringAlignment.Near
                        && format.LineAlignment == StringAlignment.Near))
                    {
                        _BinaryData.AppendRecordType(GraphicsRecordType.DrawChar);
                        this._BinaryData.AppendInt32((int)txt[0]);
                        var fh = font.GetHeight(this._Owner);
                        AppendPoint(
                            layoutRectangleLeft,
                            layoutRectangleTop + fh * topFixRate);
                        if (font.Strikeout)
                        {
                            // 删除线
                            var fw = this.MeasureString(txt, font).Width;
                            var linetop = layoutRectangleTop + fh / 2 + fh * 0.3f;
                            this.DrawLine(
                                new Pen(brush, 1),
                                layoutRectangleLeft,
                                linetop,
                                layoutRectangleLeft + fw,
                                linetop);
                        }
                        return;
                    }
                }
                SizeF layoutSize;
                string[] strLines;
                if (txt.Length == 2
                     && DCTextUtils.IsHighSurrogate(txt[0])
                     && DCTextUtils.IsLowSurrogate(txt[1]))
                {
                    // 绘制代理字符
                    strLines = new string[] { txt };
                }
                else
                {
                    strLines = CharacterMeasurer.StaticSplitToLines(
                        this.PageUnit,
                        txt,
                        font.Name,
                        font.Size,
                        font.Style,
                        layoutRectangleWidth,
                        format,
                        out layoutSize,
                        null);
                    //strLines = DCSoft.Drawing.DrawerUtil.StaticSplitToLines(this, txt, font, layoutRectangle.Width, format, out layoutSize);
                    if (strLines == null || strLines.Length == 0)
                    {
                        return;
                    }
                    //if( strLines.Length == 2 && strLines[1].Length == 0)
                    //{
                    //    strLines = CharacterMeasurer.StaticSplitToLines(
                    //    this.PageUnit,
                    //    txt,
                    //    font.Name,
                    //    font.Size,
                    //    font.Style,
                    //    layoutRectangle.Width,
                    //    format,
                    //    out layoutSize,
                    //    null);
                    //}
                }
                var strLinesLength = strLines.Length;
                var fontHeight = font.GetHeight(this._Owner);
                var oldLayoutTop = layoutRectangleTop;
                layoutRectangleTop += fontHeight * topFixRate;
                //if (layoutRectangleHeight > 0)
                //{
                //    layoutRectangleTop += fontHeight * _CharTopFixHeightRate;
                //    //layoutRectangle.Offset(0, fontHeight * _CharTopFixHeightRate);
                //}
                //else
                //{
                //    layoutRectangleTop += fontHeight * 0.26f;
                //    //layoutRectangle.Offset(0, fontHeight * 0.26f);
                //}

                var layoutTop = layoutRectangleTop;
                if (layoutRectangleHeight > 0)
                {
                    if (format.LineAlignment == StringAlignment.Center)
                    {
                        layoutTop = layoutRectangleTop + (layoutRectangleHeight - fontHeight * strLinesLength) / 2;
                    }
                    else if (format.LineAlignment == StringAlignment.Far)
                    {
                        layoutTop = layoutRectangleTop + layoutRectangleHeight - fontHeight * strLinesLength;
                    }
                }
                bool needClip = false;
                //var layoutRectangleWidth = layoutRectangleWidth;
                float maxLineWidth = 0;
                if (layoutRectangleWidth > 0)
                {
                    for (var lineIndex = 0; lineIndex < strLinesLength; lineIndex++)
                    {
                        var strLine = strLines[lineIndex];
                        var lineWidth = CharacterMeasurer.MeasureSingleLineStringUseTrueTypeFont(this._PageUnit, strLine, font).Width;
                        if (lineWidth > maxLineWidth)
                        {
                            maxLineWidth = lineWidth;
                        }
                    }
                    if (layoutRectangleHeight > 0)
                    {
                        if ((maxLineWidth > layoutRectangleWidth)
                            || (fontHeight * strLinesLength > layoutRectangleHeight))
                        {
                            needClip = true;
                        }
                    }
                }
                if (needClip)
                {
                    var rect2 = new RectangleF(
                        layoutRectangleLeft,
                        oldLayoutTop,
                        layoutRectangleWidth,
                        layoutRectangleHeight);
                    //this.DrawRectangle(Pens.Red, rect2);
                    this.SetClip(rect2);
                }
                for (var lineIndex = 0; lineIndex < strLinesLength; lineIndex++)
                {
                    var strLine = strLines[lineIndex];
                    var layoutLeft = layoutRectangleLeft;
                    float lineWidth = 0;
                    if (strLinesLength == 1 && maxLineWidth > 0)
                    {
                        lineWidth = maxLineWidth;
                    }
                    else
                    {
                        lineWidth = this.MeasureString(strLine, font).Width;
                    }
                    if (layoutRectangleWidth > 0)
                    {
                        if (format.Alignment == StringAlignment.Center)
                        {
                            layoutLeft = /*6.25f +*/ layoutRectangleLeft + (layoutRectangleWidth - lineWidth) / 2;
                        }
                        else if (format.Alignment == StringAlignment.Far)
                        {
                            layoutLeft = layoutRectangleLeft + layoutRectangleWidth - lineWidth;
                        }
                    }
                    //if( strLine == null || strLine.Length == 0 )
                    //{
                    //    var ss = 1;
                    //}
                    CanvasGraphicsState objSaved = null;
                    try
                    {
                        if (isVertical)
                        {
                            objSaved = (CanvasGraphicsState)this.Save();
                            this.ResetClip();
                            this.TranslateTransform(
                                layoutLeft+ fontHeight*0.5f ,
                                layoutTop - fontHeight * topFixRate,
                                MatrixOrder.Prepend);
                            this.RotateTransform(90);
                        }
                        this._BinaryData.AppendRecordType(GraphicsRecordType.DrawString);
                        this._BinaryData.AppendString(strLine);
                        if (isVertical)
                        {
                            AppendPoint(0, 0);
                        }
                        else
                        {
                            AppendPoint(layoutLeft, layoutTop);
                        }
                        if (layoutRectangleWidth > 0)
                        {
                            var disW = layoutRectangleWidth * this._PageUnitRate * this._ZoomRate;
                            if( layoutRectangleWidth < lineWidth )
                            {
                                disW = -disW;
                            }
                            this._BinaryData.AppendInt16((short)disW);
                        }
                        else
                        {
                            this._BinaryData.AppendInt16((short)10000);
                        }
                        if (font.Underline)
                        {
                            var lineTop = layoutTop + fontHeight * (1 - topFixRate);
                            this.DrawLine(
                                new Pen(brush, 1),
                                layoutLeft,
                                lineTop,
                                layoutLeft + lineWidth,
                                lineTop);
                        }
                        if (font.Strikeout)
                        {
                            // 删除线
                            var lineTop = layoutTop + fontHeight * (0.5f - topFixRate);
                            this.DrawLine(
                                new Pen(brush, 1),
                                layoutLeft,
                                lineTop,
                                layoutLeft + lineWidth,
                                lineTop);
                        }
                    }
                    finally
                    {
                        if (objSaved != null)
                        {
                            this.Restore(objSaved);
                            objSaved = null;
                        }
                    }
                    layoutTop += fontHeight;
                }//for

                if (needClip)
                {
                    this.ResetClip();
                }
            }
        }

        private Brush _CurrentBrush = null;
        private void SetCurrentBrush(Brush b)
        {
            if (b == null)
            {
                this._CurrentBrush = null;
                var index = this._BrushTable.GetIndex(Color.Black);
                _BinaryData.AppendRecordType(GraphicsRecordType.SetCurrentBrush);
                this._BinaryData.AppendInt16(index);// this._String.Append(this._BrushTable.GetIndex(Color.Black));
            }
            else if (this._CurrentBrush == null || this._CurrentBrush.EqualsValue(b) == false)
            {
                var strIndex = this._BrushTable.GetIndex(b);
                this._CurrentBrush = b;
                _BinaryData.AppendRecordType(GraphicsRecordType.SetCurrentBrush);
                this._BinaryData.AppendInt16(strIndex);// this._String.Append(strIndex);
            }
        }


        private const float _CharTopFixHeightRate = 0.74f;

        #endregion


        #region MeasureString()
        public SizeF MeasureString(string text, Font font, SizeF size)
        {
            return MeasureString(text, font, (int)size.Width, null);
        }
        public SizeF MeasureString(string text, Font font, float vWidth)
        {
            return MeasureString(text, font, (int)vWidth, null);
        }
        public SizeF MeasureString(string text, Font font)
        {
            var result = CharacterMeasurer.MeasureStringUseTrueTypeFont(
                this._PageUnit,
                text,
                font,
                100000,
                null);
            //if (this._ZoomRate != 1)
            //{
            //    result.Width = result.Width * this._ZoomRate;
            //    result.Height = result.Height * this._ZoomRate;
            //}
            return result;
        }
        public SizeF MeasureString(string text, Font font, SizeF size, StringFormat format)
        {
            return MeasureString(text, font, (int)size.Width, format);
        }
        public SizeF MeasureString(string text, Font font, int width, StringFormat format)
        {
            if (string.IsNullOrEmpty(text))
            {
                return SizeF.Empty;
            }
            if (text.Length == 1)
            {
                return CharacterMeasurer.MeasureCharUseTrueTypeFont(this._PageUnit, text[0], font);
            }
            SizeF layoutSize;
            var strLines = CharacterMeasurer.StaticSplitToLines(
                this.PageUnit,
                text,
                font.Name,
                font.Size,
                font.Style,
                width,
                format,
                out layoutSize,
                null);
            //var strLines = DCSoft.Drawing.DrawerUtil.StaticSplitToLines(this, text, font, width, format, out layoutSize);
            if (layoutSize.IsEmpty)
            {
                layoutSize = CharacterMeasurer.MeasureSingleLineStringUseTrueTypeFont(
                    this._PageUnit,
                    text,
                    font);
            }
            return layoutSize;
        }

        #endregion


        #region DrawImage()

        public void SetImageSmoothing(bool v)
        {
            this._BinaryData.AppendRecordType(GraphicsRecordType.SetImageSmoothing);
            this._BinaryData.AppendBoolean(v);
        }
        public void DrawImage(Image img, Rectangle rect)
        {
            DrawImage(img, rect.Left, rect.Top, rect.Width, rect.Height);
        }
        public void DrawImage(Image image, float x, float y)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            var bs = image.ToBinary();
            if (bs == null || bs.Length == 0)
            {
                return;
            }

            // 直接绘制图片数据
            var index = this._ImageTable.GetIndex(bs);
            if (image.Width == 0 || image.Height == 0)
            {
                this._BinaryData.AppendRecordType(GraphicsRecordType.DrawImageXY);
                this._BinaryData.AppendInt16(index);// this._String.Append(this._ImageTable.GetIndex(bs));
                AppendPoint(x, y);
            }
            else
            {
                this._BinaryData.AppendRecordType(GraphicsRecordType.DrawImage);
                this._BinaryData.AppendInt16(index);// this._String.Append(this._ImageTable.GetIndexString(bs));
                                                    //this.AppendRectangle(x, y, image.Width / this._PageUnitRate, image.Height / this._PageUnitRate);
                AppendPoint(x, y);
                this._BinaryData.Append2Int32((int)(this._ZoomRate * image.Width), (int)(this._ZoomRate * image.Height)); // AppendFloat(this._ZoomRate * height * this._PageUnitRate);
            }
        }
        public void DrawImage(Image image, float x, float y, float width, float height)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            var bs = image.ToBinaryForCanvasGraphics();
            if (bs == null || bs.Length == 0)
            {
                return;
            }
            var index = this._ImageTable.GetIndex(bs);
            this._BinaryData.AppendRecordType(GraphicsRecordType.DrawImage);
            this._BinaryData.AppendInt16(index);// this._String.Append(this._ImageTable.GetIndexString(bs));
            this.AppendRectangle(x, y, width, height);
        }
        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            DrawImage(image, (float)x, (float)y, (float)width, (float)height);
        }
        public void DrawImageUnscaled(Image image, int x, int y)
        {
            DrawImage(image, (float)x, (float)y);
        }
        public void DrawImageUnscaled(Image image, Rectangle rect)
        {
            DrawImage(image, (float)rect.X, (float)rect.Y);
        }
        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            if (destRect.Width == 0)
            {

            }
            //if(image.ToBinary() == null )
            //{
            //    return;
            //}
            var bs = image.ToBinary();
            if (bs == null || bs.Length == 0)
            {
                return;
            }
            var index = this._ImageTable.GetIndex(bs);
            _BinaryData.AppendRecordType(GraphicsRecordType.DrawImageExt);
            this._BinaryData.AppendInt16(index);// this._String.Append(this._ImageTable.GetIndexString(bs));
            this._BinaryData.AppendInt16((short)srcRect.Left);
            this._BinaryData.AppendInt16((short)srcRect.Top);
            this._BinaryData.AppendInt16((short)srcRect.Width);
            this._BinaryData.AppendInt16((short)srcRect.Height);
            AppendRectangle(destRect);
        }

        #endregion
        public void DrawLine(Pen pen, Point p1, Point p2)
        {
            DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
        }
        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="pen">画笔对象</param>
        /// <param name="x1">起点X坐标</param>
        /// <param name="y1">起点Y坐标</param>
        /// <param name="x2">终点X坐标</param>
        /// <param name="y2">终点Y坐标</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            if (pen == null)
            {
                throw new ArgumentNullException("pen");
            }
            var intIndex = this._PenTable.GetIndex(pen);
            this._BinaryData.AppendRecordType(GraphicsRecordType.DrawLine);
            this._BinaryData.AppendInt16(intIndex);
            this.AppendPoint(x1, y1);
            this.AppendPoint(x2, y2);
        }
        private void AppendPoint(float x, float y)
        {
            if (this._ZoomRate == 1)
            {
                this._BinaryData.Append2Int32(
                    (int)Math.Round(this._AbsoluteOffsetX + x * this._PageUnitRate),
                    (int)Math.Round(this._AbsoluteOffsetY + y * this._PageUnitRate));
            }
            else
            {
                this._BinaryData.Append2Int32(
                    (int)Math.Round(this._ZoomRate * (this._AbsoluteOffsetX
                    + x * this._PageUnitRate)),
                    (int)Math.Round(this._ZoomRate * (this._AbsoluteOffsetY
                    + y * this._PageUnitRate)));
            }
        }
        public void DrawLines(Pen pen, PointF[] points)
        {
            if (pen == null)
            {
                throw new ArgumentNullException("pen");
            }
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            if (points.Length > 0)
            {
                var intIndex = this._PenTable.GetIndex(pen);
                this._BinaryData.AppendRecordType(GraphicsRecordType.DrawLines);
                this._BinaryData.AppendInt16(intIndex);
                this.AppendPoints(points);
            }
        }
        public void DrawLines(Pen pen, Point[] points)
        {
            if (pen == null)
            {
                throw new ArgumentNullException("pen");
            }
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            if (points.Length > 0)
            {
                var intIndex = this._PenTable.GetIndex(pen);
                this._BinaryData.AppendRecordType(GraphicsRecordType.DrawLines);
                this._BinaryData.AppendInt16(intIndex);
                this.AppendPoints(points);
            }
        }
        public void FillRegion(Brush b, Region regioni)
        {

        }
        public void FillPath(Brush brush, GraphicsPath path)
        {
            //return;
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            var bindex = this._BrushTable.GetIndex(brush);
            this._BinaryData.AppendRecordType(GraphicsRecordType.FillPath);
            this._BinaryData.AppendInt16(bindex);
            this._BinaryData.AppendSingle(this._ZoomRate * this._PageUnitRate);
            this._BinaryData.AppendSingle(this._AbsoluteOffsetX * this._ZoomRate);
            this._BinaryData.AppendSingle(this._AbsoluteOffsetY * this._ZoomRate);
            this._BinaryData.AppendString(path.ToCanvasString());

        }

        public void Restore(GraphicsState gstate)
        {
            if (gstate == null)
            {
                throw new ArgumentNullException("gstate");
            }
            CheckDispose();
            ((CanvasGraphicsState)gstate).Restore(this);
            //this._String.AppendRecordType(GraphicsRecordType.Restore);
        }
        public GraphicsState Save()
        {
            //this._String.AppendRecordType(GraphicsRecordType.Save);
            return new CanvasGraphicsState(this);
        }

        private bool _Disposed;
        public void Dispose()
        {
            //if (this._ParentImage is Bitmap)
            //{
            //    var writer = new MyBinaryBuilder();
            //    writer.AppendByte(Bitmap.GraphicsImageHeader);
            //    writer.AppendInt16((short)this._ParentImage.Width);
            //    writer.AppendInt16((short)this._ParentImage.Height);
            //    var bs = this.FinallyWriteTo(writer);
            //    writer.Dispose();
            //    ((Bitmap)this._ParentImage).DataFromGraphics = bs;
            //}
            this._Disposed = true;
            if (this._ColorTable != null)
            {
                this._ColorTable.Clear();
                this._ColorTable = null;
            }
            if (this._FontTable != null)
            {
                this._FontTable.Clear();
                this._FontTable = null;
            }
            if (this._PenTable != null)
            {
                this._PenTable.Clear();
                this._PenTable = null;
            }
            if (this._ImageTable != null)
            {
                this._ImageTable.Clear();
                this._ImageTable = null;
            }
            if (this._BinaryData != null)
            {
                this._BinaryData.Dispose();
                this._BinaryData = null;
            }
        }
        protected void CheckDispose()
        {
            if (this._Disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        public override string ToString()
        {
            throw new Exception("不支持 ");
        }
        public byte[] ToByteArray()
        {
            var str = new MyBinaryBuilder(MyBinaryBuilder.GlobalBufferForPackage);
            var bs = this.FinallyWriteTo(str);
            str.Dispose();
            return bs;
        }
        internal byte[] FinallyWriteTo(MyBinaryBuilder strResult)
        {
            if (strResult == null)
            {
                throw new ArgumentNullException(nameof(strResult));
            }
#if OutputGraphicsRecordTypeName
            strResult.Append('"' + GraphicsRecordType.CheckVersion.ToString() + '"');
#else
            strResult.AppendRecordType(GraphicsRecordType.CheckVersion);
#endif
            strResult.AppendInt32(FormatVersion);
            if (this._ColorTable != null)
            {
                this._ColorTable.WriteTo(strResult, this);
            }
            if (this._FontTable != null)
            {
                this._FontTable.WriteTo(strResult, this);
            }
            if (this._PenTable != null)
            {
                this._PenTable.ZoomRate = GraphicsUnitConvert.Convert(this.ZoomRate, this.PageUnit, GraphicsUnit.Pixel);
                this._PenTable.WriteTo(strResult, this);
            }
            if (this._BrushTable != null)
            {
                this._BrushTable.WriteTo(strResult, this);
            }
            if (this._ImageTable != null)
            {
                this._ImageTable.WriteTo(strResult, this);
            }
            strResult.AppendRecordType(GraphicsRecordType.UpdateClearRectangle);
            //strResult.AppendLine();
            var bsResult = new byte[strResult.Length + this._BinaryData.Length + 1];
            System.Buffer.BlockCopy(strResult._Buffer, 0, bsResult, 0, strResult.Length);
            System.Buffer.BlockCopy(this._BinaryData._Buffer, 0, bsResult, strResult.Length, this._BinaryData.Length);
            bsResult[bsResult.Length - 1] = (byte)GraphicsRecordType.First;
            return bsResult;

            //strResult.Capacity = strResult.Length + this._String.Length + 1;
            //strResult.AppendBinaryBuilder(this._String);
            //strResult.AppendRecordType(GraphicsRecordType.First);
            //int len = strResult.Length;

            //if (strResult.Capacity == strResult.Length)
            //{
            //    var bsResult = strResult._Buffer;
            //    strResult.Dispose();
            //    return bsResult;
            //}
            //else
            //{
            //    var bsResult2 = strResult.ToByteArray();
            //    strResult.Dispose();
            //    return bsResult2;
            //}
        }


        /// <summary>
        /// 版本号
        /// </summary>
        public const int FormatVersion = 20221031;
        private void AppendPoints(PointF[] ps)
        {
            this._BinaryData.AppendInt16((short)ps.Length);
            var len = ps.Length;
            for (int iCount = 0; iCount < len; iCount++)
            {
                this.AppendPoint(ps[iCount].X, ps[iCount].Y);
            }
        }
        private void AppendPoints(Point[] ps)
        {
            this._BinaryData.AppendInt16((short)ps.Length);
            var len = ps.Length;
            for (int iCount = 0; iCount < len; iCount++)
            {
                this.AppendPoint(ps[iCount].X, ps[iCount].Y);
            }
        }
        private void AppendRectangle(RectangleF rect)
        {
            this.AppendRectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        private void AppendRectangle(float x, float y, float width, float height)
        {
            if (this._ZoomRate == 1)
            {
                this._BinaryData.Append4Int32(
                    (int)Math.Round(this._AbsoluteOffsetX + x * this._PageUnitRate),
                    (int)Math.Round(this._AbsoluteOffsetY + y * this._PageUnitRate),
                    (int)(width * this._PageUnitRate),
                    (int)(height * this._PageUnitRate));
            }
            else
            {
                this._BinaryData.Append4Int32(
                    (int)Math.Round(this._ZoomRate * (this._AbsoluteOffsetX + x * this._PageUnitRate)),
                    (int)Math.Round(this._ZoomRate * (this._AbsoluteOffsetY + y * this._PageUnitRate)),
                    (int)(this._ZoomRate * width * this._PageUnitRate),
                    (int)(this._ZoomRate * height * this._PageUnitRate));
            }
        }

        public RectangleF GetTargetRectangle(float x, float y, float width, float height, float zoomRate)
        {
            var tx = (this._AbsoluteOffsetX + x * this._PageUnitRate);
            var ty = (this._AbsoluteOffsetY + y * this._PageUnitRate);
            var tw = width * this._PageUnitRate;
            var th = height * this._PageUnitRate;
            //this._Transform.Translate(0, 0);
            if (this._Transform.IsIdentity == false)
            {
                var p1 = this._Transform.TransformPointFPageUnitRate(tx, ty, this._PageUnitRate);
                var p2 = this._Transform.TransformPointFPageUnitRate(tx + tw, ty + th, this._PageUnitRate);
                return new RectangleF(
                    p1.X * zoomRate,
                    p1.Y * zoomRate,
                    (p2.X - p1.X) * zoomRate,
                    (p2.Y - p1.Y) * zoomRate);
            }
            else
            {
                return new RectangleF(tx * zoomRate, ty * zoomRate, tw * zoomRate, th * zoomRate);
            }
        }
        private void AppendRectangleFloat(float x, float y, float width, float height)
        {
            if (this._ZoomRate == 1)
            {
                this._BinaryData.Append4Int32(
                    BitConverter.SingleToInt32Bits(this._AbsoluteOffsetX + x * this._PageUnitRate),
                    BitConverter.SingleToInt32Bits(this._AbsoluteOffsetY + y * this._PageUnitRate),
                    BitConverter.SingleToInt32Bits(width * this._PageUnitRate),
                    BitConverter.SingleToInt32Bits(height * this._PageUnitRate)); // AppendFloat(this._ZoomRate * height * this._PageUnitRate);
            }
            else
            {
                this._BinaryData.Append4Int32(
                    BitConverter.SingleToInt32Bits(this._ZoomRate * (this._AbsoluteOffsetX + x * this._PageUnitRate)),
                    BitConverter.SingleToInt32Bits(this._ZoomRate * (this._AbsoluteOffsetY + y * this._PageUnitRate)),
                    BitConverter.SingleToInt32Bits(this._ZoomRate * width * this._PageUnitRate),
                    BitConverter.SingleToInt32Bits(this._ZoomRate * height * this._PageUnitRate)); // AppendFloat(this._ZoomRate * height * this._PageUnitRate);
            }
        }
        private int AppendRectangleForCache(float x, float y, float width, float height)
        {
            int result = this._BinaryData.Length + 8;
            if (this._ZoomRate == 1)
            {
                this._BinaryData.Append4Int32(
                    (int)(this._AbsoluteOffsetX + x * this._PageUnitRate),
                    (int)(this._AbsoluteOffsetY + y * this._PageUnitRate),
                    (int)(width * this._PageUnitRate),
                    (int)(height * this._PageUnitRate)); // AppendFloat(this._ZoomRate * height * this._PageUnitRate);
            }
            else
            {
                this._BinaryData.Append4Int32(
                    (int)(this._ZoomRate * (this._AbsoluteOffsetX + x * this._PageUnitRate)),
                    (int)(this._ZoomRate * (this._AbsoluteOffsetY + y * this._PageUnitRate)),
                    (int)(this._ZoomRate * width * this._PageUnitRate),
                    (int)(this._ZoomRate * height * this._PageUnitRate)); // AppendFloat(this._ZoomRate * height * this._PageUnitRate);
            }
            return result;
            //AppendFloat(x);
            //this._String.Append(',');
            //AppendFloat(y);
            //this._String.Append(',');
            //AppendFloat(width);
            //this._String.Append(',');
            //AppendFloat(height);
        }



        private MyBinaryBuilder _BinaryData;

        private MyImageTable _ImageTable = new MyImageTable();
        private class MyImageTable : ObjectCache<byte[]>
        {
            protected override GraphicsRecordType GetRecordType()
            {
                return GraphicsRecordType.ImageTable;
            }
            protected override void WriteItemTo(MyBinaryBuilder strResult, byte[] item, CanvasGraphics g)
            {
                var intHeaderFlag = Bitmap.StaticGetImageFormatByteHeaderFlag(item);
                strResult.AppendByte((byte)intHeaderFlag);

                //if (FileHeaderHelper.HasJpegHeader(item))
                //{
                //    strResult.AppendByte((byte)0);// strResult.Append("data:image/jpeg;base64,");
                //}
                //else if (FileHeaderHelper.HasPNGHeader(item))
                //{
                //    strResult.AppendByte((byte)1);//strResult.Append("data:image/png;base64,");
                //}
                //else if (FileHeaderHelper.HasGIFHeader(item))
                //{
                //    strResult.AppendByte((byte)2);//strResult.Append("data:image/gif;base64,");
                //}
                //else if (FileHeaderHelper.HasBMPHeader(item))
                //{
                //    strResult.AppendByte((byte)3);//strResult.Append("data:image/bmp;base64,");
                //}
                //else if (Image.IsBlobUrlData(item))
                //{
                //    strResult.AppendByte((byte)5);
                //}
                //else
                //{
                //    strResult.AppendByte((byte)4);//strResult.Append("data:image/png;base64,");
                //}
                strResult.AppendByteArray(item);
            }

            private static readonly BinaryEqualityComparer _Comparer = new BinaryEqualityComparer(true);
            public override bool Equals(byte[] item1, byte[] item2)
            {
                return _Comparer.Equals(item1, item2);
            }
            public override int GetHashCode(byte[] item)
            {
                return _Comparer.GetHashCode(item);
            }
        }
        private MyBrushTable _BrushTable = new MyBrushTable();
        private class MyBrushTable : ObjectCache<Brush>
        {
            public short GetIndex(Color fillColor)
            {
                if (base._LastKeyValue is SolidBrush
                    && ((SolidBrush)base._LastKeyValue).Color == fillColor)
                {
                    return base._LastIndex;
                }
                return base.GetIndex(new SolidBrush(fillColor));
            }

            public override short GetIndex(Brush v)
            {
                if ((v is SolidBrush) == false)
                {
                    return base.GetIndex(v);
                    //DCSoft.DCConsole.Default.WriteLine("xxxx");
                }
                if ((object)v == (object)base._LastKeyValue)
                {
                    return base._LastIndex;
                }
                if (base._LastKeyValue != null && v.EqualsValue(base._LastKeyValue))
                {
                    return base._LastIndex;
                }
                return base.GetIndex(v);
            }
            protected override GraphicsRecordType GetRecordType()
            {
                return GraphicsRecordType.BrushTable;
            }
            protected override void WriteItemTo(MyBinaryBuilder strResult, Brush item, CanvasGraphics g)
            {
                if (item is SolidBrush)
                {
                    strResult.AppendString(ColorToString(((SolidBrush)item).Color));
                }
                else if (item is System.Drawing.Drawing2D.HatchBrush hb)
                {
                    // 纹理填充
                    strResult.AppendString(hb.ToCanvasString());
                }
                else if (item is LinearGradientBrush)
                {
                    // 线性渐变色
                    var b2 = (LinearGradientBrush)item;
                    var strCode = new StringBuilder();
                    strCode.Append("*");
                    if (b2.Point1.IsEmpty == false || b2.Point2.IsEmpty == false)
                    {
                        AppendPositions(strCode, b2.Point1.X, b2.Point1.Y, b2.Point2.X, b2.Point2.Y, g);
                    }
                    else
                    {
                        switch (b2.Mode)
                        {
                            case LinearGradientMode.BackwardDiagonal:
                                AppendPositions(strCode, b2.Rectangle.Right, b2.Rectangle.Top, b2.Rectangle.Left, b2.Rectangle.Bottom, g);
                                break;
                            case LinearGradientMode.ForwardDiagonal:
                                AppendPositions(strCode, b2.Rectangle.Left, b2.Rectangle.Top, b2.Rectangle.Right, b2.Rectangle.Bottom, g);
                                break;
                            case LinearGradientMode.Horizontal:// 水平方向渐变
                                AppendPositions(strCode, b2.Rectangle.Left, b2.Rectangle.Top, b2.Rectangle.Right, b2.Rectangle.Top, g);
                                break;
                            case LinearGradientMode.Vertical:// 垂直方向渐变
                                AppendPositions(strCode, b2.Rectangle.Left, b2.Rectangle.Top, b2.Rectangle.Left, b2.Rectangle.Bottom, g);
                                break;
                        }
                    }
                    if (b2.InterpolationColors == null || b2.InterpolationColors.IsEmpty)
                    {
                        strCode.Append("$0$");
                        strCode.Append(ColorToString(b2.Color1));
                        strCode.Append("$1");
                        strCode.Append(ColorToString(b2.Color2));
                    }
                    else
                    {
                        var colors = b2.InterpolationColors.Colors;
                        var pos = b2.InterpolationColors.Positions;
                        var len = Math.Min(colors.Length, pos.Length);
                        for (var iCount = 0; iCount < len; iCount++)
                        {
                            strCode.Append('$');
                            strCode.Append(pos[iCount]);
                            strCode.Append('$');
                            strCode.Append(ColorToString(colors[iCount]));
                        }
                    }
                    var strText2 = strCode.ToString();
                    strResult.AppendString(strText2);
                }
                //else if(item is System.Drawing.TextureBrush tb )
                //{

                //}
                else
                {
                    strResult.AppendString("red");
                }
            }
            private static void AppendPositions(StringBuilder strCode, float x1, float y1, float x2, float y2, CanvasGraphics g)
            {
                var zoomRate = g._ZoomRate * g._PageUnitRate;
                var dx = g._AbsoluteOffsetX * g._ZoomRate;
                var dy = g._AbsoluteOffsetY * g._ZoomRate;

                strCode.Append('$');
                strCode.Append(x1 * zoomRate + dx);
                strCode.Append('$');
                strCode.Append(y1 * zoomRate + dy);
                strCode.Append('$');
                strCode.Append(x2 * zoomRate + dx);
                strCode.Append('$');
                strCode.Append(y2 * zoomRate + dy);
            }
            private static string ColorToString(Color c)
            {
                if (c.A == 255)
                {
                    return ColorTranslator.ToHtml(c);
                }
                else
                {
                    return "rgba(" + c.R + "," + c.G + "," + c.B + "," + (c.A / 255.0).ToString("0.00") + ")";
                }
            }
            public override bool Equals(Brush x, Brush y)
            {
                var sb1 = x as SolidBrush;
                var sb2 = y as SolidBrush;
                if (sb1 != null && sb2 != null)
                {
                    return sb1.Color == sb2.Color;
                }
                else
                {
                    return false;// x.EqualsValue(y);
                }
            }

            public override int GetHashCode(Brush obj)
            {
                if (obj is SolidBrush)
                {
                    return ((SolidBrush)obj).Color.ToArgb();
                }
                else
                {
                    return obj.GetHashCode();
                }
            }
        }
        private MyPenTable _PenTable = new MyPenTable();
        private class MyPenTable : ObjectCache<Pen>
        {
            public override short GetIndex(Pen v)
            {
                if (v != null
                    && base._LastKeyValue != null
                    && v.EqualsValue(base._LastKeyValue))
                {
                    return base._LastIndex;
                }
                return base.GetIndex(v);
            }
            public float ZoomRate = 1f;

            protected override Pen CloneObject(Pen v)
            {
                return (Pen)v.Clone();
            }
            protected override GraphicsRecordType GetRecordType()
            {
                return GraphicsRecordType.PenTable;
            }
            protected override void WriteItemTo(MyBinaryBuilder strResult, Pen item, CanvasGraphics g)
            {
                strResult.AppendString(ColorTranslator.ToHtml(item.Color));
                if (item.Width == 0)
                {
                    strResult.AppendByte(0);
                }
                else if (item.Width == 1)
                {
                    strResult.AppendByte(1);
                }
                else
                {
                    var lw = (byte)Math.Ceiling(this.ZoomRate * item.Width);
                    //if( lw == 7 )
                    //{
                    //    lw = 2;
                    //}
                    strResult.AppendByte(lw);
                }
                strResult.AppendByte((byte)item.DashStyle);
            }
            public override bool Equals(Pen x, Pen y)
            {

                return x.Color.ToArgb() == y.Color.ToArgb() && x.Width == y.Width && x.DashStyle == y.DashStyle;
            }

            public override int GetHashCode(Pen obj)
            {
                return obj.Color.ToArgb() + obj.Width.GetHashCode() + obj.DashStyle.GetHashCode();
            }
        }
        private MyColorTable _ColorTable = new MyColorTable();
        private class MyColorTable : ObjectCache<Color>
        {
            protected override GraphicsRecordType GetRecordType()
            {
                return GraphicsRecordType.ColorTable;
            }
            protected override void WriteItemTo(MyBinaryBuilder strResult, Color item, CanvasGraphics g)
            {
                if (item.A != 255)
                {
                    strResult.AppendString("argb(" + item.R + "," + item.G + "," + item.B + "," + (item.A / 255.0) + ")");
                }
                else
                {
                    strResult.AppendString(ColorTranslator.ToHtml(item));
                }
            }
            public override bool Equals(Color x, Color y)
            {
                return x.ToArgb() == y.ToArgb();
            }
            public override int GetHashCode(Color obj)
            {
                return obj.ToArgb();
            }
        }
        private Font _CurrentFont = null;
        private void SetCurrentFont(Font font)
        {
            if (font == null || string.IsNullOrEmpty(font.Name))
            {
                var intIndex = this._FontTable.GetIndex(SystemFonts.DefaultFont);
                this._CurrentFont = null;
                _BinaryData.AppendRecordType(GraphicsRecordType.SetCurrentFont);
                this._BinaryData.AppendInt16((short)intIndex);
            }
            if (this._CurrentFont == null || this._CurrentFont.Equals(font) == false)
            {
                var intIndex = this._FontTable.GetIndex(font);
                _BinaryData.AppendRecordType(GraphicsRecordType.SetCurrentFont);
                this._BinaryData.AppendInt16((short)intIndex);
                this._CurrentFont = font;
            }
        }

        nint IDeviceContext.GetHdc()
        {
            throw new NotImplementedException();
        }

        void IDeviceContext.ReleaseHdc()
        {
            throw new NotImplementedException();
        }

        private MyFontTable _FontTable = new MyFontTable();
        private class MyFontTable : ObjectCache<Font>
        {
            public float ZoomRate = 1f;
            protected override GraphicsRecordType GetRecordType()
            {
                return GraphicsRecordType.FontTable;
            }
            private StringBuilder _StrTemp = new StringBuilder();
            protected override void WriteItemTo(MyBinaryBuilder strResult, Font item, CanvasGraphics g)
            {
                if ((item.Style & FontStyle.Bold) == FontStyle.Bold)
                {
                    this._StrTemp.Append("bold ");
                }
                if ((item.Style & FontStyle.Italic) == FontStyle.Italic)
                {
                    this._StrTemp.Append("italic ");
                }
                if (item.Size > 0)
                {
                    var size2 = this.ZoomRate * item.Size;
                    var pxSize = GraphicsUnitConvert.Convert(size2, GraphicsUnit.Point, GraphicsUnit.Pixel);
                    if (Math.Abs(pxSize - (int)pxSize) < 0.5)
                    {
                        this._StrTemp.Append(pxSize.ToString()).Append("px ");
                    }
                    else
                    {
                        this._StrTemp.Append(pxSize.ToString("0.00")).Append("px ");
                    }
                    //if (size2 == 12)
                    //{
                    //    this._StrTemp.Append("12pt ");
                    //}
                    //else if (size2 == 9)
                    //{
                    //    this._StrTemp.Append("9pt ");
                    //}
                    //else if (size2 == 14)
                    //{
                    //    this._StrTemp.Append("14pt ");
                    //}
                    //else
                    //{
                    //    if (Math.Abs(size2 - (int)size2) < 0.1)
                    //    {
                    //        // 尽量输出为整数
                    //        this._StrTemp.Append(StringCommon.Int32ToString((int)size2) + "pt ");
                    //    }
                    //    else
                    //    {
                    //        this._StrTemp.Append(size2.ToString() + "pt ");
                    //    }
                    //}
                }
                if (string.IsNullOrEmpty(item.Name))
                {
                    this._StrTemp.Append("宋体");
                }
                else
                {
                    this._StrTemp.Append(item.Name);
                }
                strResult.AppendString(_StrTemp.ToString());
                this._StrTemp.Clear();
            }

            public override void Clear()
            {
                this._StrTemp = null;
                base.Clear();
            }
            public override bool Equals(Font x, Font y)
            {
                return x.Name == y.Name && x.Size == y.Size && x.Style == y.Style;
            }

            public override int GetHashCode(Font obj)
            {
                var result = obj.Name == null ? 0 : obj.Name.GetHashCode();
                result = result + obj.Size.GetHashCode() + (int)obj.Style;
                return result;
            }
        }
        private abstract class ObjectCache<T> : IEqualityComparer<T>
        {
            protected ObjectCache()
            {
                this._Values = new Dictionary<T, short>(this);
            }
            private Dictionary<T, short> _Values = null;
            protected short _LastIndex = -1;
            protected T _LastKeyValue;
            public virtual short GetIndex(T v)
            {
                //if (this._LastIndex >= 0 && this.Equals(v, this._LastKeyValue))
                //{
                //    return this._LastIndex;
                //}
                short index = 0;
                if (this._Values.TryGetValue(v, out index) == false)
                {
                    index = (short)this._Values.Count;
                    v = CloneObject(v);
                    this._Values[v] = index;
                    this._LastKeyValue = v;
                }
                else
                {
                    this._LastKeyValue = v;
                }
                this._LastIndex = index;
                return index;// StringCommon.Int32ToString(index);
            }
            public virtual void Clear()
            {
                if (this._Values != null)
                {
                    this._Values.Clear();
                    this._Values = null;
                }
            }

            private T[] ToArrary()
            {
                if (this._Values.Count == 0)
                {
                    return null;
                }
                var result = new T[this._Values.Count];
                foreach (var item in this._Values)
                {
                    result[item.Value] = item.Key;
                }
                return result;
            }

            protected virtual T CloneObject(T v)
            {
                return v;
            }

            protected abstract GraphicsRecordType GetRecordType();

            protected abstract void WriteItemTo(MyBinaryBuilder strResult, T item, CanvasGraphics g);

            public void WriteTo(MyBinaryBuilder strResult, CanvasGraphics g)
            {
                if (this._Values.Count > 0)
                {
#if OutputGraphicsRecordTypeName
                    strResult.Append('"' + this.GetRecordType().ToString() + '"');
#else
                    strResult.AppendRecordType(this.GetRecordType());
#endif
                    var array = this.ToArrary();
                    strResult.AppendInt16((short)array.Length);
                    foreach (var item in array)
                    {
                        WriteItemTo(strResult, item, g);
                    }
                }
            }
            public abstract bool Equals(T item1, T item2);
            public abstract int GetHashCode(T item);
        }
        //#if FINALIZATION_WATCH
        //        static readonly TraceSwitch GraphicsFinalization = new TraceSwitch("GraphicsFinalization", "Tracks the creation and destruction of finalization");
        //        internal static string GetAllocationStack() {
        //            if (GraphicsFinalization.TraceVerbose) {
        //                return Environment.StackTrace;
        //            }
        //            else {
        //                return "Enabled 'GraphicsFinalization' switch to see stack of allocation";
        //            }
        //        }
        //        private string allocationSite = Graphics.GetAllocationStack();
        //#endif

        //        /// <devdoc>
        //        ///     The context state previous to the current Graphics context (the head of the stack).
        //        ///     We don't keep a GraphicsContext for the current context since it is available at any time from GDI+ and
        //        ///     we don't want to keep track of changes in it.
        //        /// </devdoc>
        //        private GraphicsContext previousContext;

        //        /// <devdoc>
        //        ///     Object to lock on for static methods - DO NOT use the Type, see bug#464117.
        //        /// </devdoc>
        //        private static readonly object syncObject = new Object();

        //        /// <devdoc>
        //        ///     Handle to native GDI+ graphics object.  This object is created on demand.
        //        /// </devdoc>
        //        private IntPtr nativeGraphics;

        //        /// <devdoc>
        //        ///     Handle to native DC - obtained from the GDI+ graphics object.
        //        ///     We need to cache it to implement IDeviceContext interface.
        //        /// </devdoc>
        //        private IntPtr nativeHdc;

        //        // Object reference used for printing; it could point to a PrintPreviewGraphics to obtain the VisibleClipBounds, or 
        //        // a DeviceContext holding a printer DC.
        //        private object printingHelper;

        //        // GDI+'s preferred HPALETTE.
        //        private static IntPtr halftonePalette;

        //        // pointer back to the Image backing a specific graphic object
        //        private Image backingImage;

        public delegate bool DrawImageAbort(IntPtr callbackdata);

        // Callback for EnumerateMetafile methods.  The parameters are:

        //      recordType      (if >= MinRecordType, it's an EMF+ record)
        //      flags           (always 0 for EMF records)
        //      dataSize        size of the data, or 0 if no data
        //      data            pointer to the data, or NULL if no data (UINT32 aligned)
        //      callbackData    pointer to callbackData, if any

        // This method can then call Metafile.PlayRecord to play the
        // record that was just enumerated.  If this method  returns
        // FALSE, the enumeration process is aborted.  Otherwise, it continues.        

        public delegate bool EnumerateMetafileProc(EmfPlusRecordType recordType,
                                                   int flags,
                                                   int dataSize,
                                                   IntPtr data,
                                                   PlayRecordCallback callbackData);


        //        /// <devdoc>
        //        ///     Constructor to initialize this object from a native GDI+ Graphics pointer.
        //        /// </devdoc>
        //        private Graphics(IntPtr gdipNativeGraphics)
        //        {
        //            if (gdipNativeGraphics == IntPtr.Zero)
        //            {
        //                throw new ArgumentNullException("gdipNativeGraphics");
        //            }
        //            this.nativeGraphics = gdipNativeGraphics;
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FromHdc"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Creates a new instance of the <see cref='System.Drawing.Graphics'/> class from the specified
        //        ///       handle to a device context.
        //        ///    </para>
        //        /// </devdoc>
        //        [EditorBrowsable(EditorBrowsableState.Advanced)]
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public static Graphics FromHdc(IntPtr hdc)
        //        {
        //            //IntSecurity.ObjectFromWin32Handle.Demand();

        //            if (hdc == IntPtr.Zero)
        //            {
        //                throw new ArgumentNullException("hdc");
        //            }

        //            return FromHdcInternal(hdc);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FromHdcInternal"]/*' />
        //        //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        //        [EditorBrowsable(EditorBrowsableState.Advanced)]
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public static Graphics FromHdcInternal(IntPtr hdc)
        //        {
        //            Debug.Assert(hdc != IntPtr.Zero, "Must pass in a valid DC");
        //            IntPtr nativeGraphics = IntPtr.Zero;

        //            int status = SafeNativeMethods.Gdip.GdipCreateFromHDC(new HandleRef(null, hdc), out nativeGraphics);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            return new Graphics(nativeGraphics);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FromHdc1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Creates a new instance of the Graphics class from the specified handle to 
        //        ///       a device context and handle to a device.
        //        ///    </para>
        //        /// </devdoc>
        //        [EditorBrowsable(EditorBrowsableState.Advanced)]
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public static Graphics FromHdc(IntPtr hdc, IntPtr hdevice)
        //        {
        //            //IntSecurity.ObjectFromWin32Handle.Demand();

        //            Debug.Assert(hdc != IntPtr.Zero, "Must pass in a valid DC");
        //            Debug.Assert(hdevice != IntPtr.Zero, "Must pass in a valid device");

        //            IntPtr gdipNativeGraphics = IntPtr.Zero;
        //            int status = SafeNativeMethods.Gdip.GdipCreateFromHDC2(new HandleRef(null, hdc), new HandleRef(null, hdevice), out gdipNativeGraphics);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            return new Graphics(gdipNativeGraphics);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FromHwnd"]/*' />
        //        /// <devdoc>
        //        ///    Creates a new instance of the <see cref='System.Drawing.Graphics'/> class
        //        ///    from a window handle.
        //        /// </devdoc>
        //        [EditorBrowsable(EditorBrowsableState.Advanced)]
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public static Graphics FromHwnd(IntPtr hwnd)
        //        {
        //            //IntSecurity.ObjectFromWin32Handle.Demand();
        //            return FromHwndInternal(hwnd);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FromHwndInternal"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        //        [EditorBrowsable(EditorBrowsableState.Advanced)]
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public static Graphics FromHwndInternal(IntPtr hwnd)
        //        {
        //            IntPtr nativeGraphics = IntPtr.Zero;

        //            int status = SafeNativeMethods.Gdip.GdipCreateFromHWND(new HandleRef(null, hwnd), out nativeGraphics);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            return new Graphics(nativeGraphics);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FromImage"]/*' />
        //        /// <devdoc>
        //        ///    Creates an instance of the <see cref='System.Drawing.Graphics'/> class
        //        ///    from an existing <see cref='System.Drawing.Image'/>.
        //        /// </devdoc>
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public static Graphics FromImage(Image image)
        //        {
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            if ((image.PixelFormat & PixelFormat.Indexed) != 0)
        //            {
        //                throw new Exception(SR.GetString(SR.GdiplusCannotCreateGraphicsFromIndexedPixelFormat));
        //            }
        //            Contract.Ensures(Contract.Result<Graphics>() != null);

        //            IntPtr gdipNativeGraphics = IntPtr.Zero;

        //            int status = SafeNativeMethods.Gdip.GdipGetImageGraphicsContext(new HandleRef(image, image.nativeImage),
        //                                                                            out gdipNativeGraphics);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            Graphics result = new Graphics(gdipNativeGraphics);
        //            result.backingImage = image;
        //            return result;
        //        }


        //        /// <devdoc>
        //        ///     Gets the GDI+ native graphics pointer.
        //        /// </devdoc>
        //        internal IntPtr NativeGraphics
        //        {
        //            [System.Runtime.TargetedPatchingOptOutAttribute("Performance critical to inline across NGen image boundaries")]
        //            get
        //            {
        //                Debug.Assert(this.nativeGraphics != IntPtr.Zero, "this.nativeGraphics == IntPtr.Zero.");
        //                return this.nativeGraphics;
        //            }
        //        }

        //        // SECREVIEW : About GetHdc/ReleaseHdc (See IDeviceContext interface for more info).
        //        //             - GetHdc()/GetHdc(bool) lock the Graphics object until ReleaseHdc is called.  Operations on the hdc require 
        //        //               unmanaged code permission.  The security thread is when a Graphics object that is used by a library is 
        //        //               (by bad design) handed out to some third party component (plug-in) that can block it generating a DOS attack.
        //        //             - Releasing a handed-out hdc is safe, that's the expected action from the user when done. ReleaseHdc will fail if 
        //        //               GetHdc was not previously called. Also, if the Graphics object was created from an existing dc, GDI+ will not 
        //        //               actually release it because it does not own it, it will just restore it to its original state.
        //        //             - ReleaseHdc() is part of the IDeviceContext interface, which only trusted assemblies can implement.
        //        //             - ReleaseHdc(IntPtr)/ReleaseHdcInternal(IntPtr) could be risky since we don't validate the passed-in IntPtr to
        //        //               avoid introducing a breaking change.  GDI+ returns an 'invalid parameter used' error code if  the passed hdc 
        //        //               is different from the one obtained with GetHdc(), howerver.

        //        /// <devdoc>
        //        ///     Implementation of IDeviceContext.GetHdc().
        //        /// </devdoc>
        //        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
        //        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]            
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public IntPtr GetHdc()
        //        {
        //            IntPtr hdc = IntPtr.Zero;

        //            int status = SafeNativeMethods.Gdip.GdipGetDC(new HandleRef(this, this.NativeGraphics), out hdc);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            this.nativeHdc = hdc; // need to cache the hdc to be able to release with a call to IDeviceContext.ReleaseHdc().

        //            return this.nativeHdc;
        //        }


        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.ReleaseHdc"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Releases the memory allocated for the handle to a device context.
        //        ///    </para>
        //        /// </devdoc>
        //        [EditorBrowsable(EditorBrowsableState.Advanced)]
        //        public void ReleaseHdc(IntPtr hdc)
        //        {
        //            //IntSecurity.Win32HandleManipulation.Demand();
        //            ReleaseHdcInternal(hdc);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.ReleaseHdc1"]/*' />
        //        /// <devdoc>
        //        ///     Implementation of IDeviceContext.ReleaseHdc().
        //        /// </devdoc>
        //        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]            
        //        public void ReleaseHdc()
        //        {
        //            ReleaseHdcInternal(this.nativeHdc);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.ReleaseHdcInternal"]/*' />
        //        /// <devdoc>
        //        ///    This method is public but is meant to be used by the .Net Framework only.
        //        ///    From MSDN: Internal method. Do not use.
        //        /// </devdoc>
        //        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        //        [EditorBrowsable(EditorBrowsableState.Never)]
        //        public void ReleaseHdcInternal(IntPtr hdc)
        //        {
        //            Debug.Assert(hdc == this.nativeHdc, "Invalid hdc.");


        //            int status = SafeNativeMethods.Gdip.GdipReleaseDC(new HandleRef(this, this.NativeGraphics), new HandleRef(null, hdc));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            this.nativeHdc = IntPtr.Zero;
        //        }

        //        /**
        //         * Dispose of resources associated with the graphics context
        //         *
        //         * @notes How do we set up delegates to notice others
        //         *  when a Graphics object is disposed.
        //         */
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Dispose"]/*' />
        //        /// <devdoc>
        //        ///    Deletes this <see cref='System.Drawing.Graphics'/>, and
        //        ///    frees the memory allocated for it.
        //        /// </devdoc>        
        //        [SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
        //        public void Dispose()
        //        {
        //            Dispose(true);
        //            GC.SuppressFinalize(this);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Dispose2"]/*' />
        //        [SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
        //        void Dispose(bool disposing)
        //        {
        //            //#if DEBUG
        //            //            if (!disposing && nativeGraphics != IntPtr.Zero ) {
        //            //                // Recompile commonUI\\system\\Drawing\\Graphics.cs with FINALIZATION_WATCH on to find who allocated it.
        //            //#if FINALIZATION_WATCH
        //            //                //Debug.Fail("Graphics object Disposed through finalization:\n" + allocationSite);
        //            //                Debug.WriteLine("System.Drawing.Graphics: ***************************************************");
        //            //                Debug.WriteLine("System.Drawing.Graphics: Object Disposed through finalization:\n" + allocationSite);
        //            //#else
        //            //                //Debug.Fail("A Graphics object was not Dispose()'d.  Please make sure it's not your code that should be calling Dispose().");
        //            //#endif
        //            //            }
        //            //#endif
        //            //            while(this.previousContext != null) {
        //            //                // Dispose entire stack.
        //            //                GraphicsContext context = this.previousContext.Previous;
        //            //                this.previousContext.Dispose();
        //            //                this.previousContext = context;
        //            //            }

        //            //            if (this.nativeGraphics != IntPtr.Zero) 
        //            //            {
        //            //                try
        //            //                {
        //            //                    if (this.nativeHdc != IntPtr.Zero) // avoid a handle leak.
        //            //                    {
        //            //                        ReleaseHdc();
        //            //                    }

        //            //                    if( this.PrintingHelper != null )
        //            //                    {
        //            //                        DeviceContext printerDC = this.PrintingHelper as DeviceContext;

        //            //                        if( printerDC != null )
        //            //                        {
        //            //                            printerDC.Dispose();
        //            //                            this.printingHelper = null;
        //            //                        }
        //            //                    }

        //            //#if DEBUG
        //            //                    int status =
        //            //#endif
        //            //                    SafeNativeMethods.Gdip.GdipDeleteGraphics(new HandleRef(this, this.nativeGraphics));

        //            //#if DEBUG
        //            //                    Debug.Assert(status == SafeNativeMethods.Gdip.Ok, "GDI+ returned an error status: " + status.ToString(CultureInfo.InvariantCulture));
        //            //#endif
        //            //                }
        //            //                catch(Exception ex)  // do not allow exceptions to propagate during disposing.
        //            //                {
        //            //                    if( ClientUtils.IsSecurityOrCriticalException( ex ) )
        //            //                    {
        //            //                        throw;
        //            //                    }
        //            //                    Debug.Fail("Exception thrown during disposing: \r\n" + ex.ToString());
        //            //                } 
        //            //                finally
        //            //                {
        //            //                    this.nativeGraphics = IntPtr.Zero;
        //            //                }
        //            //}
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Finalize"]/*' />
        //        /// <devdoc>
        //        ///    Deletes this <see cref='System.Drawing.Graphics'/>, and
        //        ///    frees the memory allocated for it.
        //        /// </devdoc>
        //        ~Graphics()
        //        {
        //            Dispose(false);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Flush"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Forces immediate execution of all operations currently on the stack.
        //        ///    </para>
        //        /// </devdoc>
        //        public void Flush()
        //        {
        //            Flush(FlushIntention.Flush);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Flush1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Forces execution of all operations currently on the stack.
        //        ///    </para>
        //        /// </devdoc>
        //        public void Flush(FlushIntention intention)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipFlush(new HandleRef(this, this.NativeGraphics), intention);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }


        //        /*
        //        * Methods for setting/getting:
        //        *  compositing mode
        //        *  rendering quality hint
        //        *
        //        * @notes We should probably separate rendering hints
        //        *  into several categories, e.g. antialiasing, image
        //        *  filtering, etc.
        //        */

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.CompositingMode"]/*' />
        //        /// <devdoc>
        //        ///    Gets or sets the <see cref='System.Drawing.Drawing2D.CompositingMode'/> associated with this <see cref='System.Drawing.Graphics'/>.
        //        /// </devdoc>
        //        public CompositingMode CompositingMode
        //        {
        //            get
        //            {
        //                int mode = 0;

        //                int status = SafeNativeMethods.Gdip.GdipGetCompositingMode(new HandleRef(this, this.NativeGraphics), out mode);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return (CompositingMode)mode;
        //            }
        //            set
        //            {
        //                //validate the enum value
        //                ////valid values are 0x0 to 0x1
        //                //if (!ClientUtils.IsEnumValid(value, unchecked((int)value), (int)CompositingMode.SourceOver, (int)CompositingMode.SourceCopy)){
        //                //    throw new InvalidEnumArgumentException("value", unchecked((int)value), typeof(CompositingMode));
        //                //}

        //                //int status = SafeNativeMethods.Gdip.GdipSetCompositingMode(new HandleRef(this, this.NativeGraphics), unchecked((int)value));

        //                //if (status != SafeNativeMethods.Gdip.Ok) {
        //                //    throw SafeNativeMethods.Gdip.StatusException(status);
        //                //}
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.RenderingOrigin"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        public Point RenderingOrigin
        //        {
        //            get
        //            {
        //                int x, y;

        //                int status = SafeNativeMethods.Gdip.GdipGetRenderingOrigin(new HandleRef(this, this.NativeGraphics), out x, out y);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return new Point(x, y);
        //            }
        //            set
        //            {
        //                int status = SafeNativeMethods.Gdip.GdipSetRenderingOrigin(new HandleRef(this, this.NativeGraphics), value.X, value.Y);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.CompositingQuality"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        public CompositingQuality CompositingQuality
        //        {
        //            get
        //            {
        //                CompositingQuality cq;

        //                int status = SafeNativeMethods.Gdip.GdipGetCompositingQuality(new HandleRef(this, this.NativeGraphics), out cq);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return cq;
        //            }
        //            set
        //            {
        //                //valid values are 0xffffffff to 0x4
        //                //if (!ClientUtils.IsEnumValid(value, unchecked((int)value), unchecked((int)CompositingQuality.Invalid), unchecked((int)CompositingQuality.AssumeLinear)))
        //                //{
        //                //    throw new InvalidEnumArgumentException("value", unchecked((int)value), typeof(CompositingQuality));
        //                //}

        //                //int status = SafeNativeMethods.Gdip.GdipSetCompositingQuality(new HandleRef(this, this.NativeGraphics), 
        //                //                                               value);

        //                //if (status != SafeNativeMethods.Gdip.Ok) {
        //                //    throw SafeNativeMethods.Gdip.StatusException(status);
        //                //}
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.TextRenderingHint"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets or sets the rendering mode for text associated with
        //        ///       this <see cref='System.Drawing.Graphics'/>
        //        ///       .
        //        ///    </para>
        //        /// </devdoc>
        //        public TextRenderingHint TextRenderingHint
        //        {
        //            get
        //            {
        //                TextRenderingHint hint = 0;

        //                int status = SafeNativeMethods.Gdip.GdipGetTextRenderingHint(new HandleRef(this, this.NativeGraphics), out hint);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return hint;
        //            }
        //            set
        //            {
        //                ////valid values are 0x0 to 0x5
        //                //if (!ClientUtils.IsEnumValid(value, unchecked((int)value), (int)TextRenderingHint.SystemDefault, unchecked((int)TextRenderingHint.ClearTypeGridFit)))
        //                //{
        //                //    throw new InvalidEnumArgumentException("value", unchecked((int)value), typeof(TextRenderingHint));
        //                //}

        //                //int status = SafeNativeMethods.Gdip.GdipSetTextRenderingHint(new HandleRef(this, this.NativeGraphics), value);

        //                //if (status != SafeNativeMethods.Gdip.Ok) {
        //                //    throw SafeNativeMethods.Gdip.StatusException(status);
        //                //}
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.TextContrast"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        public int TextContrast
        //        {
        //            get
        //            {
        //                int tgv = 0;

        //                int status = SafeNativeMethods.Gdip.GdipGetTextContrast(new HandleRef(this, this.NativeGraphics), out tgv);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return tgv;
        //            }
        //            set
        //            {
        //                int status = SafeNativeMethods.Gdip.GdipSetTextContrast(new HandleRef(this, this.NativeGraphics), value);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SmoothingMode"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        public SmoothingMode SmoothingMode
        //        {
        //            get
        //            {
        //                SmoothingMode mode = 0;

        //                int status = SafeNativeMethods.Gdip.GdipGetSmoothingMode(new HandleRef(this, this.NativeGraphics), out mode);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return mode;
        //            }
        //            set
        //            {
        //                //valid values are 0xffffffff to 0x4
        //                //if (!ClientUtils.IsEnumValid(value, unchecked((int)value), unchecked((int)SmoothingMode.Invalid), unchecked((int)SmoothingMode.AntiAlias)))
        //                //{
        //                //    throw new InvalidEnumArgumentException("value", unchecked((int)value), typeof(SmoothingMode));
        //                //}

        //                //int status = SafeNativeMethods.Gdip.GdipSetSmoothingMode(new HandleRef(this, this.NativeGraphics), value);

        //                //if (status != SafeNativeMethods.Gdip.Ok) {
        //                //    throw SafeNativeMethods.Gdip.StatusException(status);
        //                //}
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.PixelOffsetMode"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        public PixelOffsetMode PixelOffsetMode
        //        {
        //            get
        //            {
        //                PixelOffsetMode mode = 0;

        //                int status = SafeNativeMethods.Gdip.GdipGetPixelOffsetMode(new HandleRef(this, this.NativeGraphics), out mode);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return mode;
        //            }
        //            set
        //            {
        //                //valid values are 0xffffffff to 0x4
        //                //if (!ClientUtils.IsEnumValid(value, unchecked((int)value), unchecked((int)PixelOffsetMode.Invalid), unchecked((int)PixelOffsetMode.Half)))
        //                //{
        //                //    throw new InvalidEnumArgumentException("value", unchecked((int)value), typeof(PixelOffsetMode));
        //                //}

        //                //int status = SafeNativeMethods.Gdip.GdipSetPixelOffsetMode(new HandleRef(this, this.NativeGraphics), value);

        //                //if (status != SafeNativeMethods.Gdip.Ok) {
        //                //    throw SafeNativeMethods.Gdip.StatusException(status);
        //                //}
        //            }
        //        }

        //        /// <devdoc>
        //        ///    Represents an object used in conection with the printing API, it is used to hold a reference to a PrintPreviewGraphics (fake graphics)
        //        ///    or a printer DeviceContext (and maybe more in the future).
        //        /// </devdoc>
        //        internal object PrintingHelper
        //        {
        //            get
        //            {
        //                return this.printingHelper;
        //            }
        //            set
        //            {
        //                Debug.Assert(this.printingHelper == null, "WARNING: Overwritting the printing helper reference!");
        //                this.printingHelper = value;
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.InterpolationMode"]/*' />
        //        /// <devdoc>
        //        ///    Gets or sets the interpolation mode
        //        ///    associated with this Graphics.
        //        /// </devdoc>
        //        public InterpolationMode InterpolationMode
        //        {
        //            get
        //            {
        //                int mode = 0;

        //                int status = SafeNativeMethods.Gdip.GdipGetInterpolationMode(new HandleRef(this, this.NativeGraphics), out mode);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return (InterpolationMode)mode;
        //            }
        //            set
        //            {
        //                ////validate the enum value
        //                ////valid values are 0xffffffff to 0x7
        //                //if (!ClientUtils.IsEnumValid(value, unchecked((int)value), unchecked((int)InterpolationMode.Invalid), unchecked((int)InterpolationMode.HighQualityBicubic)))
        //                //{
        //                //    throw new InvalidEnumArgumentException("value", unchecked((int)value), typeof(InterpolationMode));
        //                //}

        //                //int status = SafeNativeMethods.Gdip.GdipSetInterpolationMode(new HandleRef(this, this.NativeGraphics), unchecked((int)value));

        //                //if (status != SafeNativeMethods.Gdip.Ok) {
        //                //    throw SafeNativeMethods.Gdip.StatusException(status);
        //                //}
        //            }
        //        }

        //        /**
        //         * Return the current world transform
        //         */
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Transform"]/*' />
        //        /// <devdoc>
        //        ///    Gets or sets the world transform
        //        ///    for this <see cref='System.Drawing.Graphics'/>.
        //        /// </devdoc>
        //        public Matrix Transform
        //        {
        //            get
        //            {
        //                Matrix matrix = new Matrix();

        //                int status = SafeNativeMethods.Gdip.GdipGetWorldTransform(new HandleRef(this, this.NativeGraphics),
        //                                                           new HandleRef(matrix, matrix.nativeMatrix));

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return matrix;
        //            }
        //            set
        //            {
        //                int status = SafeNativeMethods.Gdip.GdipSetWorldTransform(new HandleRef(this, this.NativeGraphics),
        //                                                           new HandleRef(value, value.nativeMatrix));

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }
        //            }
        //        }

        //        private GraphicsUnit _PageUnit = GraphicsUnit.Pixel;
        //        /**
        //         * Retrieve the current page transform information
        //         * notes @ these are atomic
        //         */
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.PageUnit"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public GraphicsUnit PageUnit
        //        {
        //            get
        //            {
        //                int unit = 0;

        //                int status = SafeNativeMethods.Gdip.GdipGetPageUnit(new HandleRef(this, this.NativeGraphics), out unit);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return (GraphicsUnit)unit;
        //            }
        //            set
        //            {
        //                ////valid values are 0x0 to 0x6
        //                //if (!ClientUtils.IsEnumValid(value, unchecked((int)value), (int)GraphicsUnit.World, (int)GraphicsUnit.Millimeter))
        //                //{
        //                //    throw new InvalidEnumArgumentException("value", unchecked((int)value), typeof(GraphicsUnit));
        //                //}

        //                //int status = SafeNativeMethods.Gdip.GdipSetPageUnit(new HandleRef(this, this.NativeGraphics), unchecked((int) value));

        //                //if (status != SafeNativeMethods.Gdip.Ok) {
        //                //    throw SafeNativeMethods.Gdip.StatusException(status);
        //                //}
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.PageScale"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public float PageScale
        //        {
        //            get
        //            {
        //                float[] scale = new float[] { 0.0f };

        //                int status = SafeNativeMethods.Gdip.GdipGetPageScale(new HandleRef(this, this.NativeGraphics), scale);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return scale[0];
        //            }

        //            set
        //            {
        //                int status = SafeNativeMethods.Gdip.GdipSetPageScale(new HandleRef(this, this.NativeGraphics), value);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DpiX"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public float DpiX
        //        {
        //            get
        //            {
        //                float[] dpi = new float[] { 0.0f };

        //                int status = SafeNativeMethods.Gdip.GdipGetDpiX(new HandleRef(this, this.NativeGraphics), dpi);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return dpi[0];
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DpiY"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public float DpiY
        //        {
        //            get
        //            {
        //                float[] dpi = new float[] { 0.0f };

        //                int status = SafeNativeMethods.Gdip.GdipGetDpiY(new HandleRef(this, this.NativeGraphics), dpi);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return dpi[0];
        //            }
        //        }


        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.CopyFromScreen"]/*' />
        //        /// <devdoc>
        //        ///     CopyPixels will perform a gdi "bitblt" operation to the source from the destination
        //        ///     with the given size.
        //        /// </devdoc>
        //        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize)
        //        {
        //            CopyFromScreen(upperLeftSource.X, upperLeftSource.Y, upperLeftDestination.X, upperLeftDestination.Y, blockRegionSize);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.CopyFromScreen1"]/*' />
        //        /// <devdoc>
        //        ///     CopyPixels will perform a gdi "bitblt" operation to the source from the destination
        //        ///     with the given size.
        //        /// </devdoc>
        //        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize)
        //        {
        //            CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize, CopyPixelOperation.SourceCopy);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.CopyFromScreen2"]/*' />
        //        /// <devdoc>
        //        ///     CopyPixels will perform a gdi "bitblt" operation to the source from the destination
        //        ///     with the given size and specified raster operation.
        //        /// </devdoc>
        //        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
        //        {
        //            CopyFromScreen(upperLeftSource.X, upperLeftSource.Y, upperLeftDestination.X, upperLeftDestination.Y, blockRegionSize, copyPixelOperation);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.CopyFromScreen3"]/*' />
        //        /// <devdoc>
        //        ///     CopyPixels will perform a gdi "bitblt" operation to the source from the destination
        //        ///     with the given size and specified raster operation.
        //        /// </devdoc>        
        //        [SuppressMessage("Microsoft.Performance", "CA1803:AvoidCostlyCallsWherePossible")]
        //        [ResourceExposure(ResourceScope.None)]
        //        [ResourceConsumption(ResourceScope.Process, ResourceScope.Process)]
        //        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
        //        {

        //            //switch(copyPixelOperation) { 
        //            //    case CopyPixelOperation.Blackness:
        //            //    case CopyPixelOperation.NotSourceErase:
        //            //    case CopyPixelOperation.NotSourceCopy:
        //            //    case CopyPixelOperation.SourceErase:
        //            //    case CopyPixelOperation.DestinationInvert:
        //            //    case CopyPixelOperation.PatInvert:
        //            //    case CopyPixelOperation.SourceInvert:
        //            //    case CopyPixelOperation.SourceAnd:
        //            //    case CopyPixelOperation.MergePaint:
        //            //    case CopyPixelOperation.MergeCopy:
        //            //    case CopyPixelOperation.SourceCopy:
        //            //    case CopyPixelOperation.SourcePaint:
        //            //    case CopyPixelOperation.PatCopy:
        //            //    case CopyPixelOperation.PatPaint:
        //            //    case CopyPixelOperation.Whiteness:
        //            //    case CopyPixelOperation.CaptureBlt:
        //            //    case CopyPixelOperation.NoMirrorBitmap:
        //            //        break;
        //            //    default: 
        //            //        throw new InvalidEnumArgumentException("value", unchecked((int)copyPixelOperation), typeof(CopyPixelOperation)); 
        //            // }
        //            //(new System.Security.Permissions.UIPermission(System.Security.Permissions.UIPermissionWindow.AllWindows)).Demand();

        //            //int destWidth = blockRegionSize.Width;
        //            //int destHeight = blockRegionSize.Height; 

        //            //using( DeviceContext dc = DeviceContext.FromHwnd( IntPtr.Zero )){  // screen DC
        //            //    HandleRef screenDC = new HandleRef( null, dc.Hdc );
        //            //    HandleRef targetDC = new HandleRef( null, this.GetHdc());      // this DC

        //            //    try{
        //            //        int result = SafeNativeMethods.BitBlt(targetDC, destinationX, destinationY, destWidth, destHeight, 
        //            //                                              screenDC, sourceX, sourceY, unchecked((int) copyPixelOperation));

        //            //        //a zero result indicates a win32 exception has been thrown
        //            //        if (result == 0) {
        //            //            throw new Win32Exception();
        //            //        }
        //            //    }
        //            //    finally {
        //            //        this.ReleaseHdc();
        //            //    }
        //            //}
        //        }

        //        /*
        //         * Manipulate the current transform
        //         *
        //         * @notes For get methods, we return copies of our internal objects.
        //         *  For set methods, we make copies of the objects passed in.
        //         */

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.ResetTransform"]/*' />
        //        /// <devdoc>
        //        ///    Resets the world transform to identity.
        //        /// </devdoc>
        //        public void ResetTransform()
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipResetWorldTransform(new HandleRef(this, this.NativeGraphics));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.MultiplyTransform"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Multiplies the <see cref='System.Drawing.Drawing2D.Matrix'/> that
        //        ///       represents the world transform and <paramref term="matrix"/>.
        //        ///    </para>
        //        /// </devdoc>
        //        public void MultiplyTransform(Matrix matrix)
        //        {
        //            MultiplyTransform(matrix, MatrixOrder.Prepend);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.MultiplyTransform1"]/*' />
        //        /// <devdoc>
        //        ///    Multiplies the <see cref='System.Drawing.Drawing2D.Matrix'/> that
        //        ///    represents the world transform and <paramref term="matrix"/>.
        //        /// </devdoc>
        //        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        //        {
        //            if (matrix == null)
        //            {
        //                throw new ArgumentNullException("matrix");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipMultiplyWorldTransform(new HandleRef(this, this.NativeGraphics),
        //                                                            new HandleRef(matrix, matrix.nativeMatrix),
        //                                                            order);
        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.TranslateTransform"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void TranslateTransform(float dx, float dy)
        //        {
        //            TranslateTransform(dx, dy, MatrixOrder.Prepend);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.TranslateTransform1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipTranslateWorldTransform(new HandleRef(this, this.NativeGraphics), dx, dy, order);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        // can be called during the creation of NativeGraphics
        //        /*private void TranslateTransform(float dx, float dy, MatrixOrder order, IntPtr nativeGraphics) {
        //            int status = SafeNativeMethods.Gdip.GdipTranslateWorldTransform(new HandleRef(this, nativeGraphics), dx, dy, order);

        //            if (status != SafeNativeMethods.Gdip.Ok) {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }*/

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.ScaleTransform"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void ScaleTransform(float sx, float sy)
        //        {
        //            ScaleTransform(sx, sy, MatrixOrder.Prepend);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.ScaleTransform1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipScaleWorldTransform(new HandleRef(this, this.NativeGraphics), sx, sy, order);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.RotateTransform"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void RotateTransform(float angle)
        //        {
        //            RotateTransform(angle, MatrixOrder.Prepend);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.RotateTransform1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void RotateTransform(float angle, MatrixOrder order)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipRotateWorldTransform(new HandleRef(this, this.NativeGraphics), angle, order);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /*
        //         * Transform points in the current graphics context
        //         */
        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.TransformPoints"]/*' />
        //        /// <devdoc>
        //        /// </devdoc
        //        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void TransformPoints(CoordinateSpace destSpace,
        //                                     CoordinateSpace srcSpace,
        //                                     PointF[] pts)
        //        {
        //            if (pts == null)
        //            {
        //                throw new ArgumentNullException("pts");
        //            }

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
        //            int status = SafeNativeMethods.Gdip.GdipTransformPoints(new HandleRef(this, this.NativeGraphics), unchecked((int)destSpace),
        //                                                     unchecked((int)srcSpace), buf, pts.Length);

        //            try
        //            {
        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                // must do an in-place copy because we only have a reference
        //                PointF[] newPts = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(buf, pts.Length);

        //                for (int i = 0; i < pts.Length; i++)
        //                {
        //                    pts[i] = newPts[i];
        //                }
        //            }
        //            finally
        //            {
        //                Marshal.FreeHGlobal(buf);
        //            }
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.TransformPoints1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void TransformPoints(CoordinateSpace destSpace,
        //                                    CoordinateSpace srcSpace,
        //                                    Point[] pts)
        //        {
        //            if (pts == null)
        //            {
        //                throw new ArgumentNullException("pts");
        //            }

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
        //            int status = SafeNativeMethods.Gdip.GdipTransformPointsI(new HandleRef(this, this.NativeGraphics), unchecked((int)destSpace),
        //                                                      unchecked((int)srcSpace), buf, pts.Length);

        //            try
        //            {
        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                Point[] newPts = SafeNativeMethods.Gdip.ConvertGPPOINTArray(buf, pts.Length);

        //                for (int i = 0; i < pts.Length; i++)
        //                {
        //                    pts[i] = newPts[i];
        //                }
        //            }
        //            finally
        //            {
        //                Marshal.FreeHGlobal(buf);
        //            }
        //        }

        public Color GetNearestColor(Color color)
        {
            return color;
            //int nearest = color.ToArgb();

            //int status = SafeNativeMethods.Gdip.GdipGetNearestColor(new HandleRef(this, this.NativeGraphics), ref nearest);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //return Color.FromArgb(nearest);
        }

        //        /*
        //         * Vector drawing methods
        //         *
        //         * @notes Do we need a set of methods that take
        //         *  integer coordinate parameters?
        //         */

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawLine"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a line connecting the two
        //        ///       specified points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");

        //            int status = SafeNativeMethods.Gdip.GdipDrawLine(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x1, y1, x2, y2);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawLine1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a line connecting the two
        //        ///       specified points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        //        {
        //            DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawLines"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a series of line segments that
        //        ///       connect an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawLines(Pen pen, PointF[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawLines(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen),
        //                                               new HandleRef(this, buf), points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }


        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawLine2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a line connecting the two
        //        ///       specified points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");

        //            int status = SafeNativeMethods.Gdip.GdipDrawLineI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x1, y1, x2, y2);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawLine3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a line connecting the two
        //        ///       specified points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawLine(Pen pen, Point pt1, Point pt2)
        //        {
        //            DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawLines1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a series of line segments that connect an array of
        //        ///       points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawLines(Pen pen, Point[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawLinesI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen),
        //                                                new HandleRef(this, buf), points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawArc"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws an arc from the specified ellipse.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawArc(Pen pen, float x, float y, float width, float height,
        //                            float startAngle, float sweepAngle)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");

        //            int status = SafeNativeMethods.Gdip.GdipDrawArc(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x, y,
        //                                             width, height, startAngle, sweepAngle);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawArc1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws an arc from the specified
        //        ///       ellipse.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        //        {
        //            DrawArc(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawArc2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws an arc from the specified ellipse.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawArc(Pen pen, int x, int y, int width, int height,
        //                            int startAngle, int sweepAngle)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");

        //            int status = SafeNativeMethods.Gdip.GdipDrawArcI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x, y,
        //                                              width, height, startAngle, sweepAngle);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawArc3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws an arc from the specified ellipse.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        //        {
        //            DrawArc(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawBezier"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a cubic bezier curve defined by
        //        ///       four ordered pairs that represent points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2,
        //                               float x3, float y3, float x4, float y4)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");

        //            int status = SafeNativeMethods.Gdip.GdipDrawBezier(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x1, y1,
        //                                                x2, y2, x3, y3, x4, y4);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawBezier1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a cubic bezier curve defined by
        //        ///       four points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        //        {
        //            DrawBezier(pen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawBeziers"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a series of cubic Bezier curves
        //        ///       from an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawBeziers(Pen pen, PointF[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawBeziers(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen),
        //                                                 new HandleRef(this, buf), points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawBezier2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a cubic bezier curve defined by four points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
        //        {
        //            DrawBezier(pen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawBeziers1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a series of cubic Bezier curves from an array of
        //        ///       points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawBeziers(Pen pen, Point[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawBeziersI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen),
        //                                                  new HandleRef(this, buf), points.Length);
        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawRectangle"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of a rectangle specified by
        //        ///    <paramref term="rect"/>.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawRectangle(Pen pen, Rectangle rect)
        //        {
        //            DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawRectangle1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of the specified
        //        ///       rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawRectangle(Pen pen, float x, float y, float width, float height)
        //        {
        //            if (pen == null)
        //            {
        //                throw new ArgumentNullException("pen");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipDrawRectangle(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x, y,
        //                                                   width, height);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawRectangle2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of the specified rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawRectangle(Pen pen, int x, int y, int width, int height)
        //        {
        //            if (pen == null)
        //            {
        //                throw new ArgumentNullException("pen");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipDrawRectangleI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x, y, width, height);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawRectangles"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outlines of a series of
        //        ///       rectangles.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawRectangles(Pen pen, RectangleF[] rects)
        //        {
        //            if (pen == null)
        //            {
        //                throw new ArgumentNullException("pen");
        //            }
        //            if (rects == null)
        //            {
        //                throw new ArgumentNullException("rects");
        //            }

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects);
        //            int status = SafeNativeMethods.Gdip.GdipDrawRectangles(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen),
        //                                                    new HandleRef(this, buf), rects.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawRectangles1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outlines of a series of rectangles.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawRectangles(Pen pen, Rectangle[] rects)
        //        {
        //            if (pen == null)
        //            {
        //                throw new ArgumentNullException("pen");
        //            }
        //            if (rects == null)
        //            {
        //                throw new ArgumentNullException("rects");
        //            }

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects);
        //            int status = SafeNativeMethods.Gdip.GdipDrawRectanglesI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen),
        //                                                     new HandleRef(this, buf), rects.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawEllipse"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of an
        //        ///       ellipse defined by a bounding rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawEllipse(Pen pen, RectangleF rect)
        //        {
        //            DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawEllipse1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of an
        //        ///       ellipse defined by a bounding rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");

        //            int status = SafeNativeMethods.Gdip.GdipDrawEllipse(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x, y,
        //                                                 width, height);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawEllipse2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of an ellipse specified by a bounding
        //        ///       rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawEllipse(Pen pen, Rectangle rect)
        //        {
        //            DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawEllipse3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of an ellipse defined by a bounding rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawEllipse(Pen pen, int x, int y, int width, int height)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");

        //            int status = SafeNativeMethods.Gdip.GdipDrawEllipseI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x, y,
        //                                                  width, height);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawPie"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of a pie section
        //        ///       defined by an ellipse and two radial lines.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        //        {
        //            DrawPie(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle,
        //                    sweepAngle);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawPie1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of a pie section
        //        ///       defined by an ellipse and two radial lines.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawPie(Pen pen, float x, float y, float width,
        //                            float height, float startAngle, float sweepAngle)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");

        //            int status = SafeNativeMethods.Gdip.GdipDrawPie(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x, y, width,
        //                                             height, startAngle, sweepAngle);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawPie2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of a pie section defined by an ellipse
        //        ///       and two radial lines.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        //        {
        //            DrawPie(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle,
        //                    sweepAngle);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawPie3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the outline of a pie section defined by an ellipse and two radial
        //        ///       lines.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawPie(Pen pen, int x, int y, int width, int height,
        //                            int startAngle, int sweepAngle)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");

        //            int status = SafeNativeMethods.Gdip.GdipDrawPieI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), x, y, width,
        //                                              height, startAngle, sweepAngle);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawPolygon"]/*' />
        //        /// <devdoc>
        //        ///    Draws the outline of a polygon defined
        //        ///    by an array of points.
        //        /// </devdoc>
        //        public void DrawPolygon(Pen pen, PointF[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawPolygon(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen),
        //                                                 new HandleRef(this, buf), points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawPolygon1"]/*' />
        //        /// <devdoc>
        //        ///    Draws the outline of a polygon defined
        //        ///    by an array of points.
        //        /// </devdoc>
        //        public void DrawPolygon(Pen pen, Point[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawPolygonI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen),
        //                                                  new HandleRef(this, buf), points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawPath"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the lines and curves defined by a
        //        ///    <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawPath(Pen pen, GraphicsPath path)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (path == null)
        //                throw new ArgumentNullException("path");

        //            int status = SafeNativeMethods.Gdip.GdipDrawPath(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen),
        //                                              new HandleRef(path, path.nativePath));

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawCurve"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a curve defined by an array of
        //        ///       points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawCurve(Pen pen, PointF[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawCurve(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                               points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawCurve1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a curve defined by an array of
        //        ///       points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawCurve(Pen pen, PointF[] points, float tension)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawCurve2(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                                points.Length, tension);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawCurve2"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
        //        {
        //            DrawCurve(pen, points, offset, numberOfSegments, 0.5f);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawCurve3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a curve defined by an array of
        //        ///       points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments,
        //                              float tension)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawCurve3(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                                points.Length, offset, numberOfSegments,
        //                                                tension);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawCurve4"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a curve defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawCurve(Pen pen, Point[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawCurveI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                                points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawCurve5"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a curve defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawCurve(Pen pen, Point[] points, float tension)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawCurve2I(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                                 points.Length, tension);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawCurve6"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a curve defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments,
        //                              float tension)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawCurve3I(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                                 points.Length, offset, numberOfSegments,
        //                                                 tension);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawClosedCurve"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a closed curve defined by an
        //        ///       array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawClosedCurve(Pen pen, PointF[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawClosedCurve(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                                     points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawClosedCurve1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a closed curve defined by an
        //        ///       array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawClosedCurve2(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                                      points.Length, tension);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawClosedCurve2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a closed curve defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawClosedCurve(Pen pen, Point[] points)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawClosedCurveI(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                                      points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawClosedCurve3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a closed curve defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode)
        //        {
        //            if (pen == null)
        //                throw new ArgumentNullException("pen");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipDrawClosedCurve2I(new HandleRef(this, this.NativeGraphics), new HandleRef(pen, pen.NativePen), new HandleRef(this, buf),
        //                                                       points.Length, tension);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Clear"]/*' />
        //        /// <devdoc>
        //        ///    Fills the entire drawing surface with the
        //        ///    specified color.
        //        /// </devdoc>
        //        public void Clear(Color color)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipGraphicsClear(new HandleRef(this, this.NativeGraphics), color.ToArgb());

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillRectangle"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a rectangle with a <see cref='System.Drawing.Brush'/>.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillRectangle(Brush brush, RectangleF rect)
        //        {
        //            FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillRectangle1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a rectangle with a
        //        ///    <see cref='System.Drawing.Brush'/>.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        //        {
        //            if (brush == null)
        //            {
        //                throw new ArgumentNullException("brush");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipFillRectangle(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush), x, y,
        //                                                   width, height);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillRectangle2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a rectangle with a <see cref='System.Drawing.Brush'/>.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillRectangle(Brush brush, Rectangle rect)
        //        {
        //            FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillRectangle3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a rectangle with a <see cref='System.Drawing.Brush'/>.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillRectangle(Brush brush, int x, int y, int width, int height)
        //        {
        //            if (brush == null)
        //            {
        //                throw new ArgumentNullException("brush");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipFillRectangleI(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush), x, y, width, height);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        public void FillRectangles(Brush brush, RectangleF[] rects)
        {
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            if (rects == null)
            {
                throw new ArgumentNullException("rects");
            }
            foreach (var item in rects)
            {
                this.FillRectangle(brush, item.Left, item.Top, item.Width, item.Height);
            }
        }
        public void FillRectangles(Brush brush, Rectangle[] rects)
        {
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            if (rects == null)
            {
                throw new ArgumentNullException("rects");
            }
            foreach (var item in rects)
            {
                this.FillRectangle(brush, item.Left, item.Top, item.Width, item.Height);
            }
        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillRectangles1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interiors of a series of rectangles with a <see cref='System.Drawing.Brush'/>.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillRectangles(Brush brush, Rectangle[] rects)
        //        {
        //            if (brush == null)
        //            {
        //                throw new ArgumentNullException("brush");
        //            }
        //            if (rects == null)
        //            {
        //                throw new ArgumentNullException("rects");
        //            }

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects);
        //            int status = SafeNativeMethods.Gdip.GdipFillRectanglesI(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush),
        //                                                     new HandleRef(this, buf), rects.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillPolygon"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a polygon defined
        //        ///       by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillPolygon(Brush brush, PointF[] points)
        //        {
        //            FillPolygon(brush, points, FillMode.Alternate);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillPolygon1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a polygon defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipFillPolygon(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush),
        //                                                 new HandleRef(this, buf), points.Length, unchecked((int)fillMode));

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillPolygon2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a polygon defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillPolygon(Brush brush, Point[] points)
        //        {
        //            FillPolygon(brush, points, FillMode.Alternate);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillPolygon3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a polygon defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipFillPolygonI(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush),
        //                                                  new HandleRef(this, buf), points.Length, unchecked((int)fillMode));

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillEllipse"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of an ellipse
        //        ///       defined by a bounding rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillEllipse(Brush brush, RectangleF rect)
        //        {
        //            FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillEllipse1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of an ellipse defined by a bounding rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillEllipse(Brush brush, float x, float y, float width,
        //                                float height)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");

        //            int status = SafeNativeMethods.Gdip.GdipFillEllipse(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush), x, y,
        //                                                 width, height);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillEllipse2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of an ellipse defined by a bounding rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillEllipse(Brush brush, Rectangle rect)
        //        {
        //            FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillEllipse3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of an ellipse defined by a bounding
        //        ///       rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillEllipse(Brush brush, int x, int y, int width, int height)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");

        //            int status = SafeNativeMethods.Gdip.GdipFillEllipseI(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush), x, y,
        //                                                  width, height);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillPie"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a pie section defined by an ellipse and two radial
        //        ///       lines.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillPie(Brush brush, Rectangle rect, float startAngle,
        //                            float sweepAngle)
        //        {
        //            FillPie(brush, rect.X, rect.Y, rect.Width, rect.Height, startAngle,
        //                    sweepAngle);
        //        }

        //        // float verison
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillPie1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a pie section defined by an ellipse and two radial
        //        ///       lines.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillPie(Brush brush, float x, float y, float width,
        //                            float height, float startAngle, float sweepAngle)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");

        //            int status = SafeNativeMethods.Gdip.GdipFillPie(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush), x, y,
        //                                             width, height, startAngle, sweepAngle);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int verison
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillPie2"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a pie section defined by an ellipse
        //        ///       and two radial lines.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillPie(Brush brush, int x, int y, int width,
        //                            int height, int startAngle, int sweepAngle)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");

        //            int status = SafeNativeMethods.Gdip.GdipFillPieI(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush), x, y,
        //                                              width, height, startAngle, sweepAngle);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillPath"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a path.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillPath(Brush brush, GraphicsPath path)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");
        //            if (path == null)
        //                throw new ArgumentNullException("path");

        //            int status = SafeNativeMethods.Gdip.GdipFillPath(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush),
        //                                              new HandleRef(path, path.nativePath));

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillClosedCurve"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior a closed
        //        ///       curve defined by an
        //        ///       array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillClosedCurve(Brush brush, PointF[] points)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipFillClosedCurve(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush),
        //                                                               new HandleRef(this, buf), points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillClosedCurve1"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the
        //        ///       interior of a closed curve defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode)
        //        {
        //            FillClosedCurve(brush, points, fillmode, 0.5f);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillClosedCurve2"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipFillClosedCurve2(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush),
        //                                                      new HandleRef(this, buf), points.Length,
        //                                                      tension, unchecked((int)fillmode));

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillClosedCurve3"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior a closed curve defined by an array of points.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillClosedCurve(Brush brush, Point[] points)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipFillClosedCurveI(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush),
        //                                                     new HandleRef(this, buf), points.Length);

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillClosedCurve4"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode)
        //        {
        //            FillClosedCurve(brush, points, fillmode, 0.5f);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillClosedCurve5"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");
        //            if (points == null)
        //                throw new ArgumentNullException("points");

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
        //            int status = SafeNativeMethods.Gdip.GdipFillClosedCurve2I(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush),
        //                                                      new HandleRef(this, buf), points.Length,
        //                                                      tension, unchecked((int)fillmode));

        //            Marshal.FreeHGlobal(buf);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.FillRegion"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Fills the interior of a <see cref='System.Drawing.Region'/>.
        //        ///    </para>
        //        /// </devdoc>
        //        public void FillRegion(Brush brush, Region region)
        //        {
        //            if (brush == null)
        //                throw new ArgumentNullException("brush");

        //            if (region == null)
        //                throw new ArgumentNullException("region");

        //            int status = SafeNativeMethods.Gdip.GdipFillRegion(new HandleRef(this, this.NativeGraphics), new HandleRef(brush, brush.NativeBrush),
        //                                                new HandleRef(region, region.nativeRegion));

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /*
        //         * Text drawing methods
        //         *
        //         * @notes Should there be integer versions, also?
        //         */

        //        // Without clipping rectangle
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawString"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws a string with the specified font.
        //        ///    </para>
        //        /// </devdoc>
        //        public void DrawString(String s, Font font, Brush brush, float x, float y)
        //        {
        //            DrawString(s, font, brush, new RectangleF(x, y, 0, 0), null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawString1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawString(String s, Font font, Brush brush, PointF point)
        //        {
        //            DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0, 0), null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawString2"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawString(String s, Font font, Brush brush, float x, float y, StringFormat format)
        //        {
        //            DrawString(s, font, brush, new RectangleF(x, y, 0, 0), format);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawString3"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawString(String s, Font font, Brush brush, PointF point, StringFormat format)
        //        {
        //            DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0, 0), format);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawString4"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawString(String s, Font font, Brush brush, RectangleF layoutRectangle)
        //        {
        //            DrawString(s, font, brush, layoutRectangle, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawString5"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawString(String s, Font font, Brush brush,
        //                               RectangleF layoutRectangle, StringFormat format)
        //        {

        //            if (brush == null)
        //                throw new ArgumentNullException("brush");

        //            if (s == null || s.Length == 0) return;
        //            if (font == null)
        //                throw new ArgumentNullException("font");

        //            GPRECTF grf = new GPRECTF(layoutRectangle);
        //            IntPtr nativeStringFormat = (format == null) ? IntPtr.Zero : format.nativeFormat;
        //            int status = SafeNativeMethods.Gdip.GdipDrawString(new HandleRef(this, this.NativeGraphics), s, s.Length, new HandleRef(font, font.NativeFont), ref grf, new HandleRef(format, nativeStringFormat), new HandleRef(brush, brush.NativeBrush));

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // MeasureString

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.MeasureString"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public SizeF MeasureString(String text, Font font, SizeF layoutArea, StringFormat stringFormat,
        //                                   out int charactersFitted, out int linesFilled)
        //        {
        //            if (text == null || text.Length == 0)
        //            {
        //                charactersFitted = 0;
        //                linesFilled = 0;
        //                return new SizeF(0, 0);
        //            }
        //            if (font == null)
        //            {
        //                throw new ArgumentNullException("font");
        //            }

        //            GPRECTF grfLayout = new GPRECTF(0, 0, layoutArea.Width, layoutArea.Height);
        //            GPRECTF grfboundingBox = new GPRECTF();
        //            int status = SafeNativeMethods.Gdip.GdipMeasureString(new HandleRef(this, this.NativeGraphics), text, text.Length, new HandleRef(font, font.NativeFont), ref grfLayout,
        //                                                   new HandleRef(stringFormat, (stringFormat == null) ? IntPtr.Zero : stringFormat.nativeFormat),
        //                                                   ref grfboundingBox,
        //                                                   out charactersFitted, out linesFilled);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            return grfboundingBox.SizeF;
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.MeasureString1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public SizeF MeasureString(String text, Font font, PointF origin, StringFormat stringFormat)
        //        {
        //            if (text == null || text.Length == 0)
        //                return new SizeF(0, 0);
        //            if (font == null)
        //                throw new ArgumentNullException("font");

        //            GPRECTF grf = new GPRECTF();
        //            GPRECTF grfboundingBox = new GPRECTF();

        //            grf.X = origin.X;
        //            grf.Y = origin.Y;
        //            grf.Width = 0;
        //            grf.Height = 0;

        //            int a, b;
        //            int status = SafeNativeMethods.Gdip.GdipMeasureString(new HandleRef(this, this.NativeGraphics), text, text.Length, new HandleRef(font, font.NativeFont),
        //                ref grf,
        //                new HandleRef(stringFormat, (stringFormat == null) ? IntPtr.Zero : stringFormat.nativeFormat),
        //                ref grfboundingBox, out a, out b);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            return grfboundingBox.SizeF;

        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.MeasureString2"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public SizeF MeasureString(String text, Font font, SizeF layoutArea)
        //        {
        //            return MeasureString(text, font, layoutArea, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.MeasureString3"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public SizeF MeasureString(String text, Font font, SizeF layoutArea, StringFormat stringFormat)
        //        {
        //            if (text == null || text.Length == 0)
        //            {
        //                return new SizeF(0, 0);
        //            }

        //            if (font == null)
        //            {
        //                throw new ArgumentNullException("font");
        //            }

        //            GPRECTF grfLayout = new GPRECTF(0, 0, layoutArea.Width, layoutArea.Height);
        //            GPRECTF grfboundingBox = new GPRECTF();

        //            int a, b;
        //            int status = SafeNativeMethods.Gdip.GdipMeasureString(new HandleRef(this, this.NativeGraphics), text, text.Length, new HandleRef(font, font.NativeFont),
        //                ref grfLayout,
        //                new HandleRef(stringFormat, (stringFormat == null) ? IntPtr.Zero : stringFormat.nativeFormat),
        //                ref grfboundingBox, out a, out b);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            return grfboundingBox.SizeF;

        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.MeasureString4"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public SizeF MeasureString(String text, Font font)
        //        {
        //            return MeasureString(text, font, new SizeF(0, 0));
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.MeasureString5"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public SizeF MeasureString(String text, Font font, int width)
        //        {
        //            return MeasureString(text, font, new SizeF(width, 999999));
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.MeasureString6"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public SizeF MeasureString(String text, Font font, int width, StringFormat format)
        //        {
        //            return MeasureString(text, font, new SizeF(width, 999999), format);
        //        }

        public Region[] MeasureCharacterRanges(String text, Font font, RectangleF layoutRect,
                                          StringFormat stringFormat)
        {
            throw new NotSupportedException();
            //if (text == null || text.Length == 0)
            //{
            //    return new Region[] { };
            //}
            //if (font == null)
            //{
            //    throw new ArgumentNullException("font");
            //}

            //int count;
            //int status = SafeNativeMethods.Gdip.GdipGetStringFormatMeasurableCharacterRangeCount(new HandleRef(stringFormat, (stringFormat == null) ? IntPtr.Zero : stringFormat.nativeFormat)
            //                                                                        , out count);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //IntPtr[] gpRegions = new IntPtr[count];

            //GPRECTF grf = new GPRECTF(layoutRect);

            //Region[] regions = new Region[count];

            //for (int f = 0; f < count; f++)
            //{
            //    regions[f] = new Region();
            //    gpRegions[f] = (IntPtr)regions[f].nativeRegion;
            //}

            //status = SafeNativeMethods.Gdip.GdipMeasureCharacterRanges(new HandleRef(this, this.NativeGraphics), text, text.Length, new HandleRef(font, font.NativeFont), ref grf,
            //                                             new HandleRef(stringFormat, (stringFormat == null) ? IntPtr.Zero : stringFormat.nativeFormat),
            //                                             count, gpRegions);


            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //return regions;
        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawIcon"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [ResourceExposure(ResourceScope.None)]
        //        [ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
        //        public void DrawIcon(Icon icon, int x, int y)
        //        {
        //            if (icon == null)
        //            {
        //                throw new ArgumentNullException("icon");
        //            }

        //            if (this.backingImage != null)
        //            {
        //                // we don't call the icon directly because we want to stay in GDI+ all the time
        //                // to avoid alpha channel interop issues between gdi and gdi+
        //                // so we do icon.ToBitmap() and then we call DrawImage. this is probably slower...
        //                this.DrawImage(icon.ToBitmap(), x, y);
        //            }
        //            else
        //            {
        //                icon.Draw(this, x, y);
        //            }

        //        }
        public void DrawIconUnstretched(Icon icon, Rectangle rect)
        {
            throw new NotSupportedException();
        }
        public void DrawIcon(Icon icon, Rectangle targetRect)
        {
            if (icon == null)
            {
                throw new ArgumentNullException("icon");
            }
            throw new NotSupportedException();
            //if (this.backingImage != null)
            //{
            //    // we don't call the icon directly because we want to stay in GDI+ all the time
            //    // to avoid alpha channel interop issues between gdi and gdi+
            //    // so we do icon.ToBitmap() and then we call DrawImage. this is probably slower...
            //    this.DrawImage(icon.ToBitmap(), targetRect);
            //}
            //else
            //{
            //    icon.Draw(this, targetRect);
            //}
        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawIconUnstretched"]/*' />
        //        /// <devdoc>
        //        ///    Draws this image to a graphics object.  The drawing command originates on the graphics
        //        ///    object, but a graphics object generally has no idea how to render a given image.  So,
        //        ///    it passes the call to the actual image.  This version stretches the image to the given
        //        ///    dimensions and allows the user to specify a rectangle within the image to draw.
        //        /// </devdoc>
        //        [ResourceExposure(ResourceScope.None)]
        //        [ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
        //        public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
        //        {
        //            if (icon == null)
        //            {
        //                throw new ArgumentNullException("icon");
        //            }

        //            if (this.backingImage != null)
        //            {
        //                this.DrawImageUnscaled(icon.ToBitmap(), targetRect);
        //            }
        //            else
        //            {
        //                icon.DrawUnstretched(this, targetRect);
        //            }
        //        }

        //        /**
        //         * Draw images (both bitmap and vector)
        //         */
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Draws the specified image at the
        //        ///       specified location.
        //        ///    </para>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, PointF point)
        //        {
        //            DrawImage(image, point.X, point.Y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, float x, float y)
        //        {
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int status = SafeNativeMethods.Gdip.GdipDrawImage(new HandleRef(this, this.NativeGraphics), new HandleRef(image, image.nativeImage),
        //                                               x, y);

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage2"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, RectangleF rect)
        //        {
        //            DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage3"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, float x, float y, float width,
        //                              float height)
        //        {
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int status = SafeNativeMethods.Gdip.GdipDrawImageRect(new HandleRef(this, this.NativeGraphics),
        //                                                   new HandleRef(image, image.nativeImage),
        //                                                   x, y,
        //                                                   width, height);

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage4"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, Point point)
        //        {
        //            DrawImage(image, point.X, point.Y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage5"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, int x, int y)
        //        {
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int status = SafeNativeMethods.Gdip.GdipDrawImageI(new HandleRef(this, this.NativeGraphics), new HandleRef(image, image.nativeImage),
        //                                                x, y);

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage6"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, Rectangle rect)
        //        {
        //            DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage7"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, int x, int y, int width, int height)
        //        {
        //            if (image == null)
        //            {
        //                throw new ArgumentNullException("image");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipDrawImageRectI(new HandleRef(this, this.NativeGraphics),
        //                                                    new HandleRef(image, image.nativeImage),
        //                                                    x, y,
        //                                                    width, height);

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }



        //        // unscaled versions
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImageUnscaled"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawImageUnscaled(Image image, Point point)
        //        {
        //            DrawImage(image, point.X, point.Y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImageUnscaled1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawImageUnscaled(Image image, int x, int y)
        //        {
        //            DrawImage(image, x, y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImageUnscaled2"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawImageUnscaled(Image image, Rectangle rect)
        //        {
        //            DrawImage(image, rect.X, rect.Y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImageUnscaled3"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
        //        {
        //            DrawImage(image, x, y);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImageUnscaledAndClipped"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void DrawImageUnscaledAndClipped(Image image, Rectangle rect)
        //        {
        //            if (image == null)
        //            {
        //                throw new ArgumentNullException("image");
        //            }

        //            int width = Math.Min(rect.Width, image.Width);
        //            int height = Math.Min(rect.Height, image.Height);

        //            //We could put centering logic here too for the case when the image is smaller than the rect
        //            DrawImage(image, rect, 0, 0, width, height, GraphicsUnit.Pixel);
        //        }

        //        /*
        //         * Affine or perspective blt
        //         *  destPoints.Length = 3: rect => parallelogram
        //         *      destPoints[0] <=> top-left corner of the source rectangle
        //         *      destPoints[1] <=> top-right corner
        //         *       destPoints[2] <=> bottom-left corner
        //         *  destPoints.Length = 4: rect => quad
        //         *      destPoints[3] <=> bottom-right corner
        //         *
        //         *  @notes Perspective blt only works for bitmap images.
        //         */
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage8"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, PointF[] destPoints)
        //        {
        //            if (destPoints == null)
        //                throw new ArgumentNullException("destPoints");
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int count = destPoints.Length;

        //            if (count != 3 && count != 4)
        //                throw new ArgumentException(SR.GetString(SR.GdiplusDestPointsInvalidLength));

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
        //            int status = SafeNativeMethods.Gdip.GdipDrawImagePoints(new HandleRef(this, this.NativeGraphics),
        //                                                     new HandleRef(image, image.nativeImage),
        //                                                     new HandleRef(this, buf), count);

        //            Marshal.FreeHGlobal(buf);

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage9"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, Point[] destPoints)
        //        {
        //            if (destPoints == null)
        //                throw new ArgumentNullException("destPoints");
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int count = destPoints.Length;

        //            if (count != 3 && count != 4)
        //                throw new ArgumentException(SR.GetString(SR.GdiplusDestPointsInvalidLength));

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
        //            int status = SafeNativeMethods.Gdip.GdipDrawImagePointsI(new HandleRef(this, this.NativeGraphics),
        //                                                      new HandleRef(image, image.nativeImage),
        //                                                      new HandleRef(this, buf), count);

        //            Marshal.FreeHGlobal(buf);

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        /*
        //         * We need another set of methods similar to the ones above
        //         * that take an additional Rectangle parameter to specify the
        //         * portion of the source image to be drawn.
        //         */
        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage10"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, float x, float y, RectangleF srcRect,
        //                              GraphicsUnit srcUnit)
        //        {
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int status = SafeNativeMethods.Gdip.GdipDrawImagePointRect(
        //                                                       new HandleRef(this, this.NativeGraphics),
        //                                                       new HandleRef(image, image.nativeImage),
        //                                                       x,
        //                                                       y,
        //                                                       srcRect.X,
        //                                                       srcRect.Y,
        //                                                       srcRect.Width,
        //                                                       srcRect.Height,
        //                                                       unchecked((int)srcUnit));

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage11"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, int x, int y, Rectangle srcRect,
        //                              GraphicsUnit srcUnit)
        //        {
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int status = SafeNativeMethods.Gdip.GdipDrawImagePointRectI(
        //                                                        new HandleRef(this, this.NativeGraphics),
        //                                                        new HandleRef(image, image.nativeImage),
        //                                                        x,
        //                                                        y,
        //                                                        srcRect.X,
        //                                                        srcRect.Y,
        //                                                        srcRect.Width,
        //                                                        srcRect.Height,
        //                                                        unchecked((int)srcUnit));

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage12"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect,
        //                              GraphicsUnit srcUnit)
        //        {
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int status = SafeNativeMethods.Gdip.GdipDrawImageRectRect(
        //                                                      new HandleRef(this, this.NativeGraphics),
        //                                                      new HandleRef(image, image.nativeImage),
        //                                                      destRect.X,
        //                                                      destRect.Y,
        //                                                      destRect.Width,
        //                                                      destRect.Height,
        //                                                      srcRect.X,
        //                                                      srcRect.Y,
        //                                                      srcRect.Width,
        //                                                      srcRect.Height,
        //                                                      unchecked((int)srcUnit),
        //                                                      NativeMethods.NullHandleRef,
        //                                                      null,
        //                                                      NativeMethods.NullHandleRef
        //                                                      );

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage13"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect,
        //                              GraphicsUnit srcUnit)
        //        {
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int status = SafeNativeMethods.Gdip.GdipDrawImageRectRectI(
        //                                                       new HandleRef(this, this.NativeGraphics),
        //                                                       new HandleRef(image, image.nativeImage),
        //                                                       destRect.X,
        //                                                       destRect.Y,
        //                                                       destRect.Width,
        //                                                       destRect.Height,
        //                                                       srcRect.X,
        //                                                       srcRect.Y,
        //                                                       srcRect.Width,
        //                                                       srcRect.Height,
        //                                                       unchecked((int)srcUnit),
        //                                                       NativeMethods.NullHandleRef,
        //                                                       null,
        //                                                       NativeMethods.NullHandleRef);

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        //        // float version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.DrawImage14"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect,
        //                              GraphicsUnit srcUnit)
        //        {
        //            if (destPoints == null)
        //                throw new ArgumentNullException("destPoints");
        //            if (image == null)
        //                throw new ArgumentNullException("image");

        //            int count = destPoints.Length;

        //            if (count != 3 && count != 4)
        //                throw new ArgumentException(SR.GetString(SR.GdiplusDestPointsInvalidLength));

        //            IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);

        //            int status = SafeNativeMethods.Gdip.GdipDrawImagePointsRect(
        //                                                        new HandleRef(this, this.NativeGraphics),
        //                                                        new HandleRef(image, image.nativeImage),
        //                                                        new HandleRef(this, buf),
        //                                                        destPoints.Length,
        //                                                        srcRect.X,
        //                                                        srcRect.Y,
        //                                                        srcRect.Width,
        //                                                        srcRect.Height,
        //                                                        unchecked((int)srcUnit),
        //                                                        NativeMethods.NullHandleRef,
        //                                                        null,
        //                                                        NativeMethods.NullHandleRef);

        //            Marshal.FreeHGlobal(buf);

        //            //ignore emf metafile error
        //            IgnoreMetafileErrors(image, ref status);

        //            //check error status sensitive to TS problems
        //            CheckErrorStatus(status);
        //        }

        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect,
                              GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, null, 0);
        }

        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect,
                              GraphicsUnit srcUnit, ImageAttributes imageAttr,
                              DrawImageAbort callback)
        {
            DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, 0);
        }

        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect,
                              GraphicsUnit srcUnit, ImageAttributes imageAttr,
                              DrawImageAbort callback, int callbackData)
        {
            throw new NotSupportedException();

        }

        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            DrawImage(image, destPoints, srcRect, srcUnit, null, null, 0);

        }

        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect,
                              GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, null, 0);
        }

        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect,
                              GraphicsUnit srcUnit, ImageAttributes imageAttr,
                              DrawImageAbort callback)
        {
            DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, 0);
        }

        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect,
                              GraphicsUnit srcUnit, ImageAttributes imageAttr,
                              DrawImageAbort callback, int callbackData)
        {
            throw new NotSupportedException();
            //if (destPoints == null)
            //    throw new ArgumentNullException("destPoints");
            //if (image == null)
            //    throw new ArgumentNullException("image");

            //int count = destPoints.Length;

            //if (count != 3 && count != 4)
            //    throw new ArgumentException(SR.GetString(SR.GdiplusDestPointsInvalidLength));

            //IntPtr buf = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);

            //int status = SafeNativeMethods.Gdip.GdipDrawImagePointsRectI(
            //                                            new HandleRef(this, this.NativeGraphics),
            //                                            new HandleRef(image, image.nativeImage),
            //                                            new HandleRef(this, buf),
            //                                            destPoints.Length,
            //                                            srcRect.X,
            //                                            srcRect.Y,
            //                                            srcRect.Width,
            //                                            srcRect.Height,
            //                                            unchecked((int)srcUnit),
            //                                            new HandleRef(imageAttr, (imageAttr != null ? imageAttr.nativeImageAttributes : IntPtr.Zero)),
            //                                            callback,
            //                                            new HandleRef(null, (IntPtr)callbackData));

            //Marshal.FreeHGlobal(buf);

            ////ignore emf metafile error
            //IgnoreMetafileErrors(image, ref status);

            ////check error status sensitive to TS problems
            //CheckErrorStatus(status);

        }

        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY,
                              float srcWidth, float srcHeight, GraphicsUnit srcUnit)
        {
            DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, null);
        }

        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY,
                              float srcWidth, float srcHeight, GraphicsUnit srcUnit,
                              ImageAttributes imageAttrs)
        {
            this.DrawImage(image, destRect, new RectangleF(srcX, srcY, srcWidth, srcHeight), srcUnit);
            //DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, null);
        }
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY,
                              float srcWidth, float srcHeight, GraphicsUnit srcUnit,
                              ImageAttributes imageAttrs, DrawImageAbort callback)
        {
            DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback, IntPtr.Zero);
        }


        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY,
                              float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs,
                              DrawImageAbort callback, IntPtr callbackData)
        {
            throw new NotSupportedException();
            //if (image == null)
            //    throw new ArgumentNullException("image");

            //int status = SafeNativeMethods.Gdip.GdipDrawImageRectRect(
            //                                           new HandleRef(this, this.NativeGraphics),
            //                                           new HandleRef(image, image.nativeImage),
            //                                           destRect.X,
            //                                           destRect.Y,
            //                                           destRect.Width,
            //                                           destRect.Height,
            //                                           srcX,
            //                                           srcY,
            //                                           srcWidth,
            //                                           srcHeight,
            //                                           unchecked((int)srcUnit),
            //                                           new HandleRef(imageAttrs, (imageAttrs != null ? imageAttrs.nativeImageAttributes : IntPtr.Zero)),
            //                                           callback,
            //                                           new HandleRef(null, callbackData));

            ////ignore emf metafile error
            //IgnoreMetafileErrors(image, ref status);

            ////check error status sensitive to TS problems
            //CheckErrorStatus(status);
        }

        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY,
                              int srcWidth, int srcHeight, GraphicsUnit srcUnit)
        {
            DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, null);
        }
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY,
                              int srcWidth, int srcHeight, GraphicsUnit srcUnit,
                              ImageAttributes imageAttr)
        {
            DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr, null);
        }

        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY,
                              int srcWidth, int srcHeight, GraphicsUnit srcUnit,
                              ImageAttributes imageAttr, DrawImageAbort callback)
        {
            DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr, callback, IntPtr.Zero);
        }

        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY,
                              int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs,
                              DrawImageAbort callback, IntPtr callbackData)
        {
            throw new NotSupportedException();
            //if (image == null)
            //    throw new ArgumentNullException("image");

            //int status = SafeNativeMethods.Gdip.GdipDrawImageRectRectI(
            //                                           new HandleRef(this, this.NativeGraphics),
            //                                           new HandleRef(image, image.nativeImage),
            //                                           destRect.X,
            //                                           destRect.Y,
            //                                           destRect.Width,
            //                                           destRect.Height,
            //                                           srcX,
            //                                           srcY,
            //                                           srcWidth,
            //                                           srcHeight,
            //                                           unchecked((int)srcUnit),
            //                                           new HandleRef(imageAttrs, (imageAttrs != null ? imageAttrs.nativeImageAttributes : IntPtr.Zero)),
            //                                           callback,
            //                                           new HandleRef(null, callbackData));

            ////ignore emf metafile error
            //IgnoreMetafileErrors(image, ref status);

            ////check error status sensitive to TS problems
            //CheckErrorStatus(status);
        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, PointF destPoint,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destPoint, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile1"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, PointF destPoint,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destPoint, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile2"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        public void EnumerateMetafile(Metafile metafile, PointF destPoint,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestPoint(new HandleRef(this, this.NativeGraphics),
        //                                                                new HandleRef(metafile, mf),
        //                                                                new GPPOINTF(destPoint),
        //                                                                callback,
        //                                                                new HandleRef(null, callbackData),
        //                                                                new HandleRef(imageAttr, ia));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile3"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Point destPoint,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destPoint, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile4"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Point destPoint,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destPoint, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile5"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        public void EnumerateMetafile(Metafile metafile, Point destPoint,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestPointI(new HandleRef(this, this.NativeGraphics),
        //                                                                 new HandleRef(metafile, mf),
        //                                                                 new GPPOINT(destPoint),
        //                                                                 callback,
        //                                                                 new HandleRef(null, callbackData),
        //                                                                 new HandleRef(imageAttr, ia));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile6"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, RectangleF destRect,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destRect, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile7"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, RectangleF destRect,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destRect, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile8"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        public void EnumerateMetafile(Metafile metafile, RectangleF destRect,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            GPRECTF grf = new GPRECTF(destRect);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestRect(
        //                                                                new HandleRef(this, this.NativeGraphics),
        //                                                                new HandleRef(metafile, mf),
        //                                                                ref grf,
        //                                                                callback,
        //                                                                new HandleRef(null, callbackData),
        //                                                                new HandleRef(imageAttr, ia));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile9"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Rectangle destRect,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destRect, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile10"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Rectangle destRect,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destRect, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile11"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        public void EnumerateMetafile(Metafile metafile, Rectangle destRect,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            GPRECT gprect = new GPRECT(destRect);
        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestRectI(new HandleRef(this, this.NativeGraphics),
        //                                                                new HandleRef(metafile, mf),
        //                                                                ref gprect,
        //                                                                callback,
        //                                                                new HandleRef(null, callbackData),
        //                                                                new HandleRef(imageAttr, ia));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile12"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destPoints, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile13"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destPoints, callback, IntPtr.Zero, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile14"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            if (destPoints == null)
        //                throw new ArgumentNullException("destPoints");
        //            if (destPoints.Length != 3)
        //            {
        //                throw new ArgumentException(SR.GetString(SR.GdiplusDestPointsInvalidParallelogram));
        //            }

        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            IntPtr points = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestPoints(new HandleRef(this, this.NativeGraphics),
        //                                                                 new HandleRef(metafile, mf),
        //                                                                 points,
        //                                                                 destPoints.Length,
        //                                                                 callback,
        //                                                                 new HandleRef(null, callbackData),
        //                                                                 new HandleRef(imageAttr, ia));
        //            Marshal.FreeHGlobal(points);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }


        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile15"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Point[] destPoints,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destPoints, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile16"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Point[] destPoints,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destPoints, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile17"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        public void EnumerateMetafile(Metafile metafile, Point[] destPoints,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            if (destPoints == null)
        //                throw new ArgumentNullException("destPoints");
        //            if (destPoints.Length != 3)
        //            {
        //                throw new ArgumentException(SR.GetString(SR.GdiplusDestPointsInvalidParallelogram));
        //            }

        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            IntPtr points = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestPointsI(new HandleRef(this, this.NativeGraphics),
        //                                                                  new HandleRef(metafile, mf),
        //                                                                  points,
        //                                                                  destPoints.Length,
        //                                                                  callback,
        //                                                                  new HandleRef(null, callbackData),
        //                                                                  new HandleRef(imageAttr, ia));
        //            Marshal.FreeHGlobal(points);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile18"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, PointF destPoint,
        //                                      RectangleF srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile19"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, PointF destPoint,
        //                                      RectangleF srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile20"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        public void EnumerateMetafile(Metafile metafile, PointF destPoint,
        //                                      RectangleF srcRect, GraphicsUnit unit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            GPRECTF grf = new GPRECTF(srcRect);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestPoint(new HandleRef(this, this.NativeGraphics),
        //                                                                       new HandleRef(metafile, mf),
        //                                                                       new GPPOINTF(destPoint),
        //                                                                       ref grf,
        //                                                                       unchecked((int)unit),
        //                                                                       callback,
        //                                                                       new HandleRef(null, callbackData),
        //                                                                       new HandleRef(imageAttr, ia));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile21"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Point destPoint,
        //                                      Rectangle srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile22"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Point destPoint,
        //                                      Rectangle srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile23"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        public void EnumerateMetafile(Metafile metafile, Point destPoint,
        //                                      Rectangle srcRect, GraphicsUnit unit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            GPPOINT gppoint = new GPPOINT(destPoint);
        //            GPRECT gprect = new GPRECT(srcRect);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestPointI(new HandleRef(this, this.NativeGraphics),
        //                                                                        new HandleRef(metafile, mf),
        //                                                                        gppoint,
        //                                                                        ref gprect,
        //                                                                        unchecked((int)unit),
        //                                                                        callback,
        //                                                                        new HandleRef(null, callbackData),
        //                                                                        new HandleRef(imageAttr, ia));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile24"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, RectangleF destRect,
        //                                      RectangleF srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile25"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, RectangleF destRect,
        //                                      RectangleF srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile26"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        public void EnumerateMetafile(Metafile metafile, RectangleF destRect,
        //                                      RectangleF srcRect, GraphicsUnit unit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            GPRECTF grfdest = new GPRECTF(destRect);
        //            GPRECTF grfsrc = new GPRECTF(srcRect);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestRect(
        //                                                                         new HandleRef(this, this.NativeGraphics),
        //                                                                         new HandleRef(metafile, mf),
        //                                                                         ref grfdest,
        //                                                                         ref grfsrc,
        //                                                                         unchecked((int)unit),
        //                                                                         callback,
        //                                                                         new HandleRef(null, callbackData),
        //                                                                         new HandleRef(imageAttr, ia));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile27"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Rectangle destRect,
        //                                      Rectangle srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile28"]/*' />
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Rectangle destRect,
        //                                      Rectangle srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile29"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Rectangle destRect,
        //                                      Rectangle srcRect, GraphicsUnit unit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            GPRECT gpDest = new GPRECT(destRect);
        //            GPRECT gpSrc = new GPRECT(srcRect);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestRectI(new HandleRef(this, this.NativeGraphics),
        //                                                                       new HandleRef(metafile, mf),
        //                                                                       ref gpDest,
        //                                                                       ref gpSrc,
        //                                                                       unchecked((int)unit),
        //                                                                       callback,
        //                                                                       new HandleRef(null, callbackData),
        //                                                                       new HandleRef(imageAttr, ia));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile30"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints,
        //                                      RectangleF srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile31"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints,
        //                                      RectangleF srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile32"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints,
        //                                      RectangleF srcRect, GraphicsUnit unit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            if (destPoints == null)
        //                throw new ArgumentNullException("destPoints");
        //            if (destPoints.Length != 3)
        //            {
        //                throw new ArgumentException(SR.GetString(SR.GdiplusDestPointsInvalidParallelogram));
        //            }

        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            IntPtr buffer = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);

        //            GPRECTF grf = new GPRECTF(srcRect);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestPoints(new HandleRef(this, this.NativeGraphics),
        //                                                                        new HandleRef(metafile, mf),
        //                                                                        buffer,
        //                                                                        destPoints.Length,
        //                                                                        ref grf,
        //                                                                        unchecked((int)unit),
        //                                                                        callback,
        //                                                                        new HandleRef(null, callbackData),
        //                                                                        new HandleRef(imageAttr, ia));
        //            Marshal.FreeHGlobal(buffer);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }


        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile33"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Point[] destPoints,
        //                                      Rectangle srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback)
        //        {
        //            EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, IntPtr.Zero);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile34"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Point[] destPoints,
        //                                      Rectangle srcRect, GraphicsUnit srcUnit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData)
        //        {
        //            EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, callbackData, null);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EnumerateMetafile35"]/*' />
        //        /// <devdoc>
        //        ///    <para>[To be supplied.]</para>
        //        /// </devdoc>
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        //        public void EnumerateMetafile(Metafile metafile, Point[] destPoints,
        //                                      Rectangle srcRect, GraphicsUnit unit,
        //                                      EnumerateMetafileProc callback, IntPtr callbackData,
        //                                      ImageAttributes imageAttr)
        //        {
        //            if (destPoints == null)
        //                throw new ArgumentNullException("destPoints");
        //            if (destPoints.Length != 3)
        //            {
        //                throw new ArgumentException(SR.GetString(SR.GdiplusDestPointsInvalidParallelogram));
        //            }

        //            IntPtr mf = (metafile == null ? IntPtr.Zero : metafile.nativeImage);
        //            IntPtr ia = (imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes);

        //            IntPtr buffer = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);

        //            GPRECT gpSrc = new GPRECT(srcRect);

        //            int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestPointsI(new HandleRef(this, this.NativeGraphics),
        //                                                                         new HandleRef(metafile, mf),
        //                                                                         buffer,
        //                                                                         destPoints.Length,
        //                                                                         ref gpSrc,
        //                                                                         unchecked((int)unit),
        //                                                                         callback,
        //                                                                         new HandleRef(null, callbackData),
        //                                                                         new HandleRef(imageAttr, ia));
        //            Marshal.FreeHGlobal(buffer);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }


        //        /*
        //         * Clipping region operations
        //         *
        //         * @notes Simply incredible redundancy here.
        //         */

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SetClip"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void SetClip(Graphics g)
        //        {
        //            SetClip(g, CombineMode.Replace);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SetClip1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void SetClip(Graphics g, CombineMode combineMode)
        //        {
        //            if (g == null)
        //            {
        //                throw new ArgumentNullException("g");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipSetClipGraphics(new HandleRef(this, this.NativeGraphics), new HandleRef(g, g.NativeGraphics), combineMode);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SetClip2"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void SetClip(Rectangle rect)
        //        {
        //            SetClip(rect, CombineMode.Replace);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SetClip3"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void SetClip(Rectangle rect, CombineMode combineMode)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipSetClipRectI(new HandleRef(this, this.NativeGraphics), rect.X, rect.Y,
        //                                                  rect.Width, rect.Height, combineMode);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SetClip4"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void SetClip(RectangleF rect)
        //        {
        //            SetClip(rect, CombineMode.Replace);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SetClip5"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void SetClip(RectangleF rect, CombineMode combineMode)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipSetClipRect(new HandleRef(this, this.NativeGraphics), rect.X, rect.Y,
        //                                                 rect.Width, rect.Height, combineMode);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SetClip6"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void SetClip(GraphicsPath path)
        //        {
        //            SetClip(path, CombineMode.Replace);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SetClip7"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void SetClip(GraphicsPath path, CombineMode combineMode)
        //        {
        //            if (path == null)
        //            {
        //                throw new ArgumentNullException("path");
        //            }
        //            int status = SafeNativeMethods.Gdip.GdipSetClipPath(new HandleRef(this, this.NativeGraphics), new HandleRef(path, path.nativePath), combineMode);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.SetClip8"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void SetClip(Region region, CombineMode combineMode)
        //        {
        //            if (region == null)
        //            {
        //                throw new ArgumentNullException("region");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipSetClipRegion(new HandleRef(this, this.NativeGraphics), new HandleRef(region, region.nativeRegion), combineMode);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.IntersectClip"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        public void IntersectClip(Region reg)
        {
            throw new NotSupportedException();
        }
        public void IntersectClip(Rectangle rect)
        {
            throw new NotSupportedException();
            //int status = SafeNativeMethods.Gdip.GdipSetClipRectI(new HandleRef(this, this.NativeGraphics), rect.X, rect.Y,
            //                                      rect.Width, rect.Height, CombineMode.Intersect);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}
        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.IntersectClip1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void IntersectClip(RectangleF rect)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipSetClipRect(new HandleRef(this, this.NativeGraphics), rect.X, rect.Y,
        //                                                 rect.Width, rect.Height, CombineMode.Intersect);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.IntersectClip2"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void IntersectClip(Region region)
        //        {
        //            if (region == null)
        //                throw new ArgumentNullException("region");

        //            int status = SafeNativeMethods.Gdip.GdipSetClipRegion(new HandleRef(this, this.NativeGraphics), new HandleRef(region, region.nativeRegion),
        //                                                   CombineMode.Intersect);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        public void ExcludeClip(Region region)
        {
            throw new NotSupportedException();
        }
        public void ExcludeClip(Rectangle rect)
        {
            throw new NotSupportedException();
            //int status = SafeNativeMethods.Gdip.GdipSetClipRectI(new HandleRef(this, this.NativeGraphics), rect.X, rect.Y,
            //                                      rect.Width, rect.Height, CombineMode.Exclude);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}
        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.ExcludeClip1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void ExcludeClip(Region region)
        //        {
        //            if (region == null)
        //                throw new ArgumentNullException("region");

        //            int status = SafeNativeMethods.Gdip.GdipSetClipRegion(new HandleRef(this, this.NativeGraphics),
        //                                                   new HandleRef(region, region.nativeRegion),
        //                                                   CombineMode.Exclude);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.ResetClip"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void ResetClip()
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipResetClip(new HandleRef(this, this.NativeGraphics));

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.TranslateClip"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void TranslateClip(float dx, float dy)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipTranslateClip(new HandleRef(this, this.NativeGraphics), dx, dy);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.TranslateClip1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void TranslateClip(int dx, int dy)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipTranslateClip(new HandleRef(this, this.NativeGraphics), dx, dy);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <devdoc>
        //        ///     Combines current Graphics context with all previous contexts.
        //        ///     When BeginContainer() is called, a copy of the current context is pushed into the GDI+ context stack, it keeps track of the
        //        ///     absolute clipping and transform but reset the public properties so it looks like a brand new context.
        //        ///     When Save() is called, a copy of the current context is also pushed in the GDI+ stack but the public clipping and transform
        //        ///     properties are not reset (cumulative).  Consecutive Save context are ignored with the exception of the top one which contains 
        //        ///     all previous information.
        //        ///     The return value is an object array where the first element contains the cumulative clip region and the second the cumulative
        //        ///     translate transform matrix.
        //        ///     WARNING: This method is for internal FX support only.
        //        ///     </devdoc>
        //        //[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand, Name = "System.Windows.Forms", PublicKey = "0x00000000000000000400000000000000")]
        //        [EditorBrowsable(EditorBrowsableState.Never)]
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public object GetContextInfo()
        //        {
        //            Region cumulClip = this.Clip;           // current context clip.
        //            Matrix cumulTransform = this.Transform; // current context transform.
        //            PointF currentOffset = PointF.Empty;    // offset of current context.
        //            PointF totalOffset = PointF.Empty;      // absolute coord offset of top context.

        //            if (!cumulTransform.IsIdentity)
        //            {
        //                float[] elements = cumulTransform.Elements;
        //                currentOffset.X = elements[4];
        //                currentOffset.Y = elements[5];
        //            }

        //            GraphicsContext context = this.previousContext;

        //            while (context != null)
        //            {
        //                if (!context.TransformOffset.IsEmpty)
        //                {
        //                    cumulTransform.Translate(context.TransformOffset.X, context.TransformOffset.Y);
        //                }

        //                if (!currentOffset.IsEmpty)
        //                {
        //                    // The location of the GDI+ clip region is relative to the coordinate origin after any translate transform
        //                    // has been applied.  We need to intersect regions using the same coordinate origin relative to the previous
        //                    // context.
        //                    cumulClip.Translate(currentOffset.X, currentOffset.Y);
        //                    totalOffset.X += currentOffset.X;
        //                    totalOffset.Y += currentOffset.Y;
        //                }

        //                if (context.Clip != null)
        //                {
        //                    cumulClip.Intersect(context.Clip);
        //                }

        //                currentOffset = context.TransformOffset;

        //                // Ignore subsequent cumulative contexts.
        //                do
        //                {
        //                    context = context.Previous;

        //                    if (context == null || !context.Next.IsCumulative)
        //                    {
        //                        break;
        //                    }
        //                } while (context.IsCumulative);
        //            }

        //            if (!totalOffset.IsEmpty)
        //            {
        //                // We need now to reset the total transform in the region so when calling Region.GetHRgn(Graphics)
        //                // the HRegion is properly offset by GDI+ based on the total offset of the graphics object.
        //                cumulClip.Translate(-totalOffset.X, -totalOffset.Y);
        //            }

        //            return new object[] { cumulClip, cumulTransform };
        //        }


        //        /**
        //         *  GetClip region from graphics context
        //         */
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Clip"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        private Region _Clip = null;
        public Region Clip
        {
            get
            {
                return this._Clip;
                //Region region = new Region();

                //int status = SafeNativeMethods.Gdip.GdipGetClip(new HandleRef(this, this.NativeGraphics), new HandleRef(region, region.nativeRegion));

                //if (status != SafeNativeMethods.Gdip.Ok)
                //{
                //    throw SafeNativeMethods.Gdip.StatusException(status);
                //}

                //return region;
            }
            set
            {
                this._Clip = value;
                //SetClip(value, CombineMode.Replace);
            }
        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.ClipBounds"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public RectangleF ClipBounds
        //        {
        //            get
        //            {
        //                GPRECTF rect = new GPRECTF();

        //                int status = SafeNativeMethods.Gdip.GdipGetClipBounds(new HandleRef(this, this.NativeGraphics), ref rect);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return rect.ToRectangleF();
        //            }
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.IsClipEmpty"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public bool IsClipEmpty
        //        {
        //            get
        //            {
        //                int isEmpty;

        //                int status = SafeNativeMethods.Gdip.GdipIsClipEmpty(new HandleRef(this, this.NativeGraphics), out isEmpty);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return isEmpty != 0;
        //            }
        //        }

        //        /**
        //         * Hit testing operations
        //         */
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.VisibleClipBounds"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public RectangleF VisibleClipBounds
        //        {
        //            get
        //            {
        //                if (this.PrintingHelper != null)
        //                {
        //                    PrintPreviewGraphics ppGraphics = this.PrintingHelper as PrintPreviewGraphics;
        //                    if (ppGraphics != null)
        //                    {
        //                        return ppGraphics.VisibleClipBounds;
        //                    }
        //                }

        //                GPRECTF rect = new GPRECTF();

        //                int status = SafeNativeMethods.Gdip.GdipGetVisibleClipBounds(new HandleRef(this, this.NativeGraphics), ref rect);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return rect.ToRectangleF();
        //            }
        //        }

        //        /**
        //          * @notes atomic operation?  status needed?
        //          */
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.IsVisibleClipEmpty"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public bool IsVisibleClipEmpty
        //        {
        //            get
        //            {
        //                int isEmpty;

        //                int status = SafeNativeMethods.Gdip.GdipIsVisibleClipEmpty(new HandleRef(this, this.NativeGraphics), out isEmpty);

        //                if (status != SafeNativeMethods.Gdip.Ok)
        //                {
        //                    throw SafeNativeMethods.Gdip.StatusException(status);
        //                }

        //                return isEmpty != 0;
        //            }
        //        }


        public bool IsVisible(int x, int y)
        {
            return IsVisible(new Point(x, y));
        }

        public bool IsVisible(Point point)
        {
            return true;
            //int isVisible;

            //int status = SafeNativeMethods.Gdip.GdipIsVisiblePointI(new HandleRef(this, this.NativeGraphics), point.X, point.Y, out isVisible);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //return isVisible != 0;
        }

        public bool IsVisible(float x, float y)
        {
            return IsVisible(new PointF(x, y));
        }

        public bool IsVisible(PointF point)
        {
            return true;
            //int isVisible;

            //int status = SafeNativeMethods.Gdip.GdipIsVisiblePoint(new HandleRef(this, this.NativeGraphics), point.X, point.Y, out isVisible);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //return isVisible != 0;
        }

        public bool IsVisible(int x, int y, int width, int height)
        {
            return IsVisible(new Rectangle(x, y, width, height));
        }

        public bool IsVisible(Rectangle rect)
        {
            return true;
            //int isVisible;

            //int status = SafeNativeMethods.Gdip.GdipIsVisibleRectI(new HandleRef(this, this.NativeGraphics), rect.X, rect.Y,
            //                                        rect.Width, rect.Height, out isVisible);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //return isVisible != 0;
        }

        public bool IsVisible(float x, float y, float width, float height)
        {
            return IsVisible(new RectangleF(x, y, width, height));
        }

        public bool IsVisible(RectangleF rect)
        {
            return true;
            //int isVisible;

            //int status = SafeNativeMethods.Gdip.GdipIsVisibleRect(new HandleRef(this, this.NativeGraphics), rect.X, rect.Y,
            //                                       rect.Width, rect.Height, out isVisible);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //return isVisible != 0;
        }

        //        /// <devdoc>
        //        ///     Saves the current context into the context stack.
        //        /// </devdoc>
        //        private void PushContext(GraphicsContext context)
        //        {
        //            Debug.Assert(context != null && context.State != 0, "GraphicsContext object is null or not valid.");

        //            if (this.previousContext != null)
        //            {
        //                // Push context.
        //                context.Previous = this.previousContext;
        //                this.previousContext.Next = context;
        //            }
        //            this.previousContext = context;
        //        }

        //        /// <devdoc>
        //        ///     Pops all contexts from the specified one included.  The specified context is becoming the current context.
        //        /// </devdoc>
        //        private void PopContext(int currentContextState)
        //        {
        //            Debug.Assert(this.previousContext != null, "Trying to restore a context when the stack is empty");
        //            GraphicsContext context = this.previousContext;

        //            while (context != null)
        //            {
        //                if (context.State == currentContextState)
        //                {
        //                    this.previousContext = context.Previous;
        //                    // Pop all contexts up the stack.
        //                    context.Dispose(); // This will dipose all context object up the stack.
        //                    return;
        //                }
        //                context = context.Previous;
        //            }
        //            Debug.Fail("Warning: context state not found!");
        //        }

        //        /**
        //         * Save/restore graphics state
        //         */
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Save"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public GraphicsState Save()
        //        {
        //            GraphicsContext context = new GraphicsContext(this);
        //            int state = 0;

        //            int status = SafeNativeMethods.Gdip.GdipSaveGraphics(new HandleRef(this, this.NativeGraphics), out state);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                context.Dispose();
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            context.State = state;
        //            context.IsCumulative = true;
        //            PushContext(context);

        //            return new GraphicsState(state);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.Restore"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void Restore(GraphicsState gstate)
        //        {
        //            int status = SafeNativeMethods.Gdip.GdipRestoreGraphics(new HandleRef(this, this.NativeGraphics), gstate.nativeState);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            PopContext(gstate.nativeState);
        //        }

        //        /*
        //         * Begin and end container drawing
        //         */
        //        // float version

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.BeginContainer"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)
        //        {
        //            return null;
        //            //GraphicsContext context = new GraphicsContext(this);
        //            //int state = 0;

        //            //GPRECTF dstf = dstrect.ToGPRECTF();
        //            //GPRECTF srcf = srcrect.ToGPRECTF();

        //            //int status = SafeNativeMethods.Gdip.GdipBeginContainer(new HandleRef(this, this.NativeGraphics), ref dstf,
        //            //                                        ref srcf, unchecked((int) unit), out state);

        //            //if (status != SafeNativeMethods.Gdip.Ok) {
        //            //    context.Dispose();
        //            //    throw SafeNativeMethods.Gdip.StatusException(status);
        //            //}

        //            //context.State = state;
        //            //PushContext(context);

        //            //return new GraphicsContainer(state);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.BeginContainer1"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public GraphicsContainer BeginContainer()
        //        {
        //            GraphicsContext context = new GraphicsContext(this);
        //            int state = 0;

        //            int status = SafeNativeMethods.Gdip.GdipBeginContainer2(new HandleRef(this, this.NativeGraphics), out state);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                context.Dispose();
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            context.State = state;
        //            PushContext(context);

        //            return new GraphicsContainer(state);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.EndContainer"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void EndContainer(GraphicsContainer container)
        //        {
        //            if (container == null)
        //            {
        //                throw new ArgumentNullException("container");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipEndContainer(new HandleRef(this, this.NativeGraphics), container.nativeGraphicsContainer);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            PopContext(container.nativeGraphicsContainer);
        //        }

        //        // int version
        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.BeginContainer2"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        [ResourceExposure(ResourceScope.Process)]
        //        [ResourceConsumption(ResourceScope.Process)]
        //        public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)
        //        {
        //            GraphicsContext context = new GraphicsContext(this);
        //            int state = 0;

        //            GPRECT gpDest = new GPRECT(dstrect);
        //            GPRECT gpSrc = new GPRECT(srcrect);

        //            int status = SafeNativeMethods.Gdip.GdipBeginContainerI(new HandleRef(this, this.NativeGraphics), ref gpDest,
        //                                                     ref gpSrc, unchecked((int)unit), out state);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                context.Dispose();
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }

        //            context.State = state;
        //            PushContext(context);

        //            return new GraphicsContainer(state);
        //        }

        //        /// <include file='doc\Graphics.uex' path='docs/doc[@for="Graphics.AddMetafileComment"]/*' />
        //        /// <devdoc>
        //        /// </devdoc>
        //        public void AddMetafileComment(byte[] data)
        //        {
        //            if (data == null)
        //            {
        //                throw new ArgumentNullException("data");
        //            }

        //            int status = SafeNativeMethods.Gdip.GdipComment(new HandleRef(this, this.NativeGraphics), data.Length, data);

        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        public static IntPtr GetHalftonePalette()
        {
            throw new NotSupportedException();
            //if (halftonePalette == IntPtr.Zero)
            //{
            //    lock (syncObject)
            //    {
            //        if (halftonePalette == IntPtr.Zero)
            //        {
            //            if (!(Environment.OSVersion.Platform == System.PlatformID.Win32Windows))
            //            {
            //                AppDomain.CurrentDomain.DomainUnload += new EventHandler(OnDomainUnload);
            //            }
            //            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnDomainUnload);

            //            halftonePalette = SafeNativeMethods.Gdip.GdipCreateHalftonePalette();
            //        }
            //    }
            //}
            //return halftonePalette;
        }

        //        //This will get called for ProcessExit in case for WinNT..
        //        //This will get called for ProcessExit AND DomainUnLoad for Win9X...
        //        //
        //        [PrePrepareMethod]
        //        private static void OnDomainUnload(object sender, EventArgs e)
        //        {
        //            if (halftonePalette != IntPtr.Zero)
        //            {
        //                SafeNativeMethods.IntDeleteObject(new HandleRef(null, halftonePalette));
        //                halftonePalette = IntPtr.Zero;
        //            }
        //        }


        //        /// <devdoc>
        //        ///     GDI+ will return a 'generic error' with specific win32 last error codes when
        //        ///     a terminal server session has been closed, minimized, etc...  We don't want 
        //        ///     to throw when this happens, so we'll guard against this by looking at the
        //        ///     'last win32 error code' and checking to see if it is either 1) access denied
        //        ///     or 2) proc not found and then ignore it.
        //        /// 
        //        ///     The problem is that when you lock the machine, the secure desktop is enabled and 
        //        ///     rendering fails which is expected (since the app doesn't have permission to draw 
        //        ///     on the secure desktop). Not sure if there's anything you can do, short of catching 
        //        ///     the desktop switch message and absorbing all the exceptions that get thrown while 
        //        ///     it's the secure desktop.
        //        /// </devdoc>
        //        private void CheckErrorStatus(int status)
        //        {
        //            if (status != SafeNativeMethods.Gdip.Ok)
        //            {
        //                // Generic error from GDI+ can be GenericError or Win32Error.
        //                if (status == SafeNativeMethods.Gdip.GenericError || status == SafeNativeMethods.Gdip.Win32Error)
        //                {
        //                    int error = Marshal.GetLastWin32Error();
        //                    if (error == SafeNativeMethods.ERROR_ACCESS_DENIED || error == SafeNativeMethods.ERROR_PROC_NOT_FOUND ||
        //                            //here, we'll check to see if we are in a term. session...
        //                            (((UnsafeNativeMethods.GetSystemMetrics(NativeMethods.SM_REMOTESESSION) & 0x00000001) != 0) && (error == 0)))
        //                    {
        //                        return;
        //                    }
        //                }

        //                //legitimate error, throw our status exception
        //                throw SafeNativeMethods.Gdip.StatusException(status);
        //            }
        //        }

        //        /// <devdoc>
        //        ///     GDI+ will return a 'generic error' when we attempt to draw an Emf 
        //        ///     image with width/height == 1.  Here, we will hack around this by 
        //        ///     resetting the errorstatus.  Note that we don't do simple arg checking
        //        ///     for height || width == 1 here because transforms can be applied to
        //        ///     the Graphics object making it difficult to identify this scenario.
        //        /// </devdoc>
        //        private void IgnoreMetafileErrors(Image image, ref int errorStatus)
        //        {
        //            if (errorStatus != SafeNativeMethods.Gdip.Ok)
        //            {
        //                if (image.RawFormat.Equals(ImageFormat.Emf))
        //                {
        //                    errorStatus = SafeNativeMethods.Gdip.Ok;
        //                }
        //            }
        //        }
    }
}
