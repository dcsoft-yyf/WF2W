using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.Common
{

	/// <summary>
	/// 通用的字符串处理静态对象
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Runtime.InteropServices.ComVisible( false )]
    //[System.Reflection.Obfuscation( Exclude= true, ApplyToMembers = false  )]
	public class StringCommon
    {
        static StringCommon()
        {
            _CharStrings = new string[129];
            for(int iCount = 0;iCount <_CharStrings.Length;iCount ++)
            {
                _CharStrings[iCount] = new string((char)iCount, 1);
            }
        }

     

        private static readonly string[] _CharStrings = null;
      
        //private static string[] BuildInt32Strings()
        //{
        //    var result = new string[200];
        //    for (int iCount = 0; iCount < result.Length; iCount++)
        //    {
        //        result[iCount] = iCount.ToString();
        //    }
        //    return result;
        //}
       

        private static readonly int[] _Table_IndexOfHexChar = Build_Table_IndexOfHexChar();
        private static int[] Build_Table_IndexOfHexChar()
        {
            var result = new int[103];
            for( int iCount = 0;iCount < result.Length;iCount ++ )
            {
                int index = -1;
                if (iCount >= '0' && iCount <= '9')
                {
                    index = iCount - '0';
                }
                else if (iCount >= 'A' && iCount <= 'F')
                {
                    index = iCount - 'A' + 10;
                }
                else if (iCount >= 'a' && iCount <= 'f')
                {
                    index = iCount - 'a' + 10;
                }
                result[iCount] = index;
            }
            return result;
        }


        /// <summary>
        /// 将16进制字符转换为数字，如果不是16进制字符则返回-1
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>数值</returns>
        public static int IndexOfHexChar( char c )
        {
            if( c < 103)
            {
                return _Table_IndexOfHexChar[c];
            }
            else
            {
                return -1;
            }

            
        }

        private static readonly string[] _Int32Strings = BuildInt32Strings();
        private static string[] BuildInt32Strings()
        {
            var result = new string[200];
            for (int iCount = 0; iCount < result.Length; iCount++)
            {
                result[iCount] = iCount.ToString();
            }
            return result;
        }
        [ThreadStatic]
        private static char[] _FloatToString_Chars = new char[20];

        public static string FloatToString(float v, int maxDigsAfterZero = 10)
        {
            if (v == 0)
            {
                return "0";
            }
            if (v == 1)
            {
                return "1";
            }
            int intV = (int)v;
            if (v >= 0 && v < 200 && v == intV)
            {
                return _Int32Strings[(int)v];
            }
            else
            {
                return v.ToString();

                //if (v > 10000000000000000f || v < -10000000000000000f || float.IsNaN(v))
                //{
                //    return v.ToString();
                //}
                //long intValue = (long)v;
                //if (intValue > 100000000L || intValue < -100000000L)
                //{
                //    return v.ToString();
                //}
                //var chrs = _FloatToString_Chars;
                //if (chrs == null)
                //{
                //    chrs = new char[20];
                //    _FloatToString_Chars = chrs;
                //}
                ////Array.Clear(chrs);
                //int startIndex = 0;
                //if (v < 0)
                //{
                //    // 遇到负数
                //    v = -v;
                //    chrs[0] = '-';
                //    startIndex = 1;
                //    intValue = (long)v;
                //}
                //// 剩余的有效数字的个数,单精度浮点数最多有7个有效数字
                //int validateDigsCount = 9;
                //if (intValue > v)
                //{
                //    intValue--;
                //}
                //float vLeft = v - intValue;
                //int pos = startIndex;
                //if (intValue == 0)
                //{
                //    chrs[startIndex++] = '0';
                //}
                //else
                //{
                //    while (intValue > 0)
                //    {
                //        validateDigsCount--;
                //        var index = (int)(intValue % 10);
                //        if (validateDigsCount > 0)
                //        {
                //            chrs[startIndex++] = (char)(index + '0');
                //        }
                //        else
                //        {
                //            // 超出有效数字总个数了
                //            chrs[startIndex++] = '0';
                //        }
                //        intValue = (intValue - index) / 10;
                //    }
                //}
                //if (startIndex > pos + 1)
                //{
                //    Array.Reverse(chrs, pos, startIndex - pos);
                //}
                //// 处理小数部分
                //if (vLeft > 0 && validateDigsCount > 0)
                //{
                //    int posForPointer = startIndex;
                //    chrs[startIndex++] = '.';
                //    //int runtimeDigs = validateDigsCount ;
                //    //if(runtimeDigs > maxDigsAfterZero)
                //    //{
                //    //    runtimeDigs = maxDigsAfterZero;
                //    //}
                //    //if( runtimeDigs > 8)
                //    //{
                //    //    runtimeDigs = 8;
                //    //}

                //    while (vLeft > 0 && (maxDigsAfterZero--) >= 0)
                //    {
                //        vLeft = vLeft * 10;
                //        int index = (int)vLeft;
                //        if (index > vLeft)
                //        {
                //            index--;
                //        }
                //        if (index > 0 || validateDigsCount < 8)
                //        {
                //            validateDigsCount--;
                //        }
                //        if (validateDigsCount >= 0)
                //        {
                //            chrs[startIndex++] = (char)(index + '0');
                //        }
                //        else
                //        {
                //            // 已经没有剩余的有效数字了
                //            break;
                //        }
                //        vLeft -= index;
                //    }
                //    startIndex--;
                //    if (chrs[startIndex] >= '5')
                //    {
                //        startIndex--;
                //        // 执行四舍五入
                //        for (var iCount = startIndex; iCount > 0; iCount--)
                //        {
                //            var c = chrs[iCount];
                //            if (c == '.')
                //            {
                //                // 遇到小数点，跳过
                //                iCount--;
                //                c = chrs[iCount];
                //            }
                //            if (c != '9')
                //            {
                //                chrs[iCount]++;
                //                startIndex = iCount;
                //                break;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        startIndex--;
                //    }
                //    // 删除结尾的零
                //    if (chrs[startIndex] == '0')
                //    {
                //        while (startIndex >= posForPointer)
                //        {
                //            if (chrs[startIndex] == '0' || chrs[startIndex] == '.')
                //            {
                //                startIndex--;
                //            }
                //            else
                //            {
                //                //startIndex++;
                //                break;
                //            }
                //        }
                //    }
                //    if (chrs[startIndex] == '.')
                //    {
                //        startIndex--;
                //    }
                //    return new string(chrs, 0, startIndex + 1);
                //}
                //else
                //{
                //    return new string(chrs, 0, startIndex);
                //}
            }
        }
        public static string Int32ToString(int v)
        {
            if (v == 0)
            {
                return "0";
            }
            if (v == 1)
            {
                return "1";
            }
            if (v >= 0 && v < 200)
            {
                return _Int32Strings[v];
            }
            else
            {
                return v.ToString();
            }
        }

        //public static string[] SplitToLines( string txt )
        //{
        //    if( txt == null )
        //    {
        //        return null;
        //    }
        //    if( txt.Length == 0 )
        //    {
        //        return new string[] { txt };
        //    }
        //    var reader = new System.IO.StringReader(txt);
        //    var lines = new List<string>();
        //    var line = reader.ReadLine();
        //    while( line != null )
        //    {
        //        lines.Add(line);
        //        line = reader.ReadLine();
        //    }
        //    reader.Close();
        //    return lines.ToArray();
        //}


        private static readonly int[] _Base64Indexs = BuildBase64Indexs();
        private static int[] BuildBase64Indexs()
        {
            var chrs = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            var result = new int[127];
            for(int iCount = 0; iCount < 127; iCount ++ )
            {
                result[iCount] = chrs.IndexOf((char)iCount);
            }
            return result;
        }

	
        public static int IndexOfBase64Char( char c )
        {
            if (c < 127)
            {
                return _Base64Indexs[c];
            }
            else
            {
                return -1;
            }
        }

		public static unsafe byte[] TryConvertFromBase64String( string strData )
		{
            if ( strData == null || strData.Length == 0 )
			{
				return null;
			}
			BitCounter c = new BitCounter();
			int len = strData.Length;
            for( int iCount = 0; iCount < len; iCount ++ )
            {
                int index = IndexOfBase64Char(strData[iCount]);
                if( index >= 0 )
                {
                    c.AddBit(6, index);
                }
            }
            return c.ToArray();

		}

		private class BitCounter
		{
			private readonly List<byte> _Values = new List<byte>();
			private long _CurrentValue = 0;
			private int _BitCount = 0;
			public void AddBit(int bites, int Value)
			{
				if (bites > 0)
				{
					_CurrentValue = (_CurrentValue << bites) + Value ;
					_BitCount += bites;

					if (_BitCount >= 32)
					{
						Fill(false);
					}
				}
			}
			private void Fill( bool fillAllBits  )
			{
				long cv = _CurrentValue;
				int bitLeft = _BitCount % 8;
				if( bitLeft > 0 )
				{
					long mask = (1 << (bitLeft )) - 1;
					_CurrentValue = _CurrentValue & mask;
					cv = cv >> bitLeft;
					_BitCount -= bitLeft;
				}
				if (_BitCount == 32)
				{
					byte[] bs = BitConverter.GetBytes((int)cv);
					_Values.Add(bs[3]);
					_Values.Add(bs[2]);
					_Values.Add(bs[1]);
					_Values.Add(bs[0]);
					_BitCount = 0;
				}
				else
				{
					while (_BitCount > 0)
					{
						_BitCount -= 8;
						byte v = (byte)((cv >> _BitCount) & 0xff);
						_Values.Add(v);
					}
				}
				//if(fillAllBits && bitLeft > 0 )
				//{
				//	_Values.Add((byte)_CurrentValue);
				//	_BitCount = 0;
				//}
				//else
				{
					_BitCount = bitLeft;
				}
			}
			public byte[] ToArray()
			{
                if (_BitCount > 0)
                {
                    Fill(true);
                }
                return _Values.ToArray();
			}
		}
	}// class StringCommon
}
