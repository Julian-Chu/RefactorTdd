using System;

internal class Period
{
    public static bool IsValidDateRange(DateTime start, DateTime end)
    {
        return start <= end;
    }
}