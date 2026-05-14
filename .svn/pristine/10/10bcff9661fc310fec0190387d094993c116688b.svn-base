using System;
using System.Collections.Generic;
using System.Text;
//using System.Data;
using System.Collections;
using System.Xml.Serialization;
using System.ComponentModel;
using DCSoft.Data;
using System.Drawing;
using DCSoft.Common;
using System.Runtime.InteropServices ;
using DCSoft.Drawing;

namespace DCSoft.TemperatureChart
{
    // 体温单的数据处理
    public partial class TemperatureDocument
    {
#if ! DCWriterForWASM
        /// <summary>
        /// 清除数据
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ClearData()
        {
            this.Datas.Clear();
        }

        private DateTime _CaretDateTime = TemperatureDocument.NullDate;
        /// <summary>
        /// 光标所在的时间值
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [System.Xml.Serialization.XmlIgnore]
        public DateTime CaretDateTime
        {
            get
            {
                return _CaretDateTime;
            }
            set
            {
                _CaretDateTime = value;
            }
        }
#endif
        private DCTimeLineParameterList _Parameters = new DCTimeLineParameterList();
        /// <summary>
        /// 文档参数值
        /// </summary>
        [XmlArrayItem("Parameter" , typeof( DCTimeLineParameter ))]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineParameterList Parameters
        {
            get
            {
                if (_Parameters == null)
                {
                    _Parameters = new DCTimeLineParameterList();
                }
                return _Parameters; 
            }
            set
            {
                _Parameters = value; 
            }
        }

        private DocumentDataList _Datas = new DocumentDataList();
        /// <summary>
        /// 数值
        /// </summary>
        [DefaultValue( null )]
        [XmlArrayItem("Data" , typeof( DocumentData ))]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DocumentDataList Datas
        {
            get
            {
                if (_Datas == null)
                {
                    _Datas = new DocumentDataList();
                }
                return _Datas; 
            }
            set
            {
                _Datas = value; 
            }
        }
#if !DCWriterForWASM
        /// <summary>
        /// 声明某些状态无效
        /// </summary>
        public void InvalidateState()
        {
            foreach (DocumentData item in this.Datas)
            {
                item.Values.SortInvalidate = true;
            }
        }
        /// <summary>
        /// 根据名称获得数据项目
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>获得的数据列表</returns>
        internal ValuePointList GetValuePointsByName(string name)
        {
            return this.Datas.GetValuesByName(name);
        }

