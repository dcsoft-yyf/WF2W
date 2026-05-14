using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace DCSoft.Common
{
    /// <summary>
    /// 支持展现属性的类型转换器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class TypeConverterSupportProperties : System.ComponentModel.TypeConverter
    {
        /// <summary>
        /// 支持获得属性
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// 获得属性
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection ps = TypeDescriptor.GetProperties(value, attributes);
            return ps;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return false;
            }
            return base.CanConvertTo(context, destinationType);
        }
    }
}
