//------------------------------------------------------------------------------
// <copyright file="FontFamily.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
* font family object (sdkinc\GDIplusFontFamily.h)
*/

namespace System.Drawing {
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System;
    using System.ComponentModel;
    using Microsoft.Win32;    
    using System.Globalization;
    using System.Drawing.Text;
    using System.Drawing;
    using System.Drawing.Internal;
    using System.Runtime.Versioning;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms.Internal;

    /**
     * Represent a FontFamily object
     */
    /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily"]/*' />
    /// <devdoc>
    ///    Abstracts a group of type faces having a
    ///    similar basic design but having certain variation in styles.
    /// </devdoc>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed class FontFamily : MarshalByRefObject, IDisposable {
        private const int LANG_NEUTRAL = 0;
        private IntPtr nativeFamily;
        private bool createDefaultOnFail;

#if DEBUG
        private static object lockObj = new object();
        private static int idCount = 0;
        private int id;
#endif

        /// <devdoc>
        ///     Sets the GDI+ native family.
        /// </devdoc>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")]
        private void SetNativeFamily( IntPtr family ) 
        {
            Debug.Assert( this.nativeFamily == IntPtr.Zero, "Setting GDI+ native font family when already initialized." );
            Debug.Assert( family != IntPtr.Zero, "Setting GDI+ native font family to null." );

            this.nativeFamily = family;
#if DEBUG
            lock(lockObj){
                id = ++idCount;
            }
#endif
        }

        ///<devdoc>
        ///     Internal constructor to initialize the native GDI+ font to an existing one.
        ///     Used to create generic fonts and by FontCollection class.
        ///</devdoc>
        internal FontFamily(IntPtr family) 
        {
            SetNativeFamily( family );
        }

        ///// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.FontFamily3"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Initializes a new instance of the <see cref='System.Drawing.FontFamily'/>
        /////       class with the specified name.
        /////
        /////       The <paramref name="createDefaultOnFail"/> parameter determines how errors are
        /////       handled when creating a font based on a font family that does not exist on the
        /////       end user's system at run time. If this parameter is true, then a fall-back font
        /////       will always be used instead. If this parameter is false, an exception will be thrown.
        /////    </para>
        ///// </devdoc>
        ////[ResourceExposure(ResourceScope.Process)]
        ////[ResourceConsumption(ResourceScope.Process)]
        //internal FontFamily(string name, bool createDefaultOnFail)
        //{
        //    this._Name = name;
        //    //this.createDefaultOnFail = createDefaultOnFail;
        //    //CreateFontFamily(name, null);
        //}

        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.FontFamily"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.FontFamily'/>
        ///       class with the specified name.
        ///    </para>
        /// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        public FontFamily(string name)
        {
            this._Name = name;
        }

        internal FontFamily(string name , int intCellAscent,int intCellDescent,int intEmHeight , int intLineSpacing)
        {
            this._Name = name;
            this._CellAscent = intCellAscent;
            this._CellDescent = intCellDescent;
            this._EmHeight = intEmHeight;
            this._LineSpacing = intLineSpacing;
            if( this._EmHeight > 0 )
            {
                this.FixRateForCanvasDraw = (float)this._CellAscent / (float)this._EmHeight;
                if( name == "Arial Black")
                {
                    this.FixRateForCanvasDraw = 0.6f;
                }
            }
        }

       
        /// <summary>
        /// 计算修正量与字体大小的比率（仅当CellAscent > EmHeight时返回有效比率，否则返回0）
        /// </summary>
        /// <param name="cellAscent">字体CellAscent原始值（FontFamily.GetCellAscent()）</param>
        /// <param name="cellDescent">字体CellDescent原始值（FontFamily.GetCellDescent()，参数保留，不影响核心计算）</param>
        /// <param name="emHeight">字体EmHeight原始值（FontFamily.GetEmHeight()）</param>
        /// <returns>修正量/字体大小的比率（无需修正返回0）</returns>
        public static float GetFixValueToFontSizeRatio(int cellAscent, int cellDescent, int emHeight)
        {
            // 边界校验：EmHeight为0时直接返回0（避免除零错误）
            if (emHeight == 0)
                return 0f;

            // 步骤1：计算CellAscent相对EmHeight的比率
            float ascentRatio = (float)cellAscent / emHeight;

            // 步骤2：计算修正量与字体大小的比率（修正量=字体大小×(ascentRatio-1) → 修正量/字体大小=ascentRatio-1）
            float fixRatio = ascentRatio - 1f;

            // 步骤3：仅当修正比率>0时返回（否则无需修正，返回0）
            return fixRatio > 0f ? fixRatio : 0f;
        }

