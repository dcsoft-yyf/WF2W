using System;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic ;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

namespace DCSoft.Common
{
	/// <summary>
	/// 数值,类型转换相关帮助类
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Runtime.InteropServices.ComVisible(false)]
    public static class ValueTypeHelper
	{
        

        private readonly static Dictionary<Type, System.ComponentModel.TypeConverter> _TypeConverters 
            = new Dictionary<Type, System.ComponentModel.TypeConverter>();
        /// <summary>
        /// 获得类型对应的类型转换器
        /// </summary>
        /// <param name="t">类型对象</param>
        /// <returns>类型转换器</returns>
        public static System.ComponentModel.TypeConverter GetTypeConverter(Type t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }
            System.ComponentModel.TypeConverter result = null;
            lock (_TypeConverters)
            {
                if (_TypeConverters.TryGetValue(t, out result) == false)
                {
                    var attrs = t.GetCustomAttributes(true);
                    foreach (var attr in attrs)
                    {
                        if (attr is System.ComponentModel.TypeConverterAttribute)
                        {
                            var ta = (System.ComponentModel.TypeConverterAttribute)attr;
                            var tc = Type.GetType(ta.ConverterTypeName, false, true);
                            if( tc == null || ta.ConverterTypeName.Length > 0 )
                            {
                                int index = ta.ConverterTypeName.IndexOf(',');
                                if(index >= 0 )
                                {
                                    var typeName = ta.ConverterTypeName.Substring(0, index);
                                    tc = Type.GetType(typeName, false, true);
                                    if(tc == null )
                                    {
                                        tc = t.Assembly.GetType(typeName, false, true);
                                    }
                                }
                            }
                            if (tc != null)
                            {
                                result = (System.ComponentModel.TypeConverter)System.Activator.CreateInstance(tc);
                            }
                            break;
                        }
                    }
                    if (result == null)
                    {
                        result = System.ComponentModel.TypeDescriptor.GetConverter(t);
                    }
                    if (result == null)
                    {
                        result = new System.ComponentModel.TypeConverter();
                    }
                    _TypeConverters[t] = result;
                }
            }
            return result;
        }

      
        private class MyPropertyNameCompaer : IComparer<PropertyInfo >
        {
            public int Compare(PropertyInfo x, PropertyInfo y)
            {
                return string.Compare(x.Name, y.Name, false);
            }
        }
