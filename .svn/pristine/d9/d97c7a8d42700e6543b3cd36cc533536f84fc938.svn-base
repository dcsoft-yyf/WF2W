using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Resources;
using System.IO;
using System.Globalization;
using System.Collections;
using System.ComponentModel;

namespace DCSoft.Common
{
    /// <summary>
    /// 类型成员描述信息帮助类型
    /// </summary>
    /// <remarks>袁永福到此一游</remarks>
    [System.Runtime.InteropServices.ComVisible(false)]
    public static class DCDescriptionHelper
    {
        /// <summary>
        /// 说明后缀
        /// </summary>
        public static readonly string DescriptionEndFix = "_Description";
        /// <summary>
        /// 获得成员说明
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>获得的成员说明</returns>
        public static string GetDescription(Type type, string memberName)
        {
            string name = FixName(type.FullName + "." + memberName);
            return GetValue(name, true);
        }

        /// <summary>
        /// 获得成员说明
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>获得的成员说明</returns>
        public static string GetDisplayName(Type type, string memberName)
        {
            string name = FixName(type.FullName + "." + memberName);
            return GetValue(name, false);
        }

        /// <summary>
        /// 修正名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FixName(string name)
        {
            if (name == null || name.Length == 0)// name == null || string.IsNullOrEmpty(name))
            {
                return name;
            }
            name = name.Replace('.', '_');
            return name;
        }


        //private static readonly Dictionary<string, string> _Values = new Dictionary<string, string>();
        internal static string GetValue(string name, bool isDesc)
        {
            if (name == null)
            {
                return null;
            }
            CheckDelayItems();
            DescriptionItem item = null;
            if( _Values.TryGetValue( name , out item ))
            {
                if (isDesc)
                {
                    return item.Description;
                }
                else
                {
                    return item.Name;
                }
            }
            return null;
        }

        internal static void ClearBuffer()
        {
            _Values.Clear();
            _ResLoaded.Clear();
            //_InitializedAssemblies.Clear();
        }

       
        /// <summary>
        /// 准备指定的类型
        /// </summary>
        /// <param name="t"></param>
        public static void PrepareType( Type t )
        {
            
          
        }

        private static readonly List<DelayItem> _DelayItems = new List<DelayItem>();

        private class DelayItem
        {
            public Assembly Asm = null;
            public string resName = null;
        }

        private static readonly List<string> _ResLoaded = new List<string>();

        private static void CheckDelayItems()
        {
            if(_DelayItems.Count > 0 )
            {
                lock( _DelayItems)
                {
                    foreach( var item in _DelayItems)
                    {
                        InnerLoadResource(item.Asm, item.resName);
                    }
                    _DelayItems.Clear();
                }
            }
        }

        private class DescriptionItem
        {
            public string Name = null;
            public string Description = null;
            public override string ToString()
            {
                return this.Name + " " + this.Description;
            }
        }
        private static readonly Dictionary<string, DescriptionItem> _Values = new Dictionary<string, DescriptionItem>();

        private static void InnerLoadResource(Assembly asm, string resourceName)
        {
            string fn = asm.FullName + "#" + resourceName;
            if (_ResLoaded.Contains(fn))
            {
                // 已经加载过了
                return;
            }
            _ResLoaded.Add(fn);
            try
            {

                var dics = EncryptResourceStringsHelper.BuildSortedDictionary(asm, resourceName);
                if (dics != null && dics.Count > 0)
                {
                    foreach (var item in dics)
                    {
                        if (item.Key.Length > 0 && item.Value != null && item.Value.Length > 0)
                        {
                            string name2 = null;
                            bool isDesc = false;
                            if (item.Key.EndsWith(DescriptionEndFix))
                            {
                                isDesc = true;
                                name2 = string.Intern(item.Key.Substring( 0 , item.Key.Length - DescriptionEndFix.Length));
                            }
                            else
                            {
                                name2 = string.Intern(item.Key);
                            }
                            DescriptionItem itemVs = null;
                            if (_Values.TryGetValue(name2, out itemVs) == false)
                            {
                                itemVs = new DescriptionItem();
                                _Values[name2] = itemVs;
                            }
                            if (isDesc)
                            {
                                itemVs.Description = item.Value;
                            }
                            else
                            {
                                itemVs.Name = string.Intern(item.Value);
                            }
                        }
                    }

                   
                }
            }
            catch (System.Exception ext)
            {
                //DCConsole.Default.WriteLineError(ext.ToString());
            }
        }
    }
    /// <summary>
    /// 显示名称特性
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public sealed class DCDisplayNameAttribute : DisplayNameAttribute
    {
        
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public DCDisplayNameAttribute(Type type, string memberName)
        {
            this._Type = type;
            this._MemberName = memberName;
        }

        private readonly Type _Type = null;
        private readonly string _MemberName = null;
        public string InnerMemberName
        {
            get
            {
                return _MemberName;
            }
        }
         

        private bool _Initalized = false;
        private void CheckInitalized()
        {
#if WINFORM || DCWriterForWinFormNET6
            if (_Initalized == false)
            {
                _Initalized = true;
                this._Value = DCDescriptionHelper.GetDisplayName(this._Type, this._MemberName);

               
            }
#else
            this._Value = this._ResourceItemName;
#endif
        }


        private readonly string _ResourceItemName = null;
        /// <summary>
        /// 内部的名称
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ResourceItemName
        {
            get
            {
                CheckInitalized();
                return _ResourceItemName;
            }
        }

        private string _Value = null;
        internal string _RawValue = null;
        /// <summary>
        /// 获得的显示名称
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override string DisplayName
        {
            get
            {

#if WINFORM || DCWriterForWinFormNET6
                if (DCDescriptionPropoertyDescriptor.ShowLocalizationDisplayName)
                {
                    if( _RawValue != null && _RawValue.Length > 0 )
                    {
                        return _RawValue;
                    }
                    this.CheckInitalized();
                    if (this._Value == null)
                    {
                        this._Value = this._MemberName;
                    }
                    if (this._Value == null)
                    {
                        this._Value = string.Empty;
                    }
                    return _Value;
                }
                else
#endif
                {
                    string name = this._MemberName;
                    if (name == null)
                    {
                        name = string.Empty;
                    }
                    return name;
                }
            }
        }
        public override string ToString()
        {
            return this.DisplayName;
        }
    }

