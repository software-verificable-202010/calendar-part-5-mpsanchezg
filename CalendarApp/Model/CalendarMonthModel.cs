using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CalendarApp.Model
{
	public class CalendarMonthModel
	{
		#region Private Fields

		private int monthNumber;
		private List<CalendarDayModel> daysOfMonth;
		private int yearOfMonth;

		#endregion

		public CalendarMonthModel(int monthNumber, int year)
		{
			YearOfMonth = year;
			MonthNumber = monthNumber;

		}

		#region Properties

		public int MonthNumber 
		{ 
			get => monthNumber;
			set 
			{ 
				monthNumber = value;
				daysOfMonth = SetDaysOfTheMonth(MonthNumber, YearOfMonth);
			}
		}
		public List<CalendarDayModel> DaysOfMonth
		{
			get => daysOfMonth;
			set => daysOfMonth = value;
		}
		public int YearOfMonth
		{
			get => yearOfMonth;
			set => yearOfMonth = value;
		}

		#endregion

		#region Private Methods

		private List<CalendarDayModel> SetDaysOfTheMonth(int month, int year)
		{
			List<CalendarDayModel> newDaysOfMonth = new List<CalendarDayModel>();
			int firstNumberOfDay = 1;
			int nDaysInMonth = DateTime.DaysInMonth(year, month);

			for (int dayNumber = firstNumberOfDay; dayNumber <= nDaysInMonth; dayNumber++)
			{
				DateTime dayToAdd = new DateTime(year, month, dayNumber);
				AddDayWithMoreProperties(dayToAdd, newDaysOfMonth);
			}

			return newDaysOfMonth;
		}
		private void AddDayWithMoreProperties(DateTime dayToAdd, List<CalendarDayModel> calendarDaysOfMonth)
		{
			string colorOfDayToAdd = PutColorByDayOfWeek(dayToAdd);
			CalendarDayModel calendarDay = new CalendarDayModel(dayToAdd, colorOfDayToAdd);
			calendarDaysOfMonth.Add(calendarDay);
		}

		private string PutColorByDayOfWeek(DateTime day)
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

		#region Public Methods
		
		public override bool Equals(object obj)
		{
			return Equals(obj as CalendarMonthModel);
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public bool Equals(CalendarMonthModel calendarMonth)
		{
			if (calendarMonth == null)
			{
				return false;
			}
			return MonthNumber == calendarMonth.MonthNumber && YearOfMonth == calendarMonth.YearOfMonth;
		}
		#endregion
	}
}
