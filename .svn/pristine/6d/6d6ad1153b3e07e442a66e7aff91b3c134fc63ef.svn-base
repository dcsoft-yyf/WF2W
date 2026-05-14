using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Xml.Serialization ;
using DCSoft.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using DCSoft.Drawing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间轴区域信息对象
    /// </summary>

#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(false)]
    //[DCSoft.Common.DCDescriptionResourceSource(typeof(DCSoft.TemperatureChart.DCTimeLineDescriptionResource))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
    [Serializable]
#endif
    public partial class TimeLineZoneInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TimeLineZoneInfo()
        {

        }
#if ! DCWriterForWASM
        [NonSerialized]
        private TimeLineZoneInfo _ParentZone = null;
        /// <summary>
        /// 父区域
        /// </summary>
        internal TimeLineZoneInfo ParentZone
        {
            get 
            {
                return _ParentZone; 
            }
            set
            {
                _ParentZone = value; 
            }
        }
        [NonSerialized]
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
        [Browsable( false )]
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

        private int _ZoneIndex = 0;
        /// <summary>
        /// 区域序号
        /// </summary>
        internal int ZoneIndex
        {
            get
            {
                return _ZoneIndex; 
            }
            set
            {
                _ZoneIndex = value; 
            }
        }
        [NonSerialized]
        private RuntimeTickInfo _FirstTickItem = null;
        /// <summary>
        /// 第一个时刻信息对象
        /// </summary>
        internal RuntimeTickInfo FirstTickItem
        {
            get
            {
                return _FirstTickItem; 
            }
            set
            {
                _FirstTickItem = value; 
            }
        }
        [NonSerialized]
        private RuntimeTickInfo _LastTickItem = null;
        /// <summary>
        /// 最后一个时刻信息对象
        /// </summary>
        internal RuntimeTickInfo LastTickItem
        {
            get
            {
                return _LastTickItem; 
            }
            set
            {
                _LastTickItem = value; 
            }
        }
        [NonSerialized]
        private float _Left = 0;
        /// <summary>
        /// 左端位置
        /// </summary>
        internal float Left
        {
            get { return _Left; }
            set { _Left = value; }
        }
        [NonSerialized]
        private float _Top = 0;
        /// <summary>
        /// 顶端位置
        /// </summary>
        internal float Top
        {
            get { return _Top; }
            set { _Top = value; }
        }
        [NonSerialized]
        private float _Width = 0;
        /// <summary>
        /// 宽度
        /// </summary>
        internal float Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        [NonSerialized]
        private float _Height = 0;
        /// <summary>
        /// 高度
        /// </summary>
        internal float Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
