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
            var period = new Period(start, end);
            if (!period.IsValidDateRange())
            {
                return 0;
            }

            var budgets = _repo.GetAll();

            //if (IsSameMonth(period.Start, period.End))
            //{
            //    var budget = budgets.SingleOrDefault(x => x.YearMonth.Equals(period.Start.ToString("yyyyMM")));
            //    if (budget == null)
            //    {
            //        return 0;
            //    }

            //    return budget.IntervalAmount(period);
            //}
            //else
            //{
            DateTime currentMonth = new DateTime(period.Start.Year, period.Start.Month, 1);
            double totalAmount = 0;
            do
            {
                var budgetByMonth = budgets.SingleOrDefault(x => x.YearMonth.Equals(currentMonth.ToString("yyyyMM")));
                if (budgetByMonth != null)
                {
                    totalAmount += budgetByMonth.IntervalAmount(period);
                }

                currentMonth = currentMonth.AddMonths(1);
            } while (currentMonth <= period.End);

            return totalAmount;
            //}
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