using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection ;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.Common ;
using DCSoft.Drawing;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// Y坐标轴信息
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
#if !DCWriterForWASM
    [Serializable]
    [DCSoft.Common.DCPublishAPI]
    //[DCSoft.Common.DCDescriptionResourceSource(typeof(DCSoft.TemperatureChart.DCTimeLineDescriptionResource))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
#endif
    public partial class YAxisInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public YAxisInfo()
        {
        }

        private float _MaxTextDisplayLength = 0f;
        /// <summary>
        /// 最大文本显示长度
        /// </summary>
        [DefaultValue( 0f )]
        [DCDisplayName(typeof(YAxisInfo), "MaxTextDisplayLength")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        [Category("Appearance")]
        public float MaxTextDisplayLength
        {
            get
            {
                return _MaxTextDisplayLength; 
            }
            set
            {
                _MaxTextDisplayLength = value; 
            }
        }

        private bool _FixTopHeightForPadding = false;
        /// <summary>
        /// 为内边距修正标题顶端位置和高度
        /// </summary>
        internal bool FixTopHeightForPadding
        {
            get
            {
                return _FixTopHeightForPadding; 
            }
            set
            {
                _FixTopHeightForPadding = value; 
            }
        }

        private bool _MergeIntoLeft = false;
        /// <summary>
        /// 并入到左边的标尺
        /// </summary>
        [DefaultValue(false)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "MergeIntoLeft")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool MergeIntoLeft
        {
            get
            {
                return _MergeIntoLeft; 
            }
            set
            {
                _MergeIntoLeft = value; 
            }
        }

        private bool _BorderVisible = true;
        /// <summary>
        /// 是否显示Y轴标尺的边框
        /// </summary>
        [DefaultValue(true)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "BorderVisible")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool BorderVisible
        {
            get
            {
                return _BorderVisible;
            }
            set
            {
                _BorderVisible = value;
            }
        }

        private string _Name = null;
        /// <summary>
        /// 名称
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo),"Name")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
#if WINFORM || DCWriterForWinFormNET6
        [Editor(typeof(EditNameUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
#endif
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

        private float _TopPadding = TemperatureDocument.InnerNullValue;
        /// <summary>
        /// 数据网格线顶端空白边距比率，取值范围从0.0到1.0之间，若属性值为空则采用DocumentConfig.DataGridTopPadding属性值。
        /// </summary>
        [DefaultValue(TemperatureDocument.InnerNullValue)]
        [DCDisplayName(typeof(YAxisInfo), "TopPadding")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float TopPadding
        {
            get
            {
                return _TopPadding;
            }
            set
            {
                _TopPadding = value;
            }
        }

        private float _BottomPadding = TemperatureDocument.InnerNullValue;
        /// <summary>
        /// 数据网格线底端空白边距比率，取值范围从0.0到1.0之间，若属性值为空则采用DocumentConfig.DataGridBottomPadding属性值。
        /// </summary>
        [DefaultValue(TemperatureDocument.InnerNullValue)]
        [DCDisplayName(typeof(YAxisInfo), "BottomPadding")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float BottomPadding
        {
            get
            {
                return _BottomPadding;
            }
            set
            {
                _BottomPadding = value;
            }
        }
#if !DCWriterForWASM

        [NonSerialized]
        private float _RuntimeTopPadding = 0;
        internal float RuntimeTopPadding
        {
            get
            {
                return _RuntimeTopPadding; 
            }
        }

        [NonSerialized]
        private float _RuntimeBottomPadding = 0;
        internal float RuntimeBottomPadding
        {
            get
            {
                return _RuntimeBottomPadding; 
            }
        }

        /// <summary>
        /// 更新运行时使用的内边距比率
        /// </summary>
        /// <param name="document"></param>
        internal void UpdateRuntimePadding(TemperatureDocument document)
        {
            this._RuntimeTopPadding = 0;
            if (this.TopPadding >= 0)
            {
                // 使用本地配置
                this._RuntimeTopPadding = this.TopPadding;
            }
            else
            {
                if (document.Config.DataGridTopPadding >= 0)
                {
                    // 使用文档全局配置
                    this._RuntimeTopPadding = document.Config.DataGridTopPadding;
                }
            }
            if (this._RuntimeTopPadding >= 1)
            {
                this._RuntimeTopPadding = 0;
            }

            this._RuntimeBottomPadding = 0;
            if (this.BottomPadding >= 0)
            {
                // 使用本地配置
                this._RuntimeBottomPadding = this.BottomPadding;
            }
            else
            {
                if (document.Config.DataGridBottomPadding >= 0)
                {
                    // 使用文档全局配置
                    this._RuntimeBottomPadding = document.Config.DataGridBottomPadding;
                }
            }
            if (this._RuntimeBottomPadding >= 1)
            {
                this._RuntimeBottomPadding = 0;
            }
        }
#endif
        private bool _HighlightOutofNormalRange = true;
        /// <summary>
        /// 数据点超出正常范围时高亮度显示
        /// </summary>
        [DefaultValue( true )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "HighlightOutofNormalRange")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool HighlightOutofNormalRange
        {
            get 
            {
                return _HighlightOutofNormalRange; 
            }
            set
            {
                _HighlightOutofNormalRange = value; 
            }
        }

        private DateTimePrecisionMode _InputTimePrecision = DateTimePrecisionMode.Minute;
        /// <summary>
        /// 输入时间的精度
        /// </summary>
        [DefaultValue( DateTimePrecisionMode.Minute )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "InputTimePrecision")]
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

        private int _ValuePrecision = 2;
        /// <summary>
        /// 数值精度,也就是保持小数点后面几位。
        /// </summary>
        [DefaultValue( 2 )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "ValuePrecision")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ValuePrecision
        {
            get
            {
                return _ValuePrecision; 
            }
            set
            {
                _ValuePrecision = value; 
            }
        }
#if !DCWriterForWASM
        internal float FixForValuePrecison(float v)
        {
            if (this.ValuePrecision < 0)
            {
                return v;
            }
            else if (TemperatureDocument.IsNullValue(v))
            {
                return v;
            }
            else
            {
                float result = (float)Math.Round(v, this.ValuePrecision);
                return result;
            }
        }
#endif
        private bool _AllowInterrupt = true;
        /// <summary>
        /// 允许数据线中断
        /// </summary>
        [DefaultValue( true )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "AllowInterrupt")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool AllowInterrupt
        {
            get
            {
                return _AllowInterrupt; 
            }
            set 
            {
                _AllowInterrupt = value; 
            }
        }


        private XFontValue _Font = null;
        /// <summary>
        /// 字体
        /// </summary>
        [DefaultValue(null)]
        [XmlElement]
        [DCDisplayName(typeof(YAxisInfo), "Font")]
        [Browsable(true)]
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
        [DCDisplayName(typeof(YAxisInfo), "ValueFont")]
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


        private int _LineWidth = 1;
        /// <summary>
        /// 线条宽度
        /// </summary>
        [DefaultValue( 1 )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "LineWidth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int LineWidth
        {
            get
            {
                return _LineWidth; 
            }
            set
            {
                _LineWidth = value; 
            }
        }
        private Color _LanternValueColorForUp = Color.Blue;
        /// <summary>
        /// 物理升温颜色
        /// </summary>
        [Browsable(true)]
        [DefaultColorValue("Blue")]
        [DCDisplayName(typeof(YAxisInfo), "LanternValueColorForUp")]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color LanternValueColorForUp
        {
            get
            {
                return _LanternValueColorForUp;
            }
            set
            {
                _LanternValueColorForUp = value;
            }
        }
        /// <summary>
        /// 文本形式的物理升温颜色
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [DCDisplayName(typeof(YAxisInfo), "LanternValueColorForUpValue")]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string LanternValueColorForUpValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.LanternValueColorForUp, Color.Blue);
            }
            set
            {
                this.LanternValueColorForUp = XMLSerializeHelper.StringToColor(value, Color.Blue);
            }
        }
        private Color _LanternValueColorForDown = Color.Red;
        /// <summary>
        /// 物理降温颜色
        /// </summary>
        [Browsable(true)]
        [DefaultColorValue("Red")]
        [DCDisplayName(typeof(YAxisInfo), "LanternValueColorForDown")]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color LanternValueColorForDown
        {
            get
            { 
                return _LanternValueColorForDown; 
            }
            set
            { 
                _LanternValueColorForDown = value;
            }
        }
        /// <summary>
        /// 文本形式的物理降温颜色
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCDisplayName(typeof(YAxisInfo), "LanternValueColorForDownValue")]
        public string LanternValueColorForDownValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.LanternValueColorForDown, Color.Red);
            }
            set
            {
                this.LanternValueColorForDown = XMLSerializeHelper.StringToColor(value, Color.Red);
            }
        }

        private DashStyle _LineStyleForLanternValue = DashStyle.Dash;
        /// <summary>
        /// 自定义灯笼点的连线的线型
        /// </summary>
        [Browsable(true)]
        [DefaultValue(DashStyle.Dash)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCDisplayName(typeof(YAxisInfo), "LineStyleForLanternValue")]
        public DashStyle LineStyleForLanternValue
        {
            get
            {
                return _LineStyleForLanternValue;
            }
            set
            {
                _LineStyleForLanternValue = value;
            }
        }

        private float _SymbolSize = 20f;
        /// <summary>
        /// 图例大小
        /// </summary>
        [DefaultValue( 20f )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "SymbolSize")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SymbolSize
        {
            get
            {
                return _SymbolSize; 
            }
            set
            {
                _SymbolSize = value; 
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 运行时使用的线条宽度
        /// </summary>
        internal int RuntimeLineWidth
        {
            get
            {
                if (this.Selected)
                {
                    return (int)Math.Ceiling(this.LineWidth * 1.5);
                }
                else
                {
                    return this.LineWidth;
                }
            }
        }

        /// <summary>
        /// 运行时使用的图例大小
        /// </summary>
        internal float RuntimeSymbolSize
        {
            get
            {
                if (this.Selected)
                {
                    return (int)Math.Ceiling(this.SymbolSize * 1.5);
                }
                else
                {
                    return this.SymbolSize;
                }
            }
        }
#endif
        private float _SpecifyTitleWidth = 0f;
        /// <summary>
        /// 指定的标题宽度,小于等于0为无效设置，将自动计算标题宽度。采用GraphicUnit.Document为单位。
        /// </summary>
        [DefaultValue( 0f )]
        [DCDisplayName(typeof(YAxisInfo), "SpecifyTitleWidth")]
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

        private ValuePointDataSourceInfo _DataSource = null;
        /// <summary>
        /// 数据源
        /// </summary>
        [DefaultValue(null)]
        [XmlElement]
        [DCDisplayName(typeof(YAxisInfo), "DataSource")]
        [Browsable( true )]
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
         
        private bool _AllowOutofRange = false ;
        /// <summary>
        /// 允许数值超出范围
        /// </summary>
        [DefaultValue( false )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "AllowOutofRange")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool AllowOutofRange
        {
            get
            {
                return _AllowOutofRange; 
            }
            set
            {
                _AllowOutofRange = value; 
            }
        }

        private bool _SeparatorLineVisible  = true;
        /// <summary>
        /// 文本分隔线是否显示
        /// </summary>
        [DefaultValue(true)]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "SeparatorLineVisible")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool SeparatorLineVisible
        {
            get
            {
                return _SeparatorLineVisible;
            }
            set
            {
                _SeparatorLineVisible = value;
            }
        }

        private bool _ClickToHide = true;
        /// <summary>
        /// 鼠标点击来隐藏数据
        /// </summary>
        [DefaultValue( true )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "ClickToHide")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ClickToHide
        {
            get
            {
                return _ClickToHide; 
            }
            set
            {
                _ClickToHide = value; 
            }
        }

        private bool _Visible = true;
        /// <summary>
        /// 对象是否可见
        /// </summary>
        [DefaultValue( true )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "Visible")]
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

        private bool _ValueVisible = true;
        /// <summary>
        /// 数值是否可见
        /// </summary>
        [DefaultValue(true)]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "ValueVisible")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ValueVisible
        {
            get
            {
                return _ValueVisible; 
            }
            set
            {
                _ValueVisible = value; 
            }
        }

        private bool _EnableLanternValue = false;
        /// <summary>
        /// 允许使用灯笼数据
        /// </summary>
        [DefaultValue( false )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "EnableLanternValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool EnableLanternValue
        {
            get
            {
                return _EnableLanternValue; 
            }
            set
            {
                _EnableLanternValue = value; 
            }
        }

        private string _LanternValueTitle = null;
        /// <summary>
        /// 灯笼数据名称
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "LanternValueTitle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string LanternValueTitle
        {
            get 
            {
                return _LanternValueTitle; 
            }
            set
            {
                _LanternValueTitle = value; 
            }
        }

        private YAxisInfoStyle _Style = YAxisInfoStyle.Value;
        /// <summary>
        /// 坐标轴样式
        /// </summary>
        [DefaultValue( YAxisInfoStyle.Value )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "Style")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public YAxisInfoStyle Style
        {
            get
            {
                return _Style; 
            }
            set
            {
                _Style = value; 
            }
        }

        private string _HollowCovertTargetName = null;
        /// <summary>
        /// 空心覆盖目标名称
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "HollowCovertTargetName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string HollowCovertTargetName
        {
            get
            {
                return _HollowCovertTargetName; 
            }
            set
            {
                _HollowCovertTargetName = value; 
            }
        }

        //private DCHollowCoverMode _HollowCoverDetectMode = DCHollowCoverMode.None;
        ///// <summary>
        ///// 空心覆盖检测模式
        ///// </summary>
        //[DefaultValue( DCHollowCoverMode.None )]
        //[XmlAttribute]
        //[DCDisplayName(typeof(YAxisInfo), "HollowCoverDetectMode")]
        //public DCHollowCoverMode HollowCoverDetectMode
        //{
        //    get
        //    {
        //        return _HollowCoverDetectMode; 
        //    }
        //    set
        //    {
        //        _HollowCoverDetectMode = value; 
        //    }
        //}
 
        private string _ShadowName = null;
        /// <summary>
        /// 阴影数据线名称
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "ShadowName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ShadowName
        {
            get
            {
                return _ShadowName; 
            }
            set
            {
                _ShadowName = value;
#if !DCWriterForWASM

                _ShadowInfo = null;
#endif
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 对应的阴影数据点对象
        /// </summary>
        [NonSerialized]
        internal YAxisInfo ReverseShadowInfo = null;

        private YAxisInfo _ShadowInfo = null;
        /// <summary>
        /// 引用数据线对象
        /// </summary>
        internal YAxisInfo ShadowInfo
        {
            get
            {
                return _ShadowInfo; 
            }
            set
            {
                _ShadowInfo = value; 
            }
        }
#endif
        private bool _ShadowPointVisible = true;
        /// <summary>
        /// 心率脉搏阴影区域是否显示
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCDisplayName(typeof(YAxisInfo), "ShadowPointVisible")]
        public bool ShadowPointVisible
        {
            get 
            { 
                return _ShadowPointVisible; 
            }
            set 
            { 
                _ShadowPointVisible = value;
            }
        }


        private YAxisShadowStyle _ShadowStyle =  YAxisShadowStyle.LeftSlant;
        /// <summary>
        /// 心率脉搏阴影区域的填充样式
        /// </summary>
        [Browsable(true)]
        [DefaultValue(YAxisShadowStyle.LeftSlant)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCDisplayName(typeof(YAxisInfo), "ShadowStyle")]
        public YAxisShadowStyle ShadowStyle
        {
            get
            {
                return _ShadowStyle;
            }
            set
            {
                _ShadowStyle = value;
            }
        }

        private bool _VerticalLine = false;
        /// <summary>
        /// 控制脉搏和心率之间的数值连线是否显示
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        [DCDisplayName(typeof(YAxisInfo), "VerticalLine")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public bool VerticalLine
        {
            get
            {
                return _VerticalLine; 
            }
            set 
            {
                _VerticalLine = value;
            }
        }
        private string _TitleValueDispalyFormat = null;
        /// <summary>
        /// 标题数值格式字符串
        /// </summary>
        [DefaultValue( null )]
        [DCDisplayName(typeof(YAxisInfo), "TitleValueDispalyFormat")]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TitleValueDispalyFormat
        {
            get
            {
                return _TitleValueDispalyFormat; 
            }
            set
            {
                _TitleValueDispalyFormat = value; 
            }
        }

        private bool _TitleVisible = true;
        /// <summary>
        /// 是否显示Y坐标刻度和标题
        /// </summary>
        [DefaultValue( true )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "TitleVisible")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool TitleVisible
        {
            get
            {
                return _TitleVisible;
            }
            set
            {
                _TitleVisible = value; 
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 运行时的标题是否可见
        /// </summary>
        internal bool RuntimeTitleVisible = true;
#endif

        private string _Title = null;
        /// <summary>
        /// 标题
        /// </summary>
        [DefaultValue( null )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "Title")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
#if WINFORM || DCWriterForWinFormNET6
        [Editor(typeof(EditTitleUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
#endif
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
#if !DCWriterForWASM

        /// <summary>
        /// 刻度尺区域
        /// </summary>
        internal RectangleF TitleBounds
        {
            get
            {
                return new RectangleF(
                    this.TitleLeft, 
                    this.TitleTop, 
                    this.TitleWidth, 
                    this.TitleHeight);
            }
        }

        private float _TitleLeft = 0;
        /// <summary>
        /// 标题区域左端位置
        /// </summary>
        [Browsable( false )]
        [XmlIgnore]
        public float TitleLeft
        {
            get
            {
                return _TitleLeft; 
            }
            set
            {
                if (_TitleLeft != value)
                {
                    this._TitleLeft = value;
                }
            }
        }

        private YAxisInfo _MergedInfo = null;
        /// <summary>
        /// 获取或设置与该Y轴合并的上一个Y轴
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        internal YAxisInfo MergeInfo
        {
            get
            {
                return _MergedInfo;
            }
            set
            {
                this._MergedInfo = value;
            }
        }

        private float _TitleTop = 0;
        /// <summary>
        /// 标题区域顶端位置
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public float TitleTop
        {
            get
            {
                return _TitleTop;
            }
            set
            {
                _TitleTop = value;
            }
        }

        /// <summary>
        /// 标题区域的低端位置
        /// </summary>
        [Browsable( false )]
        [XmlIgnore]
        public float TitleBottom
        {
            get
            {
                return this._TitleTop + this._TitleHeight;
            }
        }

        private float _TitleWidth = 0;
        /// <summary>
        /// 标题区域宽度
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public float TitleWidth
        {
            get
            {
                return _TitleWidth;
            }
            set
            {
                //if (value != _TitleWidth)
                {
                    _TitleWidth = value;
                }
            }
        }

        private float _TitleHeight = 0;
        /// <summary>
        /// 标题区域宽度
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public float TitleHeight
        {
            get
            {
                return _TitleHeight;
            }
            set
            {
                _TitleHeight = value;
            }
        }
#endif
        private int _YSplitNum = 8;
        /// <summary>
        /// Y轴分割区域数量
        /// </summary>
        [DefaultValue(8)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "YSplitNum")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int YSplitNum
        {
            get
            {
                return _YSplitNum;
            }
            set
            {
                _YSplitNum = value;
            }
        }

        private string _ValueFormatString = null;
        /// <summary>
        /// 数值格式化字符串
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "ValueFormatString")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ValueFormatString
        {
            get
            {
                return _ValueFormatString; 
            }
            set
            { 
                _ValueFormatString = value; 
            }
        }



#if !DCWriterForWASM

        internal float GetDisplayY(TemperatureDocument document, RectangleF dataGridBounds, float Value)
        {
            float rate = GetDisplayScaleRate(Value);
            if (TemperatureDocument.IsNullValue(rate))
            {
                return float.NaN;
            }
            else
            {
                return dataGridBounds.Top
                    + dataGridBounds.Height *  this.RuntimeTopPadding 
                    + dataGridBounds.Height * (1f - this.RuntimeTopPadding - this.RuntimeBottomPadding ) * ( 1f - rate);
            }
        }

        /// <summary>
        /// 检查数值是否超限
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal bool CheckValueRange(float v)
        {
            if (TemperatureDocument.IsNullValue(v))
            {
                return true;
            }
            if (v > this.MaxValue || v < this.MinValue)
            {
                // 超出范围
                if (this.AllowOutofRange)
                {
                    // 允许超范围
                    return true;
                }
                else
                {
                    //  不允许超范围
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 根据显示比例获得数值
        /// </summary>
        /// <returns>对应的数值</returns>
        /// <param name="document">文档对象</param>
        /// <param name="dataGridBounds">数据边界坐标</param>
        /// <param name="displayY">Y轴</param>
        internal float GetValueByDisplayY(TemperatureDocument document , RectangleF dataGridBounds , float displayY )
        {
            float yMax = dataGridBounds.Top + dataGridBounds.Height * (1f - this.RuntimeBottomPadding );
            float yMin = dataGridBounds.Top + dataGridBounds.Height * this.RuntimeTopPadding;
            float rate = 1 - (displayY - yMin) / (yMax - yMin);
            if (TemperatureDocument.IsNullValue(rate))
            {
                return float.NaN;
            }
            if (this.HasScales)
            {
                for (int iCount = 1; iCount < this.Scales.Count; iCount++)
                {
                    YAxisScaleInfo scale = this.Scales[iCount];
                    if (rate >= scale.ScaleRate)
                    {
                        // 获得上一个刻度
                        YAxisScaleInfo preScale = this.Scales[iCount - 1];
                        // 在两个刻度之间进行线性插值
                        float result = 0;
                        if (rate > preScale.ScaleRate || preScale.ScaleRate == scale.ScaleRate )
                        {
                            result = preScale.Value;
                        }
                        else
                        {
                            result = scale.Value
                                + (preScale.Value - scale.Value)
                                * ((rate - scale.ScaleRate) / (preScale.ScaleRate - scale.ScaleRate));
                        }
                        return FixForValuePrecison(result) ;
                    }
                }
                return 0;
            }
            else
            {
                if (rate >= 1)
                {
                    return this.MaxValue;
                }
                else if (rate <= 0)
                {
                    return this.MinValue;
                }
                else
                {
                    return FixForValuePrecison( this.MinValue + (this.MaxValue - this.MinValue) * rate );
                }
            }
        }

        /// <summary>
        /// 获得显示的比率
        /// </summary>
        /// <param name="Value">数值</param>
        /// <returns>显示的比率</returns>
        private float GetDisplayScaleRate(float Value)
        {
            this._OutofRangeFlag = 0;
            if (TemperatureDocument.IsNullValue(Value))
            {
                return float.NaN;
            }
            if ( this.HasScales )
            {
                for (int iCount = 1 ; iCount < this.Scales.Count ; iCount ++ )
                {
                    YAxisScaleInfo scale = this.Scales[iCount];
                    if (Value >= scale.Value)
                    {
                        // 获得上一个刻度
                        YAxisScaleInfo preScale = this.Scales[iCount - 1];
                        // 在两个刻度之间进行线性插值
                        float rate = 0;
                        if (Value > preScale.Value)
                        {
                            rate = preScale.ScaleRate ;
                        }
                        else
                        {
                            rate = scale.ScaleRate 
                                + ( preScale.ScaleRate - scale.ScaleRate ) 
                                * (( Value - scale.Value ) / ( preScale.Value - scale.Value ));
                        }
                        return rate;
                    }
                }
                return 0;
            }
            else
            {
                if (Value > this.MaxValue)
                {
                    this._OutofRangeFlag = 1;
                    if (this.AllowOutofRange)
                    {
                        return 1f;
                    }
                    else
                    {
                        return float.NaN;
                    }
                }
                if (Value < this.MinValue)
                {
                    this._OutofRangeFlag = -1;
                    if (this.AllowOutofRange)
                    {
                        return 0f;
                    }
                    else
                    {
                        return float.NaN;
                    }
                }
                return (Value - this.MinValue )/( this.MaxValue - this.MinValue );
            }
        }

        private int _OutofRangeFlag = 0;
        /// <summary>
        /// 超出取值范围标记
        /// </summary>
        [Browsable( false )]
        internal int OutofRangeFlag
        {
            get
            {
                return _OutofRangeFlag; 
            }
        }
#endif

        //private bool _TextFlagMode = false;
        ///// <summary>
        ///// 文本标记模式
        ///// </summary>
        //[DefaultValue( false )]
        //[System.Xml.Serialization.XmlAttribute]
        //public bool TextFlagMode
        //{
        //    get
        //    {
        //        return _TextFlagMode; 
        //    }
        //    set
        //    {
        //        _TextFlagMode = value; 
        //    }
        //}


        private Color _AlertLineColor = Color.Red;
        /// <summary>
        /// 警戒线（原红线）的颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "AlertLineColor")]
        [DefaultColorValue("Black")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color AlertLineColor
        {
            get
            {
                return _AlertLineColor;
            }
            set
            {
                _AlertLineColor = value;
            }
        }

        /// <summary>
        /// 警戒线（原红线）的颜色字符串
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string AlertLineColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.AlertLineColor, Color.Black);
            }
            set
            {
                this.AlertLineColor = XMLSerializeHelper.StringToColor(value, Color.Black);
            }
        }



        private float _RedLineValue = TemperatureDocument.InnerNullValue;
        /// <summary>
        /// 水平红线对应的数值
        /// </summary>
        [DefaultValue( TemperatureDocument.InnerNullValue)]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "RedLineValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float RedLineValue
        {
            get 
            {
                return _RedLineValue; 
            }
            set
            {
                _RedLineValue = value; 
            }
        }

        private float _RedLineWidth = 1f;

        /// <summary>
        /// 水平紅线宽度
        /// </summary>
        [DefaultValue(1f)]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "RedLineWidth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float RedLineWidth
        {
            get
            {
                return _RedLineWidth;
            }
            set
            {
                _RedLineWidth = value;
            }
        }

        private bool _RedLinePrintVisible = true;
        /// <summary>
        /// 打印时是否显示红线
        /// </summary>
        [DefaultValue(true)]
        [DCDisplayName(typeof(YAxisInfo), "RedLinePrintVisible")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool RedLinePrintVisible
        {
            get 
            {
                return _RedLinePrintVisible; 
            }
            set 
            {
                _RedLinePrintVisible = value; 
            }
        }

        private Color _ValueTextBackColor = Color.Transparent;
        /// <summary>
        /// 数值文本背景色
        /// </summary>
        [DefaultColorValue("Transparent")]
        [XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "ValueTextBackColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color ValueTextBackColor
        {
            get
            {
                return _ValueTextBackColor;
            }
            set
            {
                _ValueTextBackColor = value;
            }
        }
        /// <summary>
        /// 文本形式的ValueTextBackColor属性值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ValueTextBackColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.ValueTextBackColor, Color.Transparent);
            }
            set
            {
                this.ValueTextBackColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        #region 废弃的属性，合并到AbNormalRangeSettings当中

        //private Color _NormalRangeBackColor = Color.Transparent;
        ///// <summary>
        ///// 正常数值区域背景色
        ///// </summary>
        //[DefaultColorValue("Transparent")]
        //[XmlIgnore]
        //[DCDisplayName(typeof(YAxisInfo), "NormalRangeBackColor")]
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public Color NormalRangeBackColor
        //{
        //    get
        //    {
        //        return _NormalRangeBackColor;
        //    }
        //    set
        //    {
        //        _NormalRangeBackColor = value;
        //    }
        //}
        ///// <summary>
        ///// 文本形式的NormalRangeBackColor属性值
        ///// </summary>
        //[Browsable(false)]
        //[DefaultValue(null)]
        //[System.Xml.Serialization.XmlAttribute]
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public string NormalRangeBackColorValue
        //{
        //    get
        //    {
        //        return XMLSerializeHelper.ColorToString(this.NormalRangeBackColor, Color.Transparent);
        //    }
        //    set
        //    {
        //        this.NormalRangeBackColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
        //    }
        //}

        //private Color _OutofNormalRangeBackColor = Color.Transparent;
        ///// <summary>
        ///// 超出正常区域背景色
        ///// </summary>
        //[DefaultColorValue("Transparent")]
        //[XmlIgnore]
        //[DCDisplayName(typeof(YAxisInfo), "OutofNormalRangeBackColor")]
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public Color OutofNormalRangeBackColor
        //{
        //    get
        //    {
        //        return _OutofNormalRangeBackColor;
        //    }
        //    set
        //    {
        //        _OutofNormalRangeBackColor = value;
        //    }
        //}
        ///// <summary>
        ///// 文本形式的OutofNormalRangeBackColor属性值
        ///// </summary>
        //[Browsable(false)]
        //[DefaultValue(null)]
        //[System.Xml.Serialization.XmlAttribute]
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public string OutofNormalRangeBackColorValue
        //{
        //    get
        //    {
        //        return XMLSerializeHelper.ColorToString(this.OutofNormalRangeBackColor, Color.Transparent);
        //    }
        //    set
        //    {
        //        this.OutofNormalRangeBackColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
        //    }
        //}

        //private float _NormalMaxValue = TemperatureDocument.InnerNullValue;
        ///// <summary>
        ///// 数值正常范围的最大值
        ///// </summary>
        //[DefaultValue(TemperatureDocument.InnerNullValue)]
        //[System.Xml.Serialization.XmlAttribute]
        //[DCDisplayName(typeof(YAxisInfo), "NormalMaxValue")]
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public float NormalMaxValue
        //{
        //    get
        //    {
        //        return _NormalMaxValue;
        //    }
        //    set
        //    {
        //        _NormalMaxValue = value;
        //    }
        //}

        //private float _NormalMinValue = TemperatureDocument.NullValue;
        ///// <summary>
        ///// 数值正常范围的最小值
        ///// </summary>
        //[DefaultValue(TemperatureDocument.InnerNullValue)]
        //[System.Xml.Serialization.XmlAttribute]
        //[DCDisplayName(typeof(YAxisInfo), "NormalMinValue")]
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public float NormalMinValue
        //{
        //    get
        //    {
        //        return _NormalMinValue;
        //    }
        //    set
        //    {
        //        _NormalMinValue = value;
        //    }
        //}
        #endregion

        private AbNormalRangeSettings _AbNormalRangeSettings = null;
        /// <summary>
        /// Y轴异常数值区域样式配置信息
        /// </summary>
        [Browsable(true)]
        [DCDisplayName(typeof(YAxisInfo), "AbNormalRangeSettings")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public AbNormalRangeSettings AbNormalRangeSettings
        {
            get
            {
                if (_AbNormalRangeSettings == null)
                {
                    _AbNormalRangeSettings = new AbNormalRangeSettings();
                }
                return _AbNormalRangeSettings;
            }
            set
            {
                _AbNormalRangeSettings = value;
            }
        }

        ///// <summary>
        ///// 判断数值是否超出正常值范围
        ///// </summary>
        ///// <param name="v">数值</param>
        ///// <returns>是否超出正常值范围</returns>
        //internal bool IsOoutofNormalRange(float v)
        //{
        //    if (TemperatureDocument.IsNullValue(v))
        //    {
        //        return false;
        //    }
        //    if (TemperatureDocument.IsNullValue(this.NormalMinValue) == false )
        //    {
        //        if (v < this.NormalMinValue)
        //        {
        //            return true;
        //        }
        //    }
        //    if (TemperatureDocument.IsNullValue(this.NormalMaxValue) == false)
        //    {
        //        if (v > this.NormalMaxValue)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        private float _MaxValue = 100f;
        /// <summary>
        /// 最大值
        /// </summary>
        [DefaultValue( 100f )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "MaxValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float MaxValue
        {
            get
            {
                return _MaxValue; 
            }
            set
            {
                _MaxValue = value; 
            }
        }

        private float _MinValue = 0;
        /// <summary>
        /// 最小值
        /// </summary>
        [DefaultValue( 0f)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "MinValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float MinValue
        {
            get
            {
                return _MinValue; 
            }
            set
            {
                _MinValue = value; 
            }
        }

        private bool _ShowLegendInRule = true;
        /// <summary>
        /// 在标尺中显示图例
        /// </summary>
        [DefaultValue( true )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "ShowLegendInRule")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ShowLegendInRule
        {
            get 
            {
                return _ShowLegendInRule; 
            }
            set
            {
                _ShowLegendInRule = value; 
            }
        }

        private bool _ShowPointValue = false;
        /// <summary>
        /// 获取或设置是否在数据点的图例旁边显示数据点的值
        /// </summary>
        [DefaultValue(false)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "ShowPointValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ShowPointValue
        {
            get
            {
                return _ShowPointValue;
            }
            set
            {
                _ShowPointValue = value;
            }
        }

        private Color _ColorForPointValue = Color.Black;
        /// <summary>
        /// 显示数据点正常数值所使用的颜色
        /// </summary>
        [XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "ColorForPointValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color ColorForPointValue
        {
            get
            {
                return _ColorForPointValue;
            }
            set
            {
                _ColorForPointValue = value;
            }
        }
        /// <summary>
        /// 显示数据点正常数值所使用的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ColorValueForPointValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.ColorForPointValue, Color.Transparent);
            }
            set
            {
                this.ColorForPointValue = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private Color _ColorForDownValue = Color.Red;
        /// <summary>
        /// 显示数据点数值低于下限时所使用的颜色
        /// </summary>
        [XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "ColorForDownValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color ColorForDownValue
        {
            get
            {
                return _ColorForDownValue;
            }
            set
            {
                _ColorForDownValue = value;
            }
        }
        /// <summary>
        /// 显示数据点数值低于下限时所使用的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ColorValueForDownValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.ColorForDownValue, Color.Transparent);
            }
            set
            {
                this.ColorForDownValue = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private Color _ColorForUpValue = Color.Red;
        /// <summary>
        /// 显示数据点数值超上限时所使用的颜色
        /// </summary>
        [XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "ColorForUpValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color ColorForUpValue
        {
            get
            {
                return _ColorForUpValue;
            }
            set
            {
                _ColorForUpValue = value;
            }
        }
        /// <summary>
        /// 显示数据点数值超上限时所使用的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ColorValueForUpValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.ColorForUpValue, Color.Transparent);
            }
            set
            {
                this.ColorForUpValue = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////

        private ValuePointSymbolStyle _SymbolStyle = ValuePointSymbolStyle.SolidCicle;
        /// <summary>
        /// 数据点符号类型
        /// </summary>
        [DefaultValue( ValuePointSymbolStyle.SolidCicle )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "SymbolStyle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePointSymbolStyle SymbolStyle
        {
            get
            {
                return _SymbolStyle; 
            }
            set
            {
                _SymbolStyle = value; 
            }
        }

        private float _SymbolOffsetX = 0f;
        /// <summary>
        /// 数据点符号绘制横向偏移
        /// </summary>
        [DefaultValue(0f)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "SymbolOffsetX")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SymbolOffsetX
        {
            get
            {
                return _SymbolOffsetX;
            }
            set
            {
                _SymbolOffsetX = value;
            }
        }

        private float _SymbolOffsetY = 0f;
        /// <summary>
        /// 数据点符号绘制纵向偏移
        /// </summary>
        [DefaultValue(0f)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "SymbolOffsetY")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SymbolOffsetY
        {
            get
            {
                return _SymbolOffsetY;
            }
            set
            {
                _SymbolOffsetY = value;
            }
        }

        private char _CharacterForCharSymbolStyle = 'R';
        /// <summary>
        /// 获取或设置当SpecifySymbolStyle枚举被设置成字符或套圈字符时，此处将要使用的字符变量
        /// </summary>
        //[DefaultValue('R')]
        [System.Xml.Serialization.XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "CharacterForCharSymbolStyle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public char CharacterForCharSymbolStyle
        {
            get
            {
                return _CharacterForCharSymbolStyle;
            }
            set
            {
                _CharacterForCharSymbolStyle = value;
            }
        }

        [DefaultValue(82)]
        [Browsable(false)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int IntCharSymbol
        {
            get
            {
                return (int)CharacterForCharSymbolStyle;
            }
            set
            {
                CharacterForCharSymbolStyle = (char)value;
            }
        }

        private string _BottomTitle = null;
        /// <summary>
        /// 底端标题
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "BottomTitle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string BottomTitle
        {
            get
            {
                return _BottomTitle; 
            }
            set
            {
                _BottomTitle = value; 
            }
        }

        private Color _TitleBackColor = Color.Transparent;
        /// <summary>
        /// 标题背景颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "TitleBackColor")]
        [DefaultColorValue("Transparent")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color TitleBackColor
        {
            get
            {
                return _TitleBackColor;
            }
            set
            {
                _TitleBackColor = value;
            }
        }

        /// <summary>
        /// 文本形式的标题背景颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TitleBackColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.TitleBackColor, Color.Transparent);
            }
            set
            {
                this.TitleBackColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private Color _HiddenValueTitleBackColor = Color.LightGray;
        /// <summary>
        /// 数据隐藏时的标题背景色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "HiddenValueTitleBackColor")]
        [DefaultColorValue("LightGray")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color HiddenValueTitleBackColor
        {
            get
            {
                return _HiddenValueTitleBackColor;
            }
            set
            {
                _HiddenValueTitleBackColor = value;
            }
        }

        /// <summary>
        /// 文本形式的标题文本颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string HiddenValueTitleBackColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.HiddenValueTitleBackColor, Color.LightGray);
            }
            set
            {
                this.HiddenValueTitleBackColor = XMLSerializeHelper.StringToColor(value, Color.LightGray);
            }
        }

        private Color _TitleColor = Color.Black;
        /// <summary>
        /// 标题文本颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "TitleColor")]
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

        private Color _SymbolColor = Color.Red;
        /// <summary>
        /// 数据点符号颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "SymbolColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color SymbolColor
        {
            get
            {
                return _SymbolColor; 
            }
            set
            {
                _SymbolColor = value; 
            }
        }

        private bool _Selected = false;
        /// <summary>
        /// 被选择状态
        /// </summary>
        [DefaultValue( false )]
        [Browsable( false )]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Selected
        {
            get
            {
                return _Selected; 
            }
            set
            {
                _Selected = value; 
            }
        }

        /// <summary>
        /// 文本形式的颜色值
        /// </summary>
        [Browsable( false )]
        [DefaultValue("Red")]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SymbolColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.SymbolColor, Color.Red);
            }
            set
            {
                this.SymbolColor = XMLSerializeHelper.StringToColor(value, Color.Red);
            }
        }

        ///// <summary>
        ///// Y坐标轴区域左边界位置
        ///// </summary>
        //[NonSerialized]
        //internal float ViewLeft = 0f;
        /// <summary>
        /// 在坐标轴区域顶端位置
        /// </summary>
        [NonSerialized]
        internal float GridViewTop = 0f;
        internal float GridViewHeight = 0f;
        ///// <summary>
        ///// Y坐标轴文本在视图中占据的宽度
        ///// </summary>
        //[NonSerialized]
        //internal float ViewWidth = 0f;
       
        private string _DataSourceName = null;
        /// <summary>
        /// 数据源的名称
        /// </summary>
        [DefaultValue( null )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "DataSourceName")]
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
        [DCDisplayName(typeof(YAxisInfo), "ValueFieldName")]
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

        private string _LanternValueFieldName = null;
        /// <summary>
        /// 灯笼数据字段名称
        /// </summary>
        [DefaultValue( null )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "LanternValueFieldName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string LanternValueFieldName
        {
            get
            {
                return _LanternValueFieldName; 
            }
            set
            {
                _LanternValueFieldName = value; 
            }
        }

        private ValuePointSymbolStyle _SpecifyLanternSymbolStyle = ValuePointSymbolStyle.HollowCicle;
        /// <summary>
        /// 指定的数据点的灯笼数据的图例样式
        /// </summary>
        [DefaultValue(ValuePointSymbolStyle.HollowCicle)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "SpecifyLanternSymbolStyle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePointSymbolStyle SpecifyLanternSymbolStyle
        {
            get
            {
                return _SpecifyLanternSymbolStyle;
            }
            set
            {
                _SpecifyLanternSymbolStyle = value;
            }
        }

        private char _CharacterForLanternSymbolStyle = 'R';
        /// <summary>
        /// 获取或设置当灯笼数据图例枚举被设置成字符或套圈字符时，此处将要使用的字符变量
        /// </summary>
        //[DefaultValue('R')]
        [System.Xml.Serialization.XmlIgnore]
        [DCDisplayName(typeof(YAxisInfo), "CharacterForLanternSymbolStyle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public char CharacterForLanternSymbolStyle
        {
            get
            {
                return _CharacterForLanternSymbolStyle;
            }
            set
            {
                _CharacterForLanternSymbolStyle = value;
            }
        }

        [DefaultValue(82)]
        [Browsable(false)]
        [XmlAttribute]
        public int IntCharLantern
        {
            get
            {
                return (int)CharacterForLanternSymbolStyle;
            }
            set
            {
                CharacterForLanternSymbolStyle = (char)value;
            }
        }

        private string _TimeFieldName = null;
        /// <summary>
        /// 表示数据时间的字段名
        /// </summary>
        [DefaultValue( null )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "TimeFieldName")]
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

        //private ValuePointList _Values = new ValuePointList();
        ///// <summary>
        ///// 数据点列表
        ///// </summary>
        //[XmlArrayItem("Value" , typeof( ValuePoint ))]
        //public ValuePointList Values
        //{
        //    get
        //    {
        //        if (_Values == null)
        //        {
        //            _Values = new ValuePointList();
        //        }
        //        return _Values; 
        //    }
        //    set
        //    {
        //        _Values = value; 
        //    }
        //}
#if !DCWriterForWASM


        /// <summary>
        /// 上一次数据点坐标
        /// </summary>
        [NonSerialized]
        internal PointF LastPoint = new PointF(float.NaN, float.NaN);
        /// <summary>
        /// 上一次数据点对象
        /// </summary>
        [NonSerialized]
        internal ValuePoint LastValuePoint = null;
#endif
        private YAxisScaleInfoList _Scales = null;
        /// <summary>
        /// 自定义的刻度信息列表
        /// </summary>
        [XmlArrayItem("Scale" , typeof( YAxisScaleInfo ))]
        [DefaultValue( null )]
        [Browsable( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCDisplayName(typeof(YAxisInfo), "Scales")]
        public YAxisScaleInfoList Scales
        {
            get
            {
                if (_Scales == null)
                {
                    _Scales = new YAxisScaleInfoList();
                }
                return _Scales; 
            }
            set
            {
                _Scales = value; 
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 存在有效的自定义的刻度信息
        /// </summary>
        internal bool HasScales
        {
            get
            {
                return this._Scales != null && this._Scales.Count > 2;
            }
        }

        /// <summary>
        /// 返回表示对象数值的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name + " " +  this.Title + " " + this.GetType().Name;
        }
         /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public YAxisInfo Clone()
        {
            YAxisInfo info = (YAxisInfo)this.MemberwiseClone();
            //if (cloneValues)
            //{
            //    if (this._Values != null)
            //    {
            //        info._Values = this._Values.Clone();
            //    }
            //}
            //else
            //{
            //    info._Values = null;
            //}
            if (this._Scales != null)
            {
                info._Scales = new YAxisScaleInfoList();
                foreach (YAxisScaleInfo info2 in this._Scales)
                {
                    info._Scales.Add(info2);
                }
            }
            return info;
        }


        internal void WriteJson(DCFastJsonTextWriter writer)
        {
            writer.WritePropertyNoFixName("Title", this.Title);
            writer.WritePropertyNoFixName("Name", this.Name);
            writer.WritePropertyNoFixNameBoolean("AllowOutofRange", this.AllowOutofRange);
            writer.WritePropertyNoFixNameBoolean("EnableLanternValue", this.EnableLanternValue);
            writer.WritePropertyNoFixName("MaxValue", this.MaxValue.ToString());
            writer.WritePropertyNoFixName("MinValue", this.MinValue.ToString());
            writer.WritePropertyNoFixName("SymbolColorValue", this.SymbolColorValue);
            writer.WritePropertyNoFixName("SymbolStyle", this.SymbolStyle.ToString());
            writer.WritePropertyNoFixName("YSplitNum", this.YSplitNum.ToString());
            writer.WritePropertyNoFixName("Style", this.Style.ToString());
            writer.WritePropertyNoFixName("SymbolSize", this.SymbolSize.ToString());
            writer.WritePropertyNoFixName("TitleColorValue", this.TitleColorValue);            
            writer.WritePropertyNoFixName("ShadowName", this.ShadowName);
            writer.WritePropertyNoFixName("CharSymbol", this.CharacterForCharSymbolStyle.ToString());
            writer.WritePropertyNoFixName("CharLantern", this.CharacterForLanternSymbolStyle.ToString());
            writer.WritePropertyNoFixNameBoolean("TitleVisible", this.TitleVisible);
            writer.WritePropertyNoFixNameBoolean("ShowLegendInRule", this.ShowLegendInRule);
            writer.WritePropertyNoFixName("BottomTitle", this.BottomTitle);
            writer.WritePropertyNoFixName("RedLineValue", this.RedLineValue.ToString());
            writer.WritePropertyNoFixName("AlertLineColorValue", this.AlertLineColorValue);
            writer.WritePropertyNoFixNameBoolean("VerticalLine", this.VerticalLine);
            writer.WritePropertyNoFixNameBoolean("ShadowPointVisible", this.ShadowPointVisible);
            writer.WritePropertyNoFixNameBoolean("AllowInterrupt", this.AllowInterrupt);
            writer.WritePropertyNoFixName("HollowCovertTargetName", this.HollowCovertTargetName);
            writer.WritePropertyNoFixName("LanternValueColorForUpValue", this.LanternValueColorForUpValue);
            writer.WritePropertyNoFixName("LanternValueColorForDownValue", this.LanternValueColorForDownValue);
            writer.WritePropertyNoFixName("TopPadding", this.TopPadding.ToString());
            writer.WritePropertyNoFixName("BottomPadding", this.BottomPadding.ToString());
            writer.WritePropertyNoFixNameBoolean("MergeIntoLeft", this.MergeIntoLeft);
            writer.WritePropertyNoFixName("SpecifyTitleWidth", this.SpecifyTitleWidth.ToString());
            writer.WritePropertyNoFixName("LineStyleForLanternValue", this.LineStyleForLanternValue.ToString());
            writer.WritePropertyUseAttribute("ValueTextBackColorValue", this.ValueTextBackColorValue);
            writer.WritePropertyNoFixNameBoolean("ShowPointValue", this.ShowPointValue);
        }

#endif
    }

    /// <summary>
    /// Y坐标轴信息列表
    /// </summary>
#if !DCWriterForWASM
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true  )]
#endif
    public partial class YAxisInfoList : List<YAxisInfo>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public YAxisInfoList()
        {
        }
#if !DCWriterForWASM

        public YAxisInfo LastInfo
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[this.Count - 1];
                }
                else
                {
                    return null;
                }
            }
        }
        public float TotalTitleWidth
        {
            get
            {
                float result = 0;
                foreach (YAxisInfo item in this)
                {
                    if (item.MergeIntoLeft == false )
                    {
                        result += item.TitleWidth;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 获取该Y轴集合在横向上有多少Y轴（要考虑到合并的情况）
        /// </summary>
        public int HorizontalYAxisCount
        {
            get
            {
                int result = 0;
                foreach (YAxisInfo item in this)
                {
                    if (item.MergeIntoLeft == false)
                    {
                        result++;
                    }
                }
                return result;
            }
        }


        /// <summary>
        /// 获得指定名称的项目
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>找到的项目</returns>
        public YAxisInfo GetItemByName(string name)
        {
            foreach (YAxisInfo item in this)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public YAxisInfoList Clone()
        {
            YAxisInfoList list = new YAxisInfoList();
            foreach( YAxisInfo info in this )
            {
                list.Add( info.Clone(  ));
            }
            return list ;
        }

#endif
    }
}
