using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;

namespace DCSoft.Common
{
    public static class SafeCreateResourceManager
    {
        internal static System.Resources.ResourceManager Create(string resName, Assembly asm)
        {
            if (resName == null || resName.Length == 0)
            {
                throw new ArgumentNullException("resName");
            }
            var name = DCResourceManager.FixManifestResourceName(resName + ".resources", asm);
            if (name != null)
            {
                return new System.Resources.ResourceManager(name.Substring(0, name.Length - 10), asm);
            }
            return null;
        }
    }

    /// <summary>
    /// 对字符串资源的一个包装
    /// </summary>
    /// <remarks >
    /// 袁永福到此一游 2016-2-25
    /// </remarks>
    [System.Runtime.InteropServices.ComVisible( false )]
    internal class DCResourceManager : System.Resources.ResourceManager
    {
        public static string FixManifestResourceName(string resName, Assembly asm)
        {
            if (resName == null || resName.Length == 0)
            {
                throw new ArgumentNullException("resName");
            }
            if (asm.GetManifestResourceInfo(resName) != null)
            {
                return resName;
            }
            var resNameItems = resName.Split('.');

            int index = resName.LastIndexOf('.');
            string strShortName = resName;
            if (index > 0)
            {
                strShortName = resName.Substring(index + 1);
            }
            var names = asm.GetManifestResourceNames();
            foreach (var item in names)
            {
                //if(item == "DCSoft.DCWriterForWinForm.WASM.test.xml")
                //{

                //}
                var itemNames = item.Split('.');
                int currentIndex = resNameItems.Length - 1;
                for (int iCount = itemNames.Length - 1; iCount >= 0; iCount--)
                {
                    if (itemNames[iCount] == resNameItems[currentIndex])
                    {
                        currentIndex--;
                        if (currentIndex < 0)
                        {
                            break;
                        }
                    }
                }
                if (currentIndex < 0)
                {
                    return item;
                }
            }
            return null;
        }

        public static System.IO.Stream SafeGetManifestResourceStream( string resName , Assembly asm )
        {
            if (resName == null || resName.Length == 0)
            {
                throw new ArgumentNullException("resName");
            }
            var name = FixManifestResourceName(resName, asm);
            if(name != null )
            {
                return asm.GetManifestResourceStream(name);
            }
            return null;
        }
        

#if ! DCWriterForWASM
        /// <summary>
        /// 安全的从程序集资源中加载文本内容
        /// </summary>
        /// <param name="resName">资源名称</param>
        /// <param name="asm">程序集对象</param>
        /// <param name="encoding">文本编码格式</param>
        /// <returns>加载在文本内容</returns>
        public static string SafeLoadResourceTextFile( string resName , Assembly asm , System.Text.Encoding encoding )
        {
            if( asm == null )
            {
                throw new ArgumentNullException("asm");
            }
            var name = FixManifestResourceName(resName, asm);
            if (name != null )
            {
                using (var reader = new System.IO.StreamReader(asm.GetManifestResourceStream(name), encoding, true))
                {
                    return reader.ReadToEnd();
                }
            }
            return null;
        }
        public static bool ReleaseResourceContainer( Type t )
        {
            if( t == null )
            {
                throw new ArgumentNullException("t");
            }
            foreach( var field in t.GetFields( BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public ))
            {
                if( field.FieldType == typeof ( System.Resources.ResourceManager))
                {
                    var rm = (System.Resources.ResourceManager)field.GetValue(null);
                    if(rm != null )
                    {
                        rm.ReleaseAllResources();
                        field.SetValue(null, null);
                        return true;
                    }
                    break;
                }
            }
            return false;
        }
        private readonly static Dictionary<string, ResourceManager> _Mans = new Dictionary<string, ResourceManager>();
        
       
        private DCResourceManager( System.Resources.ResourceManager man )
        {
            if (man == null)
            {
                throw new ArgumentNullException("man");
            }
            this._BaseManager = man;
        }

        private readonly System.Resources.ResourceManager _BaseManager = null;
        public override string BaseName
        {
            get
            {
                return this._BaseManager.BaseName;
            }
        }

        //public override ResourceSet GetResourceSet(System.Globalization.CultureInfo culture, bool createIfNotExists, bool tryParents)
        //{
        //    return this._BaseManager.GetResourceSet(culture, createIfNotExists, tryParents);
        //}

        public override bool IgnoreCase
        {
            get
            {
                return this._BaseManager.IgnoreCase;
            }
            set
            {
                this._BaseManager.IgnoreCase = value;
            }
        }

        public override void ReleaseAllResources()
        {
            this._BaseManager.ReleaseAllResources();
        }
        public override Type ResourceSetType
        {
            get
            {
                return this._BaseManager.ResourceSetType;
            }
        }

