using CalendarApp.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CalendarApp.ViewModel
{
	public class CalendarWeekViewModel : ViewModelBase
	{
		#region Private Variables

		private DateTime currentDay;
		private ObservableCollection<CalendarDayModel> daysOfCurrentWeek;
		private string currentMonth;
		private int currentYear;
		private CalendarWeekModel currentCalendarWeek;
		private RelayCommand goToNextWeekCommand;
		private RelayCommand goToLastWeekCommand;
		private const string currentDayProperty = "CurrentDay";
		private const string currentMonthProperty = "CurrentMonth";
		private const string currentCalendarWeekProperty = "CurrentCalendarWeek";
		private const string daysOfCurrentWeekProperty = "DaysOfCurrentWeek";
		private const string currentYearProperty = "CurrentYear";
		private UserModel currentUser;
		private ObservableCollection<UserModel> usersCollection;
		private string selectedUser;
		private const string currentUserProperty = "CurrentUser";
		private const string usersCollectionProperty = "UsersCollection";
		private const string selectedUserProperty = "SelectedUser";
		private CalendarModelContext db;

		#endregion

		public CalendarWeekViewModel()
		{
			CurrentDay = DateTime.Today;
			CurrentCalendarWeek = new CalendarWeekModel(CurrentDay);
			DaysOfCurrentWeek = new ObservableCollection<CalendarDayModel> (CurrentCalendarWeek.DaysOfWeek);
			db = new CalendarModelContext();
			CurrentUser = Constants.CurrentUser;
			GoToNextWeekCommand = new RelayCommand(OnGoToNextWeek, CanGoToNextWeek);
			GoToLastWeekCommand = new RelayCommand(OnGoToLastWeek, CanGoToLastWeek);
			GoToCalendarOfOtherUser = new RelayCommand(OnGoToCalendarOfOtherUser, CanGoToCalendarOfOtherUser);
		}

		#region Properties

		public DateTime CurrentDay 
		{ 
			get => currentDay;
			set { 
				currentDay = value;
				NotifyPropertyChanged(currentDayProperty);
			}
		}
		public ObservableCollection<CalendarDayModel> DaysOfCurrentWeek
		{ 
			get => daysOfCurrentWeek;
			set 
			{ 
				daysOfCurrentWeek = value;
				ChangeMonthOfWeek();
				ChangeYearOfWeek();
				NotifyPropertyChanged(daysOfCurrentWeekProperty);
			} 
		}
		public CalendarWeekModel CurrentCalendarWeek
		{
			get => currentCalendarWeek;
			set
			{
				currentCalendarWeek = value;
				NotifyPropertyChanged(currentCalendarWeekProperty);
			}
		}
		public string CurrentMonth
		{
			get => currentMonth;
			set
			{
				currentMonth = value;
				NotifyPropertyChanged(currentMonthProperty);
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
		public UserModel CurrentUser
		{
			get => currentUser;
			set
			{
				currentUser = value;
				NotifyPropertyChanged(currentUserProperty);
			}
		}
		public ObservableCollection<UserModel> UsersCollection
		{
			get => usersCollection;
			set
			{
				usersCollection = value;
				NotifyPropertyChanged(usersCollectionProperty);
			}
		}
		public string SelectedUser
		{
			get => selectedUser;
			set
			{
				selectedUser = value;
				NotifyPropertyChanged(selectedUserProperty);
			}
		}
		public RelayCommand GoToNextWeekCommand
		{
			get => goToNextWeekCommand;
			set => goToNextWeekCommand = value;
		}
		public RelayCommand GoToLastWeekCommand
		{
			get => goToLastWeekCommand;
			set => goToLastWeekCommand = value;
		}
		public RelayCommand GoToCalendarOfOtherUser
		{
			get;
			set;
		}

		#endregion

		#region Private Methods
		private bool CanGoToNextWeek()
		{
			return true;
		}
		private void OnGoToNextWeek()
		{
			CurrentDay = CurrentDay.AddDays(Constants.DaysOfAWeek);
			DaysOfCurrentWeek = new ObservableCollection<CalendarDayModel>(GetCalendarDaysOfWeek(CurrentDay));
		}

		private bool CanGoToLastWeek()
		{
			return true;
		}
		private void OnGoToLastWeek()
		{
			CurrentDay = CurrentDay.AddDays(-Constants.DaysOfAWeek);
			DaysOfCurrentWeek = new ObservableCollection<CalendarDayModel>(GetCalendarDaysOfWeek(CurrentDay));
		}

		private bool CanGoToCalendarOfOtherUser()
		{
			return true;
		}
		private void OnGoToCalendarOfOtherUser()
		{
			UsersCollection = GetAllUsers();
			if(SelectedUser != null)
			{
				CurrentUser = db.Users.First(u => u.UserName == SelectedUser);
				Constants.SelectedUser = CurrentUser;
				DaysOfCurrentWeek = new ObservableCollection<CalendarDayModel>(GetCalendarDaysOfWeek(CurrentDay));
			}			
		}

		private List<CalendarDayModel> GetCalendarDaysOfWeek(DateTime day)
		{
			List<CalendarDayModel> calendarDaysOfWeek = new List<CalendarDayModel>();
			List<DateTime> daysOfWeek = GetOtherDaysOfWeekByADay(day);
			string colorOfCalendarDay;
			for (int dayOfWeekIndex = Constants.FirstElement; dayOfWeekIndex < Constants.DaysOfAWeek; dayOfWeekIndex++)
			{
				if(CurrentUser.UserName == Constants.CurrentUser.UserName)
				{
					colorOfCalendarDay = PutColorOfCalendarDayOfWeek(daysOfWeek[dayOfWeekIndex]);
					calendarDaysOfWeek.Add(new CalendarDayModel(daysOfWeek[dayOfWeekIndex], colorOfCalendarDay));
					continue;
				}
				colorOfCalendarDay = PutColorOfCalendarOfWeekOfOtherUser(daysOfWeek[dayOfWeekIndex]);
				calendarDaysOfWeek.Add(new CalendarDayModel(daysOfWeek[dayOfWeekIndex], colorOfCalendarDay));

			}

			return calendarDaysOfWeek;
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
		private string PutColorOfCalendarOfWeekOfOtherUser(DateTime day)
		{
			if (IsToday(day))
			{
				return Constants.ColorOfTodayOfOtherUser;
			}
			else if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
			{
				return Constants.ColorOfWeekOfOtherUser;
			}
			return Constants.ColorOfWeekendOfOtherUser;
		}
		private bool IsToday(DateTime date)
		{
			return date == DateTime.Today;
		}
		private List<DateTime> GetOtherDaysOfWeekByADay(DateTime day)
		{
			List<DateTime> daysOfWeekBySelectedDay;
			int currentDayOfWeek = (int)day.DayOfWeek;
			DateTime sunday = day.AddDays(-currentDayOfWeek);
			DateTime monday = GetMonday(sunday, currentDayOfWeek);
			daysOfWeekBySelectedDay = Enumerable.Range(
				Constants.StartDayNumber,
				Constants.DaysOfAWeek
			).Select(days => monday.AddDays(days)).ToList();

			return daysOfWeekBySelectedDay;
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

		private void ChangeMonthOfWeek()
		{
			int monthOfThursday = DaysOfCurrentWeek[Constants.Thursday - Constants.OneDay].Date.Month;
			CurrentMonth = Constants.MonthNames[monthOfThursday - Constants.OneMonth];
		}
		private void ChangeYearOfWeek()
		{
			CurrentYear = DaysOfCurrentWeek[Constants.Thursday - Constants.OneDay].Date.Year;
		}

		private ObservableCollection<UserModel> GetAllUsers()
		{
			var users = new List<UserModel>();
			var userList = db.Users;
			foreach (var user in userList.ToList())
			{
				users.Add(user);

			}
			return new ObservableCollection<UserModel>(users);
		}

		#endregion

	}
}
