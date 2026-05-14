using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace DCSoft.Common
{
    /// <summary>
    /// 调试相关的例程
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public static class DebugHelper
    {
        /// <summary>
        /// 是否为WINDOWS操作系统
        /// </summary>
        private static bool _IsWindowsPlatform = false;
        /// <summary>
        /// 是否为WINDOWS操作系统
        /// </summary>
        public static bool IsWindowsPlatform
        {
            get
            {
                return _IsWindowsPlatform;
            }
        }

        /// <summary>
        /// 是否为Linux/Unix操作系统
        /// </summary>
        private static bool _IsLinuxOrUnixPlatform ;
        /// <summary>
        /// 是否为Linux/Unix操作系统
        /// </summary>
        public static bool IsLinuxOrUnixPlatform
        {
            get
            {
                return _IsLinuxOrUnixPlatform;
            }
        }

        static DebugHelper()
        {
            var p = Environment.OSVersion.Platform;
            //p = PlatformID.Unix;
            _IsWindowsPlatform =
                p == PlatformID.Win32NT
                || p == PlatformID.Win32S
                || p == PlatformID.Win32Windows
                || p == PlatformID.WinCE;
            _IsLinuxOrUnixPlatform = p == PlatformID.Unix;

            //IsWindowsPlatform = false;
            //IsLinuxOrUnixPlatform = true;
        }

      
    }
}
