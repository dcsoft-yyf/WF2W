using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间轴专用的BOOL值
    /// </summary>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(true)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    [System.Runtime.InteropServices.Guid("5D450A19-F0CB-48F6-93CD-A0462B678BFC")]
#endif
    public enum DCTimeLineBooleanValue
    {
        /// <summary>
        /// 从其它元素继承BOOL值
        /// </summary>
        Inherit,
        /// <summary>
        /// 是
        /// </summary>
        True,
        /// <summary>
        /// 否
        /// </summary>
        False
    }
}
