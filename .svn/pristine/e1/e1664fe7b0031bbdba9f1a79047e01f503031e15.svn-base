using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 简单的可逆图形绘制器
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class SimpleReversibleDrawer
    {
        /// <summary>
        /// 绘制可逆线条
        /// </summary>
        /// <param name="hwnd">窗体句柄对象</param>
        /// <param name="x1">起点X坐标</param>
        /// <param name="y1">起点Y坐标</param>
        /// <param name="x2">终点X坐标</param>
        /// <param name="y2">终点Y坐标</param>
        public static void DrawReversibleLine(IntPtr hwnd, int x1, int y1, int x2, int y2)
        {
#if WINFORM || DCWriterForWinFormNET6
            IntPtr newPen = CreatePen(0, 1, ColorTranslator.ToWin32(Color.SkyBlue));
            IntPtr hdc = GetDC(hwnd);
            if (hdc != IntPtr.Zero)
            {
                IntPtr oldPen = SelectObject(hdc, newPen);
                int old = SetROP2(hdc, 7);
                MoveToEx(hdc, x1, y1, 0);
                LineTo(hdc, x2, y2);
                SetROP2(hdc, old);
                SelectObject(hdc, oldPen);
                ReleaseDC(hwnd, hdc);
            }
            DeleteObject(newPen);
#endif
        }

#if WINFORM || DCWriterForWinFormNET6
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern int DeleteObject(System.IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SelectObject(System.IntPtr hDC, System.IntPtr hObject);

        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr GetDC(IntPtr hWnd);

#region 声明Win32API函数 ******************************************************************
         
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreatePen(int PenStyle, int Width, int Color);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern bool LineTo(System.IntPtr hDC, int X, int Y);
         

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern bool MoveToEx(System.IntPtr hDC, int X, int Y, int lpPoint);

#endregion
         
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern int SetROP2(System.IntPtr hDC, int DrawMode);
#endif
    }
}
