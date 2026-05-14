
namespace System.Drawing
{
    [System.Reflection.Obfuscation(Exclude = true , ApplyToMembers = false  )]
    [System.ComponentModel.TypeConverterAttribute(typeof(FontConverter))]

    public sealed class Font : MarshalByRefObject, ICloneable, IDisposable
    {
        static Font()
        {
            DCValueConvert.CheckIsBlazorWASM();
        }
        private static string _DefaultFontName;
        internal static string DefaultFontName
        {
            get { return _DefaultFontName; }
            set
            {
                _DefaultFontName = value;
            }
        }

        const int LogFontCharSetOffset = 23;
        const int LogFontNameOffset = 28;

        //IntPtr nativeFont;
        float fontSize = 9 ;
        FontStyle fontStyle = FontStyle.Regular;
        //FontFamily fontFamily;
        GraphicsUnit fontUnit = GraphicsUnit.Point;
        byte gdiCharSet = SafeNativeMethods.DEFAULT_CHARSET;
        bool gdiVerticalFont;
        string _Name = SystemFonts._DefaultFontName;
        public Font(Font prototype, FontStyle newStyle)
        {
            // Copy over the originalFontName because it won't get initialized
            this._Name = prototype.OriginalFontName;
            this.fontStyle = newStyle;
            this.fontSize = prototype.fontSize;
            this.fontUnit = prototype.fontUnit;

            //Initialize(prototype.FontFamily, prototype.Size, newStyle, prototype.Unit, SafeNativeMethods.DEFAULT_CHARSET, false);
        }

        public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit)
        {
            this._Name = family.Name;
            this.fontSize = emSize;
            this.fontStyle = style;
            this.fontUnit = unit;
            //Initialize(family, emSize, style, unit, SafeNativeMethods.DEFAULT_CHARSET, false);
        }

        public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        {
            this._Name = family.Name;
            this.fontSize = emSize;
            this.fontStyle = style;
            this.fontUnit = unit;
            //Initialize(family, emSize, style, unit, gdiCharSet, gdiVerticalFont);
        }

