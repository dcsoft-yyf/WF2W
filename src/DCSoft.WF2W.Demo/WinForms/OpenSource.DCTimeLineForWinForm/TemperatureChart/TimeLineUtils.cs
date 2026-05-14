using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    internal static class TimeLineUtils
    {

        public static DateTime AddTime(DateTime dtm, double v , DCTimeUnit unit, bool fixField)
        {
            DateTime nextTime = dtm;
            switch (unit)
            {
                case DCTimeUnit.Second:
                    nextTime = nextTime.AddSeconds(v);
                    break;
                case DCTimeUnit.Minute:
                    if (fixField)
                    {
                        nextTime = new DateTime(
                            nextTime.Year, 
                            nextTime.Month, 
                            nextTime.Day, 
                            nextTime.Hour, 
                            nextTime.Minute, 
                            0);
                    }
                    nextTime = nextTime.AddMinutes(v);
                    break;
                case DCTimeUnit.Hour:
                    if (fixField)
                    {
                        nextTime = new DateTime(nextTime.Year, nextTime.Month, nextTime.Day, nextTime.Hour, 0, 0);
                    }
                    nextTime = nextTime.AddHours(v);
                    break;
                case DCTimeUnit.Day:
                    if (fixField)
                    {
                        nextTime = new DateTime(nextTime.Year, nextTime.Month, nextTime.Day, 0, 0, 0);
                    }
                    nextTime = nextTime.AddDays(v);
                    break;
                case DCTimeUnit.Week:
                    if (fixField)
                    {
                        nextTime = new DateTime(nextTime.Year, nextTime.Month, nextTime.Day, 0, 0, 0);
                    }
                    nextTime = nextTime.AddDays(v);
                    break;
                case DCTimeUnit.Month:
                    if (fixField)
                    {
                        nextTime = new DateTime(nextTime.Year, nextTime.Month, 1, 0, 0, 0);
                    }
                    nextTime = nextTime.AddMonths((int)v);
                    break;
                case DCTimeUnit.Year:
                    if (fixField)
                    {
                        nextTime = new DateTime(nextTime.Year, 1, 1, 0, 0, 0);
                    }
                    nextTime = nextTime.AddYears((int)v);
                    break;
            }
            return nextTime;
        }
    }
}
