using System;
using System.Collections;

namespace DCSoft.Common
{
    
    /// <summary>
    /// 二进制数据处理的例程
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
	public static class BinaryHelper /////////
	{
        private static int _RndCounter = 0;
        /// <summary>
		/// 比较两个字节数组内容是否一致
		/// </summary>
		/// <param name="bs1">字节数组1</param>
		/// <param name="bs2">字节数组2</param>
		/// <returns>两个字节数组内容是否一致</returns>
		public static bool Equals(byte[] bs1, byte[] bs2)
        {
            if (bs1 == bs2)
            {
                return true;
            }
            int len1 = bs1 == null ? 0 : bs1.Length;
            int len2 = bs2 == null ? 0 : bs2.Length;
            if (len1 != len2)
            {
                // 长度不一致
                return false;
            }
            if (bs1 == null || bs2 == null)
            {
                // 有一个为空
                return false;
            }
            int len = bs1.Length;

            for (int iCount = bs1.Length - 1; iCount >= 0; iCount--)
            {
                if (bs1[iCount] != bs2[iCount])
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}