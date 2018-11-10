using System;
using System.Collections.Generic;

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

            return OverlappingAmountBetweenBudgets(period, budgets);
        }

        private static double OverlappingAmountBetweenBudgets(Period period, List<Budget> budgets)
        {
            double totalAmount = 0;
            foreach (var budgetByMonth in budgets)
            {
                totalAmount += budgetByMonth.IntervalAmount(period);
            }

            return totalAmount;
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