        public override object GetObject(string name)
        {
            return this._BaseManager.GetObject(name);
        }

        public override object GetObject(string name, System.Globalization.CultureInfo culture)
        {
            return this._BaseManager.GetObject(name, culture);
        }
         
        public override string GetString(string name)
        {
            if (IsProtectedResourceName(name) == false)
            {
                if (_CustomStrings.ContainsKey(name))
                {
                    return _CustomStrings[name];
                }
            }
            return this._BaseManager.GetString(name);
        }

        public override string GetString(string name, System.Globalization.CultureInfo culture)
        {
            if (IsProtectedResourceName(name) == false)
            {
                if (_CustomStrings.ContainsKey(name))
                {
                    return _CustomStrings[name];
                }
            }
            return this._BaseManager.GetString(name, culture);
        }
        private readonly static Dictionary<string, string> _CustomStrings = new Dictionary<string, string>();
        private readonly static List<string> _ProtectedResourceNames = new List<string>();
        /// <summary>
        /// 添加受保护的资源名称
        /// </summary>
        /// <param name="name"></param>
        public static void AddProtectedResourceName(string name)
        {
            if (_ProtectedResourceNames.Contains(name) == false)
            {
                _ProtectedResourceNames.Add(name);
            }
        }

        /// <summary>
        /// 判断是否为受保护的资源名称 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool IsProtectedResourceName(string name)
        {
            if ( name == null || name.Length == 0 )// string.IsNullOrEmpty(name))
            {
                return false;
            }
            if (_ProtectedResourceNames.Count > 0)
            {
                foreach (string name2 in _ProtectedResourceNames)
                {
                    if (name2 == name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static List<string> GetAllStringValues()
        {
            List<PropertyInfo> ps = new List<PropertyInfo>();
            foreach (Type t in _StringProperties.Keys)
            {
                List<PropertyInfo> names = _StringProperties[t];
                ps.AddRange(names);
            }
            ps.Sort( new PropertyNameComparer());
            List<string> result = new List<string>();
            string lastName = null;
            foreach (PropertyInfo item in ps)
            {
                if (item.Name != lastName)
                {
                    lastName = item.Name;
                    if (IsProtectedResourceName(item.Name) == false)
                    {
                        result.Add(item.Name);
                        result.Add(Convert.ToString(item.GetValue(null, null)));
                    }
                }
            }
            return result;
        }
        private class PropertyNameComparer : IComparer<PropertyInfo >
        {
            public int Compare(PropertyInfo x, PropertyInfo y)
            {
                return string.Compare(x.Name, y.Name);
            }
        }
        /// <summary>
        /// 设置自定义的字符串资源值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="text">值</param>
        public static void SetCustomStringValue(string name, string text)
        {
            if (IsProtectedResourceName(name))
            {
                return;
            }
            if ( name != null && name.Length > 0 )// string.IsNullOrEmpty(name) == false)
            {
                if (text == null || text.Length == 0 )// string.IsNullOrEmpty(text))
                {
                    if (_CustomStrings.ContainsKey(name))
                    {
                        _CustomStrings.Remove(name);
                    }
                }
                else
                {
                    _CustomStrings[name] = text;
                }
            }
        }
        /// <summary>
        /// 修改字符串资源容器类型
        /// </summary>
        /// <param name="resourceContainerType">资源类型</param>
        public static void ChangeSourceManager(Type resourceContainerType )
        {
            if (resourceContainerType == null)
            {
                throw new ArgumentNullException("resourceContainerType");
            }
           
            if (_StringProperties.ContainsKey(resourceContainerType))
            {
                // 处理过了
                return;
            }
            foreach (PropertyInfo p in resourceContainerType.GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (p.Name == "ResourceManager" && p.PropertyType.Equals(typeof(System.Resources.ResourceManager)))
                {
                    object v = p.GetValue(null , null );
                    foreach (FieldInfo f in resourceContainerType.GetFields(
                         BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                    {
                        if (f.FieldType.Equals(typeof(System.Resources.ResourceManager)))
                        {
                            object fv = f.GetValue(null);
                            DCResourceManager newValue = new DCResourceManager((ResourceManager)fv);
                            f.SetValue( null , newValue );
                            List<PropertyInfo> names = new List<PropertyInfo>();
                            foreach (PropertyInfo p2 in resourceContainerType.GetProperties(
                                BindingFlags.Public 
                                | BindingFlags.NonPublic 
                                | BindingFlags.Static))
                            {
                                if (p2.CanRead && p2.PropertyType.Equals(typeof(string)))
                                {
                                    names.Add(p2);
                                }
                            }
                            _StringProperties[resourceContainerType] = names;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private readonly static Dictionary<Type, List<PropertyInfo>> _StringProperties
            = new Dictionary<Type, List<PropertyInfo>>();
#endif
    }
}