        /// <summary>
        /// 每页显示的天数
        /// </summary>
        [DefaultValue(7)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int NumOfDaysInOnePage
        {
            get
            {
                return this.Config.NumOfDaysInOnePage;
            }
            set
            {
                this.Config.NumOfDaysInOnePage = value;
            }
        }

        private int RuntimeNumOfDaysInOnePage
        {
            get
            {
                if (this.RuntimeViewMode == DocumentViewMode.Timeline)
                {
                    if (this.Days <= 0)
                    {
                        return this.NumOfDaysInOnePage;
                    }
                    else
                    {
                        return this.Days;
                    }
                }
                else
                {
                    return this.NumOfDaysInOnePage;
                }
            }
        }
#endif
        /// <summary>
        /// 标题行信息
        /// </summary>
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineInfoList HeaderLines
        {
            get
            {
                return this.Config.HeaderLines;
            }
            set
            {
                this.Config.HeaderLines = value;
            }
        }
#if !DCWriterForWASM

        [NonSerialized]
        private TitleLineInfoList _RuntimeHeaderLines = null;
        /// <summary>
        /// 实际参与绘图的标题行列表
        /// </summary>
        internal TitleLineInfoList RuntimeHeaderLines
        {
            get
            {
                if (this.InnerBehaviorMode == DocumentBehaviorMode.DesignMode)
                {
                    // 设计模式直接返回所有的标题行
                    return this.Config.HeaderLines;
                }
                else
                {
                    return this.Config.HeaderLines.GetRuntimeInfos();
                }
                //if (_RuntimeHeaderLines == null)
                //{
                //    _RuntimeHeaderLines = this.Config.HeaderLines.GetRuntimeInfos();

                //}
                //return _RuntimeHeaderLines; 
            }
            set
            {
                _RuntimeHeaderLines = value;
            }
        }
#endif

        /// <summary>
        /// 页脚行信息
        /// </summary>
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineInfoList FooterLines
        {
            get
            {
                return this.Config.FooterLines;
            }
            set
            {
                this.Config.FooterLines = value;
            }
        }
#if !DCWriterForWASM

        [NonSerialized]
        private TitleLineInfoList _RuntimeFooterLines = null;
        /// <summary>
        /// 实际参与绘图的标题行列表
        /// </summary>
        internal TitleLineInfoList RuntimeFooterLines
        {
            get
            {
                if (this.InnerBehaviorMode == DocumentBehaviorMode.DesignMode)
                {
                    // 设计模式直接返回所有的标题行
                    return this.Config.FooterLines;
                }
                else
                {
                    return this.Config.FooterLines.GetRuntimeInfos();
                }
                //if (_RuntimeFooterLines == null)
                //{
                //    _RuntimeFooterLines = this.Config.FooterLines.GetRuntimeInfos();

                //}
                //return _RuntimeFooterLines;
            }
            set
            {
                _RuntimeFooterLines = value;
            }
        }
#endif

        /// <summary>
        /// Y坐标轴信息列表
        /// </summary>
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public YAxisInfoList YAxisInfos
        {
            get
            {
                return this.Config.YAxisInfos;
            }
            set
            {
                this.Config.YAxisInfos = value;
            }
        }

#if !DCWriterForWASM
        /// <summary>
        /// 获得指定名称的行对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>获得的对象</returns>
        public TitleLineInfo GetTitleLineInfoByName(string name)
        {
            TitleLineInfo info = this.HeaderLines.GetItemByName(name);
            if (info == null)
            {
                info = this.FooterLines.GetItemByName(name);
            }
            return info;
        }

        /// <summary>
        /// 根据序号设置页眉标题文本
        /// </summary>
        /// <param name="index">从0开始计算的序号</param>
        /// <param name="text">文本</param>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetHeaderLableValueByIndex(int index, string text)
        {
            if(index >= 0 && index < this.HeaderLabels.Count)
            {
                this.HeaderLabels[index].Value = text;
            }
        }

        /// <summary>
        /// 设置页眉标题文本
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        [System.Obsolete("请使用SetParameterValue(name,value)")]
        //[System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetHeaderLableValue(string title, string text)
        {
            foreach (HeaderLabelInfo lbl in this.HeaderLabels)
            {
                if (lbl.Title == title)
                {
                    lbl.Value = text;
                    break;
                }
            }
        }

        /// <summary>
        /// 设置文档参数值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="Value">参数值</param>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetParameterValue(string name, string Value)
        {
            this.Parameters.SetValue(name, Value);
        }

        /// <summary>
        /// 获得文档参数值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>参数值</returns>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string GetParameterValue(string name)
        {
            return this.Parameters.GetValue(name);
        }
         
        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="Value">数值</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeValue(string name, DateTime dtm, float Value)
        {
            ValuePointList ps = GetValuePointsByName(name);
            if (ps != null)
            {
                lock (this)
                {
                    ps.AddByTimeValue(dtm, Value);
                }
            }
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列的名称</param>
        /// <param name="point">数据点对象</param>
        /// <param name="covermode">覆盖模式设为true则当插入同时间点的数据点时，删除之前已存在的同时间点的数据点</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPoint(string name, ValuePoint point, bool covermode = false)
        {
            if (point == null)
            {
                throw new ArgumentNullException("point");
            }
            ValuePointList ps = GetValuePointsByName(name);
            if (ps != null)
            {
                lock (this)
                {
                    
                    if (covermode == true)
                    {
                        ValuePoint vpForDelete = null;
                        for (var i = ps.Count - 1; i >= 0; i--)
                        {
                            ValuePoint vpp = ps[i];
                            if(vpp.Time == point.Time)
                            {
                                vpForDelete = vpp;
                                break;
                            }
                        }
                        if(vpForDelete != null)
                        {
                            ps.Remove(vpForDelete);
                        }
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////
                    ps.Add(point);
                    ps.SortInvalidate = true;
                }
            }
            
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="text">数值</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeText(string name, DateTime dtm, string text)
        {
            ValuePointList ps = GetValuePointsByName(name);
            if (ps != null)
            {
                lock (this)
                {
                    ps.AddByTimeText(dtm, text);
                }
            }
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="text">文本</param>
        /// <param name="Value">数值</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeTextValue(string name, DateTime dtm, string text, float Value)
        {
            ValuePointList ps = GetValuePointsByName(name);
            if (ps != null)
            {
                lock (this)
                {
                    ps.AddByTimeTextValue(dtm, text, Value);
                }
            }
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="text">文本</param>
        /// <param name="htmlColorValue">HTML格式的颜色值</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeTextColor(string name, DateTime dtm, string text, string htmlColorValue)
        {
            ValuePointList ps = GetValuePointsByName(name);
            if (ps != null)
            {
                ValuePoint vp = new ValuePoint();
                vp.Time = dtm;
                vp.Text = text;
                vp.ColorValue = htmlColorValue;
                lock (this)
                {
                    ps.Add(vp);
                }
            }
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="name">数据序列名称</param>
        /// <param name="dtm">数据时间</param>
        /// <param name="Value">数值</param>
        /// <param name="landernValue">灯笼数值</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddPointByTimeValueLandernValue(
            string name, 
            DateTime dtm, 
            float Value, 
            float landernValue)
        {
            ValuePointList ps = GetValuePointsByName(name);
            if (ps != null)
            {
                lock (this)
                {
                    ps.AddByTimeValueLandernValue(dtm, Value, landernValue);
                }
            }
        }

        /// <summary>
        /// 设置计算天数使用的开始日期
        /// </summary>
        /// <param name="name">数据行的名称</param>
        /// <param name="startDate">开始日期</param>
        /// <returns>操作是否成功</returns>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool SetStartDateForDayIndex(string name, DateTime startDate)
        {
            TitleLineInfo info = this.GetTitleLineInfoByName(name);
            if (info != null)
            {
                info.StartDate = startDate;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改指定时间区域的范围
        /// </summary>
        /// <param name="zoneName">时间区域名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>操作是否修改了数据</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool SetTimeLineZoneRange(string zoneName, DateTime startTime, DateTime endTime)
        {
            TimeLineZoneInfo zone = this.Config.Zones.GetByName(zoneName);
            if (zone != null)
            {
                zone.StartTime = startTime;
                zone.EndTime = endTime;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 设置指定时间区域中的数据点颜色
        /// </summary>
        /// <param name="zoneName">时间区域名称</param>
        /// <param name="valueName">数据序列名称</param>
        /// <param name="colorValue">颜色值，比如"#ff00ff"</param>
        /// <returns>操作修改的数据点个数</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int SetSymbolStyleByTimeZone(
            string zoneName,
            string valueName,
            string colorValue )
        {
            int result = 0;
            TimeLineZoneInfo zone = this.Config.Zones.GetByName(zoneName);
            if (zone != null)
            {
                ValuePointList vps = GetValuePointsByName(valueName);
                if (vps != null && vps.Count > 0)
                {
                    foreach (ValuePoint vp in vps)
                    {
                        if (zone.Contains(vp.Time))
                        {
                            vp.ColorValue = colorValue;
                            result++;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 设置指定时间区域中的数据点样式
        /// </summary>
        /// <param name="zoneName">时间区域名称</param>
        /// <param name="valueName">数据序列名称</param>
        /// <param name="style">新的数据点图标样式</param>
        /// <returns>操作修改的数据点个数</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int SetSymbolStyleByTimeZone(
            string zoneName, 
            string valueName, 
            ValuePointSymbolStyle style)
        {
            int result = 0;
            TimeLineZoneInfo zone = this.Config.Zones.GetByName(zoneName);
            if (zone != null)
            {
                ValuePointList vps = GetValuePointsByName(valueName);
                if (vps != null && vps.Count > 0)
                {
                    foreach (ValuePoint vp in vps)
                    {
                        if (zone.Contains(vp.Time))
                        {
                            vp.SpecifySymbolStyle = style;
                            result++;
                        }
                    }
                }
            }
            return result;
        }

        


        [NonSerialized]
        private System.Collections.Hashtable _DataSources = new System.Collections.Hashtable();
        /// <summary>
        /// 对象绑定的数据源对象列表
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Collections.Hashtable DataSources
        {
            get
            {
                if (_DataSources == null)
                {
                    _DataSources = new Hashtable();
                }
                return _DataSources;
            }
        }
        /// <summary>
        /// 更新文档总页数
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [ComVisible(true)]
        public void UpdateNumOfPage()
        {
            DateTime dtm1 = DateTime.MinValue;
            DateTime dtm2 = DateTime.MinValue;
            UpdateNumOfPage(out dtm1, out dtm2);
        }


        [NonSerialized]
        private int _Days = 0;
        /// <summary>
        /// 时间跨度天数
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Days
        {
            get
            {
                return _Days; 
            }
        }

        /// <summary>
        /// 运行时使用的时间刻度列表
        /// </summary>
        [NonSerialized]
        private RuntimeTickInfoList _RuntimeTicks = null;
        /// <summary>
        /// 运行时使用的时间刻度列表
        /// </summary>
        internal RuntimeTickInfoList RuntimeTicks
        {
            get
            {
                return _RuntimeTicks; 
            }
        }

        
        /// <summary>
        /// 运行时记录数据网格区中网格线位置的内部结构
        /// </summary>
        [NonSerialized]
        private RuntimeTickInfoList _RuntimeTicksForVerticalDataGrid = null;
        /// <summary>
        /// 运行时使用的时间刻度列表
        /// </summary>
        internal RuntimeTickInfoList RuntimeTicksForVerticalDataGrid
        {
            get
            {
                return _RuntimeTicksForVerticalDataGrid;
            }
        }


        /// <summary>
        /// 内部使用的默认时间均匀分配的刻度列表
        /// </summary>
        [NonSerialized]
        private RuntimeTickInfoList _DefaultRuntimeTicks = null;
        /// <summary>
        /// 运行时使用的时间刻度列表
        /// </summary>
        internal RuntimeTickInfoList DefaultRuntimeTicks
        {
            get
            {
                return _DefaultRuntimeTicks;
            }
        }

        /// <summary>
        /// 构造一个均匀分配时间段的运行时时刻列表
        /// </summary>
        /// <param name="count">时间段数</param>
        /// <returns></returns>
        private TickInfoList BuildEvenlyRuntimeTicks(int count)
        {
            TickInfoList defaultTicks = new TickInfoList();
            int icount = count * 2;
            if(24 % count != 0)
            {
                count = 6;
            }
            float hourstep = 24f / (count * 2);
            for (float i = hourstep; i <= 24; i = i + hourstep * 2)
            {
                defaultTicks.AddItem(i, i.ToString(), Color.Black);
            }
            return defaultTicks;
        }



        /// <summary>
        /// 刷新运行时时刻列表
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="maxTickNum"></param>
        private void RefreshRuntimeTicks( 
            DateTime startTime , 
            DateTime endTime , 
            int maxTickNum ,
            bool needRefreshDefaultRuntimeTicks = false)
        {
            //除了初始化用户自定义的时刻以外还要初始化一个内置的默认属性的时刻用于页脚数据行的绘制
            TickInfoList defaultTicks = BuildEvenlyRuntimeTicks(this.Config.Ticks.Count);
            TickInfoList usingTicks = needRefreshDefaultRuntimeTicks == false ? this.Config.Ticks : defaultTicks;
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            //float tick = CountDown.GetTickCountFloat();
            if (this.Config.Zones != null)
            {
                this.Config.Zones.RefreshState();
            }
            RuntimeTickInfoList usingTickInfoList = new RuntimeTickInfoList();
            usingTickInfoList.StartTime = startTime;
            usingTickInfoList.EndTime = endTime;
            usingTickInfoList._StdTickUnit = this.Config.TickUnit;
            // 首先根据全局刻度设置来生成时间刻度列表
            this.Config.CheckDefaultTicks();
            //int tickIndex = this.Config.Ticks.GetHourTickIndex( startTime );
            usingTickInfoList.InsertRuntimeTickInfo( 0, startTime, endTime, usingTicks, null );
            if (this.Config.Zones != null && this.Config.Zones.Count > 0)
            {
                // 使用时间轴区域的设置来替换掉部分刻度标尺
                foreach (TimeLineZoneInfo zone in this.Config.Zones)
                {
                    //if (zone.Ticks == null || zone.Ticks.Count == 0)
                    //{
                    //    // 没有刻度，处理下一个区域
                    //    continue;
                    //}
                    bool hidden = false;
                    TimeLineZoneInfo zone2 = zone.ParentZone;
                    while (zone2 != null)
                    {
                        if (zone2.IsExpanded == false)
                        {
                            hidden = true;
                            break;
                        }
                        zone2 = zone2.ParentZone;
                    }
                    if (hidden)
                    {
                        // 由于父区域收缩而隐藏了。
                        continue;
                    }
                    DateTime zoneStartTime = startTime;
                    if (TemperatureDocument.IsNullDate(zone.StartTime) == false)
                    {
                        zoneStartTime = zone.StartTime;
                    }
                    int startIndex = -1;
                    for (int iCount = 0; iCount < usingTickInfoList.Count; iCount++)
                    {
                        if (usingTickInfoList[iCount].EndTime >= zoneStartTime)
                        {
                            startIndex = iCount;
                            break;
                        }
                    }
                    DateTime zoneEndTime = endTime;
                    if (TemperatureDocument.IsNullDate(zone.EndTime) == false)
                    {
                        zoneEndTime = zone.EndTime;
                    }
                    int endIndex = -1 ;
                    for (int iCount = startIndex ; iCount < usingTickInfoList.Count; iCount++)
                    {
                        //2016.3.2添加try catch 数组越界问题
                        try
                        {
                            RuntimeTickInfo item2 = usingTickInfoList[iCount];
                            if (item2.EndTime > zoneEndTime)
                            {
                                endIndex = iCount;
                                break;
                            }
                        }
                        catch
                        { }

                    }
                     
                    if (startIndex >= 0 || endIndex >= 0)
                    {
                        if( startIndex == -1 )
                        {
                            startIndex = 0;
                        }
                        if (endIndex == -1)
                        {
                            endIndex = usingTickInfoList.Count ;
                        }
                        //this._RuntimeTicks[startIndex].EndTime = zoneStartTime;
                        //this._RuntimeTicks[endIndex].StartTime = zoneEndTime;
                        // 删除部分刻度
                        if (endIndex - startIndex > 1)
                        {
                            usingTickInfoList.RemoveRange(startIndex + 1 , endIndex - startIndex - 1 );
                        }
                        // 插入区域刻度
                        usingTickInfoList.InsertRuntimeTickInfo(
                            startIndex + 1 ,
                            zoneStartTime,
                            zoneEndTime,
                            zone.Ticks,
                            zone);
                    }
                }//foreach
            }//if
            // 应用最大个数
            if (maxTickNum > 0 && maxTickNum < usingTickInfoList.Count)
            {
                usingTickInfoList.RemoveRange(maxTickNum, usingTickInfoList.Count - maxTickNum );
            }
            // 刷新序号
            for (int iCount = 0; iCount < usingTickInfoList.Count; iCount++)
            {
                usingTickInfoList[iCount].Index = iCount;
            }
            // 设置标识变量
            DateTime curDay = usingTickInfoList[0].StartTime.Date.AddDays( -1 );
            foreach (RuntimeTickInfo item in usingTickInfoList)
            {
                if (item.StartTime.Date > curDay)
                {
                    item.FirstTickInDate = true;
                    curDay = item.StartTime.Date;
                }
                else
                {
                    item.FirstTickInDate = false;
                }
            }

            //除了初始化用户自定义的时刻以外还要初始化一个内置的默认属性的时刻用于页脚数据行的绘制
            if (needRefreshDefaultRuntimeTicks == true)
            {
                this._DefaultRuntimeTicks = usingTickInfoList;
            }
            else
            {
                this._RuntimeTicks = usingTickInfoList;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //tick = CountDown.GetTickCountFloat() - tick;
        }

        /// <summary>
        /// 数值的最大日期
        /// </summary>
        [NonSerialized]
        private DateTime _DataMaxDate = NullDate;

        /// <summary>
        /// 更新文档的总页数
        /// </summary>
        /// <param name="maxDate">输出的数据最大日期</param>
        /// <param name="minDate">输出的数据最小日期</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void UpdateNumOfPage(out DateTime maxDate, out DateTime minDate)
        {
            // 有数据的时间区间的最大值
            maxDate = NullDate;
            // 有数据的时间区间的最小值
            minDate = NullDate;
            _DataMaxDate = NullDate;

            DateTime SpecifyStartDate = NullDate;
            DateTime SpecifyEndDate = NullDate;

            lock (this)
            {
                foreach (DocumentData data in this.Datas)
                {
                    ValuePointList vps = data.Values;
                    if (vps.Count > 0)
                    {
                        vps.CheckSortInvalidate();
                        if (maxDate == NullDate || maxDate < vps.MaxDate.Date)
                        {
                            maxDate = vps.MaxDate.Date;
                        }
                        if (minDate == NullDate || minDate > vps.MinDate.Date)
                        {
                            minDate = vps.MinDate.Date;
                        }
                        if (_DataMaxDate == NullDate || _DataMaxDate < vps.MaxDate)
                        {
                            // 获得数值最大时间
                            _DataMaxDate = vps.MaxDate;
                        }
                    }
                }//foreach
                if (this.RuntimeViewMode == DocumentViewMode.Timeline)
                {
                    maxDate = maxDate.AddDays(this.Config.ExtendDaysForTimeLine);
                }
            }
            if (string.IsNullOrEmpty(this.Config.SpecifyStartDate) == false)
            {
                // 指定了开始日期
                DateTime dtm2 = DateTime.MinValue;
                if (DateTime.TryParse(this.Config.SpecifyStartDate, out dtm2))
                {
                    SpecifyStartDate = dtm2;
                }
            }
            if (string.IsNullOrEmpty(this.Config.SpecifyEndDate) == false)
            {
                // 指定了结束日期
                DateTime dtm2 = DateTime.MinValue;
                if (DateTime.TryParse(this.Config.SpecifyEndDate, out dtm2))
                {
                    SpecifyEndDate = dtm2;
                }
            }

            //这里新增对指定开始结束日期的细化处理逻辑
            if (SpecifyStartDate != NullDate)
            {
                minDate = SpecifyStartDate;
            }
            if (SpecifyEndDate != NullDate && SpecifyEndDate > minDate)
            {
                maxDate = SpecifyEndDate;
            }
            if (minDate >= maxDate)
            {
                if (this.RuntimeViewMode == DocumentViewMode.Timeline)
                {
                    maxDate = minDate.AddDays(this.Config.ExtendDaysForTimeLine);
                }
                else
                {
                    maxDate = minDate.AddDays(this.RuntimeNumOfDaysInOnePage).AddMilliseconds(-1);
                }
            }
            //////////////////////////////////////////////////////////

            //累计各个时间区域的最大最小时间
            if (this.Config.Zones != null)
            {
                foreach (TimeLineZoneInfo item in this.Config.Zones)
                {
                    if (TemperatureDocument.IsNullDate(item.StartTime) == false)
                    {
                        if (minDate > item.StartTime)
                        {
                            minDate = item.StartTime;
                        }
                    }
                    if (TemperatureDocument.IsNullDate(item.EndTime) == false)
                    {
                        if (maxDate < item.EndTime)
                        {
                            maxDate = item.EndTime;
                        }
                    }
                }
            }
            if (maxDate != NullDate)
            {
                maxDate = maxDate.AddDays(1);
            }
            if( maxDate != NullDate )
            {
                TimeSpan valueSpan = maxDate - minDate;
                this._Days = valueSpan.Days;
                this._NumOfPages = (int)Math.Ceiling(valueSpan.Days
                    / (double)this.RuntimeNumOfDaysInOnePage);
                if (this._NumOfPages == 0)
                {
                    this._NumOfPages = 1;
                }
                if (this.PageIndex >= this.NumOfPages)
                {
                    this.PageIndex = this.NumOfPages - 1;
                }
                if (this.PageIndex < 0)
                {
                    this.PageIndex = 0;
                }
            }
            else
            {
                maxDate = DateTime.Today;
                minDate = maxDate.AddDays(-this.NumOfDaysInOnePage);
                this._NumOfPages = 1;
                this.PageIndex = 0;
            }
        }

        [NonSerialized]
        private Dictionary<string, object> _SQLParameters = new Dictionary<string, object>();
        /// <summary>
        /// SQL查询使用的参数列表
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Dictionary<string, object> SQLParameters
        {
            get
            {
                if (_SQLParameters == null)
                {
                    _SQLParameters = new Dictionary<string, object>();
                }
                return _SQLParameters;
            }
            set
            {
                _SQLParameters = value;
            }
        }
        /// <summary>
        /// 刷新文档数据
        /// </summary>
        /// <param name="conn"></param>
        public void RefreshDocumentData(System.Data.IDbConnection conn)
        {
            if (conn == null)
            {
                throw new ArgumentNullException("conn");
            }
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            int result = 0;
            using (System.Data.IDbCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Clear();
                if (this.SQLParameters.Count > 0)
                {
                    // 添加参数
                    foreach (string key in this.SQLParameters.Keys)
                    {
                        System.Data.IDbDataParameter p = cmd.CreateParameter();
                        p.ParameterName = key;
                        p.Value = this.SQLParameters[key];
                        cmd.Parameters.Add(p);
                    }
                }
                if (string.IsNullOrEmpty(this.Config.SQLTextForHeaderLabel) == false)
                {
                    // 读取标题信息
                    foreach (HeaderLabelInfo lbl in this.Config.HeaderLabels)
                    {
                        if (string.IsNullOrEmpty(lbl.ValueFieldName) == false)
                        {
                            lbl.Value = null;
                        }
                    }
                    cmd.CommandText = this.Config.SQLTextForHeaderLabel;
                    System.Data.IDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);

                    if (reader.Read())
                    {
                        foreach (HeaderLabelInfo lbl in this.Config.HeaderLabels)
                        {
                            if (string.IsNullOrEmpty(lbl.ValueFieldName) == false)
                            {
                                int fi = reader.GetOrdinal(lbl.ValueFieldName);
                                if (fi >= 0 && reader.IsDBNull(fi) == false)
                                {
                                    lbl.Value = Convert.ToString(reader.GetValue(fi));
                                    result++;
                                }
                            }
                        }//foreach
                    }
                    reader.Close();
                }
                Dictionary<ValuePointDataSourceInfo, ValuePointList> datas = new Dictionary<ValuePointDataSourceInfo, ValuePointList>();
                foreach (TitleLineInfo line in this.Config.HeaderLines)
                {
                    if (line.DataSource != null)
                    {
                        datas[line.DataSource] = this.Datas.GetValuesByName(line.Name);
                    }
                }
                foreach (TitleLineInfo line in this.Config.FooterLines)
                {
                    if (line.DataSource != null)
                    {
                        datas[line.DataSource] = this.Datas.GetValuesByName(line.Name);
                    }
                }
                foreach (YAxisInfo ya in this.Config.YAxisInfos)
                {
                    if (ya.DataSource != null)
                    {
                        datas[ya.DataSource] = this.Datas.GetValuesByName(ya.Name);
                    }
                }
                foreach (ValuePointDataSourceInfo ds in datas.Keys)
                {
                    if (string.IsNullOrEmpty(ds.SQLText) == false)
                    {
                        ValuePointList list = datas[ds];
                        list.Clear();
                        cmd.CommandText = ds.SQLText;
                        System.Data.IDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                        int v = ds.Fill(reader, list);
                        reader.Close();
                        result += v;
                    }
                }//foreach
            }//using
        }
        /// <summary>
        /// 刷新数据源绑定
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RefreshDataSource()
        {
            //this.ClearData();
            lock (this)
            {
                foreach (HeaderLabelInfo lbl in this.HeaderLabels)
                {
                    if (string.IsNullOrEmpty(lbl.DataSourceName) == false)
                    {
                        object ds = this.DataSources[lbl.DataSourceName];
                        if (ds != null)
                        {
                            if (string.IsNullOrEmpty(lbl.ValueFieldName))
                            {
                                lbl.Value = Convert.ToString(ds);
                            }
                            else
                            {
                                DCSingleDataSource ds2 = new DCSingleDataSource(ds);
                                lbl.Value = Convert.ToString(ds2.ReadValue(lbl.ValueFieldName));
                            }
                        }
                    }//if
                }//foreach
                foreach (YAxisInfo ya in this.YAxisInfos)
                {
                    if (string.IsNullOrEmpty(ya.DataSourceName) == false)
                    {
                        object ds = this.DataSources[ya.DataSourceName];
                        if (ds != null)
                        {
                            ValuePointList values = this.Datas.GetValuesByName(ya.Name);
                            values.BindingDataSource(
                                ds,
                                ya.TimeFieldName,
                                ya.TextFieldName,
                                ya.ValueFieldName,
                                ya.LanternValueFieldName);
                            values.SortByTime();
                        }
                    }
                }
                foreach (TitleLineInfo line in this.FooterLines)
                {
                    if (string.IsNullOrEmpty(line.DataSourceName) == false)
                    {
                        object ds = this.DataSources[line.DataSourceName];
                        if (ds != null)
                        {
                            ValuePointList values = this.Datas.GetValuesByName(line.Name);
                            values.BindingDataSource(
                                ds,
                                line.TimeFieldName,
                                line.TextFieldName,
                                line.ValueFieldName,
                                null);
                            values.SortByTime();
                        }
                    }
                }
                foreach (TitleLineInfo line in this.HeaderLines)
                {
                    if (string.IsNullOrEmpty(line.DataSourceName) == false)
                    {
                        object ds = this.DataSources[line.DataSourceName];
                        if (ds != null)
                        {
                            ValuePointList values = this.Datas.GetValuesByName(line.Name);
                            values.BindingDataSource(
                                ds,
                                line.TimeFieldName,
                                line.TextFieldName,
                                line.ValueFieldName,
                                null);
                            values.SortByTime();
                        }
                    }
                }
            }
        }
#endif
    }
}
