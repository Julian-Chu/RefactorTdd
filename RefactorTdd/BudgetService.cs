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

            return _repo.GetAll().Sum(b => b.IntervalAmount(period));
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