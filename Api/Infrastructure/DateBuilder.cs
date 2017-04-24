using System;

namespace Api.Infrastructure
{
    public static class DateBuilder
    {
        public static bool TryBuildFrom(int year, int month, int day, out DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(year, month);

            if (day > daysInMonth)
            {
                date = DateTime.MinValue;
                return false;
            }

            date = new DateTime(year, month, day);
            return true;
        }
    }
}
