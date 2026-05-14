using System;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using DCSoft.Drawing;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;
using DCSoft.Common;

namespace DCSoft.Printing
{
    /// <summary>
    /// 页面设置对象
    /// </summary>
    [Serializable()]
    [System.ComponentModel.TypeConverter(typeof(XPageSettingsTypeConverter))]
#if WINFORM || DCWriterForWinFormNET6
    [System.ComponentModel.Editor(typeof(XPageSettingEditor), typeof(UITypeEditor))]
#endif
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    [DCSoft.Common.DCPublishAPI]
    [DCSoft.Common.DCXSD("PageSettings")]
    public partial class XPageSettings : ICloneable, IDisposable
    {
        //private static  Dictionary<PaperKind , Size > myStandardPaperSize = null;
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static XPageSettings()
        {
            
        }
    
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XPageSettings()
        {
        }

#if !DCWriterForWASM
        /// <summary>
        /// 纸张大小,本属性用于整体的设置纸张大小。不能使用该属性下面的属性。
        /// </summary>
        /// <remarks>
        /// 本属性用于整体的设置纸张大小。不能使用该属性下面的属性。
        /// 比如 instance.PaperSize.Width = 600,这样做是不对的。
        /// 应该使用 instance.PaperWidth = 600。
        /// </remarks>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public PaperSize PaperSize
        {
            get
            {
                return new PaperSize(
                    this.PaperKind.ToString(),
                    this.PaperWidth,
                    this.PaperHeight);
            }
            set
            {
                intPaperKind = value.Kind;
                intPaperWidth = value.Width;
                intPaperHeight = value.Height;
            }
        }
#endif
        private System.Drawing.Printing.PaperKind intPaperKind = System.Drawing.Printing.PaperKind.A4;
        /// <summary>
        /// 纸张尺寸类型
        /// </summary>
        [System.ComponentModel.DefaultValue(PaperKind.A4)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Printing.PaperKind PaperKind
        {
            get
            {
                return intPaperKind;
            }
            set
            {
                intPaperKind = value;
                //if (myStandardPaperSize.ContainsKey(intPaperKind))
                //{
                //    Size size = (Size)myStandardPaperSize[intPaperKind];
                //    intPaperWidth = size.Width;
                //    intPaperHeight = size.Height;
                //}
            }
        }

        
       
        private int _DesignerPaperWidth = 0;
        /// <summary>
        /// 设计器纸张宽度,单位百分之一英寸
        /// </summary>
        [DefaultValue(0)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public int DesignerPaperWidth
        {
            get
            {
                return _DesignerPaperWidth;
            }
            set
            {
                _DesignerPaperWidth = value;
            }
        }

        private int intPaperWidth = 827;
        /// <summary>
        /// 纸张宽度,单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(827)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int PaperWidth
        {
            get
            {
                if (intPaperKind != PaperKind.Custom)
                {
                    Size size = (Size)StandardPaperSizeInfo.GetStandardPaperSize(intPaperKind);
                    if (size.IsEmpty == false)
                    {
                        return size.Width;
                    }
                }
                return intPaperWidth;
            }
            set
            {
                DesignerPaperWidth = value;
                intPaperWidth = value;
            }
        }

        private int _DesignerPaperHeight = 0;
        /// <summary>
        /// 设计器纸张高度
        /// </summary>
        [DefaultValue(0)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public int DesignerPaperHeight
        {
            get { return _DesignerPaperHeight; }
            set { _DesignerPaperHeight = value; }
        }

        public Size GetPageLayoutSize()
        {
            if (intPaperKind != PaperKind.Custom)
            {
                Size size = StandardPaperSizeInfo.GetStandardPaperSize(intPaperKind);
                if (size.IsEmpty == false)
                {
                    return size;
                }
            }
            return new Size(intPaperWidth, intPaperHeight);// intPaperHeight;
        }

        private int intPaperHeight = 1169;
        /// <summary>
        /// 纸张高度 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(1169)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int PaperHeight
        {
            get
            {
                if (intPaperKind != PaperKind.Custom)
                {
                    Size size = StandardPaperSizeInfo.GetStandardPaperSize(intPaperKind);
                    if (size.IsEmpty == false)
                    {
                        return size.Height;
                    }
                }
                return intPaperHeight;
            }
            set
            {
                DesignerPaperHeight = value;
                intPaperHeight = value;
            }
        }

        private const int DefaultMarginValue = 100;
        [NonSerialized]
        private Margins _Margins = null;
        /// <summary>
        /// 页边距,单位百分之一英寸
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Margins Margins
        {
            get
            {
                if (_Margins == null)
                {
                    _Margins = new Margins(DefaultMarginValue, DefaultMarginValue, DefaultMarginValue, DefaultMarginValue);
                }
                return _Margins;
            }
            set
            {
                _Margins = value;
            }
        }

        /// <summary>
        /// 左页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int LeftMargin
        {
            get
            {
                if (this._Margins == null)
                {
                    return DefaultMarginValue;
                }
                else
                {
                    return this._Margins.Left;
                }
            }
            set
            {
                this.Margins.Left = value;
            }
        }

        /// <summary>
        /// 顶页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int TopMargin
        {
            get
            {
                if (this._Margins == null)
                {
                    return DefaultMarginValue;
                }
                else
                {
                    return this._Margins.Top;
                }
            }
            set
            {
                this.Margins.Top = value;
            }
        }

        /// <summary>
        /// 右页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int RightMargin
        {
            get
            {
                if (this._Margins == null)
                {
                    return DefaultMarginValue;
                }
                else
                {
                    return this._Margins.Right;
                }
            }
            set
            {
                this.Margins.Right = value;
            }
        }

        /// <summary>
        /// 底页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int BottomMargin
        {
            get
            {
                if (this._Margins == null)
                {
                    return DefaultMarginValue;
                }
                else
                {
                    return this._Margins.Bottom;
                }
            }
            set
            {
                this.Margins.Bottom = value;
            }
        }

        /// <summary>
        /// 厘米为单位的下页边距
        /// </summary>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.ComponentModel.DefaultValue(0)]
        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float BottomMarginInCM
        {
            get
            {
                return GraphicsUnitConvert.ConvertToCM(this.BottomMargin / 100f, GraphicsUnit.Inch);
            }
            set
            {
                this.BottomMargin = (int)(GraphicsUnitConvert.ConvertFromCM(value, GraphicsUnit.Inch) * 100f);
            }
        }
        /// <summary>
        /// 厘米为单位的左页边距
        /// </summary>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.ComponentModel.DefaultValue(0)]
        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float LeftMarginInCM
        {
            get
            {
                return GraphicsUnitConvert.ConvertToCM(this.LeftMargin / 100f, GraphicsUnit.Inch);
            }
            set
            {
                this.LeftMargin = (int)(GraphicsUnitConvert.ConvertFromCM(value, GraphicsUnit.Inch) * 100f);
            }
        }
        /// <summary>
        /// 厘米为单位的上页边距
        /// </summary>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.ComponentModel.DefaultValue(0)]
        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float TopMarginInCM
        {
            get
            {
                return GraphicsUnitConvert.ConvertToCM(this.TopMargin / 100f, GraphicsUnit.Inch);
            }
            set
            {
                this.TopMargin = (int)(GraphicsUnitConvert.ConvertFromCM(value, GraphicsUnit.Inch) * 100f);
            }
        }
        /// <summary>
        /// 厘米为单位的右页边距
        /// </summary>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.ComponentModel.DefaultValue(0)]
        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float RightMarginInCM
        {
            get
            {
                return GraphicsUnitConvert.ConvertToCM(this.RightMargin / 100f, GraphicsUnit.Inch);
            }
            set
            {
                this.RightMargin = (int)(GraphicsUnitConvert.ConvertFromCM(value, GraphicsUnit.Inch) * 100f);
            }
        }

        private bool _Landscape = false;
        /// <summary>
        /// 横向打印标记
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Landscape
        {
            get
            {
                return _Landscape;
            }
            set
            {
                _Landscape = value;
            }
        }
#if !DCWriterForWASM
        /// <summary>
        /// 将页面设置应用到打印机设置信息对象上
        /// </summary>
        /// <param name="printer"></param>
        public void ApplyPageSettings(System.Drawing.Printing.PrinterSettings printer)
        {
            if (printer == null)
            {
                throw new ArgumentNullException("printer");
            }
            if (this.PaperKind == System.Drawing.Printing.PaperKind.Custom)
            {
                if (this.Landscape == false)
                {
                    printer.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(
                        PaperSizeName_Custom,
                        this.PaperWidth,
                        this.PaperHeight);
                }
                else
                {
                    printer.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(
                        PaperSizeName_Custom,
                        this.PaperWidth,
                        this.PaperHeight);
                }
            }
           
        }

       
        public System.Drawing.Printing.PageSettings CreateStdPageSettings(System.Drawing.Printing.PrinterSettings pst)
        {
           
            {
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                if (intPaperKind == System.Drawing.Printing.PaperKind.Custom)
                {
                    if (this._Landscape == false)
                        ps.PaperSize = new System.Drawing.Printing.PaperSize(
                            PaperSizeName_Custom,
                            this.PaperWidth,
                            this.PaperHeight);
                    else
                        ps.PaperSize = new System.Drawing.Printing.PaperSize(
                            PaperSizeName_Custom,
                            this.PaperWidth,
                            this.PaperHeight);
                }
                else
                {
                    bool bolSet = false;
                    if (pst == null)
                    {
                        pst = new System.Drawing.Printing.PrinterSettings();
                    }
                    foreach (System.Drawing.Printing.PaperSize size in GetPageSizes(pst))
                    {
                        if (size.Kind == intPaperKind)
                        {
                            ps.PaperSize = size;
                            bolSet = true;
                            break;
                        }
                    }
                    if (bolSet == false)
                    {
                        if (this._Landscape == false)
                            ps.PaperSize = new System.Drawing.Printing.PaperSize(
                                PaperSizeName_Custom,
                                this.PaperWidth,
                                this.PaperHeight);
                        else
                            ps.PaperSize = new System.Drawing.Printing.PaperSize(
                                PaperSizeName_Custom,
                                this.PaperWidth,
                                this.PaperHeight);
                    }
                }
                if (pst != null)
                {
                    ps.PrinterSettings = pst;
                }
                ps.Margins = (Margins)this.Margins.Clone();
                ps.Landscape = this._Landscape;
                return ps;
            }
        }

       
#endif

        private GraphicsUnit _ViewUnit = GraphicsUnit.Document;
        /// <summary>
        /// 视图区域使用的度量单位
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]

        public GraphicsUnit ViewUnit
        {
            get
            {
                return _ViewUnit;
            }
            set
            {
                _ViewUnit = value;
            }
        }

        /// <summary>
        /// 视图单位的左页边距
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]

        public float ViewLeftMargin
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    this.LeftMargin,
                    GraphicsUnit.Document,
                    _ViewUnit) * 3;
            }
        }

        /// <summary>
        /// 视图单位的顶页边距
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]

        public float ViewTopMargin
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    this.TopMargin,
                    GraphicsUnit.Document,
                    this.ViewUnit) * 3;
            }
        }

        /// <summary>
        /// 视图单位的右页边距
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public float ViewRightMargin
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    this.RightMargin,
                    GraphicsUnit.Document,
                    this.ViewUnit) * 3;
            }
        }

        /// <summary>
        /// 视图单位的下页边距
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public float ViewBottomMargin
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    this.BottomMargin,
                    GraphicsUnit.Document,
                    this.ViewUnit) * 3;
            }
        }

        public SizeF GetViewPaperSize()
        {
            var psize = GetPageLayoutSize();
            var rate = (float)GraphicsUnitConvert.GetRate(GraphicsUnit.Document, this.ViewUnit) * 3;
            if (this.Landscape)
            {
                return new SizeF(psize.Height * rate, psize.Width * rate);
            }
            else
            {
                return new SizeF(psize.Width * rate, psize.Height * rate);
            }
        }

        /// <summary>
        /// 纸张的视图宽度
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public float ViewPaperWidth
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    this._Landscape ? this.PaperHeight : this.PaperWidth,
                    GraphicsUnit.Document,
                    this._ViewUnit)
                    * 3;
            }
        }

        /// <summary>
        /// 纸张的视图高度
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public float ViewPaperHeight
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    (float)(this.Landscape ? this.PaperWidth : this.PaperHeight),
                    GraphicsUnit.Document,
                    this.ViewUnit)
                    * 3.0f;
            }
        }

        public void InnerSetViewPaperHeight(float value)
        {
            int v = (int)(GraphicsUnitConvert.Convert(
                value,
                this.ViewUnit,
                GraphicsUnit.Document) / 3.0);
            if (this.Landscape)
            {
                this.PaperWidth = v;
            }
            else
            {
                this.PaperHeight = v;
            }
        }

        /// <summary>
        /// 纸张可打印的客户区域的宽度,单位 Document
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]

        public float ViewClientWidth
        {
            get
            {
                int w = this._Landscape ? this.PaperHeight : this.PaperWidth;
                w = w - this.LeftMargin - this.RightMargin;
                float result = (float)GraphicsUnitConvert.Convert(w, GraphicsUnit.Document, this._ViewUnit) * 3;
                
                return result;
            }
            
        }


        /// <summary>
        /// 纸张可打印的客户区域的高度
        /// </summary>
        [System.ComponentModel.Browsable(false)]

        public float ViewClientHeight
        {
            get
            {
                int h = this.Landscape ? this.PaperWidth : this.PaperHeight;
                h = h - this.TopMargin - this.BottomMargin;
                float result = (float)GraphicsUnitConvert.Convert(h, GraphicsUnit.Document, this.ViewUnit) * 3;
                
                return result;
            }
        }
         
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>

        object ICloneable.Clone()
        {
            XPageSettings ps = new XPageSettings();
            this.CopyTo(ps);
            return ps;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public XPageSettings Clone()
        {
            return (XPageSettings)((ICloneable)this).Clone();
        }

#if !DCWriterForWASM
        public void ApplyTo(System.Drawing.Printing.PageSettings ps)
        {
            if (ps == null)
            {
                throw new ArgumentNullException("ps");
            }
           
            
            ps.Landscape = this.Landscape;
        }
#endif
        private static readonly string PaperSizeName_Custom = "Custom";
#if !DCWriterForWASM
        internal static PaperSize GetCustomPaperSize(
            PageSettings ps,
            XPageSettings xps,
            bool forPrintPreview)
        {
            var paperWidth = xps.PaperWidth;
            var paperHeight = xps.PaperHeight;
           
            System.Drawing.Printing.PaperSize newSize = new PaperSize(
                        PaperSizeName_Custom,
                        paperWidth,
                        paperHeight);
            if (forPrintPreview)
            {
                // 对于打印预览，则直接返回结果.
                return newSize;
            }
            // 查找最佳匹配大小的已有纸张类型
            int maxValue = 10000;
            var pss = GetPageSizes(ps.PrinterSettings);
            foreach (System.Drawing.Printing.PaperSize mySize in pss)
            {
                //if (mySize.Kind == System.Drawing.Printing.PaperKind.Custom)
                {
                    int disW = Math.Abs(mySize.Width - paperWidth);
                    int disH = Math.Abs(mySize.Height - paperHeight);
                    if (disW + disH < maxValue)
                    {
                        newSize = mySize;
                        maxValue = disW + disH;
                    }
                }
            }
            if (maxValue > 70)
            {
                // 差距实在是太大了
                newSize = new PaperSize(
                        PaperSizeName_Custom,
                        paperWidth,
                        paperHeight);
            }
            // 没找到最匹配的
            return newSize;
        }
#endif

        public void CopyTo(XPageSettings ps)
        {
            if (ps != null)
            {
                ps.intPaperKind = intPaperKind;
                ps.intPaperWidth = intPaperWidth;
                ps.intPaperHeight = intPaperHeight;
                ps._Margins = (Margins)this.Margins.Clone();

                ps._Landscape = _Landscape;

                ps._ViewUnit = _ViewUnit;
                
            }
        }

        /// <summary>
        /// 解析文本获得页面设置信息,本方法能解析ToString()函数输出的文本
        /// </summary>
        /// <remarks >
        /// 本方法支持的文本格式为“纸张类型,Landscape,LeftMargin=整数,TopMargin=整数,RightMargin=整数,BttomMargin=整数,PrinterName=打印机名称,PaperSource=纸张来源,StickToPageSize=True/False,AutoPaperWidth”
        /// 文本中各个项目间用半角逗号分开,可以只设置某些属性，未指明的属性值采用默认值。
        /// </remarks>
        /// <param name="Value">要解析的文本</param>

        public void Parse(string Value)
        {
            if (Value == null)
            {
                return;
            }
            string[] items = Value.Split(',');
            foreach (string item in items)
            {
                if (Enum.IsDefined(typeof(System.Drawing.Printing.PaperKind), item))
                {
                    // 解析纸张类型
                    this.PaperKind = (PaperKind)Enum.Parse(typeof(PaperKind), item, true);
                }
                else
                {
                    string strName = item.Trim().ToLower();
                    string strValue = string.Empty;
                    int index = item.IndexOf('=');
                    if (index > 0)
                    {
                        strName = item.Substring(0, index).Trim().ToLower();
                        strValue = item.Substring(index + 1).Trim();
                    }
                    switch (strName)
                    {
                        
                        case "paperwidth":
                            // 纸张宽度
                            int w = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out w))
                            {
                                this.PaperWidth = w;
                            }
                            break;
                        case "paperheight":
                            // 纸张高度
                            int h = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out h))
                            {
                                this.PaperHeight = h;
                            }
                            break;
                        case "leftmargin":
                            // 左页边距
                            int left = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out left))
                            {
                                this.LeftMargin = left;
                            }
                            break;
                        case "topmargin":
                            // 上页边距
                            int top = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out top))
                            {
                                this.TopMargin = top;
                            }
                            break;
                        case "rightmargin":
                            // 右页边距
                            int right = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out right))
                            {
                                this.RightMargin = right;
                            }
                            break;
                        case "bottommargin":
                            // 下页边距
                            int bottom = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out bottom))
                            {
                                this.BottomMargin = bottom;
                            }
                            break;                       
                    }
                }
            }//foreach
        }
        private float ConvertToCM(float v)
        {
            return GraphicsUnitConvert.ConvertToCM(v * 3, GraphicsUnit.Document);
        }

        private void AddValue(StringBuilder str, string name)
        {
            if (str.Length > 0)
            {
                str.Append(',');
            }
            str.Append(name);
        }
        private void AddValue(StringBuilder str, string name, string v)
        {
            if (str.Length > 0)
            {
                str.Append(',');
            }
            str.Append(name);
            str.Append('=');
            str.Append(v);
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new StringBuilder();
            str.Append(intPaperKind.ToString());
            if (this.PaperKind == PaperKind.Custom)
            {
                AddValue(str, "PaperWidth", this.PaperWidth + "(" + ConvertToCM(this.PaperWidth).ToString("0.00") + "CM)");
                AddValue(str, "PaperHeight", this.PaperHeight + "(" + ConvertToCM(this.PaperHeight).ToString("0.00") + "CM)");
            }
            if (this.Landscape)
            {
                AddValue(str, "Landscape");
            }
            if (this.LeftMargin != 100)
            {
                AddValue(str, "LeftMargin", this.LeftMargin + "(" + ConvertToCM(this.LeftMargin).ToString("0.00") + "CM)");
            }
            if (this.TopMargin != 100)
            {
                AddValue(str, "TopMargin", this.TopMargin + "(" + ConvertToCM(this.TopMargin).ToString("0.00") + "CM)");
            }
            if (this.RightMargin != 100)
            {
                AddValue(str, "RightMargin", this.RightMargin + "(" + ConvertToCM(this.RightMargin).ToString("0.00") + "CM)");
            }
            if (this.BottomMargin != 100)
            {
                AddValue(str, "BottomMargin", this.BottomMargin + "(" + ConvertToCM(this.BottomMargin).ToString("0.00") + "CM)");
            }
            return str.ToString();
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public void Dispose()
        {
          
        }

#if !DCWriterForWASM
        private static Dictionary<string, PrinterSettings.PaperSizeCollection> _pageSizesBuffer
            = new Dictionary<string, PrinterSettings.PaperSizeCollection>();
        /// <summary>
        /// 获得打印机配置的纸张大小列表
        /// </summary>
        /// <remarks>
        /// 由于获得PrinterSettings.PaperSizes属性值耗时，在此做一个全局缓存，提高第二次的读取速度。
        /// </remarks>
        /// <param name="ps"></param>
        /// <returns></returns>
        internal static PrinterSettings.PaperSizeCollection GetPageSizes(PrinterSettings ps)
        {
            if (ps == null)
            {
                throw new ArgumentNullException("ps");
            }
            string name = ps.PrinterName;
            if (name == null)
            {
                name = string.Empty;
            }
            if (_pageSizesBuffer.ContainsKey(name))
            {
                return _pageSizesBuffer[name];
            }
            else
            {
                PrinterSettings.PaperSizeCollection result = ps.PaperSizes;
                _pageSizesBuffer[name] = result;
                return result;
            }
        }
#endif


    }//public class XPageSettings


#if WINFORM || DCWriterForWinFormNET6
    [System.Runtime.InteropServices.ComVisible(false)]
    public class XPageSettingEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object Value)
        {
            return Value;
        }
    }
#endif

    [System.Runtime.InteropServices.ComVisible(false)]
    public class XPageSettingsTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context,
            object Value,
            Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(XPageSettings), attributes).Sort(new string[]{
                "PaperKind" ,
                "PaperWidth" ,
                "PaperHeight",
                "Landscape" ,
                "LeftMargin",
                "TopMargin",
                "RightMargin" ,
                "BottomMargin",
                "PaperSource",
                "PrinterName"
                });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.Equals(typeof(string)))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object Value)
        {
            if (Value is string)
            {
                XPageSettings settings = new XPageSettings();
                settings.Parse((string)Value);
                return settings;
            }//if
            return base.ConvertFrom(context, culture, Value);
        }

    }

}