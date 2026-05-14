//------------------------------------------------------------------------------
// <copyright file="SystemInformation.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms {
    using System.Text;
    using System.Configuration.Assemblies;
    using System.Threading;
    using System.Runtime.Remoting;
    using System.Runtime.InteropServices;

    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    using System;
    using Microsoft.Win32;
    using System.IO;
    using System.Security;
    using System.Security.Permissions;
    using System.Drawing;
    using System.ComponentModel;
    using System.Runtime.Versioning;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public static class SystemInformation {
        // 这里采用了Windows11台式电脑的默认值。
        // 这里采用部分属性替换成常量是为了帮助优化编译器进行代码内联和裁剪。
        public const bool DragFullWindows = true;
        public const bool HighContrast = false;
        public const int MouseWheelScrollLines = 3;
        public static System.Drawing.Size PrimaryMonitorSize { get { return new System.Drawing.Size(3440, 1440); } }
        public const int VerticalScrollBarWidth = 17;
        public const int HorizontalScrollBarHeight = 17;
        public const int CaptionHeight = 23;
        public static System.Drawing.Size BorderSize { get { return new System.Drawing.Size(1, 1); } }
        public static System.Drawing.Size FixedFrameBorderSize { get { return new System.Drawing.Size(3, 3); } }
        public const int VerticalScrollBarThumbHeight = 17;
        public const int HorizontalScrollBarThumbWidth = 17;
        public static System.Drawing.Size IconSize { get { return new System.Drawing.Size(32, 32); } }
        public static System.Drawing.Size CursorSize { get { return new System.Drawing.Size(32, 32); } }
        public static Font MenuFont { get { return System.Windows.Forms.Control.DefaultFont; } }// [Font: Name = Microsoft YaHei UI, Size = 12, Units = 0, GdiCharSet = 1, GdiVerticalFont = False]; } }
        public const int MenuHeight = 20;
        public static PowerStatus PowerStatus { get { return new System.Windows.Forms.PowerStatus(); } }
        public static Rectangle WorkingArea { get { return Screen.PrimaryScreen.WorkingArea; } }
        public const int KanjiWindowHeight = 0;
        public const bool MousePresent = true;
        public const int VerticalScrollBarArrowHeight = 17;
        public const int HorizontalScrollBarArrowWidth = 17;
        public const bool DebugOS = false;
        public const bool MouseButtonsSwapped = false;
        public static System.Drawing.Size MinimumWindowSize { get { return new System.Drawing.Size(136, 39); } }
        public static System.Drawing.Size CaptionButtonSize { get { return new System.Drawing.Size(36, 22); } }
        public static System.Drawing.Size FrameBorderSize { get { return new System.Drawing.Size(8, 8); } }
        public static System.Drawing.Size MinWindowTrackSize { get { return new System.Drawing.Size(136, 39); } }
        public static System.Drawing.Size DoubleClickSize { get { return new System.Drawing.Size(4, 4); } }
        public const int DoubleClickTime = 500;
        public static System.Drawing.Size IconSpacingSize { get { return new System.Drawing.Size(75, 75); } }
        public const bool RightAlignedMenus = false;
        public const bool PenWindows = false;
        public const bool DbcsEnabled = true;
        public const int MouseButtons = 16;
        public const bool Secure = false;
        public static System.Drawing.Size Border3DSize { get { return new System.Drawing.Size(2, 2); } }
        public static System.Drawing.Size MinimizedWindowSpacingSize { get { return new System.Drawing.Size(160, 28); } }
        public static System.Drawing.Size SmallIconSize { get { return new System.Drawing.Size(16, 16); } }
        public const int ToolWindowCaptionHeight = 23;
        public static System.Drawing.Size ToolWindowCaptionButtonSize { get { return new System.Drawing.Size(22, 22); } }
        public static System.Drawing.Size MenuButtonSize { get { return new System.Drawing.Size(19, 19); } }
        public const System.Windows.Forms.ArrangeStartingPosition ArrangeStartingPosition = System.Windows.Forms.ArrangeStartingPosition.Hide;
        public const System.Windows.Forms.ArrangeDirection ArrangeDirection = System.Windows.Forms.ArrangeDirection.Left;
        public static System.Drawing.Size MinimizedWindowSize { get { return new System.Drawing.Size(160, 28); } }
        public static System.Drawing.Size MaxWindowTrackSize { get { return new System.Drawing.Size(3460, 1460); } }
        public static System.Drawing.Size PrimaryMonitorMaximizedWindowSize { get { return new System.Drawing.Size(3456, 1408); } }
        public const bool Network = true;
        public const bool TerminalServerSession = false;
        public const System.Windows.Forms.BootMode BootMode = System.Windows.Forms.BootMode.Normal;
        public static System.Drawing.Size DragSize { get { return new System.Drawing.Size(4, 4); } }
        public const bool ShowSounds = false;
        public static System.Drawing.Size MenuCheckSize { get { return new System.Drawing.Size(17, 17); } }
        public const bool MidEastEnabled = false;
        public const bool NativeMouseWheelSupport = true;
        public const bool MouseWheelPresent = true;
        public static Rectangle VirtualScreen { get { return Screen.PrimaryScreen.Bounds ; } }
        public const int MonitorCount = 1;
        public const bool MonitorsSameDisplayFormat = true;
        public const string ComputerName = "DCSoft.YYF";
        public const string UserDomainName = "DCSoft.YYF";
        public const bool UserInteractive = true;
        public const string UserName = "yyf";
        public const bool IsDropShadowEnabled = true;
        public const bool IsFlatMenuEnabled = true;
        public const bool IsFontSmoothingEnabled = true;
        public const int FontSmoothingContrast = 1200;
        public const int FontSmoothingType = 2;
        public const int IconHorizontalSpacing = 75;
        public const int IconVerticalSpacing = 75;
        public const bool IsIconTitleWrappingEnabled = true;
        public const bool MenuAccessKeysUnderlined = false;
        public const int KeyboardDelay = 1;
        public const bool IsKeyboardPreferred = false;
        public const int KeyboardSpeed = 31;
        public static System.Drawing.Size MouseHoverSize { get { return new System.Drawing.Size(4, 4); } }
        public const int MouseHoverTime = 400;
        public const int MouseSpeed = 4;
        public const bool IsSnapToDefaultEnabled = false;
        public const System.Windows.Forms.LeftRightAlignment PopupMenuAlignment = System.Windows.Forms.LeftRightAlignment.Right;
        public const bool IsMenuFadeEnabled = true;
        public const int MenuShowDelay = 400;
        public const bool IsComboBoxAnimationEnabled = true;
        public const bool IsTitleBarGradientEnabled = true;
        public const bool IsHotTrackingEnabled = true;
        public const bool IsListBoxSmoothScrollingEnabled = true;
        public const bool IsMenuAnimationEnabled = true;
        public const bool IsSelectionFadeEnabled = true;
        public const bool IsToolTipAnimationEnabled = true;
        public const bool UIEffectsEnabled = true;
        public const bool IsActiveWindowTrackingEnabled = false;
        public const int ActiveWindowTrackingDelay = 0;
        public const bool IsMinimizeRestoreAnimationEnabled = false;
        public const int BorderMultiplierFactor = 1;
        public const int CaretBlinkTime = 530;
        public const int CaretWidth = 1;
        public const int MouseWheelScrollDelta = 120;
        public const int VerticalFocusThickness = 1;
        public const int HorizontalFocusThickness = 1;
        public const int VerticalResizeBorderThickness = 8;
        public const int HorizontalResizeBorderThickness = 8;
        public const System.Windows.Forms.ScreenOrientation ScreenOrientation = System.Windows.Forms.ScreenOrientation.Angle0;
        public const int SizingBorderWidth = 5;
        public static System.Drawing.Size SmallCaptionButtonSize { get { return new System.Drawing.Size(22, 22); } }
        public static System.Drawing.Size MenuBarButtonSize { get { return new System.Drawing.Size(19, 19); } }
        //        // private constructor to prevent creation
        //        //
        //        private SystemInformation() {
        //        }

        //        // Figure out if all the multimon stuff is supported on the OS
        //        //
        //        private static bool checkMultiMonitorSupport = false;
        //        private static bool multiMonitorSupport = false;
        //        private static bool checkNativeMouseWheelSupport = false;
        //        private static bool nativeMouseWheelSupport = true;
        //        private static bool highContrast = false;
        //        private static bool systemEventsAttached = false;
        //        private static bool systemEventsDirty = true;

        //        private static IntPtr processWinStation = IntPtr.Zero;
        //        private static bool isUserInteractive = false;

        //        private static PowerStatus powerStatus = null;

        //        private const int  DefaultMouseWheelScrollLines = 3;

        //        ////////////////////////////////////////////////////////////////////////////
        //        // SystemParametersInfo
        //        //

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.DragFullWindows"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the user has enabled full window drag.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool DragFullWindows {
        //            get {
        //                int data = 0;
        //                WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETDRAGFULLWINDOWS, 0, ref data, 0);
        //                return data != 0;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.HighContrast"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the user has selected to run in high contrast
        //        ///       mode.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool HighContrast {
        //            get {
        //                EnsureSystemEvents();
        //                if (systemEventsDirty) {
        //                    WinFormNativeMethods.HIGHCONTRAST_I data = new WinFormNativeMethods.HIGHCONTRAST_I();
        //                    data.cbSize = Marshal.SizeOf(data);
        //                    data.dwFlags = 0;
        //                    data.lpszDefaultScheme = IntPtr.Zero;

        //                    bool b = WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETHIGHCONTRAST, data.cbSize, ref data, 0);

        //                    // NT4 does not support this parameter, so we always force
        //                    // it to false if we fail to get the parameter.
        //                    //
        //                    if (b) {
        //                        highContrast = (data.dwFlags & WinFormNativeMethods.HCF_HIGHCONTRASTON) != 0;
        //                    }
        //                    else {
        //                        highContrast = false;
        //                    }
        //                    systemEventsDirty = false;
        //                }

        //                return highContrast;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MouseWheelScrollLines"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the number of lines to scroll when the mouse wheel is rotated.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int MouseWheelScrollLines {
        //            get {
        //                if (NativeMouseWheelSupport) {
        //                    int data = 0;
        //                    WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETWHEELSCROLLLINES, 0, ref data, 0);
        //                    return data;
        //                }
        //                else {
        //                    IntPtr hWndMouseWheel = IntPtr.Zero;

        //                    // Check for the MouseZ "service". This is a little app that generated the
        //                    // MSH_MOUSEWHEEL messages by monitoring the hardware. If this app isn't
        //                    // found, then there is no support for MouseWheels on the system.
        //                    //
        //                    hWndMouseWheel = WinFormUnsafeNativeMethods.FindWindow(WinFormNativeMethods.MOUSEZ_CLASSNAME, WinFormNativeMethods.MOUSEZ_TITLE);

        //                    if (hWndMouseWheel != IntPtr.Zero) {

        //                        // Register the MSH_SCROLL_LINES message...
        //                        //
        //                        int message = WinFormSafeNativeMethods.RegisterWindowMessage(WinFormNativeMethods.MSH_SCROLL_LINES);


        //                        int lines = (int)WinFormUnsafeNativeMethods.SendMessage(new HandleRef(null, hWndMouseWheel), message, 0, 0);

        //                        // this fails under terminal server, so we default to 3, which is the windows
        //                        // default.  Nobody seems to pay attention to this value anyways...
        //                        if (lines != 0) {
        //                            return lines;
        //                        }
        //                    }
        //                }

        //                return DefaultMouseWheelScrollLines;
        //            }
        //        }

        //        ////////////////////////////////////////////////////////////////////////////
        //        // SystemMetrics
        //        //

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.PrimaryMonitorSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the dimensions of the primary display monitor in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size PrimaryMonitorSize {
        //            get {
        //                // Windows 11 typical primary monitor default (1920x1080)
        //                return Screen.PrimaryScreen.Bounds.Size;// new Size(1920, 1080);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.VerticalScrollBarWidth"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the width of the vertical scroll bar in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int VerticalScrollBarWidth {
        //            get {
        //                // Windows 11 default scrollbar width
        //                return 17;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.HorizontalScrollBarHeight"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the height of the horizontal scroll bar in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int HorizontalScrollBarHeight {
        //            get {
        //                // Windows 11 default scrollbar height
        //                return 17;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.CaptionHeight"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the height of the normal caption area of a window in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int CaptionHeight {
        //            get {
        //                // Windows 11 default title bar height
        //                return 32;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.BorderSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the width and
        //        ///       height of a window border in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size BorderSize {
        //            get {
        //                // Windows 11 border size
        //                return new Size(1, 1);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.FixedFrameBorderSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the thickness in pixels, of the border for a window that has a caption
        //        ///       and is not resizable.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size FixedFrameBorderSize {
        //            get {
        //                // Windows 11 fixed frame border size
        //                return new Size(3, 3);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.VerticalScrollBarThumbHeight"]/*' />
        //        /// <devdoc>
        //        ///    <para>Gets the height of the scroll box in a vertical scroll bar in pixels.</para>
        //        /// </devdoc>
        //        public static int VerticalScrollBarThumbHeight {
        //            get {
        //                // Approximate default thumb height
        //                return 40;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.HorizontalScrollBarThumbWidth"]/*' />
        //        /// <devdoc>
        //        ///    <para>Gets the width of the scroll box in a horizontal scroll bar in pixels.</para>
        //        /// </devdoc>
        //        public static int HorizontalScrollBarThumbWidth {
        //            get {
        //                // Approximate default thumb width
        //                return 40;
        //            }
        //        }

        ///*
        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IconFont"]/*' />
        //        public static Font IconFont {
        //            get {
        //                Font iconFont = IconFontInternal;
        //                return iconFont != null ? iconFont : Control.DefaultFont;
        //            }
        //        }

        //        // IconFontInternal is the same as IconFont, only it does not fall back to Control.DefaultFont
        //        // if the icon font can not be retrieved.  It returns null instead.
        //        internal static Font IconFontInternal {
        //            get {
        //                Font iconFont = null;

        //                NativeMethods.ICONMETRICS data = new NativeMethods.ICONMETRICS();
        //                bool result = UnsafeNativeMethods.SystemParametersInfo(NativeMethods.SPI_GETICONMETRICS, data.cbSize, data, 0);

        //                Debug.Assert(!result || data.iHorzSpacing == IconHorizontalSpacing, "Spacing in ICONMETRICS does not match IconHorizontalSpacing.");
        //                Debug.Assert(!result || data.iVertSpacing == IconVerticalSpacing, "Spacing in ICONMETRICS does not match IconVerticalSpacing.");

        //                if (result && data.lfFont != null) {
        //                    try {
        //                        iconFont = Font.FromLogFont(data.lfFont);
        //                    }
        //                    catch {}
        //                }
        //                return iconFont;
        //            }
        //        }
        //*/

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IconSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the default dimensions of an icon in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size IconSize {
        //            get {
        //                // Windows 11 default icon size
        //                return new Size(32, 32);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.CursorSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the dimensions of a cursor in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size CursorSize {
        //            get {
        //                // Windows default cursor size
        //                return new Size(32, 32);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MenuFont"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the system's font for menus.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Font MenuFont {
        //            [ResourceExposure(ResourceScope.Process)]
        //            [ResourceConsumption(ResourceScope.Process)]
        //            get {
        //                return SystemFonts.DefaultFont;
        //                //Font menuFont = null;

        //                ////we can get the system's menu font through the NONCLIENTMETRICS structure via SystemParametersInfo
        //                ////
        //                //WinFormNativeMethods.NONCLIENTMETRICS data = new WinFormNativeMethods.NONCLIENTMETRICS();
        //                //bool result = WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETNONCLIENTMETRICS, data.cbSize, data, 0);

        //                //if (result && data.lfMenuFont != null) {
        //                //    // SECREVIEW : This assert is safe since we created the NONCLIENTMETRICS struct.
        //                //    //
        //                //    //IntSecurity.ObjectFromWin32Handle.Assert();
        //                //    try {
        //                //        menuFont = Font.FromLogFont(data.lfMenuFont);
        //                //    }
        //                //    catch {
        //                //        // menu font is not true type.  Default to standard control font.
        //                //        //
        //                //        menuFont = Control.DefaultFont;
        //                //    }
        //                //    finally {
        //                //        //CodeAccessPermission.RevertAssert();
        //                //    }
        //                //}
        //                //return menuFont;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MenuHeight"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the height of a one line of a menu in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int MenuHeight {
        //            get {
        //                // Windows 11 default menu height
        //                return 19;
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.PowerStatus"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Returns the current system power status.        
        //        ///    </para>
        //        /// </devdoc>
        //        public static PowerStatus PowerStatus
        //        {
        //            get
        //            {
        //                if (powerStatus == null) {
        //                    powerStatus = new PowerStatus();
        //                }
        //                return powerStatus;
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.WorkingArea"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the size of the working area in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Rectangle WorkingArea {
        //            get {
        //                // Windows 11 typical taskbar height ~ 48; working area is screen minus taskbar
        //                return Screen.PrimaryScreen.WorkingArea;// new Rectangle(0, 0, 1920, 1080 - 48);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.KanjiWindowHeight"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets
        //        ///       the height, in pixels, of the Kanji window at the bottom
        //        ///       of the screen for double-byte (DBCS) character set versions of Windows.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int KanjiWindowHeight {
        //            get {
        //                return 0;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MousePresent"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the system has a mouse installed.
        //        ///    </para>
        //        /// </devdoc>
        //        [EditorBrowsable(EditorBrowsableState.Never)]
        //        public static bool MousePresent {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.VerticalScrollBarArrowHeight"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the height in pixels, of the arrow bitmap on the vertical scroll bar.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int VerticalScrollBarArrowHeight {
        //            get {
        //                return 17;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.HorizontalScrollBarArrowWidth"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the width, in pixels, of the arrow bitmap on the horizontal scrollbar.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int HorizontalScrollBarArrowWidth {
        //            get {
        //                return 17;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.DebugOS"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether this is a debug version of the operating
        //        ///       system.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool DebugOS {
        //            get {
        //                return false;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MouseButtonsSwapped"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the functions of the left and right mouse
        //        ///       buttons have been swapped.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool MouseButtonsSwapped {
        //            get {
        //                return false;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MinimumWindowSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the minimum allowable dimensions of a window in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size MinimumWindowSize {
        //            get {
        //                return new Size(112, 34);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.CaptionButtonSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the dimensions in pixels, of a caption bar or title bar
        //        ///       button.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size CaptionButtonSize {
        //            get {
        //                return new Size(46, 32);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.FrameBorderSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the thickness in pixels, of the border for a window that can be resized.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size FrameBorderSize {
        //            get {
        //                return new Size(8, 8);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MinWindowTrackSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the system's default
        //        ///       minimum tracking dimensions of a window in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size MinWindowTrackSize {
        //            get {
        //                return new Size(112, 34);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.DoubleClickSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the dimensions in pixels, of the area that the user must click within
        //        ///       for the system to consider the two clicks a double-click. The rectangle is
        //        ///       centered around the first click.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size DoubleClickSize {
        //            get {
        //                return new Size(4, 4);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.DoubleClickTime"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the maximum number of milliseconds allowed between mouse clicks for a
        //        ///       double-click.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int DoubleClickTime {
        //            get {
        //                return 500;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IconSpacingSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets
        //        ///       the dimensions in pixels, of the grid used
        //        ///       to arrange icons in a large icon view.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size IconSpacingSize {
        //            get {
        //                return new Size(75, 75);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.RightAlignedMenus"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether drop down menus should be right-aligned with
        //        ///       the corresponding menu bar item.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool RightAlignedMenus {
        //            get {
        //                return false;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.PenWindows"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the Microsoft Windows for Pen computing
        //        ///       extensions are installed.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool PenWindows {
        //            get {
        //                return false;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.DbcsEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the operating system is capable of handling
        //        ///       double-byte (DBCS) characters.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool DbcsEnabled {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MouseButtons"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the number of buttons on mouse.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int MouseButtons {
        //            get {
        //                return 3;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.Secure"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether security is present on this operating system.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool Secure {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.Border3DSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the dimensions in pixels, of a 3-D
        //        ///       border.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size Border3DSize {
        //            get {
        //                return new Size(2, 2);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MinimizedWindowSpacingSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>Gets the dimensions
        //        ///       in pixels, of
        //        ///       the grid into which minimized windows will be placed.</para>
        //        /// </devdoc>
        //        public static Size MinimizedWindowSpacingSize {
        //            get {
        //                return new Size(160, 28);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.SmallIconSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets
        //        ///       the recommended dimensions of a small icon in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size SmallIconSize {
        //            get {
        //                return new Size(16, 16);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.ToolWindowCaptionHeight"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the height of
        //        ///       a small caption in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int ToolWindowCaptionHeight {
        //            get {
        //                return 23;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.ToolWindowCaptionButtonSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the
        //        ///       dimensions of small caption buttons in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size ToolWindowCaptionButtonSize {
        //            get {
        //                return new Size(16, 16);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MenuButtonSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets
        //        ///       the dimensions in pixels, of menu bar buttons.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size MenuButtonSize {
        //            get {
        //                return new Size(19, 19);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.ArrangeStartingPosition"]/*' />
        //        /// <devdoc>
        //        ///    <para>Gets flags specifying how the system arranges minimized windows.</para>
        //        /// </devdoc>
        //        public static ArrangeStartingPosition ArrangeStartingPosition {
        //            get {
        //                return ArrangeStartingPosition.BottomLeft;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.ArrangeDirection"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets flags specifying how the system arranges minimized windows.
        //        ///    </para>
        //        /// </devdoc>
        //        public static ArrangeDirection ArrangeDirection {
        //            get {
        //                return ArrangeDirection.Down;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MinimizedWindowSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the dimensions in pixels, of a normal minimized window.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size MinimizedWindowSize {
        //            get {
        //                return new Size(160, 28);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MaxWindowTrackSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the default maximum dimensions in pixels, of a
        //        ///       window that has a caption and sizing borders.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size MaxWindowTrackSize {
        //            get {
        //                return new Size(8192, 8192);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.PrimaryMonitorMaximizedWindowSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the default dimensions, in pixels, of a maximized top-left window on the
        //        ///       primary monitor.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size PrimaryMonitorMaximizedWindowSize {
        //            get {
        //                return WorkingArea.Size;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.Network"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether this computer is connected to a network.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool Network {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.TerminalServerSession"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       To be supplied.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool TerminalServerSession {
        //            get {
        //                return false;
        //            }
        //        }



        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.BootMode"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value that specifies how the system was started.
        //        ///    </para>
        //        /// </devdoc>
        //        public static BootMode BootMode {
        //            get {
        //                return BootMode.Normal;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.DragSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the dimensions in pixels, of the rectangle that a drag operation
        //        ///       must extend to be considered a drag. The rectangle is centered on a drag
        //        ///       point.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size DragSize {
        //            get {
        //                return new Size(4, 4);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.ShowSounds"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the user requires an application to present
        //        ///       information visually in situations where it would otherwise present the
        //        ///       information in audible form.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool ShowSounds {
        //            get {
        //                return false;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MenuCheckSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the
        //        ///       dimensions of the default size of a menu checkmark in pixels.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size MenuCheckSize {
        //            get {
        //                return new Size(16, 16);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MidEastEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value
        //        ///       indicating whether the system is enabled for Hebrew and Arabic languages.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool MidEastEnabled {
        //            get {
        //                return false;
        //            }
        //        }

        //        private static bool MultiMonitorSupport {
        //            get {
        //                if (!checkMultiMonitorSupport) {
        //                    multiMonitorSupport = (WinFormUnsafeNativeMethods.GetSystemMetrics(WinFormNativeMethods.SM_CMONITORS) != 0);
        //                    checkMultiMonitorSupport = true;
        //                }
        //                return multiMonitorSupport;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.NativeMouseWheelSupport"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the system natively supports the mouse wheel
        //        ///       in newer mice.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool NativeMouseWheelSupport {
        //            get {
        //                if (!checkNativeMouseWheelSupport) {
        //                    nativeMouseWheelSupport = (WinFormUnsafeNativeMethods.GetSystemMetrics(WinFormNativeMethods.SM_MOUSEWHEELPRESENT) != 0);
        //                    checkNativeMouseWheelSupport = true;
        //                }
        //                return nativeMouseWheelSupport;
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MouseWheelPresent"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether there is a mouse with a mouse wheel
        //        ///       installed on this machine.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool MouseWheelPresent {
        //            get {

        //                bool mouseWheelPresent = false;

        //                if (!NativeMouseWheelSupport) {
        //                    IntPtr hWndMouseWheel = IntPtr.Zero;

        //                    // Check for the MouseZ "service". This is a little app that generated the
        //                    // MSH_MOUSEWHEEL messages by monitoring the hardware. If this app isn't
        //                    // found, then there is no support for MouseWheels on the system.
        //                    //
        //                    hWndMouseWheel = WinFormUnsafeNativeMethods.FindWindow(WinFormNativeMethods.MOUSEZ_CLASSNAME, WinFormNativeMethods.MOUSEZ_TITLE);

        //                    if (hWndMouseWheel != IntPtr.Zero) {
        //                        mouseWheelPresent = true;
        //                    }
        //                }
        //                else {
        //                    mouseWheelPresent = (WinFormUnsafeNativeMethods.GetSystemMetrics(WinFormNativeMethods.SM_MOUSEWHEELPRESENT) != 0);
        //                }
        //                return mouseWheelPresent;
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.VirtualScreen"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the
        //        ///       bounds of the virtual screen.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Rectangle VirtualScreen {
        //            get {
        //                if (MultiMonitorSupport) {
        //                    return new Rectangle(WinFormUnsafeNativeMethods.GetSystemMetrics(WinFormNativeMethods.SM_XVIRTUALSCREEN),
        //                                         WinFormUnsafeNativeMethods.GetSystemMetrics(WinFormNativeMethods.SM_YVIRTUALSCREEN),
        //                                         WinFormUnsafeNativeMethods.GetSystemMetrics(WinFormNativeMethods.SM_CXVIRTUALSCREEN),
        //                                         WinFormUnsafeNativeMethods.GetSystemMetrics(WinFormNativeMethods.SM_CYVIRTUALSCREEN));
        //                }
        //                else {
        //                    Size size = PrimaryMonitorSize;
        //                    return new Rectangle(0, 0, size.Width, size.Height);
        //                }
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MonitorCount"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the number of display monitors on the desktop.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int MonitorCount {
        //            get {
        //                if (MultiMonitorSupport) {
        //                    return WinFormUnsafeNativeMethods.GetSystemMetrics(WinFormNativeMethods.SM_CMONITORS);
        //                }
        //                else {
        //                    return 1;
        //                }
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MonitorsSameDisplayFormat"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether all the display monitors have the
        //        ///       same color format.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool MonitorsSameDisplayFormat {
        //            get {
        //                if (MultiMonitorSupport) {
        //                    return WinFormUnsafeNativeMethods.GetSystemMetrics(WinFormNativeMethods.SM_SAMEDISPLAYFORMAT) != 0;
        //                }
        //                else {
        //                    return true;
        //                }
        //            }
        //        }

        //        ////////////////////////////////////////////////////////////////////////////
        //        // Misc
        //        //


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.ComputerName"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the computer name of the current system.
        //        ///    </para>
        //        /// </devdoc>
        //        public static string ComputerName {
        //            get {
        //                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "SensitiveSystemInformation Demanded");
        //                //IntSecurity.SensitiveSystemInformation.Demand();

        //                StringBuilder sb = new StringBuilder(256);
        //                WinFormUnsafeNativeMethods.GetComputerName(sb, new int[] {sb.Capacity});
        //                return sb.ToString();
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.UserDomainName"]/*' />
        //        /// <devdoc>
        //        ///    Gets the user's domain name.
        //        /// </devdoc>
        //        public static string UserDomainName {
        //            get {
        //                return Environment.UserDomainName;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.UserInteractive"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the current process is running in user
        //        ///       interactive mode.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool UserInteractive {
        //            get {
        //                if (Environment.OSVersion.Platform == System.PlatformID.Win32NT) {
        //                    IntPtr hwinsta = IntPtr.Zero;

        //                    hwinsta = WinFormUnsafeNativeMethods.GetProcessWindowStation();
        //                    if (hwinsta != IntPtr.Zero && processWinStation != hwinsta) {
        //                        isUserInteractive = true;

        //                        int lengthNeeded = 0;
        //                        WinFormNativeMethods.USEROBJECTFLAGS flags = new WinFormNativeMethods.USEROBJECTFLAGS();

        //                        if (WinFormUnsafeNativeMethods.GetUserObjectInformation(new HandleRef(null, hwinsta), WinFormNativeMethods.UOI_FLAGS, flags, Marshal.SizeOf(flags), ref lengthNeeded)) {
        //                            if ((flags.dwFlags & WinFormNativeMethods.WSF_VISIBLE) == 0) {
        //                                isUserInteractive = false;
        //                            }
        //                        }
        //                        processWinStation = hwinsta;
        //                    }
        //                }
        //                else {
        //                    isUserInteractive = true;
        //                }
        //                return isUserInteractive;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.UserName"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the user name for the current thread, that is, the name of the
        //        ///       user currently logged onto the system.
        //        ///    </para>
        //        /// </devdoc>
        //        public static string UserName {
        //            get {
        //                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "SensitiveSystemInformation Demanded");
        //                //IntSecurity.SensitiveSystemInformation.Demand();

        //                StringBuilder sb = new StringBuilder(256);
        //                WinFormUnsafeNativeMethods.GetUserName(sb, new int[] {sb.Capacity});
        //                return sb.ToString();
        //            }
        //        }

        //        private static void EnsureSystemEvents() {
        //            if (!systemEventsAttached) {
        //                SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(SystemInformation.OnUserPreferenceChanged);
        //                systemEventsAttached = true;
        //            }
        //        }

        //        private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref) {
        //            systemEventsDirty = true;
        //        }

        //        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        // NEW ADDITIONS FOR WHIDBEY                                                                            //
        //        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsDropShadowEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the drop shadow effect in enabled. Defaults to false 
        //        ///       downlevel.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsDropShadowEnabled {
        //            get {
        //                if (OSFeature.Feature.OnXp) {
        //                    int data = 0;
        //                    WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETDROPSHADOW, 0, ref data, 0);
        //                    return data != 0;
        //                }
        //                return false;
        //           }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsFlatMenuEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the native user menus have a flat menu appearance. Defaults to false 
        //        ///       downlevel.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsFlatMenuEnabled {
        //            get {
        //                if (OSFeature.Feature.OnXp) {
        //                    int data = 0;
        //                    WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETFLATMENU, 0, ref data, 0);
        //                    return data != 0;
        //                }
        //                return false;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsFontSmoothingEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets a value indicating whether the Font Smoothing OSFeature.Feature is enabled. 
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsFontSmoothingEnabled {
        //            get {
        //                int data = 0;
        //                WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETFONTSMOOTHING, 0, ref data, 0);
        //                return data != 0;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.FontSmoothingContrast"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Returns a contrast value that is ClearType smoothing.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int FontSmoothingContrast {
        //            get {
        //                if (OSFeature.Feature.OnXp) {
        //                    int data = 0;
        //                    WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETFONTSMOOTHINGCONTRAST, 0, ref data, 0);
        //                    return data;
        //                }
        //                else {
        //                    throw new NotSupportedException(SR.GetString(SR.SystemInformationFeatureNotSupported));
        //                }
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.FontSmoothingType"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Returns a type of Font smoothing.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int FontSmoothingType {
        //            get {
        //                if (OSFeature.Feature.OnXp) {
        //                    int data = 0;
        //                    WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETFONTSMOOTHINGTYPE, 0, ref data, 0);
        //                    return data;
        //                }
        //                else {
        //                    throw new NotSupportedException(SR.GetString(SR.SystemInformationFeatureNotSupported));
        //                }
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IconHorizontalSpacing"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Retrieves the width in pixels of an icon cell.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int IconHorizontalSpacing {
        //            get {
        //                return 75;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IconVerticalSpacing"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Retrieves the height in pixels of an icon cell.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int IconVerticalSpacing {
        //            get {
        //                return 75;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsIconTitleWrappingEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Gets a value indicating whether the Icon title wrapping is enabled.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsIconTitleWrappingEnabled {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MenuAccessKeysUnderlined"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Gets a value indicating whether the menu access keys are always underlined.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool MenuAccessKeysUnderlined {
        //            get {
        //                return false;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.KeyboardDelay"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Retrieves the Keyboard repeat delay setting, which is a value in the 
        //        ///       range from 0 through 3. The Actual Delay Associated with each value may vary depending on the 
        //        ///       hardware.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int KeyboardDelay {
        //            get {
        //                return 1;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsKeyboardPreferred"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Gets a value indicating whether the user relies on Keyboard instead of mouse and wants 
        //        ///      applications to display keyboard interfaces that would be otherwise hidden.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsKeyboardPreferred {
        //            get {
        //                return false;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.KeyboardSpeed"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Retrieves the Keyboard repeat speed setting, which is a value in the 
        //        ///       range from 0 through 31. The actual rate may vary depending on the 
        //        ///       hardware.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int KeyboardSpeed {
        //            get {
        //                return 31;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MouseHoverSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the Size in pixels of the rectangle within which the mouse pointer has to stay.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size MouseHoverSize {
        //            get {
        //                return new Size(4, 4);
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MouseHoverTime"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int MouseHoverTime {
        //            get {
        //                return 400;
        //            }

        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MouseSpeed"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Gets the current mouse speed.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int MouseSpeed {
        //            get {
        //                return 10;
        //            }

        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsSnapToDefaultEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Determines whether the snap-to-default-button feature is enabled.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsSnapToDefaultEnabled {
        //            get {
        //                return false;
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.PopupMenuAlignment"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Determines whether the Popup Menus are left Aligned or Right Aligned.
        //        ///    </para>
        //        /// </devdoc>
        //        public static LeftRightAlignment PopupMenuAlignment {
        //            get {
        //                return LeftRightAlignment.Left;

        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsMenuFadeEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Determines whether the maenu fade animation feature is enabled. Defaults to false 
        //        ///       downlevel.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsMenuFadeEnabled {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MenuShowDelay"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Indicates the time, in milliseconds, that the system waits before displaying a shortcut menu.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int MenuShowDelay {
        //            get {
        //                return 400;
        //            }

        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsComboBoxAnimationEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether the slide open effect for combo boxes is enabled.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsComboBoxAnimationEnabled {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsTitleBarGradientEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether the gradient effect for windows title bars is enabled.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsTitleBarGradientEnabled {
        //            get {
        //                return true;
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsHotTrackingEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether the hot tracking of user interface elements is enabled.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsHotTrackingEnabled {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsListBoxSmoothScrollingEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether the smooth scrolling effect for listbox is enabled.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsListBoxSmoothScrollingEnabled {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsMenuAnimationEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether the menu animation feature is enabled.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsMenuAnimationEnabled {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsSelectionFadeEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether the selection fade effect is enabled. Defaults to false 
        //        ///       downlevel.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsSelectionFadeEnabled {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsToolTipAnimationEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether the tool tip animation is enabled. Defaults to false 
        //        ///      downlevel.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsToolTipAnimationEnabled {
        //            get {
        //                return true;

        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.UIEffectsEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether all the UI Effects are enabled. Defaults to false 
        //        ///      downlevel.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool UIEffectsEnabled {
        //            get {
        //                return true;
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsActiveWindowTrackingEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether the active windows tracking (activating the window the mouse in on) is ON or OFF.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsActiveWindowTrackingEnabled {
        //            get {
        //                return false;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.ActiveWindowTrackingDelay"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Retrieves the active window tracking delay, in milliseconds.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int ActiveWindowTrackingDelay {
        //            get {
        //                return 0;
        //            }

        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.IsMinimizeRestoreAnimationEnabled"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Indicates whether the active windows tracking (activating the window the mouse in on) is ON or OFF.
        //        ///    </para>
        //        /// </devdoc>
        //        public static bool IsMinimizeRestoreAnimationEnabled {
        //            get {
        //                return true;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.BorderMultiplierFactor"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Retrieves the border multiplier factor that determines the width of a windo's sizing border.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int BorderMultiplierFactor {
        //            get {
        //                return 1;
        //            }

        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.CaretBlinkTime"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Indicates the caret blink time.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int CaretBlinkTime {
        //            get {
        //                return 530;
        //            }

        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.CaretWidth"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       Indicates the caret width in edit controls.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int CaretWidth {
        //            get {
        //                return 1;
        //            }

        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MouseWheelScrollDelta"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       None.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int MouseWheelScrollDelta {
        //            get {
        //                return WinFormNativeMethods.WHEEL_DELTA;
        //            }

        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.VerticalFocusThickness"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       The width of the left and right edges of the focus rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int VerticalFocusThickness {
        //            get {
        //                return 1;
        //            }

        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.HorizontalFocusThickness"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       The width of the top and bottom edges of the focus rectangle.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int HorizontalFocusThickness {
        //            get {
        //                return 1;
        //            }

        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.VerticalResizeBorderThickness"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       The height of the vertical sizing border around the perimeter of the window that can be resized.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int VerticalResizeBorderThickness {
        //            get {
        //                return 8;
        //            }

        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.HorizontalResizeBorderThickness"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       The width of the horizontal sizing border around the perimeter of the window that can be resized.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int HorizontalResizeBorderThickness {
        //            get {
        //                return 8;
        //            }

        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.ScreenOrientation"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///       The orientation of the screen in degrees.
        //        ///    </para>
        //        /// </devdoc>
        //        public static ScreenOrientation ScreenOrientation {
        //            get {
        //                return ScreenOrientation.Angle0;
        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.SizingBorderWidth"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Specifies the thikness, in pixels, of the Sizing Border.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int SizingBorderWidth {
        //            get {
        //                return 1;
        //            }
        //        }

        //        /*
        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.VerticalScrollBarWidth"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Specified the width, in pixels, of a standard vertical scrollbar.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int VerticalScrollBarWidth {
        //            get {

        //                //we can get the system's menu font through the NONCLIENTMETRICS structure via SystemParametersInfo
        //                //
        //                NativeMethods.NONCLIENTMETRICS data = new NativeMethods.NONCLIENTMETRICS();
        //                UnsafeNativeMethods.SystemParametersInfo(NativeMethods.SPI_GETNONCLIENTMETRICS, data.cbSize, data, 0);
        //                if (result && data.iScrollWidth > 0) {
        //                    return data.iScrollWidth;
        //                }
        //                else {
        //                    return 0;
        //                }


        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.HorizontalScrollBarWidth"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Specified the height, in pixels, of a standard horizontal scrollbar.
        //        ///    </para>
        //        /// </devdoc>
        //        public static int HorizontalScrollBarWidth {
        //            get {

        //                //we can get the system's menu font through the NONCLIENTMETRICS structure via SystemParametersInfo
        //                //
        //                NativeMethods.NONCLIENTMETRICS data = new NativeMethods.NONCLIENTMETRICS();
        //                UnsafeNativeMethods.SystemParametersInfo(NativeMethods.SPI_GETNONCLIENTMETRICS, data.cbSize, data, 0);
        //                if (result && data.iScrollHeight > 0) {
        //                    return data.iScrollHeight;
        //                }
        //                else {
        //                    return 0;
        //                }
        //            }
        //        }


        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.CaptionButtonSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Specified the Size, in pixels, of the caption buttons.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size CaptionButtonSize {
        //            get {

        //                //we can get the system's menu font through the NONCLIENTMETRICS structure via SystemParametersInfo
        //                //
        //                NativeMethods.NONCLIENTMETRICS data = new NativeMethods.NONCLIENTMETRICS();
        //                UnsafeNativeMethods.SystemParametersInfo(NativeMethods.SPI_GETNONCLIENTMETRICS, data.cbSize, data, 0);
        //                if (result && data.iCaptionHeight > 0 && data.iCaptionWidth > 0) {
        //                    return new Size(data.iCaptionWidth, data.iCaptionHeight);
        //                }
        //                else {
        //                    return return new Size(0, 0);
        //                }


        //            }
        //        }
        //        */

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.SmallCaptionButtonSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Specified the Size, in pixels, of the small caption buttons.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size SmallCaptionButtonSize {
        //            get {

        //                //we can get the system's menu font through the NONCLIENTMETRICS structure via SystemParametersInfo
        //                //
        //                WinFormNativeMethods.NONCLIENTMETRICS data = new WinFormNativeMethods.NONCLIENTMETRICS();
        //                bool result = WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETNONCLIENTMETRICS, data.cbSize, data, 0);
        //                if (result && data.iSmCaptionHeight > 0 && data.iSmCaptionWidth > 0) {
        //                    return new Size(data.iSmCaptionWidth, data.iSmCaptionHeight);
        //                }
        //                else {
        //                    return Size.Empty;
        //                }


        //            }
        //        }

        //        /// <include file='doc\SystemInformation.uex' path='docs/doc[@for="SystemInformation.MenuBarButtonSize"]/*' />
        //        /// <devdoc>
        //        ///    <para>
        //        ///      Specified the Size, in pixels, of the menu bar buttons.
        //        ///    </para>
        //        /// </devdoc>
        //        public static Size MenuBarButtonSize {
        //            get {

        //                //we can get the system's menu font through the NONCLIENTMETRICS structure via SystemParametersInfo
        //                //
        //                WinFormNativeMethods.NONCLIENTMETRICS data = new WinFormNativeMethods.NONCLIENTMETRICS();
        //                bool result = WinFormUnsafeNativeMethods.SystemParametersInfo(WinFormNativeMethods.SPI_GETNONCLIENTMETRICS, data.cbSize, data, 0);
        //                if (result && data.iMenuHeight > 0 && data.iMenuWidth > 0) {
        //                    return new Size(data.iMenuWidth, data.iMenuHeight);
        //                }
        //                else {
        //                    return Size.Empty;
        //                }


        //            }
        //        }

        //        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        // End ADDITIONS FOR WHIDBEY                                                                            //
        //        //////////////////////////////////////////////////////////////////////////////////////////////////////////


        //        /// <devdoc>
        //        ///     Checks whether the current [....] app is running on a secure desktop under a terminal
        //        ///     server session.  This is the case when the TS session has been locked.
        //        ///     This method is useful when calling into GDI+ Graphics methods that modify the object's
        //        ///     state, these methods fail under a locked terminal session.
        //        /// </devdoc>
        //        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        internal static bool InLockedTerminalSession()
        {
            return false;
            //bool retVal = false;

            //if (SystemInformation.TerminalServerSession)
            //{
            //    // Let's try to open the input desktop, it it fails with access denied assume
            //    // the app is running on a secure desktop.
            //    IntPtr hDsk = WinFormSafeNativeMethods.OpenInputDesktop(0, false, WinFormNativeMethods.DESKTOP_SWITCHDESKTOP);

            //    if (hDsk == IntPtr.Zero)
            //    {
            //        int error = Marshal.GetLastWin32Error();
            //        retVal = error == WinFormNativeMethods.ERROR_ACCESS_DENIED;
            //    }

            //    if (hDsk != IntPtr.Zero)
            //    {
            //        WinFormSafeNativeMethods.CloseDesktop(hDsk);
            //    }
            //}

            //return retVal;
        }
    }
}

