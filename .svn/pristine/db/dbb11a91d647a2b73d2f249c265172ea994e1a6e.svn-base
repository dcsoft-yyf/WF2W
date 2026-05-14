using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 数据点点击事件委托对象
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( true)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
[System.Runtime.InteropServices. Guid("7CB2272E-E00A-4556-A4E9-EB90556FBCC3")]
    public delegate void ValuePointClickEventHandler( 
        object eventSender , 
        ValuePointClickEventArgs args );

    /// <summary>
    /// 数据点点击事件参数
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public partial class ValuePointClickEventArgs 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="vp"></param>
        public ValuePointClickEventArgs(ValuePoint vp)
        {
            this._YAxis = vp.Parent as YAxisInfo;
            this._TitleLine = vp.Parent as TitleLineInfo;
            this._Point = vp;
        }

        private YAxisInfo _YAxis = null;
        /// <summary>
        /// 点所属的Y坐标轴
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public YAxisInfo YAxis
        {
            get
            {
                return _YAxis; 
            }
        }

        /// <summary>
        /// 数据序列的标题
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SerialTitle
        {
            get
            {
                if (_TitleLine != null)
                {
                    return _TitleLine.Title;
                }
                if (_YAxis != null)
                {
                    return _YAxis.Title;
                }
                return null;
            }
        }

        /// <summary>
        /// 数据序列的名称
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SerialName
        {
            get
            {
                if (_TitleLine != null)
                {
                    return _TitleLine.Name;
                }
                if (_YAxis != null)
                {
                    return _YAxis.Name ;
                }
                return null;
            }
        }
        private TitleLineInfo _TitleLine = null;
        /// <summary>
        /// 数据点所属的标题行信息对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineInfo TitleLine
        {
            get
            {
                return _TitleLine; 
            }
        }

        private ValuePoint _Point = null;
        /// <summary>
        /// 数据点对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint Point
        {
            get
            {
                return _Point; 
            }
        }
    }
}
