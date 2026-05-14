using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.IO;
using DCSoft.Common;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

// 袁永福到此一游

namespace DCSoft.Drawing
{
    
    /// <summary>
    /// 画布对象
    /// </summary>
#if ! DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(false)]
#if ! DOTNETCORE
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
#endif
#endif
    public class DCGraphics : IDisposable
    {
       

        /// <summary> 
        /// 根据图片创造这个画布对象
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <returns>画布对象</returns>

        public static DCGraphics FromImage(Image img)
        {
            DCGraphics g = new DCGraphics(Graphics.FromImage(img));
            g.AutoDisposeNativeGraphics = true;
            return g;
        }

        protected DCGraphics() { }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="g"></param>
        public DCGraphics(Graphics g)
        {
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            this._Graphis = g;
            this._PageUnitFast = g.PageUnit;
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="g"></param>
        public DCGraphics(DCGraphics g)
        {
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            this._AutoDisposeImageForPDF = g._AutoDisposeImageForPDF;
            this._AutoDisposeNativeGraphics = g._AutoDisposeNativeGraphics;
            this._ClipVersion = g._ClipVersion;
            this._Graphis = g._Graphis;
            this._PageUnitFast = g._PageUnitFast;
        }


        public IntPtr GetHdc()
        {
            if (this._Graphis != null)
            {
                return this._Graphis.GetHdc();
            }
            else
            {
                return IntPtr.Zero;
            }
        }
        public void ReleaseHdc( IntPtr hdc )
        {
            if( this._Graphis != null )
            {
                this._Graphis.ReleaseHdc(hdc);
            }
        }

        private Graphics _Graphis = null;

        /// <summary>
        /// 原始的画布对象
        /// </summary>
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public Graphics NativeGraphics
        {
            get
            {
                return _Graphis;
            }
        }


       
        

        public bool IsPDFMode
        {
            get
            {
                return false;
            }
        }
      

        public void Clear(Color c)
        {
            if (this._Graphis != null)
            {
                this._Graphis.Clear(c);
            }
            //if (this._Recorder != null)
            //{
            //    this._Recorder.Clear(c);
            //}
        }

        public virtual void AddPage( float pageWidth , float pageHeight )
        {

        }
        public virtual void EndPage()
        {

        }

        public virtual void ResetClip()
        {


            if (_Graphis != null)
            {
                _Graphis.ResetClip();
            }

        }

        /// <summary>
        /// 获得字体高度
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float GetFontHeight(Font font)
        {
            if (this._Graphis != null)
            {
                return font.GetHeight(this._Graphis);
            }
            else if (this.GraphisForMeasure != null)
            {
                return font.GetHeight(this.GraphisForMeasure);
            }
            else
            {
                return font.GetHeight(this.DpiY);
            }
        }

        /// <summary>
        /// 获得字体高度
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
       // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual float GetFontHeight(XFontValue font)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            bool hasFixed = false ;
            try
            {
                if (this._Graphis != null)
                {
                    return font.GetHeight(this._Graphis);
                }
                else if (this.GraphisForMeasure != null)
                {
                    //this.GraphisForMeasure.PageUnit = this.PageUnit;
                    return font.GetHeight(this.GraphisForMeasure);
                }
                else
                {
                    return font.GetHeight(this.DpiY);
                }
            }
            catch( System.Exception ext)
            {
                if (font.FixDisposedValue())
                {
                    hasFixed = true;
                }
                else
                {
                    //DCConsole.Default.WriteLine("GetFontHeight报错，清空字体缓存。" + Environment.NewLine + ext.ToString());
                    XFontValue.ClearBuffer();
                    throw;
                }
            }
            if(hasFixed)
            {
                if (this._Graphis != null)
                {
                    return font.GetHeight(this._Graphis);
                }
                else if (this.GraphisForMeasure != null)
                {
                    return font.GetHeight(this.GraphisForMeasure);
                }
                else
                {
                    return font.GetHeight(this.DpiY);
                }
            }
            return font.Size;
        }

        //  [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public PixelOffsetMode PixelOffsetMode
        {
            get
            {
                if (_Graphis == null)
                {
                    return PixelOffsetMode.Default;
                }
                else
                {
                    return _Graphis.PixelOffsetMode;
                }
            }
            set
            {
                //if (this._Recorder != null)
                //{
                //    this._Recorder.PixelOffsetMode = value;
                //}
                if (_Graphis != null)
                {
                    _Graphis.PixelOffsetMode = value;
                }
            }
        }


        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TextRenderingHint TextRenderingHint
        {
            get
            {
                if (_Graphis == null)
                {
                    return TextRenderingHint.SystemDefault;
                }
                else
                {
                    return _Graphis.TextRenderingHint;
                }
            }
            set
            {
                //if (this._Recorder != null)
                //{
                //    this._Recorder.TextRenderingHint = value;
                //}
                if (_Graphis != null)
                {
                    _Graphis.TextRenderingHint = value;
                }
            }
        }

        //  [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public InterpolationMode InterpolationMode
        {
            get
            {
                if (this._Graphis == null)
                {
                    return InterpolationMode.Default;
                }
                else
                {
                    return this._Graphis.InterpolationMode;
                }
            }
            set
            {
                //if (this._Recorder != null)
                //{
                //    this._Recorder.InterpolationMode = value;
                //}
                if (_Graphis != null)
                {
                    _Graphis.InterpolationMode = value;
                }
            }
        }

        private GraphicsUnit _PageUnitFast = GraphicsUnit.Document;
        /// <summary>
        /// 快速获取度量单位
        /// </summary>
        public GraphicsUnit PageUnitFast
        {
            get
            {
                return _PageUnitFast;
            }
        }


