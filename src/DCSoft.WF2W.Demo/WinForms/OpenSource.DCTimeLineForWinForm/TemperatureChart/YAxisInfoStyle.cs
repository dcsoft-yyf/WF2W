using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// Y坐标轴样式
    /// </summary>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible( true )]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    [System.Runtime.InteropServices. Guid("56457014-298A-4D60-A54A-DCB482B65351")]
#endif
    public enum YAxisInfoStyle
    {
        /// <summary>
        /// 数值
        /// </summary>
        Value ,
        /// <summary>
        /// 文本
        /// </summary>
        Text ,
        /// <summary>
        /// 完整的背景
        /// </summary>
        Background,
        /// <summary>
        /// 部分背景
        /// </summary>
        PartialBackground,
        /// <summary>
        /// 每个文字均位于格子内的文本类型
        /// </summary>
        TextInsideGrid
    }

    /// <summary>
    /// Y坐阴影区域填充样式
    /// </summary>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(true)]
    [System.Runtime.InteropServices.Guid("564FF014-298A-4D60-A54A-DCFF82B65351")]
#endif
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum YAxisShadowStyle
    {
        /// <summary>
        /// 左斜线
        /// </summary>
        LeftSlant,
        /// <summary>
        /// 右斜线
        /// </summary>
        RightSlant,
        /// <summary>
        /// 竖线
        /// </summary>
        Vertical
    }

}
