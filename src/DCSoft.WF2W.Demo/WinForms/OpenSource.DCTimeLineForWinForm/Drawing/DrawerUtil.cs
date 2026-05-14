//using System.Windows.Forms;
using Microsoft.Win32;
using System;
//using DCSoft.Common ;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DCSoft.Drawing
{
	/// <summary>
	/// DrawerUtil 的摘要说明。
	/// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public static class DrawerUtil
	{
            
        private static float _DesktopDPIRate = 0;

        ///// <summary>
        ///// 获得桌面DPI和应用程序使用的DPI的比例
        ///// </summary>
        //public static float DesktopDPIRate
        //{
        //    get
        //    {
        //        if (_DesktopDPIRate == 0)
        //        {
        //            float dd = GetDesktopDPI();
        //            using (System.Windows.Forms.Form frm = new System.Windows.Forms.Form())
        //            {
        //                using (Graphics g = frm.CreateGraphics())
        //                {
        //                    _DesktopDPIRate = dd / (float)g.DpiX;
        //                }
        //            }
        //        }
        //        return _DesktopDPIRate;
        //    }
        //}
        //private static int _DesktopDPI = 0;
        //public static int GetDesktopDPI()
        //{
        //    if (_DesktopDPI == 0)
        //    {
        //        _DesktopDPI = 0;
        //        try
        //        {
        //            RegistryKey key = Registry.CurrentUser;
        //            RegistryKey pixeKey = key.OpenSubKey("Control Panel\\Desktop");
        //            if (pixeKey != null)
        //            {
        //                var pixels = pixeKey.GetValue("LogPixels");
        //                if (pixels != null)
        //                {
        //                    _DesktopDPI = int.Parse(pixels.ToString());
        //                }
        //                pixeKey.Close();
        //            }
        //            if( _DesktopDPI == 0 )
        //            {
        //                pixeKey = key.OpenSubKey("Control Panel\\Desktop\\WindowMetrics");
        //                if (pixeKey != null)
        //                {
        //                    var pixels = pixeKey.GetValue("AppliedDPI");
        //                    if (pixels != null)
        //                    {
        //                        _DesktopDPI = int.Parse(pixels.ToString());
        //                    }
        //                    pixeKey.Close();
        //                }
        //            }
        //        }
        //        catch (Exception ext)
        //        {
        //            //DCConsole.Default.WriteLineError(ext.ToString());
        //            _DesktopDPI = 96;
        //        }
        //        if (_DesktopDPI == 0)
        //        {
        //            _DesktopDPI = 96;
        //        }
        //    }
        //    return _DesktopDPI;
        //}

        private static System.Drawing.Pen mySelectionPen = null;
        private static System.Drawing.Pen myCurrentSelectionPen = null;
        public static System.Drawing.Pen GetSelectionPen(float width, bool IsCurrent)
        {
            if (IsCurrent)
            {
                if (myCurrentSelectionPen == null || myCurrentSelectionPen.Width != width)
                {
                    if (myCurrentSelectionPen != null)
                    {
                        myCurrentSelectionPen.Dispose();
                    }
                    System.Drawing.Drawing2D.HatchBrush brush = new System.Drawing.Drawing2D.HatchBrush(
                        System.Drawing.Drawing2D.HatchStyle.LightDownwardDiagonal,//.Percent25 ,
                        System.Drawing.Color.Blue,
                        System.Drawing.Color.Transparent);
                    myCurrentSelectionPen = new Pen(brush, width);
                }
                return myCurrentSelectionPen;
            }
            else
            {
                if (mySelectionPen == null || mySelectionPen.Width != width)
                {
                    if (mySelectionPen != null)
                    {
                        mySelectionPen.Dispose();
                    }
                    System.Drawing.Drawing2D.HatchBrush brush = new System.Drawing.Drawing2D.HatchBrush(
                        System.Drawing.Drawing2D.HatchStyle.LightDownwardDiagonal,//.Percent25 ,
                        System.Drawing.Color.Black,
                        System.Drawing.Color.Transparent);
                    mySelectionPen = new Pen(brush, width);
                }
                return mySelectionPen;
            }
        }
       
        public static void DrawImageUnscaledNearestNeighbor(DCGraphics g, Image img, int x, int y)
        {
            InterpolationMode ipm = g.InterpolationMode;
            PixelOffsetMode pom = g.PixelOffsetMode;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;// .NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.DrawImageUnscaled(img, x, y);
            g.InterpolationMode = ipm;
            g.PixelOffsetMode = pom;
        }



        

    }
}
