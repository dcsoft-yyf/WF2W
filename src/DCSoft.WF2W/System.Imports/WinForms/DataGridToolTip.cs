//------------------------------------------------------------------------------
// <copyright file="DataGridToolTip.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {
    using System.Runtime.Remoting;
    using System.Runtime.InteropServices;
    using System.Drawing;
    
    using System.Windows.Forms;
    using Microsoft.Win32;
    using System.Diagnostics;
    using System.ComponentModel;

    // this class is basically a NativeWindow that does toolTipping
    // should be one for the entire grid
    internal class DataGridToolTip : MarshalByRefObject {
        // the toolTip control
        private NativeWindow tipWindow = null;

        // the dataGrid which contains this toolTip
        private DataGrid dataGrid = null;

        // CONSTRUCTOR
        public DataGridToolTip(DataGrid dataGrid)
        {
            Debug.Assert(dataGrid!= null, "can't attach a tool tip to a null grid");
            this.dataGrid = dataGrid;
        }

        // will ensure that the toolTip window was created
        public void CreateToolTipHandle()
        {
            if (tipWindow == null || tipWindow.Handle == IntPtr.Zero)
            {
                WinFormNativeMethods.INITCOMMONCONTROLSEX icc = new WinFormNativeMethods.INITCOMMONCONTROLSEX();
                icc.dwICC = WinFormNativeMethods.ICC_TAB_CLASSES;
                icc.dwSize = Marshal.SizeOf(icc);
                WinFormSafeNativeMethods.InitCommonControlsEx(icc);
                CreateParams cparams = new CreateParams();
                cparams.Parent = dataGrid.Handle;
                cparams.ClassName = WinFormNativeMethods.TOOLTIPS_CLASS;
                cparams.Style = WinFormNativeMethods.TTS_ALWAYSTIP;
                tipWindow = new NativeWindow();
                tipWindow.CreateHandle(cparams);

                WinFormUnsafeNativeMethods.SendMessage(new HandleRef(tipWindow, tipWindow.Handle), WinFormNativeMethods.TTM_SETMAXTIPWIDTH, 0, SystemInformation.MaxWindowTrackSize.Width);
                WinFormSafeNativeMethods.SetWindowPos(new HandleRef(tipWindow, tipWindow.Handle), WinFormNativeMethods.HWND_NOTOPMOST, 0, 0, 0, 0, WinFormNativeMethods.SWP_NOSIZE | WinFormNativeMethods.SWP_NOMOVE | WinFormNativeMethods.SWP_NOACTIVATE);
                WinFormUnsafeNativeMethods.SendMessage(new HandleRef(tipWindow, tipWindow.Handle), WinFormNativeMethods.TTM_SETDELAYTIME, WinFormNativeMethods.TTDT_INITIAL, 0);
            }
        }

        // this function will add a toolTip to the
        // windows system
        public void AddToolTip(String toolTipString, IntPtr toolTipId, Rectangle iconBounds)
        {
            Debug.Assert(tipWindow != null && tipWindow.Handle != IntPtr.Zero, "the tipWindow was not initialized, bailing out");

            if (toolTipString == null)
                throw new ArgumentNullException("toolTipString");
            if (iconBounds.IsEmpty)
                throw new ArgumentNullException("iconBounds", DCSR.GetString(DCSR.DataGridToolTipEmptyIcon));

            WinFormNativeMethods.TOOLINFO_T toolInfo = new WinFormNativeMethods.TOOLINFO_T();
            toolInfo.cbSize = Marshal.SizeOf(toolInfo);
            toolInfo.hwnd = dataGrid.Handle;
            toolInfo.uId = toolTipId;
            toolInfo.lpszText = toolTipString;
            toolInfo.rect = WinFormNativeMethods.RECT.FromXYWH(iconBounds.X, iconBounds.Y, iconBounds.Width, iconBounds.Height);
            toolInfo.uFlags = WinFormNativeMethods.TTF_SUBCLASS;
            WinFormUnsafeNativeMethods.SendMessage(new HandleRef(tipWindow, tipWindow.Handle), WinFormNativeMethods.TTM_ADDTOOL, 0, toolInfo);
        }

        public void RemoveToolTip(IntPtr toolTipId)
        {
            WinFormNativeMethods.TOOLINFO_T toolInfo = new WinFormNativeMethods.TOOLINFO_T();
            toolInfo.cbSize = Marshal.SizeOf(toolInfo);
            toolInfo.hwnd = dataGrid.Handle;
            toolInfo.uId = toolTipId;
            WinFormUnsafeNativeMethods.SendMessage(new HandleRef(tipWindow, tipWindow.Handle), WinFormNativeMethods.TTM_DELTOOL, 0, toolInfo);
        }

        // will destroy the tipWindow
        public void Destroy()
        {
            Debug.Assert(tipWindow != null, "how can one destroy a null window");
            tipWindow.DestroyHandle();
            tipWindow = null;
        }
    }
}
