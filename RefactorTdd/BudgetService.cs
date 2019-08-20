using System;
using System.Collections.Generic;
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

            return _repo.GetAll().Sum(b => b.AmountPerDayInMonth * b.Period.GetOverlapDays(period));
        }
    }
}