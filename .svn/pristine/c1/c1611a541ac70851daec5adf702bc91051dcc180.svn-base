using System;
using System.Runtime.InteropServices ;

namespace DCSoft.WinForms.Native
{
	/// <summary>
	/// 光标操作对象
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class Win32Caret
	{
		
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="ctl">窗体对象</param>
		public Win32Caret( System.Windows.Forms.IWin32Window ctl )
		{
			myControl = ctl ;
		}

		/// <summary>
		/// 创建光标对象
		/// </summary>
		/// <param name="hBitmap">图片句柄</param>
		/// <param name="nWidth">光标宽度</param>
		/// <param name="nHeight">光标高度</param>
		/// <returns>操作是否成功</returns>
		public bool Create( int hBitmap , int nWidth , int nHeight )
		{
            if (myControl == null)
            {
                return false;
            }
            else
            {
                return CreateCaret(myControl.Handle, hBitmap, nWidth, nHeight);
            }
		}
		/// <summary>
		/// 设置光标位置
		/// </summary>
		/// <param name="x">X坐标</param>
		/// <param name="y">Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool SetPos( int x , int y )
		{
            if (myControl == null)
            {
                return false;
            }
            else
            {
                return SetCaretPos(x, y);
            }
		}
		/// <summary>
		/// 删除光标
		/// </summary>
		/// <returns>操作是否成功</returns>
		public bool Destroy()
		{
			return DestroyCaret();
		}
		/// <summary>
		/// 显示光标
		/// </summary>
		/// <returns>操作是否成功</returns>
        public bool Show()
        {
            if (myControl == null)
            {
                return false;
            }
            else
            {
                return ShowCaret(myControl.Handle);
            }
        }
		/// <summary>
		/// 隐藏光标
		/// </summary>
		/// <returns>操作是否成功</returns>
		public bool Hide()
		{
            if (myControl == null)
            {
                return false;
            }
            else
            {
                return HideCaret(myControl.Handle);
            }
		}

		

		#region 内部代码 ******************************************************

		private readonly System.Windows.Forms.IWin32Window myControl = null;

		

        [StructLayout(LayoutKind.Sequential)]
        private struct MyNativePOINT
        {
            public int x;
            public int y;
        }

        //// 导入处理光标的 Windows 32 位 API
        //// 创建光标
        //[DllImport("User32.dll")]
        //private static extern bool GetCaretPos(ref  MyNativePOINT p);
		

		// 导入处理光标的 Windows 32 位 API
		// 创建光标
		[DllImport("User32.dll")]
		private static extern bool CreateCaret(IntPtr hWnd, int hBitmap, int nWidth, int nHeight);
		// 设置光标位置
		[DllImport("User32.dll")]
		private static extern bool SetCaretPos(int x, int y);
		// 删除光标
		[DllImport("User32.dll")]
		private static extern bool DestroyCaret();
		// 显示光标
		[DllImport("User32.dll")]
		private static extern bool ShowCaret(IntPtr hWnd);
		// 隐藏光标
		[DllImport("User32.dll")]
		private static extern bool HideCaret(IntPtr hWnd);

		#endregion

	}//public class Win32Caret
}