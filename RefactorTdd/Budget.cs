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
                return FirstDayOfMonth().Year;
            }
        }

        private DateTime FirstDayOfMonth()
        {
            return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);
        }

        public int Month
        {
            get { return FirstDayOfMonth().Month; }
        }

        public int DailyAmountOfBudget()
        {
            return Amount / DateTime.DaysInMonth(Year, Month);
        }
    }
}