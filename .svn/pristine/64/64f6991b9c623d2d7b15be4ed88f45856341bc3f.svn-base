//wyc20240306 
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing.Printing;
using DCSoft.Common;
using DCSoft.Drawing;
using DCSoft.Printing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 体温信息文档对象
    /// </summary>
    ///<remarks>编制 袁永福</remarks>
#if !DCWriterForWASM
    [Serializable]
    [DCSoft.Common.DCPublishAPI]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
#endif
    public partial class TemperatureDocument
    {
#if !DCWriterForWASM
        /// <summary>
        /// 内核版本
        /// </summary>
        public const string CoreVersion = "2014-5-20";
        /// <summary>
        /// 程序集是否混淆加密
        /// </summary>
        [Browsable(false)]
        public static bool IsAssemblyObfuscation
        {
            get
            {
                return typeof(TempClass).Name != "TempClass";
            }
        }
        private class TempClass
        {
        }
        /// <summary>
        /// 时间轴文档绘制数据点的自定义图标事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [field:NonSerialized]
        public event DrawValuePointSymbolEventHandler EventDrawValuePointSymbol = null;
#endif

#if WINFORM || DCWriterForWinFormNET6
        /// <summary>
        /// 时间轴文档对象的绘制完毕事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [field:NonSerialized]
        public event System.Windows.Forms.PaintEventHandler EventAfterDrawDocument = null;
#endif

#if !DCWriterForWASM

        private List<char> _IllegalTextEndCharForLinux = null;
        internal string GetTextProcessedForLinux(string txt)
        {
            if (DCGraphicsForTimeLine.TimeLineRunInLinuxMode == false ||
                    this._IllegalTextEndCharForLinux.Count == 0 ||
                    txt == null || txt.Length == 0)
            {
                return txt;
            }

            if (this._IllegalTextEndCharForLinux.Contains(txt[txt.Length - 1]) == true)
            {
                return txt + ".";
            }
            else
            {
                return txt;
            }
        }
#endif

        /// <summary>
        /// 初始化对象
        /// </summary>
        public TemperatureDocument()
        {
#if ! DCWriterForWASM
            //DCSoft.Writer.DCWriterPublish.Start();

            UpdateSupportTimeLineViewMode();

            if (this.InnerBehaviorMode == DocumentBehaviorMode.DesignMode)
            {
                // 设计模式直接返回所有的标题行
                this._RuntimeHeaderLines = this.Config.HeaderLines;
                this._RuntimeFooterLines = this.Config.FooterLines;
            }
            if (_RuntimeHeaderLines == null)
            {
                _RuntimeHeaderLines = this.Config.HeaderLines.GetRuntimeInfos();
                _RuntimeFooterLines = this.Config.FooterLines.GetRuntimeInfos();
            }

            _IllegalTextEndCharForLinux = new List<char>();
#endif
        }

#if !DCWriterForWASM
        private static int _InstanceCount = 0;

        private int _InstanceIndex = _InstanceCount++;
        /// <summary>
        /// 对象实例编号
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int InstanceIndex
        {
            get
            {
                return _InstanceIndex;
            }
        }

        private DocumentBehaviorMode _BehaviorMode = DocumentBehaviorMode.Normal;
        /// <summary>
        /// 文档行为模式
        /// </summary>
        internal DocumentBehaviorMode InnerBehaviorMode
        {
            get
            {
                return _BehaviorMode;
            }
            set
            {
                _BehaviorMode = value;
            }
        }
        private bool _Modified = false;
        /// <summary>
        /// 文档数据是否发生改变标记
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Modified
        {
            get
            {
                return _Modified;
            }
            set
            {
                _Modified = value;
            }
        }
#endif
        //internal static string MyGetLongDateString(DateTime dtm)
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.Append(dtm.Year + "年" + dtm.Month + "月" + dtm.Day + "日");
        //    if (dtm.Hour > 0 && dtm.Millisecond > 0 && dtm.Second > 0)
        //    {

        //    }
        //    return str.ToString();
        //}
#if WINFORM || DCWriterForWinFormNET6
        [NonSerialized]
        private TemperatureControl _OwnerControl = null;
        /// <summary>
        /// 当前文档所属的时间轴控件对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [XmlIgnore]
        public TemperatureControl OwnerControl
        {
            get
            {
                return _OwnerControl;
            }
            set
            {
                this._OwnerControl = value;
            }
        }
#endif
        private TemperatureDocumentConfig _Config = null;
        /// <summary>
        /// 文档配置
        /// </summary>
        [DefaultValue(null)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocumentConfig Config
        {
            get
            {
                if (_Config == null)
                {
                    _Config = new TemperatureDocumentConfig();
                    _Config.CheckDefaultTicks();
                }
                return _Config;
            }
            set
            {
                this._Config = value;
#if !DCWriterForWASM

                this._SelectedObject = null;
                this.LayoutInvalidate = true;
#endif
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 检测阴影数据点的时钟秒数
        /// </summary>
        [DefaultValue(2000)]
        [System.Xml.Serialization.XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ShadowPointDetectSeconds
        {
            get
            {
                return this.Config.ShadowPointDetectSeconds;
            }
            set
            {
                this.Config.ShadowPointDetectSeconds = value;
            }
        }

        /// <summary>
        /// 图标像素宽度
        /// </summary>
        [DefaultValue(16)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ImagePixelWidth
        {
            get
            {
                return this.Config.ImagePixelWidth;
            }
            set
            {
                this.Config.ImagePixelWidth = value;
            }
        }
        /// <summary>
        /// 图标像素高度
        /// </summary>
        [DefaultValue(16)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ImagePixelHeight
        {
            get
            {
                return this.Config.ImagePixelHeight;
            }
            set
            {
                this.Config.ImagePixelHeight = value;
            }
        }

        private float _Left = 0f;
        /// <summary>
        /// 左端位置
        /// </summary>
        [DefaultValue(0f)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Left
        {
            get
            {
                return _Left;
            }
            set
            {
                if (_Left != value)
                {
                    this.LayoutInvalidate = true;
                    _Left = value;
                }
            }
        }
        /// <summary>
        /// 右端坐标
        /// </summary>
        [Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Right
        {
            get
            {
                return this._Left + this._Width;
            }
        }

        private float _Top = 0f;
        /// <summary>
        /// 顶端位置
        /// </summary>
        [DefaultValue(0)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Top
        {
            get
            {
                return _Top;
            }
            set
            {
                if (_Top != value)
                {
                    this.LayoutInvalidate = true;
                    _Top = value;
                }
            }
        }
        /// <summary>
        /// 下端坐标
        /// </summary>
        [Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Bottom
        {
            get
            {
                return _Top + this._Height;
            }
        }
        private float _Width = 750;
        /// <summary>
        /// 宽度
        /// </summary>
        [DefaultValue(750)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Width
        {
            get
            {
                return _Width;
            }
            set
            {
                if (_Width != value)
                {
                    this.LayoutInvalidate = true;
                    _Width = value;
                }
            }
        }

        private float _Height = 1020;
        /// <summary>
        /// 高度
        /// </summary>
        [DefaultValue(1020)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Height
        {
            get
            {
                return _Height;
            }
            set
            {
                if (_Height != value)
                {
                    this.LayoutInvalidate = true;
                    _Height = value;
                }
            }
        }

        /// <summary>
        /// 边界
        /// </summary>
        [Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public RectangleF Bounds
        {
            get
            {
                return new RectangleF(
                    this._Left,
                    this._Top,
                    this._Width,
                    this._Height);
            }
            set
            {
                this.Left = value.Left;
                this.Top = value.Top;
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        /// <summary>
        /// Y轴分割份数
        /// </summary>
        [DefaultValue(8)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int GridYSplitNum
        {
            get
            {
                return this.Config.GridYSplitNum;
            }
            set
            {
                this.Config.GridYSplitNum = value;
            }
        }

        /// <summary>
        /// 网格线颜色
        /// </summary>
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color GridLineColor
        {
            get
            {
                return this.Config.GridLineColor;
            }
            set
            {
                this.Config.GridLineColor = value;
            }
        }
       

        /// <summary>
        /// 数据点符号大小
        /// </summary>
        [DefaultValue(20f)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SymbolSize
        {
            get
            {
                return this.Config.SymbolSize;
            }
            set
            {
                this.Config.SymbolSize = value;
            }
        }


       

        /// <summary>
        /// 字体名称
        /// </summary>
        //[DefaultValue("宋体")]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FontName
        {
            get
            {
                return this.Config.FontName;
            }
            set
            {
                this.Config.FontName = value;
            }
        }

        /// <summary>
        /// 字体的大小
        /// </summary>
        [DefaultValue(9f)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float FontSize
        {
            get
            {
                return this.Config.FontSize;
            }
            set
            {
                this.Config.FontSize = value;
            }
        }
        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="info">数据行对象</param>
        /// <returns>创建的字体</returns>
        /// <param name="forValue">布尔对象</param>
        private XFontValue CreateFontValue(TitleLineInfo info, bool forValue, ValuePoint vp = null)
        {
            XFontValue f = null;
            if (vp != null && vp.Font != null && forValue == true)
            {
                f = vp.Font;
            }
            else if (info != null)
            {
                if (forValue)
                {
                    f = info.ValueFont;
                    if (f == null)
                    {
                        f = info.Font;
                    }
                }
                else
                {
                    f = info.Font;
                }
            }
            if (f == null)
            {
                f = new XFontValue();
                f.Name = this.FontName;
                f.Size = this.FontSize;
            }
            return f;
        }




        /// <summary>
        /// 创建字体
        /// </summary>
        /// <returns>创建的字体对象</returns>
        private XFontValue CreateFont()
        {
            return this.Config.RuntimeFont;
            //string fn = this.FontName;
            //if (string.IsNullOrEmpty(fn))
            //{
            //    fn = System.Windows.Forms.Control.DefaultFont.Name;
            //}
            //return new XFontValue(fn, this.FontSize);
        }
        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="info">数据序列对象</param>
        /// <returns>创建的字体</returns>
        /// <param name="forValue">布尔对象</param>
        private XFontValue CreateFontValue(YAxisInfo info, bool forValue)
        {
            XFontValue f = null;
            if (info != null)
            {
                if (forValue)
                {
                    f = info.ValueFont;
                    if (f == null)
                    {
                        f = info.Font;
                    }
                }
                else
                {
                    f = info.Font;
                }
            }
            if (f == null)
            {
                f = new XFontValue();
                f.Name = this.FontName;
                f.Size = this.FontSize;
            }
            return f;
        }
        /// <summary>
        /// 创建字体
        /// </summary>
        /// <returns>创建的字体对象</returns>
        private XFontValue CreateBigTitleFont()
        {
            string fn = this.FontName;
            if (string.IsNullOrEmpty(fn))
            {
                fn = XFontValue.DefaultFontName;
            }
            if (string.IsNullOrEmpty(this.Config.BigTitleFontName) == false)
            {
                fn = this.Config.BigTitleFontName;
            }
            FontStyle fontStyle = this.Config.BigTitleFontBold == true ? FontStyle.Bold : FontStyle.Regular;
            return new XFontValue(fn, this.Config.BigTitleFontSize, fontStyle);
        }
        private Color _BackColor = Color.Transparent;
        /// <summary>
        /// 背景色
        /// </summary>
        [DefaultColorValue("Transparent")]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color BackColor
        {
            get
            {
                return _BackColor;
            }
            set
            {
                _BackColor = value;
            }
        }




        /// <summary>
        /// 输出日期数据使用的格式化字符串
        /// </summary>
        [DefaultValue("yyyy-MM-dd")]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DateFormatString
        {
            get
            {
                return this.Config.DateFormatString;
            }
            set
            {
                this.Config.DateFormatString = value;
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        [DefaultValue(null)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Title
        {
            get
            {
                return this.Config.Title;
            }
            set
            {
                this.Config.Title = value;
            }
        }

#endif
        /// <summary>
        /// 页面标题
        /// </summary>
        [XmlArrayItem("Label", typeof(HeaderLabelInfo))]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public HeaderLabelInfoList HeaderLabels
        {
            get
            {
                return this.Config.HeaderLabels;
            }
            set
            {
                this.Config.HeaderLabels = value;
            }
        }
#if !DCWriterForWASM

        private DocumentViewMode _ViewMode = DocumentViewMode.Page;
        /// <summary>
        /// 文档视图模式
        /// </summary>
        [DefaultValue(DocumentViewMode.Page)]
        [Category("Layout")]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DocumentViewMode ViewMode
        {
            get
            {
                return _ViewMode;
            }
            set
            {
                if (_ViewMode != value)
                {
                    _ViewMode = value;
                    this.LayoutInvalidate = true;
                }
            }
        }
        /// <summary>
        /// 运行时使用的视图模式
        /// </summary>
        internal DocumentViewMode RuntimeViewMode
        {
            get
            {
                //if (this._ViewMode == DocumentViewMode.Timeline 
                //    && _SupportTimeLineViewMode == false )
                //{
                //    return DocumentViewMode.Page;
                //}
                return this._ViewMode;
            }
        }

        [NonSerialized]
        private int _PageIndex = 0;
        /// <summary>
        /// 从0开始计算的当前页号
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                if (_PageIndex != value)
                {
                    _PageIndex = value;
                    if (this.RuntimeViewMode == DocumentViewMode.Normal || this.RuntimeViewMode == DocumentViewMode.Page)
                    {
                        this.LayoutInvalidate = true;
                        foreach (YAxisInfo info in this.YAxisInfos)
                        {
                            foreach (ValuePoint vp in this.GetValuePointsByName(info.Name))
                            {
                                vp.Left = float.NaN;
                                vp.Top = float.NaN;
                                vp.Width = 0;
                                vp.Height = 0;
                            }
                        }
                        foreach (TitleLineInfo info in this.FooterLines)
                        {
                            foreach (ValuePoint vp in GetValuePointsByName(info.Name))
                            {
                                vp.Left = float.NaN;
                                vp.Top = float.NaN;
                                vp.Width = 0;
                                vp.Height = 0;
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 运行时的当前页码
        /// </summary>
        private int RuntimePageIndex
        {
            get
            {
                if (this.RuntimeViewMode == DocumentViewMode.Timeline)
                {
                    return 0;
                }
                if (_PageIndex < 0)
                {
                    return 0;
                }
                if (_PageIndex >= this.NumOfPages)
                {
                    return this.NumOfPages - 1;
                }
                return _PageIndex;
            }
        }
        [NonSerialized]
        internal int _NumOfPages = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        [Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int NumOfPages
        {
            get
            {
                return _NumOfPages;
            }
        }

        /// <summary>
        /// 文档配置字符串
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ConfigXml
        {
            get
            {
                System.IO.StringWriter str = new System.IO.StringWriter();
                TemperatureDocumentWriter writer = new TemperatureDocumentWriter(str);
                writer.Write237_TemperatureDocumentConfig("TemperatureDocumentConfig", null, this.Config, false, false);
                writer.Flush();
                string xml = str.ToString();
                writer.Close();
                return xml;
                //return DCSoft.Common.XMLHelper.SaveObjectToIndentXMLString(this.Config);
            }
            set
            {
                TemperatureDocumentReader reader = new TemperatureDocumentReader(new System.IO.StringReader(value));
                TemperatureDocumentConfig cfg = reader.Read237_TemperatureDocumentConfig(false, false);
                //TemperatureDocumentConfig cfg = (TemperatureDocumentConfig)
                //    DCSoft.Common.XMLHelper.LoadObjectFromXMLString(
                //    typeof(TemperatureDocumentConfig), value);
                if (cfg != null)
                {
                    this.Config = cfg;
                    this._PageIndex = 0;
                }
            }
        }


        /// <summary>
        /// 获得数据表格的HTML字符串
        /// </summary>
        /// <returns></returns>
        public string GetDataTableHtml()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("<table border='1'>");
            // 输出页眉
            str.AppendLine("\t<tr bgcolor='#dddddd'><td>时间</td>");
            List<string> yaValues = new List<string>();
            foreach (DocumentData data in this.Datas)
            {
                string name = data.Name;
                TitleLineInfo line = GetTitleLineInfoByName(data.Name);
                if (line != null)
                {
                    string runtimetitle = this.ViewMode == DocumentViewMode.Timeline ? line.Title : line.GetRuntimeTitleByPageIndex(this.PageIndex);
                    if (string.IsNullOrEmpty(runtimetitle) == false)
                    {
                        name = runtimetitle;// line.Title;
                    }
                }
                else
                {
                    YAxisInfo ya = this.YAxisInfos.GetItemByName(data.Name);
                    if (ya != null && string.IsNullOrEmpty(ya.Title) == false)
                    {
                        name = ya.Title;
                        if (ya.Style == YAxisInfoStyle.Value)
                        {
                            yaValues.Add(data.Name);
                        }
                    }
                }
                str.AppendLine("\t<td>" + name + "</td>");
            }
            str.AppendLine("\t</tr>");
            // 获得所有时间序列
            List<DateTime> times = new List<DateTime>();
            foreach (DocumentData data in this.Datas)
            {
                foreach (ValuePoint vp in data.Values)
                {
                    if (times.Contains(vp.Time) == false)
                    {
                        times.Add(vp.Time);
                    }
                }
            }
            // 时间序列排序
            times.Sort();
            foreach (DateTime dtm in times)
            {
                str.AppendLine("\t<tr>");
                str.AppendLine("\t<td>" + dtm.ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
                foreach (DocumentData data in this.Datas)
                {
                    bool match = false;
                    foreach (ValuePoint vp in data.Values)
                    {
                        if (vp.Time == dtm)
                        {
                            if (yaValues.Contains(data.Name))
                            {
                                if (IsNullValue(vp.Value))
                                {
                                    str.AppendLine("\t<td>&nbsp;</td>");
                                }
                                else
                                {
                                    str.AppendLine("\t<td>" + vp.Value + "</td>");
                                }
                            }
                            else
                            {
                                str.AppendLine("\t<td>" + vp.RuntimeText + "</td>");
                            }
                            match = true;
                            break;
                        }
                    }
                    if (match == false)
                    {
                        str.AppendLine("\t<td>&nbsp;</td>");
                    }
                }//foreach
                str.AppendLine("\t</tr>");
            }//foreach
            str.AppendLine("</table>");
            return str.ToString();
        }
        /// <summary>
        /// 设置、获得包含文档数据的XML字符串
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string XMLText
        {
            get
            {
                return this.SaveToString();// XMLHelper.SaveObjectToXMLString(this);
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                {
                    TemperatureDocument doc = new TemperatureDocument();
                    doc.LoadFromString(value);
                    if (doc != null)
                    {
                        doc.InnerCopyTo(this);
                    }
                }
            }
        }

        ///// <summary>
        ///// 设置、获得包含文档数据的带缩进的XML字符串
        ///// </summary>
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //[XmlIgnore]
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public string XMLTextIndented
        //{
        //    get
        //    {
        //        return XMLHelper.SaveObjectToIndentXMLString(this);
        //    }
        //    set
        //    {
        //        if (string.IsNullOrEmpty(value) == false)
        //        {
        //            TemperatureDocument doc = new TemperatureDocument();
        //            doc.LoadFromString(value);
        //            if (doc != null)
        //            {
        //                doc.InnerCopyTo(this);
        //            }
        //        }
        //    }
        //}



        /// <summary>
        /// 运行时使用的标准刻度宽度
        /// </summary>
        [NonSerialized]
        private float _RuntimeStdTickWidth = 0;
        private void SetRuntimeTicksWidth(bool setDefaultRuntimeTicks = false)
        {
            RuntimeTickInfoList UsingRuntimeTicks = setDefaultRuntimeTicks == false ? this._RuntimeTicks : this._DefaultRuntimeTicks;
            // 如果为时间轴模式则设置原始宽度
            float rw = this._RuntimeStdTickWidth;
            if (this.RuntimeViewMode == DocumentViewMode.Page || this.RuntimeViewMode == DocumentViewMode.Normal)
            {
                if (this.LeftHeaderWidth > 0)
                {
                    // 如果为分页模式或者单页模式，则重新计算时刻宽度
                    float tw = 0;
                    int freeCount = 0;
                    foreach (RuntimeTickInfo item in UsingRuntimeTicks)
                    {
                        if (item.Zone != null && item.Zone.SpecifyTickWidth > 0)
                        {
                            tw += item.Zone.SpecifyTickWidth;
                        }
                        else
                        {
                            tw += this._RuntimeStdTickWidth;
                            freeCount++;
                        }
                    }
                    if (tw != this.Width - this.LeftHeaderWidth)
                    {
                        if (freeCount > 0)
                        {
                            rw = (this.Width - this.LeftHeaderWidth) / freeCount;
                            if (rw <= 0)
                            {
                                // 计算的值小于等于0，则采用标准值
                                rw = this._RuntimeStdTickWidth;
                            }
                        }
                    }
                }
            }
            float leftCount = 0;
            foreach (RuntimeTickInfo item in UsingRuntimeTicks)
            {
                item.Left = leftCount;
                if (item.Zone != null && item.Zone.SpecifyTickWidth > 0)
                {
                    item.Width = item.Zone.SpecifyTickWidth;
                }
                else
                {
                    item.Width = rw;
                }
                leftCount = leftCount + item.Width;
            }
            UsingRuntimeTicks.TotalWidth = leftCount;
        }


        internal SizeF GetPreferedSizeForTimeLineViewMode(Graphics g)
        {
            g.PageUnit = this.GraphicsUnit;
            this.Config.CheckDefaultTicks();
            DateTime max = TemperatureDocument.NullDate;
            DateTime min = TemperatureDocument.NullDate;
            this.UpdateNumOfPage(out max, out min);
            //int days = (int)max.Subtract(min).TotalDays;
            this.RefreshRuntimeTicks(min, max, 0);

            //除了初始化用户自定义的时刻以外还要初始化一个内置的默认属性的时刻用于页脚数据行的绘制
            this.RefreshRuntimeTicks(min, max, 0, true);
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            //int ticks = this._RuntimeTicks.Count;// this.Config.Ticks.Count * days;
            this.ExecuteLayout(new DCGraphicsForTimeLine(g));
            float viewWidth = this._LeftHeaderWidth;

            //float viewWidth = Draw(new DCGraphics(g), RectangleF.Empty, DocumentViewParty.GetLeftHeaderWidthOnly); ;
            float viewHeight = 0;
            XFontValue myFont = CreateFont();
            //float minWidth = g.MeasureString("HHHH", myFont.Value).Width;

            float tickWidth = this.Config.SpecifyTickWidth;
            if (tickWidth <= 0)
            {
                tickWidth = g.MeasureString(
                    "HH",
                    myFont.Value,
                    10000,
                    DCStringFormat.NativeGenericTypographic).Width * 1.1f;
            }
            this._RuntimeStdTickWidth = tickWidth;
            SetRuntimeTicksWidth();
            SetRuntimeTicksWidth(true); 
            viewWidth += this._RuntimeTicks.TotalWidth;
            //viewWidth = viewWidth * 1.1f;

            float lineHeight = myFont.GetHeight(g) * 1.1f;
            viewHeight = lineHeight * (
                this.RuntimeHeaderLines.Count
                + this.RuntimeFooterLines.Count);
            viewHeight += 1000;// 数值折线区域标准高度1000单位
            return new SizeF(viewWidth, viewHeight);
        }

        /// <summary>
        /// 重新计算LeftHeaderWidth属性值
        /// </summary>
        internal void UpdateLeftHeaderWidth()
        {
            return;
           
        }

        /// <summary>
        /// 创建一个完整的图片，包含所有的内容
        /// </summary>
        /// <returns></returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Bitmap CreateFullContentBmp()
        {
            Bitmap bmp = new Bitmap((int)this.Width, (int)this.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;

                g.Clear(Color.White);
                this.PageIndex = 0;
                Draw2(
                    new DCGraphicsForTimeLine(g),
                    new RectangleF(0, 0, this.Right, this.Bottom),
                    DocumentViewParty.Both);
            }
            return bmp;
        }
        /// <summary>
        /// 创建包含指定页码内容的图片对象
        /// </summary>
        /// <param name="pageIndex">从0开始计算的页码</param>
        /// <returns>创建的图片对象</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Bitmap CreatePageBmp(int pageIndex)
        {
            if (this.RuntimeViewMode == DocumentViewMode.Timeline
                || this.RuntimeViewMode == DocumentViewMode.Normal)
            {
                SizeF size = GraphicsUnitConvert.Convert(
                    new SizeF(this.Width, this.Height),
                    this.GraphicsUnit,
                    GraphicsUnit.Pixel);
                Bitmap bmp = new Bitmap((int)size.Width, (int)size.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (var g2 = new DCGraphicsForTimeLine(g))
                    {
                        g2.AutoSetInnerMatrix();
                        g2.NativeGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        g2.NativeGraphics.SmoothingMode = SmoothingMode.HighQuality;
                        g2.NativeGraphics.CompositingQuality = CompositingQuality.HighQuality;
                        g2.NativeGraphics.InterpolationMode = InterpolationMode.HighQualityBilinear;

                        g.PageUnit = this.GraphicsUnit;
                        if (this.Config.PageBackColor.A == 0)
                        {
                            g.Clear(Color.White);
                        }
                        else
                        {
                            g.Clear(this.Config.PageBackColor);
                        }
                        this.PageIndex = PageIndex;

                        Draw2(
                            g2,
                            new RectangleF(0, 0, this.Right, this.Bottom),
                            DocumentViewParty.Both);
                     
                        g2.CleanInnerMatrix();
                    }
                }
                return bmp;
            }
            else
            {
                //PageSettings settings = new PageSettings();
                //this.Config.PageSettings.WriteTo(settings);

                XPageSettings settings1 = new XPageSettings();
                settings1.PaperSize = new PaperSize(
                    this.Config.PageSettings.PaperSizeName,
                    this.Config.PageSettings.PaperWidth,
                    this.Config.PageSettings.PaperHeight);
                settings1.Landscape = this.Config.PageSettings.Landscape;
                settings1.Margins = new Margins(
                    this.Config.PageSettings.LeftMargin,
                    this.Config.PageSettings.RightMargin,
                    this.Config.PageSettings.TopMargin,
                    this.Config.PageSettings.BottomMargin);

                //PrintDocument pd = new PrintDocument();
                //settings.PrinterSettings.PrinterName = pd.PrinterSettings.PrinterName;//设置默认打印机

                int useWidth = this.Config.PageSettings.Landscape ? settings1.PaperSize.Height : settings1.PaperSize.Width;
                int useHeight = this.Config.PageSettings.Landscape ? settings1.PaperSize.Width : settings1.PaperSize.Height;
                Bitmap bmp = new Bitmap(
                    (int)(useWidth * 96.0 / 100.0),
                    (int)(useHeight * 96.0 / 100.0));
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (var g2 = new DCGraphicsForTimeLine(g))
                    {
                        g2.AutoSetInnerMatrix();

                        g2.NativeGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        g2.NativeGraphics.SmoothingMode = SmoothingMode.HighQuality;
                        g2.NativeGraphics.CompositingQuality = CompositingQuality.HighQuality;
                        g2.NativeGraphics.InterpolationMode = InterpolationMode.HighQualityBilinear;

                        if (this.Config.PageBackColor.A == 0)
                        {
                            g.Clear(Color.White);
                        }
                        else
                        {
                            g.Clear(this.Config.PageBackColor);
                        }
                        LayoutForPageView(settings1);
                        DocumentViewMode back = this.ViewMode;
                        this.ViewMode = DocumentViewMode.Page;
                        this.PageIndex = pageIndex;
                        RectangleF rectf = new RectangleF(
                            0,
                            0,
                            this.Config.PageSettings.Landscape ? this.Bottom : this.Right,
                            this.Config.PageSettings.Landscape ? this.Right : this.Bottom);
                        Draw2(
                            g2,
                            new RectangleF(0, 0, this.Right, this.Bottom),
                            DocumentViewParty.Both);
                        this.ViewMode = back;
                       
                        g2.AutoSetInnerMatrix();
                    }
                }
                return bmp;
            }
        }
        /// <summary>
        /// 为页面视图模式进行内容排版
        /// </summary>
        /// <param name="pageSettings">页面设置</param>
        /// <returns></returns>
        internal RectangleF LayoutForPageView(XPageSettings pageSettings)
        {
            // 计算页面框架视图边界
            System.Drawing.Size s = System.Drawing.Size.Empty;
            if (pageSettings.PaperSize.Kind == PaperKind.Custom)
            {
                s.Width = pageSettings.PaperSize.Width;
                s.Height = pageSettings.PaperSize.Height;
            }
            else
            {
                s = DCSoft.Drawing.StandardPaperSizeInfo.GetStandardPaperSize(pageSettings.PaperSize.Kind);
            }
            int w = s.Width;
            int h = s.Height;
            if (pageSettings.Landscape)
            {
                int t = w;
                w = h;
                h = t;
            }
            Rectangle rc = new Rectangle(0, 0, w, h);
            //Rectangle rc = StandardPaperSizeInfo.SafeGetBounds(pageSettings);

            RectangleF _PageBounds = new Rectangle(
                     0,
                     0,
                     (int)(rc.Width * 3),
                     (int)(rc.Height * 3));
            // 计算文档视图边界
            RectangleF bounds = RectangleF.Empty;
            bounds.X = _PageBounds.X + pageSettings.Margins.Left * 3;
            bounds.Y = _PageBounds.Y + pageSettings.Margins.Top * 3;
            bounds.Width = _PageBounds.Right - pageSettings.Margins.Right * 3 - bounds.Left;
            bounds.Height = _PageBounds.Bottom - pageSettings.Margins.Bottom * 3 - bounds.Top;
            //bounds = GraphicsUnitConvert.Convert(bounds, GraphicsUnit.Document, this.GraphicsUnit);
            this.Left = bounds.Left;
            this.Top = bounds.Top;
            this.Width = bounds.Width;
            this.Height = bounds.Height;
            return _PageBounds;
        }
        internal object GetObject(float x, float y)
        {
            if (this.RuntimeViewMode == DocumentViewMode.Page)
            {
                if (this.Config.Labels != null)
                {
                    foreach (DCTimeLineLabel lbl in this.Config.Labels)
                    {
                        RectangleF rect = GetObjectBounds(lbl);
                        if (rect.IsEmpty == false && rect.Contains(x, y))
                        {
                            return lbl;
                        }
                    }
                }
            }
            if (this.Config.Images != null)
            {
                foreach (DCTimeLineImage info in this.Config.Images)
                {
                    RectangleF rect = GetObjectBounds(info);
                    if (rect.IsEmpty == false && rect.Contains(x, y))
                    {
                        return info;
                    }
                }
            }

            if (this.Config.HeaderLabels != null)
            {
                foreach (HeaderLabelInfo info in this.Config.HeaderLabels)
                {
                    RectangleF rect = GetObjectBounds(info);
                    if (rect.IsEmpty == false && rect.Contains(x, y))
                    {
                        return info;
                    }
                }
            }
            if (this.Config.HeaderLines != null)
            {
                foreach (TitleLineInfo info in this.Config.HeaderLines)
                {
                    RectangleF rect = GetObjectBounds(info);
                    if (rect.IsEmpty == false && rect.Contains(x, y))
                    {
                        return info;
                    }
                }
            }
            if (this.Config.YAxisInfos != null)
            {
                foreach (YAxisInfo info in this.Config.YAxisInfos)
                {
                    RectangleF rect = GetObjectBounds(info);
                    if (rect.IsEmpty == false && rect.Contains(x, y))
                    {
                        return info;
                    }
                }//foreach
            }
            if (this.Config.FooterLines != null)
            {
                foreach (TitleLineInfo info in this.Config.FooterLines)
                {
                    RectangleF rect = GetObjectBounds(info);
                    if (rect.IsEmpty == false && rect.Contains(x, y))
                    {
                        return info;
                    }
                }
            }
            if (this.Config.Zones != null)
            {
                // 反向遍历
                TimeLineZoneInfoList zones = this.Config.Zones;
                for (int iCount = zones.Count - 1; iCount >= 0; iCount--)
                {
                    TimeLineZoneInfo zone = zones[iCount];
                    RectangleF rect = new RectangleF(zone.Left, zone.Top, zone.Width, zone.Height);
                    if (rect.IsEmpty == false && rect.Contains(x, y))
                    {
                        return zone;
                    }
                }
            }
            return null;
        }



        [NonSerialized]
        private object _SelectedObject = null;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        internal object SelectedObject
        {
            get
            {
                return _SelectedObject;
            }
            set
            {
                _SelectedObject = value;
            }
        }

        private RectangleF GetObjectBounds(object instance)
        {
            if (instance is HeaderLabelInfo)
            {
                HeaderLabelInfo info = (HeaderLabelInfo)instance;
                return new RectangleF(info.Left, info.Top, info.Width, info.Height);
            }
            if (instance is TitleLineInfo)
            {
                TitleLineInfo info = (TitleLineInfo)instance;
                return info.TitleBounds;
            }
            if (instance is YAxisInfo)
            {
                YAxisInfo ya = (YAxisInfo)instance;
                return ya.TitleBounds;
            }
            if (instance is TemperatureDocument || instance is TemperatureDocumentConfig)
            {
                if (this.RuntimeViewMode == DocumentViewMode.Page
                    || this.RuntimeViewMode == DocumentViewMode.Normal)
                {
                    return new RectangleF(this.Left, this.Top, this.Width, this.Height);
                }
                else
                {
                    return new RectangleF(this.Left, this.Top, this.LeftHeaderWidth, this.Height);
                }
            }
            if (instance is DCTimeLineImage)
            {
                DCTimeLineImage img = (DCTimeLineImage)instance;
                return new RectangleF(
                    this.Left + img.Left,
                    this.Top + img.Top,
                    img.ImagePixelWidth,
                    img.ImagePixelHeight);
            }
            if (instance is DCTimeLineLabel)
            {
                DCTimeLineLabel lbl = (DCTimeLineLabel)instance;
                return new RectangleF(
                    this.Left + lbl.Left,
                    this.Top + lbl.Top,
                    lbl.Width,
                    lbl.Height);
            }
            if (instance is TimeLineZoneInfo)
            {
                TimeLineZoneInfo zone = (TimeLineZoneInfo)instance;
                return new RectangleF(zone.Left, zone.Top, zone.Width, zone.Height);
            }
            return RectangleF.Empty;
        }
#endif


        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocument Clone()
        {
            TemperatureDocument doc = (TemperatureDocument)this.MemberwiseClone();
            if (this._Config != null)
            {
                doc._Config = this._Config.Clone();
            }
            if (this._Datas != null)
            {
                doc._Datas = this._Datas.Clone();
            }
            if (this._Parameters != null)
            {
                doc._Parameters = this._Parameters.Clone();
            }
            return doc;
        }

        /// <summary>
        /// 表示空日期的字符串
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly string NullDateString = InnerNullDateString;

        public const string InnerNullDateString = "1900-1-1";

        internal bool _NoDataInDocument = true;

        /// <summary>
        /// 表示空的日期
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DateTime NullDate = new DateTime(1900, 1, 1);
        /// <summary>
        /// 表示空的数据
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly float NullValue = InnerNullValue;

        public const float InnerNullValue = -10000f;
#if !DCWriterForWASM

        /// <summary>
        /// 判断是否为空数值
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal static bool IsNullValue(float v)
        {
            return float.IsNaN(v) || v == InnerNullValue;
        }

        /// <summary>
        /// 判断是否为空日期数值
        /// </summary>
        /// <param name="dtm"></param>
        /// <returns></returns>
        internal static bool IsNullDate(DateTime dtm)
        {
            return dtm == DateTime.MinValue || dtm == NullDate;
        }
        
        /// <summary>
        /// 是否支持时间轴视图模式
        /// </summary>
        [NonSerialized]
        private static bool _SupportTimeLineViewMode = true;
        /// <summary>
        /// 更新是否支持时间轴视图模式的标志
        /// </summary>
        internal static void UpdateSupportTimeLineViewMode()
        {
           // _SupportTimeLineViewMode = GetSupportFunction( MyDCFunctionIDConsts.TimeLineViewMode );//MyDCFunctionIDConsts.TimeLineViewMode
        }

        /// <summary>
        /// 将对象转换为一个JSON字符串
        /// </summary>
        /// <returns></returns>
        internal string ToJsonString()
        {
            var writer = new DCFastJsonTextWriter();
            writer.IndentFormat = true;
            this.WriteJson(writer);
            var result = writer.ToString();
            return result;
        }

        internal void WriteJson(DCFastJsonTextWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyNoFixName("ViewMode", this.ViewMode.ToString());
            writer.WritePropertyNoFixName("PageIndex", this.PageIndex.ToString());
            writer.WritePropertyNoFixName("NumOfPages", this.NumOfPages.ToString());
            if (this.Config != null)
            {
                writer.WriteStartProperty("Config");
                writer.WriteStartObject();
                this.Config.WriteJson(writer);
                writer.WriteEndObject();
                writer.WriteEndProperty();
            }
            if (this.Datas != null && this.Datas.Count > 0)
            {
                writer.WriteStartProperty("Values");
                writer.WriteStartArray();
                foreach (DocumentData item in this.Datas)
                {
                    writer.WriteStartObject();
                    item.WriteJson(writer);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndProperty();
            }
            writer.WriteEndObject();
        }
#endif
    }
}