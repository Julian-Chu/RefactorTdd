using System;
using System.Linq;

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
    }

    public class BudgetService
    {
        private readonly IBudgetRepo _repo;

        private static void Main()
        {
        }

        public BudgetService(IBudgetRepo repo)
        {
            _repo = repo;
        }

        public double TotalAmount(DateTime start, DateTime end)
        {
            var period = new Period(start, end);
            var mystart = period.Start;
            var myEnd = period.End;
            if (!IsValidDateRange(mystart, myEnd))
            {
                return 0;
            }

            var budgets = _repo.GetAll();

            if (IsSameMonth(mystart, myEnd))
            {
                var budget = budgets.SingleOrDefault(x => x.YearMonth.Equals(mystart.ToString("yyyyMM")));
                if (budget == null)
                {
                    return 0;
                }

                var dailyAmount = budget.DailyAmountOfBudget();
                var intervalDays = DaysInterval(mystart, myEnd);
                return dailyAmount * intervalDays;
            }
            else
            {
                DateTime currentMonth = new DateTime(mystart.Year, mystart.Month, 1);
                double totalAmount = 0;
                do
                {
                    var budgetByMonth = budgets.SingleOrDefault(x => x.YearMonth.Equals(currentMonth.ToString("yyyyMM")));
                    if (budgetByMonth != null)
                    {
                        var intervalDays = IntervalDays(period, budgetByMonth);
                        totalAmount += budgetByMonth.DailyAmountOfBudget() * intervalDays;
                    }

                    currentMonth = currentMonth.AddMonths(1);
                } while (currentMonth <= myEnd);

                return totalAmount;
            }
        }

        private static int IntervalDays(Period period, Budget budgetByMonth)
        {
            DateTime intervalStart;
            DateTime intervalEnd;
            if (IsFirstMonth(period.Start, budgetByMonth.FirstDayOfMonth()))
            {
                intervalStart = period.Start;
                intervalEnd = budgetByMonth.LastDay();
            }
            else if (IsLastMonth(period.End, budgetByMonth.FirstDayOfMonth()))
            {
                intervalStart = budgetByMonth.FirstDayOfMonth();
                intervalEnd = period.End;
            }
            else
            {
                intervalStart = budgetByMonth.FirstDayOfMonth();
                intervalEnd = budgetByMonth.LastDay();
            }

            var intervalDays = (intervalEnd - intervalStart).Days + 1;
            return intervalDays;
        }

        private static DateTime FirstDayOfMonth(DateTime end)
        {
            var intervalStart = new DateTime(end.Year, end.Month, 1);
            return intervalStart;
        }

        private static DateTime LastDayOfMonth(DateTime start)
        {
            var intervalEndDay = DateTime.DaysInMonth(start.Year, start.Month);
            var intervalEnd = new DateTime(start.Year, start.Month, intervalEndDay);
            return intervalEnd;
        }

        private static bool IsLastMonth(DateTime end, DateTime currentMonth)
        {
            return currentMonth.ToString("yyyyMM") == end.ToString("yyyyMM");
        }

        private static bool IsFirstMonth(DateTime start, DateTime currentMonth)
        {
            return currentMonth.ToString("yyyyMM") == start.ToString("yyyyMM");
        }

        private static bool IsSameMonth(DateTime start, DateTime end)
        {
            return start.ToString("yyyyMM") == end.ToString("yyyyMM");
        }

        private static bool IsValidDateRange(DateTime start, DateTime end)
        {
            return start <= end;
        }

        private static int DaysInterval(DateTime start, DateTime end)
        {
            return end.Subtract(start).Days + 1;
        }
    }
}