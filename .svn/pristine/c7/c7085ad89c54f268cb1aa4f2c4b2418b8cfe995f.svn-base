using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing;

namespace DCSoft.Common
{
    [System.Runtime.InteropServices.ComVisible(false)]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public static class XMLSerializeHelper
    {
//#if ! DCWriterForWASM
//        ///// <summary>
//        ///// 获得可以参与XML序列化的属性列表
//        ///// </summary>
//        ///// <param name="t">类型</param>
//        ///// <param name="declaredOnly">是否只获得类型内部定义的属性</param>
//        ///// <returns>属性信息列表</returns>
//        //public static List<PropertyInfo> GetSerializableProperties( Type t, bool declaredOnly)
//        //{
//        //    if( t == null )
//        //    {
//        //        throw new ArgumentNullException("t");
//        //    }
//        //    List<PropertyInfo> list = new List<PropertyInfo>();
//        //    var flag = BindingFlags.Public | BindingFlags.Instance;
//        //    if(declaredOnly)
//        //    {
//        //        flag = flag | BindingFlags.DeclaredOnly;
//        //    }
//        //    var ps = t.GetProperties(flag);
//        //    foreach( var p in ps )
//        //    {
//        //        if( p.CanRead == false || p.CanWrite == false )
//        //        {
//        //            continue;
//        //        }
//        //        if( p.IsSpecialName )
//        //        {
//        //            continue;
//        //        }
//        //        if( Attribute.GetCustomAttribute( p , typeof( XmlIgnoreAttribute) , true ) != null )
//        //        {
//        //            continue;
//        //        }
//        //        if( Attribute.GetCustomAttribute( p , typeof( System.ObsoleteAttribute) , true ) != null )
//        //        {
//        //            continue;
//        //        }
//        //        if( XmlReader.IsName( p.Name ) == false )
//        //        {
//        //            continue;
//        //        }
//        //        var pss = p.GetIndexParameters();
//        //        if( pss != null && pss.Length > 0 )
//        //        {
//        //            continue;
//        //        }
//        //        list.Add(p);
//        //    }
//        //    list.Sort(new PropertyNameCompare());
//        //    return list;
//        //}


//        //private class PropertyNameCompare : System.Collections.Generic.IComparer<PropertyInfo>
//        //{
//        //    public int Compare(PropertyInfo x, PropertyInfo y)
//        //    {
//        //        return string.Compare(x.Name, y.Name);
//        //    }
//        //}
//#endif
        private readonly static Dictionary<System.Reflection.PropertyInfo, bool> _HasXmlIgnoreAttribute 
            = new Dictionary<PropertyInfo, bool>();
        ///// <summary>
        ///// 判断属性是否标记了XmlIgnoreAttribute特性
        ///// </summary>
        ///// <param name="p"></param>
        ///// <returns></returns>
        //public static bool HasXmlIgnoreAttribute(System.Reflection.PropertyInfo p)
        //{
        //    if (p == null)
        //    {
        //        throw new ArgumentNullException("p");
        //    }
        //    if (_HasXmlIgnoreAttribute.ContainsKey(p))
        //    {
        //        return _HasXmlIgnoreAttribute[p];
        //    }
        //    if (Attribute.GetCustomAttribute(p, typeof(System.Xml.Serialization.XmlIgnoreAttribute), true) == null)
        //    {
        //        _HasXmlIgnoreAttribute[p] = false;
        //        return false;
        //    }
        //    else
        //    {
        //        _HasXmlIgnoreAttribute[p] = true;
        //        return true;
        //    }
        //}
       
        private static readonly string _X2 = "X2";
        private static readonly int _ARGB_BLACK = Color.Black.ToArgb();
        private static readonly string _Black_String = "#000000";
        private static readonly int _ARGB_WHITE = Color.White.ToArgb();
        private static readonly string _White_String = "#FFFFFF";
        private static readonly int _ARGB_Gray = Color.Gray.ToArgb();
        private static readonly string _Gray_String = "#" + Color.Gray.R.ToString(_X2) 
            + Color.Gray.G.ToString(_X2) 
            + Color.Gray.B.ToString(_X2);
        public static string ColorToString(Color c)
        {
            if (c.A == 255)
            {
                var argb = c.ToArgb();
                if( argb == _ARGB_BLACK )
                {
                    return _Black_String;
                }
                if( argb == _ARGB_WHITE)
                {
                    return _White_String;
                }
                if(argb == _ARGB_Gray)
                {
                    return _Gray_String;
                }
                string v = '#' + c.R.ToString(_X2) + c.G.ToString(_X2) + c.B.ToString(_X2);
                return v;
            }
            else
            {
                string v = '#' + c.A.ToString(_X2) + c.R.ToString(_X2) + c.G.ToString(_X2) + c.B.ToString(_X2);
                return v;
            }
        }

        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static string ColorToString(Color c, Color defaultValue)
        {
            if (c.ToArgb() == defaultValue.ToArgb())
            {
                return null;
            }
            else
            {
                if (c.A == 255)
                {
                    var argb = c.ToArgb();
                    if (argb == _ARGB_BLACK)
                    {
                        return _Black_String;
                    }
                    if (argb == _ARGB_WHITE)
                    {
                        return _White_String;
                    }
                    if (argb == _ARGB_Gray)
                    {
                        return _Gray_String;
                    }
                    string v = '#' + c.R.ToString(_X2) + c.G.ToString(_X2) + c.B.ToString(_X2);
                    return v;
                }
                else
                {
                    string v = '#' +  c.A.ToString(_X2) + c.R.ToString(_X2) + c.G.ToString(_X2) + c.B.ToString(_X2);
                    return v;
                }
            }
        }

        private static int Parse2HexValue(string txt, int startIndex)
        {
            int result = 0;
            int index = StringCommon.IndexOfHexChar(txt[startIndex]);
            if (index > 0)
            {
                result = index << 4;
            }
            index = StringCommon.IndexOfHexChar(txt[startIndex + 1]);
            if (index > 0)
            {
                result += index;
            }
            return result;
        }

        private static readonly string _Color_00000000 = "#00000000";
        private static readonly string _Color_00ffffff = "#00ffffff";
        private static readonly string _Color_000000 = "#000000";
        private static readonly string _Color_ffffff = "#ffffff";
        private static readonly string _Empty = "Empty";

        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Color StringToColor(string v, Color defaultValue)
        {
            if (v == null || v.Length == 0)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    if (v[0] == '#')
                    {
                        if (v.Length == 9)
                        {
                            if (v == _Color_00000000)
                            {
                                return Color.Empty;
                            }
                            if (string.Equals(v, _Color_00ffffff, StringComparison.OrdinalIgnoreCase))
                            {
                                return Color.Transparent;
                            }
                            int a = Parse2HexValue(v, 1);
                            int r = Parse2HexValue(v, 3);
                            int g = Parse2HexValue(v, 5);// int.Parse(v.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                            int b = Parse2HexValue(v, 7);// int.Parse(v.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
                            Color c = Color.FromArgb(a, r, g, b);
                            return c;
                        }
                        else if (v.Length == 7)
                        {
                            if (v == _Color_000000)
                            {
                                return Color.Black;
                            }
                            if (string.Equals(v, _Color_ffffff, StringComparison.OrdinalIgnoreCase))
                            {
                                return Color.White;
                            }
                            int r = Parse2HexValue(v, 1);// int.Parse(v.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                            int g = Parse2HexValue(v, 3);// int.Parse(v.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                            int b = Parse2HexValue(v, 5);// int.Parse(v.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                            Color c = Color.FromArgb(r, g, b);
                            return c;
                        }
                    }
                    if( v.Length > 10 && v.StartsWith("rgb(", StringComparison.OrdinalIgnoreCase) )
                    {
                        v = v.Substring(4, v.Length - 5);
                        if (v.IndexOf(',') > 0)
                        {
                            string[] items = v.Split(',');
                            var vs = new int[4];
                            var vsCount = 0;
                            foreach (string item in items)
                            {
                                int iv = 0;
                                if (int.TryParse(item.Trim(), out iv))
                                {
                                    vs[vsCount++] = iv;
                                    if (vsCount == 4)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (vsCount == 1)
                            {
                                return Color.FromArgb(vs[0], 255, 255);
                            }
                            else if (vsCount == 2)
                            {
                                return Color.FromArgb(vs[0], vs[1], 255);
                            }
                            else if (vsCount == 3)
                            {
                                return Color.FromArgb(vs[0], vs[1], vs[2]);
                            }
                        }
                    }
                    else if (v.Length > 10 && v.StartsWith("rgba(", StringComparison.OrdinalIgnoreCase))
                    {
                        v = v.Substring(5, v.Length - 6);
                        if (v.IndexOf(',') > 0)
                        {
                            string[] items = v.Split(',');
                            if (items.Length == 4)
                            {
                                var a = 0f;
                                var r = 0;
                                var g = 0;
                                var b = 0;
                                if(int.TryParse( items[0], out r )
                                    && int.TryParse(items[1] , out g )
                                    && int.TryParse(items[2] , out b )
                                    && float.TryParse( items[3] , out a ))
                                {
                                    return Color.FromArgb((int)(a * 255), r, g, b);
                                }
                            }
                        }
                    }
                    if (v == _Empty || string.Equals(v, _Empty, StringComparison.OrdinalIgnoreCase))
                    {
                        return Color.Empty;
                    }
                    Color c2 = ColorTranslator.FromHtml(v);
                    return c2;
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        
#if WINFORM || DCWriterForWinFormNET6
        //public static bool ReadSinglePropertyValue(XmlReader reader, object instance )
        //{
        //    if (reader == null)
        //    {
        //        throw new ArgumentNullException("reader");
        //    }
        //    if (instance == null)
        //    {
        //        throw new ArgumentNullException("instance");
        //    }
        //    string propertyName = reader.LocalName;
        //    if (string.IsNullOrEmpty(propertyName))
        //    {
        //        throw new ArgumentNullException("reader.LocalName");
        //    }
        //    string txt = reader.Value;
        //    if (reader.NodeType == XmlNodeType.Element)
        //    {
        //        txt = reader.ReadString();
        //    }
        //    PropertyInfo p = instance.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        //    if (p == null)
        //    {
        //        throw new ArgumentOutOfRangeException( instance.GetType().FullName + "没有属性" + propertyName);
        //    }
        //    object v = null;
        //    if( p.PropertyType.Equals( typeof( DateTime )))
        //    {
        //        v = ToDateTime( txt );
        //    }
        //    else if( p.PropertyType.Equals( typeof( Color )))
        //    {
        //        v = ToColor( txt );
        //    }
        //    else
        //    {
        //        v = Convert.ChangeType(txt, p.PropertyType);
        //    }
        //    p.SetValue( instance , v , null );
        //    return true; 
        //}
        //public static int ReadAllAttributeValue(XmlReader reader, object instance)
        //{
        //    int result = 0;
        //    if (reader.MoveToFirstAttribute())
        //    {
        //        do
        //        {
        //            if (ReadSinglePropertyValue(reader, instance))
        //            {
        //                result++;
        //            }
        //        }
        //        while (reader.MoveToNextAttribute());
        //    }
        //    return result;
        //}

        //public static bool WriteAttributeString(XmlWriter writer, object instance, string propertyName)
        //{
        //    return WritePropertyValue(writer, instance, propertyName, true);
        //}
        //public static bool WriteElementString(XmlWriter writer, object instance, string propertyName)
        //{
        //    return WritePropertyValue(writer, instance, propertyName, false);
        //}
        //private static bool WritePropertyValue(XmlWriter writer, object instance, string propertyName, bool attributeType)
        //{
        //    if (writer == null)
        //    {
        //        throw new ArgumentNullException("writer");
        //    }
        //    if (instance == null)
        //    {
        //        throw new ArgumentNullException("instance");
        //    }
        //    if (string.IsNullOrEmpty(propertyName))
        //    {
        //        throw new ArgumentNullException("propertyName");
        //    }
        //    PropertyInfo p = instance.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        //    object v = p.GetValue(instance, null);
        //    if (v != null)
        //    {
        //        object dv = ValueTypeHelper.GetPropertyDefaultValue(p);
        //        if (v.Equals(dv) == false)
        //        {
        //            string txt = ToString(v);
        //            if (string.IsNullOrEmpty(txt) == false)
        //            {
        //                if (attributeType)
        //                {
        //                    writer.WriteAttributeString(propertyName, txt);
        //                }
        //                else
        //                {
        //                    writer.WriteElementString(propertyName, txt);
        //                }
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
         //private static readonly string _DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        //public static DateTime ToDateTime(string txt)
        //{
        //    DateTime dtm = DateTime.MinValue;
        //    if (DateTime.TryParseExact(txt, _DateTimeFormat, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out dtm))
        //    {
        //        return dtm;
        //    }
        //    return DateTime.MinValue;
        //}
        //public static Color ToColor(string txt)
        //{
        //    return ColorTranslator.FromHtml(txt);
        //}
        //public static string ToString(object v)
        //{
        //    if (v == null || DBNull.Value.Equals(v))
        //    {
        //        return null;
        //    }
        //    if (v is DateTime)
        //    {
        //        return DateTimeCommon.FastToYYYY_MM_DD_HH_MM_SS( (DateTime)v);
        //    }
        //    else if (v is Color)
        //    {
        //        return ColorTranslator.ToHtml((Color)v);
        //    }
        //    return v.ToString();
        //}
#endif

    }
}
