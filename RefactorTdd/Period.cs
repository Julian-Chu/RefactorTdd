using System;

public class Period
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public bool IsValidDateRange()
    {
        return Start <= End;
    }

    public int GetOverlapDays(Period anotherPeriod)
    {
        var tempEnd = End <= anotherPeriod.End ? End : anotherPeriod.End;
        var tempStart = Start <= anotherPeriod.Start ? anotherPeriod.Start : Start;
        return tempEnd.Subtract(tempStart).Days + 1;
    }
}