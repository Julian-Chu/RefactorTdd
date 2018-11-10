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

                var dailyAmount = budget.Amount / DateTime.DaysInMonth(start.Year, start.Month);
                return dailyAmount * DaysInterval(start, end);
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
                        if (currentMonth.ToString("yyyyMM") == start.ToString("yyyyMM"))
                            totalAmount += AmountPerDayInMonth(budgetByMonth, start) *
                                          (DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1);
                        else if (currentMonth.ToString("yyyyMM") == end.ToString("yyyyMM"))
                            totalAmount += AmountPerDayInMonth(budgetByMonth, end) * end.Day;
                        else
                            totalAmount += budgetByMonth.Amount;
                    }

                    currentMonth = currentMonth.AddMonths(1);
                } while (currentMonth <= end);

                return totalAmount;
            }
        }

        private static bool IsSameMonth(DateTime start, DateTime end)
        {
            return start.ToString("yyyyMM") == end.ToString("yyyyMM");
        }

        private static int AmountPerDayInMonth(Budget budgetByMonth, DateTime tempDate)
        {
            return budgetByMonth.Amount / DateTime.DaysInMonth(tempDate.Year, tempDate.Month);
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