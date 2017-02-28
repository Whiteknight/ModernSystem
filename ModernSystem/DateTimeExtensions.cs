using System;
using System.Globalization;

namespace ModernSystem
{
    public static class DateTimeExtensions
    {
        public const string ISO8601FormatStr = "yyyy-MM-ddTHH:mm:ss.fffK";

        public static string ToStringISO8601(this DateTime dateTime)
        {
            return dateTime.ToString(ISO8601FormatStr, CultureInfo.InvariantCulture);
        }

        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }
    }
}
