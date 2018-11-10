using System;
using System.Linq;

namespace RefactorTdd
{
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
            if (!IsValidDateRange(start, end))
            {
                return 0;
            }

            var budgets = _repo.GetAll();

            if (IsSameMonth(start, end))
            {
                var budget = budgets.SingleOrDefault(x => x.YearMonth.Equals(start.ToString("yyyyMM")));
                if (budget == null)
                {
                    return 0;
                }

                var dailyAmount = budget.DailyAmountOfBudget();
                var intervalDays = DaysInterval(start, end);
                return dailyAmount * intervalDays;
            }
            else
            {
                DateTime currentMonth = new DateTime(start.Year, start.Month, 1);
                double totalAmount = 0;
                do
                {
                    var budgetByMonth =
                        budgets.SingleOrDefault(x => x.YearMonth.Equals(currentMonth.ToString("yyyyMM")));
                    if (budgetByMonth != null)
                    {
                        int intervalDays = 0;
                        if (IsFirstMonth(start, currentMonth))
                        {
                            intervalDays = IntervalDays(start, LastDayOfMonth(start));
                        }
                        else if (IsLastMonth(end, currentMonth))
                        {
                            intervalDays = IntervalDays(FirstDayofMonth(end), end);
                        }
                        else
                        {
                            intervalDays = IntervalDays(FirstDayofMonth(currentMonth), LastDayOfMonth(currentMonth));
                        }
                        totalAmount += budgetByMonth.DailyAmountOfBudget() * intervalDays;
                    }

                    currentMonth = currentMonth.AddMonths(1);
                } while (currentMonth <= end);

                return totalAmount;
            }
        }

        private static DateTime FirstDayofMonth(DateTime end)
        {
            var intervalStart = new DateTime(end.Year, end.Month, 1);
            return intervalStart;
        }

        private static int IntervalDays(DateTime intervalStart, DateTime intervalEnd)
        {
            return (intervalEnd - intervalStart).Days + 1;
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