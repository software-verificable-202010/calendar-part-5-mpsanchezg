using CalendarApp.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalendarApp.ViewModel
{
	public class CalendarMonthViewModel : ViewModelBase
	{
		#region Private Variables
		private DateTime currentDate;
		private int currentMonth;
		private string currentMonthName;
		private int currentYear;
		private CalendarMonthModel currentCalendarMonth;
		private List<CalendarDayModel> daysOfCurrentMonth;
		private RelayCommand goToLastMonthCommand;
		private RelayCommand goToNextMonthCommand;
		private const string daysOfCurrentMonthProperty = "DaysOfCurrentMonth";
		private const string currrentMonthProperty = "CurrentYear";
		private const string currentMonthNameProperty = "CurrentMonthName";
		private const string currentYearProperty = "CurrentYear";
		private const string currentCalendarMonthProperty = "CurrentCalendarMonth";
		#endregion

		public CalendarMonthViewModel()
		{			
			CurrentDate = DateTime.Now;
			CurrentMonth = CurrentDate.Month;
			CurrentYear = CurrentDate.Year;
			CurrentCalendarMonth = new CalendarMonthModel(CurrentMonth, CurrentYear);
			GoToNextMonthCommand = new RelayCommand(OnGoToNextMonth, CanGoToNextMonth);
			GoToLastMonthCommand = new RelayCommand(OnGoToLastMonth, CanGoToLastMonth);
		}

		#region Properties
		public DateTime CurrentDate
		{
			get => currentDate; 
			set => currentDate = value;
		}
		public int CurrentMonth { 
			get => currentMonth; 
			set 
			{ 
				currentMonth = GetCorrectNumberOfMonth(value);
				CurrentMonthName = Constants.MonthNames[currentMonth - Constants.OneMonth];
				NotifyPropertyChanged(currrentMonthProperty);
			}
		}
		public string CurrentMonthName 
		{ 
			get => currentMonthName;
			set { 
				currentMonthName = value;
				NotifyPropertyChanged(currentMonthNameProperty);
			}
		}
		public int CurrentYear 
		{ 
			get => currentYear;
			set 
			{ 
				currentYear = value;
				NotifyPropertyChanged(currentYearProperty);

			}
		}
		public List<CalendarDayModel> DaysOfCurrentMonth 
		{ 
			get => daysOfCurrentMonth;
			set 
			{ 
				daysOfCurrentMonth = value;
				NotifyPropertyChanged(daysOfCurrentMonthProperty);
			}
		}
		public CalendarMonthModel CurrentCalendarMonth
		{
			get => currentCalendarMonth;
			set
			{
				currentCalendarMonth = value;
				if(currentCalendarMonth != null)
				{
					CurrentMonth = currentCalendarMonth.MonthNumber;
					CurrentYear = currentCalendarMonth.YearOfMonth;
					SetMissingLastDaysOfWeekToCalendarMonth(currentCalendarMonth);
					NotifyPropertyChanged(currentCalendarMonthProperty);
				}
			}
		}
		public RelayCommand GoToNextMonthCommand
		{
			get => goToNextMonthCommand;
			set => goToNextMonthCommand = value;
		}
		public RelayCommand GoToLastMonthCommand
		{
			get => goToLastMonthCommand;
			set => goToLastMonthCommand = value;
		}
		#endregion

		#region Private Methods

		private void SetMissingLastDaysOfWeekToCalendarMonth(CalendarMonthModel calendarMonthModel)
		{
			CalendarDayModel firstDayOfMonth = calendarMonthModel.DaysOfMonth.First();
			if (FirstDayIsMonday(firstDayOfMonth.Date))
			{
				DaysOfCurrentMonth = currentCalendarMonth.DaysOfMonth;
				return;
			}
			DaysOfCurrentMonth = AddDaysOfLastMonth(calendarMonthModel);
			return;
		}
		private bool FirstDayIsMonday(DateTime dateTime)
		{
			if (dateTime.DayOfWeek == DayOfWeek.Monday)
			{
				return true;
			}
			return false;
		}
		private List<CalendarDayModel> AddDaysOfLastMonth(CalendarMonthModel calendarMonthModel)
		{
			List<CalendarDayModel> daysOfCalendarMonth = calendarMonthModel.DaysOfMonth;
			CalendarDayModel firstDayOfDateTimes = daysOfCalendarMonth[Constants.FirstElement];
			int numberOfDayOfWeek = (int)firstDayOfDateTimes.Date.DayOfWeek;
			int yearOfCalendarMonth = calendarMonthModel.YearOfMonth;
			int lastMonth = GetCorrectNumberOfMonth(calendarMonthModel.MonthNumber - Constants.OneMonth);
			int numberOfDayInLastMonth = DateTime.DaysInMonth(yearOfCalendarMonth, lastMonth);
			int numberOfMissingDays = GetNumberOfMissingDays(numberOfDayOfWeek);
			int numberOfDayToAdd;

			for (int day = Constants.StartDayNumber; day < numberOfMissingDays; day++)
			{
				numberOfDayToAdd = numberOfDayInLastMonth - day;
				DateTime dateOfDayToAdd = new DateTime(yearOfCalendarMonth, lastMonth, numberOfDayToAdd);
				string colorOfDayToAdd = Constants.ColorOfDaysOfOtherMonth;
				CalendarDayModel dayToAdd = new CalendarDayModel(dateOfDayToAdd, colorOfDayToAdd);
				daysOfCalendarMonth.Insert(Constants.FirstElement, dayToAdd);
			}
			return daysOfCalendarMonth;
		}

		private int GetNumberOfMissingDays(int numberOfDayOfWeek)
		{
			if (numberOfDayOfWeek < (int)DayOfWeek.Monday)
			{
				return Constants.DaysOfAWeek - 1;
			}
			return numberOfDayOfWeek - 1;
		}
		
		private bool CanGoToNextMonth()
		{
			return true;
		}
		private void OnGoToNextMonth()
		{
			int nextMonth = GetCorrectNumberOfMonth(CurrentMonth + Constants.OneMonth);

			int yearOfNextMonth = CurrentYear;
			if (nextMonth == Constants.January)
			{
				yearOfNextMonth += Constants.OneYear;
			}
			CurrentCalendarMonth = new CalendarMonthModel(nextMonth, yearOfNextMonth);
		}

		private bool CanGoToLastMonth()
		{			
			return true;
		}
		private void OnGoToLastMonth()
		{
			int lastMonth = GetCorrectNumberOfMonth(CurrentMonth - Constants.OneMonth);
			int yearOfLastMonth = CurrentYear;
			if (lastMonth == Constants.December)
			{
				yearOfLastMonth -= Constants.OneYear;
			}
			CurrentCalendarMonth = new CalendarMonthModel(lastMonth, yearOfLastMonth);
		}

		private int GetCorrectNumberOfMonth(int numberOfMonth)
		{
			int monthNumber = numberOfMonth % Constants.NumberOfMonthsInAYear;
			if (monthNumber == 0)
			{
				return Constants.December;
			}
			return monthNumber;

		}

		#endregion

	}
}
