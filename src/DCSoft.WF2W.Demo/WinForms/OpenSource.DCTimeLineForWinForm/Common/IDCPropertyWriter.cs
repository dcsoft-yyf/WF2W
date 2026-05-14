using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.Common
{
    [System.Runtime.InteropServices.ComVisible(false)]
    public interface IDCPropertyWriter
    {
       
        /// <summary>
        /// 开始输出数组
        /// </summary>
        void WriteStartArray();
        /// <summary>
        /// 结束输出数组
        /// </summary>
        void WriteEndArray();
        /// <summary>
        /// 开始输出对象
        /// </summary>
        void WriteStartObject(string objName =null );

        /// <summary>
        /// 结束输出对象
        /// </summary>
        void WriteEndObject();
        void WriteStartProperty(string propertyName);

        ////伍贻超20210729：用于在数组中输出纯文本的单个项
        //public virtual void WriteSingleTextInArray(string text)
        //{
        //}


        void WriteEndProperty();
#if !DCWriterForWASM
        /// <summary>
        /// 输出属性值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="Value">属性值</param>
        void WriteProperty(string name, string Value);

        /// <summary>
        /// 快速模式输出属性值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="Value">属性值</param>
        void WritePropertyUseAttribute(string name, string Value);
#endif
        
    }
}
