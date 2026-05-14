using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 选择模式
    /// </summary>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible( false )]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
#endif
    public enum DCTimeLineSelectionMode
    {
        /// <summary>
        /// 不选择
        /// </summary>
        None ,
        /// <summary>
        /// 单选模式
        /// </summary>
        SingleSelect ,
        /// <summary>
        /// 多选模式
        /// </summary>
        MultiSelec
    }
}
