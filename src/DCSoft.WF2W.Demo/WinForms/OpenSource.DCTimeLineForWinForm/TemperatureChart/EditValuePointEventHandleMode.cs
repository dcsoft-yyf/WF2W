using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 编辑数据点事件处理模式
    /// </summary>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible( false )]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
#endif
    public enum EditValuePointEventHandleMode
    {
        /// <summary>
        /// 不允许编辑数据点
        /// </summary>
        None ,
        /// <summary>
        /// 应用程序编程处理
        /// </summary>
        Program ,
        /// <summary>
        /// 静默模式
        /// </summary>
        Silent ,
        /// <summary>
        /// 使用内置的用户界面
        /// </summary>
        OwnedUI
    }
}
