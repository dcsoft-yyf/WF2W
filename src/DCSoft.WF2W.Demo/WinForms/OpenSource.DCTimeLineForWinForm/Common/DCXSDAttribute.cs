using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.Common
{
    /// <summary>
    /// 自定义XSD标记
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    [Serializable]
    public sealed class DCXSDAttribute : Attribute
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCXSDAttribute(DCXSDOutputType type )
        {
            this._OutputType = type;
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCXSDAttribute( string name , DCXSDOutputType type)
        {
            _OutputType = type;
            _Name = name;
        }
        public DCXSDAttribute(Type specifyType )
        {
            this._SpecifyType = specifyType;
        }
        public DCXSDAttribute ( string name, string elementName  , Type elementType )
        {
            this._Name = name;
            this._ArrayElementName = elementName;
            this._ArrayElementType = elementType;
            this._OutputType = DCXSDOutputType.Element;
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCXSDAttribute( string name)
        {
            _Name = name;
        }
        private readonly string _Name = null;
        /// <summary>
        /// 名称
        /// </summary>
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Name
        {
            get
            {
                return _Name;
            }
            //set
            //{
            //    _Name = value;
            //}
        }

        private readonly DCXSDOutputType _OutputType = DCXSDOutputType.Default;
        /// <summary>
        /// 输出类型
        /// </summary>
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCXSDOutputType OutputType
        {
            get
            {
                return _OutputType;
            }
            //set
            //{
            //    _OutputType = value;
            //}
        }
        private readonly string _ArrayElementName = null;
       // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ArrayElementName
        {
            get
            {
                return _ArrayElementName;
            }
            //set
            //{
            //    _ArrayElementName = value;
            //}
        }

        private readonly Type _ArrayElementType = null;
        /// <summary>
        /// 数组元素类型
        /// </summary>
       // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Type ArrayElementType
        {
            get
            {
                return _ArrayElementType;
            }
            //set
            //{
            //    _ArrayElementType = value;
            //}
        }

        private readonly Type _SpecifyType = null;
        /// <summary>
        /// 指定数据类型
        /// </summary>
       // [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Type SpecifyType
        {
            get
            {
                return _SpecifyType;
            }
            //set
            //{
            //    _SpecifyType = value;
            //}
        }
#if !DCWriterForWASM

        public static DCXSDAttribute MyGetAttribute( System.Reflection.MemberInfo m )
        {
            return (DCXSDAttribute)Attribute.GetCustomAttribute(m, typeof(DCXSDAttribute), false);
        }
#endif
    }

    /// <summary>
    /// 输出类型
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum DCXSDOutputType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,
        /// <summary>
        /// 元素
        /// </summary>
        Element ,
        /// <summary>
        /// 属性
        /// </summary>
        Attribute,
        /// <summary>
        /// 对象唯一编号
        /// </summary>
        ID,
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// 忽略掉了，不输出。
        /// </summary>
        Ignore
    }
}
