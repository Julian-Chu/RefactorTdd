using System;

namespace RefactorTdd
{
    public class Budget
    {
        public string YearMonth { get; set; }
        public int Amount { get; set; }

        public int Year
        {
            get
            {
                return FirstDay().Year;
            }
        }

        public DateTime FirstDay()
        {
            return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);
        }

        public int Month
        {
            get { return FirstDay().Month; }
        }

        public int DailyAmount()
        {
            return Amount / DateTime.DaysInMonth(Year, Month);
        }

        public DateTime LastDay()
        {
            var daysInMonth = DateTime.DaysInMonth(Year, Month);
            return new DateTime(FirstDay().Year, FirstDay().Month, daysInMonth);
        }

        public double IntervalAmount(Period period)
        {
            return DailyAmount() * period.IntervalDays(CreatePeriod());
        }

        private Period CreatePeriod()
        {
            return new Period(this.FirstDay(), this.LastDay());
        }
    }
}