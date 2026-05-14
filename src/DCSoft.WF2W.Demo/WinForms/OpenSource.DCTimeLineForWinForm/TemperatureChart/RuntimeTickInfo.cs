using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DCSoft.TemperatureChart
{

    internal class RuntimeTickInfo
    {
        /// <summary>
        /// 所在的时间区域对象
        /// </summary>
        public TimeLineZoneInfo Zone = null;
        /// <summary>
        /// 刻度开始时间
        /// </summary>
        public DateTime StartTime = DateTime.MinValue;
        /// <summary>
        /// 刻度结束时间
        /// </summary>
        public DateTime EndTime = DateTime.MinValue;
        /// <summary>
        /// 为当天第一个时刻对象
        /// </summary>
        public bool FirstTickInDate = false;
        /// <summary>
        /// 文本
        /// </summary>
        public string Text = null;
        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color = Color.Black;
        public float Left = 0;
        public float Width = 0;
        /// <summary>
        /// 序号
        /// </summary>
        public int Index = 0;
        /// <summary>
        /// 时刻序号
        /// </summary>
        public int TickIndex = 0;
        public override string ToString()
        {
            return EndTime.ToString("dd-HH") + "   " + Text;
        }
    }

    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    internal class RuntimeTickInfoList : List<RuntimeTickInfo>
    {
        public RuntimeTickInfoList()
        {
        }

        
        private DateTime _StartTime = TemperatureDocument.NullDate;

        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        private DateTime _EndTime = TemperatureDocument.NullDate;

        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        /// <summary>
        /// 最后一个刻度的时间
        /// </summary>
        public DateTime LastTime
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[this.Count - 1].EndTime;
                }
                else
                {
                    return TemperatureDocument.NullDate;
                }
            }
        }

        /// <summary>
        /// 列表中最后一个成员
        /// </summary>
        public RuntimeTickInfo LastItem
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

        public int GetStartDetectIndex(RectangleF dataGridBounds, RectangleF clipRectangle)
        {
            return GetStartIndexFoDetectLeft(Math.Max(dataGridBounds.Left, clipRectangle.Left) - dataGridBounds.Left);
        }

        /// <summary>
        /// 为检测刻度左端位置而计算开始计算的刻度编号
        /// </summary>
        /// <param name="left">指定的左端位置</param>
        /// <returns>获得的开始序号</returns>
        public int GetStartIndexFoDetectLeft(float left)
        {
            int startIndex = 0;
            int endIndex = this.Count - 1;
            int loopCount = 0;
            while (endIndex - startIndex > 10)
            {
                loopCount++;
                int index = (endIndex + startIndex) / 2;
                if (this[index].Left > left)
                {
                    endIndex = index;
                }
                else
                {
                    startIndex = index;
                }
            }
            DateTime dtm = this[ startIndex ].StartTime.Date ;
            for (int iCount = startIndex - 1 ; iCount >= 0; iCount--)
            {
                RuntimeTickInfo item = this[iCount];
                if (item.EndTime.Date < dtm)
                {
                    startIndex = iCount + 1;
                }
            }
            return startIndex;
        }

        public RuntimeTickInfo SafeGetItem( int index )
        {
            if (index < 0 || index >= this.Count)
            {
                return null;
            }
            else
            {
                return this[index];
            }
        }
        public DateTime GetMinDateForDetect(int tickIndex)
        {
            if (tickIndex < 0)
            {
                return this.StartTime;
            }
            return this[tickIndex].StartTime;

        }


        public DateTime GetMaxDateForDetect(int tickIndex)
        {
            if (tickIndex >= this.Count)
            {
                return this.EndTime;
            }
            return this[tickIndex].EndTime;
           
        }

        public float GetValuePointXPosition(DateTime dtm , ref RuntimeTickInfo tickInfo , bool linearMode = false )
        {

            int tickIndex = GetGlobalTickIndex(dtm, true , false  );
            if (dtm.Hour == 0 && dtm.Minute == 0 && dtm.Second == 0 && dtm != this.StartTime)
            {
                tickIndex++;
            }
            //////////////////////////////////////////////////////////但如果该在分页模式下位于首个刻度，就又正常了，先临时顶一下
            if (tickIndex >= 0 && tickIndex < this.Count -1 )
            {
                RuntimeTickInfo item = this[tickIndex];
                if (item.EndTime >= dtm)
                {
                    if (item.Zone != null && item.Zone.AlignToGrid == false)
                    {
                        // 不对齐到网格线
                        if (dtm <= item.StartTime )
                        {
                            tickInfo = item;
                            return item.Left;
                        }
                        else
                        {
                            float result = item.Left + item.Width * (dtm.Ticks - item.StartTime.Ticks) / (float)(item.EndTime.Ticks - item.StartTime.Ticks);
                            tickInfo = item;
                            return result;
                        }
                    }
                    if (tickIndex >= this.Count)
                    {
                        tickInfo = this[this.Count - 1];
                    }
                    else
                    {
                        tickInfo = this[tickIndex];
                    }

                    if (linearMode )
                    {
                        return GetTickLinearPosition(tickIndex, dtm);
                    }
                    else
                    {
                        return GetTickMiddlePosition(tickIndex);
                    }
                }
                if (item.Zone != null
                    && item.Zone.AlignToGrid == false)
                {
                    RuntimeTickInfo nextItem = this[tickIndex + 1];
                    if (nextItem.Zone == item.Zone)
                    {
                        // 不对齐到网格线，则在这个小刻度中取线性值
                        float result = item.Left + (nextItem.Left - item.Left) * (dtm.Ticks - item.EndTime.Ticks) 
                            / (float)(nextItem.EndTime.Ticks - item.EndTime.Ticks);
                        tickInfo = item;
                        return result;
                    }
                }
            }
            if (tickIndex >= this.Count)
            {
                tickInfo = this[this.Count - 1];
            }
            else
            {
                tickInfo = this[tickIndex];
            }

            if (linearMode == true)
            {
                return GetTickLinearPosition(tickIndex, dtm);
            }
            else
            {
                return GetTickMiddlePosition(tickIndex);
            }
        }


        public float GetTickMiddlePosition(int tickIndex)
        {
            if (tickIndex < 0)
            {
                return 0;
            }
            if (tickIndex >= this.Count)
            {
                RuntimeTickInfo item = this[this.Count - 1];
                return item.Left + item.Width;
            }
            else
            {
                RuntimeTickInfo item = this[tickIndex];
                return item.Left + item.Width / 2;
            }
        }

        /// <summary>
        /// 根据时间在刻度时间段内的相对位置获取刻度坐标的线性位置
        /// </summary>
        /// <param name="tickIndex"></param>
        /// <param name="dtm"></param>
        /// <returns></returns>
        public float GetTickLinearPosition(int tickIndex, DateTime dtm)
        {
            if (tickIndex < 0)
            {
                return 0;
            }
            if (tickIndex >= this.Count)
            {
                RuntimeTickInfo item = this[this.Count - 1];
                return item.Left + item.Width;
            }
            else
            {
                RuntimeTickInfo item = this[tickIndex];

                if (dtm < item.StartTime)
                {
                    return item.Left;
                }
                else if (dtm > item.EndTime)
                {
                    return item.Left + item.Width;
                }
                else
                {
                    //根据给定时间在刻度时间段内的比率分配坐标
                    TimeSpan totalspan = item.EndTime - item.StartTime;
                    TimeSpan span = dtm - item.StartTime;
                    float rate = (float)span.Ticks / (float)totalspan.Ticks;
                    float width = item.Width * rate;
                    return item.Left + width;
                }               
            }
        }

        /// <summary>
        /// 获得全局的时刻编号
        /// </summary>
        /// <param name="dtm">时间</param>
        /// <param name="forValuePoint">是否是为了数据点而计算序号</param>
        /// <param name="forStartTime">采用StartTime进行计算</param>
        /// <returns>序号</returns>
        public int GetGlobalTickIndex(DateTime dtm , bool forValuePoint , bool forStartTime )
        {
            if (this.Count == 0)
            {
                return 0;
            }
            if (dtm < this[0].EndTime)
            {
                return 0;
            }
            if (dtm > this[this.Count - 1].EndTime)
            {
                return this.Count - 1;
            }
            //float tickSpan = DCSoft.Common.CountDown.GetTickCountFloat();
            int startIndex = 0;
            int endIndex = this.Count - 1 ;
            int totalCount = 0;
            int result = -1;
            int loopCount = 0;
            // 由于大部分情况下时间刻度是比较均匀的，在此计算大概的起始位置，减少循环次数
            int matchIndex = ( int )( this.Count * (dtm.Ticks - this.StartTime.Ticks) / (this.EndTime.Ticks - this.StartTime.Ticks) );
            if (matchIndex >= 0 && matchIndex < this.Count)
            {
                DateTime dtm2 = forStartTime ? this[matchIndex].StartTime : this[matchIndex].EndTime;
                if (dtm < dtm2)
                {
                    endIndex = matchIndex;
                    int index2 = Math.Max(0, endIndex - 10);
                    if (this[index2].EndTime < dtm)
                    {
                        startIndex = index2;
                    }
                }
                else
                {
                    startIndex = matchIndex;
                    int index2 = Math.Min(startIndex + 10, this.Count - 1);
                    if (this[index2].StartTime > dtm)
                    {
                        endIndex = index2;
                    }
                }
            }
            while (endIndex - startIndex > 5)
            {
                loopCount++;
                if (totalCount++ > 1000)
                {
                    // 认为是死循环了，返回0
                    result = 0;
                    break;
                }
                // 前后区域长度超过10则采用二分法检索
                int index = (startIndex + endIndex) / 2;
                DateTime curDtm = forStartTime ? this[index].StartTime : this[index].EndTime;
                int eql = dtm.CompareTo(curDtm);
                if ( eql < 0 )
                {
                    endIndex = index;
                }
                else if ( eql > 0 )
                {
                    startIndex = index;
                }
                else// 时间相等
                {
                    result = index;
                    break;
                }
            }//while
            if (result == -1)
            {
                // 顺序查找
                endIndex = Math.Min(endIndex + 1, this.Count);
                for (int iCount = startIndex; iCount < endIndex; iCount++)
                {
                    loopCount++;
                    //RuntimeTickInfo item = this[iCount];
                    DateTime itemTime = this[iCount].EndTime;
                    if (forStartTime)
                    {
                        itemTime = this[iCount].StartTime;
                    }
                    if (dtm <= itemTime)
                    {
                        return iCount;
                    }
                }
            }
            if (result < 0)
            {
                result = endIndex;
            }
            //tickSpan = DCSoft.Common.CountDown.GetTickCountFloat() - tickSpan;
            return result;
        }

        public void InsertRuntimeTickInfo(
            int index, 
            DateTime startTime, 
            DateTime endTime, 
            TickInfoList ticks , 
            TimeLineZoneInfo zone )
        {
            RuntimeTickInfo preTick = this.SafeGetItem(index-1);
            RuntimeTickInfo nextTick = this.SafeGetItem(index);
            if (zone != null && zone.IsExpanded == false)
            {
                // 对于被收缩的刻度，只显示一个刻度
                RuntimeTickInfo info = new RuntimeTickInfo();
                info.Zone = zone;
                info.StartTime = zone.StartTime;
                info.EndTime = zone.EndTime;
                info.Text = string.Empty;
                info.TickIndex = 0;
                this.Insert(index, info);
                // 将时间刻度前后连续起来
                if (preTick != null)
                {
                    preTick.EndTime = info.StartTime;
                }
                if (nextTick != null)
                {
                    nextTick.StartTime = info.EndTime;
                }
                return ;
            }

            if (ticks == null || ticks.Count == 0)
            {
                if (zone != null && zone.AutoTickStepSeconds > 0)
                {
                    // 自动生成时刻列表
                    ticks = new TickInfoList();
                    ticks.FillTickItems(zone.AutoTickStepSeconds, 24 * 3600 , zone.AutoTickFormatString);
                    if (ticks.Count == 0)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            if (ticks == null || ticks.Count == 0)
            {
                // 没有设置刻度
                return;
            }
            int tickIndex = ticks.GetStartHourTickIndex(startTime);
            DateTime curDate = startTime.Date;
            int totalCount = 0;
            bool firstZero = ticks[0].Value == 0;

            // 判断是否为等分刻度
            bool isHalve = false;           
            //float step = 24f / ticks.Count;
            //for (int iCount = 0; iCount < ticks.Count; iCount++)
            //{
            //    float v = step * ( iCount + 1 );
            //    if (Math.Abs(v - ticks[iCount].Value) > 0.01 )
            //    {
            //        isHalve = false;
            //        break;
            //    }
            //}
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
             
            for (DateTime timeCount = startTime; ; )
            {
                if (totalCount++ > 1000000)
                {
                    // 超出1000000次循环，则认为是死循环，退出
                    break;
                }
                TickInfo tick = ticks[tickIndex];
                
                RuntimeTickInfo info = new RuntimeTickInfo();
                info.TickIndex = tickIndex;
                info.Text = tick.Text;
                info.Color = tick.Color;
                info.Zone = zone;

                // 计算刻度开始时间
                if (tickIndex == 0)
                {
                    // 对于第一个刻度，刻度开始时间为零时
                    info.StartTime = curDate;
                }
                else
                {
                    if (firstZero)
                    {
                        // 对于零开始的刻度，
                        // 刻度开始时间为当前数值
                        info.StartTime = curDate.AddHours(ticks[tickIndex].Value);
                    }
                    else
                    {
                        // 对于非零开始的刻度，
                        // 刻度开始时间为本刻度数值和上个刻度数值的中间值
                        if (isHalve)
                        {
                            info.StartTime = curDate.AddHours(ticks[tickIndex - 1].Value );
                        }
                        else
                        {
                            info.StartTime = curDate.AddHours((ticks[tickIndex - 1].Value + tick.Value) / 2);
                        }
                    }
                }

                if (tickIndex == ticks.Count - 1)
                {
                    info.EndTime = (curDate.AddDays(1))/*.AddMilliseconds(-1)*/;
                }
                else
                {
                    if (firstZero)
                    {
                        // 对于零开始的刻度，
                        // 刻度结束时间等于下一个刻度的数值
                        info.EndTime = (curDate.AddHours(ticks[tickIndex + 1].Value)).AddMilliseconds(-1);
                    }
                    else if (isHalve)
                    {
                        //等分刻度
                        info.EndTime = (curDate.AddHours(ticks[tickIndex].Value)).AddMilliseconds(-1);
                    }
                    else
                    {
                        // 对于非零开始的刻度，
                        // 刻度结束时间等于本刻度数值和下一个刻度数值的中间值
                        info.EndTime = (curDate.AddHours((tick.Value + ticks[tickIndex + 1].Value) / 2)).AddMilliseconds(-1);
                    }
                }
                timeCount = info.EndTime;
                if (info.StartTime >= endTime)
                {
                    if (endTime.Hour == 0 && endTime.Minute == 0 && endTime.Second == 0)
                    {
                        // 结束时间为当天的零时，则退出
                        break;
                    }
                }
                this.Insert(index, info);
                
                index++;
                tickIndex++;
                if (tickIndex >= ticks.Count)
                {
                    tickIndex -= ticks.Count;
                    curDate = curDate.AddDays(1);
                }
                if (timeCount >= endTime)
                {
                    break;
                }
            }//for
            if (zone != null)
            {
                if (preTick != null)
                {
                    RuntimeTickInfo tick = this.SafeGetItem(this.IndexOf(preTick) + 1);
                    if (tick != null)
                    {
                        preTick.EndTime = tick.StartTime;
                    }
                }
                if (nextTick != null)
                {
                    RuntimeTickInfo tick = this.SafeGetItem(this.IndexOf(nextTick) - 1);
                    if (tick != null)
                    {
                        nextTick.StartTime = tick.EndTime;
                        if (nextTick.StartTime == nextTick.EndTime)
                        {
                            // 遇到时间长度为0的刻度，删除
                            this.Remove(nextTick);
                        }
                    }
                }
            }
        }
#if !DCWriterForWASM
        internal DateTime GetDateTimeByXPosition(RectangleF dataGridBounds, float x)
        {
            int startIndex = 0;
            int endIndex = this.Count - 1;
            int loopCount = 0;
            x = x - dataGridBounds.Left;
            // 首先使用二分法快速缩小位置
            while (endIndex - startIndex > 5)
            {
                if (loopCount++ > 1000)
                {
                    // 认为是死循环了，返回无效数据
                    return TemperatureDocument.NullDate;
                }
                int index = (startIndex + endIndex) / 2;
                float pos = this[index].Left;
                if (x < pos)
                {
                    endIndex = index;
                }
                else
                {
                    startIndex = index;
                }
            }//while
            for (int iCount = endIndex; iCount >= startIndex ; iCount -- )
            {
                RuntimeTickInfo info = this[iCount];
                if (info.Left < x)
                {
                    // 命中一个时刻
                    if (TemperatureDocument.IsNullDate(info.EndTime))
                    {
                        return info.StartTime;
                    }
                    else
                    {
                        // 在两个时间内进行线性差值
                        long tickStart = info.StartTime.Ticks;
                        long tickEnd = info.EndTime.Ticks;
                        long v = (long)((tickEnd - tickStart) * (x - info.Left) / info.Width );
                        //TimeSpan span = new TimeSpan(v);

                        DateTime dtm = TemperatureDocument.NullDate;
                        try
                        {
                            dtm = new DateTime(tickStart + v);
                        }
                        catch
                        {
                            
                        }
                        return dtm;
                    }
                }
            }
            return TemperatureDocument.NullDate;
        }
#endif
        /// <summary>
        /// 获得指定日期在标尺上的X坐标值
        /// </summary>
        /// <param name="dataGridBounds"></param>
        /// <param name="dtm"></param>
        /// <returns></returns>
        public float GetXPosition(RectangleF dataGridBounds, DateTime dtm)
        {
            int index = GetGlobalTickIndex(dtm, false , false );
            if (index < 0)
            {
                return float.NaN;
            }
            if (index >= this.Count)
            {
                return dataGridBounds.Left + this[this.Count - 1].Left + this[this.Count - 1].Width;
            }
            RuntimeTickInfo info = this[index];
            if (info.EndTime == dtm)
            {
                return dataGridBounds.Left + info.Left + info.Width;
            }
            if (info.TickIndex == 0 || info.EndTime == dtm )
            {
                return dataGridBounds.Left + info.Left;
            }
            
            //float tickWidth = dataGridBounds.Width / this.Count;
            if (index > 0 )
            {
                float result = dataGridBounds.Left + info.Left +
                    info.Width * ( dtm.Ticks - info.StartTime.Ticks) / (float)(info.EndTime.Ticks - info.StartTime.Ticks);
                return result;
            }
            else
            {
                return this[ index].Left ;
            }
        }

        /// <summary>
        /// 获得指定日期在标尺上的X坐标值
        /// </summary>
        /// <param name="dataGridBounds">数据网格矩形</param>
        /// <param name="dtm">时间</param>
        /// <param name="tickInfo">对应的时刻对象</param>
        /// <returns></returns>
        public float GetXPositionForLabel(
            RectangleF dataGridBounds, 
            DateTime dtm , 
            ref RuntimeTickInfo tickInfo )
        {
            int index = GetGlobalTickIndex(dtm, false , false );
            if (index < 0)
            {
                return float.NaN;
            }
            if (index >= this.Count)
            {
                return dataGridBounds.Left + this[this.Count - 1].Left + this[this.Count - 1].Width;
            }
            if (dtm.Hour == 0 && dtm.Minute == 0 && dtm.Second == 0 && dtm != this.StartTime
                && dtm < this.EndTime)
            {
                index++;
            }
            /////////////////
            RuntimeTickInfo info = this[index];
            if (index > 0 && dtm < info.EndTime)
            {
                index--;
            }
            //float tickWidth = dataGridBounds.Width / this.Count;
            if (index < this.Count - 1)
            {
                //RuntimeTickInfo nextInfo = this[index + 1];
                tickInfo = info;
                float result = dataGridBounds.Left + info.Left ;
                return result;
            }
            else
            {
                tickInfo = this[index];
                return this[index].Left;
            }
        }

        /// <summary>
        /// 标准时刻单位
        /// </summary>
        internal DCTimeUnit _StdTickUnit = DCTimeUnit.Hour;

        ///// <summary>
        ///// 获得
        ///// </summary>
        ///// <param name="index"></param>
        ///// <param name="tickStep"></param>
        ///// <param name="unit"></param>
        ///// <returns></returns>
        //public int GetRuntimeTickStep(int index, int tickStep, DCTimeUnit unit )
        //{
        //    DateTime dtm = TemperatureDocument.AddDateTime( this[index].Time , tickStep , unit );
        //    int index2 = GetGlobalTickIndex(dtm, false);
        //    int result = index2 - index;

        //    return result;
        //}

        /// <summary>
        /// 获得指定天的视图边框
        /// </summary>
        /// <param name="dataGridBounds"></param>
        /// <param name="dtm"></param>
        /// <returns></returns>
        public RectangleF GetDayBounds(RectangleF dataGridBounds, DateTime dtm)
        {
            RectangleF rect2 = new RectangleF(
                            -1,
                            dataGridBounds.Top,
                            -1,
                            dataGridBounds.Height);
            //float tickWidth = dataGridBounds.Width / this.Count;
            bool matchEnd = false;
            dtm = dtm.Date ;
            DateTime endDate = dtm.AddDays(1);
            int startIndex = GetGlobalTickIndex(dtm, false, true);
            //RuntimeTickInfo info = this.SafeGetItem(startIndex);
            startIndex = Math.Min(this.Count - 1, startIndex + 5);
            int loopCount = 0;
            for (int iCount = startIndex ; iCount >= startIndex - 10 ; iCount--)
            {
                loopCount++;
                RuntimeTickInfo item = this[iCount];
                if (dtm >= item.StartTime)
                {
                    rect2.X = dataGridBounds.Left + item.Left;
                    if (dtm == item.EndTime)
                    {
                        rect2.X = dataGridBounds.Left + item.Left + item.Width;
                    }
                    for (int iCount2 = iCount; iCount2 < this.Count; iCount2++)
                    {
                        loopCount++;
                        RuntimeTickInfo item2 = this[iCount2];
                        if (endDate <= item2.EndTime)
                        {
                            rect2.Width = dataGridBounds.Left + item2.Left + item2.Width - rect2.Left;
                            matchEnd = true;
                            break;
                        }
                    }
                    break;
                }
            }
             
            if (rect2.Left < 0)
            {
                return RectangleF.Empty;
            }
            if (matchEnd == false)
            {
                rect2.Width = dataGridBounds.Right - rect2.Left;
            }
            return rect2;
        }

        private float _TotalWidth = 0;

        public float TotalWidth
        {
            get { return _TotalWidth; }
            set { _TotalWidth = value; }
        }
       

        public float GetTickLeft(int index)
        {
            if (this.Count == 0)
            {
                return 0;
            }
            if (index < 0)
            {
                return 0;
            }
            if (index >= this.Count)
            {
                return this[this.Count - 1].Left + this[ this.Count -1].Width ;
            }
            return this[index].Left;
        }

    }
}
