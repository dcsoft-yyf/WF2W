using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.WinForms.Native;
using System.Windows.Forms;
using DCSoft.Drawing ;
using System.Drawing;
using DCSoft.Common;
//using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Reflection;
using System.ComponentModel;


namespace DCSoft.WinForms
{
    /// <summary>
    /// WinForm窗体工具类
    /// </summary>
    public static class WinFormUtils
    {
        /// <summary>
        /// 设置窗体的默认字体
        /// </summary>
        /// <param name="frm">窗体对象</param>
        public static void SetFormDefaultFont( System.Windows.Forms.Control frm )
        {
            if( frm != null )
            {
                frm.Font = FormDefaultFont;
            }
        }
        /// <summary>
        /// 窗体的默认字体
        /// </summary>
        internal static readonly Font FormDefaultFont = new Font("宋体", 9);
      
        /// <summary>
        /// 延时执行一次
        /// </summary>
        /// <param name="callBack">执行的委托对象</param>
        /// <param name="millisecend">延时的毫秒数</param>
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        public static void RunOnceDelay(EventHandler callBack, int millisecend)
        {
            if (callBack == null)
            {
                throw new ArgumentNullException("callBack");
            }
            var tmr = new System.Windows.Forms.Timer();
            tmr.Interval = millisecend;
            tmr.Tag = callBack;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Start();
        }

        private static void tmr_Tick(object sender, EventArgs e)
        {
            var tmr = (System.Windows.Forms.Timer)sender;
            tmr.Stop();
            tmr.Dispose();
            EventHandler handler = tmr.Tag as EventHandler;
            if (handler != null)
            {
                handler(null, null);
            }
        }

    }
}
