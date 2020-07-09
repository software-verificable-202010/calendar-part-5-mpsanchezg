using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using CalendarApp.Model;
using CalendarApp.View;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.ViewModel
{
	public class UserManagerViewModel : ViewModelBase
	{
		#region Private Variables
		private CalendarModelContext db;
		private UserModel currentUser;
		private string logOnUserName;
		private const string userNameProperty = "LoginUserName";
		private const string currentUserProperty = "CurrentUser";
		#endregion


		public UserManagerViewModel()
		{
			db = new CalendarModelContext();
			LogOnCommand = new RelayCommand(OnLogin, CanLogin);
		}

		#region Properties
		public UserModel CurrentUser
		{
			get => currentUser;
			set
			{
				if (value == currentUser)
				{
					return;
				}
				currentUser = value;
				Constants.CurrentUser = CurrentUser;
				Constants.SelectedUser = CurrentUser;
				NotifyPropertyChanged(currentUserProperty);

			}
		}
		public string LogOnUserName
		{
			get => logOnUserName;
			set
			{
				logOnUserName = value;
				NotifyPropertyChanged(userNameProperty);
			}
		}
		public RelayCommand LogOnCommand
		{
			get;
		}
		#endregion

		#region Private Methods
		private bool CanLogin()
		{
			return true;
		}
		private void OnLogin()
		{
			const string messageBoxTitle = "Alerta.";
			if (IsValidUsername(logOnUserName))
			{
				CurrentUser = db.Users
					.Include(u => u.UserEvents)
					.ThenInclude(ue => ue.Event)
					.First(u => u.UserName == LogOnUserName);
				GoToCalendar();
				return;
			}
			MessageBox.Show(Constants.FailedLogOn, messageBoxTitle, MessageBoxButton.OK);
			return;
		}
		private bool IsValidUsername(string username)
		{
			var allUsers = db.Users.ToList();
			for (var userIndex = Constants.FirstElement; userIndex < allUsers.Count; userIndex++)
			{
				if (allUsers[userIndex].UserName == username)
				{
					return true;
				}
			}
			return false;
		}

		private void GoToCalendar()
		{
			CalendarView calendarDayView = new CalendarView();
			calendarDayView.Show();
			foreach (Window window in Application.Current.Windows)
			{
				if (window.Title == Constants.MainWindow)
				{
					window.Close();
				}
			}
		}

		#endregion
	}
}
