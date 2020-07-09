using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Model
{
	public class CalendarWeekModel
	{
		#region Private Variables
		
		private int year;
		private int monthNumber;
		private DateTime selectedDateOfWeek;
		private List<CalendarDayModel> daysOfWeek;

		#endregion

		public CalendarWeekModel(DateTime selectedDateOfWeek)
		{
			SelectedDateOfWeek = selectedDateOfWeek;
			MonthNumber = SelectedDateOfWeek.Month;
			Year = SelectedDateOfWeek.Year;
			DaysOfWeek = GetCalendarDaysOfWeek(SelectedDateOfWeek);
		}

		#region Properties

		public int Year
		{
			get => year;
			set => year = value;
		}
		public int MonthNumber
		{
			get => monthNumber;
			set => monthNumber = value;
		}
		public DateTime SelectedDateOfWeek
		{
			get => selectedDateOfWeek;
			set => selectedDateOfWeek = value;
		}
		public List<CalendarDayModel> DaysOfWeek
		{
			get => daysOfWeek;
			set => daysOfWeek = value;
		}

		#endregion

		#region Private Methods
		private List<CalendarDayModel> GetCalendarDaysOfWeek(DateTime dateOfWeek)
		{
			List<CalendarDayModel> calendarDaysOfWeek = new List<CalendarDayModel>();
			List<DateTime> daysOfWeekOfCalendar = GetDaysOfWeekBySelectedDay(dateOfWeek);
			for (int dayOfWeekIndex = Constants.FirstElement; dayOfWeekIndex < Constants.DaysOfAWeek; dayOfWeekIndex++)
			{
				string colorOfCalendarDay = PutColorOfCalendarDayOfWeek(daysOfWeekOfCalendar[dayOfWeekIndex]);
				calendarDaysOfWeek.Add(new CalendarDayModel(daysOfWeekOfCalendar[dayOfWeekIndex], colorOfCalendarDay));
			}

			return calendarDaysOfWeek;
		}
		private List<DateTime> GetDaysOfWeekBySelectedDay(DateTime selectedDayOfWeek)
		{
			int currentDayOfWeek = (int) selectedDayOfWeek.DayOfWeek;
			DateTime sunday = selectedDayOfWeek.AddDays(-currentDayOfWeek);
			DateTime monday = GetMonday(sunday, currentDayOfWeek);
			var daysOfWeekByASelectedDay = Enumerable.Range(
				Constants.StartDayNumber, 
				Constants.DaysOfAWeek
			).Select(days => monday.AddDays(days)).ToList();

			return daysOfWeekByASelectedDay;
		}
		private DateTime GetMonday(DateTime sunday, int currentDayOfWeek)
		{
			DateTime monday = sunday.AddDays(Constants.OneDay);
			if (currentDayOfWeek == Constants.StartDayNumber)
			{
				return monday.AddDays(-Constants.DaysOfAWeek);
			}
			return monday;
		}
		private string PutColorOfCalendarDayOfWeek(DateTime day)
		{
			if (IsToday(day))
			{
				return Constants.ColorOfToday;
			}
			else if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
			{
				return Constants.ColorOfWeek;
			}
			return Constants.ColorOfWeekend;
		}
		private bool IsToday(DateTime date)
		{
			return date == DateTime.Today;
		}
		#endregion

	}
}