        //  [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual GraphicsUnit PageUnit
        {
            get
            {
                if (this._Graphis == null)
                {
                    return GraphicsUnit.Pixel;
                }
                else
                {
                    return this._Graphis.PageUnit;
                }
            }
            set
            {
               

                if (this._Graphis != null)
                {
                    this._Graphis.PageUnit = value;
                }
                this._PageUnitFast = value;
               
            }
        }

        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public SmoothingMode SmoothingMode
        {
            get
            {
                if (this._Graphis == null)
                {
                    return System.Drawing.Drawing2D.SmoothingMode.Default;
                }
                else
                {
                    return this._Graphis.SmoothingMode;
                }
            }
            set
            {
               
                if (this._Graphis != null)
                {
                    this._Graphis.SmoothingMode = value;
                }
               
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float DpiX
        {
            get
            {
                if (_Graphis == null)
                {
                    return 96;
                }
                else
                {
                    return this._Graphis.DpiX;
                }
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float DpiY
        {
            get
            {
                if (_Graphis == null)
                {
                    return 96;
                }
                else
                {
                    return this._Graphis.DpiY;
                }
            }
        }


        //private Matrix _Transform = null;

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void TransformPoints(PointF[] pts)
        {
            if (this._MyMartrix == null)
            {
                this.Transform.TransformPoints(pts);
            }
            else
            {
                this._MyMartrix.TransformPoints(pts);
            }
        }

        private MyMartrixClass _MyMartrix = null;
        public MyMartrixClass MyMartrix
        {
            get
            {
                return this._MyMartrix;
            }
        }
        /// <summary>
        ///  是否处于LINUX运行模式
        /// </summary>
        public static bool LinuxMode = DCSoft.Common.DebugHelper.IsLinuxOrUnixPlatform;// System.Environment.OSVersion.Platform == PlatformID.MacOSX || System.Environment.OSVersion.Platform == PlatformID.Unix;

        

        /// <summary>
        /// 转换画笔线条宽度
        /// </summary>
        /// <param name="vValue">旧宽度</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>新宽度</returns>
        public static float ConvertPenWidth(
           float vValue,
           System.Drawing.GraphicsUnit OldUnit,
           System.Drawing.GraphicsUnit NewUnit)
        {
            if (LinuxMode)
            {
                return vValue;
            }
            else
            {
                return GraphicsUnitConvert.Convert(vValue, OldUnit, NewUnit);
            }
        }

        public bool AutoSetInnerMatrix()
        {
            //var os = System.Environment.OSVersion.Platform;
            if (LinuxMode )// os == PlatformID.MacOSX || os == PlatformID.Unix )
            {
                this._MyMartrix = new MyMartrixClass(this._Graphis.Transform);
                this._Graphis.ResetTransform();
                //this._Graphis.ResetClip();
                return true;
            }
            return false;
        }
        public void CleanInnerMatrix()
        {
            if (this._MyMartrix != null)
            {
                this._Graphis.Transform = this._MyMartrix.CreateMatrix();
                this._MyMartrix = null;
            }
        }
        [System.Runtime.InteropServices.ComVisible(false)]
        public class MyMartrixClass
        {

            private float a = 1;
            private float b = 0;
            private float c = 0;
            private float d = 1;
            private float e = 0;
            private float f = 0;
            public System.Drawing.Drawing2D.Matrix CreateMatrix()
            {
                return new Matrix(this.a, this.b, this.c, this.d, this.e, this.f);
            }
            internal bool IsDefault { get { return a == 1 && b == 0 && c == 0 && d == 1 && e == 0 && f == 0; } }
            //internal IList<object> Data { get { return new List<object> { a, b, c, d, e, f }; } }
            //double Determinant { get { return a * d - b * c; } }

            internal MyMartrixClass(System.Drawing.Drawing2D.Matrix m)
            {
                if (m != null)
                {
                    var ms = m.Elements;
                    this.a = ms[0];
                    this.b = ms[1];
                    this.c = ms[2];
                    this.d = ms[3];
                    this.e = ms[4];
                    this.f = ms[5];
                }
            }

            internal void SetMatrix(System.Drawing.Drawing2D.Matrix m)
            {
                if (m != null)
                {
                    var ms = m.Elements;
                    this.a = ms[0];
                    this.b = ms[1];
                    this.c = ms[2];
                    this.d = ms[3];
                    this.e = ms[4];
                    this.f = ms[5];
                }
            }

            internal MyMartrixClass(float a, float b, float c, float d, float e, float f)
            {
                this.a = a;
                this.b = b;
                this.c = c;
                this.d = d;
                this.e = e;
                this.f = f;
            }

            internal MyMartrixClass() : this(1, 0, 0, 1, 0, 0)
            {
            }


            public void SetScaleTransform(float sx, float sy)
            {
                var m = this.CreateMatrix();
                m.Scale(sx, sy);
                this.SetMatrix(m);
                m.Dispose();
            }



            public PointF Transform(PointF point)
            {
                return Transform(point.X, point.Y);
            }
            public void TransformPoints(PointF[] points)
            {
                int length = points.Length;
                //PointF[] result = new PointF[length];
                for (int i = 0; i < length; i++)
                {
                    points[i] = Transform(points[i]);
                }
                //return result;
            }

            public RectangleF Transform(RectangleF rect)
            {
                var p1 = Transform(rect.Left, rect.Top);
                var p2 = Transform(rect.Right, rect.Bottom);
                return new RectangleF(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            }

            public Rectangle Transform(Rectangle rect)
            {
                var p1 = Transform(rect.Left, rect.Top);
                var p2 = Transform(rect.Right, rect.Bottom);
                return new Rectangle((int)p1.X, (int)p1.Y, (int)(p2.X - p1.X), (int)(p2.Y - p1.Y));
            }

            public void SetTransform(float dx, float dy)
            {
                var m = this.CreateMatrix();
                m.Translate(dx, dy);
                this.SetMatrix(m);
                m.Dispose();
            }
            public RectangleF Transform(float left, float top, float width, float height)
            {
                var p1 = Transform(left, top);
                var p2 = Transform(left + width, top + height);
                return new RectangleF(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            }

            public void TransformRectangleF(ref float left, ref float top, ref float width, ref float height)
            {
                var p1 = Transform(left, top);
                var p2 = Transform(left + width, top + height);
                left = p1.X;
                top = p1.Y;
                width = p2.X - p1.X;
                height = p2.Y - p1.Y;
            }


            public PointF Transform(float x, float y)
            {
                return new PointF(x * a + y * c + e, x * b + y * d + f);
            }

            public void Transform(ref float x, ref float y)
            {
                x = x * a + y * c + e;
                y = x * b + y * d + f;
            }
            public void Transform(ref int x, ref int y)
            {
                x = (int)(x * a + y * c + e);
                y = (int)(x * b + y * d + f);
            }

        }

        public System.Drawing.PointF TransformPoint( System.Drawing.PointF p )
        {
            var ps = new PointF[] { p };
            this.TransformPoints(ps);
            return ps[0];
        }
        public void TransformPoint(ref float x , ref float y )
        {
            var ps = new PointF[] { new PointF( x , y ) };
            this.TransformPoints(ps);
            x = ps[0].X;
            y = ps[0].Y;
        }

        public void UnTransformPoint(ref float x, ref float y)
        {
            var m = new DCMatrix(this.Transform);
            m.UnTransformPoint(ref x, ref y);
        }

        
        public virtual System.Drawing.Drawing2D.Matrix Transform
        {
            get
            {
                if (_Graphis != null)
                {
                    return _Graphis.Transform;
                }
                return null;
            }
            set
            {
                
                if (_Graphis != null)
                {
                    _Graphis.Transform = value;
                }
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void TranslateTransform(float x, float y, MatrixOrder order)
        {
           
            if (_Graphis != null)
            {
                _Graphis.TranslateTransform(x, y, order);
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.SetMatrix(_Graphis.Transform);
                }
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void ResetTransform()
        {
            if (_Graphis != null)
            {
                _Graphis.ResetTransform();
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void RotateTransform(float angle)
        {

            if (_Graphis != null)
            {
                _Graphis.RotateTransform(angle);
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.SetMatrix(_Graphis.Transform);
                }
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void TranslateTransform(float x, float y)
        {

            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.SetTransform(x, y);
                    //this._MyMartrix.SetMatrix(_Graphis.Transform);
                }
                else
                {
                    this._Graphis.TranslateTransform(x, y);
                }
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void ScaleTransform(float sx, float sy)
        {
            if (this._Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.SetScaleTransform(sx, sy);
                }
                else
                {
                    this._Graphis.ScaleTransform(sx, sy);
                }
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPolygon(Color c, PointF[] ps)
        {
            DrawPolygon(GraphicsObjectBuffer.GetPen(c), ps);
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPolygon(Color c, float lineWidth, PointF[] ps)
        {
            using (Pen p = new Pen(c, lineWidth))
            {
                DrawPolygon(p, ps);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPolygon(Color c, float lineWidth, DashStyle lineStyle, PointF[] ps)
        {
            using (Pen p = new Pen(c, lineWidth))
            {
                p.DashStyle = lineStyle;
                DrawPolygon(p, ps);
            }
        }
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPolygon(Pen p, PointF[] ps)
        {
            if (_Graphis != null)
            {
                _Graphis.DrawPolygon(p, ps);
            }
        }

        public void DrawEllipse(Color c, float lineWidth, DashStyle lineStyle, RectangleF rect)
        {
            if (lineWidth == 1 && lineStyle == DashStyle.Solid)
            {
                DrawEllipse(c, rect);
            }
            else
            {
                using( var p = new Pen(  c, lineWidth ))
                {
                    p.DashStyle = lineStyle;
                    DrawEllipse(p, rect);
                }
            }
        }

        public virtual void DrawEllipse(System.Drawing.Color c , RectangleF rect)
        {
            if( c == Color.Black)
            {
                DrawEllipse(Pens.Black, rect);
            }
            else if( c == Color.White)
            {
                DrawEllipse(Pens.White, rect);
            }
            else
            {
                using( var p = new Pen( c ))
                {
                    DrawEllipse(p, rect);
                }
            }
        }

        public virtual void DrawEllipse(System.Drawing.Pen pen, RectangleF rect)
        {
           

            if (_Graphis != null)
            {
                if (this._MyMartrix == null)
                {
                    this._Graphis.DrawEllipse(pen, rect);
                }
                else
                {
                    var rect2 = this._MyMartrix.Transform(rect);
                    this._Graphis.DrawEllipse(pen, rect2);
                }
            }
           
        }


        public virtual void DrawImage(XImageValue img, float x, float y)
        {
            if(img == null )
            {
                throw new ArgumentNullException("img");
            }
            
            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.Transform(ref x, ref y);
                }
                _Graphis.DrawImage(img.Value, x, y);
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(img.Value);
            }
#endif
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawImage(Image img, float x, float y , byte[] nativeImageData = null)
        {
           

            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.Transform(ref x, ref y);
                }
                _Graphis.DrawImage(img, x, y);
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(img);
            }
#endif
        }

        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]


        public void DrawPDFImage(Image img, RectangleF rect, bool autoDisposeImage, byte[] nativeImageData  )
        {
           
        }

        public virtual void DrawImage(XImageValue img, RectangleF rect )
        {
            this.DrawImage(img.Value, rect , img.GetImageDataRaw());
        }

     

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawImage(Image img, RectangleF rect,  byte[] nativeImageData = null )
        {

            if (_Graphis != null)
            {
                if (this._MyMartrix == null)
                {
                    _Graphis.DrawImage(img, rect);
                }
                else
                {
                    _Graphis.DrawImage(img, this._MyMartrix.Transform(rect));
                }
           }
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(img);
            }
        }

        private bool _AutoDisposeImageForPDF = true;
        /// <summary>
        /// 在输出PDF时自动销毁PDF里面的图片对象
        /// </summary>
        public bool AutoDisposeImageForPDF
        {
            get
            {
                return this._AutoDisposeImageForPDF;
            }
            set
            {
                //if (this._Recorder != null)
                //{
                //    this._Recorder.AutoDisposeImageForPDF = value;
                //}
                this._AutoDisposeImageForPDF = value;
            }
        }

        public virtual void DrawImage(XImageValue img, RectangleF descRect, RectangleF sourceRect, GraphicsUnit unit)
        {
           
            if (_Graphis != null)
            {
                if (this._MyMartrix == null)
                {
                    _Graphis.DrawImage(img.Value , descRect, sourceRect, unit);
                }
                else
                {
                    _Graphis.DrawImage(img.Value, this._MyMartrix.Transform(descRect), sourceRect, unit);
                }
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(img.Value);
            }
#endif
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawImage(Image img, RectangleF descRect, RectangleF sourceRect, GraphicsUnit unit)
        {

            if (_Graphis != null)
            {
                if (this._MyMartrix == null)
                {
                    _Graphis.DrawImage(img, descRect, sourceRect, unit);
                }
                else
                {
                    _Graphis.DrawImage(img, this._MyMartrix.Transform(descRect), sourceRect, unit);
                }
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(img);
            }
#endif
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawImage(Image img, float x, float y, float width, float height)
        {
            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.TransformRectangleF(ref x, ref y, ref width, ref height);
                }
                _Graphis.DrawImage(img, x, y, width, height);
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(img);
            }
#endif
        }

        public virtual void DrawImageUnscaled(XImageValue image, int x, int y)
        {
            DrawImageUnscaled(image.Value, x, y);
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawImageUnscaled(Image image, int x, int y)
        {
            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.Transform(ref x, ref y);
                }
                this._Graphis.DrawImageUnscaled(image, x, y);
            }
            
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(image);
            }
#endif
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLine(Color c, PointF pt1, PointF pt2)
        {
            DrawLine(c, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLine(Color c, float lineWidth, PointF pt1, PointF pt2)
        {
            if( lineWidth == 1 )
            {
                DrawLine(c, pt1.X, pt1.Y, pt2.X, pt2.Y);
            }
            using (var p = new Pen(c, lineWidth))
            {
                this.DrawLine(p, pt1.X, pt1.Y, pt2.X, pt2.Y);
            }
        }
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLine(Color c, float lineWidth, DashStyle lineStyle, PointF pt1, PointF pt2)
        {
            if (lineWidth == 1 && lineStyle == DashStyle.Solid)
            {
                DrawLine(c, pt1.X, pt1.Y, pt2.X, pt2.Y);
            }
            else
            {
                using (var p = new Pen(c, lineWidth))
                {
                    p.DashStyle = lineStyle;
                    this.DrawLine(p, pt1.X, pt1.Y, pt2.X, pt2.Y);
                }
            }
        }


        public void DrawLine(Color c, float lineWidth, float x1, float y1, float x2, float y2)
        {
            if (lineWidth == 1)
            {
                DrawLine(c, x1, y1, x2, y2);
            }
            else
            {
                using (var p = new Pen(c, lineWidth))
                {
                    DrawLine(p, x1, y1, x2, y2);
                }
            }
        }

        public void DrawLine(Color c, float lineWidth, DashStyle lineStyle, float x1, float y1, float x2, float y2)
        {
            if (lineWidth == 1 && lineStyle == DashStyle.Solid)
            {
                DrawLine(c, x1, y1, x2, y2);
            }
            else
            {
                using (var p = new Pen(c, lineWidth))
                {
                    p.DashStyle = lineStyle;
                    DrawLine(p, x1, y1, x2, y2);
                }
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLine(Color c, float x1, float y1, float x2, float y2)
        {
            if( c == Color.Black)
            {
                this.DrawLine(Pens.Black, x1, y1, x2, y2);
            }
            else if( c == Color.White)
            {
                this.DrawLine(Pens.White, x1, y1, x2, y2);
            }
            else
            {
                using( var p = new Pen( c ))
                {
                    this.DrawLine(p, x1, y1, x2, y2);
                }
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.Transform(ref x1, ref y1);
                    this._MyMartrix.Transform(ref x2, ref y2);
                }
                this._Graphis.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLines(XPenStyle pen, PointF[] points)
        {
            DrawLines(pen.Value, points);
        }

        

        //// [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLines(Color lineColor, float lineWidth, DashStyle lineStyle, PointF[] points)
        {
            using(var p = new Pen(lineColor , lineWidth ))
            {
                p.DashStyle = lineStyle;
                DrawLines(p, points);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void DrawLines(Pen pen, PointF[] points)
        {
            if (_Graphis != null)
            {
                this._MyMartrix?.TransformPoints(points);
                this._Graphis.DrawLines(pen, points);
            }
        }


        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLine(XPenStyle pen, PointF pt1, PointF pt2)
        {
            DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLine(XPenStyle pen, float x1, float y1, float x2, float y2)
        {
           
            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.Transform(ref x1, ref y1);
                    this._MyMartrix.Transform(ref x2, ref y2);
                }
                this._Graphis.DrawLine(pen.Value, x1, y1, x2, y2);
            }
          
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawLine(XPenStyle pen, int x1, int y1, int x2, int y2)
        {
            DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        public void FillPolygon(Brush brush, PointF[] points)
        {
            if (_Graphis != null)
            {
                this._MyMartrix?.TransformPoints(points);
                this._Graphis.FillPolygon(brush, points);
            }
        }

        //// [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public void FillPolygon(XBrushStyle brush, PointF[] points)
        //{
        //    if (_Graphis != null)
        //    {
        //        this._MyMartrix?.TransformPoints(points);
        //        using (System.Drawing.Brush b = brush.CreateBrush())
        //        {
        //            this._Graphis.FillPolygon(b, points);
        //        }
        //    }
        //}

      

        //// [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawRectangle(Color color, float lineWidth, DashStyle lineStyle, float x, float y, float width, float height)
        {
            if(lineWidth == 1 && lineStyle == DashStyle.Solid )
            {
                DrawRectangle(color, x, y, width, height);
            }
            else
            {
                using (var p = new System.Drawing.Pen( color , lineWidth ))
                {
                    p.DashStyle = lineStyle;
                    DrawRectangle(p, x, y, width, height);
                }
            }
        }

        public virtual void DrawRectangle(System.Drawing.Color c, float x, float y, float width, float height,float roundRadio=0)
        {
            if (c == Color.Black)
            {
                DrawRectangle(Pens.Black, x, y, width, height , roundRadio);
            }
            else if (c == Color.White)
            {
                DrawRectangle(Pens.White, x, y, width, height ,roundRadio);
            }
            else
            {
                using (var p = new Pen(c))
                {
                    DrawRectangle(p, x, y, width, height,roundRadio);
                }
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void DrawRectangle(Pen pen, float x, float y, float width, float height, float roundRadio=0)
        {
          
            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.TransformRectangleF(ref x, ref y, ref width, ref height);
                }
                this._Graphis.DrawRectangle(pen, x, y, width, height);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawRectangle(Pen pen, Rectangle rect)
        {
            this.DrawRectangle(pen, (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
            
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawRectangle(Pen pen, RectangleF rect)
        {
            DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
          
        }
         
        
        public void DrawRectangle(XPenStyle pen, float x, float y, float width, float height)
        {
            DrawRectangle(pen.Value, x, y, width, height);

            
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawRectangle(XPenStyle pen, Rectangle rect)
        {
            DrawRectangle(pen.Value, (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);

           
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawRectangle(XPenStyle pen, RectangleF rect)
        {
            DrawRectangle(pen.Value, rect.Left, rect.Top, rect.Width, rect.Height);
            
        }
       
        public void DrawString(
            string s,
            XFontValue font,
            Color color,
            RectangleF layoutRectangle,
            StringAlignment alignment,
            StringAlignment lineAlignment)
        {
            DrawString(s, font, color, layoutRectangle, alignment, lineAlignment, true);
        }


        public void DrawString(
            string s,
            Font font,
            Brush b,
            RectangleF layoutRectangle,
            StringAlignment alignment,
            StringAlignment lineAlignment)
        {
            using( var format = new StringFormat())
            {
                format.Alignment = alignment;
                format.LineAlignment = lineAlignment;
                this.DrawString(s, font, b, layoutRectangle, format);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawString(
            string s,
            XFontValue font,
            Color color,
            RectangleF layoutRectangle,
            StringAlignment alignment,
            StringAlignment lineAlignment,
            bool noWrap)
        {
            lock (typeof(XFontValue))
            {
                using (var format = new DCStringFormat())
                {
                    format.Alignment = alignment;
                    format.LineAlignment = lineAlignment;
                    format.FormatFlags = format.FormatFlags | StringFormatFlags.NoClip;
                    if (noWrap)
                    {
                        format.FormatFlags = format.FormatFlags | StringFormatFlags.NoWrap;
                    }
                    DrawString(s, font, color, layoutRectangle, format);
                }
            }
        }

        public virtual void DrawString(string s, XFontValue font, Color txtColor , RectangleF layoutRectangle, DCStringFormat format)
        {

            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    layoutRectangle = this._MyMartrix.Transform(layoutRectangle);
                }
                this._Graphis.DrawString(
                    s, 
                    font.Value,
                    GraphicsObjectBuffer.GetSolidBrush(txtColor),
                    layoutRectangle, 
                    format.Value());
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    layoutRectangle = this._MyMartrix.Transform(layoutRectangle);
                }
                this._Graphis.DrawString(s, font, brush, layoutRectangle, format);
            }
            if (this._LogType == DCGraphicsLogType.Content)
            {
                LogContent(s);
            }
        }

#if WINFORM || DCWriterForWinFormNET6
        public void NativeDrawString(
            string text,
            XFontValue font,
            Rectangle rect,
            Color color,
            int format)// System.Windows.Forms.TextFormatFlags format)
        {
            if (this._Graphis != null)
            {
                System.Windows.Forms.TextRenderer.DrawText(
                                    this._Graphis,
                                    text,
                                    font.Value,
                                    rect,
                                    color,
                                    (System.Windows.Forms.TextFormatFlags)format);
            }
           
        }
#endif

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawString(
            string s,
            XFontValue font,
            Color txtColor,
            RectangleF rect,
            DrawStringFormatExt format)
        {
            lock (typeof(XFontValue))
            {
                if (_Graphis != null)
                {
                    if (format.UseAdvancedDirectionVertical == true
                        /*&& (format.FormatFlags | StringFormatFlags.DirectionVertical) == format.FormatFlags*/)
                    {
                        //伍贻超20181026：当使用高级竖形排列模式，且FormatFlags包含竖形排列时，使用特殊的处理逻辑
                        //将解析出来的单个字符另外按横向模式与给定的新的坐标进行绘制
                        string[] ss = TransformingStringToArray(s, format.UseAdvancedDirectionVertical2);

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
                            Regex pattern = new Regex("[^0-9a-zA-Z]");
                            for (int i = 0; i < ss.Length; i++)
                            {
                                //在这里判断数组中的每一个解析出来的字符串，若字符串为单字符，则横向绘制不使用竖向模式。
                                //每绘制完一个字符串在绘制下一个字符串时需要重新计算坐标
                                //20181213备注：竖向绘制英文字符在X轴上会有偏移，微调下，但不能影响中文字符
                                string text = ss[i];

                              
                                //else
                                {
                                    SizeF sf = this._Graphis.MeasureString(text, font.Value, (int)rect.Width, format2.Value);
                                    rectf.Height = sf.Height;
                                    if (this._MyMartrix != null)
                                    {
                                        var rect3 = this._MyMartrix.Transform(rectf);
                                        this._Graphis.DrawString(
                                            text,
                                            font.Value,
                                            GraphicsObjectBuffer.GetSolidBrush(txtColor),
                                            rect3,
                                            format2.Value);
                                    }
                                    else
                                    {
                                        this._Graphis.DrawString(
                                            text,
                                            font.Value,
                                            GraphicsObjectBuffer.GetSolidBrush(txtColor),
                                            rectf,
                                            format2.Value);
                                    }
                                    rectf.Y = rectf.Y + rectf.Height;
                                }

                            }
                        }
                    }
                    else
                    {
                        if (this._MyMartrix == null)
                        {
                            this._Graphis.DrawString(
                                s,
                                font.Value,
                                GraphicsObjectBuffer.GetSolidBrush(txtColor),
                                rect,
                                format.Value);
                        }
                        else
                        {
                            var rect3 = this._MyMartrix.Transform(rect);
                            this._Graphis.DrawString(
                                s,
                                font.Value,
                                GraphicsObjectBuffer.GetSolidBrush(txtColor),
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


        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawString(string s, XFontValue font, Color txtColor, float x, float y, DrawStringFormatExt format)
        {

            lock (typeof(XFontValue))
            {
                if (_Graphis != null)
                {
                    this._MyMartrix?.Transform(ref x, ref y);
                    this._Graphis.DrawString(
                        s,
                        font.Value,
                        GraphicsObjectBuffer.GetSolidBrush(txtColor),
                        x,
                        y,
                        format?.Value);
                }
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(s);
            }
#endif
        }

        public virtual void DrawString(string s, XFontValue font, Color txtColor, float x, float y, DCStringFormat format)
        {

            lock (typeof(XFontValue))
            {
                if (_Graphis != null)
                {
                    this._MyMartrix?.Transform(ref x, ref y);
                    this._Graphis.DrawString(
                        s,
                        font.Value,
                        GraphicsObjectBuffer.GetSolidBrush(txtColor),
                        x,
                        y,
                        format?.Value());
                }
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(s);
            }
#endif
        }


        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawString(string text, Font font, Brush brush, float x, float y, StringFormat format)
        {

            if (_Graphis != null)
            {
                //wyc20231201:防止_MyMartrix为空
                if (this._MyMartrix != null)
                {
                    this._MyMartrix.Transform(ref x, ref y);
                }
                this._Graphis.DrawString(text, font, brush, x, y, format);
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(text);
            }
#endif
        }
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawString(string text, XFontValue font, Color txtColor, float x, float y)
        {
            DrawString(text, font, txtColor, x, y, DCStringFormat.GenericDefault);
            
        }

      
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {

            if (_Graphis != null)
            {
                this._MyMartrix?.Transform(ref x, ref y);
                this._Graphis.DrawString(s, font, brush, x, y);
            }
#if !DCWriterForWASM
            if (this.LogType == DCGraphicsLogType.Content)
            {
                LogContent(s);
            }
#endif
        }
        public void FillTextBackColor(string text, XFontValue font, Color textBackColor, RectangleF layoutRectangle, DCStringFormat format)
        {
            if (text == null || text.Length == 0)
            {
                return;
            }
            if (textBackColor.A == 0)
            {
                return;
            }
            // 文本背景色
            SizeF size = this.MeasureString(text, font, 100000, format);
            RectangleF rect = DCSoft.Drawing.RectangleCommon.AlignRect(
                layoutRectangle,
                size.Width,
                size.Height,
                format.Alignment ,
                format.LineAlignment);
            rect = RectangleF.Intersect(rect, layoutRectangle);
            using (var b = new SolidBrush(textBackColor))
            {
                this.FillRectangle(b, rect);
            }
            
        }

        public void DrawString(string txt, DrawStringFormatExt format)
        {
            //if (this._Recorder != null)
            //{
            //    this._Recorder.DrawString(txt, format);
            //}
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            if (txt == null || txt.Length == 0)// string.IsNullOrEmpty(txt))
            {
                return;
            }
            using (StringFormat f = format.CreateStringFormat())
            {
                SolidBrush b = GraphicsObjectBuffer.GetSolidBrush(format.Color);
                XFontValue font = format.Font;
                if (font == null)
                {
                    font = new XFontValue();
                }
                var targetRect = new RectangleF(format.Left, format.Top, format.Width, format.Height);
                if (format.TextBackColor.A > 0)
                {
                    // 文本背景色
                    SizeF size = this.GraphisForMeasure.MeasureString(txt, font.Value, 10000, f);
                    RectangleF rect = DCSoft.Drawing.RectangleCommon.AlignRect(
                        targetRect,
                        size.Width,
                        size.Height,
                        f);
                    rect = RectangleF.Intersect(rect, new RectangleF(format.Left, format.Top, format.Width, format.Height));
                    if( this._MyMartrix != null )
                    {
                        rect = this._MyMartrix.Transform(rect);
                    }
                    using (SolidBrush b2 = new SolidBrush(format.TextBackColor))
                    {

                        if (_Graphis != null)
                        {
                            _Graphis.FillRectangle(b2, rect);
                        }
                    }

                }
                if(this._MyMartrix != null )
                {
                    targetRect = this._MyMartrix.Transform(targetRect);
                }

                if (_Graphis != null)
                {
                    _Graphis.DrawString(
                        txt,
                        font.Value,
                        b,
                        targetRect,
                        f);
                }
#if !DCWriterForWASM
                if (this.LogType == DCGraphicsLogType.Content)
                {
                    LogContent(txt);
                }
#endif
            }
        }

        private static Graphics _GraphisForMeasure = null;

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public Graphics GraphisForMeasure
        {
            get
            {
                if (this._Graphis != null)
                {
                    return this._Graphis;
                }
                return null;
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public SizeF MeasureString(string text, Font font, int width, StringFormat format)
        {
            return this.GraphisForMeasure.MeasureString(text, font, width, format);
        }

        public virtual SizeF MeasureString(string text, XFontValue font, int width, DCStringFormat format)
        {
            if(this.GraphisForMeasure == null )
            {
                throw new InvalidOperationException("this.GraphisForMeasure null");
            }
            try
            {
                return this.GraphisForMeasure.MeasureString(text, font.Value, width, format?.Value());
            }
            catch
            {
                font.FixDisposedValue();
                return this.GraphisForMeasure.MeasureString(text, font.Value, width, format?.Value());
            }
        }
       

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public SizeF MeasureString(string text, XFontValue font, int width, DrawStringFormatExt format)
        {
            return this.MeasureString(text, font, width, format.CreateDCStringFormat());
           
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public SizeF MeasureString(string text, XFontValue font, int width)
        {
            return this.MeasureString(text, font, width, (DCStringFormat)null);
            //return this.GraphisForMeasure.MeasureString(text, font.Value, width);
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public SizeF MeasureString(string text, XFontValue font)
        {
            return this.MeasureString(text, font, 100000, (DCStringFormat)null);
        }

        //public SizeF MeasureString(string text, IMyFontExt font)
        //{
        //    return font.MeasureString(this.GraphisForMeasure, text, 10000, StringFormat.GenericTypographic);
        //}
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public SizeF MeasureString(string text, Font font)
        {
            return this.GraphisForMeasure.MeasureString(text, font);
        }

        //// [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public void FillRectangle(XBrushStyle brush, RectangleF rect)
        //{
        //    using (System.Drawing.Brush b = brush.CreateBrush())
        //    {
        //        FillRectangle(b, rect.Left, rect.Top, rect.Width, rect.Height);
        //    }
        //}

        //// [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public void FillRectangle(XBrushStyle brush, Rectangle rect)
        //{
        //    using (System.Drawing.Brush b = brush.CreateBrush())
        //    {
        //        FillRectangle(b, rect.Left, rect.Top, rect.Width, rect.Height);
        //    }
        //}

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void FillRectangle(Brush brush, RectangleF rect)
        {
            FillRectangle(brush, rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public void FillRectangle(Brush brush, Rectangle rect)
        {
            FillRectangle(brush, (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
          

            if (_Graphis != null)
            {
                this._MyMartrix?.TransformRectangleF(ref x, ref y, ref width, ref height);
                this._Graphis.FillRectangle(brush, x, y, width, height);
               
            }
        }
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void FillRectangle(Color c, RectangleF rect)
        {
            FillRectangle(c, rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public void FillRectangle(Color c, Rectangle rect)
        {
            FillRectangle(c, (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
        }
        private static readonly  int _AliceBlueARGB = System.Drawing.Color.AliceBlue.ToArgb();
        private static readonly int _LighGrayARGB = System.Drawing.Color.LightGray.ToArgb();
        public virtual void FillRectangle(Color c, float x, float y, float width, float height)
        {

            if (c == Color.Black)
            {
                FillRectangle(Brushes.Black, x, y, width, height);
            }
            else if( c == Color.White)
            {
                FillRectangle(Brushes.White, x, y, width, height);
            }
            else
            {
                using( var b= new SolidBrush(c ))
                {
                    FillRectangle(b, x, y, width, height);
                }    
            }
        }
      



       


       

        public void FillRegion(Brush brush, Region region)
        {
           
            if (this._Graphis != null)
            {
                this._Graphis.FillRegion(brush, region);
            }
        }
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void FillPath(Brush brush, GraphicsPath path)
        {
            //if (this._Recorder != null)
            //{
            //    this._Recorder.FillPath(brush, path);
            //}
            if (_Graphis != null)
            {
                this._Graphis.FillPath(brush, path);
            }
        }
        //public void FillPath(Brush brush, DCGraphicsPath path)
        //{
        //    //if (this._Recorder != null)
        //    //{
        //    //    this._Recorder.FillPath(brush, path);
        //    //}
        //    if (_Graphis != null)
        //    {
        //        using (var p2 = path.Build())
        //        {
        //            this._Graphis.FillPath(brush, p2);
        //        }
        //    }
        //}

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void FillPath(Color c, GraphicsPath path)
        {
            //if (this._Recorder != null)
            //{
            //    this._Recorder.FillPath(c, path);
            //}
            if (_Graphis != null)
            {
                this._Graphis.FillPath(GraphicsObjectBuffer.GetSolidBrush(c), path);
            }
        }

        //public void FillPath(Color c, DCGraphicsPath path)
        //{
        //    //if (this._Recorder != null)
        //    //{
        //    //    this._Recorder.FillPath(c, path);
        //    //}
        //    if (_Graphis != null)
        //    {
        //        using (var p2 = path.Build())
        //        {
        //            this._Graphis.FillPath(GraphicsObjectBuffer.GetSolidBrush(c), p2);
        //        }
        //    }
        //}

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPath(Color lineColor, float lineWidth, DashStyle lineStyle, GraphicsPath path)
        {
            //if (this._Recorder != null)
            //{
            //    this._Recorder.DrawPath(lineColor, lineWidth, lineStyle, path);
            //}
            using (Pen p = new Pen(lineColor, lineWidth))
            {
                p.DashStyle = lineStyle;
                DrawPath(p, path);
            }
        }

        //public void DrawPath(Color lineColor, float lineWidth, DashStyle lineStyle, DCGraphicsPath path)
        //{
        //    //if (this._Recorder != null)
        //    //{
        //    //    this._Recorder.DrawPath(lineColor, lineWidth, lineStyle, path);
        //    //}
        //    using (Pen p = new Pen(lineColor, lineWidth))
        //    {
        //        p.DashStyle = lineStyle;
        //        DrawPath(p, path);
        //    }
        //}

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPath(Color lineColor, float lineWidth, GraphicsPath path)
        {
            using (Pen p = new Pen(lineColor, lineWidth))
            {
                DrawPath(p, path);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPath(Color lineColor, GraphicsPath path)
        {
            DrawPath(GraphicsObjectBuffer.GetPen(lineColor), path);
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPath(Pen p, GraphicsPath path)
        {
            //if (this._Recorder != null)
            //{
            //    this._Recorder.DrawPath(p, path);
            //}
            if (_Graphis != null)
            {
                this._Graphis.DrawPath(p, path);
            }
        }
        //public void DrawPath(Pen p, DCGraphicsPath path)
        //{
        //    if (this._Graphis != null)
        //    {
        //        using (var p2 = path.Build())
        //        {
        //            this._Graphis.DrawPath(p, p2);
        //        }
        //    }
        //}

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPath(XPenStyle p, GraphicsPath path)
        {
            //if (this._Recorder != null)
            //{
            //    this._Recorder.DrawPath(p, path);
            //}
            if (_Graphis != null)
            {
                using (Pen p2 = p.CreatePen())
                {
                    this._Graphis.DrawPath(p2, path);
                }
            }
        }

        //public void DrawPath(XPenStyle p, DCGraphicsPath path)
        //{
        //    //if (this._Recorder != null)
        //    //{
        //    //    this._Recorder.DrawPath(p, path);
        //    //}
        //    if (_Graphis != null)
        //    {
        //        using (Pen p2 = p.CreatePen())
        //        {
        //            using (var p22 = path.Build())
        //            {
        //                this._Graphis.DrawPath(p2, p22);
        //            }
        //        }
        //    }
        //}

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPie(Pen p, Rectangle rect, float startAngle, float sweepAngle)
        {
            //if (this._Recorder != null)
            //{
            //    this._Recorder.DrawPie(p, rect, startAngle, sweepAngle);
            //}
            if (this._Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    rect = this._MyMartrix.Transform(rect);
                }
                this._Graphis.DrawPie(p, rect, startAngle, sweepAngle);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void DrawPie(Pen p, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            //if (this._Recorder != null)
            //{
            //    this._Recorder.DrawPie(p, x, y, width, height, startAngle, sweepAngle);
            //}
            if (this._Graphis != null)
            {
                this._MyMartrix?.TransformRectangleF(ref x, ref y, ref width, ref height);
                this._Graphis.DrawPie(p, x, y, width, height, startAngle, sweepAngle);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void FillPie(Brush b, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            //if (this._Recorder != null)
            //{
            //    this._Recorder.FillPie(b, x, y, width, height, startAngle, sweepAngle);
            //}
            if (this._Graphis != null)
            {
                this._MyMartrix?.TransformRectangleF(ref x, ref y, ref width, ref height);
                this._Graphis.FillPie(b, x, y, width, height, startAngle, sweepAngle);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void FillEllipse(Color c, RectangleF rect)
        {
            if (c == Color.Black)
            {
                FillEllipse(Brushes.Black, rect);
            }
            else if (c == Color.White)
            {
                FillEllipse(Brushes.White, rect);
            }
            else
            {
                using( var b = new SolidBrush(c ))
                {
                    FillEllipse(b, rect);
                }
            }
        }


     
       
        public virtual void FillEllipse(Brush brush, RectangleF rect)
        {
           

            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    rect = this._MyMartrix.Transform(rect);
                }
                this._Graphis.FillEllipse(brush, rect);
            }
        }

       
        private int _ClipVersion = 0;
        public int ClipVersion
        {
            get
            {
                return this._ClipVersion;
            }
        }
        public virtual void SetClip(RectangleF rect)
        {
            this._ClipVersion++;
          

            if (_Graphis != null)
            {
                _Graphis.SetClip(rect);
            }
        }

      

        public void SetClip(Region r, System.Drawing.Drawing2D.CombineMode mode)
        {
            this._ClipVersion++;
            //if (this._Recorder != null)
            //{
            //    this._Recorder.SetClip(r, mode);
            //}
            if (this._Graphis != null)
            {
                this._Graphis.SetClip(r, mode);
            }
        }
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetClip(RectangleF rect, CombineMode mode)
        {
            this._ClipVersion++;
#if DCWriterForWASM
            this._Graphis.SetClip(rect);
#else
            //if (this._Recorder != null)
            //{
            //    this._Recorder.SetClip(rect, mode);
            //}

            if (_Graphis != null)
            {
                if (this._MyMartrix != null)
                {
                    rect = this._MyMartrix.Transform(rect);
                }
                _Graphis.SetClip(rect, mode);
            }
#endif
        }


        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual object Save()
        {

            if (_Graphis != null)
            {
                return _Graphis.Save();
            }
            else
            {
                return null;
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void Restore(object obj)
        {
            if (obj != null)
            {
                this._ClipVersion++;

                if (_Graphis != null && obj is GraphicsState)
                {
                    _Graphis.Restore((GraphicsState)obj);
                }
            }
        }

        private bool _AutoDisposeNativeGraphics = false;
        /// <summary>
        /// 是否自动销毁掉底层的画布对象
        /// </summary>
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public bool AutoDisposeNativeGraphics
        {
            get
            {
                return _AutoDisposeNativeGraphics;
            }
            set
            {
                //if (this._Recorder != null)
                //{
                //    this._Recorder.AutoDisposeNativeGraphics = value;
                //}
                _AutoDisposeNativeGraphics = value;
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public virtual void Dispose()
        {
            if (this.AutoDisposeNativeGraphics)
            {
                if (_Graphis != null)
                {
                    Graphics g = this._Graphis;
                    this._Graphis = null;
                    g.Dispose();
                }
            }
        }

        private DCGraphicsLogType _LogType = DCGraphicsLogType.None;
        /// <summary>
        /// 内容记录类型
        /// </summary>
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public DCGraphicsLogType LogType
        {
            get
            {
                return _LogType;
            }
            set
            {
                _LogType = value;
            }
        }

        private Stream _LogStream = null;
        /// <summary>
        /// 记录内容使用的流对象
        /// </summary>
        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public Stream LogStream
        {
            get
            {
                return _LogStream;
            }
            set
            {
                _LogStream = value;
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public void LogContent(string txt)
        {
            if (this.LogType == DCGraphicsLogType.Content
                && this._LogStream != null
                && (txt != null && txt.Length > 0))// string.IsNullOrEmpty(txt) == false )
            {
                byte[] bs = Encoding.UTF8.GetBytes(txt);
                this.LogStream.Write(bs, 0, bs.Length);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public void LogContent(byte[] bs)
        {
            if (this.LogType == DCGraphicsLogType.Content
                && this._LogStream != null
                && bs != null
                && bs.Length > 0)
            {
                this.LogStream.Write(bs, 0, bs.Length);
            }
        }

        // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public void LogContent(Image img)
        {
            if (this.LogType == DCGraphicsLogType.Content
                && this.LogStream != null
                && img != null)
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Png);
                ms.WriteTo(this.LogStream);
            }
        }

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

                //伍贻超20211018：LINUX模式下时间轴竖形文本绘制有问题，试图将字符串中所有单个字符全部拆开一个一个绘制
                if (LinuxMode == true )
                {
                    foreach (char c in source)
                    {
                        stringList.Add(c.ToString());
                    }
                    return stringList.ToArray();
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                //wyc20220407：处理新机制：利用特殊字符处理分割
                if(useSpecificSplitChar == true)
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
    }

    /// <summary>
    /// 图形记录类型
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum DCGraphicsLogType
    {
        /// <summary>
        /// 不记录
        /// </summary>
        None,
        /// <summary>
        /// 只记录内容数据
        /// </summary>
        Content,
        /// <summary>
        /// 图形数据
        /// </summary>
        Graphics
    }


}
