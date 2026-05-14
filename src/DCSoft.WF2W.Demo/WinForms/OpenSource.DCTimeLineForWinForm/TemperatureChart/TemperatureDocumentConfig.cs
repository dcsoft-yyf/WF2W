using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using DCSoft.Common;
using System.Drawing;
using DCSoft.Drawing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 体温单文档配置信息对象
    /// </summary>
    /// <remarks>
    /// 袁永福到此一游
    /// </remarks>

#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(false)]
    [Serializable]
    [DCSoft.Common.DCPublishAPI]
    [TypeConverter( typeof( DCSoft.Common.TypeConverterSupportProperties))]
    //[DCSoft.Common.DCDescriptionResourceSource(typeof(DCSoft.TemperatureChart.DCTimeLineDescriptionResource))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
#endif
    public partial class TemperatureDocumentConfig
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TemperatureDocumentConfig()
        {
            //this.CheckDefaultTicks();
            this._Version = "1.0";

            //20170204伍贻超添加：(DCWRITER-1405)为防止XML文档中的PageSettings段落被人为删除，导致加载文档时此字段为null引发各类BUG，在此处添加初始化手段
            this._PageSettings = new DocumentPageSettings();
        }

        private bool _BothBlackWhenPrint = false;
        /// <summary>
        /// 打印时全部采用黑色
        /// </summary>
        [DefaultValue( false )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "BothBlackWhenPrint")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool BothBlackWhenPrint
        {
            get
            {
                return _BothBlackWhenPrint; 
            }
            set
            {
                _BothBlackWhenPrint = value; 
            }
        }

        private float _LineWidthZoomRateWhenPrint = 1f;
        /// <summary>
        /// 打印时的线条粗细缩放比率
        /// </summary>
       [DefaultValue( 1f )]
       [DCDisplayName(typeof(TemperatureDocumentConfig), "LineWidthZoomRateWhenPrint")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float LineWidthZoomRateWhenPrint
        {
            get { return _LineWidthZoomRateWhenPrint; }
            set { _LineWidthZoomRateWhenPrint = value; }
        }
          


        private DocumentLinkVisualStyle _LinkVisualStyle = DocumentLinkVisualStyle.Hover;
        /// <summary>
        /// 超链接可视化样式
        /// </summary>
        [DefaultValue( DocumentLinkVisualStyle.Hover )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "LinkVisualStyle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DocumentLinkVisualStyle LinkVisualStyle
        {
            get
            {
                return _LinkVisualStyle; 
            }
            set
            {
                _LinkVisualStyle = value; 
            }
        }

        private bool _DebugMode = false;
        /// <summary>
        /// 调试模式
        /// </summary>
        [DefaultValue( false )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "DebugMode")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool DebugMode
        {
            get
            {
                return _DebugMode; 
            }
            set
            {
                _DebugMode = value; 
            }
        }
        private EditValuePointEventHandleMode _EditValuePointMode = EditValuePointEventHandleMode.None;
        /// <summary>
        /// 数据点编辑模式
        /// </summary>
        [DefaultValue( EditValuePointEventHandleMode.None )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "EditValuePointMode")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public EditValuePointEventHandleMode EditValuePointMode
        {
            get
            {
                return _EditValuePointMode; 
            }
            set
            {
                _EditValuePointMode = value; 
            }
        }


        private bool _EnableExtMouseMoveEvent = false;
        /// <summary>
        /// 是否启用增强型的鼠标移动事件处理逻辑
        /// </summary>
        [DefaultValue(false)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "EnableExtMouseMoveEvent")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool EnableExtMouseMoveEvent
        {
            get
            {
                return _EnableExtMouseMoveEvent;
            }
            set
            {
                _EnableExtMouseMoveEvent = value;
            }
        }


        private bool _EnableDataGridLinearAxisMode = false;
        /// <summary>
        /// 获取或设置数据网格区是否使用线性坐标模式
        /// </summary>
        [DefaultValue(false)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "EnableDataGridLinearAxisMode")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool EnableDataGridLinearAxisMode
        {
            get
            {
                return _EnableDataGridLinearAxisMode;
            }
            set
            {
                _EnableDataGridLinearAxisMode = value;
            }
        }


        private bool _Readonly = false;
        /// <summary>
        /// 文档是只读的
        /// </summary>
        [DefaultValue( false )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "Readonly")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Readonly
        {
            get
            {
                return _Readonly; 
            }
            set
            {
                _Readonly = value; 
            }
        }

        private float _ExtendDaysForTimeLine = 0;
        /// <summary>
        /// 为时间轴模式而延长的天数
        /// </summary>
        [DefaultValue( 0f )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "ExtendDaysForTimeLine")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float ExtendDaysForTimeLine
        {
            get
            {
                return _ExtendDaysForTimeLine; 
            }
            set
            {
                _ExtendDaysForTimeLine = value; 
            }
        }

       

        private string _IllegalTextEndCharForLinux = null;
        /// <summary>
        /// 设置LINUX下需要特殊处理的文本字符串结尾字符集合
        /// </summary>
        [DefaultValue(null)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "IllegalTextEndCharForLinux")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string IllegalTextEndCharForLinux
        {
            get
            {
                return _IllegalTextEndCharForLinux;
            }
            set
            {
                _IllegalTextEndCharForLinux = value;
            }
        }


        private string _TitleForToolTip = null;
        /// <summary>
        /// 提示信息的标题文本
        /// </summary>
        [DefaultValue(null)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "TitleForToolTip")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TitleForToolTip
        {
            get
            {
                return _TitleForToolTip;
            }
            set
            {
                _TitleForToolTip = value;
            }
        }


        private bool _EnableCustomValuePointSymbol = false;
        /// <summary>
        /// 获取或设置是否启用自定义绘制数据点图标
        /// </summary>
        [DefaultValue(false)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "EnableCustomValuePointSymbol")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool EnableCustomValuePointSymbol
        {
            get
            {
                return _EnableCustomValuePointSymbol;
            }
            set
            {
                _EnableCustomValuePointSymbol = value;
            }
        }


        private StringAlignment _HeaderLabelLineAlignment = StringAlignment.Far;
        /// <summary>
        /// 获取或设置标题标签的竖向对齐方式
        /// </summary>
        [DefaultValue(StringAlignment.Far)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "HeaderLabelLineAlignment")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment HeaderLabelLineAlignment
        {
            get
            {
                return _HeaderLabelLineAlignment;
            }
            set
            {
                _HeaderLabelLineAlignment = value;
            }
        }

        private DCTimeLineSelectionMode _SelectionMode = DCTimeLineSelectionMode.None;
        /// <summary>
        /// 选择模式
        /// </summary>
        [DefaultValue( DCTimeLineSelectionMode.None )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "SelectionMode")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineSelectionMode SelectionMode
        {
            get
            {
                return _SelectionMode; 
            }
            set
            {
                _SelectionMode = value; 
            }
        }

        private bool _ShowTooltip = true;
        /// <summary>
        /// 是否显示提示文本 
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "ShowTooltip")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ShowTooltip
        {
            get
            {
                return _ShowTooltip;
            }
            set
            {
                _ShowTooltip = value;
            }
        }

        private bool _AllowUserCollapseZone = true;
        /// <summary>
        /// 允许用户收缩时间区域
        /// </summary>
        [DefaultValue( true )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "AllowUserCollapseZone")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool AllowUserCollapseZone
        {
            get
            {
                return _AllowUserCollapseZone; 
            }
            set
            {
                _AllowUserCollapseZone = value; 
            }
        }


        private string _Version = null;
        /// <summary>
        /// 版本号
        /// </summary>
        [XmlAttribute]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "Version")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Version
        {
            get
            {
                return _Version; 
            }
            set
            {
                _Version = value; 
            }
        }

       

        private DCTimeUnit _TickUnit = DCTimeUnit.Hour;
        /// <summary>
        /// 标准时刻单位
        /// </summary>
        [DefaultValue( DCTimeUnit.Hour )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "TickUnit")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeUnit TickUnit
        {
            get
            {
                return _TickUnit; 
            }
            set
            {
                _TickUnit = value; 
            }
        }

        private float _DataGridTopPadding = 0f;
        /// <summary>
        /// 数据网格线顶端空白边距比率，取值范围从0.0到1.0之间
        /// </summary>
        [DefaultValue( 0f )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "DataGridTopPadding")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float DataGridTopPadding
        {
            get
            {
                return _DataGridTopPadding; 
            }
            set
            {
                _DataGridTopPadding = value; 
            }
        }

        private float _DataGridBottomPadding = 0f;
        /// <summary>
        /// 数据网格线底端空白边距比率，取值范围从0.0到1.0之间
        /// </summary>
        [DefaultValue(0f)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "DataGridBottomPadding")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float DataGridBottomPadding
        {
            get
            {
                return _DataGridBottomPadding;
            }
            set
            {
                _DataGridBottomPadding = value;
            }
        }

        private string _SQLTextForHeaderLabel = null;
        /// <summary>
        /// 查询标题头数据使用的SQL语句
        /// </summary>
        [DefaultValue(null)]
        [XmlElement]
