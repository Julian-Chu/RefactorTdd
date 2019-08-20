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
	}
}