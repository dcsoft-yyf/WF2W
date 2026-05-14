//------------------------------------------------------------------------------
// <copyright file="SystemFonts.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing {

    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;
    using System.IO;
    using System.Runtime.Versioning;
    using System.Runtime.CompilerServices;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed class SystemFonts {
        private static readonly object SystemFontsKey = new object();

        // Cannot be instantiated.
        private SystemFonts() {
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font CaptionFont {
            get {
                return DefaultFont;
            }
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font SmallCaptionFont {
            get {
                return DefaultFont;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font MenuFont {
            get {
                return DefaultFont;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font StatusFont {
            get {
                return DefaultFont;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font MessageBoxFont {
            get {
                return DefaultFont;
            }
        }    
    
        /// <devdoc>
        ///     Determines whether the specified exception should be handled.
        /// </devdoc>
        private static bool IsCriticalFontException( Exception ex ){
            return !(
                // In any of these cases we'll handle the exception.
                ex is ExternalException         ||
                ex is ArgumentException         ||
                ex is OutOfMemoryException      || // GDI+ throws this one for many reasons other than actual OOM.
                ex is InvalidOperationException ||
                ex is NotImplementedException   ||
                ex is FileNotFoundException );
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font IconTitleFont {
            get {
                return DefaultFont;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                          //
        // SystemFonts.DefaultFont is code moved from System.Windows.Forms.Control.DefaultFont      //
        // System.Windows.Forms.Control.DefaultFont delegates to SystemFonts.DefaultFont now.       //
        // Treat any changes to this code as you would treat breaking changes.                      //
        //                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////
        internal static string _DefaultFontName = "ËÎÌו";
        internal static string DefaultFontName
        {
            get { return _DefaultFontName; }
        }
        private static Font _DefaultFont = new Font("ËÎÌו", 9);
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font DefaultFont {
            get {
                return _DefaultFont;
            }
            set
            {
                if(value != null )
                {
                    _DefaultFont = value;
                    _DefaultFontName = value.Name;
                }
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font DialogFont {
            get {
                Font dialogFont = null;

                if((UnsafeNativeMethods.GetSystemDefaultLCID() & 0x3ff) == 0x0011) {
                    // for JAPANESE culture always return DefaultFont
                    dialogFont = DefaultFont;
                } else if (Environment.OSVersion.Platform == System.PlatformID.Win32Windows) {
                    // use DefaultFont for Win9X
                    dialogFont = DefaultFont;
                } else  {
                    try {
                        // use MS Shell Dlg 2, 8pt for anything else than Japanese and Win9x
                        dialogFont = new Font("MS Shell Dlg 2", 8);
                    }
                    catch (ArgumentException) {
                    }
                }

                if (dialogFont == null) {
                    dialogFont = DefaultFont;
                } else if (dialogFont.Unit != GraphicsUnit.Point) {
                    dialogFont = FontInPoints(dialogFont);
                }

                //
                // JAPANESE or Win9x: SystemFonts.DefaultFont returns a new Font object every time it is invoked.
                // So for JAPANESE or Win9x we return the DefaultFont w/ its SystemFontName set to DialogFont.
                //
                dialogFont.SetSystemFontName("DialogFont");
                return dialogFont;
            }
        }

        //Copied from System.Windows.Forms.ControlPaint
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        private static Font FontInPoints(Font font) {
            return new Font(font.FontFamily, font.SizeInPoints, font.Style, GraphicsUnit.Point, font.GdiCharSet, font.GdiVerticalFont);
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Font GetFontByName(string systemFontName) {
            if ("CaptionFont".Equals(systemFontName)) {
                return CaptionFont;
            } else if ("DefaultFont".Equals(systemFontName)) {
                return DefaultFont;
            } else if ("DialogFont".Equals(systemFontName)) {
                return DialogFont;
            } else if ("IconTitleFont".Equals(systemFontName)) {
                return IconTitleFont;
            } else if ("MenuFont".Equals(systemFontName)) {
                return MenuFont;
            } else if ("MessageBoxFont".Equals(systemFontName)) {
                return MessageBoxFont;
            } else if ("SmallCaptionFont".Equals(systemFontName)) {
                return SmallCaptionFont;
            } else if ("StatusFont".Equals(systemFontName)) {
                return StatusFont;
            } else {
                return null;
            }
        }
    }
}

