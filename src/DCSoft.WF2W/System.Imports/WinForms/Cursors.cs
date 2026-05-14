//------------------------------------------------------------------------------
// <copyright file="Cursors.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms {

    using System.Diagnostics;

    using System;
    using System.Drawing;


    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public static class Cursors {
        private static  Cursor appStarting = null;
        private static  Cursor arrow       = null;
        private static  Cursor cross       = null;
        private static  Cursor defaultCursor = null;
        private static  Cursor iBeam       = null;
        private static  Cursor no          = null;
        private static  Cursor sizeAll     = null;
        private static  Cursor sizeNESW    = null;
        private static  Cursor sizeNS      = null;
        private static  Cursor sizeNWSE    = null;
        private static  Cursor sizeWE      = null;
        private static  Cursor upArrow     = null;
        private static  Cursor wait        = null;
        private static  Cursor help        = null;
        private static  Cursor hSplit      = null;
        private static  Cursor vSplit      = null;
        private static  Cursor noMove2D    = null;
        private static  Cursor noMoveHoriz = null;
        private static  Cursor noMoveVert  = null;
        private static  Cursor panEast     = null;
        private static  Cursor panNE       = null;
        private static  Cursor panNorth    = null;
        private static  Cursor panNW       = null;
        private static  Cursor panSE       = null;
        private static  Cursor panSouth    = null;
        private static  Cursor panSW       = null;
        private static  Cursor panWest     = null;
        private static  Cursor hand        = null;
        internal static Cursor KnownCursorFromHCursor( IntPtr handle ) {
            if (handle == IntPtr.Zero) {
                return null;
            }
            else {
                return new Cursor(handle);
            }
            
            // if (handle == Cursors.AppStarting.Handle)   return Cursors.AppStarting;
            // if (handle == Cursors.Arrow.Handle)         return Cursors.Arrow;
            // if (handle == Cursors.IBeam.Handle)         return Cursors.IBeam;
            // if (handle == Cursors.Cross.Handle)         return Cursors.Cross;
            // if (handle == Cursors.Default.Handle)       return Cursors.Default;
            // if (handle == Cursors.No.Handle)            return Cursors.No;
            // if (handle == Cursors.SizeAll.Handle)       return Cursors.SizeAll;
            // if (handle == Cursors.SizeNS.Handle)        return Cursors.SizeNS;
            // if (handle == Cursors.SizeWE.Handle)        return Cursors.SizeWE;
            // if (handle == Cursors.SizeNWSE.Handle)      return Cursors.SizeNWSE;
            // if (handle == Cursors.SizeNESW.Handle)      return Cursors.SizeNESW;
            // if (handle == Cursors.VSplit.Handle)        return Cursors.VSplit;
            // if (handle == Cursors.HSplit.Handle)        return Cursors.HSplit;
            // if (handle == Cursors.WaitCursor.Handle)    return Cursors.WaitCursor;
            // if (handle == Cursors.Help.Handle)          return Cursors.Help;
            // if (handle == IntPtr.Zero)     return null;
            
            //         appStarting = new Cursor(NativeMethods.IDC_APPSTARTING,0);
            //         arrow = new Cursor(NativeMethods.IDC_ARROW,0);
            //         cross = new Cursor(NativeMethods.IDC_CROSS,0);
            //         defaultCursor = new Cursor(NativeMethods.IDC_ARROW,0);
            //         iBeam = new Cursor(NativeMethods.IDC_IBEAM,0);
            //         no = new Cursor(NativeMethods.IDC_NO,0);
            //         sizeAll = new Cursor(NativeMethods.IDC_SIZEALL,0);
            //         sizeNESW = new Cursor(NativeMethods.IDC_SIZENESW,0);
            //         sizeNS      = new Cursor(NativeMethods.IDC_SIZENS,0);
            //         sizeNWSE    = new Cursor(NativeMethods.IDC_SIZENWSE,0);
            //         sizeWE      = new Cursor(NativeMethods.IDC_SIZEWE,0);
            //         upArrow     = new Cursor(NativeMethods.IDC_UPARROW,0);
            //         wait        = new Cursor(NativeMethods.IDC_WAIT,0);
            //         help        = new Cursor(NativeMethods.IDC_HELP,0);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        public static readonly Cursor AppStarting = new Cursor("progress");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor Arrow = new Cursor("default");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor Cross = new Cursor("crosshair");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor Default = new Cursor("default");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor IBeam = new Cursor("text");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor No = new Cursor("not-allowed");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor SizeAll = new Cursor("move");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor SizeNESW = new Cursor("nesw-resize");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor SizeNS = new Cursor("ns-resize");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor SizeNWSE = new Cursor("nwse-resize");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor SizeWE = new Cursor("ew-resize");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor UpArrow = new Cursor("n-resize");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor WaitCursor = new Cursor("wait");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor Help = new Cursor("help");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor HSplit = new Cursor("row-resize");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor VSplit = new Cursor("col-resize");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor NoMove2D = new Cursor("not-allowed");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor NoMoveHoriz = new Cursor("not-allowed");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor NoMoveVert = new Cursor("not-allowed");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor PanEast = new Cursor("grab");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor PanNE = new Cursor("grab");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor PanNorth = new Cursor("grab");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor PanNW = new Cursor("grab");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor PanSE = new Cursor("grab");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor PanSouth = new Cursor("grab");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor PanSW = new Cursor("grab");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor PanWest = new Cursor("grab");
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static readonly Cursor Hand = new Cursor("pointer");
    }
}
