using System;

namespace RefactorTdd
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public int IntervalDays(Budget budgetByMonth)
        {
            if (Start > End)
            {
                return 0;
            }

            var another = new Period(budgetByMonth.FirstDay(), budgetByMonth.LastDay());
            if (End < another.Start || Start > another.End)
            {
                return 0;
            }

            DateTime intervalEnd = End;
            if (End > another.End)
            {
                intervalEnd = another.End;
            }

            DateTime intervalStart = Start;
            if (Start < another.Start)
            {
                intervalStart = another.Start;
            }

            return (intervalEnd - intervalStart).Days + 1;
        }
    }
}