#if WINFORM || DCWriterForWinFormNET6
        [Editor(typeof(dlgSQLText.SQLTextEditor), typeof(System.Drawing.Design.UITypeEditor))]
#endif
        [DCDisplayName(typeof(TemperatureDocumentConfig), "SQLTextForHeaderLabel")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SQLTextForHeaderLabel
        {
            get
            {
                return _SQLTextForHeaderLabel;
            }
            set
            {
                _SQLTextForHeaderLabel = value;
            }
        }

        private float _SpecifyTickWidth = 0f;
        /// <summary>
        /// 指定的最小刻度长度，小于等于0则自动计算，默认为0.
        /// </summary>
        [DefaultValue(0f)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "SpecifyTickWidth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SpecifyTickWidth
        {
            get
            {
                return _SpecifyTickWidth; 
            }
            set
            {
                _SpecifyTickWidth = value; 
            }
        }

        private DCTimeLineImageList _Images = new DCTimeLineImageList();
        /// <summary>
        /// 贴图列表
        /// </summary>
        [XmlArrayItem( "Image" , typeof( DCTimeLineImage ))]
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineImageList Images
        {
            get
            {
                return _Images; 
            }
            set
            {
                _Images = value; 
            }
        }

        private DCTimeLineLabelList _Labels = new DCTimeLineLabelList();
        /// <summary>
        /// 额外的文本标签列表
        /// </summary>
        [XmlArrayItem("Label" , typeof( DCTimeLineLabel ))]
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineLabelList Labels
        {
            get
            {
                return _Labels; 
            }
            set
            {
                _Labels = value; 
            }
        }

        private string _PageIndexText = "第[%pageindex%]页";
        /// <summary>
        /// 页码文本
        /// </summary>
        [DefaultValue( null )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "PageIndexText")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string PageIndexText
        {
            get
            {
                return _PageIndexText; 
            }
            set
            {
                _PageIndexText = value; 
            }
        }

        private string _SpecifyStartDate = null;
        /// <summary>
        /// 指定的开始日期
        /// </summary>
        [DefaultValue( null )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "SpecifyStartDate")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SpecifyStartDate
        {
            get
            {
                return _SpecifyStartDate; 
            }
            set
            {
                _SpecifyStartDate = value; 
            }
        }

        private string _SpecifyEndDate = null;
        /// <summary>
        /// 指定的结束日期
        /// </summary>
        [DefaultValue( null )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "SpecifyEndDate")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SpecifyEndDate
        {
            get
            {
                return _SpecifyEndDate; 
            }
            set
            {
                _SpecifyEndDate = value; 
            }
        }

        private DocumentPageSettings _PageSettings = null ;
        /// <summary>
        /// 页面设置
        /// </summary>
        [DefaultValue( null )]
        [Browsable( true )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "PageSettings")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DocumentPageSettings PageSettings
        {
            get
            {
                return _PageSettings; 
            }
            set
            {
                _PageSettings = value; 
            }
        }
         

        private string _FooterDescription = null;
        /// <summary>
        /// 在文档下面显示的标注
        /// </summary>
        [DefaultValue( null )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "FooterDescription")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FooterDescription
        {
            get
            {
                return _FooterDescription; 
            }
            set
            {
                _FooterDescription = value; 
            }
        }

        private bool _ShowIcon = false ;
        /// <summary>
        /// 是否显示文字上面的图标
        /// </summary>
        [DefaultValue( false  )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "ShowIcon")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ShowIcon
        {
            get
            {
                return _ShowIcon; 
            }
            set
            {
                _ShowIcon = value; 
            }
        }

        private int _ImagePixelWidth = 16;
        /// <summary>
        /// 图片像素宽度
        /// </summary>
        [DefaultValue( 16 )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "ImagePixelWidth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ImagePixelWidth
        {
            get
            {
                return _ImagePixelWidth; 
            }
            set
            {
                _ImagePixelWidth = value; 
            }
        }

        private int _ImagePixelHeight = 16;
        /// <summary>
        /// 图片像素高度
        /// </summary>
        [DefaultValue(16)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "ImagePixelHeight")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ImagePixelHeight
        {
            get
            {
                return _ImagePixelHeight;
            }
            set
            {
                _ImagePixelHeight = value;
            }
        }

        private int _ShadowPointDetectSeconds = 2000;
        /// <summary>
        /// 检测阴影数据点的时钟秒数
        /// </summary>
        [DefaultValue(2000)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "ShadowPointDetectSeconds")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ShadowPointDetectSeconds
        {
            get
            {
                return _ShadowPointDetectSeconds;
            }
            set
            {
                _ShadowPointDetectSeconds = value;
            }
        }


        //private int _GridYSplitNum = 8;
        /// <summary>
        /// Y轴分割份数
        /// </summary>
        [DefaultValue(8)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "GridYSplitNum")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int GridYSplitNum
        {
            get
            {
                //return _GridYSplitNum;
                return this.GridYSplitInfo.GridYSplitNum;
            }
            set
            {
                //_GridYSplitNum = value;
                this.GridYSplitInfo.GridYSplitNum = value;
            }
        }


        private GridYSplitInfo _GridYSplitInfo = null;

        /// <summary>
        /// Y轴网格线配置信息
        /// </summary>
        [Browsable(true)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "GridYSplitInfo")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public GridYSplitInfo GridYSplitInfo
        {
            get
            {
                if (_GridYSplitInfo == null)
                {
                    _GridYSplitInfo = new GridYSplitInfo();
                }
                return _GridYSplitInfo;
            }
            set
            {
                _GridYSplitInfo = value;
            }
        }

        private TimeLineZoneInfoList _Zones = null;
        /// <summary>
        /// 时间区域信息列表
        /// </summary>
        [XmlArrayItem("Zone", typeof(TimeLineZoneInfo))]
        [Browsable(false)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "Zones")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TimeLineZoneInfoList Zones
        {
            get
            {
                if (_Zones == null)
                {
                    _Zones = new TimeLineZoneInfoList();
                }
                return _Zones; 
            }
            set
            {
                _Zones = value; 
            }
        }

#if !DCWriterForWASM
        /// <summary>
        /// 判断是否存在有效的时间区域
        /// </summary>
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool HasTimeLineZones
        {
            get
            {
                if (this.Zones.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
#endif
        private TickInfoList _Ticks = null;
        /// <summary>
        /// 刻度信息列表
        /// </summary>
        [XmlArrayItem( "Tick" , typeof( TickInfo ))]
        [Browsable( true )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "Ticks")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TickInfoList Ticks
        {
            get
            {
                if (_Ticks == null)
                {
                    _Ticks = new TickInfoList();
                }
                return _Ticks; 
            }
            set
            {
                _Ticks = value; 
            }
        }


        private string _TagString = string.Empty;
        /// <summary>
        /// 额外的字符串数据，用来存储额外的自定义信息
        /// </summary>
        [DefaultValue("")]
        [System.Xml.Serialization.XmlElement]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TagString
        {
            get
            {
                return _TagString;
            }
            set
            {
                this._TagString = value;
            }
        }

        internal void CheckDefaultTicks()
        {
            if (this._Ticks == null || this._Ticks.Count == 0)
            {
                this._Ticks = new TickInfoList();
                this._Ticks.AddItem(2, "2", Color.Red);
                this._Ticks.AddItem(6, "6", Color.Red);
                this._Ticks.AddItem(10, "10", Color.Black);
                this._Ticks.AddItem(14, "14", Color.Black);
                this._Ticks.AddItem(18, "18", Color.Black);
                this._Ticks.AddItem(22, "22", Color.Red);
            }
        }

       

         
        private float _SymbolSize = 20;
        /// <summary>
        /// 数据点符号大小，以Document(1/300英寸)为单位
        /// </summary>
        [DefaultValue(20f)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "SymbolSize")]
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

        private string _FontName = XFontValue.DefaultFontName;// System.Drawing.SystemFonts.DefaultFont.Name;
        /// <summary>
        /// 字体名称
        /// </summary>
        //[DefaultValue("宋体")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "FontName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FontName
        {
            get
            {
                return _FontName;
            }
            set
            {
                _FontName = value;
            }
        }

        private float _FontSize = 9f;
        /// <summary>
        /// 字体的大小
        /// </summary>
        [DefaultValue(9f)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "FontSize")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float FontSize
        {
            get
            {
                return _FontSize;
            }
            set
            {
                _FontSize = value;
            }
        }
        /// <summary>
        /// 运行时使用的字体
        /// </summary>
        internal XFontValue RuntimeFont
        {
            get
            {
                XFontValue f = new XFontValue();
                f.Name = this.FontName;
                f.Size = this.FontSize;
                return f;
            }
        }

        private float _BigTitleFontSize = 27f;
        /// <summary>
        /// 大标题使用的字体大小
        /// </summary>
        [DefaultValue( 27f )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "BigTitleFontSize")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float BigTitleFontSize
        {
            get
            {
                return _BigTitleFontSize; 
            }
            set
            {
                _BigTitleFontSize = value; 
            }
        }

        private string _BigTitleFontName = null;
        /// <summary>
        /// 大标题使用的字体名称
        /// </summary>
        [DefaultValue(null)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "BigTitleFontName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string BigTitleFontName
        {
            get
            {
                return _BigTitleFontName;
            }
            set
            {
                _BigTitleFontName = value;
            }
        }

        private bool _BigTitleFontBold = false;
        /// <summary>
        /// 大标题使用的字体是否加粗
        /// </summary>
        [DefaultValue(false)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "BigTitleFontBold")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool BigTitleFontBold
        {
            get
            {
                return _BigTitleFontBold;
            }
            set
            {
                _BigTitleFontBold = value;
            }
        }

        private XFontValue _PageIndexFont = null;
        /// <summary>
        /// 页码字体
        /// </summary>
        //[DefaultValue( null )]
        //[Browsable( true )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "PageIndexFont")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XFontValue PageIndexFont
        {
            get { return _PageIndexFont; }
            set { _PageIndexFont = value; }
        }

        /// <summary>
        /// 运行时真正使用的页码字体
        /// </summary>
        internal XFontValue RuntimePageIndexFont
        {
            get
            {
                XFontValue f = this.PageIndexFont;
                if (f == null)
                {
                    f = this.RuntimeFont;
                }
                return f;
            }
        } 
         
        private Color _ForeColor = Color.Black;
        /// <summary>
        /// 图形前景色，为默认的线条和文字颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(true)]
        [DefaultColorValue("Black")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "ForeColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
            }
        }

        /// <summary>
        /// 文本形式的线条颜色
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ForeColorValue
        {
            get
            {
                return DCTimeLineUtils.ColorToXMLString(this.ForeColor, Color.Black);
            }
            set
            {
                this.ForeColor = DCTimeLineUtils.XMLStringToColor(value, Color.Black);
            }
        }
         

        private Color _BigVerticalGridLineColor = Color.Red;
        /// <summary>
        /// 大的垂直网格线颜色
        /// </summary>
        [DefaultColorValue("Red")]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable( true )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "BigVerticalGridLineColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color BigVerticalGridLineColor
        {
            get
            {
                return _BigVerticalGridLineColor;
            }
            set
            {
                _BigVerticalGridLineColor = value;
            }
        }

        private float _BigVerticalGridLineWidth = 2;
        /// <summary>
        /// 大的垂直网格线宽度
        /// </summary>
        [DefaultValue(2f)]

        [Browsable(true)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "BigVerticalGridLineWidth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float BigVerticalGridLineWidth
        {
            get
            {
                return _BigVerticalGridLineWidth;
            }
            set
            {
                _BigVerticalGridLineWidth = value;
            }
        }

        /// <summary>
        /// 大的垂直网格线颜色
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string BigVerticalGridLineColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.BigVerticalGridLineColor, Color.Red);
            }
            set
            {
                this.BigVerticalGridLineColor = XMLSerializeHelper.StringToColor(value, Color.Red);
            }
        }

        private Color _BackColor = Color.Transparent;
        /// <summary>
        /// 页面背景色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(true)]
        [DefaultColorValue("Transparent")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "BackColor")]
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


        private bool _ValueTextTransparentBackColor = false;
        /// <summary>
        /// 数值文本是否是透明背景色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(true)]
        [DefaultValue(false)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "ValueTextTransparentBackColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ValueTextTransparentBackColor
        {
            get { return _ValueTextTransparentBackColor; }
            set { _ValueTextTransparentBackColor = value; }
        }

        /// <summary>
        /// 文本形式的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string BackColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.BackColor, Color.Transparent);
            }
            set
            {
                this.BackColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private Color _PageBackColor = Color.White;
        /// <summary>
        /// 页面背景色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(true)]
        [DefaultColorValue("White")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "PageBackColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color PageBackColor
        {
            get
            {
                return _PageBackColor;
            }
            set
            {
                _PageBackColor = value;
            }
        }

        /// <summary>
        /// 文本形式的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string PageBackColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.PageBackColor, Color.White);
            }
            set
            {
                this.PageBackColor = XMLSerializeHelper.StringToColor(value, Color.White);
            }
        }

        

        private Color _GridLineColor = Color.Black;
        /// <summary>
        /// 数据点符号颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [Browsable( true )]
        [DefaultColorValue("Black")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "GridLineColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color GridLineColor
        {
            get
            {
                return _GridLineColor;
            }
            set
            {
                _GridLineColor = value;
            }
        }

        /// <summary>
        /// 文本形式的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string GridLineColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.GridLineColor, Color.Black);
            }
            set
            {
                this.GridLineColor = XMLSerializeHelper.StringToColor(value, Color.Black);
            }
        }

        private Color _GridBackColor = Color.Transparent;
        /// <summary>
        /// 数据点符号颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(true)]
        [DefaultColorValue("Transparent")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "GridBackColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color GridBackColor
        {
            get
            {
                return _GridBackColor;
            }
            set
            {
                _GridBackColor = value;
            }
        }

        /// <summary>
        /// 文本形式的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string GridBackColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.GridBackColor, Color.Transparent);
            }
            set
            {
                this.GridBackColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private string _DateFormatString = "yyyy-MM-dd";
        /// <summary>
        /// 输出日期数据使用的格式化字符串
        /// </summary>
        [DefaultValue("yyyy-MM-dd")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "DateFormatString")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DateFormatString
        {
            get
            {
                return _DateFormatString;
            }
            set
            {
                _DateFormatString = value;
            }
        }

        private string _DateFormatStringForCrossYear = "yyyy-MM-dd";
        /// <summary>
        /// 输出跨年日期数据使用的格式化字符串
        /// </summary>
        [DefaultValue("yyyy-MM-dd")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "DateFormatStringForCrossYear")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DateFormatStringForCrossYear
        {
            get
            {
                return _DateFormatStringForCrossYear;
            }
            set
            {
                _DateFormatStringForCrossYear = value;
            }
        }

        private string _DateFormatStringForCrossMonth = "MM-dd";
        /// <summary>
        /// 输出跨月日期数据使用的格式化字符串
        /// </summary>
        [DefaultValue("MM-dd")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "DateFormatStringForCrossMonth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DateFormatStringForCrossMonth
        {
            get
            {
                return _DateFormatStringForCrossMonth;
            }
            set
            {
                _DateFormatStringForCrossMonth = value;
            }
        }

        private string _DateFormatStringForCrossWeek = "dd";
        /// <summary>
        /// 输出跨星期日期数据使用的格式化字符串
        /// </summary>
        [DefaultValue("dd")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "DateFormatStringForCrossWeek")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DateFormatStringForCrossWeek
        {
            get
            {
                return _DateFormatStringForCrossWeek;
            }
            set
            {
                _DateFormatStringForCrossWeek = value;
            }
        }

        private string _DateFormatStringForFirstIndexFirstPage = "yyyy-MM-dd";
        /// <summary>
        /// 输出首页首个日期数据使用的格式化字符串
        /// </summary>
        [DefaultValue("yyyy-MM-dd")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "DateFormatStringForFirstIndexFirstPage")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DateFormatStringForFirstIndexFirstPage
        {
            get
            {
                return _DateFormatStringForFirstIndexFirstPage;
            }
            set
            {
                _DateFormatStringForFirstIndexFirstPage = value;
            }
        }

        private string _DateFormatStringForFirstIndexOtherPage = "dd";
        /// <summary>
        /// 输出非首页首个日期数据使用的格式化字符串
        /// </summary>
        [DefaultValue("dd")]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "DateFormatStringForFirstIndexOtherPage")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DateFormatStringForFirstIndexOtherPage
        {
            get
            {
                return _DateFormatStringForFirstIndexOtherPage;
            }
            set
            {
                _DateFormatStringForFirstIndexOtherPage = value;
            }
        }

        private string _Title = null;
        /// <summary>
        /// 标题
        /// </summary>
        [DefaultValue(null)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "Title")]
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

        private float _SpecifyTitleHeight = 0f;
        /// <summary>
        /// 指定的标题高度
        /// </summary>
        [DefaultValue( 0f )]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "SpecifyTitleHeight")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SpecifyTitleHeight
        {
            get
            {
                return _SpecifyTitleHeight; 
            }
            set
            {
                _SpecifyTitleHeight = value; 
            }
        }

        private HeaderLabelInfoList _HeaderLabels = new HeaderLabelInfoList();
        /// <summary>
        /// 页面标题
        /// </summary>
        [XmlArrayItem("Label", typeof(HeaderLabelInfo))]
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public HeaderLabelInfoList HeaderLabels
        {
            get
            {
                if (_HeaderLabels == null)
                {
                    _HeaderLabels = new HeaderLabelInfoList();
                }
                return _HeaderLabels;
            }
            set
            {
                _HeaderLabels = value;
            }
        }
          
         
        private int _NumOfDaysInOnePage = 7;
        /// <summary>
        /// 每页显示的天数
        /// </summary>
        [DefaultValue(7)]
        [DCDisplayName(typeof(TemperatureDocumentConfig), "NumOfDaysInOnePage")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int NumOfDaysInOnePage
        {
            get
            {
                return _NumOfDaysInOnePage;
            }
            set
            {
                _NumOfDaysInOnePage = value;
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 获得文档中所有的文本行列表
        /// </summary>
        /// <returns></returns>
        internal TitleLineInfoList GetAllTitleLines()
        {
            TitleLineInfoList lines = new TitleLineInfoList();
            lines.AddRange(this.HeaderLines);
            lines.AddRange(this.FooterLines);
            return lines;
        }
#endif
        private TitleLineInfoList _HeaderLines = new TitleLineInfoList();
        /// <summary>
        /// 标题行信息
        /// </summary>
        [XmlArrayItem("Line", typeof(TitleLineInfo))]
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineInfoList HeaderLines
        {
            get
            {
                return _HeaderLines;
            }
            set
            {
                _HeaderLines = value;
            }
        }

        private TitleLineInfoList _FooterLines = new TitleLineInfoList();
        /// <summary>
        /// 页脚行信息
        /// </summary>
        [XmlArrayItem("Line", typeof(TitleLineInfo))]
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineInfoList FooterLines
        {
            get
            {
                return _FooterLines;
            }
            set
            {
                _FooterLines = value;
            }
        }


        private YAxisInfoList _YAxisInfos = new YAxisInfoList();
        /// <summary>
        /// Y坐标轴信息列表
        /// </summary>
        [XmlArrayItem("YAxis", typeof(YAxisInfo))]
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public YAxisInfoList YAxisInfos
        {
            get
            {
                if (_YAxisInfos == null)
                {
                    _YAxisInfos = new YAxisInfoList();
                }
                return _YAxisInfos;
            }
            set
            {
                _YAxisInfos = value;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocumentConfig Clone()
        {
            TemperatureDocumentConfig cfg = (TemperatureDocumentConfig)this.MemberwiseClone();
            if (this._Images != null)
            {
                cfg._Images = new DCTimeLineImageList();
                foreach (DCTimeLineImage img in this._Images)
                {
                    cfg._Images.Add(img.Clone());
                }
            }
            if (this._FooterLines != null)
            {
                cfg._FooterLines = new TitleLineInfoList();
                foreach (TitleLineInfo info in this._FooterLines)
                {
                    cfg._FooterLines.Add(info.Clone());
                }
            }
            if (this._HeaderLines != null)
            {
                cfg._HeaderLines = new TitleLineInfoList();
                foreach (TitleLineInfo info in this._HeaderLines)
                {
                    cfg._HeaderLines.Add(info.Clone());
                }
            }
            if (this._YAxisInfos != null)
            {
                cfg._YAxisInfos = new YAxisInfoList();
                foreach (YAxisInfo info in this._YAxisInfos)
                {
                    cfg._YAxisInfos.Add(info);
                }
            }
            if (this._Ticks != null)
            {
                cfg._Ticks = this._Ticks.Clone();
            }
            if (this._Zones != null)
            {
                cfg._Zones = new TimeLineZoneInfoList();
                foreach (TimeLineZoneInfo item in this._Zones)
                {
                    cfg._Zones.Add(item.Clone());
                }
            }
            //doc._Config = this._Config.cl
            if (this.HeaderLabels != null)
            {
                cfg._HeaderLabels = this._HeaderLabels.Clone();
            }
            if (this._Labels != null)
            {
                cfg._Labels = this._Labels.Clone();
            }
            return cfg;
        }
#if !DCWriterForWASM

        internal void WriteJson(DCFastJsonTextWriter writer)
        {
            writer.WritePropertyNoFixName("SpecifyStartDate", this.SpecifyStartDate);
            writer.WritePropertyNoFixName("SpecifyEndDate", this.SpecifyEndDate);
            writer.WritePropertyNoFixName("GridYSplitNum", this.GridYSplitInfo.GridYSplitNum.ToString());
            writer.WritePropertyNoFixName("GridYSpaceNum", this.GridYSplitInfo.GridYSpaceNum.ToString());
            writer.WritePropertyNoFixName("Title", this.Title);
            writer.WritePropertyNoFixName("SpecifyTitleHeight", this.SpecifyTitleHeight.ToString());
            writer.WritePropertyNoFixName("NumOfDaysInOnePage", this.NumOfDaysInOnePage.ToString());

            writer.WritePropertyNoFixName("BigVerticalGridLineColorValue", this.BigVerticalGridLineColorValue);
            writer.WritePropertyNoFixName("GridLineColorValue", this.GridLineColorValue);
            writer.WritePropertyNoFixName("FontName", this.FontName);
            writer.WritePropertyNoFixName("FontSize", this.FontSize.ToString());
            writer.WritePropertyNoFixName("DateFormatString", this.DateFormatString);
            writer.WritePropertyNoFixName("DateFormatStringForCrossMonth", this.DateFormatStringForCrossMonth);
            writer.WritePropertyNoFixName("DateFormatStringForCrossWeek", this.DateFormatStringForCrossWeek);
            writer.WritePropertyNoFixName("DateFormatStringForCrossYear", this.DateFormatStringForCrossYear);
            writer.WritePropertyNoFixName("DateFormatStringForFirstIndexFirstPage", this.DateFormatStringForFirstIndexFirstPage);
            writer.WritePropertyNoFixName("DateFormatStringForFirstIndexOtherPage", this.DateFormatStringForFirstIndexOtherPage);
            writer.WritePropertyNoFixName("PageIndexText", this.PageIndexText);

            writer.WritePropertyNoFixNameBoolean("EnableDataGridLinearAxisMode", this.EnableDataGridLinearAxisMode);

            writer.WritePropertyNoFixName("DataGridBottomPadding", this.DataGridBottomPadding.ToString());
            writer.WritePropertyNoFixName("DataGridTopPadding", this.DataGridTopPadding.ToString());

            writer.WritePropertyNoFixName("BigTitleFontSize", this.BigTitleFontSize.ToString());
            writer.WritePropertyNoFixName("FooterDescription", this.FooterDescription);

            writer.WritePropertyNoFixName("ThickLineWidth", this.GridYSplitInfo.ThickLineWidth.ToString());
            writer.WritePropertyNoFixName("ThinLineWidth", this.GridYSplitInfo.ThinLineWidth.ToString());

            writer.WritePropertyNoFixName("IllegalTextEndCharForLinux", this.IllegalTextEndCharForLinux);

            string ticktexts = "";
            string tickvalues = "";
            string tickcolorvalues = "";
            for (int i = 0; i < this.Ticks.Count; i++)
            {
                ticktexts += this.Ticks[i].Text;
                tickvalues += this.Ticks[i].Value;
                tickcolorvalues += this.Ticks[i].ColorValue;
                if (i != this.Ticks.Count -1)
                {
                    ticktexts += ",";
                    tickvalues += ",";
                    tickcolorvalues += ",";
                }
            }
            writer.WritePropertyNoFixName("TickTexts", ticktexts);
            writer.WritePropertyNoFixName("TickValues", tickvalues);
            writer.WritePropertyNoFixName("TickColorValues", tickcolorvalues);

            writer.WriteStartProperty("PageSettings");
            if (this.PageSettings != null)
            {
                writer.WriteStartObject();
                this.PageSettings.WriteJson(writer);
                writer.WriteEndObject();
            }
            writer.WriteEndProperty();

            writer.WriteStartProperty("HeaderLabels");
            writer.WriteStartArray();
            if (this.HeaderLabels != null && this.HeaderLabels.Count > 0)
            {
                foreach (HeaderLabelInfo header in this.HeaderLabels)
                {
                    writer.WriteStartObject();
                    header.WriteJson(writer);
                    writer.WriteEndObject();
                }
            }
            writer.WriteEndArray();
            writer.WriteEndProperty();

            writer.WriteStartProperty("HeaderLines");
            writer.WriteStartArray();
            if (this.HeaderLines != null && this.HeaderLines.Count > 0)
            {
                foreach (TitleLineInfo info in this.HeaderLines)
                {
                    writer.WriteStartObject();
                    info.WriteJson(writer);
                    writer.WriteEndObject();
                }
            }
            writer.WriteEndArray();
            writer.WriteEndProperty();

            writer.WriteStartProperty("Labels");
            writer.WriteStartArray();
            if (this.Labels != null && this.Labels.Count > 0)
            {
                foreach (DCTimeLineLabel label in this.Labels)
                {
                    writer.WriteStartObject();
                    label.WriteJson(writer);
                    writer.WriteEndObject();
                }
            }
            writer.WriteEndArray();
            writer.WriteEndProperty();
            ////////////////////////////

            writer.WriteStartProperty("FooterLines");
            writer.WriteStartArray();
            if (this.FooterLines != null && this.FooterLines.Count > 0)
            {
                foreach (TitleLineInfo info in this.FooterLines)
                {
                    writer.WriteStartObject();
                    info.WriteJson(writer);
                    writer.WriteEndObject();
                }
            }
            writer.WriteEndArray();
            writer.WriteEndProperty();

            writer.WriteStartProperty("YAxisInfos");
            writer.WriteStartArray();
            if (this.YAxisInfos != null && this.YAxisInfos.Count > 0)
            {
                foreach (YAxisInfo info in this.YAxisInfos)
                {
                    writer.WriteStartObject();
                    info.WriteJson(writer);
                    writer.WriteEndObject();
                }
            }
            writer.WriteEndArray();
            writer.WriteEndProperty();
        }
#endif
    }
}
