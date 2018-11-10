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
            if (!IsValidDateRange())
            {
                return 0;
            }
            DateTime intervalEnd = End;
            if (End > budgetByMonth.LastDay())
            {
                intervalEnd = budgetByMonth.LastDay();
            }

            DateTime intervalStart = Start;
            if (Start < budgetByMonth.FirstDay())
            {
                intervalStart = budgetByMonth.FirstDay();
            }

            return (intervalEnd - intervalStart).Days + 1;
        }

        public bool IsValidDateRange()
        {
            return Start <= End;
        }
    }
}