        public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            this._Name = familyName;
            this.fontSize = emSize;
            this.fontStyle = style;
            this.fontUnit = unit;
            //Initialize(familyName, emSize, style, unit, gdiCharSet, IsVerticalName(familyName));
        }


        public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        {
            this._Name = familyName;
            this.fontSize = emSize;
            this.fontStyle = style;
            this.fontUnit = unit;
            //if (float.IsNaN(emSize) || float.IsInfinity(emSize) || emSize <= 0)
            //{
            //    throw new ArgumentException(SR.GetString(SR.InvalidBoundArgument, "emSize", emSize, 0, "System.Single.MaxValue"), "emSize");
            //}

            //Initialize(familyName, emSize, style, unit, gdiCharSet, gdiVerticalFont);
        }


        public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit)
        {
            this._Name = familyName;
            this.fontSize = emSize;
            this.fontStyle = style;
            this.fontUnit = unit;
            //Initialize(familyName, emSize, style, unit, SafeNativeMethods.DEFAULT_CHARSET, IsVerticalName(familyName));
        }

        public Font(string familyName, float emSize, FontStyle style)
        {
            this._Name = familyName;
            this.fontSize = emSize;
            this.fontStyle = style;
            //Initialize(familyName, emSize, style, GraphicsUnit.Point, SafeNativeMethods.DEFAULT_CHARSET, IsVerticalName(familyName));
        }


        public Font(string familyName, float emSize)
        {
            this._Name = familyName;
            this.fontSize = emSize;
            //Initialize(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, SafeNativeMethods.DEFAULT_CHARSET, IsVerticalName(familyName));
        }

        
        private Font(IntPtr nativeFont, byte gdiCharSet, bool gdiVerticalFont)
        {
            throw new NotSupportedException();
            //Debug.Assert(this.nativeFont == IntPtr.Zero, "GDI+ native font already initialized, this will generate a handle leak");
            //Debug.Assert(nativeFont != IntPtr.Zero, "nativeFont is null");

            //int status = 0;
            //float size = 0;
            //GraphicsUnit unit = GraphicsUnit.Point;
            //FontStyle style = FontStyle.Regular;
            //IntPtr nativeFamily = IntPtr.Zero;

            //this.nativeFont = nativeFont;

            //status = SafeNativeMethods.Gdip.GdipGetFontUnit(new HandleRef(this, nativeFont), out unit);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //    throw SafeNativeMethods.Gdip.StatusException(status);

            //status = SafeNativeMethods.Gdip.GdipGetFontSize(new HandleRef(this, nativeFont), out size);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //    throw SafeNativeMethods.Gdip.StatusException(status);

            //status = SafeNativeMethods.Gdip.GdipGetFontStyle(new HandleRef(this, nativeFont), out style);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //    throw SafeNativeMethods.Gdip.StatusException(status);

            //status = SafeNativeMethods.Gdip.GdipGetFamily(new HandleRef(this, nativeFont), out nativeFamily);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //    throw SafeNativeMethods.Gdip.StatusException(status);

            //SetFontFamily(new FontFamily(nativeFamily));

            //Initialize(this.fontFamily, size, style, unit, gdiCharSet, gdiVerticalFont);
        }
        //private void Initialize(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        //{
        //    //this.originalFontName = familyName;

        //    //SetFontFamily(new FontFamily(StripVerticalName(familyName), true /* createDefaultOnFail */ ));
        //    //Initialize(this.fontFamily, emSize, style, unit, gdiCharSet, gdiVerticalFont);
        //}

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font FromHfont(IntPtr hfont)
        {
            throw new NotSupportedException();
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font FromLogFont(object lf)
        {
            throw new NotSupportedException();

        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font FromLogFont(object lf, IntPtr hdc)
        {
            throw new NotSupportedException();
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font FromHdc(IntPtr hdc)
        {
            throw new NotSupportedException();
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }


        internal IntPtr NativeFont
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        private FontFamily _Family = null;
        public FontFamily FontFamily
        {
            get
            {
                if(this._Family == null )
                {
                    this._Family = FontFamily.GetInstance(this._Name);
                }
                return this._Family;
            }
        }

        private void SetFontFamily(FontFamily family)
        {
            this._Name = family.Name;
        }


        public void Dispose()
        {
        }


        private static bool IsVerticalName(string familyName)
        {
            return familyName != null && familyName.Length > 0 && familyName[0] == '@';
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Bold
        {
            get
            {
                return (this.fontStyle & FontStyle.Bold) != 0;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public byte GdiCharSet
        {
            get
            {
                return gdiCharSet;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool GdiVerticalFont
        {
            get
            {
                return gdiVerticalFont;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Italic
        {
            get
            {
                return (this.fontStyle & FontStyle.Italic) != 0;
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Name
        {
            get 
            {
                return this._Name;// this.FontFamily.Name; 
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string OriginalFontName
        {
            get { return this._Name; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Strikeout
        {
            get
            {
                return (this.fontStyle & FontStyle.Strikeout) != 0;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Underline
        {
            get
            {
                return (this.fontStyle & FontStyle.Underline) != 0;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            Font font = obj as Font;

            if (font == null)
            {
                return false;
            }

            return font._Name == this._Name&&
                font.GdiVerticalFont == this.GdiVerticalFont &&
                font.GdiCharSet == this.GdiCharSet &&
                font.fontStyle == this.fontStyle &&
                font.Size == this.Size &&
                font.Unit == this.Unit;
        }



        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override int GetHashCode()
        {
            return ( this._Name == null ? 0 : this._Name.GetHashCode()) + unchecked((int)((((UInt32)fontStyle << 13) | ((UInt32)fontStyle >> 19)) ^
                         (((UInt32)fontUnit << 26) | ((UInt32)fontUnit >> 6)) ^
                         (((UInt32)fontSize << 7) | ((UInt32)fontSize >> 25))));
        }

        private static string StripVerticalName(string familyName)
        {
            if (familyName != null && familyName.Length > 1 && familyName[0] == '@')
            {
                return familyName.Substring(1);
            }
            return familyName;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override string ToString()
        {
            return string.Format("[{0}: Name={1}, Size={2}, Units={3}, GdiCharSet={4}, GdiVerticalFont={5}]",
                                    GetType().Name,
                                    this.FontFamily.Name,
                                    this.fontSize,
                                    (int)this.fontUnit,
                                    this.gdiCharSet,
                                    this.gdiVerticalFont);
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Text.Json.Nodes.JsonObject ToJsonObject()
        {
            var obj = new System.Text.Json.Nodes.JsonObject
            {
                ["Name"] = this.Name,
                ["Size"] = this.Size
            };
            if(this.Bold)
            {
                obj.Add("Bold", this.Bold);
            }
            if(this.Italic )
            {
                obj.Add("Italic", this.Italic);
            }
            if( this.Underline)
            {
                obj.Add("Underline", this.Underline);
            }
            if(this.Strikeout)
            {
                obj.Add("Strikeout", this.Strikeout);
            }
            if(this.Unit != GraphicsUnit.Point )
            {
                obj.Add("Unit", this.Unit.ToString());
            }
            return obj;
        }

        public void ToLogFont(object logFont)
        {
            throw new NotSupportedException();
            //IntPtr screenDC = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
            //try
            //{
            //    Graphics graphics = Graphics.FromHdcInternal(screenDC);

            //    try
            //    {
            //        this.ToLogFont(logFont, graphics);
            //    }
            //    finally
            //    {
            //        graphics.Dispose();
            //    }
            //}
            //finally
            //{
            //    UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, screenDC));
            //}
        }

        public unsafe void ToLogFont(object logFont, Graphics graphics)
        {
            throw new NotSupportedException();
            ////IntSecurity.ObjectFromWin32Handle.Demand();

            //if (graphics == null)
            //    throw new ArgumentNullException("graphics");

            //int status;

            //// handle proper marshalling of LogFontName as Unicode or ANSI
            //if (Marshal.SystemDefaultCharSize == 1)
            //    status = SafeNativeMethods.Gdip.GdipGetLogFontA(new HandleRef(this, this.NativeFont), new HandleRef(graphics, graphics.NativeGraphics), logFont);
            //else
            //    status = SafeNativeMethods.Gdip.GdipGetLogFontW(new HandleRef(this, this.NativeFont), new HandleRef(graphics, graphics.NativeGraphics), logFont);

            //// append "@" to the begining of the string if we are 
            //// a gdiVerticalFont.
            ////
            //if (gdiVerticalFont)
            //{
            //    if (Marshal.SystemDefaultCharSize == 1)
            //    {

            //        // copy contents of name, over 1 byte
            //        //
            //        for (int i = 30; i >= 0; i--)
            //        {
            //            Marshal.WriteByte(logFont,
            //                              LogFontNameOffset + i + 1,
            //                              Marshal.ReadByte(logFont, LogFontNameOffset + i));
            //        }

            //        // write ANSI '@' sign at begining of name
            //        //
            //        Marshal.WriteByte(logFont, LogFontNameOffset, (byte)(int)'@');
            //    }
            //    else
            //    {
            //        // copy contents of name, over 2 bytes (UNICODE)
            //        //
            //        for (int i = 60; i >= 0; i -= 2)
            //        {
            //            Marshal.WriteInt16(logFont,
            //                               LogFontNameOffset + i + 2,
            //                               Marshal.ReadInt16(logFont, LogFontNameOffset + i));
            //        }

            //        // write UNICODE '@' sign at begining of name
            //        //
            //        Marshal.WriteInt16(logFont, LogFontNameOffset, (short)'@');
            //    }
            //}

            //if (Marshal.ReadByte(logFont, LogFontCharSetOffset) == 0)
            //{
            //    Marshal.WriteByte(logFont, LogFontCharSetOffset, gdiCharSet);
            //}

            //if (status != SafeNativeMethods.Gdip.Ok)
            //    throw SafeNativeMethods.Gdip.StatusException(status);
        }

        public IntPtr ToHfont()
        {
            return IntPtr.Zero;
            //SafeNativeMethods.LOGFONT lf = new SafeNativeMethods.LOGFONT();

            ////IntSecurity.ObjectFromWin32Handle.Assert();

            //try {
            //    this.ToLogFont(lf);
            //}
            //finally {
            //    //System.Security.CodeAccessPermission.RevertAssert();
            //}

            //IntPtr handle = IntUnsafeNativeMethods.IntCreateFontIndirect(lf);

            //if (handle == IntPtr.Zero) {
            //    throw new Win32Exception();
            //}

            //return handle;

        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float GetHeight(Graphics graphics)
        {
            var f = this.FontFamily;
            if (f == null)
            {
                return DCSoft.GraphicsUnitConvert.Convert(
                    this.fontSize,
                    this.fontUnit,
                    graphics.PageUnit);
            }
            else
            {
                var result = ((float)f.GetLineSpacing(FontStyle.Regular) / (float)f.GetEmHeight(FontStyle.Regular)) * this.SizeInPoints;
                result = GraphicsUnitConvert.Convert(result, GraphicsUnit.Point, graphics.PageUnit);
                return result;
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float GetHeight()
        {
            var f = this.FontFamily;
            if (f == null)
            {
                return DCSoft.GraphicsUnitConvert.Convert(
                    this.fontSize,
                    this.fontUnit,
                    GraphicsUnit.Pixel);
            }
            else
            {
                var result = ((float)f.GetLineSpacing(FontStyle.Regular) / (float)f.GetEmHeight(FontStyle.Regular)) * this.SizeInPoints;
                result = GraphicsUnitConvert.Convert(result, GraphicsUnit.Point, GraphicsUnit.Pixel);
                return result;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float GetHeight(float dpi)
        {
            return this.GetHeight();
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public FontStyle Style
        {
            get
            {
                return fontStyle;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Size
        {
            get
            {
                return fontSize;
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SizeInPoints
        {
            get
            {
                if (Unit == GraphicsUnit.Point)
                {
                    return Size;
                }
                else
                {
                    return DCSoft.GraphicsUnitConvert.Convert(
                        this.fontSize,
                        this.Unit,
                        GraphicsUnit.Point);

                }
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public GraphicsUnit Unit
        {
            get
            {
                return fontUnit;
            }
        }
        public int Height
        {
            get
            {
                return (int)Math.Ceiling(GetHeight());
            }
        }

        public bool IsSystemFont
        {
            get
            {
                return false;// !String.IsNullOrEmpty(this.systemFontName);
            }
        }

        internal void SetSystemFontName(string systemFontName)
        {
            throw new NotSupportedException();
            //this.systemFontName = systemFontName;
        }
    }
}

