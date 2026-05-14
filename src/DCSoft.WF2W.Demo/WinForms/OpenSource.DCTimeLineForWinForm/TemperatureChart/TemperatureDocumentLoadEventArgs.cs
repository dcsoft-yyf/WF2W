using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    [System.Runtime.InteropServices.ComVisible(false)]
    public class TemperatureDocumentLoadEventArgs
    {
        public TemperatureDocumentLoadEventArgs()
        {
            LoadHintString = LoadingDocumentResultString = string.Empty;
        }

        /// <summary>
        /// 前端传送的加载提示信息字符串
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string LoadHintString { get; set; }
        /// <summary>
        /// 由事件中填充的需要加载的时间轴文档XML字符串
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string LoadingDocumentResultString { get; set; }
    }

}
