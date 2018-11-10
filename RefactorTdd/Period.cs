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

        public int IntervalDays(Period another)
        {
            if (IsInvalid())
            {
                return 0;
            }

            if (HasNoOverlap(another))
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

        private bool HasNoOverlap(Period another)
        {
            return End < another.Start || Start > another.End;
        }

        private bool IsInvalid()
        {
            return Start > End;
        }
    }
}