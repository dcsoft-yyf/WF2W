using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DCSoft.OFD
{
    /// <summary>
    /// OFD画布对象
    /// </summary>
    internal class SVGGraphics
    {

        public SVGGraphics(System.IO.TextWriter txtWriter)
        {
            if (txtWriter == null)
            {
                throw new ArgumentNullException("txtWriter");
            }
            this._BaseWriter = txtWriter;
            this._SVG = new DCXmlTextWriter(txtWriter);
            this._SVG.WriteStartDocument();
            this._SVG.WriteStartElement("div");
        }


        private bool _AutoDisposeWriter = true;
        private SVGGraphics()
        {

        }

        private DCXmlTextWriter _SVG = null;

        private System.IO.TextWriter _BaseWriter = null;

        private float _GlobalOffsetX = 0;
        private float _GlobalOffsetY = 0;
        /// <summary>
        /// 设置全局性的偏移量
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetGlobalOffset(float x, float y)
        {
            this._GlobalOffsetX = x;
            this._GlobalOffsetY = y;
        }


        private Matrix _Transform = new Matrix();
        public Matrix Transform
        {
            get
            {
                return this._Transform;
            }
            set
            {
                this._Transform = value;
            }
        }


        public void RotateTransform(float angle)
        {
            this._Transform.Rotate(angle);
        }
        public void TranslateTransform(float dx, float dy)
        {
            this._Transform.Reset();
            this._Transform.Translate(dx, dy);
        }

        private class OFDState : GraphicsState
        {
            public Matrix Transform = null;
            public GraphicsUnit PageUnit = GraphicsUnit.Point;
            public RectangleF ClipRectangle = RectangleF.Empty;
        }
        public GraphicsState SaveState()
        {
            var result = new OFDState();
            result.Transform = this._Transform.Clone();
            result.PageUnit = this._PageUnit;
            result.ClipRectangle = this._ClipRectangle;
            return result;
        }
        public void RestoreState(GraphicsState state)
        {
            var obj = state as OFDState;
            if (obj != null)
            {
                this._Transform = obj.Transform;
                this._PageUnit = obj.PageUnit;
                this._ClipRectangle = obj.ClipRectangle;
            }
        }
        private float _UnitConvertRate = 1;
        private GraphicsUnit _PageUnit = GraphicsUnit.Pixel;
        public GraphicsUnit PageUnit
        {
            get
            {
                return this._PageUnit;
            }
            set
            {
                this._PageUnit = value;
                if (this._SVG == null)
                {
                    switch (this._PageUnit)
                    {
                        case GraphicsUnit.Document: this._UnitConvertRate = 0.08466666666667f; break;
                        case GraphicsUnit.Inch: this._UnitConvertRate = 25.4f; break;
                        case GraphicsUnit.Millimeter: this._UnitConvertRate = 1; break;
                        case GraphicsUnit.Pixel: this._UnitConvertRate = 0.264583341218531f; break;
                        case GraphicsUnit.Point: this._UnitConvertRate = 0.352777777777778f; break;
                        default: throw new NotSupportedException(this._PageUnit.ToString());
                    }
                }
                else
                {
                    switch (this._PageUnit)
                    {
                        case GraphicsUnit.Document: this._UnitConvertRate = 0.32f; break;
                        case GraphicsUnit.Inch: this._UnitConvertRate = 96f; break;
                        case GraphicsUnit.Millimeter: this._UnitConvertRate = 3.7795f; break;
                        case GraphicsUnit.Pixel: this._UnitConvertRate = 1; break;
                        case GraphicsUnit.Point: this._UnitConvertRate = 1.3333333f; break;
                        default: throw new NotSupportedException(this._PageUnit.ToString());
                    }
                }
            }
        }

        private int _MaxUnitID = 0;
        public int AllocUnitID()
        {
            return _MaxUnitID++;
        }
        private int _CurrentDocumentID = -1;


        public void Dispose()
        {
            if (this._CurrentCharsX != null)
            {
                this._CurrentCharsX.Clear();
                this._CurrentCharsX = null;
                this._CurrentChars.Clear();
                this._CurrentChars = null;
            }

            if (this._SVG != null)
            {
                //this._SVG.EventBeforeWriteStartElement = null;
                this.CloseSVGPage();
                if (this._AutoDisposeWriter)
                {
                    this._SVG.WriteEndDocument();
                    this._SVG.Close();
                    this._SVG = null;
                }
                if (this._SVGFontStyles != null)
                {
                    this._SVGFontStyles.Clear();
                    this._SVGFontStyles = null;
                }
                return;
            }
            if (this._FontItems != null)
            {
                this._FontItems.Clear();
                this._FontItems = null;
            }
            this._Transform = null;
        }


        private List<int> _PageIDs = new List<int>();
        private int _CurrentPageIndex = -1;
        private float _PageWidth = 100;
        private float _PageHeight = 100;

        private bool _HasAddPage = false;

        private float _PageContentHeight = 0;
        private void CloseSVGPage()
        {
            if (this._HasAddPage)
            {
                this._HasAddPage = false;
                this._SVG.WriteStartElement("rect");
                this._SVG.WriteAttributeString("dctype", "contentheight");
                this._SVG.WriteAttributeString("width", "0");
                this._SVG.WriteAttributeString("height", "0");
                this._SVG.WriteAttributeString("value", this._PageContentHeight.ToString());
                this._SVG.WriteEndElement();
                this._SVG.WriteEndElement();
            }
        }

        //在蓝芯70版本的浏览器中，生成的svg不能连续选择，需要添加特定的css DUWRITER5_0-3791
        internal static bool SelectSvgForLanXin70 = false;

        private static int _SVGEntryIndex = 1;
        public void StartSVGPageContent(float pageWidth, float pageHeight)
        {
            this._HasAddPage = true;
            this._PageWidth = pageWidth;
            this._PageHeight = pageHeight;
            this._PageContentHeight = 0;
            this._SVG.WriteStartElement("g");
            if (this._SVGFontStyles != null && this._SVGFontStyles.Count > 0)
            {
                this._SVG.WriteStartElement("style");
                var strCss = new StringBuilder();

                foreach (var item in this._SVGFontStyles)
                {
                    item.UserID = "dcf_" + (_SVGEntryIndex++);
                    strCss.AppendLine("  ." + item.UserID + "{" + ToCSSString(item.Name, item.Size, item.Style) + ";fill:black}");
                }

                //DUWRITER5_0-3791 添加蓝芯70浏览器的CSS样式
                if (SelectSvgForLanXin70)
                {
                    string selection1 = "  svg *::selection {fill: blue;}";
                    strCss.AppendLine(selection1);
                    string selection2 = "  svg *::-moz-selection {fill: blue;}";
                    strCss.AppendLine(selection2);
                    string selection3 = "  svg *::-webkit-selection {fill: blue;}";
                    strCss.AppendLine(selection3);
                }
                // 处理医学表达式线变虚线和表格边框展示不全的问题【DUWRITER5_0-3873】
                string lineCssStr = "line{shape-rendering:auto;}";
                strCss.AppendLine(lineCssStr);
                // 添加不可选中样式给存在属性user-select=none的元素
                string unselectableCssStr = "[user-select=none]{-webkit-user-select:none;-ms-user-select:none;-o-user-select:none;-moz-user-select:none;-khtml-user-select:none;user-select:none}";
                strCss.AppendLine(unselectableCssStr);

                this._SVG.WriteString(strCss.ToString());
                this._SVG.WriteEndElement();
            }
            this._CurrentChars.Clear();
            this._CurrentCharsX.Clear();
            //this._SVG.EventBeforeWriteStartElement = this.CommitCurrentChars;
        }


        //private static readonly RectangleF MaxRect = new RectangleF(-10000000, -10000000, 20000000, 20000000);
        private RectangleF _ClipRectangle = RectangleF.Empty;// MaxRect;
        public RectangleF ClipRectangle
        {
            get { return this._ClipRectangle; }
            //set { this._ClipRectangle = value; }
        }
        private Region _Clip = null;
        public Region Clip
        {
            get
            {
                if (this._Clip == null)
                {
                    this._Clip = new Region(this._ClipRectangle);
                }
                return this._Clip;
            }
            set
            {
                this._Clip = value;
                if (value != null && value.IsSingleRectangleF())
                {
                    this._ClipRectangle = value.GetSingleRectangleF();
                }
            }
        }
        public void ResetClip()
        {
            this._ClipRectangle = RectangleF.Empty;
            this._Clip = null;
        }
        public void SetClip(RectangleF rect)
        {
            this._ClipRectangle = this.ToLocalRectangleFloat(rect.X, rect.Y, rect.Width, rect.Height);
            this._Clip = null;
        }
        //private class GroupInfo
        //{
        //    public bool ClipRectangleEmpty = false;
        //    public RectangleF ClipRectangle;
        //    public string FontName;
        //    public float FontSize;
        //    public FontStyle vFontStyle;
        //    public int StartPosition;
        //    public int ContentStartPosition;
        //    public StringBuilder BaseStringBuilder;
        //}
        //public void SetPageRootElement(XTextDocumentContentElement dce)
        //{
        //    if (this._ParentElements != null)
        //    {
        //        this._ParentElements.Clear();
        //        this._ParentElements.Add(dce);
        //    }
        //}

        //public void SetCurrentParent(XTextContainerElement parentElement)
        //{
        //    if (this._ParentElements != null)
        //    {
        //        if (this._ParentElements.LastElement == parentElement)
        //        {
        //            // 父节点未发生改变
        //        }
        //        else if (this._ParentElements.Count > 0)
        //        {
        //            // 父节点发生改变了
        //            if (this._ParentElements.LastElement == parentElement.Parent)
        //            {
        //                // 进入下一层子节点
        //                this._ParentElements.AddRaw(parentElement);
        //            }
        //            else if (this._ParentElements.LastElement.Parent == parentElement)
        //            {
        //                // 进入上一层
        //                this._ParentElements.RemoveAt(this._ParentElements.Count - 1);
        //            }
        //            else
        //            {
        //                var p2 = parentElement;
        //                while (p2 != null)
        //                {
        //                    var index2 = this._ParentElements.IndexOfRevert(p2);
        //                    if (index2 >= 0)
        //                    {

        //                    }
        //                    p2 = p2.Parent;
        //                }
        //            }
        //            var index = this._ParentElements.IndexOfRevert(parentElement);
        //            if (index < 0)
        //            {

        //            }
        //        }
        //    }
        //}
        //private XTextElementList _ParentElements = null;

        //private Stack<GroupInfo> _GroupClipRectangles = null;
        //public override void BeginGroupShape(string strID, RectangleF clipRect, string vFontName, float vFontSize, FontStyle vFontStyle, bool bolShapeRenderingAuto)
        //{
        //    this.CommitCurrentCharsForSVG();
        //    if (this._SVG != null)
        //    {
        //        if (this._GroupClipRectangles == null)
        //        {
        //            this._GroupClipRectangles = new Stack<GroupInfo>();
        //        }
        //        var info = new GroupInfo();
        //        if (clipRect.IsEmpty == false)
        //        {
        //            info.ClipRectangle = this.ToLocalRectangleFloat(
        //                clipRect.Left - 2,
        //                clipRect.Top - 2,
        //                clipRect.Width + 4,
        //                clipRect.Height + 7);
        //            info.ClipRectangleEmpty = false;
        //        }
        //        else
        //        {
        //            info.ClipRectangleEmpty = true;
        //        }
        //        info.FontName = vFontName;
        //        info.FontSize = vFontSize;
        //        info.vFontStyle = vFontStyle;
        //        if (this._SVG.BaseTextWriter is System.IO.StringWriter)
        //        {
        //            this._SVG.AutoComplete(DCXmlTextWriter.Token.Content);
        //            //this._SVG.WriteString(" ");
        //            var str2 = ((System.IO.StringWriter)this._SVG.BaseTextWriter).GetStringBuilder();
        //            info.BaseStringBuilder = str2;
        //            info.StartPosition = str2.Length;
        //        }
        //        string strClipID = null;
        //        if (clipRect.Width > 0 && clipRect.Height > 0)
        //        {
        //            this._ClipRectangle = info.ClipRectangle;// this.ToLocalRectangle(clipRect.X, clipRect.Y, clipRect.Width, clipRect.Height);
        //            strClipID = GetSVGClipPathID(new DCSystem_Drawing.RectangleF(-10000, -10000, 1, 1));
        //        }
        //        this._SVG.WriteStartElement("g");
        //        if (strID != null && strID.Length > 0)
        //        {
        //            this._SVG.WriteAttributeString("id", strID);
        //        }
        //        if (vFontName != null && vFontName.Length > 0)
        //        {
        //            this.WriteSVGFont(vFontName, vFontSize, vFontStyle, DCSystem_Drawing.Color.Black);
        //        }
        //        this.WriteSVGClipID(strClipID);
        //        if (bolShapeRenderingAuto)
        //        {
        //            this._SVG.WriteAttributeStringRaw("shape-rendering", "auto");
        //        }
        //        this._GroupClipRectangles.Push(info);
        //        if (info.BaseStringBuilder != null)
        //        {
        //            info.ContentStartPosition = info.BaseStringBuilder.Length;
        //        }
        //    }
        //}

        //public override void EndGroupShape()
        //{
        //    this.CommitCurrentCharsForSVG();
        //    if (this._SVG != null)
        //    {
        //        var info = this._GroupClipRectangles.Pop();
        //        this._ClipRectangle = info.ClipRectangle;
        //        this._SVG.WriteEndElement();
        //        if (info.BaseStringBuilder != null)
        //        {
        //            if (info.BaseStringBuilder.Length - info.ContentStartPosition < 5)
        //            {
        //                // 认为这组SVG没有输出任何内容，则删除已经输出过的内容。
        //                info.BaseStringBuilder.Remove(info.StartPosition, info.BaseStringBuilder.Length - info.StartPosition);
        //            }
        //            info.BaseStringBuilder = null;
        //        }
        //    }
        //}
        private static readonly int _Black_ARGB = Color.Black.ToArgb();
        private void WriteSVGBrush(Brush b)
        {
            if (b is SolidBrush)
            {
                var fc = ((SolidBrush)b).Color;
                if (fc.ToArgb() == _Black_ARGB)
                {
                    this._SVG.WriteAttributeString("fill", "black");
                }
                else
                {
                    if (fc.A == 255)
                    {
                        this._SVG.WriteAttributeString("fill", ColorTranslator.ToHtml(fc));
                    }
                    else
                    {
                        var c2 = Color.FromArgb(fc.R, fc.G, fc.B);
                        this._SVG.WriteAttributeString("fill", ColorTranslator.ToHtml(c2));
                        this._SVG.WriteAttributeString("fill-opacity", (fc.A / 255.0).ToString());
                    }
                }
            }
            else if (b is LinearGradientBrush)
            {
                LinearGradientBrush b2 = (LinearGradientBrush)b;
                if (b2.SVGID == null)
                {
                    throw new InvalidOperationException("未准备画刷");
                }
                this._SVG.WriteAttributeString("fill", "url('#" + b2.SVGID + "')");
            }
            else if (b is TextureBrush)
            {
                var tb = (TextureBrush)b;
                if (tb.SVGID == null)
                {
                    throw new InvalidOperationException("未准备画刷");
                }
                this._SVG.WriteAttributeString("fill", "url('#" + tb.SVGID + "')");
            }
            else
            {
                throw new NotSupportedException(b.ToString());
            }
        }
        //private void WriteSVGBounds(float x , float y , float width , float height )
        //{
        //    this._SVG.WriteAttributeString("x", x.ToString());
        //    this._SVG.WriteAttributeString("y", y.ToString());
        //    this._SVG.WriteAttributeString("width", width.ToString());
        //    this._SVG.WriteAttributeString("height", height.ToString());
        //}
        //[ThreadStatic]
        //private static readonly char[] _Buffer_SVGSingleToString = new char[40];

        //private static string SVGSingleToString(float v)
        //{
        //    //if( _Buffer_SVGSingleToString == null )
        //    //{
        //    //    _Buffer_SVGSingleToString = new char[40];
        //    //}
        //    var len = StaticAppendSingle(_Buffer_SVGSingleToString, 0, v, 10000);
        //    if(len > 0 )
        //    {
        //        return new string(_Buffer_SVGSingleToString, 0, len);
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}
        private void WriteSVGBounds(Rectangle rect)
        {
            this._SVG.WriteAttributeInt32("x", rect.X);
            this._SVG.WriteAttributeInt32("y", rect.Y);
            this._SVG.WriteAttributeInt32("width", rect.Width);
            this._SVG.WriteAttributeInt32("height", rect.Height);
            if (rect.Bottom > this._PageContentHeight)
            {
                this._PageContentHeight = rect.Bottom;
            }
        }

        //private float _LastYValue = float.MinValue;
        //private char[] _LastYString = null;
        //private int _LastYStringLen = 0;

        //private void WriteMatrixAttribute( string strName , Matrix m)
        //{
        //    if(this._SVG != null )
        //    {
        //        this._SVG.WriteAttributeStringRaw(
        //            strName,
        //            "matrix("
        //            + m._Element0 + " "
        //            + m._Element1 + " "
        //            + m._Element2 + " "
        //            + m._Element3 + " "
        //            + (m._Element4 * this._UnitConvertRate )+ " "
        //            + (m._Element5 * this._UnitConvertRate)+ ")");
        //    }
        //}
        //private void WriteSVGPoint(float x, float y)
        //{
        //    //var strX = SVGSingleToString(x);
        //    //if( x -100 > float.Parse( strX ) )
        //    //{
        //    //    strX = SVGSingleToString(x);
        //    //}
        //    this.WriteSVGAttributeSingle("x", x);
        //    if (y == this._LastYValue)
        //    {
        //        this._SVG.WriteAttributeCharsRaw("y", this._LastYString, this._LastYStringLen);
        //    }
        //    else
        //    {
        //        var len = StaticAppendSingle(_Buffer_SVGSingleToString, 0, y, 10000);
        //        if (len > 0)
        //        {
        //            this._SVG.WriteAttributeCharsRaw("y", _Buffer_SVGSingleToString, len);
        //            this._LastYValue = y;
        //            if( this._LastYString == null )
        //            {
        //                this._LastYString = (char[])_Buffer_SVGSingleToString.Clone();
        //            }
        //            else
        //            {
        //                Array.Copy(_Buffer_SVGSingleToString, this._LastYString, len);
        //            }
        //            this._LastYStringLen = len;
        //        }
        //    }
        //    //this.WriteSVGAttributeSingle("y", y);
        //    //this._SVG.WriteAttributeStringRaw("x", SVGSingleToString(x));
        //    //this._SVG.WriteAttributeStringRaw("y", SVGSingleToString(y));
        //    if (y > this._PageContentHeight)
        //    {
        //        this._PageContentHeight = y;
        //    }
        //}
        //private void WriteSVGAttributeSingle(string name , float v )
        //{
        //    //if (_Buffer_SVGSingleToString == null)
        //    //{
        //    //    _Buffer_SVGSingleToString = new char[40];
        //    //}
        //    var len = StaticAppendSingle(_Buffer_SVGSingleToString, 0, v, 10000);
        //    if(len > 0 )
        //    {
        //        this._SVG.WriteAttributeCharsRaw(name, _Buffer_SVGSingleToString, len);
        //    }
        //}
        //private void WriteSVGAttributeInt32(string name, int v)
        //{
        //    //if (_Buffer_SVGSingleToString == null)
        //    //{
        //    //    _Buffer_SVGSingleToString = new char[40];
        //    //}
        //    var len = StaticAppendInt32(_Buffer_SVGSingleToString, 0, v);
        //    if (len > 0)
        //    {
        //        this._SVG.WriteAttributeCharsRaw(name, _Buffer_SVGSingleToString, len);
        //    }
        //}

        //private static int _LastInt32Value = int.MinValue;
        //private static int _lastValueLength = 0;
        //private static char[] _LastChars = null;
        //private static void WriteAttributeInt32UseLastValue(XmlTextWriter writer, string name, int v)
        //{
        //    if (v == _LastInt32Value)
        //    {
        //        writer.WriteAttributeCharsRaw(name, _LastChars, _lastValueLength);
        //    }
        //    else
        //    {
        //        var len = StaticAppendInt32(_Buffer_SVGSingleToString, 0, v);
        //        if (len > 0)
        //        {
        //            _LastInt32Value = v;
        //            _lastValueLength = len;
        //            if (_LastChars == null)
        //            {
        //                _LastChars = (char[])_Buffer_SVGSingleToString.Clone();
        //            }
        //            else
        //            {
        //                Array.Copy(_Buffer_SVGSingleToString, _LastChars, len);
        //            }
        //            writer.WriteAttributeCharsRaw(name, _Buffer_SVGSingleToString, len);
        //        }
        //    }
        //}

        //private static void WriteAttributeInt32( XmlTextWriter writer , string name , int v )
        //{
        //    var len = StaticAppendInt32(_Buffer_SVGSingleToString, 0, v);
        //    if (len > 0)
        //    {
        //        writer.WriteAttributeCharsRaw(name, _Buffer_SVGSingleToString, len);
        //    }
        //}
        //private static void WriteAttributeSingle(XmlTextWriter writer, string name, float v)
        //{
        //    var len = StaticAppendSingle(_Buffer_SVGSingleToString, 0, v, 10000);
        //    if (len > 0)
        //    {
        //        writer.WriteAttributeCharsRaw(name, _Buffer_SVGSingleToString, len);
        //    }
        //}
        //private static string ToPercentString( float v , float baseValue )
        //{
        //    if(v == 0 )
        //    {
        //        return "0%";
        //    }
        //    else
        //    {
        //        return (v * 100 / baseValue ).ToString() + "%";
        //    }
        //}
        private void PrepareSVG(Brush b)
        {
            if (b is LinearGradientBrush)
            {
                var lb2 = (LinearGradientBrush)b;
                if (lb2.SVGID == null)
                {
                    lb2.SVGID = "dc_lb_" + (_SVGEntryIndex++);
                    this._SVG.WriteStartElement("linearGradient");
                    this._SVG.WriteAttributeString("id", lb2.SVGID);
                    switch (lb2.Mode)
                    {
                        case LinearGradientMode.Horizontal: break;
                        case LinearGradientMode.Vertical: this._SVG.WriteAttributeString("gradientTransform", "rotate(90)"); break;
                        case LinearGradientMode.BackwardDiagonal: this._SVG.WriteAttributeString("gradientTransform", "rotate(45)"); break;
                        case LinearGradientMode.ForwardDiagonal: this._SVG.WriteAttributeString("gradientTransform", "rotate(-45)"); break;
                    }
                    var cls = lb2.InterpolationColors;
                    if (cls != null
                        && cls.Colors != null
                        && cls.Colors.Length > 0
                        && cls.Positions != null
                        && cls.Positions.Length == cls.Colors.Length)
                    {
                        for (var iCount = 0; iCount < cls.Colors.Length; iCount++)
                        {
                            this._SVG.WriteStartElement("stop");
                            this._SVG.WriteAttributeString("stop-color", ColorTranslator.ToHtml(cls.Colors[iCount]));
                            this._SVG.WriteAttributeString("offset", Convert.ToString(cls.Positions[iCount] * 100) + "%");
                            this._SVG.WriteEndElement();
                        }
                    }
                    else
                    {
                        this._SVG.WriteAttributeString("offset", "0%");
                        this._SVG.WriteAttributeString("stop-color", ColorTranslator.ToHtml(lb2.Color1));
                        this._SVG.WriteEndElement();
                        this._SVG.WriteStartElement("stop");
                        this._SVG.WriteAttributeString("offset", "100%");
                        this._SVG.WriteAttributeString("stop-color", ColorTranslator.ToHtml(lb2.Color2));
                        this._SVG.WriteEndElement();
                    }
                    this._SVG.WriteEndElement();
                }
            }
            else if (b is TextureBrush)
            {
                var b2 = (TextureBrush)b;
                if (b2.SVGID == null)
                {
                    b2.SVGID = "dc_tb_" + (_SVGEntryIndex++);
                    this._SVG.WriteStartElement("pattern");
                    this._SVG.WriteAttributeString("id", b2.SVGID);
                    this._SVG.WriteAttributeString("x", "0");
                    this._SVG.WriteAttributeString("y", "0");
                    this._SVG.WriteAttributeString("width", "0.1");
                    this._SVG.WriteAttributeString("height", "0.1");
                    var bmp = b2.Image as Bitmap;
                    if (bmp != null && bmp.Data != null && bmp.Data.Length > 0)
                    {
                        this._SVG.WriteStartElement("image");
                        this._SVG.WriteAttributeString("width", bmp.Width.ToString());
                        this._SVG.WriteAttributeString("height", bmp.Height.ToString());
                        this._SVG.WriteAttributeString("href", bmp.CreateBlobUrl());
                        this._SVG.WriteEndElement();
                    }
                    this._SVG.WriteEndElement();
                }
            }
        }

        //wyc20241009:添加开关控制，白色颜色输出成NONE。避免低版本谷歌浏览器显示灰线问题DUWRITER5_0-3695
        internal static bool OutputSVGWhiteColorUsingNoneStroke = false;
        private void WriteSVGPen(Pen p)
        {
            string colorstring = ColorTranslator.ToHtml(p.Color);
            if (OutputSVGWhiteColorUsingNoneStroke == true && p.Color == Color.White)
            {
                colorstring = "none";
            }
            ////////////////////////////////////////////////////////////////////////////////////////////
            this._SVG.WriteAttributeString("stroke", colorstring);
            if (p.Width > 1)
            {
                //this._SVG.WriteAttributeString("stroke-width", p.Width.ToString());
                var w2 = GraphicsUnitConvert.ToPixel(p.Width, GraphicsUnit.Document);
                if (w2 > 1)
                {
                    this._SVG.WriteAttributeString("stroke-width", w2.ToString());
                }
            }
            var pd = p.DashStyle;
            if (pd == DashStyle.Dash)
            {
                this._SVG.WriteAttributeString("stroke-dasharray", "5,5");
            }
            else if (pd == DashStyle.DashDot)
            {
                this._SVG.WriteAttributeString("stroke-dasharray", "5,2");
            }
            else if (pd == DashStyle.DashDotDot)
            {
                this._SVG.WriteAttributeString("stroke-dasharray", "5,2,2");
            }
            else if (pd == DashStyle.Dot)
            {
                this._SVG.WriteAttributeString("stroke-dasharray", "2,2");
            }
        }
        public void FillPie(Brush b, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            var rect = this.ToLocalRectangleFloat(x, y, width, height);
            if (this._PageContentHeight < rect.Bottom)
            {
                this._PageContentHeight = rect.Bottom;
            }
            this.PrepareSVG(b);
            if (sweepAngle != 0 && (sweepAngle % 360 == 0))
            {
                // 完整的椭圆
                this.FillEllipse(b, rect.Left, rect.Top, rect.Width, rect.Height);
            }
            else
            {
                var data = GraphicsPath.BuildArcPath(-10000, -10000, rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle);
                if (data != null)
                {
                    var strClip = this.GetSVGClipPathID(rect);
                    this._SVG.WriteStartElement("path");
                    this.WriteSVGBrush(b);
                    var cx = rect.Left + rect.Width / 2;
                    var cy = rect.Top + rect.Height / 2;
                    var strData = "M " + cx + " " + cy + " L" + data[0] + " " + data[1] + " " + data[2] + " Z";
                    //Console.WriteLine(strData);
                    this._SVG.WriteAttributeString("d", strData);
                    this._SVG.WriteEndElement();
                }
            }
        }

        public void DrawPie(Pen p, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            this.CommitCurrentCharsForSVG();
            var rect = this.ToLocalRectangle(x, y, width, height);
            if (this._SVG != null)
            {
                //if (this._PageContentHeight < rect.Bottom)
                //{
                //    this._PageContentHeight = rect.Bottom;
                //}
                //if( sweepAngle != 0 && ( sweepAngle % 360 == 0 ))
                //{
                //    // 完整的椭圆
                //    this.DrawEllipse(p, rect.Left, rect.Top, rect.Width, rect.Height);
                //}
                //else
                //{
                //    var data = GraphicsPath.BuildArcPath(rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle);
                //    if( data != null )
                //    {
                //        var strClip = this.GetSVGClipPathID(rect);
                //        this._SVG.WriteStartElement("path");
                //        this._SVG.WriteAttributeString("fill", "transparent");
                //        this.WriteSVGPen(p);
                //        var strData = "M " + data[0] + " " + data[1] + data[3] + " Z";
                //        this._SVG.WriteAttributeString("d", strData);
                //        this._SVG.WriteEndElement();
                //    }
                //}

            }
        }
        public void DrawEllipse(Pen pen, float x, float y, float w, float h)
        {
            this.CommitCurrentCharsForSVG();
            var rect = this.ToLocalRectangleFloat(x, y, w, h);
            if (this._PageContentHeight < rect.Bottom)
            {
                this._PageContentHeight = rect.Bottom;
            }
            var strClipID = this.GetSVGClipPathID(rect);
            this._SVG.WriteStartElement("ellipse");
            this._SVG.WriteAttributeInt32("cx", (int)(rect.X + rect.Width / 2));
            this._SVG.WriteAttributeInt32("cy", (int)(rect.Y + rect.Height / 2));
            this._SVG.WriteAttributeInt32("rx", (int)(rect.Width / 2));
            this._SVG.WriteAttributeInt32("ry", (int)(rect.Height / 2));
            this.WriteSVGClipID(strClipID);
            this.WriteSVGPen(pen);
            this._SVG.WriteAttributeString("fill", "none");
            this._SVG.WriteEndElement();
        }

        public void FillEllipse(Brush b, float x, float y, float w, float h)
        {
            var rect = this.ToLocalRectangleFloat(x, y, w, h);
            if (this._PageContentHeight < rect.Bottom)
            {
                this._PageContentHeight = rect.Bottom;
            }
            var strClipID = GetSVGClipPathID(rect);
            this._SVG.WriteStartElement("ellipse");
            this._SVG.WriteAttributeInt32("cx", (int)(rect.X + rect.Width / 2));
            this._SVG.WriteAttributeInt32("cy", (int)(rect.Y + rect.Height / 2));
            this._SVG.WriteAttributeInt32("rx", (int)(rect.Width / 2));
            this._SVG.WriteAttributeInt32("ry", (int)(rect.Height / 2));
            this.WriteSVGClipID(strClipID);
            this.WriteSVGBrush(b);
            this._SVG.WriteEndElement();
        }
        public void DrawPath(Pen p, GraphicsPath path)
        {
            this.CommitCurrentCharsForSVG();
            var m2 = this._Transform.Clone();
            if (this._UnitConvertRate != 1)
            {
                m2.Scale(this._UnitConvertRate, this._UnitConvertRate);
                m2.Element4 *= this._UnitConvertRate;
                m2.Element5 *= this._UnitConvertRate;
            }
            this._SVG.WriteStartElement("path");
            this._SVG.WriteAttributeString("fill", "none");
            var p2 = (Pen)p.Clone();
            if (m2.Element0 > 0)
            {
                p2.Width = p2.Width / m2.Element0;
            }
            this.WriteSVGPen(p2);
            this._SVG.WriteAttributeString(
                "transform",
                m2.ToCSSString());
            this._SVG.WriteAttributeString("d", path.ToSVGString());
            this._SVG.WriteEndElement();
        }

        public void FillPath(Brush b, GraphicsPath path)
        {
            var m2 = this._Transform.Clone();
            if (this._UnitConvertRate != 1)
            {
                m2.Scale(this._UnitConvertRate, this._UnitConvertRate);
                m2.Element4 *= this._UnitConvertRate;
                m2.Element5 *= this._UnitConvertRate;
            }
            this.PrepareSVG(b);
            this._SVG.WriteStartElement("path");
            this.WriteSVGBrush(b);
            this._SVG.WriteAttributeString(
                "transform",
                m2.ToCSSString());
            this._SVG.WriteAttributeString("d", path.ToSVGString());
            this._SVG.WriteEndElement();
        }

        public void DrawRectangle(Pen pen, float x, float y, float w, float h, float roundRadio)
        {
            this.CommitCurrentCharsForSVG();
            var rect = this.ToLocalRectangleFloat(x, y, w, h);
            var strClipID = GetSVGClipPathID(rect);
            this._SVG.WriteStartElement("rect");
            this.WriteSVGBounds(DCValueConvert.ToInt32(rect));
            if (roundRadio > 0)
            {
                var ri = ToLocalLength(roundRadio);
                if (ri > 1)
                {
                    this._SVG.WriteAttributeSingle("rx", ri);
                    this._SVG.WriteAttributeSingle("ry", ri);
                }
            }
            this.WriteSVGClipID(strClipID);
            this._SVG.WriteAttributeString("fill", "none");
            this.WriteSVGPen(pen);
            this._SVG.WriteEndElement();
        }
        public void FillRectangleFloat(Color c, float x, float y, float width, float height)
        {
            var rect = this.ToLocalRectangleFloat(x, y, width, height);
            var strClipID = GetSVGClipPathID(rect);
            this._SVG.WriteStartElement("rect");
            this._SVG.WriteAttributeSingle("x", rect.X);
            this._SVG.WriteAttributeSingle("y", rect.Y);
            this._SVG.WriteAttributeSingle("width", rect.Width);
            this._SVG.WriteAttributeSingle("height", rect.Height);
            if (rect.Bottom > this._PageContentHeight)
            {
                this._PageContentHeight = rect.Bottom;
            }

            //if (roundRadio > 0)
            //{
            //    var ri = ToLocalLength(roundRadio);
            //    if (ri > 1)
            //    {
            //        WriteAttributeSingle(this._SVG, "rx", ri);
            //        WriteAttributeSingle(this._SVG, "ry", ri);
            //    }
            //}
            this.WriteSVGClipID(strClipID);
            this._SVG.WriteAttributeStringRaw("fill", ColorTranslator.ToHtml(c));
            this._SVG.WriteEndElement();
        }
        public void FillRectangle(Brush b, float x, float y, float w, float h, float roundRadio)
        {
            var rect = this.ToLocalRectangleFloat(x, y, w, h);
            var strClipID = GetSVGClipPathID(rect);
            this._SVG.WriteStartElement("rect");
            this.WriteSVGBounds(DCValueConvert.ToInt32(rect));
            if (roundRadio > 0)
            {
                var ri = ToLocalLength(roundRadio);
                if (ri > 1)
                {
                    this._SVG.WriteAttributeSingle("rx", ri);
                    this._SVG.WriteAttributeSingle("ry", ri);
                }
            }
            this.WriteSVGClipID(strClipID);
            this.WriteSVGBrush(b);
            this._SVG.WriteEndElement();
        }
        private RectangleF GetBounds(float x1, float y1, float x2, float y2)
        {
            float left, top, width, height;
            if (x1 < x2)
            {
                left = x1;
                width = x2 - x1;
            }
            else
            {
                left = x2;
                width = x1 - x2;
            }
            if (y1 < y2)
            {
                top = y1;
                height = y2 - y1;
            }
            else
            {
                top = y2;
                height = y1 - y2;
            }
            if (x1 == x2)
            {
                left -= 4;
                width += 8;
            }
            if (y1 == y2)
            {
                top -= 4;
                height += 2;
            }
            return new RectangleF(left, top, width, height);
        }


        public void DrawLine(Pen p, float x1, float y1, float x2, float y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                // 无意义的数据
                return;
            }
            this.CommitCurrentCharsForSVG();
            var pp1 = this.ToLocalPoint(x1, y1);
            var pp2 = this.ToLocalPoint(x2, y2);
            string strClipID = null;
            if (this._ClipRectangle.IsEmpty == false)
            {
                float vLeft, vTop, vRight, vBottom;
                if (pp1.X < pp2.X)
                {
                    vLeft = pp1.X;
                    vRight = pp2.X;
                }
                else
                {
                    vLeft = pp2.X;
                    vRight = pp1.X;
                }
                if (pp1.Y < pp2.Y)
                {
                    vTop = pp1.Y;
                    vBottom = pp2.Y;
                }
                else
                {
                    vTop = pp2.Y;
                    vBottom = pp1.Y;
                }
                if (vLeft < this._ClipRectangle.Left - 1
                    || vTop < this._ClipRectangle.Top - 1
                    || vRight > this._ClipRectangle.Right + 1
                    || vBottom > this._ClipRectangle.Bottom + 1)
                {
                    strClipID = GetSVGClipPathID(new RectangleF(vLeft, vTop, vRight - vLeft, vBottom - vTop));
                }
            }
            this._SVG.WriteStartElement("line");
            if (pp1.X == pp2.X)
            {
                // 竖线
                this._SVG.WriteAttributeInt32AddHalf("x1", pp1.X);
                this._SVG.WriteAttributeInt32("y1", pp1.Y);
                this._SVG.WriteAttributeInt32AddHalf("x2", pp2.X);
                this._SVG.WriteAttributeInt32("y2", pp2.Y);
            }
            else if (pp1.Y == pp2.Y)
            {
                // 横线
                this._SVG.WriteAttributeInt32("x1", pp1.X);
                this._SVG.WriteAttributeInt32AddHalf("y1", pp1.Y);
                this._SVG.WriteAttributeInt32("x2", pp2.X);
                this._SVG.WriteAttributeInt32AddHalf("y2", pp2.Y);
            }
            else
            {
                this._SVG.WriteAttributeInt32("x1", pp1.X);
                this._SVG.WriteAttributeInt32("y1", pp1.Y);
                this._SVG.WriteAttributeInt32("x2", pp2.X);
                this._SVG.WriteAttributeInt32("y2", pp2.Y);
            }
            //this._SVG.WriteAttributeInt32("x1", pp1.X);
            //this._SVG.WriteAttributeInt32("x2", pp2.X);
            //if (pp1.Y == pp2.Y )
            //{
            //    // 横线
            //    this._SVG.WriteAttributeInt32("y1", pp1.Y + 1);
            //    this._SVG.WriteAttributeInt32("y2", pp2.Y + 1);
            //}
            //else
            //{
            //    this._SVG.WriteAttributeInt32("y1", pp1.Y);
            //    this._SVG.WriteAttributeInt32("y2", pp2.Y);
            //}
            this.WriteSVGClipID(strClipID);
            this.WriteSVGPen(p);
            this._SVG.WriteEndElement();
            return;
        }
        public void DrawLines(Pen p, PointF[] ps)
        {
            if (ps == null || ps.Length < 2)
            {
                return;
            }
            float maxx = 0;
            float maxy = 0;
            float minx = 0;
            float miny = 0;
            for (var iCount = 0; iCount < ps.Length; iCount++)
            {
                var pl = this.ToLocalPointFloat(ps[iCount].X, ps[iCount].Y);
                ps[iCount] = pl;
                if (iCount == 0 || maxx < pl.X) maxx = pl.X;
                if (iCount == 0 || minx > pl.X) minx = pl.X;
                if (iCount == 0 || maxy < pl.Y) maxy = pl.Y;
                if (iCount == 0 || miny > pl.Y) miny = pl.Y;
                if (this._PageContentHeight < maxy)
                {
                    this._PageContentHeight = maxy;
                }
            }
            minx--;
            miny--;
            maxx += 2;
            maxy += 2;
            this.CommitCurrentCharsForSVG();
            //var strClipID = this.GetSVGClipPathID(new RectangleF(minx, maxx - minx, miny, maxy - miny));
            this._SVG.WriteStartElement("path");
            var strLines = new StringBuilder();
            strLines.Append("M ");
            strLines.Append(DCXmlTextWriter.SingleToString(ps[0].X));
            strLines.Append(' ');
            strLines.Append(DCXmlTextWriter.SingleToString(ps[0].Y));
            for (var iCount = 1; iCount < ps.Length; iCount++)
            {
                strLines.Append(" L ");
                strLines.Append(DCXmlTextWriter.SingleToString(ps[iCount].X));
                strLines.Append(' ');
                strLines.Append(DCXmlTextWriter.SingleToString(ps[iCount].Y));
            }
            this._SVG.WriteAttributeString("fill", "none");
            this._SVG.WriteAttributeString("d", strLines.ToString());
            //this.WriteSVGClipID(strClipID);
            this.WriteSVGPen(p);
            this._SVG.WriteEndElement();
        }
        public void DrawImage(Image img, float x, float y, float width, float height, byte[] rawData)
        {
            this.CommitCurrentCharsForSVG();
            if (rawData == null || rawData.Length == 0)
            {
                if (img != null)
                {
                    if (img is Bitmap)
                    {
                        rawData = ((Bitmap)img).Data;
                    }
                    else
                    {
                        var ms2 = new System.IO.MemoryStream();
                        img.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                        rawData = ms2.ToArray();
                    }
                }
                else
                {
                    throw new ArgumentNullException("img");
                }
            }
            var rect = this.ToLocalRectangle(x, y, width, height);
            this._SVG.WriteStartElement("image");
            this.WriteSVGBounds(rect);

            this._SVG.WriteAttributeImageData("href", rawData);
            //this._SVG.WriteAttributeStringRaw("href", XImageValue.StaticGetEmitImageSource(rawData));
            this._SVG.WriteAttributeString("decoding", "sync");

            //wyc20250319:针对图片元数据与图片元素宽高比差距较大的情况下，输出preserveAspectRatio属性使其显示正常DUWRITER5_0-4162
            if (img != null)
            {
                float originrate = (float)img.Width / (float)img.Height;
                float nowrate = width / height;
                if (Math.Abs(originrate - nowrate) > 0.01)
                {
                    this._SVG.WriteAttributeString("preserveAspectRatio", "none");
                }
            }
            this._SVG.WriteEndElement();
        }
        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit , byte[] rawData)
        {
            this.CommitCurrentCharsForSVG();
            if (rawData == null || rawData.Length == 0)
            {
                if (image != null)
                {
                    if (image is Bitmap)
                    {
                        rawData = ((Bitmap)image).Data;
                    }
                    else
                    {
                        var ms2 = new System.IO.MemoryStream();
                        image.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                        rawData = ms2.ToArray();
                    }
                }
                else
                {
                    throw new ArgumentNullException("image");
                }
            }

            var rect = this.ToLocalRectangleFloat(destRect.X, destRect.Y, destRect.Width, destRect.Height);
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                return;
            }
            if (this._PageContentHeight < rect.Bottom)
            {
                this._PageContentHeight = rect.Bottom;
            }

            float srcX = GraphicsUnitConvert.Convert(srcRect.X, srcUnit, GraphicsUnit.Pixel);
            float srcY = GraphicsUnitConvert.Convert(srcRect.Y, srcUnit, GraphicsUnit.Pixel);
            float srcW = GraphicsUnitConvert.Convert(srcRect.Width, srcUnit, GraphicsUnit.Pixel);
            float srcH = GraphicsUnitConvert.Convert(srcRect.Height, srcUnit, GraphicsUnit.Pixel);
            if (srcW <= 0 || srcH <= 0)
            {
                return;
            }

            float imgWidth = image != null ? image.Width : srcX + srcW;
            float imgHeight = image != null ? image.Height : srcY + srcH;
            if (imgWidth <= 0 || imgHeight <= 0)
            {
                return;
            }

            float scaleX = rect.Width / srcW;
            float scaleY = rect.Height / srcH;
            float drawX = rect.X - srcX * scaleX;
            float drawY = rect.Y - srcY * scaleY;
            float drawWidth = imgWidth * scaleX;
            float drawHeight = imgHeight * scaleY;

            var clipId = "dcimgcp_" + (_ClipRectangleEntryIndex++);
            this._SVG.WriteStartElement("clippath");
            this._SVG.WriteAttributeString("id", clipId);
            this._SVG.WriteStartElement("rect");
            this._SVG.WriteAttributeSingle("x", rect.X);
            this._SVG.WriteAttributeSingle("y", rect.Y);
            this._SVG.WriteAttributeSingle("width", rect.Width);
            this._SVG.WriteAttributeSingle("height", rect.Height);
            this._SVG.WriteEndElement();
            this._SVG.WriteEndElement();

            var outerClipId = this.GetSVGClipPathID(rect);
            this._SVG.WriteStartElement("g");
            this.WriteSVGClipID(outerClipId);
            this._SVG.WriteStartElement("g");
            this._SVG.WriteAttributeString("clip-path", "url(#" + clipId + ")");

            this._SVG.WriteStartElement("image");
            this._SVG.WriteAttributeSingle("x", drawX);
            this._SVG.WriteAttributeSingle("y", drawY);
            this._SVG.WriteAttributeSingle("width", drawWidth);
            this._SVG.WriteAttributeSingle("height", drawHeight);
            this._SVG.WriteAttributeImageData("href", rawData);
            this._SVG.WriteAttributeString("decoding", "sync");
            this._SVG.WriteAttributeString("preserveAspectRatio", "none");
            this._SVG.WriteEndElement();

            this._SVG.WriteEndElement();
            this._SVG.WriteEndElement();
        }
        private Dictionary<string, FontItem> _FontItems = null;
        private int GetFontID(string fontName, string text)
        {
            if (this._FontItems == null)
            {
                this._FontItems = new Dictionary<string, FontItem>();
            }
            FontItem item = null;
            if (this._FontItems.TryGetValue(fontName.ToLower(), out item) == false)
            {
                item = new FontItem();
                item.FontName = fontName;
                item.ID = this.AllocUnitID();
                this._FontItems[fontName.ToLower()] = item;
            }
            return item.ID;
        }
        private class FontItem
        {
            public int ID = 0;
            public string FontName = null;
        }

        //public delegate RectangleF[] LayoutStringHandler(
        //    GraphicsUnit pageUnit,
        //    string text,
        //    string fontName,
        //    float fontSize,
        //    FontStyle vStyle,
        //    RectangleF bounds,
        //    StringFormat format,
        //    out string[] lines);
        ///// <summary>
        ///// 多行文本排版事件
        ///// </summary>
        //public static LayoutStringHandler EventLayoutString = DCSoft.Writer.Dom.CharacterMeasurer.LayoutString;


        private int _CurrentCharStyleIndex = -100;

        private string _CurrentCharFontName = null;
        private float _CurrentCharFontSize = 0;
        private FontStyle _CurrentCharFontStyle = FontStyle.Regular;
        private Color _CurrentCharTextColor = Color.Black;
        private float _CurrentCharY = 0;
        private List<float> _CurrentCharsX = new List<float>();
        private List<char> _CurrentChars = new List<char>();

        private static char[] _Buffer_CurrentChars = new char[40];

        internal void CommitCurrentCharsForSVG()
        {
            if (this._CurrentChars == null || this._CurrentChars.Count == 0)
            {
                return;
            }
            this._SVG.WriteStartElement("text");
            this._SVG.WriteStartAttribute("x");
            this._SVG.AutoComplete(DCXmlTextWriter.Token.Content);
            var charCount = this._CurrentCharsX.Count;
            var bw = this._SVG.BaseTextWriter;
            for (var iCount = 0; iCount < charCount; iCount++)
            {
                var lx = this.ToLocalPoint(this._CurrentCharsX[iCount], 0).X;
                var len = DCXmlTextWriter.StaticAppendInt32(_Buffer_CurrentChars, 0, lx);
                if (iCount < charCount - 1)
                {
                    _Buffer_CurrentChars[len++] = ' ';
                }
                bw.Write(_Buffer_CurrentChars, 0, len);
            }
            this._SVG.WriteEndAttribute();
            this._SVG.WriteAttributeInt32UseLastValue("y", this.ToLocalPoint(0, this._CurrentCharY).Y);
            this.WriteSVGFont(
                this._CurrentCharFontName,
                this._CurrentCharFontSize,
                this._CurrentCharFontStyle,
                this._CurrentCharTextColor);
            this._SVG.AutoComplete(DCXmlTextWriter.Token.Content);
            bw.Write(this._CurrentChars.ToArray(), 0, this._CurrentChars.Count);
            this._SVG.WriteEndElement();
            this._CurrentChars.Clear();
            this._CurrentCharsX.Clear();
            this._CurrentCharFontName = null;
        }

        //添加是否输出空格，默认不输出空格 DUWRITER5_0-3751
        internal static bool OutputSVGWhitespace = false;

        public void FastDrawChar(
            char cv,
            int styleIndex,
            string fontName,
            float fontSize,
            FontStyle vStyle,
            Color txtColor,
            float x,
            float y,
            float fontHeight)
        {
            //if (this._SVG != null)
            {
                var bolWhitespace = cv == ' ' || cv == '\t' || DCTextUtils.IsHtmlWhitespace(cv);
                if (bolWhitespace && OutputSVGWhitespace == false)
                {
                    // 不输出空白字符
                    return;
                }
                if (bolWhitespace || DCXmlCharType.IsAttributeValueChar(cv))
                {
                    if (this._CurrentChars.Count > 0)
                    {
                        if (this._CurrentCharFontName != fontName
                            || this._CurrentCharFontSize != fontSize
                            || this._CurrentCharFontStyle != vStyle
                            || this._CurrentCharTextColor != txtColor
                            || this._CurrentCharY != y
                            || this._CurrentCharStyleIndex != styleIndex)
                        {
                            this.CommitCurrentCharsForSVG();
                        }
                    }
                    if (this._CurrentChars.Count == 0)
                    {
                        this._CurrentCharFontName = fontName;
                        this._CurrentCharFontSize = fontSize;
                        this._CurrentCharFontStyle = vStyle;
                        this._CurrentCharTextColor = txtColor;
                        this._CurrentCharY = y;
                        this._CurrentCharStyleIndex = styleIndex;
                    }
                    this._CurrentCharsX.Add(x);
                    if (bolWhitespace && this._SVG != null)
                    {
                        this._CurrentChars.Add('&');
                        this._CurrentChars.Add('n');
                        this._CurrentChars.Add('b');
                        this._CurrentChars.Add('s');
                        this._CurrentChars.Add('p');
                        this._CurrentChars.Add(';');
                    }
                    else
                    {
                        this._CurrentChars.Add(cv);
                    }
                    return;
                }
                // 遇到特殊字符，直接绘制
                this.CommitCurrentCharsForSVG();
            }

            //string strClipID = GetSVGClipPathID(rect);
            //if( strClipID != null && text.Length == 1 )
            //{
            //    strClipID = GetSVGClipPathID(rect);
            //}
            //var back2 = this._SVG.EventBeforeWriteStartElement;
            //this._SVG.EventBeforeWriteStartElement = null;
            var p = this.ToLocalPoint(x, y);
            this._SVG.WriteStartElement("text");
            this._SVG.WriteAttributeInt32("x", p.X);
            this._SVG.WriteAttributeInt32UseLastValue("y", p.Y);
            if (p.Y > this._PageContentHeight)
            {
                this._PageContentHeight = p.Y;
            }
            //this._SVG.WriteAttributeString("dominant-baseline", "hanging");
            //this.WriteSVGPoint(p.X , p.Y - 2);
            if (fontHeight > 0)
            {
                var fh2 = this.ToLocalLength(fontHeight);
                if (this._PageContentHeight < fh2 + p.Y)
                {
                    this._PageContentHeight = fh2 + p.Y;
                }
            }
            else if (this._PageContentHeight < p.Y)
            {
                this._PageContentHeight += p.Y;
            }
            this.WriteSVGFont(fontName, fontSize, vStyle, txtColor);
            this._SVG.WriteChar(cv);
            this._SVG.WriteEndElement();
        }
        public void DrawString(
            string text,
            string fontName,
            float fontSize,
            FontStyle vStyle,
            Color txtColor,
            RectangleF layoutRectangle,
            StringFormat format)
        {
            if (text == null || text.Length == 0)
            {
                return;
            }
            if (text.Length == 1 && text[0] == ' ')
            {
                // 不绘制空格
                return;
            }
            this.CommitCurrentCharsForSVG();
            if (format == null)
            {
                format = StringFormat.GenericTypographic;
            }
            var info = TrueTypeFontSnapshort.GetInstance(fontName, vStyle);
            if (info == null || info.SupportAllChars(text) == false)
            {
                info = DefaultTrueTypeFont;
                if (info == null)
                {
                    throw new InvalidOperationException("DefaultTrueTypeFont=null");
                }
                fontName = info.FontName;
            }
            if (DCTextUtils.IsSingleCharCheckHighSurrogate(text))
            {
                if (format.Alignment == StringAlignment.Near
                    && format.LineAlignment == StringAlignment.Near)
                {
                    // 绘制单个字符
                    this.DrawSingleLineText(
                        text,
                        fontName,
                        fontSize,
                        vStyle,
                        txtColor,
                        layoutRectangle.Left,
                        layoutRectangle.Top,
                        layoutRectangle.Width,
                        layoutRectangle.Height,
                        info);
                    return;
                }
            }

            string[] strLines = null;

            var rects = CharacterMeasurer.LayoutString(
                this._PageUnit,
                text,
                fontName,
                fontSize,
                vStyle,
                layoutRectangle,
                format,
                out strLines);
            if (rects == null || rects.Length == 0)
            {
                return;
            }
            for (var iCount = 0; iCount < rects.Length; iCount++)
            {
                var rect = rects[iCount];
                //this.DrawRectangle(Pens.Blue, rect.Left, rect.Top, rect.Width, rect.Height,0);
                this.DrawSingleLineText(
                    strLines[iCount],
                    fontName,
                    fontSize,
                    vStyle,
                    txtColor,
                    rect.Left,
                    rect.Top,
                    rect.Width,
                    rect.Height,
                    info);
            }
        }

        public static TrueTypeFontSnapshort DefaultTrueTypeFont = TrueTypeFontSnapshort.GetInstance("宋体", FontStyle.Regular);

        //private static int _FontStyleIndexFix = Math.Abs((int)(new DateTime().Ticks) % 1000);
        private static string ToCSSString(string fontName, float fontSize, FontStyle vStyle)
        {
            var str = new StringBuilder();
            str.Append("dominant-baseline:text-before-edge");
            if (fontName != null && fontName.Length > 0)
            {
                str.Append(";font-family:" + fontName);
            }
            else
            {
                str.Append(";font-family:" + SystemFonts.DefaultFontName);
            }
            if (fontSize > 0)
            {
                str.Append(";font-size:" + fontSize + "pt");
            }
            if ((vStyle & FontStyle.Italic) == FontStyle.Italic)
            {
                str.Append(";font-style:italic");
            }
            else
            {
                str.Append(";font-style:normal");
            }
            if ((vStyle & FontStyle.Bold) == FontStyle.Bold)
            {
                str.Append(";font-weight:bold");
            }
            else
            {
                str.Append(";font-weight:normal");
            }
            return str.ToString();
        }


        private static int _ClipRectangleEntryIndex = 0;
        private string GetSVGClipPathID(RectangleF localRect)
        {
            if (this._ClipRectangle.IsEmpty == false
                && this._ClipRectangle.Contains(localRect) == false)
            {
                var strID = "dccp_" + (_ClipRectangleEntryIndex++);
                this._SVG.WriteStartElement("clippath");
                this._SVG.WriteAttributeString("id", strID);
                this._SVG.WriteStartElement("rect");
                var rect2 = DCValueConvert.ToInt32(this._ClipRectangle);
                this._SVG.WriteAttributeInt32("x", rect2.Left - 1);
                this._SVG.WriteAttributeInt32("y", rect2.Top - 1);
                this._SVG.WriteAttributeInt32("width", rect2.Width + 2);
                this._SVG.WriteAttributeInt32("height", rect2.Height + 2);
                this._SVG.WriteEndElement();
                this._SVG.WriteEndElement();
                return strID;
            }
            return null;
        }
        private void WriteSVGClipID(string strPathID)
        {
            if (strPathID != null && strPathID.Length > 0)
            {
                this._SVG.WriteAttributeString("clip-path", "url(#" + strPathID + ")");
            }
        }
        //private string GetSVGClassName( string vFontName , float vFontSize , FontStyle vStyle )
        //{
        //    if (this._SVGFontStyles != null && this._SVGFontStyles.Count > 0)
        //    {
        //        var f2 = new MyPDFFontInfo(vFontName , vFontSize, vStyle);
        //        foreach (var item2 in this._SVGFontStyles)
        //        {
        //            if (item2.EqualsValue(f2))
        //            {
        //                // 命中字体样式
        //                return "dcf_" + item2.UserValue.ToString();
        //            }
        //        }
        //    }
        //    return null;
        //}
        /// <summary>
        /// 表示字体定义信息的对象
        /// </summary>
        [System.Runtime.InteropServices.ComVisible(false)]
        public class MyPDFFontInfo : IDisposable
        {


            public MyPDFFontInfo(Font f) : this(f.Name, f.Size, f.Style, f.Unit)
            {
            }

            public MyPDFFontInfo(string fName, float si, FontStyle st = FontStyle.Regular, GraphicsUnit u = GraphicsUnit.Point)
            {
                this.Name = fName;
                this.Size = si;
                this.Style = st;
                this.Unit = u;
                this._HashCode = fName.GetHashCode();
                this._HashCode += si.GetHashCode();
                this._HashCode += (int)st;
                this._HashCode += 10 * (int)u;
            }
            public string UserID = null;

            public bool EqualsValue(string vFontName, float vFontSize, FontStyle vStyle)
            {
                return this.Name == vFontName && this.Size == vFontSize && this.Style == vStyle;
            }
            public bool EqualsValue(MyPDFFontInfo f)
            {
                if (f == null)
                {
                    return false;
                }
                if (f == this)
                {
                    return true;
                }
                if (f._HashCode == this._HashCode)
                {
                    if (this.Name == f.Name && this.Size == f.Size && this.Style == f.Style)
                    {
                        return true;
                    }
                }
                return false;
            }
            //public MyPDFFontInfo Clone()
            //{
            //    return (MyPDFFontInfo)this.MemberwiseClone();
            //}
            public override string ToString()
            {
                return this.Name + " " + this.Size + " " + this.Style;
            }

            public void Dispose()
            {
                if (this._Value != null)
                {
                    this._Value.Dispose();
                    this._Value = null;
                }
                //this._Family = null;
            }

            private Font _Value = null;
            public Font Value
            {
                get
                {
                    if (_Value == null)
                    {
                        _Value = new Font(this.Name, this.Size, this.Style, this.Unit);
                    }
                    return _Value;
                }
            }

            public readonly string Name;
            public readonly float Size;
            public readonly FontStyle Style;
            public bool Italic
            {
                get
                {
                    return (this.Style & FontStyle.Italic) == FontStyle.Italic;
                }
            }
            public bool Bold
            {
                get
                {
                    return (this.Style & FontStyle.Bold) == FontStyle.Bold;
                }
            }
            public readonly GraphicsUnit Unit;
            private readonly int _HashCode;

            public override int GetHashCode()
            {
                return this._HashCode;
            }

            public override bool Equals(object obj)
            {
                if (obj == this)
                {
                    return true;
                }
                var info = (MyPDFFontInfo)obj;
                return this.Name == info.Name
                    && this.Size == info.Size
                    && this.Style == info.Style
                    && this.Unit == info.Unit;
            }
        }

        private List<MyPDFFontInfo> _SVGFontStyles = null;
        //public void PrepareAddFont(XFontValue f)
        //{
        //    if (f != null)
        //    {
        //        var info = new MyPDFFontInfo(f.Name, f.Size, f.Style);
        //        if (this._SVGFontStyles == null)
        //        {
        //            this._SVGFontStyles = new List<MyPDFFontInfo>();
        //        }
        //        foreach (var item in this._SVGFontStyles)
        //        {
        //            if (item.EqualsValue(info))
        //            {
        //                return;
        //            }
        //        }
        //        this._SVGFontStyles.Add(info);
        //    }
        //}
        //private string GetSVFFontClassName(string vFontName, float vFontSize, FontStyle vStyle, Color txtColor)
        //{
        //    if (this._SVGFontStyles != null
        //        && this._SVGFontStyles.Count > 0
        //        && txtColor.ToArgb() == _Black_ARGB)
        //    {
        //        //var f2 = new MyPDFFontInfo(vFontName, vFontSize, vStyle);
        //        foreach (var item2 in this._SVGFontStyles)
        //        {
        //            if (item2.EqualsValue(vFontName, vFontSize, vStyle))
        //            {
        //                // 命中字体样式
        //                return item2.UserID;
        //            }
        //        }
        //    }
        //    return null;
        //}
        private void WriteSVGFont(string vFontName, float vFontSize, FontStyle vStyle, Color txtColor)
        {
            var bolWriteCss = true;
            if (this._SVGFontStyles != null && this._SVGFontStyles.Count > 0)
            {
                //var f2 = new MyPDFFontInfo(vFontName, vFontSize, vStyle);
                foreach (var item2 in this._SVGFontStyles)
                {
                    if (item2.EqualsValue(vFontName, vFontSize, vStyle))
                    {
                        this._SVG.WriteAttributeStringRaw("class", item2.UserID);
                        bolWriteCss = false;
                        break;
                    }
                }
                var info2 = new MyPDFFontInfo(vFontName, vFontSize, vStyle);
                info2.UserID = "dcf_" + this.AllocUnitID();
                this._SVGFontStyles.Add(info2);
                this._SVG.WriteAttributeStringRaw("class", info2.UserID);
                bolWriteCss = false;
                return;
            }

            if (bolWriteCss)
            {
                this._SVG.WriteAttributeStringRaw(
                    "style",
                    ToCSSString(vFontName, vFontSize, vStyle)
                    + ";fill:" + ColorTranslator.ToHtml(txtColor));
            }
            else if (txtColor.ToArgb() != _Black_ARGB)
            {
                // 非黑色文字
                this._SVG.WriteAttributeStringRaw("style", "fill:" + ColorTranslator.ToHtml(txtColor));
            }
        }

        private void DrawSingleLineText(
            string text,
            string fontName,
            float fontSize,
            FontStyle vStyle,
             Color txtColor,
            float x,
            float y,
            float w,
            float h,
            TrueTypeFontSnapshort info)
        {

            //if(SpecifyDebugMode)
            //{
            //    Console.WriteLine("Text:" + text + ",FontName:" + fontName + ",FontSize:" + fontSize + ",Info:" + info.FontName);
            //}
            RectangleF rect = this.ToLocalRectangleFloat(x, y, w, h);
            //var strClipID = GetSVGClipPathID(rect);
            //if( strClipID != null && text.Length == 1 )
            //{
            //    strClipID = GetSVGClipPathID(rect);
            //}
            this._SVG.WriteStartElement("text");
            if (this._PageContentHeight < rect.Bottom)
            {
                this._PageContentHeight = rect.Bottom;
            }
            var bolHasScaleTransform = this._Transform.HasScale();
            if (text.Contains(' ', StringComparison.Ordinal))
            {
                // 文本出现空格，则需要特殊处理，来体现空格的排版宽度
                var xScaleRate = this._Transform.Element0;
                if (xScaleRate <= 0)
                {
                    xScaleRate = 1;
                }
                var yScaleRate = this._Transform.Element3;
                if (yScaleRate <= 0)
                {
                    yScaleRate = 1;
                }
                var txtLen = text.Length;
                var strXPos = new StringBuilder();
                var leftCount = rect.X;
                var strNewText = new StringBuilder();
                for (var iCount = 0; iCount < txtLen; iCount++)
                {
                    var cv = text[iCount];
                    if (DCTextUtils.IsHighSurrogate(cv))
                    {
                        // UNICODE代理
                        strNewText.Append(cv);
                        strNewText.Append(text[iCount + 1]);
                        iCount++;
                        strXPos.Append(Math.Round(leftCount / xScaleRate));
                        strXPos.Append(' ');
                        leftCount += info.GetChineseWidth(fontSize, this._PageUnit) * this._UnitConvertRate;
                    }
                    else
                    {
                        if (cv == ' ' || DCTextUtils.IsHtmlWhitespace(cv))
                        {
                            // 不输出空格字符，只让它占个位置
                            leftCount += info.GetCharWidth(cv, fontSize, this._PageUnit) * this._UnitConvertRate;
                            continue;
                        }
                        strNewText.Append(cv);
                        strXPos.Append(Math.Round(leftCount / xScaleRate));
                        strXPos.Append(' ');
                        leftCount += info.GetCharWidth(cv, fontSize, this._PageUnit) * this._UnitConvertRate;
                    }
                }

                //wyc20241016:0宽度防止报错
                if (strXPos.Length > 0)
                {
                    strXPos.Remove(strXPos.Length - 1, 1);
                }

                text = strNewText.ToString();
                this._SVG.WriteAttributeStringRaw("x", strXPos.ToString());
                this._SVG.WriteAttributeInt32("y", (int)Math.Round(rect.Y / yScaleRate));
            }
            else
            {
                if (bolHasScaleTransform)
                {
                    this._SVG.WriteAttributeInt32("x", (int)(rect.X / this._Transform.Element0));
                    this._SVG.WriteAttributeInt32("y", (int)Math.Round(rect.Y / this._Transform.Element3));
                }
                else
                {
                    this._SVG.WriteAttributeInt32("x", (int)rect.X);
                    this._SVG.WriteAttributeInt32("y", (int)Math.Round(rect.Y));
                }
                //this._SVG.WriteAttributeString("dominant-baseline", "hanging");
                //this.WriteSVGPoint(rect.X, rect.Y - 1);//wyc20240819:纵坐标减2防止痕迹文本压线的问题DUWRITER5_0-3416

                //this._SVG.WriteAttributeString("x", rect.X.ToString());
                //this._SVG.WriteAttributeString("y", rect.Y.ToString());
                if (DCTextUtils.IsSingleCharCheckHighSurrogate(text) == false)
                {
                    this._SVG.WriteAttributeSingle("textLength", rect.Width);
                }
            }
            if (bolHasScaleTransform)
            {
                this._SVG.WriteAttributeStringRaw(
                    "transform",
                    "scale(" + this._Transform.Element0 + " " + this._Transform.Element3 + ")");
            }
            //this.WriteSVGClipID(strClipID);
            var bolWriteFont = true;

            if (bolWriteFont)
            {
                this.WriteSVGFont(fontName, fontSize, vStyle, txtColor);
            }
            this._SVG.WriteString(text);
            this._SVG.WriteEndElement();
        }
        private class AbbreviatedData
        {
            public static void AddCode(StringBuilder str, char c)
            {
                if (str.Length > 0)
                {
                    str.Append(' ');
                }
                str.Append(c);
            }
            public static void AddValue(StringBuilder str, float v)
            {
                if (str.Length > 0)
                {
                    str.Append(' ');
                }
                str.Append(v.ToString());
            }


            public static void AddArcPath(StringBuilder str, float x, float y, float width, float height, float angle)
            {

            }
            /// <summary>
            /// 获得一个扇形区域的OFD路径字符串
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <param name="startAngle"></param>
            /// <param name="endAngle"></param>
            /// <returns></returns>
            public static string GetArc(float x, float y, float width, float height, float startAngle, float endAngle)
            {
                var str = new StringBuilder();
                var centerX = x + width / 2.0f;
                var centerY = y + height / 2.0f;
                var radiusX = width / 2.0f;
                var radiusY = height / 2.0f;

                // 计算起点和终点坐标（角度转弧度）
                var startRad = startAngle * Math.PI / 180.0f;
                var endRad = endAngle * Math.PI / 180.0f;

                var startX = centerX + radiusX * Math.Cos(startRad);
                var startY = centerY + radiusY * Math.Sin(startRad);
                var endX = centerX + radiusX * Math.Cos(endRad);
                var endY = centerY + radiusY * Math.Sin(endRad);

                // 判断是否是大弧（角度差超过180度）
                int largeArcFlag = (endAngle - startAngle) % 360 <= 180 ? 0 : 1;
                str.Append("S 0 0 ");
                AddCode(str, 'M');
                AddValue(str, centerX);
                AddValue(str, centerY);
                AddCode(str, 'L');
                AddValue(str, (float)startX);
                AddValue(str, (float)startY);
                AddCode(str, 'A');
                AddValue(str, radiusX);
                AddValue(str, radiusY);
                str.Append(" 0 ");
                AddValue(str, largeArcFlag);
                str.Append(" 1 ");
                AddValue(str, (float)endX);
                AddValue(str, (float)endY);
                AddCode(str, 'Z');
                var txt = str.ToString();
                return txt;
            }
            public static string GetLine(float x1, float y1, float x2, float y2, float boundaryX, float boundaryY, float boundaryWidth, float boundaryHeight)
            {
                var str = new StringBuilder();
                AddCode(str, 'M');
                AddValue(str, x1 - boundaryX);
                AddValue(str, y1 - boundaryY);
                AddCode(str, 'L');
                AddValue(str, x2 - boundaryX);
                AddValue(str, y2 - boundaryY);
                var txt = str.ToString();
                return txt;
            }
            public static string GetLines(PointF[] ps, float boundsX, float boundsY, bool bolClosePath)
            {
                if (ps == null || ps.Length <= 1)
                {
                    return string.Empty;
                }
                var str = new StringBuilder();
                AddCode(str, 'S');
                AddValue(str, ps[0].X - boundsX);
                AddValue(str, ps[1].Y - boundsY);
                for (var iCount = 1; iCount < ps.Length; iCount++)
                {
                    AddCode(str, 'L');
                    AddValue(str, ps[iCount].X - boundsX);
                    AddValue(str, ps[iCount].Y - boundsY);
                }
                if (bolClosePath)
                {
                    AddCode(str, 'C');
                }
                var txt = str.ToString();
                return txt;
            }

            public static string GetRectangle(float x, float y, float w, float h)
            {
                var str = new StringBuilder();
                AddCode(str, 'S');
                AddValue(str, x);
                AddValue(str, y);
                AddCode(str, 'L');
                AddValue(str, x + w);
                AddValue(str, y);
                AddCode(str, 'L');
                AddValue(str, x + w);
                AddValue(str, y + h);
                AddCode(str, 'L');
                AddValue(str, x);
                AddValue(str, y + h);
                AddCode(str, 'C');
                var txt = str.ToString();
                return txt;
            }
            public static string GetEllipse(float x, float y, float width, float height)
            {
                var rx = width / 2.0f;
                var ry = height / 2.0f;
                var rightX = x + width;
                var centerY = y + ry;

                var str = new StringBuilder();
                AddCode(str, 'M');
                AddValue(str, x);
                AddValue(str, centerY);
                AddCode(str, 'A');
                AddValue(str, rx);
                AddValue(str, ry);
                str.Append(" 0 1 1 ");
                AddValue(str, rightX);
                AddValue(str, centerY);
                AddCode(str, 'A');
                AddValue(str, rx);
                AddValue(str, ry);
                str.Append(" 0 1 1 ");
                AddValue(str, x);
                AddValue(str, centerY);
                AddCode(str, 'C');
                var txt = str.ToString();
                return txt;
            }
        }

        /// <summary>
        /// 椭圆求点公式
        /// </summary>
        /// <param name="lpRect">椭圆边框</param>
        /// <param name="angle">角度</param>
        /// <returns></returns>
        public static PointF GetArcPoint(RectangleF lpRect, float angle)
        {
            double a = lpRect.Width / 2.0f;
            double b = lpRect.Height / 2.0f;
            if (a <= 0 || b <= 0) return new PointF(lpRect.X, lpRect.Y);

            //弧度
            double radian = angle * Math.PI / 180.0f;

            //获取弧度正弦值
            double yc = Math.Sin(radian);
            //获取弧度余弦值
            double xc = Math.Cos(radian);
            //获取曲率  r = ab/\Sqrt((a.Sinθ)^2+(b.Cosθ)^2
            double radio = (a * b) / Math.Sqrt(Math.Pow(yc * a, 2.0) + Math.Pow(xc * b, 2.0));

            //计算坐标
            double ax = radio * xc;
            double ay = radio * yc;
            var resultX = (float)(lpRect.X + a + ax);
            var resultY = (float)(lpRect.Y + b + ay);
            return new PointF(resultX, resultY);
        }
        private Point ToLocalPoint(float x, float y)
        {
            var p = this._Transform.TransformPointF(x + this._GlobalOffsetX, y + this._GlobalOffsetY);
            return new Point(
                (int)Math.Round(p.X * this._UnitConvertRate),
                (int)Math.Round(p.Y * this._UnitConvertRate));
        }
        private PointF ToLocalPointFloat(float x, float y)
        {
            var p = this._Transform.TransformPointF(x + this._GlobalOffsetX, y + this._GlobalOffsetY);
            p.X = FixDecimal(p.X * this._UnitConvertRate);
            p.Y = FixDecimal(p.Y * this._UnitConvertRate);
            return p;
            //var ps = new PointF[] { new PointF(x, y) };
            //this._Transform.TransformPoints(ps);
            //var p = ps[0];
            //p.X = p.X * this._UnitConvertRate;
            //p.Y = p.Y * this._UnitConvertRate;
            //return p;
        }
        private float ToLocalLength(float len)
        {
            return FixDecimal(len * this._UnitConvertRate);
        }

        private static float FixDecimal(float v)
        {
            var v2 = (int)(v * 100);
            return (float)v2 / 100;
        }
        public Rectangle ToLocalRectangle(float x, float y, float width, float height)
        {
            var p = this.ToLocalPointFloat(x, y);
            var vLeft = (int)Math.Round(p.X);
            var vTop = (int)Math.Round(p.Y);
            var vRight = (int)Math.Round(p.X + width * this._UnitConvertRate);
            var vBottom = (int)Math.Round(p.Y + height * this._UnitConvertRate);
            return new Rectangle(vLeft, vTop, vRight - vLeft, vBottom - vTop);
        }

        private RectangleF ToLocalRectangleFloat(float x, float y, float width, float height)
        {
            var p = this.ToLocalPointFloat(x, y);
            return new RectangleF(
                p.X,
                p.Y,
                FixDecimal(width * this._UnitConvertRate),
                FixDecimal(height * this._UnitConvertRate));
        }

        private float _LengthZoomRate = 1;
        public float LengthZoomRate
        {
            get { return this._LengthZoomRate; }
            set { this._LengthZoomRate = value; }
        }
    }
}