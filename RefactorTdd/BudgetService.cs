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
            if (!Period.IsValidDateRange(start, end))
            {
                return 0;
            }

            var budgets = _repo.GetAll();
            DateTime tempDate = new DateTime(start.Year, start.Month, 1);
            double aggrAmount = 0;
            do
            {
                var budgetByMonth =
                    budgets.SingleOrDefault(x => x.YearMonth.Equals(tempDate.ToString("yyyyMM")));
                if (budgetByMonth != null)
                {
                    var tempEnd = tempDate.AddMonths(1) <= end ? tempDate.AddMonths(1).AddDays(-1) : end;
                    var tempStart = tempDate <= start ? start : tempDate;
                    aggrAmount += budgetByMonth.AmountPerDayInMonth*
                                  DaysInterval(tempStart, tempEnd);
                }

                tempDate = tempDate.AddMonths(1);
            } while (tempDate <= end);

            return aggrAmount;
        }

        private static bool IsSameMonth(DateTime start, DateTime end)
        {
            return start.ToString("yyyyMM") == end.ToString("yyyyMM");
        }

        private static int DaysInterval(DateTime start, DateTime end)
        {
            return end.Subtract(start).Days + 1;
        }
    }
}