using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间区域事件委托类型
    /// </summary>
    /// <param name="eventSender">事件发起者</param>
    /// <param name="args">事件参数</param>
    [System.Runtime.InteropServices.ComVisible( true )]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
    [System.Runtime.InteropServices. Guid("1A4043FD-D083-4D07-B673-98E48865FA35")]
    public delegate void TimeLineZoneEventHandler(
        object eventSender , 
        TimeLineZoneEventArgs args );

    /// <summary>
    /// 时间区域事件参数对象
    /// </summary>
     
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
    public partial class TimeLineZoneEventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="zone">时间区域对象</param>
        public TimeLineZoneEventArgs(TemperatureDocument document, TimeLineZoneInfo zone)
        {
            this._Document = document;
            this._Zone = zone;
        }

        private TimeLineZoneInfo _Zone = null;

        /// <summary>
        /// 时间区域对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TimeLineZoneInfo Zone
        {
            get
            {
                return _Zone; 
            }
        }

        private TemperatureDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocument Document
        {
            get
            {
                return _Document; 
            }
        }
    }
}
