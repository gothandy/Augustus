using System;

namespace Augustus
{
    public static class AugustusDateTimeExtensions
    {
        public static int GetQuarter(this DateTime date)
        {
            return Math.Abs((date.Month - 1) / 3) + 1;
        }
    }
}