        internal readonly float FixRateForCanvasDraw = 0;
        ///// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.FontFamily1"]/*' />
        ///// <devdoc>
        /////    Initializes a new instance of the <see cref='System.Drawing.FontFamily'/>
        /////    class in the specified <see cref='System.Drawing.Text.FontCollection'/> and with the specified name.
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //public FontFamily(string name, FontCollection fontCollection)
        //{
        //    CreateFontFamily(name, fontCollection);
        //}

        ///// <devdoc>
        /////     Creates the native font family object.  
        /////     Note: GDI+ creates singleton font family objects (from the corresponding font file) and reference count them.
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //private void CreateFontFamily(string name, FontCollection fontCollection)  
        //{
        //    this._Name = name;
        //    //IntPtr fontfamily = IntPtr.Zero;
        //    //IntPtr nativeFontCollection = (fontCollection == null) ? IntPtr.Zero : fontCollection.nativeFontCollection;
           
        //    //int status = SafeNativeMethods.Gdip.GdipCreateFontFamilyFromName(name, new HandleRef(fontCollection, nativeFontCollection), out fontfamily);

        //    //if (status != SafeNativeMethods.Gdip.Ok)
        //    //{
        //    //    if (createDefaultOnFail)
        //    //    {
        //    //        fontfamily = GetGdipGenericSansSerif();  // This throws if failed.
        //    //    }
        //    //    else
        //    //    {
        //    //        // Special case this incredibly common error message to give more information
        //    //        if (status == SafeNativeMethods.Gdip.FontFamilyNotFound)
        //    //        {
        //    //            throw new ArgumentException(SR.GetString(SR.GdiplusFontFamilyNotFound, name));
        //    //        }
        //    //        else if (status == SafeNativeMethods.Gdip.NotTrueTypeFont)
        //    //        {
        //    //            throw new ArgumentException(SR.GetString(SR.GdiplusNotTrueTypeFont, name));
        //    //        }
        //    //        else
        //    //        {
        //    //            throw SafeNativeMethods.Gdip.StatusException(status);
        //    //        }
        //    //    }
        //    //}

        //    //SetNativeFamily( fontfamily );
        //}

        ///// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.FontFamily2"]/*' />
        ///// <devdoc>
        /////    Initializes a new instance of the <see cref='System.Drawing.FontFamily'/>
        /////    class from the specified generic font family.
        ///// </devdoc>
        //[ResourceExposure(ResourceScope.Process)]
        //[ResourceConsumption(ResourceScope.Process)]
        //public FontFamily(GenericFontFamilies genericFamily) {
        //    IntPtr fontfamily = IntPtr.Zero;
        //    int status;

