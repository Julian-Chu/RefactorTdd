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
            var period = new Period(start,end);
            if (!period.IsValidDateRange())
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
                    aggrAmount += budgetByMonth.AmountPerDayInMonth *
                                  budgetByMonth.Period.GetOverlapDays(period);
                }

                tempDate = tempDate.AddMonths(1);
            } while (tempDate <= end);

            return aggrAmount;
        }
    }
}