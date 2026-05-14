 
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Xml.Serialization;
using System.Drawing ;
using DCSoft.Common;
using DCSoft.Drawing;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 网格线标题行信息
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
#if !DCWriterForWASM
    [Serializable]
    [System.Xml.Serialization.XmlType]
    [DCSoft.Common.DCPublishAPI]
    //[DCSoft.Common.DCDescriptionResourceSource(typeof(DCSoft.TemperatureChart.DCTimeLineDescriptionResource))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
#endif
    public partial class TitleLineInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TitleLineInfo()
        {
        }

        private DateTimePrecisionMode _InputTimePrecision = DateTimePrecisionMode.Minute;
        /// <summary>
        /// 输入时间的精度
        /// </summary>
        [DefaultValue(DateTimePrecisionMode.Minute)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "InputTimePrecision")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DateTimePrecisionMode InputTimePrecision
        {
            get
            {
                return _InputTimePrecision;
            }
            set
            {
                _InputTimePrecision = value;
            }
        }

        private string _Name = null;
        /// <summary>
        /// 名称
        /// </summary>
        [DefaultValue( null )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "Name")]
#if WINFORM || DCWriterForWinFormNET6
        [Editor(typeof(EditNameUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
#endif
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Name
        {
            get
            {
                return _Name; 
            }
            set
            {
                _Name = value; 
            }
        }

        private bool _AutoHeight = false;
        /// <summary>
        /// 获取或设置页眉/脚标题行是否根据文本内容来自动调整高度
        /// </summary>
        [DefaultValue(false)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "AutoHeight")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool AutoHeight
        {
            get
            {
                return _AutoHeight;
            }
            set
            {
                _AutoHeight = value;
            }
        }

        private bool _VisibleWhenNoValuePoint = true;
        /// <summary>
        /// 获取或设置页眉/脚标题行在分页模式下当前页无数据点时是否可见
        /// </summary>
        [DefaultValue(true)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "VisibleWhenNoValuePoint")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool VisibleWhenNoValuePoint
        {
            get
            {
                return _VisibleWhenNoValuePoint;
            }
            set
            {
                _VisibleWhenNoValuePoint = value;
            }
        }

        private bool _Visible = true;
        /// <summary>
        /// 获取或设置页眉/脚标题行是否可见
        /// </summary>
        [DefaultValue(true)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "Visible")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                _Visible = value;
            }
        }


        private bool _BlankDateWhenNoData = false;
        /// <summary>
        /// 获取或设置当文档内没有数据时是否显示日期数据行内容
        /// </summary>
        [DefaultValue(false)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "BlankDateWhenNoData")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool BlankDateWhenNoData
        {
            get
            {
                return _BlankDateWhenNoData;
            }
            set
            {
                _BlankDateWhenNoData = value;
            }
        }

        private bool _RuntimeVisible = true;
        /// <summary>
        /// 获取或设置页眉/脚标题行实际在运行时是否可见（内部使用）
        /// </summary>
        [DefaultValue(true)]
        internal bool RuntimeVisible
        {
            get
            {
                return _RuntimeVisible;
            }
            set
            {
                _RuntimeVisible = value;
            }
        }

        private bool _HiddenOnPageViewWhenNoValuePoints = false;
        /// <summary>
        /// 获取或设置在分页视图下当页内的页眉/脚标题行在没有数据点的时候是否隐藏
        /// </summary>
        [DefaultValue(false)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "HiddenOnPageViewWhenNoValuePoints")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool HiddenOnPageViewWhenNoValuePoints
        {
            get
            {
                return _HiddenOnPageViewWhenNoValuePoints;
            }
            set
            {
                _HiddenOnPageViewWhenNoValuePoints = value;
            }
        }

        private string _GroupName = null;
        /// <summary>
        /// 分组名称
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "GroupName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string GroupName
        {
            get
            {
                return _GroupName; 
            }
            set
            {
                _GroupName = value; 
            }
        }

        private bool _AfterOperaDaysFromZero = true;
        /// <summary>
        /// 术后天数是否从0显示
        /// add by ld
        /// </summary>
        [DefaultValue(true)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "AfterOperaDaysFromZero")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool AfterOperaDaysFromZero
        {
            get
            {
                return _AfterOperaDaysFromZero;
            }
            set
            {
                _AfterOperaDaysFromZero = value;
            }
        }

        private bool _AfterOperaDaysBeginOne = false;
        /// <summary>
        /// 术后天数当天起是否从1开始显示而不是从0显示
        /// </summary>
        [DefaultValue(false)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "AfterOperaDaysBeginOne")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool AfterOperaDaysBeginOne
        {
            get
            {
                return _AfterOperaDaysBeginOne;
            }
            set
            {
                _AfterOperaDaysBeginOne = value;
            }
        }

        private RectangleF _ExpandedHandleBounds = RectangleF.Empty;
        /// <summary>
        /// 展开收缩句柄显示区域
        /// </summary>
        internal RectangleF ExpandedHandleBounds
        {
            get
            {
                return _ExpandedHandleBounds;
            }
            set
            {
                _ExpandedHandleBounds = value;
            }
        }

        private bool _IsExpanded = true;
        /// <summary>
        /// 对象是否是展开的
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool IsExpanded
        {
            get
            {
                return _IsExpanded;
            }
            set
            {
                _IsExpanded = value;
            }
        }

      
        private bool _ShowExpandedHandle = false;
        /// <summary>
        /// 是否显示展开收缩句柄
        /// </summary>
        internal bool ShowExpandedHandle
        {
            get
            {
                return _ShowExpandedHandle; 
            }
            set
            {
                _ShowExpandedHandle = value; 
            }
        }

        private bool _ValueTextMultiLine = false;
        /// <summary>
        /// 数值文本多行显示
        /// </summary>
        [DefaultValue( false )]
        [DCDisplayName(typeof(TitleLineInfo), "ValueTextMultiLine")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ValueTextMultiLine
        {
            get
            {
                return _ValueTextMultiLine; 
            }
            set
            {
                _ValueTextMultiLine = value; 
            }
        }

      
        private Color _OutofNormalRangeTextColor = Color.Red;
        /// <summary>
        /// 超出正常区域背景色
        /// </summary>
        [DefaultColorValue("Red")]
        [XmlIgnore]
        [DCDisplayName(typeof(TitleLineInfo), "OutofNormalRangeTextColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color OutofNormalRangeTextColor
        {
            get
            {
                return _OutofNormalRangeTextColor;
            }
            set
            {
                _OutofNormalRangeTextColor = value;
            }
        }
        /// <summary>
        /// 文本形式的OutofNormalRangeTextColor属性值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string OutofNormalRangeTextColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.OutofNormalRangeTextColor, Color.Red);
            }
            set
            {
                this.OutofNormalRangeTextColor = XMLSerializeHelper.StringToColor(value, Color.Red);
            }
        }

        private float _NormalMaxValue = TemperatureDocument.InnerNullValue;
        /// <summary>
        /// 数值正常范围的最大值
        /// </summary>
        [DefaultValue(TemperatureDocument.InnerNullValue)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "NormalMaxValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float NormalMaxValue
        {
            get
            {
                return _NormalMaxValue;
            }
            set
            {
                _NormalMaxValue = value;
            }
        }

        private float _NormalMinValue = TemperatureDocument.InnerNullValue;
        /// <summary>
        /// 数值正常范围的最小值
        /// </summary>
        [DefaultValue(TemperatureDocument.InnerNullValue)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "NormalMinValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float NormalMinValue
        {
            get
            {
                return _NormalMinValue;
            }
            set
            {
                _NormalMinValue = value;
            }
        }

         

        private DCExtendGridLineType _ExtendGridLineType = DCExtendGridLineType.Below;
        /// <summary>
        /// 延伸的网格线样式
        /// </summary>
        [DefaultValue(DCExtendGridLineType.Below )]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "ExtendGridLineType")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCExtendGridLineType ExtendGridLineType
        {
            get
            {
                return _ExtendGridLineType; 
            }
            set
            {
                _ExtendGridLineType = value; 
            }
        }

        

        private bool _EnableEndTime = true ;
        /// <summary>
        /// 启动数据块结束时间
        /// </summary>
        [DefaultValue(true)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "EnableEndTime")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool EnableEndTime
        {
            get
            {
                return _EnableEndTime; 
            }
            set
            {
                _EnableEndTime = value; 
            }
        }

        private float _BlockWidth = 15f;
        /// <summary>
        /// 色块宽度
        /// </summary>
        [DefaultValue(15f)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "BlockWidth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float BlockWidth
        {
            get 
            {
                return _BlockWidth; 
            }
            set
            {
                _BlockWidth = value; 
            }
        }


        private string _ValueDisplayFormat = null;
        /// <summary>
        /// 显示数值使用的格式化字符串
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "ValueDisplayFormat")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ValueDisplayFormat
        {
            get
            {
                return _ValueDisplayFormat; 
            }
            set
            {
                _ValueDisplayFormat = value; 
            }
        }

        private string _LoopTextList = null;
        /// <summary>
        /// 循环显示的文本，各个项目之间用半角逗号分开
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "LoopTextList")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string LoopTextList
        {
            get
            {
                return _LoopTextList; 
            }
            set
            {
                _LoopTextList = value; 
            }
        }

        private float _SpecifyTitleWidth = 0f;
        /// <summary>
        /// 指定的标题宽度,小于等于0为无效设置，将自动计算标题宽度。
        /// </summary>
        [DefaultValue(0f)]
        [DCDisplayName(typeof(TitleLineInfo), "SpecifyTitleWidth")]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SpecifyTitleWidth
        {
            get
            {
                return _SpecifyTitleWidth;
            }
            set
            {
                _SpecifyTitleWidth = value;
            }
        }

        private string _Title = null;
        /// <summary>
        /// 标题文本
        /// </summary>
        [DefaultValue( null )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "Title")]
#if WINFORM || DCWriterForWinFormNET6
        [Editor(typeof(EditTitleUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
#endif
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Title
        {
            get
            {
                return _Title; 
            }
            set
            {
                _Title = value; 
            }
        }

        private string _PageTitleTexts = null;
        /// <summary>
        /// 获取或设置数据行每页标题文本列表
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "PageTitleTexts")]
#if WINFORM || DCWriterForWinFormNET6
        [Editor(typeof(EditTitleUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
#endif
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string PageTitleTexts
        {
            get
            {
                return _PageTitleTexts;
            }
            set
            {
                _PageTitleTexts = value;
            }
        }
#if !DCWriterForWASM

        internal string GetRuntimeTitleByPageIndex(int pageindex)
        {
            if (this.PageTitleTexts == null || this.PageTitleTexts.Length == 0)
            {
                return this.Title;
            }
            else
            {
                string[] titles = this.PageTitleTexts.Split(',');
                if (pageindex >= 0 && pageindex <= titles.Length - 1)
                {
                    return titles[pageindex];
                }
                else
                {
                    return this.Title;
                }
            }
        }
#endif
        private XFontValue _Font = null;
        /// <summary>
        /// 字体
        /// </summary>
        [DefaultValue(null)]
        [XmlElement]
        [DCDisplayName(typeof(TitleLineInfo), "Font")]
        [Browsable( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XFontValue Font
        {
            get
            {
                return _Font;
            }
            set
            {
                _Font = value;
            }
        }


        private XFontValue _ValueFont = null;
        /// <summary>
        /// 字体
        /// </summary>
        [DefaultValue(null)]
        [XmlElement]
        [DCDisplayName(typeof(TitleLineInfo), "ValueFont")]
        [Browsable(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XFontValue ValueFont
        {
            get
            {
                return _ValueFont;
            }
            set
            {
                _ValueFont = value;
            }
        }

        private Color _TitleColor = Color.Black;
        /// <summary>
        /// 标题文本颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [DCDisplayName(typeof(TitleLineInfo), "TitleColor")]
        [DefaultColorValue("Black")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color TitleColor
        {
            get
            {
                return _TitleColor;
            }
            set
            {
                _TitleColor = value;
            }
        }

        /// <summary>
        /// 文本形式的标题文本颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TitleColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.TitleColor, Color.Black);
            }
            set
            {
                this.TitleColor = XMLSerializeHelper.StringToColor(value, Color.Black);
            }
        }

        private Color _TextColor = Color.Black;
        /// <summary>
        /// 文本颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [DCDisplayName(typeof(TitleLineInfo), "TextColor")]
        [DefaultColorValue("Black")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color TextColor
        {
            get
            {
                return _TextColor;
            }
            set
            {
                _TextColor = value;
            }
        }

        /// <summary>
        /// 文本颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TextColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.TextColor, Color.Black);
            }
            set
            {
                this.TextColor = XMLSerializeHelper.StringToColor(value, Color.Black);
            }
        }

        private StringAlignment _TitleAlign = StringAlignment.Center;
        /// <summary>
        /// 标题对齐方式
        /// </summary>
        [DefaultValue( StringAlignment.Center )]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "TitleAlign")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment TitleAlign
        {
            get
            {
                return _TitleAlign; 
            }
            set
            {
                _TitleAlign = value; 
            }
        }

        private StringAlignment _ValueAlign = StringAlignment.Center;
        /// <summary>
        /// 数值对齐方式
        /// </summary>
        [DefaultValue(StringAlignment.Center)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "ValueAlign")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment ValueAlign
        {
            get
            {
                return _ValueAlign;
            }
            set
            {
                _ValueAlign = value;
            }
        }

        private int _MaxValueForDayIndex = 13;
        /// <summary>
        /// 显示的ForDayIndex的最大值，为0表示不限制。默认为13.
        /// </summary>
        [DefaultValue(13)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "MaxValueForDayIndex")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int MaxValueForDayIndex
        {
            get
            {
                return _MaxValueForDayIndex; 
            }
            set
            {
                _MaxValueForDayIndex = value; 
            }
        }

        private ValuePointDataSourceInfo _DataSource = null;
        /// <summary>
        /// 数据源
        /// </summary>
        [DefaultValue( null )]
        [XmlElement]
        [DCDisplayName(typeof(TitleLineInfo), "DataSource")]
        [Browsable(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePointDataSourceInfo DataSource
        {
            get 
            {
                
                //if (_DataSource == null)
                //{
                //    _DataSource = new ValuePointDataSourceInfo();
                //}
                return _DataSource; 
            }
            set
            {
                _DataSource = value; 
            }
        }

        private string _CircleText = null;
        /// <summary>
        /// 画圈的文本值
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "CircleText")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string CircleText
        {
            get
            {
                return _CircleText; 
            }
            set
            {
                _CircleText = value; 
            }
        }

        private float _SpecifyHeight = 0;
        /// <summary>
        /// 指定的高度，以Document(1/300英寸)为单位
        /// </summary>
        [DefaultValue( 0f )]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "SpecifyHeight")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SpecifyHeight
        {
            get
            {
                return _SpecifyHeight; 
            }
            set
            {
                _SpecifyHeight = value; 
            }
        }
#if ! DCWriterForWASM
        //private float _Left = 0;
        ///// <summary>
        ///// 左端位置
        ///// </summary>
        //[Browsable(false)]
        //[XmlIgnore]
        //public float Left
        //{
        //    get { return _Left; }
        //    set { _Left = value; }
        //}

        private float _Top = 0;
        /// <summary>
        /// 顶端位置
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Top
        {
            get { return _Top; }
            set { _Top = value; }
        }

        //private float _Width = 0;
        ///// <summary>
        ///// 宽度
        ///// </summary>
        //[Browsable(false)]
        //[XmlIgnore]
        //public float Width
        //{
        //    get { return _Width; }
        //    set { _Width = value; }
        //}
         

        /// <summary>
        /// 对象高度
        /// </summary>
        private float _Height = 0;
        /// <summary>
        /// 对象高度
        /// </summary>
        [XmlIgnore]
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Height
        {
            get
            {
                return this._Height;
            }
            set
            {
                _Height = value;
                if (this.SpecifyHeight == 0)
                {
                    this.RuntimeHeight = _Height;
                }
            }
        }

        /// <summary>
        /// 运行时使用的高度
        /// </summary>
        internal float RuntimeHeight = 0;

        /// <summary>
        /// 计算运行时使用的高度
        /// </summary>
        /// <param name="unit">当前高度使用的度量单位</param>
        internal void RefreshRuntimeHeight(GraphicsUnit unit)
        {
            if (this.SpecifyHeight > 0)
            {
                float h = (float)DCSoft.Drawing.GraphicsUnitConvert.Convert(
                    this.SpecifyHeight,
                    GraphicsUnit.Document, unit);
                if (this.AutoHeight)
                {
                    this.RuntimeHeight = Math.Max(h, this.Height);
                }
                else
                {
                    this.RuntimeHeight = h;
                }
            }
            else
            {
                this.RuntimeHeight = Math.Max(this.RuntimeHeight, this.Height);
            }
        }
        /// <summary>
        /// 内容行数
        /// </summary>
        internal int ContentLineCount = 1;

        /// <summary>
        /// 标题边界
        /// </summary>
        internal RectangleF TitleBounds = RectangleF.Empty;

        /// <summary>
        /// 结束计算的日期
        /// </summary>
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        internal DateTime RuntimeEndDate = TemperatureDocument.NullDate;
#endif

        private string _EndDateKeyword = null;
        /// <summary>
        /// 判断结束计算日期的关键字
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "EndDateKeyword")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string EndDateKeyword
        {
            get
            {
                return _EndDateKeyword;
            }
            set
            {
                _EndDateKeyword = value;
            }
        }

        private DateTime _StartDate = TemperatureDocument.NullDate;
        /// <summary>
        /// 开始计算的日期
        /// </summary>
        [DefaultValue( typeof( DateTime ) , TemperatureDocument.InnerNullDateString)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "StartDate")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DateTime StartDate
        {
            get
            {
                return _StartDate; 
            }
            set
            {
                _StartDate = value; 
            }
        }

        private string _StartDateKeyword = null;
        /// <summary>
        /// 判断开始计算日期的关键字
        /// </summary>
        [DefaultValue( null )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "StartDateKeyword")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string StartDateKeyword
        {
            get
            {
                return _StartDateKeyword; 
            }
            set
            {
                _StartDateKeyword = value; 
            }
        }

        private bool _PreserveStartKeywordOrder = false;
        /// <summary>
        /// 获取或设置当有多个开始时间关键字时，天数日期排列顺序是否遵守文本数据点时间顺序
        /// </summary>
        [DefaultValue(false)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "PreserveStartKeywordOrder")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool PreserveStartKeywordOrder
        {
            get
            {
                return _PreserveStartKeywordOrder;
            }
            set
            {
                _PreserveStartKeywordOrder = value;
            }
        }

        private bool _ShowBackColor = true;
        /// <summary>
        /// 是否显示背景色
        /// </summary>
        [DefaultValue(true)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "ShowBackColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ShowBackColor
        {
            get
            {
                return _ShowBackColor; 
            }
            set
            {
                _ShowBackColor = value; 
            }
        }

        private TitleLineLayoutType _LayoutType = TitleLineLayoutType.Normal;
        /// <summary>
        /// 内容布局模式
        /// </summary>
        [DefaultValue( TitleLineLayoutType.Normal )]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "LayoutType")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineLayoutType LayoutType
        {
            get
            {
                return _LayoutType; 
            }
            set
            {
                _LayoutType = value; 
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 运行时使用的布局方式
        /// </summary>
        internal TitleLineLayoutType RuntimeLayoutType
        {
            get
            {
                if (this._LayoutType == TitleLineLayoutType.AutoCascade)
                {
                    if (this.IsExpanded)
                    {
                        return TitleLineLayoutType.Cascade;
                    }
                    else
                    {
                        return TitleLineLayoutType.HorizCascade;
                    }
                }
                return this._LayoutType;
            }
        }
#endif
        private int _TickStep = 1;
        /// <summary>
        /// 跨越时间刻度个数
        /// </summary>
        [DefaultValue(1)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "TickStep")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int TickStep
        {
            get
            {
                return _TickStep; 
            }
            set
            {
                if (value >= 1)
                {
                    _TickStep = value;
                }
                else
                {
                    _TickStep = 1;
                }
            }
        }

        private bool _TickLineVisible = true;
        /// <summary>
        /// 刻度线是否可见
        /// </summary>
        [DefaultValue(true)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "TickLineVisible")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool TickLineVisible
        {
            get
            {
                return _TickLineVisible;
            }
            set
            {
                _TickLineVisible = value;
            }
        }
#if !DCWriterForWASM

        //private DCTimeUnit _TimeSpanUnit = DCTimeUnit.Hour;
        ///// <summary>
        ///// 时刻单位
        ///// </summary>
        //[DefaultValue( DCTimeUnit.Hour )]
        //[XmlAttribute]
        //[DCDisplayName(typeof(TitleLineInfo), "TimeSpanUnit")]
        //public DCTimeUnit TimeSpanUnit
        //{
        //    get
        //    {
        //        return _TimeSpanUnit; 
        //    }
        //    set
        //    {
        //        _TimeSpanUnit = value; 
        //    }
        //}

        //private int _TimeSpan = 1;
        ///// <summary>
        ///// 采用时刻文本时的时间跨度
        ///// </summary>
        //[DefaultValue( 1 )]
        //[XmlAttribute]
        //[DCDisplayName(typeof(TitleLineInfo), "TimeSpan")]
        //public int TimeSpan
        //{
        //    get
        //    {
        //        return _TimeSpan; 
        //    }
        //    set
        //    {
        //        _TimeSpan = value; 
        //    }
        //}

        private bool _UpAndDownText = false;
        /// <summary>
        /// 是否上下交替绘制文本
        /// </summary>
        [DefaultValue(false )]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "UpAndDownText")]
        [ReadOnly(true)]
        [Obsolete("此属性已废弃，请设置UpAndDownTextType来确定文本上下交替样式")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool UpAndDownText
        {
            get
            {
                return _UpAndDownText; 
            }
            set
            {
                _UpAndDownText = value;
            }
        }
#endif
        private bool _ForceUpWhenPageFirstPoint = false;
        /// <summary>
        /// 获取或设置每一页的首个带上下交替文本样式的数据点是否强制使用靠上样式
        /// </summary>
        [DefaultValue(false)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "ForceUpWhenFirstPointFirstPage")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ForceUpWhenPageFirstPoint
        {
            get
            {
                return _ForceUpWhenPageFirstPoint;
            }
            set
            {
                _ForceUpWhenPageFirstPoint = value;
            }
        }

        private UpAndDownTextType _UpAndDownTextType=UpAndDownTextType.None;
        /// <summary>
        /// 上下交替绘制文本样式
        /// </summary>
        [DefaultValue(UpAndDownTextType.None)]
        [XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "UpAndDownTextType")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public UpAndDownTextType UpAndDownTextType
        {
            get
            {
                return _UpAndDownTextType;
            }
            set
            { 
                _UpAndDownTextType = value;
#if !DCWriterForWASM

                if (_UpAndDownTextType == UpAndDownTextType.None)
                {
                    _UpAndDownText = false;
                }
#endif
            }
        }

#if !DCWriterForWASM

       

        /// <summary>
        /// 运行时使用的开始计算的日期
        /// </summary>
        [NonSerialized]
        internal DateTime[] _RuntimeStartDates = null;
#endif
       

        private TitleLineValueType _ValueType = TitleLineValueType.SerialDate;
        /// <summary>
        /// 样式
        /// </summary>
        [DefaultValue( TitleLineValueType.SerialDate )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "ValueType")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineValueType ValueType
        {
            get
            {
                return _ValueType; 
            }
            set
            {
                _ValueType = value; 
            }
        }

      


        private string _DataSourceName = null;
        /// <summary>
        /// 数据源的名称
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "DataSourceName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DataSourceName
        {
            get
            {
                return _DataSourceName;
            }
            set
            {
                _DataSourceName = value;
            }
        }

        private string _ValueFieldName = null;
        /// <summary>
        /// 数据点数值绑定字段的名称
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "ValueFieldName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ValueFieldName
        {
            get
            {
                return _ValueFieldName;
            }
            set
            {
                _ValueFieldName = value;
            }
        }

        private string _TextFieldName = null;
        /// <summary>
        /// 数据点文本绑定字段的名称
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "TextFieldName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TextFieldName
        {
            get
            {
                return _TextFieldName;
            }
            set
            {
                _TextFieldName = value;
            }
        }

        private string _TimeFieldName = null;
        /// <summary>
        /// 表示数据时间的字段名
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(TitleLineInfo), "TimeFieldName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TimeFieldName
        {
            get
            {
                return _TimeFieldName;
            }
            set
            {
                _TimeFieldName = value;
            }
        } 

         
        private YAxisScaleInfoList _Scales = null;
        /// <summary>
        /// 自定义的刻度信息列表
        /// </summary>
        [XmlArrayItem("Scale", typeof(YAxisScaleInfo))]
        [DefaultValue(null)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public YAxisScaleInfoList Scales
        {
            get
            {
                //if (_Scales == null)
                //{
                //    _Scales = new YAxisScaleInfoList();
                //}
                return _Scales;
            }
            set
            {
                _Scales = value;
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name + " " + this.Title + " " +  this.GetType().Name;
        }
#endif
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineInfo Clone()
        {
            TitleLineInfo info = (TitleLineInfo)this.MemberwiseClone();
           
            return info;
        }
#if !DCWriterForWASM

        
        //代表当前标题行的标题格方框
        [XmlIgnore]
        internal RectangleF _TitleBounds = RectangleF.Empty;

        //代表标题行的整个方框
        [XmlIgnore]
        internal RectangleF _LineBounds = RectangleF.Empty;

        //代表当前标题行的所有数据格子方框
        [XmlIgnore]
        internal List<RectangleF> _BlockBoundsList = null;

        //代表当前标题行本次需要绘制的数据点的合集
        [XmlIgnore]
        internal List<ValuePoint> _ValuePointsForDraw = null;

        //代表当前标题行本次需要绘制的数据点的合集
        [XmlIgnore]
        internal List<RectangleF> _TickRectForDraw = null;
        internal void WriteJson(DCFastJsonTextWriter writer)
        {
            writer.WritePropertyNoFixName("Title", this.Title);
            writer.WritePropertyNoFixName("Name", this.Name);
            writer.WritePropertyNoFixName("StartDateKeyword", this.StartDateKeyword);
            writer.WritePropertyNoFixNameBoolean("AfterOperaDaysFromZero", this.AfterOperaDaysFromZero);
            writer.WritePropertyNoFixNameBoolean("AfterOperaDaysBeginOne", this.AfterOperaDaysBeginOne);
            writer.WritePropertyNoFixNameBoolean("PreserveStartKeywordOrder", this.PreserveStartKeywordOrder);
            writer.WritePropertyNoFixName("LayoutType", this.LayoutType.ToString());
            writer.WritePropertyNoFixName("TickStep", this.TickStep.ToString());
            writer.WritePropertyNoFixNameBoolean("TickLineVisible", this.TickLineVisible);
            writer.WritePropertyNoFixName("ValueType", this.ValueType.ToString());
            writer.WritePropertyNoFixName("SpecifyHeight", this.SpecifyHeight.ToString());
            writer.WritePropertyNoFixNameBoolean("AutoHeight", this.AutoHeight);
            writer.WritePropertyNoFixName("TextFontName", this.Font != null ? this.Font.Name : null);
            writer.WritePropertyNoFixName("TextFontSize", this.Font != null ? this.Font.Size.ToString() : null);
            writer.WritePropertyNoFixName("LoopTextList", this.LoopTextList);
            writer.WritePropertyNoFixName("UpAndDownTextType", this.UpAndDownTextType.ToString());
            writer.WritePropertyNoFixName("TitleAlign", this.TitleAlign.ToString());
            writer.WritePropertyNoFixName("SpecifyTitleWidth", this.SpecifyTitleWidth.ToString());
            writer.WritePropertyNoFixName("PageTitleTexts", this.PageTitleTexts);
            writer.WritePropertyNoFixNameBoolean("BlankDateWhenNoData", this.BlankDateWhenNoData);
        }
#endif
    }

    /// <summary>
    /// 对象列表
    /// </summary>
#if !DCWriterForWASM
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
     
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
#endif
    public partial class TitleLineInfoList : List<TitleLineInfo>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TitleLineInfoList()
        {
        }
#if !DCWriterForWASM

        /// <summary>
        /// 获得运行时使用的标题行列表
        /// </summary>
        /// <returns></returns>
        internal TitleLineInfoList GetRuntimeInfos()
        {
            TitleLineInfoList list = new TitleLineInfoList();
            TitleLineInfo lastGroupHeader = null;
            foreach (TitleLineInfo line in this)
            {
                line.ShowExpandedHandle = false ;
                if (line.LayoutType == TitleLineLayoutType.AutoCascade)
                {
                    line.ShowExpandedHandle = true;
                }
                if (lastGroupHeader != null && line.GroupName == lastGroupHeader.GroupName )
                {
                    line.ShowExpandedHandle = false;
                    if (lastGroupHeader.IsExpanded == false)
                    {
                        continue;
                    }
                }
                if (string.IsNullOrEmpty(line.GroupName) == false)
                {
                    if (lastGroupHeader == null || lastGroupHeader.GroupName != line.GroupName)
                    {
                        lastGroupHeader = line;
                        line.ShowExpandedHandle = true;
                    }
                }
                if (line.LayoutType == TitleLineLayoutType.AutoCascade 
                    && string.IsNullOrEmpty(line.GroupName))
                {
                    line.ShowExpandedHandle = true;
                }
                if (line.RuntimeVisible && line.Visible)
                {
                    list.Add(line);
                }
            }//foreach
            return list;
        }

        /// <summary>
        /// 总的运行时高度
        /// </summary>
        internal float TotalRuntimeHeight
        {
            get
            {
                float v = 0;
                foreach (TitleLineInfo line in this)
                {
                    v += line.RuntimeHeight;
                }
                return v;
            }
        }

        /// <summary>
        /// 获得指定名称的项目
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>项目</returns>
        public TitleLineInfo GetItemByName(string name)
        {
            foreach (TitleLineInfo item in this)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
#endif
    }
    /// <summary>
    /// 标题行数据类型
    /// </summary>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(true)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    [System.Runtime.InteropServices.Guid("DF516135-FAEB-41C0-877D-1956B6F15847")]
#endif
    public enum TitleLineValueType
    {
        /// <summary>
        ///  新增日期显示方式 住院日期首页第一天及跨年度第一天需写年、月、日，每页体温单的第一天及跨月份第一天需写月、日，其余只填日,宋建明
        /// </summary>
        NewSerialDate,
        /// <summary>
        /// 一系列的日期数
        /// </summary>
        SerialDate  ,
        /// <summary>
        /// 全局的天数
        /// </summary>
        GlobalDayIndex ,
        /// <summary>
        /// 不推荐使用，仅保留向下兼容性全局，请改用GlobalDayIndex
        /// </summary>
        [Browsable( false )]
        [EditorBrowsable(  EditorBrowsableState.Never )]
        InDayIndex ,
        /// <summary>
        /// 天数
        /// </summary>
        DayIndex ,
        /// <summary>
        /// 小时刻数
        /// </summary>
        HourTick ,
        /// <summary>
        /// 文本
        /// </summary>
        Text ,
        /// <summary>
        /// 数据
        /// </summary>
        Data ,
        /// <summary>
        /// 精确到刻度的文本
        /// </summary>
        TickText 
    }

    /// <summary>
    /// 扩展网格线样式
    /// </summary>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
#endif
    public enum DCExtendGridLineType
    {
        /// <summary>
        /// 不显示
        /// </summary>
        None ,
        /// <summary>
        /// 在上面
        /// </summary>
        Above,
        /// <summary>
        /// 在下面
        /// </summary>
        Below
    }

    /// <summary>
    /// 上下交替文本样式
    /// </summary>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
#endif
    public enum UpAndDownTextType
    {
        /// <summary>
        /// 不交替
        /// </summary>
        None,
        /// <summary>
        /// 根据时刻交替
        /// </summary>
        ShowByTick,
        /// <summary>
        /// 根据文本交替
        /// </summary>
        ShowByText
    }
}