        //    switch (genericFamily) {
        //        case GenericFontFamilies.Serif:
        //        {
        //            status = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySerif(out fontfamily);
        //            break;
        //        }
        //        case GenericFontFamilies.SansSerif:
        //        {
        //            status = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySansSerif(out fontfamily);
        //            break;
        //        }
        //        case GenericFontFamilies.Monospace:
        //        default:
        //        {
        //            status = SafeNativeMethods.Gdip.GdipGetGenericFontFamilyMonospace(out fontfamily);
        //            break;
        //        }
        //    }   

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);

        //    SetNativeFamily( fontfamily );
        //}

        ///// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.Finalize"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Allows an object to free resources before the object is reclaimed by the
        /////       Garbage Collector (<see langword='GC'/>).
        /////    </para>
        ///// </devdoc>
        //~FontFamily() 
        //{
        //    Dispose(false);
        //}

        /// <devdoc>
        ///     The GDI+ native font family.  It is shared by all FontFamily objects with same family name.
        /// </devdoc>
        internal IntPtr NativeFamily 
        {
            [System.Runtime.TargetedPatchingOptOutAttribute("Performance critical to inline across NGen image boundaries")]
            get 
            {
                //Debug.Assert( this.nativeFamily != IntPtr.Zero, "this.nativeFamily == IntPtr.Zero." );
                return this.nativeFamily;
            }
        }

        // The managed wrappers do not expose a Clone method, as it's really nothing more
        // than AddRef (it doesn't copy the underlying GpFont), and in a garbage collected
        // world, that's not very useful.

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override bool Equals(object obj) 
        {

            if (obj == this)
                return true;

            FontFamily ff = obj as FontFamily;

            if (ff == null)
                return false;
            
            // We can safely use the ptr to the native GDI+ FontFamily because it is common to 
            // all objects of the same family (singleton RO object).
            return ff.NativeFamily == this.NativeFamily;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override string ToString() {
            return this.Name;// string.Format(CultureInfo.CurrentCulture, "[{0}: Name={1}]", GetType().Name, this.Name);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override int GetHashCode() {
            return this.GetName(LANG_NEUTRAL).GetHashCode();
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        private static int CurrentLanguage {
            get {
                return System.Globalization.CultureInfo.CurrentUICulture.LCID;
            }
        }

        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.Dispose"]/*' />
        /// <devdoc>
        ///    Disposes of this <see cref='System.Drawing.FontFamily'/>.
        /// </devdoc>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing) 
        {
//            if (this.nativeFamily != IntPtr.Zero) 
//            {
//                try
//                {
//#if DEBUG
//                    int status =
//#endif
//                    SafeNativeMethods.Gdip.GdipDeleteFontFamily(new HandleRef(this, this.nativeFamily));
//#if DEBUG
//                    Debug.Assert(status == SafeNativeMethods.Gdip.Ok, "GDI+ returned an error status: " + status.ToString(CultureInfo.InvariantCulture));
//#endif
//                }
//                catch (Exception ex)
//                {
//                    if (ClientUtils.IsCriticalException(ex))
//                    {
//                        throw;
//                    }

//                    Debug.Fail("Exception thrown during Dispose: " + ex.ToString());
//                }
//                finally
//                {
//                    this.nativeFamily = IntPtr.Zero;
//                }
//            }
        }
        private readonly int _LineSpacing = 0;
        private readonly int _CellAscent = 0;
        private readonly int _CellDescent = 0;
        private readonly int _EmHeight = 0;

        private readonly string _Name = null;
        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.Name"]/*' />
        /// <devdoc>
        ///    Gets the name of this <see cref='System.Drawing.FontFamily'/>.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public String Name 
        {
            get 
            {
                return this._Name;// GetName(CurrentLanguage);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public String GetName(int language)
        {
            return this._Name;
            //// LF_FACESIZE is 32
            //StringBuilder name = new StringBuilder(32);

            //int status = SafeNativeMethods.Gdip.GdipGetFamilyName(new HandleRef(this, this.NativeFamily), name, language);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //    throw SafeNativeMethods.Gdip.StatusException(status);

            //return name.ToString();
        }
        internal static FontFamily GetInstance(string name )
        {
            foreach( var item in _Families )
            {
                if(item._Name == name )
                {
                    return item;
                }
            }
            return null;
        }
        private static FontFamily[] _Families = null;
        internal static void InnerSetFamilies(FontFamily[] fms)
        {
            _Families = fms;
        }
        /// <summary>
        /// 是否为支持的字体名称
        /// </summary>
        /// <param name="strName">字体名称</param>
        /// <returns>是否支持</returns>
        internal static bool IsSupportedFontName( string strName )
        {
            foreach( var item in _Families)
            {
                if(item.Name == strName )
                {
                    return true;
                }
            }
            return false;
        }
        internal static string FixFontName( string strName , string strDefaultFontName )
        {
            foreach( var item in _Families)
            {
                if(item._Name == strName )
                {
                    return strName;
                }
            }
            return strDefaultFontName;
        }
        //public static void InnerSetFamilies(string[] names)
        //{
        //    var list = new System.Collections.Generic.List<FontFamily>();
        //    var list2 = new System.Collections.Generic.List<string>();
        //    foreach (var name in names)
        //    {
        //        if (name != null && name.Length > 0 && list2.Contains(name) == false)
        //        {
        //            list2.Add(name);
        //            var f = new FontFamily(name);
        //            list.Add(f);
        //        }
        //    }
        //    list.Sort(delegate(FontFamily f1 , FontFamily f2) {
        //        return string.Compare(f1._Name, f2._Name, StringComparison.OrdinalIgnoreCase);
        //    });
        //    _Families = list.ToArray();
        //}

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static FontFamily[] Families {
            get {
                return _Families;
            }
        }

         /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.GenericSansSerif"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets a generic SansSerif <see cref='System.Drawing.FontFamily'/>.
        ///    </para>
        /// </devdoc>
        public static FontFamily GenericSansSerif {
            [ResourceExposure(ResourceScope.Process)]
            [ResourceConsumption(ResourceScope.Process)]
            get {
                return new FontFamily(GetGdipGenericSansSerif());
            }
        }

        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        private static IntPtr GetGdipGenericSansSerif() {
            IntPtr fontfamily = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySansSerif(out fontfamily);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return fontfamily;
        }

        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.GenericSerif"]/*' />
        /// <devdoc>
        ///    Gets a generic Serif <see cref='System.Drawing.FontFamily'/>.
        /// </devdoc>
        public static FontFamily GenericSerif {
            [ResourceExposure(ResourceScope.Process)]
            [ResourceConsumption(ResourceScope.Process)]
            get {
                return new FontFamily(GetNativeGenericSerif());
            }
        }

        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        private static IntPtr GetNativeGenericSerif() {
            IntPtr fontfamily = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySerif(out fontfamily);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return fontfamily;
        }

        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.GenericMonospace"]/*' />
        /// <devdoc>
        ///    Gets a generic monospace <see cref='System.Drawing.FontFamily'/>.
        /// </devdoc>
        public static FontFamily GenericMonospace {
            [ResourceExposure(ResourceScope.Process)]
            [ResourceConsumption(ResourceScope.Process)]
            get {
                return new FontFamily(GetNativeGenericMonospace());
            }
        }

        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        private static IntPtr GetNativeGenericMonospace() {
            IntPtr fontfamily = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipGetGenericFontFamilyMonospace(out fontfamily);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return fontfamily;
        }

        // No longer support in FontFamily
        // Obsolete API and need to be removed later
        //
        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.GetFamilies"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Returns an array that contains all of the <see cref='System.Drawing.FontFamily'/> objects associated with
        ///       the specified graphics context.
        ///    </para>
        /// </devdoc>
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        [Obsolete("Do not use method GetFamilies, use property Families instead")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static FontFamily[] GetFamilies(Graphics graphics) {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            return _Families;
        }

        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.IsStyleAvailable"]/*' />
        /// <devdoc>
        ///    Indicates whether the specified <see cref='System.Drawing.FontStyle'/> is
        ///    available.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool IsStyleAvailable(FontStyle style) {
            return true;
        }

        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.GetEmHeight"]/*' />
        /// <devdoc>
        ///    Gets the size of the Em square for the
        ///    specified style in font design units.
        /// </devdoc>
        public int GetEmHeight(FontStyle style) {
            return this._EmHeight;
        }


        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.GetCellAscent"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Returns the ascender metric for Windows.
        ///    </para>
        /// </devdoc>
        public int GetCellAscent(FontStyle style) {
            return this._CellAscent;
        }   

        /// <include file='doc\FontFamily.uex' path='docs/doc[@for="FontFamily.GetCellDescent"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Returns the descender metric for Windows.
        ///    </para>
        /// </devdoc>
        public int GetCellDescent(FontStyle style) {
            return this._CellDescent;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int GetLineSpacing(FontStyle style) {
            return this._LineSpacing;
        }
    }
}
