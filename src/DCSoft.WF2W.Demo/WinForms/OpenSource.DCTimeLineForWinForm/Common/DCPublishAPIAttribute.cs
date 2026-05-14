using System;
using System.Collections.Generic;
using System.Text;

// 袁永福到此一游

namespace DCSoft.Common
{
    /// <summary>
    /// 表示可以对第三方公开的编程接口。南京都昌公司对外的技术支持只负责添加该特性的编程接口，
    /// 没添加该特性的不推荐客户使用。
    /// </summary>
    [System.Reflection.Obfuscation( Exclude =true , ApplyToMembers = true )]
    [AttributeUsage(AttributeTargets.All , AllowMultiple = false )]
    public sealed class DCPublishAPIAttribute : Attribute
    {
        /// <summary>
        /// 出生对象
        /// </summary>
        public DCPublishAPIAttribute()
        {
        }

        private bool _ApplyToMembers = false;
        /// <summary>
        /// 是否应用到类型成员
        /// </summary>
        public bool ApplyToMembers
        {
            get
            {
                return _ApplyToMembers; 
            }
            set
            {
                _ApplyToMembers = value; 
            }
        }
    }
}
