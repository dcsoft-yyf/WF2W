using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;
using DCSoft.Common ;
//using System.Windows.Forms;

namespace DCSoft.TemperatureChart
{
    internal static class DCTimeLineUtils
    {
#if !DCWriterForWASM
        public static string GetDateTimeFormatString(DateTimePrecisionMode precision)
        {
            switch (precision)
            {
                case DateTimePrecisionMode.NoLimited :
                    return "yyyy-MM-dd HH:mm:ss.ff";
                case DateTimePrecisionMode.Second :
                    return "yyyy-MM-dd HH:mm:ss";
                case DateTimePrecisionMode.Minute :
                    return "yyyy-MM-dd HH:mm";
                case DateTimePrecisionMode.Hour :
                    return "yyyy-MM-dd HH";
                case DateTimePrecisionMode.Day :
                    return "yyyy-MM-dd";
                case DateTimePrecisionMode.Month :
                    return "yyyy-MM";
                case DateTimePrecisionMode.Year :
                    return "yyyy";
            }
            return "yyyy-MM-dd HH:mm:ss";
        }
        public static DateTime FormatDateTime(DateTime dtm, DateTimePrecisionMode precision)
        {
            switch (precision)
            {
                case DateTimePrecisionMode.NoLimited :
                    // 无限制
                    return dtm;
                case DateTimePrecisionMode.Second :
                    // 精确到秒
                    return new DateTime(dtm.Year, dtm.Month, dtm.Day, dtm.Hour, dtm.Minute, dtm.Second);
                case DateTimePrecisionMode.Minute :
                    // 精确到分钟
                    return new DateTime(dtm.Year, dtm.Month, dtm.Day, dtm.Hour, dtm.Minute, 0);
                case DateTimePrecisionMode.Hour :
                    // 精确到小时
                    return new DateTime(dtm.Year, dtm.Month, dtm.Day, dtm.Hour, 0 , 0);
                case DateTimePrecisionMode.Day :
                    // 精确到天
                    return new DateTime(dtm.Year, dtm.Month, dtm.Day );
                case DateTimePrecisionMode.Month :
                    // 精确到月份
                    return new DateTime(dtm.Year, dtm.Month, 1);
                case DateTimePrecisionMode.Year :
                    // 精确到年
                    return new DateTime(dtm.Year, 1, 1);
            }
            return dtm;
        }

        public static float ParseInputValue(
            string Value, 
            float maxValue, 
            float minValue, 
            ref string message,
            bool allowNullValue ,
            bool allowOutofRange)
        {
            if (string.IsNullOrEmpty(Value))
            {
                if (allowNullValue == false )
                {
                    message = DCTimeLineStrings.RequiredValue;
                }
                return float.NaN;
            }
            float v = 0;
            if (float.TryParse(Value, out v))
            {
                if (IsOutofRange(v, maxValue, minValue))
                {
                    if (allowOutofRange == false )
                    {
                        message = string.Format(
                            DCTimeLineStrings.OutofRange_Max_Min, 
                            maxValue, 
                            minValue);
                    }
                }
                return v;
            }
            return float.NaN;
        }
        /// <summary>
        /// 判断数值是否超出范围
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="maxValue">范围的最大值</param>
        /// <param name="minValue">范围的最小值</param>
        /// <returns>是否超出范围</returns>
        public static bool IsOutofRange(float v , float maxValue , float minValue)
        {
            if (TemperatureDocument.IsNullValue(v))
            {
                return false;
            }
            if (TemperatureDocument.IsNullValue(minValue) == false)
            {
                if (v < minValue)
                {
                    return true;
                }
            }
            if (TemperatureDocument.IsNullValue(maxValue) == false)
            {
                if (v > maxValue)
                {
                    return true;
                }
            }
            return false;
        }
#endif

        public static string ColorToXMLString(Color c, Color defaultValue)
        {
            return XMLSerializeHelper.ColorToString(c, defaultValue);
        }
        public static  Color  XMLStringToColor(string v, Color defaultValue)
        {
            return XMLSerializeHelper.StringToColor(v, defaultValue);
        }

    }
}
