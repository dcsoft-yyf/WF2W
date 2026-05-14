using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 超链接可视化模式
    /// </summary>

#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
#endif
    public enum DocumentLinkVisualStyle
    {
        /// <summary>
        /// 无效果
        /// </summary>
        None,
        /// <summary>
        /// 鼠标悬浮就显示
        /// </summary>
        Hover,
        /// <summary>
        /// 一直显示
        /// </summary>
        Always
    }
}
