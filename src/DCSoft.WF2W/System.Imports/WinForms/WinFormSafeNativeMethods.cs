///------------------------------------------------------------------------------
// <copyright file="SafeNativeMethods.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.Windows.Forms
{
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System;
    using System.Security;
    using System.Security.Permissions;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Drawing;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Versioning;

    using IComDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;
    using System.Text.Json.Nodes;

    [SuppressUnmanagedCodeSecurity]
    internal static class WinFormSafeNativeMethods
    {
        // Local fallback for missing extended style constant
        private const int WS_EX_WINDOWEDGE = 0x00000100;

        [DllImport("shlwapi.dll")]
        [ResourceExposure(ResourceScope.None)]
        public static extern int SHAutoComplete(HandleRef hwndEdit, int flags);

        /*
        #if DEBUG
                [DllImport(ExternDll.Shell32, EntryPoint="SHGetFileInfo")]
                private static extern IntPtr IntSHGetFileInfo([MarshalAs(UnmanagedType.LPWStr)]string pszPath, int dwFileAttributes, NativeMethods.SHFILEINFO info, int cbFileInfo, int flags);
                public static IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, NativeMethods.SHFILEINFO info, int cbFileInfo, int flags) {
                    IntPtr newHandle = IntSHGetFileInfo(pszPath, dwFileAttributes, info, cbFileInfo, flags);
                    validImageListHandles.Add(newHandle);
                    return newHandle;
                }
        #else
                [DllImport(ExternDll.Shell32, CharSet=CharSet.Auto)]
                public static extern IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, NativeMethods.SHFILEINFO info, int cbFileInfo, int flags);
        #endif
        */
        [DllImport(ExternDll.User32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int OemKeyScan(short wAsciiVal);


        [DllImport(ExternDll.Gdi32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetSystemPaletteEntries(HandleRef hdc, int iStartIndex, int nEntries, byte[] lppe);
        [DllImport(ExternDll.Gdi32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetDIBits(HandleRef hdc, HandleRef hbm, int uStartScan, int cScanLines, byte[] lpvBits, ref WinFormNativeMethods.BITMAPINFO_FLAT bmi, int uUsage);
        [DllImport(ExternDll.Gdi32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int StretchDIBits(HandleRef hdc, int XDest, int YDest, int nDestWidth, int nDestHeight, int XSrc, int YSrc, int nSrcWidth, int nSrcHeight, byte[] lpBits, ref WinFormNativeMethods.BITMAPINFO_FLAT lpBitsInfo, int iUsage, int dwRop);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateCompatibleBitmap", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern IntPtr IntCreateCompatibleBitmap(HandleRef hDC, int width, int height);
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public static IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height)
        {
            return System.Internal.HandleCollector.Add(IntCreateCompatibleBitmap(hDC, width, height), WinFormNativeMethods.CommonHandles.GDI);
        }

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetScrollInfo(HandleRef hWnd, int fnBar, [In, Out] WinFormNativeMethods.SCROLLINFO si);

        [DllImport(ExternDll.Ole32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool IsAccelerator(HandleRef hAccel, int cAccelEntries, [In] ref WinFormNativeMethods.MSG lpMsg, short[] lpwCmd);
        [DllImport(ExternDll.Comdlg32, SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ChooseFont([In, Out] WinFormNativeMethods.CHOOSEFONT cf);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetBitmapBits(HandleRef hbmp, int cbBuffer, byte[] lpvBits);
        [DllImport(ExternDll.Comdlg32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int CommDlgExtendedError();
        [DllImport(ExternDll.Oleaut32, ExactSpelling = true, CharSet = CharSet.Unicode)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void SysFreeString(HandleRef bstr);

        [DllImport(ExternDll.Oleaut32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int objects, [MarshalAs(UnmanagedType.Interface)] ref object pobjs, int pages, HandleRef pClsid, int locale, int reserved1, IntPtr reserved2);
        [DllImport(ExternDll.Oleaut32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int objects, [MarshalAs(UnmanagedType.Interface)] ref object pobjs, int pages, Guid[] pClsid, int locale, int reserved1, IntPtr reserved2);
        [DllImport(ExternDll.Oleaut32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int objects, HandleRef lplpobjs, int pages, HandleRef pClsid, int locale, int reserved1, IntPtr reserved2);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, int dwData);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, string dwData);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)] WinFormNativeMethods.HH_POPUP dwData);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)] WinFormNativeMethods.HH_FTS_QUERY dwData);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)] WinFormNativeMethods.HH_AKLINK dwData);
        [DllImport(ExternDll.Oleaut32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void VariantInit(HandleRef pObject);
        [DllImport(ExternDll.Oleaut32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void VariantClear(HandleRef pObject);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool LineTo(HandleRef hdc, int x, int y);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool MoveToEx(HandleRef hdc, int x, int y, WinFormNativeMethods.POINT pt);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool Rectangle(
                                           HandleRef hdc, int left, int top, int right, int bottom);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool PatBlt(HandleRef hdc, int left, int top, int width, int height, int rop);

        [DllImport(ExternDll.Kernel32, EntryPoint = "GetThreadLocale", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern int GetThreadLCID();

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetMessagePos();



        [DllImport(ExternDll.User32, SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int RegisterClipboardFormat(string format);
        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetClipboardFormatName(int format, StringBuilder lpString, int cchMax);

        [DllImport(ExternDll.Comdlg32, SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ChooseColor([In, Out] WinFormNativeMethods.CHOOSECOLOR cc);
        //[DllImport(ExternDll.User32, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static int RegisterWindowMessage(string msg)
        {
            return 0;
        }
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "DeleteObject", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ExternalDeleteObject(HandleRef hObject);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "DeleteObject", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern bool IntDeleteObject(HandleRef hObject);
        public static bool DeleteObject(HandleRef hObject)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hObject, WinFormNativeMethods.CommonHandles.GDI);
            return IntDeleteObject(hObject);
        }

        //[DllImport(ExternDll.Oleaut32, EntryPoint = "OleCreateFontIndirect", ExactSpelling = true, PreserveSig = false)]
        //[ResourceExposure(ResourceScope.Process)]
        //public static extern WinFormSafeNativeMethods.IFontDisp OleCreateIFontDispIndirect(WinFormNativeMethods.FONTDESC fd, ref Guid iid);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateSolidBrush", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        private static extern IntPtr IntCreateSolidBrush(int crColor);
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public static IntPtr CreateSolidBrush(int crColor)
        {
            return System.Internal.HandleCollector.Add(IntCreateSolidBrush(crColor), WinFormNativeMethods.CommonHandles.GDI);
        }
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool SetWindowExtEx(HandleRef hDC, int x, int y, [In, Out] WinFormNativeMethods.SIZE size);

        [DllImport(ExternDll.Kernel32, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int FormatMessage(int dwFlags, HandleRef lpSource, int dwMessageId,
                                               int dwLanguageId, StringBuilder lpBuffer, int nSize, HandleRef arguments);


        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void InitCommonControls();

        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool InitCommonControlsEx(WinFormNativeMethods.INITCOMMONCONTROLSEX icc);

#if DEBUG
        private static System.Collections.ArrayList validImageListHandles = ArrayList.Synchronized(new System.Collections.ArrayList());
#endif

        // 

#if DEBUG
        [DllImport(ExternDll.Comctl32, EntryPoint = "ImageList_Create")]
        [ResourceExposure(ResourceScope.Process)]
        private static extern IntPtr IntImageList_Create(int cx, int cy, int flags, int cInitial, int cGrow);
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public static IntPtr ImageList_Create(int cx, int cy, int flags, int cInitial, int cGrow)
        {
            IntPtr newHandle = IntImageList_Create(cx, cy, flags, cInitial, cGrow);
            validImageListHandles.Add(newHandle);
            return newHandle;
        }
#else
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern IntPtr ImageList_Create(int cx, int cy, int flags, int cInitial, int cGrow);
#endif

#if DEBUG
        [DllImport(ExternDll.Comctl32, EntryPoint = "ImageList_Destroy")]
        [ResourceExposure(ResourceScope.None)]
        private static extern bool IntImageList_Destroy(HandleRef himl);
        public static bool ImageList_Destroy(HandleRef himl)
        {
            System.Diagnostics.Debug.Assert(validImageListHandles.Contains(himl.Handle), "Invalid ImageList handle");
            validImageListHandles.Remove(himl.Handle);
            return IntImageList_Destroy(himl);
        }
#else
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ImageList_Destroy(HandleRef himl);
#endif
        // unfortunately, the neat wrapper to Assert for DEBUG assumes that this was created by 
        // our version of ImageList_Create, which is not always the case for the TreeView's internal 
        // native state image list. Use separate EntryPoint thunk to skip this check:
        [DllImport(ExternDll.Comctl32, EntryPoint = "ImageList_Destroy")]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ImageList_Destroy_Native(HandleRef himl);

        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int ImageList_GetImageCount(HandleRef himl);
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int ImageList_Add(HandleRef himl, HandleRef hbmImage, HandleRef hbmMask);
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int ImageList_ReplaceIcon(HandleRef himl, int index, HandleRef hicon);
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int ImageList_SetBkColor(HandleRef himl, int clrBk);
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ImageList_Draw(HandleRef himl, int i, HandleRef hdcDst, int x, int y, int fStyle);
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ImageList_Replace(HandleRef himl, int i, HandleRef hbmImage, HandleRef hbmMask);
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ImageList_DrawEx(HandleRef himl, int i, HandleRef hdcDst, int x, int y, int dx, int dy, int rgbBk, int rgbFg, int fStyle);
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ImageList_GetIconSize(HandleRef himl, out int x, out int y);

#if DEBUG
        [DllImport(ExternDll.Comctl32, EntryPoint = "ImageList_Duplicate")]
        [ResourceExposure(ResourceScope.Process)]
        private static extern IntPtr IntImageList_Duplicate(HandleRef himl);
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public static IntPtr ImageList_Duplicate(HandleRef himl)
        {
            IntPtr newHandle = IntImageList_Duplicate(himl);
            validImageListHandles.Add(newHandle);
            return newHandle;
        }
#else
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern IntPtr ImageList_Duplicate(HandleRef himl);
#endif

        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ImageList_Remove(HandleRef himl, int i);
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ImageList_GetImageInfo(HandleRef himl, int i, WinFormNativeMethods.IMAGEINFO pImageInfo);

//#if DEBUG
        [DllImport(ExternDll.Comctl32, EntryPoint = "ImageList_Read")]
        [ResourceExposure(ResourceScope.None)]
        private static extern IntPtr IntImageList_Read(WinFormUnsafeNativeMethods.IStream pstm);
        public static IntPtr ImageList_Read(WinFormUnsafeNativeMethods.IStream pstm)
        {
            IntPtr newHandle = IntImageList_Read(pstm);
            //validImageListHandles.Add(newHandle);
            return newHandle;
        }
//#else
//        [DllImport(ExternDll.Comctl32)]
//        [ResourceExposure(ResourceScope.None)]
//        public static extern IntPtr ImageList_Read(UnsafeNativeMethods.IStream pstm);
//#endif

        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ImageList_Write(HandleRef himl, WinFormUnsafeNativeMethods.IStream pstm);
        [DllImport(ExternDll.Comctl32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int ImageList_WriteEx(HandleRef himl, int dwFlags, WinFormUnsafeNativeMethods.IStream pstm);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool TrackPopupMenuEx(HandleRef hmenu, int fuFlags, int x, int y, HandleRef hwnd, WinFormNativeMethods.TPMPARAMS tpm);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr GetKeyboardLayout(int dwLayout);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr ActivateKeyboardLayout(HandleRef hkl, int uFlags);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetKeyboardLayoutList(int size, [Out, MarshalAs(UnmanagedType.LPArray)] IntPtr[] hkls);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref WinFormNativeMethods.DEVMODE lpDevMode);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] WinFormNativeMethods.MONITORINFOEX info);
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr MonitorFromPoint(WinFormNativeMethods.POINTSTRUCT pt, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr MonitorFromRect(ref WinFormNativeMethods.RECT rect, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool EnumDisplayMonitors(HandleRef hdc, WinFormNativeMethods.COMRECT rcClip, WinFormNativeMethods.MonitorEnumProc lpfnEnum, IntPtr dwData);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateHalftonePalette", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        private static extern IntPtr /*HPALETTE*/ IntCreateHalftonePalette(HandleRef hdc);
        public static IntPtr /*HPALETTE*/ CreateHalftonePalette(HandleRef hdc)
        {
            return System.Internal.HandleCollector.Add(IntCreateHalftonePalette(hdc), WinFormNativeMethods.CommonHandles.GDI);
        }
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetPaletteEntries(HandleRef hpal, int iStartIndex, int nEntries, int[] lppe);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetTextMetricsW(HandleRef hDC, [In, Out] ref WinFormNativeMethods.TEXTMETRIC lptm);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetTextMetricsA(HandleRef hDC, [In, Out] ref WinFormNativeMethods.TEXTMETRICA lptm);

        public static int GetTextMetrics(HandleRef hDC, ref WinFormNativeMethods.TEXTMETRIC lptm)
        {
            if (Marshal.SystemDefaultCharSize == 1)
            {
                // ANSI
                WinFormNativeMethods.TEXTMETRICA lptmA = new WinFormNativeMethods.TEXTMETRICA();
                int retVal = WinFormSafeNativeMethods.GetTextMetricsA(hDC, ref lptmA);

                lptm.tmHeight = lptmA.tmHeight;
                lptm.tmAscent = lptmA.tmAscent;
                lptm.tmDescent = lptmA.tmDescent;
                lptm.tmInternalLeading = lptmA.tmInternalLeading;
                lptm.tmExternalLeading = lptmA.tmExternalLeading;
                lptm.tmAveCharWidth = lptmA.tmAveCharWidth;
                lptm.tmMaxCharWidth = lptmA.tmMaxCharWidth;
                lptm.tmWeight = lptmA.tmWeight;
                lptm.tmOverhang = lptmA.tmOverhang;
                lptm.tmDigitizedAspectX = lptmA.tmDigitizedAspectX;
                lptm.tmDigitizedAspectY = lptmA.tmDigitizedAspectY;
                lptm.tmFirstChar = (char)lptmA.tmFirstChar;
                lptm.tmLastChar = (char)lptmA.tmLastChar;
                lptm.tmDefaultChar = (char)lptmA.tmDefaultChar;
                lptm.tmBreakChar = (char)lptmA.tmBreakChar;
                lptm.tmItalic = lptmA.tmItalic;
                lptm.tmUnderlined = lptmA.tmUnderlined;
                lptm.tmStruckOut = lptmA.tmStruckOut;
                lptm.tmPitchAndFamily = lptmA.tmPitchAndFamily;
                lptm.tmCharSet = lptmA.tmCharSet;

                return retVal;
            }
            else
            {
                // Unicode
                return WinFormSafeNativeMethods.GetTextMetricsW(hDC, ref lptm);
            }
        }

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateDIBSection", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Machine)]
        private static extern IntPtr IntCreateDIBSection(HandleRef hdc, HandleRef pbmi, int iUsage, byte[] ppvBits, IntPtr hSection, int dwOffset);
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static IntPtr CreateDIBSection(HandleRef hdc, HandleRef pbmi, int iUsage, byte[] ppvBits, IntPtr hSection, int dwOffset)
        {
            return System.Internal.HandleCollector.Add(IntCreateDIBSection(hdc, pbmi, iUsage, ppvBits, hSection, dwOffset), WinFormNativeMethods.CommonHandles.GDI);
        }

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateBitmap", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Machine)]
        private static extern IntPtr /*HBITMAP*/ IntCreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, IntPtr lpvBits);
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static IntPtr /*HBITMAP*/ CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, IntPtr lpvBits)
        {
            return System.Internal.HandleCollector.Add(IntCreateBitmap(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), WinFormNativeMethods.CommonHandles.GDI);
        }

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateBitmap", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Machine)]
        private static extern IntPtr /*HBITMAP*/ IntCreateBitmapShort(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits);
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static IntPtr /*HBITMAP*/ CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits)
        {
            return System.Internal.HandleCollector.Add(IntCreateBitmapShort(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), WinFormNativeMethods.CommonHandles.GDI);
        }

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateBitmap", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Machine)]
        private static extern IntPtr /*HBITMAP*/ IntCreateBitmapByte(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, byte[] lpvBits);
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static IntPtr /*HBITMAP*/ CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, byte[] lpvBits)
        {
            return System.Internal.HandleCollector.Add(IntCreateBitmapByte(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), WinFormNativeMethods.CommonHandles.GDI);
        }
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreatePatternBrush", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        private static extern IntPtr /*HBRUSH*/ IntCreatePatternBrush(HandleRef hbmp);
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public static IntPtr /*HBRUSH*/ CreatePatternBrush(HandleRef hbmp)
        {
            return System.Internal.HandleCollector.Add(IntCreatePatternBrush(hbmp), WinFormNativeMethods.CommonHandles.GDI);
        }
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateBrushIndirect", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        private static extern IntPtr IntCreateBrushIndirect(WinFormNativeMethods.LOGBRUSH lb);
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public static IntPtr CreateBrushIndirect(WinFormNativeMethods.LOGBRUSH lb)
        {
            return System.Internal.HandleCollector.Add(IntCreateBrushIndirect(lb), WinFormNativeMethods.CommonHandles.GDI);
        }
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreatePen", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        private static extern IntPtr IntCreatePen(int nStyle, int nWidth, int crColor);
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public static IntPtr CreatePen(int nStyle, int nWidth, int crColor)
        {
            return System.Internal.HandleCollector.Add(IntCreatePen(nStyle, nWidth, crColor), WinFormNativeMethods.CommonHandles.GDI);
        }


        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool SetViewportExtEx(HandleRef hDC, int x, int y, WinFormNativeMethods.SIZE size);
        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr LoadCursor(HandleRef hInst, int iconId);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public extern static bool GetClipCursor([In, Out] ref WinFormNativeMethods.RECT lpRect);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr GetCursor();
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetIconInfo(HandleRef hIcon, [In, Out] WinFormNativeMethods.ICONINFO info);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int IntersectClipRect(HandleRef hDC, int x1, int y1, int x2, int y2);
        [DllImport(ExternDll.User32, ExactSpelling = true, EntryPoint = "CopyImage", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        private static extern IntPtr IntCopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags);
        public static IntPtr CopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags)
        {
            return System.Internal.HandleCollector.Add(IntCopyImage(hImage, uType, cxDesired, cyDesired, fuFlags), WinFormNativeMethods.CommonHandles.GDI);
        }
        public static IntPtr CopyImageAsCursor(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags)
        {
            return System.Internal.HandleCollector.Add(IntCopyImage(hImage, uType, cxDesired, cyDesired, fuFlags), WinFormNativeMethods.CommonHandles.Cursor);
        }


        //[DllImport(ExternDll.User32, ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool AdjustWindowRectEx(ref WinFormNativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle)
        {
            // Approximate the non-client border and caption sizes to emulate Win32 AdjustWindowRectEx.
            // This is sufficient for layout/region computations under WASM.
            int borderX = 0;
            int borderY = 0;
            int captionY = 0;

            // Thick frame tends to be larger than a simple border
            if ((dwStyle & WinFormNativeMethods.WS_THICKFRAME) != 0)
            {
                borderX += 4;
                borderY += 4;
            }
            else if ((dwStyle & WinFormNativeMethods.WS_BORDER) != 0)
            {
                borderX += 1;
                borderY += 1;
            }

            // Caption height (title bar). Add a reasonable default height.
            if ((dwStyle & WinFormNativeMethods.WS_CAPTION) != 0)
            {
                // Typical caption height ~ 23-30px; use 24 as a conservative default.
                captionY += 28;
            }

            // Menu contributes to top non-client height (rough approximation)
            if (bMenu)
            {
                captionY += 28; // simple menu bar height approximation
            }

            // Extended styles that add a client edge or window edge
            if ((dwExStyle & WinFormNativeMethods.WS_EX_CLIENTEDGE) != 0)
            {
                borderX += 2;
                borderY += 2;
            }
            if ((dwExStyle & WS_EX_WINDOWEDGE) != 0)
            {
                borderX += 2;
                borderY += 2;
            }

            // Apply adjustments to the rect: inflate by non-client metrics
            lpRect.left -= borderX;
            lpRect.top -= (borderY + captionY);
            lpRect.right += borderX;
            lpRect.bottom += borderY;

            return true;
        }
        [DllImport(ExternDll.Ole32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int DoDragDrop(IComDataObject dataObject, WinFormUnsafeNativeMethods.IOleDropSource dropSource, int allowedEffects, int[] finalEffect);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr GetSysColorBrush(int nIndex);
        //[DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool EnableWindow(HandleRef hWnd, bool enable)
        {
            return DCWin32API.JSRuntime.Invoke<bool>("__DCWin32API.EnableWindow", hWnd.Handle.ToInt32(), enable);
        }
        //[DllImport(ExternDll.User32, ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool GetClientRect(HandleRef hWnd, [In, Out] ref WinFormNativeMethods.RECT rect)
        {
            return DCWin32API.GetClientRect(hWnd.Handle.ToInt32(), ref rect);
        }
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetDoubleClickTime();
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetUpdateRgn(HandleRef hwnd, HandleRef hrgn, bool fErase);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ValidateRect(HandleRef hWnd, [In, Out] ref WinFormNativeMethods.RECT rect);

        //
        // WARNING: Don't uncomment this code unless you absolutelly need it.  Use instead Marshal.GetLastWin32Error
        // and mark your PInvoke [DllImport(..., SetLastError=true)]
        // From MSDN:
        // GetLastWin32Error exposes the Win32 GetLastError API method from Kernel32.DLL. This method exists because 
        // it is not safe to make a direct platform invoke call to GetLastError to obtain this information. If you 
        // want to access this error code, you must call GetLastWin32Error rather than writing your own platform invoke 
        // definition for GetLastError and calling it. The common language runtime can make internal calls to APIs that 
        // overwrite the operating system maintained GetLastError.
        //
        //[DllImport(ExternDll.Kernel32, ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        //public extern static int GetLastError();

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int FillRect(HandleRef hdc, [In] ref WinFormNativeMethods.RECT rect, HandleRef hbrush);

        [DllImport(ExternDll.Gdi32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int /*COLORREF*/ GetTextColor(HandleRef hDC);


        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetBkColor(HandleRef hDC);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int /*COLORREF*/ SetTextColor(HandleRef hDC, int crColor);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int SetBkColor(HandleRef hDC, int clr);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr /* HPALETTE */SelectPalette(HandleRef hdc, HandleRef hpal, int bForceBackground);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool SetViewportOrgEx(HandleRef hDC, int x, int y, [In, Out] WinFormNativeMethods.POINT point);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateRectRgn", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        private static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
        {
            return System.Internal.HandleCollector.Add(IntCreateRectRgn(x1, y1, x2, y2), WinFormNativeMethods.CommonHandles.GDI);
        }
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int CombineRgn(HandleRef hRgn, HandleRef hRgn1, HandleRef hRgn2, int nCombineMode);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int RealizePalette(HandleRef hDC);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool LPtoDP(HandleRef hDC, [In, Out] ref WinFormNativeMethods.RECT lpRect, int nCount);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool SetWindowOrgEx(HandleRef hDC, int x, int y, [In, Out] WinFormNativeMethods.POINT point);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetViewportOrgEx(HandleRef hDC, [In, Out] WinFormNativeMethods.POINT point);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int SetMapMode(HandleRef hDC, int nMapMode);

        //[DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool IsWindowEnabled(HandleRef hWnd)
        {
            return DCWin32API.JSRuntime.Invoke<bool>("__DCWin32API.IsWindowEnabled", hWnd.Handle.ToInt32());
        }
        //[DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool IsWindowVisible(HandleRef hWnd)
        {
            return DCWin32API.JSRuntime.Invoke<bool>("__DCWin32API.IsWindowVisible", hWnd.Handle.ToInt32());
        }
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ReleaseCapture();
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern int GetCurrentThreadId();

        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);
        internal delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern int GetWindowThreadProcessId(HandleRef hWnd, out int lpdwProcessId);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetExitCodeThread(HandleRef hWnd, out int lpdwExitCode);
        //[DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool ShowWindow(HandleRef hWnd, int nCmdShow)
        {
            return DCWin32API.JSRuntime.Invoke<bool>("__DCWin32API.ShowWindow", hWnd.Handle.ToInt32(), nCmdShow);
        }

        public static bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags)
        {
            //if(hWnd.Wrapper is System.Windows.Forms.TextBox && cx < 190)
            //{
            //    var s = 1;
            //}
            var json = new JsonObject
            {
                ["InsertAfter"] = hWndInsertAfter.Handle.ToInt32(),
                ["Left"] = x,
                ["Top"] = y,
                ["Width"] = cx,
                ["Height"] = cy,
                ["Flags"] = flags
            };
            return DCWin32API.JSRuntime.Invoke<bool>("__DCWin32API.SetWindowPos", hWnd.Handle.ToInt32(), json);
            //return DCWin32API.SetWindowPos(hWnd.Handle.ToInt32(), json);
        }

        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetWindowTextLength(HandleRef hWnd);
        // this is a wrapper that comctl exposes for the NT function since it doesn't exist natively on 95.
        [DllImport(ExternDll.Comctl32, ExactSpelling = true), CLSCompliantAttribute(false)]
        [ResourceExposure(ResourceScope.None)]
        private static extern bool _TrackMouseEvent(WinFormNativeMethods.TRACKMOUSEEVENT tme);
        public static bool TrackMouseEvent(WinFormNativeMethods.TRACKMOUSEEVENT tme)
        {
            // only on NT - not on 95 - comctl32 has a wrapper for 95 and NT.
            return _TrackMouseEvent(tme);
        }
        //[DllImport(ExternDll.User32, ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool RedrawWindow(HandleRef hwnd, ref WinFormNativeMethods.RECT rcUpdate, HandleRef hrgnUpdate, int flags)
        {
            int handle = hwnd.Handle.ToInt32();
            int left = rcUpdate.left;
            int top = rcUpdate.top;
            int width = rcUpdate.right - rcUpdate.left;
            int height = rcUpdate.bottom - rcUpdate.top;

            var payload = new System.Text.Json.Nodes.JsonObject
            {
                ["Left"] = left,
                ["Top"] = top,
                ["Width"] = width,
                ["Height"] = height
            };
            int lParamHandle = DCWin32API.PackageToHandle(payload);

            bool doErase = (flags & (WinFormNativeMethods.RDW_ERASE | WinFormNativeMethods.RDW_ERASENOW)) != 0;
            bool doPaint = (flags & (WinFormNativeMethods.RDW_INVALIDATE | WinFormNativeMethods.RDW_UPDATENOW | WinFormNativeMethods.RDW_FRAME)) != 0;
            if (flags == 0)
            {
                doErase = true;
                doPaint = true;
            }

            if (doErase)
            {
                DCMessageManager.SendMessage(handle, WinFormNativeMethods.WM_ERASEBKGND, 0, lParamHandle );
            }
            if (doPaint)
            {
                DCMessageManager.SendMessage(handle, WinFormNativeMethods.WM_PAINT, 0, lParamHandle);
            }
            return true;
        }
        //[DllImport(ExternDll.User32, ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool RedrawWindow(HandleRef hwnd, WinFormNativeMethods.COMRECT rcUpdate, HandleRef hrgnUpdate, int flags)
        {
            int handle = hwnd.Handle.ToInt32();
            int left = -1, top = -1, width = -1, height = -1;
            if (rcUpdate != null)
            {
                left = rcUpdate.left;
                top = rcUpdate.top;
                width = rcUpdate.right - rcUpdate.left;
                height = rcUpdate.bottom - rcUpdate.top;
            }

            var payload = new System.Text.Json.Nodes.JsonObject
            {
                ["Left"] = left,
                ["Top"] = top,
                ["Width"] = width,
                ["Height"] = height
            };
            int lParamHandle = DCWin32API.PackageToHandle(payload);

            bool doErase = (flags & (WinFormNativeMethods.RDW_ERASE | WinFormNativeMethods.RDW_ERASENOW)) != 0;
            bool doPaint = (flags & (WinFormNativeMethods.RDW_INVALIDATE | WinFormNativeMethods.RDW_UPDATENOW | WinFormNativeMethods.RDW_FRAME)) != 0;
            if (flags == 0)
            {
                doErase = true;
                doPaint = true;
            }

            if (doErase)
            {
                DCMessageManager.PostMessage(handle, WinFormNativeMethods.WM_ERASEBKGND, 0, lParamHandle);
            }
            if (doPaint)
            {
                DCMessageManager.PostMessage(handle, WinFormNativeMethods.WM_PAINT, 0, lParamHandle);
            }
            return true;
        }
        //[DllImport(ExternDll.User32, ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool InvalidateRect(HandleRef hWnd, ref WinFormNativeMethods.RECT rect, bool erase)
        {
            const int WM_PAINT = 0x000F;
            const int WM_ERASEBKGND = 0x0014;
            int left = rect.left;
            int top = rect.top;
            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;
            var payload = new System.Text.Json.Nodes.JsonObject
            {
                ["Left"] = left,
                ["Top"] = top,
                ["Width"] = width,
                ["Height"] = height
            };
            if (erase)
            {
                DCMessageManager.PostMessage(hWnd.Handle.ToInt32(), WM_ERASEBKGND, 0, DCWin32API.PackageToHandle(payload));
            }
            return DCMessageManager.PostMessage(hWnd.Handle.ToInt32(), WM_PAINT, 0, DCWin32API.PackageToHandle(payload));
        }
        //[DllImport(ExternDll.User32, ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool InvalidateRect(HandleRef hWnd, WinFormNativeMethods.COMRECT rect, bool erase)
        {
            const int WM_PAINT = 0x000F;
            const int WM_ERASEBKGND = 0x0014;
            int left = -1, top = -1, width = -1, height = -1;
            if (rect != null)
            {
                left = rect.left;
                top = rect.top;
                width = rect.right - rect.left;
                height = rect.bottom - rect.top;
            }
            // Pack rect into Json for lParam-like payload
            var payload = new System.Text.Json.Nodes.JsonObject
            {
                ["Left"] = left,
                ["Top"] = top,
                ["Width"] = width,
                ["Height"] = height
            };
            if (erase)
            {
                DCMessageManager.PostMessage(hWnd.Handle.ToInt32(), WM_ERASEBKGND, 0, DCWin32API.PackageToHandle(payload));
            }
            return DCMessageManager.PostMessage(hWnd.Handle.ToInt32(), WM_PAINT, 0, DCWin32API.PackageToHandle(payload),new Rectangle( left , top , width , height ));
        }
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool InvalidateRgn(HandleRef hWnd, HandleRef hrgn, bool erase);
        //[DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool UpdateWindow(HandleRef hWnd)
        {
            return true;
        }
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern int GetCurrentProcessId();
        [DllImport(ExternDll.User32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int ScrollWindowEx(HandleRef hWnd, int nXAmount, int nYAmount, WinFormNativeMethods.COMRECT rectScrollRegion, ref WinFormNativeMethods.RECT rectClip, HandleRef hrgnUpdate, ref WinFormNativeMethods.RECT prcUpdate, int flags);
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.AppDomain)]
        public static extern int GetThreadLocale();
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool MessageBeep(int type);
        //[DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool DrawMenuBar(HandleRef hWnd)
        {
            return false;
        }
        //[DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool IsChild(HandleRef parent, HandleRef child)
        {
            return DCWin32API.JSRuntime.Invoke<bool>(
                "__DCWin32API.IsChild",
                parent.Handle.ToInt32(),
                child.Handle.ToInt32());
        }
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetTimer(HandleRef hWnd, int nIDEvent, int uElapse, IntPtr lpTimerFunc);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool KillTimer(HandleRef hwnd, int idEvent);
        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto),
            SuppressMessage("Microsoft.Usage", "CA2205:UseManagedEquivalentsOfWin32Api")]
        [ResourceExposure(ResourceScope.None)]
        public static extern int MessageBox(HandleRef hWnd, string text, string caption, int type);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetTickCount();
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ScrollWindow(HandleRef hWnd, int nXAmount, int nYAmount, ref WinFormNativeMethods.RECT rectScrollRegion, ref WinFormNativeMethods.RECT rectClip);
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern IntPtr GetCurrentProcess();
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern IntPtr GetCurrentThread();

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.AppDomain)]
        public extern static bool SetThreadLocale(int Locale);

        //[DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //[ResourceExposure(ResourceScope.None)]
        public static bool IsWindowUnicode(HandleRef hWnd) { return true; }
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool DrawEdge(HandleRef hDC, ref WinFormNativeMethods.RECT rect, int edge, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool DrawFrameControl(HandleRef hDC, ref WinFormNativeMethods.RECT rect, int type, int state);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetClipRgn(HandleRef hDC, HandleRef hRgn);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetRgnBox(HandleRef hRegion, ref WinFormNativeMethods.RECT clipRect);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int SetROP2(HandleRef hDC, int nDrawMode);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool DrawIcon(HandleRef hDC, int x, int y, HandleRef hIcon);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool DrawIconEx(HandleRef hDC, int x, int y, HandleRef hIcon, int width, int height, int iStepIfAniCursor, HandleRef hBrushFlickerFree, int diFlags);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int SetBkMode(HandleRef hDC, int nBkMode);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight,
                                         HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool ShowCaret(HandleRef hWnd);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool HideCaret(HandleRef hWnd);
        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern uint GetCaretBlinkTime();


        // Theming/Visual Styles
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool IsAppThemed();
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeAppProperties();
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void SetThemeAppProperties(int Flags);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr OpenThemeData(HandleRef hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int CloseThemeData(HandleRef hTheme);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int dwMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool IsThemePartDefined(HandleRef hTheme, int iPartId, int iStateId);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int DrawThemeBackground(HandleRef hTheme, HandleRef hdc, int partId, int stateId, [In] WinFormNativeMethods.COMRECT pRect, [In] WinFormNativeMethods.COMRECT pClipRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int DrawThemeEdge(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] WinFormNativeMethods.COMRECT pDestRect, int uEdge, int uFlags, [Out] WinFormNativeMethods.COMRECT pContentRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int DrawThemeParentBackground(HandleRef hwnd, HandleRef hdc, [In] WinFormNativeMethods.COMRECT prc);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int DrawThemeText(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [MarshalAs(UnmanagedType.LPWStr)] string pszText, int iCharCount, int dwTextFlags, int dwTextFlags2, [In] WinFormNativeMethods.COMRECT pRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeBackgroundContentRect(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] WinFormNativeMethods.COMRECT pBoundingRect, [Out] WinFormNativeMethods.COMRECT pContentRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeBackgroundExtent(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] WinFormNativeMethods.COMRECT pContentRect, [Out] WinFormNativeMethods.COMRECT pExtentRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeBackgroundRegion(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] WinFormNativeMethods.COMRECT pRect, ref IntPtr pRegion);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeBool(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref bool pfVal);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeColor(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref int pColor);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeEnumValue(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref int piVal);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeFilename(HandleRef hTheme, int iPartId, int iStateId, int iPropId, StringBuilder pszThemeFilename, int cchMaxBuffChars);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeFont(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, int iPropId, WinFormNativeMethods.LOGFONT pFont);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeInt(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref int piVal);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemePartSize(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] WinFormNativeMethods.COMRECT prc, System.Windows.Forms.VisualStyles.ThemeSizeType eSize, [Out] WinFormNativeMethods.SIZE psz);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemePosition(HandleRef hTheme, int iPartId, int iStateId, int iPropId, [Out] WinFormNativeMethods.POINT pPoint);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeMargins(HandleRef hTheme, HandleRef hDC, int iPartId, int iStateId, int iPropId, ref WinFormNativeMethods.MARGINS margins);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeString(HandleRef hTheme, int iPartId, int iStateId, int iPropId, StringBuilder pszBuff, int cchMaxBuffChars);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeDocumentationProperty([MarshalAs(UnmanagedType.LPWStr)] string pszThemeName, [MarshalAs(UnmanagedType.LPWStr)] string pszPropertyName, StringBuilder pszValueBuff, int cchMaxValChars);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeTextExtent(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [MarshalAs(UnmanagedType.LPWStr)] string pszText, int iCharCount, int dwTextFlags, [In] WinFormNativeMethods.COMRECT pBoundingRect, [Out] WinFormNativeMethods.COMRECT pExtentRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeTextMetrics(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, ref System.Windows.Forms.VisualStyles.TextMetrics ptm);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int HitTestThemeBackground(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, int dwOptions, [In] WinFormNativeMethods.COMRECT pRect, HandleRef hrgn, [In] WinFormNativeMethods.POINTSTRUCT ptTest, ref int pwHitTestCode);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool IsThemeBackgroundPartiallyTransparent(HandleRef hTheme, int iPartId, int iStateId);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetThemeSysBool(HandleRef hTheme, int iBoolId);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeSysInt(HandleRef hTheme, int iIntId, ref int piValue);

        [DllImportAttribute(ExternDll.User32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr OpenInputDesktop(int dwFlags, [MarshalAs(UnmanagedType.Bool)] bool fInherit, int dwDesiredAccess);

        [DllImportAttribute(ExternDll.User32)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool CloseDesktop(IntPtr hDesktop);

        // Color conversion
        //
        public static int RGBToCOLORREF(int rgbValue)
        {

            // clear the A value, swap R & B values
            int bValue = (rgbValue & 0xFF) << 16;

            rgbValue &= 0xFFFF00;
            rgbValue |= ((rgbValue >> 16) & 0xFF);
            rgbValue &= 0x00FFFF;
            rgbValue |= bValue;
            return rgbValue;
        }

        public static Color ColorFromCOLORREF(int colorref)
        {
            int r = colorref & 0xFF;
            int g = (colorref >> 8) & 0xFF;
            int b = (colorref >> 16) & 0xFF;
            return Color.FromArgb(r, g, b);
        }

        public static int ColorToCOLORREF(Color color)
        {
            return (int)color.R | ((int)color.G << 8) | ((int)color.B << 16);
        }

        [ComImport(), Guid("BEF6E003-A874-101A-8BBA-00AA00300CAB"), System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch)]
        public interface IFontDisp
        {

            string Name { get; set; }

            decimal Size { get; set; }

            bool Bold { get; set; }

            bool Italic { get; set; }

            bool Underline { get; set; }

            bool Strikethrough { get; set; }

            short Weight { get; set; }

            short Charset { get; set; }
        }
    }
}

