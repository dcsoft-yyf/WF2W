using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft
{
    /// <summary>
    /// 表示空数字的对象
    /// </summary>
    /// <remarks>在ActiveX模式下，double.NaN容易引起未知内存错误，在此提供一个方式来避免内存错误。</remarks>
    [System.Runtime.InteropServices.ComVisible( false )]
    public static class DoubleNaN
    {
        static DoubleNaN()
        {
            double v  = BitConverter.ToDouble(new byte[] { 0, 0, 0, 0, 0, 0, 248, 255 }, 0);
            NaN = v;
        }
        /// <summary>
        /// 获得非数字
        /// </summary>
        public static double NaN;
        /// <summary>
        /// 判断一个数值是否非数字
        /// </summary>
        /// <param name="v">数字</param>
        /// <returns>是否为非数字</returns>
        public static bool IsNaN(double v)
        {
            //return (ulong)(*(long*)(&v) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312uL;

            long num = BitConverter.DoubleToInt64Bits(v);
            return (num & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L;
        }
    }
}