//#if ! DCWriterForWASM
//        /// <summary>
//        /// 按照名称调用对象成员方法
//        /// </summary>
//        /// <param name="instance">对象实例</param>
//        /// <param name="methodName">方法名称，不区分大小写</param>
//        /// <param name="parameters">参数值数组</param>
//        /// <param name="throwException">是否抛出异常</param>
//        /// <returns>方法返回值</returns>
//        public static object CallMethodByName(
//            object instance, 
//            string methodName, 
//            object[] parameters , 
//            bool throwException )
//        {
//            if (instance == null)
//            {
//                if (throwException)
//                {
//                    throw new ArgumentNullException("instance");
//                }
//                return null;
//            }
//            if (string.IsNullOrEmpty(methodName))
//            {
//                if (throwException)
//                {
//                    throw new ArgumentNullException("methodName");
//                }
//                return null;
//            }
//            foreach (MethodInfo method in instance.GetType().GetMethods(
//                BindingFlags.Public
//                | BindingFlags.Instance))
//            {
//                if (string.Compare(method.Name, methodName, true) == 0)
//                {
//                    ParameterInfo[] ps = method.GetParameters();
//                    int len1 = ps == null ? 0 : ps.Length;
//                    int len2 = parameters == null ? 0 : parameters.Length;
//                    if (len1 != len2)
//                    {
//                        continue;
//                    }
//                    if (throwException)
//                    {
//                        object[] mps = null;
//                        if (len1 > 0)
//                        {
//                            mps = new object[len1];
//                            for (int iCount = 0; iCount < parameters.Length; iCount++)
//                            {
//                                if (parameters[iCount] != null)
//                                {
//                                    if (ps[iCount].ParameterType.IsInstanceOfType(parameters[iCount]))
//                                    {
//                                        mps[iCount] = parameters[iCount];
//                                    }
//                                    else
//                                    {
//                                        mps[iCount] = ConvertTo(parameters[iCount], ps[iCount].ParameterType);
//                                    }
//                                }
//                                else
//                                {
//                                    mps[iCount] = GetDefaultValue(ps[iCount].ParameterType);
//                                }
//                            }
//                        }
//                        object result = method.Invoke(instance, mps);
//                        return result;
//                    }
//                    else
//                    {
//                        try
//                        {
//                            object[] mps = null;
//                            if (len1 > 0)
//                            {
//                                mps = new object[len1];
//                                for (int iCount = 0; iCount < parameters.Length; iCount++)
//                                {
//                                    if (parameters[iCount] != null)
//                                    {
//                                        if (ps[iCount].ParameterType.IsInstanceOfType(parameters[iCount]))
//                                        {
//                                            mps[iCount] = parameters[iCount];
//                                        }
//                                        else
//                                        {
//                                            mps[iCount] = ConvertTo(parameters[iCount], ps[iCount].ParameterType);
//                                        }
//                                    }
//                                    else
//                                    {
//                                        mps[iCount] = GetDefaultValue(ps[iCount].ParameterType);
//                                    }
//                                }
//                            }
//                            object result = method.Invoke(instance, mps);
//                            return result;
//                        }
//                        catch (Exception ext)
//                        {
//                            //DCConsole.Default.WriteLineError(ext.Message);
//                            return null;
//                        }
//                    }
//                }
//            }
//            if (throwException)
//            {
//                throw new ArgumentException(instance.GetType().FullName + "." + methodName);
//            }
//            return null;
//        }
//#endif
//        public static object GetPropertyValueMultiLayer(
//            object instance,
//            string propertyName,
//            object defaultValue,
//            bool throwExecption)
//        {
//            if (instance == null)
//            {
//                throw new ArgumentNullException("instance");
//            }
//            if (string.IsNullOrEmpty(propertyName))
//            {
//                if (throwExecption)
//                {
//                    throw new ArgumentNullException("propertyName");
//                }
//                else
//                {
//                    return defaultValue;
//                }
//            }
//            string[] pItem = propertyName.Split('.');
//            object currentInstance = instance;
//            for (int iCount = 0; iCount < pItem.Length; iCount++)
//            {
//                string item = pItem[iCount].Trim();
//                if (string.IsNullOrEmpty(item))
//                {
//                    if (iCount == pItem.Length - 1)
//                    {
//                        return defaultValue;
//                    }
//                    continue;
//                }
//                PropertyInfo property = currentInstance.GetType().GetProperty(
//                    item,
//                    BindingFlags.Public | BindingFlags.Instance );
//                if (property == null)
//                {
//                    property = currentInstance.GetType().GetProperty(
//                        item,
//                        BindingFlags.Public | BindingFlags.Instance);
//                }
//                if (property == null)
//                {
//                    if (throwExecption)
//                    {
//                        throw new Exception("未找到属性" + currentInstance.GetType().FullName + "." + item);
//                    }
//                    return defaultValue;
//                }
//                object ins = property.GetValue(currentInstance, null);
//                if (ins == null)
//                {
//                    return defaultValue;
//                }
//                currentInstance = ins;
//                if (iCount == pItem.Length - 1)
//                {
//                    return ins;
//                }
//            }//for
//            return defaultValue ;
//        }

        //public static bool SetPropertyValueMultiLayer(
        //    object instance,
        //    string propertyName,
        //    object Value,
        //    bool throwExecption,
        //    bool ignoreCase = false )
        //{
        //    if (instance == null)
        //    {
        //        throw new ArgumentNullException("instance");
        //    }
        //    if (string.IsNullOrEmpty(propertyName))
        //    {
        //        throw new ArgumentNullException("propertyName");
        //    }
        //    string[] pItem = propertyName.Split('.');
        //    object currentInstance = instance;
        //    var flags = BindingFlags.Public | BindingFlags.Instance;
        //    if( ignoreCase )
        //    {
        //        flags = flags | BindingFlags.IgnoreCase;
        //    }
        //    for (int iCount = 0; iCount < pItem.Length; iCount++)
        //    {
        //        string item = pItem[ iCount ].Trim();
        //        if (string.IsNullOrEmpty(item))
        //        {
        //            continue;
        //        }
        //        if (iCount == pItem.Length - 1)
        //        {
        //            // 最后一个属性，赋值
        //            return SetPropertyValue(currentInstance, item, Value, throwExecption);
        //        }
        //        else
        //        {
        //            PropertyInfo property = currentInstance.GetType().GetProperty(
        //                item, 
        //                flags);
        //            if (property == null)
        //            {
        //                if (throwExecption)
        //                {
        //                    throw new Exception("未找到属性" + currentInstance.GetType().FullName + "." + item );
        //                }
        //            }
        //            object ins = property.GetValue(currentInstance, null);
        //            if (ins == null)
        //            {
        //                return false;
        //            }
        //            currentInstance = ins;
        //        }
        //    }
        //    return false;
        //}

        private class PropertyValueInfo
        {
            public static PropertyValueInfo GetInfo(Type t, string name)
            {
                if (t == null)
                {
                    throw new ArgumentNullException("t");
                }
                if (name == null || name.Length == 0)
                {
                    throw new ArgumentNullException("name");
                }
                name = name.Trim().ToLower();
                Dictionary<string, PropertyValueInfo> result = GetInfos(t);
                PropertyValueInfo info = null;
                if (result.TryGetValue(name, out info))
                {
                    return info;
                }
                else
                {
                    return null;
                }
            }
            private static Dictionary<Type, Dictionary<string, PropertyValueInfo>> _Infos
                = new Dictionary<Type, Dictionary<string, PropertyValueInfo>>();
            private static Dictionary<string, PropertyValueInfo> GetInfos(Type t)
            {
                if (t == null)
                {
                    throw new ArgumentNullException("t");
                }
                Dictionary<string, PropertyValueInfo> result = null;
                lock (_Infos)
                {
                    if (_Infos.TryGetValue(t, out result) == false)
                    {
                        result = new Dictionary<string, PropertyValueInfo>();
                        _Infos[t] = result;
                        foreach (PropertyInfo p in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (p.IsSpecialName)
                            {
                                continue;
                            }
                            result[p.Name.ToLower()] = new PropertyValueInfo(p);
                        }//foreach
                    }
                }
                return result;
            }

            private static bool _ReadColorDefalutValueError = false;
            public PropertyValueInfo(PropertyInfo p)
            {
                this.Name = p.Name;
                this.LowcaseName = p.Name.ToLower();
                this.RawInfo = p;
                this.CanRead = p.CanRead;
                this.CanWrite = p.CanWrite;
                if (_ReadColorDefalutValueError && p.PropertyType == typeof(System.Drawing.Color))
                {
                    // 曾经出现读取颜色默认值错误，则今后不再读取任何默认颜色值
                    this.HasDefaultValue = false;
                }
                else
                {
                    DefaultValueAttribute dva = (DefaultValueAttribute)Attribute.GetCustomAttribute(p, typeof(DefaultValueAttribute), true);
                    if (dva == null)
                    {
                        this.HasDefaultValue = false;
                    }
                    else
                    {
                        if (dva.Value == null && p.PropertyType == typeof(System.Drawing.Color))
                        {
                            // 读取默认颜色值错误
                            this.HasDefaultValue = false;
                            _ReadColorDefalutValueError = true;
                        }
                        else
                        {
                            this.HasDefaultValue = true;
                            this.DefaultValue = dva.Value;
                        }
                    }
                }
                this.IsEnumType = p.PropertyType.IsEnum;
                this.PropertyType = p.PropertyType;

                TypeConverterAttribute tca = (TypeConverterAttribute)Attribute.GetCustomAttribute(p, typeof(TypeConverterAttribute), true);
                if (tca == null)
                {
                    tca = (TypeConverterAttribute)Attribute.GetCustomAttribute(p.PropertyType, typeof(TypeConverterAttribute), true);
                }
                if (tca != null)
                {
                    Type t = Type.GetType(tca.ConverterTypeName);
                    if (t != null)
                    {
                        //伍贻超20191128：碰到不带默认构造函数的类型时下面函数有可能会出错，先跳过
                        try
                        {
                            this.Converter = (TypeConverter)System.Activator.CreateInstance(t);
                        } catch (Exception e)
                        {

                        }
                    }
                }
            }

            private readonly string Name;
            private readonly string LowcaseName;
            private readonly PropertyInfo RawInfo;
            private readonly bool CanRead;
            private readonly bool CanWrite;
            private readonly Type PropertyType;
            private readonly bool HasDefaultValue;
            private readonly object DefaultValue;
            private readonly bool IsEnumType;
            private readonly TypeConverter Converter;
            public bool SetValue(object instance, object newValue, bool throwException)
            {
                object pValue = null;
                if (newValue == null || DBNull.Value.Equals(newValue))
                {
                    // 原始值为空
                    if (this.HasDefaultValue)
                    {
                        // 使用默认值
                        pValue = this.DefaultValue;
                    }
                    else
                    {
                        // 使用类型默认值
                        pValue = GetDefaultValue(this.PropertyType);
                    }
                }
                else if (this.IsEnumType && newValue is string)
                {
                    // 枚举类型
                    if (throwException)
                    {
                        pValue = Enum.Parse(this.PropertyType, (string)newValue, true);
                    }
                    else
                    {
                        try
                        {
                            pValue = Enum.Parse(this.PropertyType, (string)newValue, true);
                        }
                        catch( System.Exception ext )
                        {
                            // 转换失败，退出
                            return false;
                        }
                    }
                }
                else if (this.PropertyType.IsInstanceOfType(newValue) == false)
                {
                    // 需要进行类型转换
                    if (throwException)
                    {

                        if (this.Converter != null)
                        {
                            pValue = this.Converter.ConvertFrom(newValue);
                        }
                        else
                        {
                            pValue = Convert.ChangeType(newValue, this.PropertyType);
                        }
                    }
                    else
                    {
                        try
                        {
                            if (this.Converter != null)
                            {
                                pValue = this.Converter.ConvertFrom(newValue);
                            }
                            else
                            {
                                if (this.IsEnumType)
                                {
                                    if (newValue is string)
                                    {
                                        pValue = Enum.Parse(this.PropertyType, (string)newValue);
                                    }
                                    else
                                    {
                                        pValue = Enum.ToObject(this.PropertyType, newValue);
                                    }
                                }
                                else
                                {
                                    pValue = Convert.ChangeType(newValue, this.PropertyType);
                                }

                            }
                        }
                        catch (Exception)
                        {
                            return false;

                        }
                    }
                }
                else
                {
                    pValue = newValue;
                }
                if (throwException)
                {
                    this.RawInfo.SetValue(instance, pValue, null);
                    return true;
                }
                else
                {
                    try
                    {
                        this.RawInfo.SetValue(instance, pValue, null);
                        return true;
                    }
                    catch (Exception)
                    {
                    }
                }
                return false;
            }

        }


        /// <summary>
        /// 设置对象的属性值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="Value">属性值</param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns>操作是否成功</returns>
        public static bool SetPropertyValue(
            object instance,
            string propertyName,
            object Value,
            bool throwException)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            PropertyValueInfo info = PropertyValueInfo.GetInfo(instance.GetType(), propertyName);
            if( info == null )
            {
                if (throwException)
                {
                    throw new ArgumentException(instance.GetType().FullName + "." + propertyName);
                }
                else
                {
                    return false;
                }
            }
            return info.SetValue(instance, Value, throwException);
            
           
        }

      

        private readonly static Dictionary<Type, Dictionary<string, PropertyInfo>> _GetPropertyValueInfos
            = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        /// <summary>
        /// 获得对象属性值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns>获得的属性值</returns>
        public static object GetPropertyValue(object instance, string propertyName, bool throwException)
        {
            if (instance == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException("instance");
                }
                return null;
            }
            if (string.IsNullOrEmpty(propertyName))
            {
                if (throwException)
                {
                    throw new ArgumentNullException("propertyName");
                }
                return null;
            }
            Type type = instance.GetType();
            Dictionary<string , PropertyInfo > properties = null ;
            lock (_GetPropertyValueInfos)
            {
                if (_GetPropertyValueInfos.TryGetValue(type, out properties) == false)
                {
                    properties = new Dictionary<string, PropertyInfo>();
                    _GetPropertyValueInfos[type] = properties;
                }
            }
            PropertyInfo p = null ;
            lock (properties)
            {
                if (properties.TryGetValue(propertyName, out p) == false)
                {
                    p = type.GetProperty(
                        propertyName,
                        BindingFlags.Public
                        | BindingFlags.Instance
                        | BindingFlags.IgnoreCase);
                    if (p == null)
                    {
                        if (throwException)
                        {
                            throw new Exception("未找到属性" + propertyName);
                        }
                        properties[propertyName] = null;
                        return null;
                    }
                    if (p.CanRead == false)
                    {
                        if (throwException)
                        {
                            throw new Exception("属性" + propertyName + "无法读取数据");
                        }
                        properties[propertyName] = null;
                        return null;
                    }
                    ParameterInfo[] ps = p.GetIndexParameters();
                    if (ps != null && ps.Length > 0)
                    {
                        if (throwException)
                        {
                            throw new Exception("属性" + propertyName + "不得有参数");
                        }
                        properties[propertyName] = null;
                        return null;
                    }
                    properties[propertyName] = p;
                }
            }
            if (p == null)
            {
                if (throwException)
                {
                    throw new Exception("没有合适的属性" + propertyName);
                }
                return null;
            }
            else
            {
                if (throwException)
                {
                    return p.GetValue(instance, null);
                }
                else
                {
                    try
                    {
                        return p.GetValue(instance, null);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

       

        //private readonly static Dictionary<PropertyInfo, object> _PropertyDefaultValues
        //    = new Dictionary<PropertyInfo, object>();
        ///// <summary>
        ///// 获得指定属性的默认值
        ///// </summary>
        ///// <param name="p">属性对象</param>
        ///// <returns>默认值</returns>
        //public static object GetPropertyDefaultValue(PropertyInfo p)
        //{
        //    if (p == null)
        //    {
        //        throw new ArgumentNullException("p");
        //    }
        //    if (_PropertyDefaultValues.ContainsKey(p))
        //    {
        //        return _PropertyDefaultValues[p];
        //    }
        //    else
        //    {
        //        lock (_PropertyDefaultValues)
        //        {
        //            object v = null;
        //            DefaultValueAttribute dva = (DefaultValueAttribute)Attribute.GetCustomAttribute(
        //                p, typeof(DefaultValueAttribute), false);
        //            if (dva != null)
        //            {
        //                v = dva.Value;
        //            }
        //            else
        //            {
        //                v = GetDefaultValue(p.PropertyType);
        //            }
        //            _PropertyDefaultValues[p] = v;
        //            return v;
        //        }
        //    }
        //}

        private static readonly System.Collections.Hashtable _TypeDefaultValue = new Hashtable();
        /// <summary>
        /// 获得指定类型的默认值
        /// </summary>
        /// <param name="ValueType">数据类型</param>
        /// <returns>默认值</returns>
        public static object GetDefaultValue(Type ValueType)
		{
            if( ValueType == null )
            {
                throw new ArgumentNullException("ValueType");
            }
            lock (_TypeDefaultValue)
            {
                if (_TypeDefaultValue.ContainsKey(ValueType))
                {
                    return _TypeDefaultValue[ValueType];
                }
                if (_TypeDefaultValue.Count == 0 )
                {
                    _TypeDefaultValue[typeof(object)] = null;
                    _TypeDefaultValue[typeof(byte)] = (byte)0;
                    _TypeDefaultValue[typeof(sbyte)] = (sbyte)0;
                    _TypeDefaultValue[typeof(short)] = (short)0;
                    _TypeDefaultValue[typeof(ushort)] = (ushort)0;
                    _TypeDefaultValue[typeof(int)] = (int)0;
                    _TypeDefaultValue[typeof(uint)] = (uint)0;
                    _TypeDefaultValue[typeof(long)] = (long)0;
                    _TypeDefaultValue[typeof(ulong)] = (ulong)0;
                    _TypeDefaultValue[typeof(char)] = (char)0;
                    _TypeDefaultValue[typeof(float)] = (float)0;
                    _TypeDefaultValue[typeof(double)] = (double)0;
                    _TypeDefaultValue[typeof(decimal)] = (decimal)0;
                    _TypeDefaultValue[typeof(bool)] = false;
                    _TypeDefaultValue[typeof(string)] = null;
                    _TypeDefaultValue[typeof(DateTime)] = DateTime.MinValue;
                    _TypeDefaultValue[typeof(System.Drawing.Point)] = System.Drawing.Point.Empty;
                    _TypeDefaultValue[typeof(System.Drawing.PointF)] = System.Drawing.PointF.Empty;
                    _TypeDefaultValue[typeof(System.Drawing.Size)] = System.Drawing.Size.Empty;
                    _TypeDefaultValue[typeof(System.Drawing.SizeF)] = System.Drawing.SizeF.Empty;
                    _TypeDefaultValue[typeof(System.Drawing.Rectangle)] = System.Drawing.Rectangle.Empty;
                    _TypeDefaultValue[typeof(System.Drawing.RectangleF)] = System.Drawing.RectangleF.Empty;
                    _TypeDefaultValue[typeof(System.Drawing.Color)] = System.Drawing.Color.Transparent;
                    _TypeDefaultValue[typeof(System.IntPtr)] = IntPtr.Zero;
                    _TypeDefaultValue[typeof(System.UIntPtr)] = UIntPtr.Zero;
                }

                if (ValueType.IsEnum)
                {
                    // 处理枚举类型
                    Array vs = Enum.GetValues(ValueType);
                    if (vs != null && vs.Length > 0)
                    {
                        object v = vs.GetValue(0);
                        _TypeDefaultValue[ValueType] = v;
                        return v;
                    }
                    else
                    {
                        object v = System.Activator.CreateInstance(ValueType);
                        _TypeDefaultValue[ValueType] = v;
                        return v;
                    }
                }
                
                if (ValueType.IsValueType)
                {
                    object v = System.Activator.CreateInstance(ValueType);
                    _TypeDefaultValue[ValueType] = v;
                    return v;
                }
            }
			return null;
		}

        ///// <summary>
        ///// 试图进行数据类型转换
        ///// </summary>
        ///// <param name="Value">数值</param>
        ///// <param name="NewType">新数据类型</param>
        ///// <param name="Result">转换结果</param>
        ///// <returns>操作是否成功</returns>
        //public static bool TryConvertTo(object Value, Type NewType, ref object Result)
        //{
        //    if (NewType == null)
        //    {
        //        throw new ArgumentNullException("NewType");
        //    }
        //    var s = "JIEJIE.NET.SWITCH:-controlfow";
        //    if (Value == null || DBNull.Value.Equals(Value))
        //    {
        //        if (NewType.IsClass)
        //        {
        //            Result = null;
        //            return true;
        //        }
        //        return false;
        //    }

        //    Type ValueType = Value.GetType();

        //    if (ValueType.Equals(NewType) || ValueType.IsSubclassOf(NewType))
        //    {
        //        Result = Value;
        //        return true;
        //    }

        //    try
        //    {
        //        bool IsStringValue = ValueType.Equals(typeof(string));
        //        if (NewType.Equals(typeof(string)))
        //        {
        //            if (IsStringValue)
        //                Result = (string)Value;
        //            else
        //                Result = Convert.ToString(Value);
        //            return true;
        //        } 
        //        if (NewType.Equals(typeof(bool)))
        //        {
        //            if( IsStringValue )
        //            {
        //                bool bol = false;
        //                if ( TryParseBoolean((string)Value, out bol))
        //                {
        //                    Result = bol;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToBoolean(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(char)))
        //        {
        //            if (IsStringValue)
        //            {
        //                //char c = char.MinValue;
        //                string str = (string) Value;
        //                if (str != null && str.Length > 0)
        //                {
        //                    Result = str[0];
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToChar(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(byte)))
        //        {
        //            if (IsStringValue)
        //            {
        //                byte b = 0;
        //                if ( byte.TryParse((string)Value , out b ))
        //                {
        //                    Result = b;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToByte(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(sbyte)))
        //        {
        //            if (IsStringValue)
        //            {
        //                sbyte sb = 0;
        //                if ( sbyte.TryParse((string)Value , out sb ))
        //                {
        //                    Result = sb;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToSByte(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(short)))
        //        {
        //            if (IsStringValue)
        //            {
        //                short si = 0;
        //                if (Int16.TryParse((string)Value, out si))
        //                {
        //                    Result = si;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToInt16(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(ushort)))
        //        {
        //            if (IsStringValue)
        //            {
        //                ushort us = 0;
        //                if ( UInt16.TryParse((string)Value, out us))
        //                {
        //                    Result = us;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToUInt16(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(int)))
        //        {
        //            if (IsStringValue)
        //            {
        //                int i = 0;
        //                if (int.TryParse((string)Value, out i))
        //                {
        //                    Result = i;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToInt32(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(uint)))
        //        {
        //            if (IsStringValue)
        //            {
        //                uint ui = 0;
        //                if (UInt32.TryParse((string)Value, out ui))
        //                {
        //                    Result = ui;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToUInt32(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(long)))
        //        {
        //            if (IsStringValue)
        //            {
        //                long lng = 0;
        //                if( Int64.TryParse( ( string ) Value , out lng ))
        //                {
        //                    Result = lng ;
        //                    return true ;
        //                }
        //                else
        //                {
        //                    return false ;
        //                }
        //            }
        //            Result = Convert.ToInt64(Value);
        //            return true ;
        //        }
        //        if (NewType.Equals(typeof(ulong)))
        //        {
        //            if (IsStringValue)
        //            {
        //                ulong ulng = 0;
        //                if ( UInt64.TryParse((string)Value, out ulng))
        //                {
        //                    Result = ulng;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToUInt64(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(float)))
        //        {
        //            if (IsStringValue)
        //            {
        //                float f = 0;
        //                if ( float.TryParse((string)Value, out f))
        //                {
        //                    Result = f;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToSingle(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(double)))
        //        {
        //            if (IsStringValue)
        //            {
        //                double v = 0;
        //                if ( double.TryParse((string)Value, out v))
        //                {
        //                    Result = v;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToDouble(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(decimal)))
        //        {
        //            if (IsStringValue)
        //            {
        //                decimal v = 0;
        //                if (decimal.TryParse((string)Value, out v))
        //                {
        //                    Result = v;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToDecimal(Convert.ToSingle(Value));// .ToDecimal( v );
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(DateTime)))
        //        {
        //            if (IsStringValue)
        //            {
        //                DateTime v = DateTime.MinValue;
        //                if (DateTime.TryParse((string)Value, out v))
        //                {
        //                    Result = v;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = Convert.ToDateTime(Value);
        //            return true;
        //        }
        //        if (NewType.Equals(typeof(TimeSpan)))
        //        {
        //            if (IsStringValue)
        //            {
        //                TimeSpan v = TimeSpan.Zero;
        //                if (TimeSpan.TryParse((string)Value, out v))
        //                {
        //                    Result = v;
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            Result = new TimeSpan(Convert.ToInt64(Value));
        //            return true;
        //        }
        //        if( NewType.Equals( typeof( byte[])))
        //        {
        //            if( IsStringValue)
        //            {
        //                try
        //                {
        //                    var bs = Convert.FromBase64String((string)Value);
        //                    Result = bs;
        //                    return true;
        //                }
        //                catch
        //                {
        //                    return false;
        //                }
        //            }
        //            return false ;
        //        }
        //        if (NewType.IsEnum)
        //        {
        //            if (Enum.IsDefined(ValueType, Value))
        //            {
        //                if (IsStringValue)
        //                {
        //                    Result = Enum.Parse(NewType, (string)Value, true);
        //                }
        //                else
        //                {
        //                    Result = Enum.ToObject(NewType, Value);
        //                }
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        var converter = GetTypeConverter(NewType);
        //        if (converter != null)
        //        {
        //            if (converter.CanConvertFrom(Value.GetType()))
        //            {
        //                Result = converter.ConvertFrom(Value);
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        if (Value is System.IConvertible)
        //        {
        //            Result = ((System.IConvertible)Value).ToType(NewType, null);
        //            return true;
        //        }

        //        Result = Convert.ChangeType(Value, NewType);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

  //      /// <summary>
  //      /// 数据类型转换
  //      /// </summary>
  //      /// <param name="Value">要转换的数据</param>
  //      /// <param name="NewType">新数据类型</param>
  //      /// <param name="DefaultValue">默认值</param>
  //      /// <returns>转换结果</returns>
		//public static object ConvertTo( object Value , Type NewType , object DefaultValue )
		//{
		//	if( NewType == null )
		//	{
		//		throw new ArgumentNullException("NewType");
		//	}
		//	if( Value == null || DBNull.Value.Equals( Value ))
		//	{
		//		return DefaultValue ;
		//	}
            
  //          Type ValueType = Value.GetType();

		//	if( ValueType.Equals( NewType ) || ValueType.IsSubclassOf( NewType ))
		//	{
		//		return Value ;
		//	}

  //          if (NewType.Equals(typeof(string)))
  //          {
  //              return Convert.ToString(Value);
  //          }
  //          if (NewType.Equals(typeof(bool)))
  //          {
  //              if (Value is String)
  //              {
  //                  return bool.Parse((string)Value);
  //              }
  //              else
  //              {
  //                  return Convert.ToBoolean(Value);
  //              }
  //          }
  //          try
  //          {
  //              if (NewType.Equals(typeof(char)))
  //              {
  //                  return Convert.ToChar(Value);
  //              }
  //              if (NewType.Equals(typeof(byte)))
  //              {
  //                  return Convert.ToByte(Value);
  //              }
  //              if (NewType.Equals(typeof(sbyte)))
  //              {
  //                  return Convert.ToSByte(Value);
  //              }
  //              if (NewType.Equals(typeof(short)))
  //              {
  //                  return Convert.ToInt16(Value);
  //              }
  //              if (NewType.Equals(typeof(ushort)))
  //              {
  //                  return Convert.ToUInt16(Value);
  //              }
  //              if (NewType.Equals(typeof(int)))
  //              {
  //                  return Convert.ToInt32(Value);
  //              }
  //              if (NewType.Equals(typeof(uint)))
  //              {
  //                  return Convert.ToUInt32(Value);
  //              }
  //              if (NewType.Equals(typeof(long)))
  //              {
  //                  return Convert.ToInt64(Value);
  //              }
  //              if (NewType.Equals(typeof(ulong)))
  //              {
  //                  return Convert.ToUInt64(Value);
  //              }
  //              if (NewType.Equals(typeof(float)))
  //              {
  //                  return Convert.ToSingle(Value);
  //              }
  //              if (NewType.Equals(typeof(double)))
  //              {
  //                  return Convert.ToDouble(Value);
  //              }
  //              if (NewType.Equals(typeof(decimal)))
  //              {
  //                  decimal dec = Convert.ToDecimal(Convert.ToSingle(Value));// .ToDecimal( v );
  //                  return dec;
  //              }
  //              if( NewType.Equals( typeof( DateTime )))
  //              {
  //                  DateTime dtm = DateTime.MinValue ;
  //                  if (ValueType.Equals(typeof(string)))
  //                      dtm = DateTime.Parse((string)Value);
  //                  else
  //                      dtm = Convert.ToDateTime(Value);
  //                  return dtm;
  //              }
  //              if (NewType.Equals(typeof(TimeSpan)))
  //              {
  //                  TimeSpan span = TimeSpan.Zero;
  //                  if (ValueType.Equals(typeof(string)))
  //                      span = TimeSpan.Parse((string)Value);
  //                  else
  //                      span = TimeSpan.Parse(Convert.ToString(Value));
  //                  return span;
  //              }
  //              if (NewType.Equals(typeof(byte[])))
  //              {
  //                  if (ValueType.Equals(typeof(string)))
  //                  {
  //                      byte[] bs = Convert.FromBase64String((string)Value);
  //                      return bs;
  //                  }
  //                  return null;
  //              }

  //              if (NewType.IsEnum)
  //              {
  //                  if (Value is string)
  //                      return System.Enum.Parse(NewType, (string)Value);
  //                  else
  //                      return System.Enum.ToObject(NewType, Value);
  //              }

  //              var converter = GetTypeConverter(NewType);
  //              if (converter != null)
  //              {
  //                  if (converter.CanConvertFrom(Value.GetType()))
  //                  {
  //                      return converter.ConvertFrom(Value);
  //                  }
  //                  return DefaultValue;
  //              }
  //              if (Value is System.IConvertible)
  //              {
  //                  return ((System.IConvertible)Value).ToType(NewType, null);
  //              }

  //              return Convert.ChangeType(Value, NewType);
  //          }
  //          catch
  //          {
  //              return DefaultValue;
  //          }
		//}

  //      /// <summary>
  //      /// 数据类型转换
  //      /// </summary>
  //      /// <param name="Value">旧数据</param>
  //      /// <param name="NewType">新数据类型</param>
  //      /// <returns>转换结果</returns>
  //      public static object ConvertTo(object Value, Type NewType)
  //      {
  //          if (NewType == null)
  //          {
  //              throw new ArgumentNullException("NewType");
  //          }
  //          if (NewType.IsInstanceOfType(Value))
  //          {
  //              // 无需转换
  //              return Value;
  //          }
  //          // 判断是否是空白字符串
  //          bool emptyString = false;
  //          if (Value is string)
  //          {
  //              string s = (string)Value;
  //              if (s == null || s.Trim().Length == 0)
  //              {
  //                  emptyString = true;
  //              }
  //          }

  //          if (Value == null || DBNull.Value.Equals(Value))
  //          {
  //              if (NewType.IsClass)
  //              {
  //                  return null;
  //              }
  //              else
  //              {
  //                  return GetDefaultValue(NewType);
  //                  //throw new ArgumentNullException("Value");
  //              }
  //          }

  //          Type ValueType = Value.GetType();

  //          if (ValueType.Equals(NewType) || ValueType.IsSubclassOf(NewType))
  //          {
  //              return Value;
  //          }

  //          if (NewType.Equals(typeof(string)))
  //          {
  //              if (Value is double && DoubleNaN.IsNaN((double)Value))
  //              {
  //                  return null;
  //              }
  //              if (Value is float && float.IsNaN((float)Value))
  //              {
  //                  return null;
  //              }
  //              return Convert.ToString(Value);
  //          }
  //          if (NewType.Equals(typeof(bool)))
  //          {
  //              if (Value is String)
  //              {
  //                  if (emptyString)
  //                  {
  //                      return false;
  //                  }
  //                  else
  //                  {
  //                      return bool.Parse((string)Value);
  //                  }
  //              }
  //              else
  //              {
  //                  if (emptyString)
  //                      return false;
  //                  else
  //                      return Convert.ToBoolean(Value);
  //              }
  //          }
  //          if (NewType.Equals(typeof(char)))
  //          {
  //              return Convert.ToChar(Value);
  //          }
  //          if (NewType.Equals(typeof(byte)))
  //          {
  //              if (emptyString)
  //                  return (byte)0;
  //              else
  //                  return Convert.ToByte(Value);
  //          }
  //          if (NewType.Equals(typeof(sbyte)))
  //          {
  //              if (emptyString)
  //                  return (sbyte)0;
  //              else
  //                  return Convert.ToSByte(Value);
  //          }
  //          if (NewType.Equals(typeof(short)))
  //          {
  //              if (emptyString)
  //                  return (short)0;
  //              else
  //                  return Convert.ToInt16(Value);
  //          }
  //          if (NewType.Equals(typeof(ushort)))
  //          {
  //              if (emptyString)
  //                  return (ushort)0;
  //              else 
  //                  return Convert.ToUInt16(Value);
  //          }
  //          if (NewType.Equals(typeof(int)))
  //          {
  //              if (emptyString)
  //                  return (int)0;
  //              else
  //                  return Convert.ToInt32(Value);
  //          }
  //          if (NewType.Equals(typeof(uint)))
  //          {
  //              if (emptyString)
  //                  return (uint)0;
  //              else
  //                  return Convert.ToUInt32(Value);
  //          }
  //          if (NewType.Equals(typeof(long)))
  //          {
  //              if (emptyString)
  //                  return (long)0;
  //              else
  //                  return Convert.ToInt64(Value);
  //          }
  //          if (NewType.Equals(typeof(ulong)))
  //          {
  //              if (emptyString)
  //                  return (ulong)0;
  //              else
  //                  return Convert.ToUInt64(Value);
  //          }
  //          if (NewType.Equals(typeof(float)))
  //          {
  //              if (emptyString)
  //                  return (float)0;
  //              else
  //                  return Convert.ToSingle(Value);
  //          }
  //          if (NewType.Equals(typeof(double)))
  //          {
  //              if (emptyString)
  //                  return (double)0;
  //              else
  //                  return Convert.ToDouble(Value);
  //          }
  //          if (NewType.Equals(typeof(decimal)))
  //          {
  //              if (emptyString)
  //              {
  //                  return decimal.Zero;
  //              }
  //              else
  //              {
  //                  decimal dec = Convert.ToDecimal(Convert.ToSingle(Value));// .ToDecimal( v );
  //                  return dec;
  //              }
  //          }
  //          if (NewType.Equals(typeof(DateTime)))
  //          {
  //              if (emptyString)
  //              {
  //                  return DateTime.MinValue;
  //              }
  //              else
  //              {
  //                  DateTime dtm = DateTime.MinValue;
  //                  if (ValueType.Equals(typeof(string)))
  //                      dtm = DateTime.Parse((string)Value);
  //                  else
  //                      dtm = Convert.ToDateTime(Value);
  //                  return dtm;
  //              }
  //          }
  //          if (NewType.Equals(typeof(TimeSpan)))
  //          {
  //              if (emptyString)
  //              {
  //                  return TimeSpan.Zero;
  //              }
  //              else
  //              {
  //                  TimeSpan span = TimeSpan.Zero;
  //                  if (ValueType.Equals(typeof(string)))
  //                      span = TimeSpan.Parse((string)Value);
  //                  else
  //                      span = TimeSpan.Parse(Convert.ToString(Value));
  //                  return span;
  //              }
  //          }
  //          if (NewType.IsEnum)
  //          {
  //              if (Value is string)
  //              {
  //                  return DCEnumTypeInfo.StringToValue(NewType, (string)Value);
  //              }
  //              else
  //              {
  //                  return System.Enum.ToObject(NewType, Convert.ToInt32(Value));
  //              }
  //          }

  //          var converter = GetTypeConverter(NewType);
  //          if (converter != null)
  //          {
  //              if (converter.CanConvertFrom(Value.GetType()))
  //              {
  //                  return converter.ConvertFrom(Value);
  //              }
  //              else
  //              {
  //                  throw new ArgumentException("Value");
  //              }
  //          }
  //          if (Value is System.IConvertible)
  //          {
  //              return ((System.IConvertible)Value).ToType(NewType, null);
  //          }
  //          return Convert.ChangeType(Value, NewType);
  //      }
      
		///// <summary>
		///// 试图将字符串解释成布尔类型值
		///// </summary>
		///// <param name="Value">字符串值</param>
		///// <param name="Result">获得布尔类型值</param>
		///// <returns>操作是否成功</returns>
		//public static bool TryParseBoolean(string Value, out bool Result)
		//{
		//	Result = false;
		//	if ( Value != null)
		//	{
		//		Value = Value.Trim();
  //              if (Value == "0")
  //              {
  //                  Result = false;
  //                  return true;
  //              }
  //              if (Value == "1")
  //              {
  //                  Result = true;
  //                  return true;
  //              }
		//		if ( String.Compare( "True" , Value , StringComparison.OrdinalIgnoreCase ) == 0 )
		//		{
		//			Result = true;
		//			return true;
		//		}
		//		if ( String.Compare( "False" , Value , StringComparison.OrdinalIgnoreCase ) == 0 )
		//		{
		//			Result = false;
		//			return true;
		//		}
		//	}
		//	return false;
		//}
         
	}
}