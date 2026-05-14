using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using DCSoft.Common;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 字体信息类型，本对象可以参与XML和二进制的序列化及反序列化。
    /// </summary>
    [Serializable()]
    [System.ComponentModel.DefaultProperty("Value")]
    [System.ComponentModel.TypeConverter(
        typeof(XFontValueTypeConverter))]
#if WINFORM || DCWriterForWinFormNET6
    [System.ComponentModel.Editor(
        typeof(XFontValueEditor),
        typeof(System.Drawing.Design.UITypeEditor))]
#endif
    [System.ComponentModel.ToolboxItem(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    [DCPublishAPI]
    [DCSoft.Common.DCXSD("FontSettings")]
    public partial class XFontValue : System.ICloneable, IComponent
    {
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static XFontValue()
        {

            SetDefaultFont(System.Drawing.SystemFonts.DefaultFont.Name, 9);

            // 获得系统中所有安装的字体名称
            var fs = System.Drawing.FontFamily.Families;
            var names = new List<string>(fs.Length);
            _FixFontName_Map = new Dictionary<string, string>(fs.Length);
            foreach (FontFamily item in fs)
            {
                if (item.Name != null && item.Name.Length > 0)
                {
                    string v = string.Intern(item.Name);
                    names.Add(v);
                    _FixFontName_Map[v] = v;
                    //_FixFontName_Map[v.ToLower()] = v;
                }
            }
            names.Sort();
            InstalledFontNames = names.ToArray();

          
            _NativeFontField = typeof(System.Drawing.Font).GetField("_nativeFont", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if( _NativeFontField == null )
            {
                _NativeFontField = typeof(System.Drawing.Font).GetField("nativeFont", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            }

        }

        private static readonly System.Reflection.FieldInfo _NativeFontField = null;

        public static void VoidMethod()
        {

        }
        [System.Reflection.Obfuscation( Exclude = true , ApplyToMembers = true )]
        public static void SetDefaultFont(string fontName, float fontSize)
        {
            DefaultFont = new Font(fontName, fontSize);
            DefaultFontName = string.Intern(fontName);
            DefaultFontSize = fontSize;
            _Default = null;
        }

        private static XFontValue _Default = null;
        /// <summary>
        /// 默认值
        /// </summary>
        public static XFontValue Default
        {
            get
            {
                if( _Default == null )
                {
                    _Default = new XFontValue(DefaultFontName, DefaultFontSize);
                }
                return _Default;
            }
        }
        /// <summary>
        /// 系统中所有安装的字体名横
        /// </summary>
        public readonly static string[] InstalledFontNames;



        /// <summary>
        /// 如果安装了指定的字体则返回字体名称，如果没安装则返回空引用
        /// </summary>
        /// <param name="fontName">字体名称</param>
        /// <returns>安装的字体名称</returns>
        public static string FixFontNameIfInstalled(string fontName)
        {
            if (fontName == null || fontName.Length == 0)
            {
                throw new ArgumentNullException("fontName");
            }
            //fontName = GetRuntimeFontName(fontName);
            foreach (var name in InstalledFontNames)
            {
                if (string.Compare(name, fontName, true) == 0)
                {
                    return name;
                }
            }
            return null;
        }

        
        
        private readonly static Dictionary<string, string> _FixFontName_Map;

        [System.Runtime.InteropServices.ComVisible( false )]
        public delegate string FixFontNameEventHandler(string fontName);

        /// <summary>
        /// 修正字体名称事件
        /// </summary>
        public static FixFontNameEventHandler EventFixFontName = null;

        /// <summary>
        /// 修正字体名称
        /// </summary>
        /// <param name="name">字体名称</param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns>修正后的字体名称</returns>
        public static string FixFontName(string name, bool throwException)
        {
            if (name == null || name.Length == 0)
            {
                if (throwException)
                {
                    throw new ArgumentNullException("name");
                }
                return DefaultFontName;
            }

            if(name == DefaultFontName )
            {
                return DefaultFontName;
            }
            //name = GetRuntimeFontName(name);
            //name = string.Intern(name);
            string result = null;
            if( EventFixFontName != null )
            {
                result = EventFixFontName(name);
                return result;
            }
            if (_FixFontName_Map.TryGetValue(name, out result))
            {
                return result;
            }



        BeginFixFontName2:;

            if (_FixFontName_Map.TryGetValue(name, out result))
            {
                return result;
            }
            if (throwException)
            {
                throw new ArgumentOutOfRangeException("FontName:" + name);
            }
            int index = name.IndexOf("(");
            if (index > 0)
            {
                // 字体名称中出现圆括号，则进行修正，再次尝试获得正确的字体名称
                string name2 = name.Substring(0, index).Trim();
                if (name2.Length > 0)
                {
                    name = name2;
                    goto BeginFixFontName2;
                }
            }
            foreach (string item in InstalledFontNames)
            {
                if (item.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) >= 0
                    || name.IndexOf(item, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    _FixFontName_Map[name] = item;
                    return item;
                }
            }
            _FixFontName_Map[name] = DefaultFontName;
            return DefaultFontName;

        }

        //public const string DefaultValueString = "宋体,9";
        /// <summary>
        /// 建议的对象变量名称
        /// </summary>
        public static string SuggestBaseName = "Font";

        /// <summary>
        /// 默认字体
        /// </summary>
        [NonSerialized()]

        public static System.Drawing.Font DefaultFont = null;
        
        /// <summary>
        /// 默认字体名称
        /// </summary>
        [System.NonSerialized()]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public static string DefaultFontName = null;
        /// <summary>
        /// 默认字体大小
        /// </summary>
        [System.NonSerialized()]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public static float DefaultFontSize = 9;

        /// <summary>
        /// 初始化对象
        /// </summary>
        public XFontValue()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="name">字体名称</param>
        /// <param name="size">字体大小</param>
        public XFontValue(string name, float size)
        {
            _Name = name;
            _Size = size;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="name">字体名称</param>
        /// <param name="size">字体大小</param>
        /// <param name="style">字体样式</param>
        public XFontValue(
            string name,
            float size,
            System.Drawing.FontStyle style)
        {
            _Name = name;
            _Size = size;
            this.Style = style;
        }

        /// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="name">字体名称</param>
		/// <param name="size">字体大小</param>
		/// <param name="style">字体样式</param>
        /// <param name="unit">度量单位</param>
		public XFontValue(
            string name,
            float size,
            System.Drawing.FontStyle style,
            GraphicsUnit unit)
        {
            _Name = name;
            _Size = size;
            this.Style = style;
            this.Unit = unit;
        }



        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="f">字体对象</param>
        public XFontValue(System.Drawing.Font f , bool setFontValue = false )
        {
            if (f != null)
            {
                this.Name = f.Name;
                this.Size = f.Size;
                this.Style = f.Style;
                this.Unit = f.Unit;
                if( setFontValue )
                {
                    this.myValue = f;
                }
            }
            //this.Value = f ;
        }

      

        
        public string RuntimeName()
        {
            if (this._Name == null || this._Name.Length == 0)
            {
                return DefaultFontName;
            }
            else
            {
                return _Name;
            }
        }
        private string _Name = DefaultFontName;
        /// <summary>
        /// 字体名称
        /// </summary>
#if WINFORM || DCWriterForWinFormNET6
        [System.ComponentModel.Editor(
            "DCSoft.WinForms.Design.FontNameUITypeEditor",
            typeof(System.Drawing.Design.UITypeEditor))]
#endif
        [DefaultFontNameValueAttribute()]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCSoft.Common.DCXSD(DCXSDOutputType.Attribute)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    if (_Name == null || _Name.Length == 0)
                    {
                        _Name = DefaultFontName;
                    }
                    else
                    {
                        _Name = string.Intern(_Name);
                    }
                    myValue = null;
                    //this._RawFontIndex = -1;
                }
            }
        }

     

        private float _Size = DefaultFontSize;
        /// <summary>
        /// 字体大小
        /// </summary>
#if WINFORM || DCWriterForWinFormNET6
        [System.ComponentModel.Editor(
            "DCSoft.Editor.FontSizeEditor",
            typeof(System.Drawing.Design.UITypeEditor))]
#endif
        [System.ComponentModel.DefaultValue(9f)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCSoft.Common.DCXSD(DCXSDOutputType.Attribute)]
        public float Size
        {
            get
            {
                return _Size;
            }
            set
            {
                if (_Size != value)
                {
                    _Size = value;
                    if (_Size <= 0)
                    {
                        _Size = DefaultFontSize;
                    }
                    myValue = null;
                    //this._RawFontIndex = -1;
                }
            }
        }


        private GraphicsUnit _Unit = GraphicsUnit.Point;
        /// <summary>
        /// 字体大小的度量单位
        /// </summary>
        [DefaultValue(GraphicsUnit.Point)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCSoft.Common.DCXSD(DCXSDOutputType.Attribute)]
        public GraphicsUnit Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                _Unit = value;
            }
        }

        private bool _Bold;
        /// <summary>
        /// 是否粗体
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCSoft.Common.DCXSD(DCXSDOutputType.Attribute)]
        public bool Bold
        {
            get
            {
                return _Bold;
            }
            set
            {
                if (_Bold != value)
                {
                    _Bold = value;
                    myValue = null;
                    //this._RawFontIndex = -1;
                }
            }
        }
        private bool _Italic;
        /// <summary>
        /// 是否斜体
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCSoft.Common.DCXSD(DCXSDOutputType.Attribute)]
        public bool Italic
        {
            get
            {
                return _Italic;
            }
            set
            {
                if (_Italic != value)
                {
                    _Italic = value;
                    myValue = null;
                    //this._RawFontIndex = -1;
                }
            }
        }

        private bool _Underline ;
        /// <summary>
        /// 下划线
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCSoft.Common.DCXSD(DCXSDOutputType.Attribute)]
        public bool Underline
        {
            get
            {
                return _Underline;
            }
            set
            {
                if (_Underline != value)
                {
                    _Underline = value;
                    myValue = null;
                    //this._RawFontIndex = -1;
                }
            }
        }

        private bool _Strikeout;
        /// <summary>
        /// 删除线
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCSoft.Common.DCXSD(DCXSDOutputType.Attribute)]
        public bool Strikeout
        {
            get
            {
                return _Strikeout;
            }
            set
            {
                if (_Strikeout != value)
                {
                    _Strikeout = value;
                    myValue = null;
                    //this._RawFontIndex = -1;
                }
            }
        }

        /// <summary>
        /// 字体样式
        /// </summary>
        [System.ComponentModel.DefaultValue(
            System.Drawing.FontStyle.Regular)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.FontStyle Style
        {
            get
            {
                System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
                if (this._Bold)
                {
                    style = System.Drawing.FontStyle.Bold;
                }
                if (this._Italic)
                {
                    style = style | System.Drawing.FontStyle.Italic;
                }
                if (this._Underline)
                {
                    style = style | System.Drawing.FontStyle.Underline;
                }
                if (this._Strikeout)
                {
                    style = style | System.Drawing.FontStyle.Strikeout;
                }
                return style;
            }
            set
            {
                if (this.Style != value)
                {
                    this._Bold = GetStyle(value, System.Drawing.FontStyle.Bold);
                    this._Italic = GetStyle(value, System.Drawing.FontStyle.Italic);
                    this._Underline = GetStyle(value, System.Drawing.FontStyle.Underline);
                    this._Strikeout = GetStyle(value, System.Drawing.FontStyle.Strikeout);
                    myValue = null;
                    //this._RawFontIndex = -1;
                }
            }
        }

        private bool GetStyle(
            System.Drawing.FontStyle intValue,
            System.Drawing.FontStyle MaskFlag)
        {
            return (intValue & MaskFlag) == MaskFlag;
        }

        /// <summary>
        /// 修正字体名称,使得字体在本系统中有效
        /// </summary>
        /// <returns>操作是否修改了字体名称</returns>

        public bool FixFontName()
        {
            string name = FixFontName(this._Name, false);
            if (name != _Name)
            {
                //this._RawFontIndex = -1;
                _Name = name;
                return true;
            }
            else
            {
                return false;
            }
        }

       

       
        /// <summary>
        /// 缓存字体的列表
        /// </summary>
        [System.NonSerialized()]
        private static readonly MyFontBuffer _Buffer = new MyFontBuffer();

        private static readonly Dictionary<System.Drawing.Font, string> _FontNames = new Dictionary<Font, string>();
      

        /// <summary>
        /// 失败的字体名称列表
        /// </summary>
        [System.NonSerialized()]
        private static List<string> BadFontNames = new List<string>();
        private static int _BufferVersion = 0;
        /// <summary>
        /// 清空缓存区累计次数
        /// </summary>

        public static int BufferVersion
        {
            get
            {
                return _BufferVersion;
            }
        }

        /// <summary>
        /// 清空内置的字体缓冲区
        /// </summary>

        public static void ClearBuffer(int minBufferItemSize = 0)
        {
            if (_Buffer != null)
            {
                lock (_Buffer)
                {
                    if (minBufferItemSize > 0)
                    {
                        if (_Buffer.Count < minBufferItemSize)
                        {
                            return;
                        }
                    }
                    //lock (myBuffer)
                    {
                        _FontNames.Clear();
                        _Buffer.Clear();
                        BadFontNames.Clear();
                    }
                    //System.GC.Collect();
                    _BufferVersion++;
                }
                if (BufferCleared != null)
                {
                    BufferCleared(null, null);
                }
            }
        }

        /// <summary>
        /// 字体缓存对象清空时间
        /// </summary>

        public static event EventHandler BufferCleared = null;

        

    
        [NonSerialized]
        private int _LocalBufferVersion = _BufferVersion;
        
#if DCWriterForWASM
        private static readonly string DefaultSystemFontName = "宋体";
#else
        //#if WINFORM || DCWriterForWinFormNET6
        private static readonly string DefaultSystemFontName = string.Intern(System.Drawing.SystemFonts.DefaultFont.Name);// System.Windows.Forms.Control.DefaultFont.Name;
#endif                                                                                                                     //#else
        //        private const string DefaultSystemFontName = "宋体";
        //#endif
#if !DCWriterForWASM

        public static Font CreateFont(string fontName, float fontSize, FontStyle style, GraphicsUnit unit)
        {
            fontName = FixFontName(fontName, false);
            try
            {
                return new Font(fontName, fontSize, style, unit);
            }
            catch (System.Exception ext)
            {
                return new Font(DefaultFontName, fontSize, style, unit);
            }
        }
#endif
        public static System.Drawing.Font CreateDefaultFontValue()
        {
            return new System.Drawing.Font(FixFontName(DefaultFontName,false), DefaultFontSize, FontStyle.Regular);
        }

        public System.Drawing.Font CreateFont()
        {
            return new System.Drawing.Font(this._Name, this._Size, this.Style, this._Unit);
        }
        /// <summary>
        /// 是否为默认字体
        /// </summary>
        private bool _IsDefalutValue = false;
        [System.NonSerialized()]
        private System.Drawing.Font myValue;
        public void ClearValue()
        {
            if( this.myValue != null )
            {
                lock(_Buffer)
                {
                    _Buffer.Remove(this.myValue);
                    this._LocalBufferVersion++;
                    _BufferVersion++;
                }
            }
            this.myValue = null;
        }
        /// <summary>
        /// 字体对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Font Value
        {
            get
            {
                //if (this.myValue != null && (IntPtr)_NativeFontField.GetValue(this.myValue) == IntPtr.Zero)
                //{

                //}

                if (this.myValue == null || this._LocalBufferVersion != _BufferVersion)
                {
                    this.myValue = null;
                    this._IsDefalutValue = false;
                    lock (_Buffer)
                    {
                        //this._RawFontIndex = -1;
                        this._LocalBufferVersion = _BufferVersion;
                        string fname = FixFontName(this._Name, false);
                        //if (string.IsNullOrEmpty(fname))
                        //{
                        //    fname = DefaultSystemFontName ;
                        //}
                        float fsize = this._Size;
                        if (fsize <= 0)
                        {
                            fsize = DefaultFontSize;
                        }
                        System.Drawing.FontStyle fstyle = this.Style;

                        // 判断是否是曾经失败的字体名称
                        if (BadFontNames.Count > 0)
                        {
                            foreach (string name in BadFontNames)
                            {
                                if (string.Compare(name, fname, true) == 0)
                                {
                                    fname = DefaultFontName;
                                    break;
                                }
                            }
                        }

                        if (fname == DefaultFontName
                            && fsize == DefaultFontSize
                            && fstyle == System.Drawing.FontStyle.Regular)
                        {
                            // 默认字体
                            myValue = DefaultFont;
                            if (_Buffer.Contains(DefaultFont) == false)
                            {
                                _Buffer.Add(DefaultFont);
                            }
                            return this.myValue;
                        }
                        else
                        {
                            int index = _Buffer.IndexOf(fname, fsize, fstyle, this._Unit);
                            if (index >= 0)
                            {
                                this.myValue = _Buffer.GetValue(index);
                                //this._RawFontIndex = index;
                                return this.myValue;
                            }
                        
                        }
                        if (this.myValue == null)
                        {
                            if (_Buffer.Count > 500)
                            {
                                //throw new System.Exception("缓存的字体过多");
                                return DefaultFont;
                            }
                            System.Drawing.FontFamily ff = null;
                            try
                            {
                                ff = new System.Drawing.FontFamily(fname);
                                if (ff.Name != fname)
                                {
                                    // 创建字体时发生异常，则认为是不存在的字体
                                    // 指定的字体名称是失败的
                                    ff = new System.Drawing.FontFamily(DefaultFontName);
                                    bool find = false;
                                    foreach (string name in BadFontNames)
                                    {
                                        if (string.Compare(name, this._Name, true) == 0)
                                        {
                                            find = true;
                                            break;
                                        }
                                    }
                                    if (find == false)
                                    {
                                        BadFontNames.Add(this._Name);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                // 指定的字体名称是失败的
                                ff = new System.Drawing.FontFamily(DefaultFontName);
                                bool find = false;
                                foreach (string name in BadFontNames)
                                {
                                    if (string.Compare(name, this._Name, true) == 0)
                                    {
                                        find = true;
                                        break;
                                    }
                                }
                                if (find == false)
                                {
                                    BadFontNames.Add(this._Name);
                                }
                            }
                            try
                            {
                                if (ff.IsStyleAvailable(this.Style) == false)
                                {
                                    //某些字体不支持当前字体样式,则重新搜索设置合适的字体样式.
                                    foreach (System.Drawing.FontStyle st in new System.Drawing.FontStyle[] {
                                        System.Drawing.FontStyle.Regular ,
                                        System.Drawing.FontStyle.Bold ,
                                        System.Drawing.FontStyle.Italic ,
                                        System.Drawing.FontStyle.Underline ,
                                        System.Drawing.FontStyle.Strikeout ,
                                        System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic })
                                    {
                                        if (ff.IsStyleAvailable(st))
                                        {
                                            this.Style = st;
                                            break;
                                        }
                                    }
                                }
                                this.myValue = _Buffer.Add(ff, this._Size, this.Style, this.Unit);
                                //this._RawFontIndex = _Buffer.Count - 1;
                                //myValue = new System.Drawing.Font(ff, _Size, this.Style, this.Unit);
                                //_Buffer.Add(myValue);
                            }
                            catch (Exception ext)
                            {
                                //DCConsole.Default.WriteLineError(ext.Message);
                                myValue = DefaultFont;
                            }
                        }
                        else
                        {
                            if (_Buffer.Contains(this.myValue) == false)
                            {
                                _Buffer.Add(this.myValue);
                                //this._RawFontIndex = _Buffer.Count - 1;
                            }
                        }
                    }//lock
                }//if
                return myValue;
            }
            set
            {
                if (value == null)
                {
                    value = DefaultFont;
                }
                if (EqualsValue(value) == false)
                {
                    _Name = value.Name;
                    _Size = value.Size;
                    _Bold = value.Bold;
                    _Italic = value.Italic;
                    _Underline = value.Underline;
                    _Strikeout = value.Strikeout;
                    _Unit = value.Unit;
                    myValue = value;
                }
            }
        }

        public static void CheckBufferValidate()
        {
            lock(_Buffer)
            {
                _Buffer.CheckInvalidate();
            }
        }
        private class MyFontBuffer
        {
            public void CheckInvalidate()
            {
                if (XFontValue._NativeFontField != null)
                {
                    foreach (var item in this.Values)
                    {
                        var h = (IntPtr)XFontValue._NativeFontField.GetValue(item);
                        if(h == IntPtr.Zero )
                        {
                            this.Clear();
                            break;
                        }
                    }
                }
            }

            public int Count
            {
                get
                {
                    return this.Values.Count;
                }
            }
            public void Remove(Font f)
            {
                if (f != null)
                {
                    this.Values.Remove(f);
                }
            }
            public bool Contains(Font f)
            {
                return this.Indexs2.ContainsKey(f);
            }
#if !DCWriterForWASM

            public int IndexOf(Font f)
            {
                lock (this)
                {
                    int result = 0;
                    if (this.Indexs2.TryGetValue(f, out result))
                    {
                        return result;
                    }
                }
                return -1;
            }
#endif
            public int IndexOf(string fName, float fontSize, FontStyle style, GraphicsUnit unit)
            {
                var info = new XFontInfo(fName, fontSize, style, unit);
                lock (this)
                {
                    int result = 0;
                    if (this.Indexs.TryGetValue(info, out result))
                    {
                        return result;
                    }
                }
                return -1;
            }
            public Font GetValue(int index)
            {
                return this.Values[index];
            }

            private readonly Dictionary<XFontInfo, int> Indexs = new Dictionary<XFontInfo, int>();

            public void Add(Font f)
            {
                lock (this)
                {
                    this.Values.Add(f);
                    this.Names.Add(f.Name);
                    this.Indexs[new XFontInfo(f)] = this.Values.Count - 1;
                    this.Indexs2[f] = this.Values.Count - 1;
                }
            }
             
            public Font Add(FontFamily fn, float fontSize, FontStyle style, GraphicsUnit unit)
            {
                lock (this)
                {
                    var f = new Font(fn, fontSize, style, unit);
                    this.Values.Add(f);
                    this.Names.Add(f.Name);
                    this.Indexs[new XFontInfo(fn.Name, fontSize, style, unit)] = this.Values.Count - 1;
                    this.Indexs2[f] = this.Values.Count - 1;
                    return f;
                }
            }

            private readonly Dictionary<Font, int> Indexs2 = new Dictionary<Font, int>();
            private readonly List<Font> Values = new List<Font>();
            private readonly List<string> Names = new List<string>();
            public void Clear()
            {
                lock (this)
                {
                    foreach (var f in this.Values)
                    {
                        f.Dispose();
                    }
                    this.Indexs.Clear();
                    this.Indexs2.Clear();
                    this.Values.Clear();
                    this.Names.Clear();
                }
            }
        }
        /// <summary>
        /// 获得字体的以像素为单位的高度
        /// </summary>
        /// <returns>字体高度</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float GetHeight()
        {
            System.Drawing.Font f = this.Value;
            if (f == null)
            {
                return 0;
            }
            else
            {
                return f.GetHeight();
            }
        }
        /// <summary>
        /// 获得字体高度
        /// </summary>
        /// <param name="dpi"></param>
        /// <returns></returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float GetHeight(float dpi )
        {
            var f = this.Value;
            if( f == null )
            {
                return 0;
            }
            else
            {
                return f.GetHeight(dpi);
            }
        }

        
        internal bool FixDisposedValue()
        {
            if( this.myValue != null && _NativeFontField != null )
            {
                var v = (IntPtr) _NativeFontField.GetValue(this.myValue);
                if (v == IntPtr.Zero)
                {
                    System.Console.WriteLine("检测到字体已销毁，试图修复。");
                    lock (_Buffer)
                    {
                        XFontValue.ClearBuffer();
                        if (this.myValue == DefaultFont)
                        {
                            DefaultFont = new Font(DefaultFontName, DefaultFontSize);
                            _BufferVersion++;
                        }
                    }
                    this.myValue = null;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得字体的高度
        /// </summary>
        /// <param name="g">绘图对象</param>
        /// <returns>字体高度</returns>
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float GetHeight(DCGraphics g)
        {

            System.Drawing.Font f = this.Value;
            if (f == null)
            {
                return 0;
            }
            else if (g.NativeGraphics != null)
            {
                return f.GetHeight(g.NativeGraphics);
            }
            else if (g.GraphisForMeasure != null)
            {
                return f.GetHeight(g.GraphisForMeasure);
            }
            else
            {
                return f.GetHeight(g.DpiY);
            }
        }
        private static readonly Dictionary<string, float> _StdFontHeights = new Dictionary<string, float>();
        /// <summary>
        /// 获得字体的高度
        /// </summary>
        /// <param name="g">绘图对象</param>
        /// <returns>字体高度</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float GetHeight(System.Drawing.Graphics g)
        {
            if (g == null )
            {
                throw new ArgumentNullException("g");
            }
          
            //var h1 = GraphicsUnitConvert.Convert(this._Size, this._Unit, g.PageUnit);
            System.Drawing.Font f = this.Value;

            if (f == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    var result2 =  f.GetHeight(g);
                    return result2;
                }
                catch ( System.Exception ext)
                {
                    _BufferVersion++;
                    lock (_Buffer)
                    {
                        _Buffer.Remove(this.myValue);
                    }
                    this.myValue = null;
                    if(this.FixDisposedValue())
                    {
                        return this.Value.GetHeight(g);// return GetFontHeight2(this.Value, g);// f.GetHeight(g);
                    }
                    else
                    {
                        this.myValue = new Font(this._Name, this._Size, this.Style);
                        return this.myValue.GetHeight(g);
                        //throw;
                    }
                }
            }
        }
         
        private static float GetFontHeight2( System.Drawing.Font f , System.Drawing.Graphics g )
        {
            return f.GetHeight(g);
        }
        

        /// <summary>
        /// 获得指定度量单位下的字体高度
        /// </summary>
        /// <param name="unit">指定的度量单位</param>
        /// <returns>字体高度</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float GetHeight(GraphicsUnit unit)
        {
            return GraphicsUnitConvert.Convert(this.Value.SizeInPoints, GraphicsUnit.Point, unit);
        }


        /// <summary>
        /// 比较对象和指定字体的设置是否一致
        /// </summary>
        /// <param name="f">字体对象</param>
        /// <returns>是否一致</returns>

        public bool EqualsValue(System.Drawing.Font f)
        {
            if (f == null)
            {
                return false;
            }
            if (_Name != f.Name)
            {
                return false;
            }
            if (_Size != f.Size)
            {
                return false;
            }
            if (_Bold != f.Bold)
            {
                return false;
            }
            if (_Italic != f.Italic)
            {
                return false;
            }
            if (_Underline != f.Underline)
            {
                return false;
            }
            if (_Strikeout != f.Strikeout)
            {
                return false;
            }
            if (_Unit != f.Unit)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 比较对象和指定字体的设置是否一致
        /// </summary>
        /// <param name="f">字体对象</param>
        /// <returns>是否一致</returns>

        public bool EqualsValue(XFontValue f)
        {
            if (f == null)
            {
                return false;
            }
            if (this == f)
            {
                return true;
            }
            if (_Name != f._Name)
            {
                return false;
            }
            if (_Size != f._Size)
            {
                return false;
            }
            if (_Bold != f._Bold)
            {
                return false;
            }
            if (_Italic != f._Italic)
            {
                return false;
            }
            if (_Underline != f._Underline)
            {
                return false;
            }
            if (_Strikeout != f._Strikeout)
            {
                return false;
            }
            if (_Unit != f._Unit)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public XFontValue Clone()
        {
            XFontValue font = new XFontValue();
            font.CopySettings(this);
            //this._RawFontIndex = -1;
            return font;
        }
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object System.ICloneable.Clone()
        {
            XFontValue font = new XFontValue();
            font.CopySettings(this);
            //this._RawFontIndex = -1;
            return font;
        }
        /// <summary>
        /// 将指定字体对象的设置复制到本对象
        /// </summary>
        /// <param name="SourceFont">来源字体对象</param>
        public void CopySettings(XFontValue SourceFont)
        {
            this._Name = SourceFont._Name;
            this._Size = SourceFont._Size;
            this._Bold = SourceFont._Bold;
            this._Italic = SourceFont._Italic;
            this._Underline = SourceFont._Underline;
            this._Strikeout = SourceFont._Strikeout;
            this._Unit = SourceFont._Unit;
        }

        /// <summary>
        /// 比较两个对象内容是否相同
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>内容是否相同</returns>

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is XFontValue))
                return false;
            XFontValue f = (XFontValue)obj;
            return f._Bold == this._Bold
                && f._Italic == this._Italic
                && f._Strikeout == this._Strikeout
                && f._Underline == this._Underline
                && f._Size == this._Size
                && f._Name == this._Name
                && f._Unit == this._Unit;
        }

        /// <summary>
        /// 获得对象的哈希代码
        /// </summary>
        /// <returns>哈希代码</returns>

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// 获得表示对象数据的字符串
        /// </summary>
        /// <returns>字符串</returns>

        public override string ToString()
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            list.Add(this.Name);
            list.Add(this.Size.ToString());
            if (this.Style != System.Drawing.FontStyle.Regular)
            {
                list.Add("style=" + this.Style.ToString("G"));
            }
            if (this._Unit != GraphicsUnit.Point)
            {
                list.Add(this._Unit.ToString("G"));
            }
            return string.Join(", ", (string[])list.ToArray(typeof(string)));
        }
        /// <summary>
        /// 解析字符串，设置对象数据
        /// </summary>
        /// <param name="Text">要解析的字符串</param>

        public void Parse(string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return;
            }
            string[] items = Text.Split(',');
            if (items.Length < 1)
            {
                throw new ArgumentException("必须符合 name,size,style=Bold,Italic,Underline,Strikeout 样式");
            }
            string name = items[0];

            float size = 9f;
            if (items.Length >= 2)
            {
                if (float.TryParse(items[1].Trim(), out size) == false)
                {
                    size = 9f;
                }
            }
            if (size <= 0)
            {
                size = 1;
            }

            System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
            bool flag = false;
            for (int iCount = 2; iCount < items.Length; iCount++)
            {
                string item = items[iCount].Trim().ToLower();
                if (flag == false)
                {
                    if (item.StartsWith("style"))
                    {
                        int index = item.IndexOf("=");
                        if (index > 0)
                        {
                            flag = true;
                            item = item.Substring(index + 1);
                        }
                    }
                }
                if (flag)
                {
                    if (Enum.IsDefined(typeof(FontStyle), item.Trim()))
                    {
                        FontStyle s2 = (FontStyle)Enum.Parse(
                            typeof(FontStyle), item.Trim(), true);
                        style = style | s2;
                    }
                    else if (Enum.IsDefined(typeof(GraphicsUnit), item.Trim()))
                    {
                        this._Unit = (GraphicsUnit)Enum.Parse(
                            typeof(GraphicsUnit), item.Trim(), true);
                    }
                }
            }
            this.Name = name;
            this.Size = size;
            this.Style = style;
        }

#region IComponent 成员

        /// <summary>
        /// 对象销毁事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        [field: NonSerialized]
        public event EventHandler Disposed = null;

        [NonSerialized]
        private ISite mySite;
        /// <summary>
        /// 组件站点对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public ISite Site
        {
            get
            {
                return mySite;
            }
            set
            {
                mySite = value;
            }
        }

#endregion

#region IDisposable 成员

        /// <summary>
        /// 销毁对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]

        public void Dispose()
        {
            this.mySite = null;
            this.myValue = null;
            this._Name = null;
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }

#endregion
    }//public class XFontValue : System.ICloneable

   

    

    /// <summary>
    /// 字体宽度样式
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
    public enum FontWidthStyle
    {
        /// <summary>
        /// 比例字体
        /// </summary>
        Proportional,
        /// <summary>
        /// 等宽字体
        /// </summary>
        Monospaced
    }


   

    /// <summary>
    /// 默认字体名称特性
    /// </summary>
    [System.AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public sealed class DefaultFontNameValueAttribute : System.ComponentModel.DefaultValueAttribute
    {
        static DefaultFontNameValueAttribute()
        {
            DefaultFontName = XFontValue.DefaultFontName;
        }
        /// <summary>
        /// 默认字体名称
        /// </summary>
        public static string DefaultFontName = null;

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DefaultFontNameValueAttribute() : base(DefaultFontName)
        {
        }
    }

    //#endif
#if WINFORM || DCWriterForWinFormNET6
    /// <summary>
    /// 字体属性编辑器,设计器内部使用
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class XFontValueEditor : System.Drawing.Design.UITypeEditor
    {
        /// <summary>
        /// 编辑对象数据
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="provider">参数</param>
        /// <param name="Value">旧数据</param>
        /// <returns>新数据</returns>
		public override object EditValue(
            ITypeDescriptorContext context,
            IServiceProvider provider,
            object Value)
        {
            using (System.Windows.Forms.FontDialog dlg = new System.Windows.Forms.FontDialog())
            {
                dlg.ShowApply = false;
                dlg.ShowColor = false;
                if (Value != null)
                {
                    if (Value is System.Drawing.Font)
                    {
                        dlg.Font = (System.Drawing.Font)Value;
                    }
                    else if (Value is XFontValue)
                    {
                        dlg.Font = ((XFontValue)Value).Value;
                    }
                }
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Type pt = context.PropertyDescriptor.PropertyType;
                    if (pt.Equals(typeof(System.Drawing.Font)))
                    {
                        Value = dlg.Font;
                    }
                    else if (pt.Equals(typeof(XFontValue)))
                    {
                        Value = new XFontValue(dlg.Font);
                    }
                    else
                    {
                        Value = dlg.Font;
                    }
                }
            }//if
            return Value;
        }

        /// <summary>
        /// 获得对象数据的编辑样式
        /// </summary>
        /// <param name="context">参数</param>
        /// <returns>编辑样式</returns>
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
#endif
    /// <summary>
    /// 字体类型转换器,设计器内部使用
    /// </summary>
    [System.ComponentModel.Browsable(false)]
    [System.Runtime.InteropServices.ComVisible(false)]
    public class XFontValueTypeConverter : TypeConverter
    {
        /// <summary>
        /// 支持列出对象属性
        /// </summary>
        /// <param name="context">参数</param>
        /// <returns>支持</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 获得对象属性列表
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="Value">对象</param>
        /// <param name="attributes">参数</param>
        /// <returns>获得的对象属性列表</returns>
        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context, object Value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(XFontValue), attributes).Sort(
                new string[] { "Name", "Size", "Bold", "Italic", "Underline", "Strikeout", "Unit" });
        }

        /// <summary>
        /// 判断能否进行数据类型转换
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="sourceType">原始数据类型</param>
        /// <returns>能否转换</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(System.Drawing.Font))
                return true;
            return ((sourceType == typeof(string))
                || base.CanConvertFrom(context, sourceType));
        }
        /// <summary>
        /// 判断能否进行数据类型转换
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="destinationType">目标数据类型</param>
        /// <returns>能否转换</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(System.Drawing.Font))
                return true;
            if (destinationType == typeof(string))
                return true;
            return ((destinationType == typeof(InstanceDescriptor))
                || base.CanConvertTo(context, destinationType));
        }

        /// <summary>
        /// 将数据转换为字体对象
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="culture">参数</param>
        /// <param name="Value">输入的数据</param>
        /// <returns>转换结果</returns>
        public override object ConvertFrom(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object Value)
        {
            if (Value == null)
            {
                return null;
            }
            if (Value is System.Drawing.Font)
            {
                return new XFontValue((System.Drawing.Font)Value);
            }
            if (!(Value is string))
            {
                return base.ConvertFrom(context, culture, Value);
            }
            string str = ((string)Value).Trim();
            if (str.Length == 0)
            {
                return null;
            }
            XFontValue font = new XFontValue();
            font.Parse(str);
            return font;
        }

        /// <summary>
        /// 将字体转换为其他类型
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="culture">参数</param>
        /// <param name="Value">对象数据</param>
        /// <param name="destinationType">目标数据类型</param>
        /// <returns>转换结果</returns>
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object Value,
            Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (Value == null)
            {
                return null;
            }
            if (destinationType == typeof(string))
            {
                XFontValue font = (XFontValue)Value;
                if (font == null)
                {
                    return "错误的类型";
                }
                return font.ToString();
            }
            if (destinationType == typeof(System.Drawing.Font))
            {
                XFontValue font = (XFontValue)Value;
                return font.Value;
            }
            if ((destinationType == typeof(InstanceDescriptor))
                && (Value is XFontValue))
            {
                XFontValue font = (XFontValue)Value;
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                System.Collections.ArrayList types = new System.Collections.ArrayList();
                list.Add(font.Name);
                types.Add(typeof(string));
                list.Add(font.Size);
                types.Add(typeof(float));
                if (font.Style != System.Drawing.FontStyle.Regular)
                {
                    list.Add(font.Style);
                    types.Add(typeof(System.Drawing.FontStyle));
                }

                System.Reflection.MemberInfo constructor = typeof(XFontValue).GetConstructor(
                    (Type[])types.ToArray(typeof(Type)));
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, list.ToArray());
                }
            }
            return base.ConvertTo(context, culture, Value, destinationType);
        }

       
    }//public class XFontValueTypeConverter : TypeConverter
}