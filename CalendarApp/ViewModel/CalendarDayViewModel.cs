using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarApp.Model;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.ViewModel
{
	class CalendarDayViewModel : ViewModelBase
	{
		#region Private Fields

		private UserModel currentUser;
		private ObservableCollection<UserModel> usersCollection;
		private ObservableCollection<EventModel> userEventsCollection;
		private const string userEventsCollectionProperty = "UserEventsCollection";
		private const string currentUserProperty = "CurrentUser";
		private const string usersCollectionProperty = "UsersCollection";
		private CalendarModelContext db;

		#endregion

		public CalendarDayViewModel()
		{
			db = new CalendarModelContext();
			CurrentUser = Constants.SelectedUser;
			
		}

		#region Properties

		public ObservableCollection<EventModel> UserEventsCollection
		{
			get => userEventsCollection;
			set
			{
				userEventsCollection = value;
				NotifyPropertyChanged(userEventsCollectionProperty);
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
		#endregion

		

	}
}
