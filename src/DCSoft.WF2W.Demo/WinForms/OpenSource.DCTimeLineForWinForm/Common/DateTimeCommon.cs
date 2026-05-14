using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.Common
{
    [System.Runtime.InteropServices.ComVisible( false )]
    public static class DateTimeCommon
    {
        private static DateTime _StartTime = DateTime.Now;
        public static DateTime GetTimeForSecond(DateTime dtm )
        {
            //var result = dtm.AddMilliseconds(-dtm.Millisecond);

            return new DateTime(dtm.Year, dtm.Month, dtm.Day, dtm.Hour, dtm.Minute, dtm.Second);
        }
#if !DCWriterForWASM
        /// <summary>
        /// 获得精确到秒的当前时刻
        /// </summary>
        public static DateTime NowInSecond
        {
            get
            {
                var result = DateTime.Now;
                return new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second);
            }
        }
        /// <summary>
        /// 获得应用程序启动后时刻数
        /// </summary>
        /// <returns>时刻数</returns>
        /// <remarks>
        /// System.GetTickCount()会循环重复，在某些电脑还计算错误。在此替换功能
        /// </remarks>
        public static long GetInt64TickCount()
        {
            return (long)(DateTime.Now - _StartTime).TotalMilliseconds;
        }
        public static string FormatTimeSpan( TimeSpan span )
        {
            var str = new System.Text.StringBuilder();
            if(span.Days > 0 )
            {
                str.Append(span.Days + "天");
            }
            if( span.Hours > 0 )
            {
                str.Append(span.Hours + "小时");
            }
            if(span.Minutes > 0 )
            {
                str.Append(span.Minutes + "分");
            }
            if(span.Seconds > 0 )
            {
                str.Append(span.Seconds + "秒");
            }
            return str.ToString();
             
        }
        public static string GetNowYYYY_MM_DD_HH_MM_SS()
        {
            return FastToYYYY_MM_DD_HH_MM_SS(DateTime.Now);
        }
#endif

        public const string FORMAT_yyyy_MM_dd_HH_mm_ss = "yyyy-MM-dd HH:mm:ss";

        [ThreadStatic]
        private static char[] _Chars = "2020-05-04 15:28:11".ToCharArray();
        /// <summary>
        /// 快速将日期时间转换为字符串
        /// </summary>
        /// <param name="dtm"></param>
        /// <returns></returns>
        public static string FastToYYYY_MM_DD_HH_MM_SS(DateTime dtm)
        {
            if (_Chars == null)
            {
                // -------0123456789012345678
                _Chars = "2020-05-04 15:28:11".ToCharArray();
            }
            int dec = 0;
            int num = dtm.Year;
            dec = num % 10; _Chars[3] = (char)(dec + '0'); num = (num - dec) / 10;
            dec = num % 10; _Chars[2] = (char)(dec + '0'); num = (num - dec) / 10;
            dec = num % 10; _Chars[1] = (char)(dec + '0'); num = (num - dec) / 10;
            dec = num % 10; _Chars[0] = (char)(dec + '0');

            num = dtm.Month;
            dec = num % 10; _Chars[6] = (char)(dec + '0'); num = (num - dec) / 10;
            dec = num % 10; _Chars[5] = (char)(dec + '0');

            num = dtm.Day;
            dec = num % 10; _Chars[9] = (char)(dec + '0'); num = (num - dec) / 10;
            dec = num % 10; _Chars[8] = (char)(dec + '0');

            num = dtm.Hour;
            dec = num % 10; _Chars[12] = (char)(dec + '0'); num = (num - dec) / 10;
            dec = num % 10; _Chars[11] = (char)(dec + '0');

            num = dtm.Minute;
            dec = num % 10; _Chars[15] = (char)(dec + '0'); num = (num - dec) / 10;
            dec = num % 10; _Chars[14] = (char)(dec + '0');

            num = dtm.Second;
            dec = num % 10; _Chars[18] = (char)(dec + '0'); num = (num - dec) / 10;
            dec = num % 10; _Chars[17] = (char)(dec + '0');

            return new string(_Chars);
        }
#if !DCWriterForWASM
        public static DateTime ParseYYYY_MM_DD(string str, DateTime defaultValue)
        {
            if (str == null || str.Length < 10)
            {
                return defaultValue;
            }
            try
            {
                int year = 1000 * (str[0] - '0') + 100 * (str[1] - '0') + 10 * (str[2] - '0') + 10 * (str[3] - '0');
                int mon_ = 10 * (str[05] - '0') + (str[06] - '0');
                int day_ = 10 * (str[08] - '0') + (str[09] - '0');
                return new DateTime(year, mon_, day_);
            }
            catch (Exception ext)
            {
                return defaultValue;
            }
        }

        public static bool TryParseHH_MM_SS(string str, ref TimeSpan v)
        {
            if (str == null || str.Length < 8)
            {
                return false ;
            }
            try
            {
                int hour = 10 * (str[0] - '0') + (str[1] - '0');
                int min_ = 10 * (str[3] - '0') + (str[4] - '0');
                int sec_ = 10 * (str[6] - '0') + (str[7] - '0');
                v = new TimeSpan(hour, min_, sec_);
                return true;
            }
            catch (Exception ext)
            {
                return false ;
            }
        }
#endif
        public static DateTime FastFromYYYY_MM_DD_HH_MM_SS(string str, DateTime defaultValue)
        {
            if (str == null || str.Length < 19)
            {
                return defaultValue;
            }
            try
            {
                int year = 1000 * (str[0] - '0') + 100 * (str[1] - '0') + 10 * (str[2] - '0') + 1 * (str[3] - '0');
                int mon_ = 10 * (str[05] - '0') + (str[06] - '0');
                int day_ = 10 * (str[08] - '0') + (str[09] - '0');
                int hour = 10 * (str[11] - '0') + (str[12] - '0');
                int min_ = 10 * (str[14] - '0') + (str[15] - '0');
                int sec_ = 10 * (str[17] - '0') + (str[18] - '0');
                return new DateTime(year, mon_, day_, hour, min_, sec_);
            }
            catch (Exception ext)
            {
                return defaultValue;
            }
        }


#if !DCWriterForWASM
        private static int CharToNumber(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            else
            {
                return 0;
            }
        }
        private static void ToNumberString(int v, char[] str, int startIndex, int chrLen)
        {
            for (int iCount = chrLen - 1; iCount >= 0; iCount--)
            {
                int dec = v % 10;
                str[startIndex + iCount] = (char)(dec + '0');
                v -= dec;
                v = v / 10;
            }
        }
#endif
    }
}
