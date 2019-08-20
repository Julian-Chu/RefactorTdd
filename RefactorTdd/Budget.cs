using System;
using System.Globalization;

namespace RefactorTdd
{
	public class Budget
	{
		public string YearMonth { get; set; }
		public int Amount { get; set; }

		public int AmountPerDayInMonth => Amount / DateTime.DaysInMonth(Date.Year, Date.Month);

		private DateTime Date => DateTime.ParseExact(YearMonth, "yyyyMM", CultureInfo.InvariantCulture);

		public Period Period
		{
			get
			{
				DateTime start = new DateTime(Date.Year, Date.Month, 1);
				DateTime end = start.AddMonths(1).AddDays(-1);
				return new Period(start, end);
			}
		}
	}
}