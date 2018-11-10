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
            var firstDay = another.Start;
            var lastDay = another.End;
            if (End < firstDay || Start > lastDay)
            {
                return 0;
            }

            DateTime intervalEnd = End;
            if (End > lastDay)
            {
                intervalEnd = lastDay;
            }

            DateTime intervalStart = Start;
            if (Start < firstDay)
            {
                intervalStart = firstDay;
            }

            return (intervalEnd - intervalStart).Days + 1;
        }
    }
}