//------------------------------------------------------------------------------
// <copyright file="Screen.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */

namespace System.Windows.Forms {
    using System.Threading;
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections;
    using Microsoft.Win32;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class Screen {

        internal static void SetScreenSize(int width, int height) {
            _Instance._Bounds = new Rectangle(0, 0, width, height);
        }

        private Screen() {

        }

        private static Screen _Instance = new Screen();
        private static readonly Screen[] _AllScreens = new Screen[] { _Instance };
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
        public static Screen[] AllScreens
        {
            get
            {
                return _AllScreens;
            }
        }
        public int BitsPerPixel {
            get {
                return 32;
            }
        }

        private System.Drawing.Rectangle _Bounds = new Rectangle(0, 0, 100, 100);
        public Rectangle Bounds {
            get {
                return this._Bounds;
            }
        }

        public string DeviceName {
            get {
                return "DCSoft";
            }
        }

        public bool Primary {
            get {
                return true ;
            }
        }
        public static Screen PrimaryScreen {
            get {
                return _Instance;
            }
        }
        public Rectangle WorkingArea {
            get {
                return _Bounds;

            }
        } 
        public override bool Equals(object obj) {
            return obj is Screen;
        }

        public static Screen FromPoint(Point point) {
            return _Instance;
        }
        public static Screen FromRectangle(Rectangle rect) {
            return _Instance;
        }
        public static Screen FromControl(Control control) {
            return _Instance;
        }

        public static Screen FromHandle(IntPtr hwnd) {
            return _Instance;
        }
        public static Screen FromHandleInternal(IntPtr hwnd)
        {
            return _Instance;
        }

        public static Rectangle GetWorkingArea(Point pt) {
            return _Instance.WorkingArea;
        }
       
        public static Rectangle GetWorkingArea(Rectangle rect) {
            return _Instance.WorkingArea;
        }
        
        public static Rectangle GetWorkingArea(Control ctl) {
            return _Instance.WorkingArea;
        }

        public static Rectangle GetBounds(Point pt) {
            return _Instance.Bounds;
        }
         
        public static Rectangle GetBounds(Rectangle rect) {
            return _Instance.Bounds;
        }
        
        public static Rectangle GetBounds(Control ctl) {
            return _Instance.Bounds;
        }
        public override int GetHashCode() {
            return this.GetHashCode();
        }
        public override string ToString() {
            return GetType().Name + "Width:" + this._Bounds.Width + " Height:" + this._Bounds.Height;
        }
    }
}