#endif
        //private RectangleF _Bounds = RectangleF.Empty;
        ///// <summary>
        ///// 对象在文档视图中的边界
        ///// </summary>
        //internal RectangleF Bounds
        //{
        //    get
        //    {
        //        return _Bounds;
        //    }
        //    set
        //    {
        //        _Bounds = value;
        //    }
        //}

        private string _Name = null;
        /// <summary>
        /// 区域名称
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        [Browsable(true)]
        [DCDisplayName(typeof(TimeLineZoneInfo), "Name")]
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

        private DateTime _StartTime = TemperatureDocument.NullDate;
        /// <summary>
        /// 区域开始时间
        /// </summary>
        [XmlAttribute]
        [DefaultValue( typeof( DateTime ) , TemperatureDocument.InnerNullDateString) ]
        [Browsable(true)]
        [DCDisplayName(typeof(TimeLineZoneInfo), "StartTime")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DateTime StartTime
        {
            get
            {
                return _StartTime; 
            }
            set
            {
                _StartTime = value; 
            }
        }

        private DateTime _EndTime = TemperatureDocument.NullDate;
        /// <summary>
        /// 区域结束时间
        /// </summary>
        [XmlAttribute]
        [DefaultValue(typeof(DateTime), TemperatureDocument.InnerNullDateString)]
        [Browsable(true)]
        [DCDisplayName(typeof(TimeLineZoneInfo), "EndTime")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
            }
        }

        private bool _AlignToGrid = true;
        /// <summary>
        /// 是否对齐到网格线
        /// </summary>
        [DefaultValue(true)]
        [XmlAttribute]
        [Browsable(true)]
        [DCDisplayName(typeof(TimeLineZoneInfo), "AlignToGrid")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool AlignToGrid
        {
            get
            {
                return _AlignToGrid; 
            }
            set
            {
                _AlignToGrid = value; 
            }
        }

        private DashStyle _GridLineStyle = DashStyle.Solid;
        /// <summary>
        /// 网格线样式
        /// </summary>
        [DefaultValue( DashStyle.Solid )]
        [XmlAttribute]
        [Browsable(true)]
        [DCDisplayName(typeof(TimeLineZoneInfo), "GridLineStyle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DashStyle GridLineStyle
        {
            get
            {
                return _GridLineStyle; 
            }
            set
            {
                _GridLineStyle = value; 
            }
        }


        private Color _GridLineColor = Color.Transparent;
        /// <summary>
        /// 网格线颜色值
        /// </summary>
        [XmlIgnore]
        [Browsable(true)]
        [DefaultColorValue("Transparent")]
        [DCDisplayName(typeof(TimeLineZoneInfo), "GridLineColor")]
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
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string GridLineColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.GridLineColor, Color.Transparent);
            }
            set
            {
                this.GridLineColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private Color _BackColor = Color.Transparent;
        /// <summary>
        /// 颜色值
        /// </summary>
        [XmlIgnore]
        [Browsable( true )]
        [DefaultColorValue("Transparent")]
        [DCDisplayName(typeof(TimeLineZoneInfo), "BackColor")]
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
        /// 文本形式的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
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

        private float _SpecifyTickWidth = 0f;
        /// <summary>
        /// 指定的最小刻度长度，小于等于0则自动计算，默认为0.
        /// </summary>
        [DefaultValue(0f)]
        [DCDisplayName(typeof(TimeLineZoneInfo), "SpecifyTickWidth")]
        [XmlAttribute]
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

        private TickInfoList _Ticks = null;
        /// <summary>
        /// 时间刻度
        /// </summary>
        [XmlArrayItem("Tick", typeof(TickInfo))]
        [Browsable(true)]
        [DCDisplayName(typeof(TimeLineZoneInfo), "Ticks")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TickInfoList Ticks
        {
            get
            {
                //if (_Ticks == null)
                //{
                //    _Ticks = new TickInfoList();
                //}
                return _Ticks; 
            }
            set
            {
                _Ticks = value; 
            }
        }

        private int _AutoTickStepSeconds = 0;
        /// <summary>
        /// 自动生成刻度使用的时间步长秒数，大于0有效
        /// </summary>
        [DefaultValue( 0 )]
        [DCDisplayName(typeof(TimeLineZoneInfo), "AutoTickStepSeconds")]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int AutoTickStepSeconds
        {
            get
            {
                return _AutoTickStepSeconds; 
            }
            set
            {
                _AutoTickStepSeconds = value; 
            }
        }

        private string _AutoTickFormatString = null;
        /// <summary>
        /// 自动生成刻度时使用的格式化字符串
        /// </summary>
        [DefaultValue( null )]
        [DCDisplayName(typeof(TimeLineZoneInfo), "AutoTickFormatString")]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string AutoTickFormatString
        {
            get
            {
                return _AutoTickFormatString; 
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this._AutoTickFormatString = null;
                }
                else
                {
                    this._AutoTickFormatString = value;
                }
            }
        }
#if !DCWriterForWASM
        /// <summary>
        /// 判断是否在时间区域中
        /// </summary>
        /// <param name="dtm">时间值</param>
        /// <returns>是否在区域中</returns>
        internal bool Contains(DateTime dtm)
        {
            if (TemperatureDocument.IsNullDate(this.StartTime) == false)
            {
                if (dtm < this.StartTime)
                {
                    return false;
                }
            }
            if (TemperatureDocument.IsNullDate(this.EndTime) == false)
            {
                if (dtm > this.EndTime)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns></returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override string ToString()
        {
            string txt = this.Name + "|" +  this.StartTime.ToString("yyyy-MM-dd") + "->" + this.EndTime.ToString("yyyy-MM-dd");
            if (this._Ticks != null)
            {
                txt = txt + ":" + this._Ticks.ToString();
            }
            return txt;
        }
#endif

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TimeLineZoneInfo Clone()
        {
            TimeLineZoneInfo info = (TimeLineZoneInfo)this.MemberwiseClone();
            if (this._Ticks != null)
            {
                info._Ticks = this._Ticks.Clone();
            }
            return info;
        }
    }

    /// <summary>
    /// 时间轴区域信息列表
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [System.Runtime.InteropServices.ComVisible( false )]
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
#endif
    public class TimeLineZoneInfoList : List<TimeLineZoneInfo>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TimeLineZoneInfoList()
        {
        }
#if !DCWriterForWASM
        /// <summary>
        /// 获得指定名称的区域对象，名称区分大小写
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>获得的对象</returns>
        public TimeLineZoneInfo GetByName(string name)
        {
            foreach (TimeLineZoneInfo item in this)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// 刷新状态
        /// </summary>
        public void RefreshState()
        {
            // 按照StartTime进行排序
            this.Sort(new ZoneComparer());
            foreach (TimeLineZoneInfo zone in this)
            {
                zone.ParentZone = null;
            }
            // 设置序号
            for( int iCount = 0 ; iCount < this.Count  ; iCount ++ )
            {
                TimeLineZoneInfo zone = this[iCount];
                // 设置序号
                zone.ZoneIndex = iCount;
                // 设置父区域
                for (int iCount2 = iCount + 1; iCount2 < this.Count; iCount2++)
                {
                    TimeLineZoneInfo zone2 = this[iCount2];
                    if (zone2.StartTime < zone.EndTime)
                    {
                        zone2.ParentZone = zone;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 获得指定时间所在的时间区域对象
        /// </summary>
        /// <param name="dtm">时间</param>
        /// <returns>获得的区域对象</returns>
        public TimeLineZoneInfo GetZone(DateTime dtm)
        {
            // 反向遍历
            for (int iCount = this.Count - 1; iCount >= 0; iCount--)
            {
                TimeLineZoneInfo item = this[iCount];
                if (TemperatureDocument.IsNullDate(item.StartTime) == false && dtm >= item.StartTime )
                {
                    if (TemperatureDocument.IsNullDate(item.EndTime) == false)
                    {
                        if (dtm >= item.EndTime)
                        {
                            continue;
                        }
                    }
                    return item;
                }
            }
            return null;
        }
        private class ZoneComparer : IComparer<TimeLineZoneInfo >
        {
            public int Compare(TimeLineZoneInfo x, TimeLineZoneInfo y)
            {
                return x.StartTime.CompareTo(y.StartTime);
            }
        }
#endif
    }
}
