using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
// 袁永福到此一游

namespace DCSoft.Common
{
    /// <summary>
    /// 枚举类型信息对象
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DCEnumTypeInfo
    {

        public static string FastToString( Type enumType , object v )
        {
            if( enumType == null )
            {
                throw new ArgumentNullException("enumType");
            }
            DCEnumTypeInfo info = GetInstance(enumType);
            if( info == null )
            {
                throw new NotSupportedException(enumType.FullName);
            }
            else
            {
                return info.GetName(v);
            }
        }
        private static readonly Dictionary<Type, DCEnumTypeInfo> _Instances = new Dictionary<Type, DCEnumTypeInfo>();
        /// <summary>
        /// 获得对象实例
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static DCEnumTypeInfo GetInstance( Type t )
        {
            if( t == null )
            {
                throw new ArgumentNullException("t");
            }
            if( t.IsEnum == false )
            {
                throw new ArgumentException(t.FullName);
            }
            DCEnumTypeInfo info = null;
            lock (_Instances)
            {
                if (_Instances.TryGetValue(t, out info) == false)
                {
                    info = new DCEnumTypeInfo(t);
                    _Instances.Add(t, info);
                }
            }
            return info;
        }

        private DCEnumTypeInfo ( Type t )
        {
            if( t == null )
            {
                throw new ArgumentNullException("t");
            }
            this._EnumType = t;
            if( Attribute.GetCustomAttribute( t , typeof( FlagsAttribute ) , false ) != null )
            {
                this._IsFlag = true;
            }
            this._Names = new Dictionary<string, object>();
            List<DCEnumItemInfo> items = new List<DCEnumItemInfo>();
            long maxValue = 0;
            foreach (FieldInfo f in t.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                DCEnumItemInfo item = new DCEnumItemInfo(Convert.ToInt64(f.GetValue(null)), f.GetValue(null), f.Name);
                if( string.Compare(f.Name , "all" , true ) == 0)
                {
                    this._AllItem = item;
                }
                items.Add(item);
                this._Names[f.Name] = f.GetValue(null);
                if (this._IsFlag)
                {
                    maxValue = maxValue | item.IntValue;
                }
                else
                {
                    if (item.IntValue > maxValue)
                    {
                        maxValue = item.IntValue;
                    }
                }
            }//foreach
            if (items.Count > 0)
            {
                this._DefaultValue = items[0].Value;
            }
            this._Values = items.ToArray();
            if (items.Count > 0)
            {
                // 可设置快速访问用的数组
                int len = Math.Min((int)maxValue + 1, MaxFastArraySize);
                if(len < 0 )
                {
                    // 可能超出INT32范围了。
                    len = MaxFastArraySize;
                }
                _FastValues = new DCEnumItemInfo[len];
                foreach (DCEnumItemInfo item in items)
                {
                    if (item.IntValue >= 0 && item.IntValue < len)
                    {
                        _FastValues[item.IntValue] = item;
                    }
                }
               
            }
        }
        private readonly DCEnumItemInfo _AllItem = null;

        private object _DefaultValue = null;

        private Type _EnumType = null;

        private readonly bool _IsFlag = false;
        /// <summary>
        /// 是否为可重叠的标记性的枚举类型
        /// </summary>
        public bool IsFlag
        {
            get
            {
                return this._IsFlag;
            }
        }
        private const int MaxFastArraySize = 257;
        /// <summary>
        /// 用于快速定位项目的数组
        /// </summary>
        private readonly DCEnumItemInfo[] _FastValues = null;
        /// <summary>
        /// 所有项目的数组
        /// </summary>
        private readonly DCEnumItemInfo[] _Values = null;
        ///// <summary>
        ///// 所有项目的数组
        ///// </summary>
        //internal DCEnumItemInfo[] Values
        //{
        //    get
        //    {
        //        return _Values;
        //    }
        //}
        private readonly Dictionary<string, object> _Names = null;

#if ! DCWriterForWASM
        private bool _HasLoadDescription = false;

        private string _Description = null;
        /// <summary>
        /// 附加的说明
        /// </summary>
        public string Description
        {
            get
            {
                if(_HasLoadDescription == false )
                {
                    _HasLoadDescription = true;
                    var dpa = (DescriptionAttribute)Attribute.GetCustomAttribute(this._EnumType, typeof(DescriptionAttribute), true);
                    if(dpa != null )
                    {
                        this._Description = dpa.Description;
                    }
                }
                return this._Description;
            }
        }
        /// <summary>
        /// 获得枚举值名称
        /// </summary>
        /// <param name="v">数值</param>
        /// <returns>名称</returns>
        public string GetName( object v )
        {
            if (this._AllItem != null && this._AllItem.Value.Equals(v))
            {
                return this._AllItem.Name;
            }
            if (this._FastValues != null)
            {
                int iv = (int)v;
                if (iv >= 0 && iv < this._FastValues.Length)
                {
                    if (this._FastValues[iv] != null)
                    {
                        return this._FastValues[iv].Name;
                    }
                    else
                    {
                        string name = v.ToString();
                        this._FastValues[iv] = new DCEnumItemInfo(iv, Enum.ToObject(this._EnumType, iv), name);
                        return name ;
                    }
                }
            }
            return v.ToString();
        }
#endif

        public object GetValue(string name)
        {
            if (name == null || name.Length == 0)
            {
                return this._DefaultValue;
            }
            if (this._Names != null)
            {
                object v = null;
                if (this._Names.TryGetValue(name, out v))
                {
                    return v;
                }
            }
            return Enum.Parse(this._EnumType, name);
        }
    }

    /// <summary>
    /// 枚举类型项目信息
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    internal class DCEnumItemInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="intValue">整数数值</param>
        /// <param name="V">数值</param>
        /// <param name="name">名称</param>
        /// <param name="desc">说明</param>
        public DCEnumItemInfo ( long intValue , object V , string name  )
        {
            this._IntValue = intValue;
            this._Value = V;
            this._Name = name;
            //this._Description = desc;
        }

        private readonly long _IntValue = 0;
        /// <summary>
        /// 整数数值
        /// </summary>
        public long IntValue
        {
            get
            {
                return this._IntValue;
            }
        }

        private readonly object _Value = null;
        /// <summary>
        /// 数值
        /// </summary>
        public object Value
        {
            get
            {
                return this._Value;
            }
        }

        private readonly string _Name = null;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._Name;
            }
        }

       // private readonly string _Description = null;
        public override string ToString()
        {
            return this.Name + "=" + this.IntValue;
        }
       
    }

}