    /// <summary>
    /// 成员说明特性
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public sealed class DCDescriptionAttribute : DescriptionAttribute
    {
        ///// <summary>
        ///// 初始化对象
        ///// </summary>
        //public DCDescriptionAttribute()
        //{
        //}

        private static bool _ShowMemberName = false;
        /// <summary>
        /// 显示成员名称
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static bool ShowMemberName
        {
            get { return _ShowMemberName; }
            set { _ShowMemberName = value; }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public DCDescriptionAttribute(Type type, string memberName)
        {
            this._Type = type;
            this._MemberName = memberName;
        }

        private bool _CheckMemberExist = true;
        /// <summary>
        /// 检查类型成员是否存在
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool CheckMemberExist
        {
            get
            {
                return _CheckMemberExist;
            }
            set
            {
                _CheckMemberExist = value;
            }
        }

        private readonly Type _Type = null;
        private readonly string _MemberName = null;
        public string InnerMemberName
        {
            get
            {
                return _MemberName;
            }
        }
        private bool _Initalized = false;
        private void CheckInitalized()
        {
#if WINFORM || DCWriterForWinFormNET6
            if (_Initalized == false)
            {
                _Initalized = true;
                this._Value = DCDescriptionHelper.GetDescription(this._Type, this._MemberName);

               
                if (this._Value == null)
                {
                    this._Value = string.Empty;
                }
            }
#endif
        }

        private readonly string _ResourceItemName = null;
        /// <summary>
        /// 内部的名称
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ResourceItemName
        {
            get
            {
                CheckInitalized();
                return _ResourceItemName;
            }
        }

        private string _Value = null;
        /// <summary>
        /// 说明文字
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override string Description
        {
            get
            {
                if( _RawValue != null && _RawValue.Length > 0 )
                {
                    return _RawValue;
                }
                CheckInitalized();
                if (this._Value == null)
                {
                    this._Value = string.Empty;
                }
                return this._Value;
            }
        }

        internal string _RawValue = null;

        public override string ToString()
        {
            return this.Description;
        }
    }

    

    /// <summary>
    /// 支持资源的属性描述对象
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DCDescriptionPropoertyDescriptor : System.ComponentModel.PropertyDescriptor
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="member"></param>
        public DCDescriptionPropoertyDescriptor(PropertyDescriptor member)
            : base(member)
        {
            myBaseProperty = (PropertyDescriptor)member;
        }

        private static bool _ShowLocalizationDisplayName = true;
        /// <summary>
        /// 是否显示本地化名称
        /// </summary>
        public static bool ShowLocalizationDisplayName
        {
            get
            {
                return _ShowLocalizationDisplayName;
            }
            set
            {
                if (_ShowLocalizationDisplayName != value)
                {
                    _ShowLocalizationDisplayName = value;
                    DCDescriptionHelper.ClearBuffer();
                }
            }
        }

        private readonly PropertyDescriptor myBaseProperty = null;

        /// <summary>
        /// 添加ValueChanged事件句柄
        /// </summary>
        /// <param name="component"></param>
        /// <param name="handler"></param>
        public override void AddValueChanged(object component, EventHandler handler)
        {
            myBaseProperty.AddValueChanged(component, handler);
        }
      
        /// <summary>
        /// 属性
        /// </summary>
        public override AttributeCollection Attributes
        {
            get
            {
                return myBaseProperty.Attributes;
            }
        }
        /// <summary>
        /// 能否重置数值
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool CanResetValue(object component)
        {
            return myBaseProperty.CanResetValue(component);
        }
        /// <summary>
        /// 分组
        /// </summary>
        public override string Category
        {
            get
            {
                return myBaseProperty.Category;
            }
        }
        /// <summary>
        /// 组件类型
        /// </summary>
        public override Type ComponentType
        {
            get { return myBaseProperty.ComponentType; }
        }
        /// <summary>
        /// 属性值转换器
        /// </summary>
        public override TypeConverter Converter
        {
            get
            {
                return myBaseProperty.Converter;
            }
        }

        /// <summary>
        /// 说明
        /// </summary>
        public override string Description
        {
            get
            {
                string desc = DCDescriptionHelper.GetDescription(myBaseProperty.ComponentType, this.Name);
                if (desc != null && desc.Length > 0)// string.IsNullOrEmpty(desc) == false)
                {
                    return desc;
                }
                return myBaseProperty.Description;
            }
        }
        /// <summary>
        /// 运行时属性
        /// </summary>
        public override bool DesignTimeOnly
        {
            get
            {
                return myBaseProperty.DesignTimeOnly;
            }
        }
        /// <summary>
        /// 显示的名称
        /// </summary>
        public override string DisplayName
        {
            get
            {
                string name = myBaseProperty.DisplayName;
                if (_ShowLocalizationDisplayName)
                {
                    name = DCDescriptionHelper.GetDisplayName(myBaseProperty.ComponentType, this.Name);
                }
                if (name == null || name.Length == 0)// string.IsNullOrEmpty(name))
                {
                    name = myBaseProperty.DisplayName;
                }
                return name;
            }
        }
        
        /// <summary>
        /// 获得子属性
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
        {
            return myBaseProperty.GetChildProperties(instance, filter);
        }
        /// <summary>
        /// 获得数值编辑器
        /// </summary>
        /// <param name="editorBaseType"></param>
        /// <returns></returns>
        public override object GetEditor(Type editorBaseType)
        {
            return myBaseProperty.GetEditor(editorBaseType);
        }
        //protected override object GetInvocationTarget(Type type, object instance)
        //{
        //    return myBaseProperty.GetInvocationTarget(type, instance);
        //}
        /// <summary>
        /// 获得属性值
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override object GetValue(object component)
        {
            return myBaseProperty.GetValue(component);
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public override bool IsBrowsable
        {
            get
            {
                return myBaseProperty.IsBrowsable;
            }
        }
        /// <summary>
        /// 是否本地化
        /// </summary>
        public override bool IsLocalizable
        {
            get
            {
                return myBaseProperty.IsLocalizable;
            }
        }
        /// <summary>
        /// 是否只读
        /// </summary>
        public override bool IsReadOnly
        {
            get { return myBaseProperty.IsReadOnly; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public override string Name
        {
            get
            {
                return myBaseProperty.Name;
            }
        }
        //protected override int NameHashCode
        //{
        //    get
        //    {
        //        return myBaseProperty.NameHashCode;
        //    }
        //}
        /// <summary>
        /// 触发ValueChanged事件
        /// </summary>
        /// <param name="component"></param>
        /// <param name="e"></param>
        protected override void OnValueChanged(object component, EventArgs e)
        {
            //myBaseProperty.OnValueChanged(component, e);
        }
        /// <summary>
        /// 属性值数据类型
        /// </summary>
        public override Type PropertyType
        {
            get { return myBaseProperty.PropertyType; }
        }
        /// <summary>
        /// 去除删除数值事件句柄
        /// </summary>
        /// <param name="component"></param>
        /// <param name="handler"></param>
        public override void RemoveValueChanged(object component, EventHandler handler)
        {
            myBaseProperty.RemoveValueChanged(component, handler);
        }
        /// <summary>
        /// 重置属性值
        /// </summary>
        /// <param name="component"></param>
        public override void ResetValue(object component)
        {
            myBaseProperty.ResetValue(component);
        }
        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public override void SetValue(object component, object value)
        {
            myBaseProperty.SetValue(component, value);
        }
        /// <summary>
        /// 是否序列化数值
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool ShouldSerializeValue(object component)
        {
            return myBaseProperty.ShouldSerializeValue(component);
        }
    }
}
