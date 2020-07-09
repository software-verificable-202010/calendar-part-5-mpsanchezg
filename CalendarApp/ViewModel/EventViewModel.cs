using CalendarApp.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.ViewModel
{
	public class EventViewModel : ViewModelBase
	{
		#region Private Variables
		private string title;
		private string description;
		private DateTime startDateAndTime;
		private DateTime finishDateAndTime;
		private UserModel currentUser;
		private string invitedUser;
		private List<EventModel> userEvents;
		private ObservableCollection<EventModel> currentUserEventsCollection;
		private ObservableCollection<UserModel> allUsers;
		private ObservableCollection<UserModel> invitedUsers;
		private int customEventId;
		private EventModel customEvent;
		private const string finishDateAndTimeProperty = "FinishDateAndTime";
		private const string startDateAndTimeProperty = "StartDateAndTime";
		private const string descriptionProperty = "Description";
		private const string titleProperty = "Title";
		private const string currentUserProperty = "CurrentUser";
		private const string userEventsProperty = "UserEvents";
		private const string invitedUsersProperty = "InvitedUsers";
		private const string allUserEventsProperty = "AllUserEvents";
		private const string invitedUserProperty = "InvitedUser";
		private const string currentUserEventsCollectionProperty = "CurrentUserEventsCollection";
		private const string customEventProperty = "CustomEvent";
		private const string customEventIdProperty = "CustomEventId";
		private CalendarModelContext db;
		#endregion

		public EventViewModel()
		{
			db = new CalendarModelContext();
			StartDateAndTime = DateTime.Today;
			FinishDateAndTime = DateTime.Today;
			// TODO: change the static variable current user
			CurrentUser = Constants.CurrentUser;
			CurrentUserEvents = GetUserEvents(CurrentUser);
			CurrentUserEventsCollection = new ObservableCollection<EventModel>(CurrentUserEvents);
			AllUsers = new ObservableCollection<UserModel>(GetAllUsers());
			InvitedUsers = new ObservableCollection<UserModel>();
			CreateEventCommand = new RelayCommand(OnCreateEvent, CanCreateEvent);
			AddInvitedUsersCommand = new RelayCommand(OnInviteUsers, CanInviteUsers);
			EditEventCommand = new RelayCommand(OnEditEvent, CanEditEvent);
			DeleteEventCommand = new RelayCommand(OnDeleteEvent, CanDeleteEvent);
		}

		#region Properties
		public DateTime FinishDateAndTime
		{
			get => finishDateAndTime;
			set
			{
				finishDateAndTime = value;
				NotifyPropertyChanged(finishDateAndTimeProperty);
			}
		}
		public DateTime StartDateAndTime
		{
			get => startDateAndTime;
			set
			{
				startDateAndTime = value;
				NotifyPropertyChanged(startDateAndTimeProperty);
			}
		}
		public string Description
		{
			get => description;
			set
			{
				description = value;
				NotifyPropertyChanged(descriptionProperty);
			}
		}
		public string Title
		{
			get => title;
			set
			{
				title = value;
				NotifyPropertyChanged(titleProperty);
			}
		}
		public List<EventModel> CurrentUserEvents
		{
			get => userEvents;
			set
			{
				userEvents = value;
				NotifyPropertyChanged(userEventsProperty);
			}
		}
		public UserModel CurrentUser
		{
			get => currentUser;
			set
			{
				if (value != currentUser)
				{
					currentUser = value;
					NotifyPropertyChanged(currentUserProperty);
				}
			}
		}
		public string InvitedUser
		{
			get => invitedUser;
			set
			{
				if (value != invitedUser)
				{
					invitedUser = value;
					NotifyPropertyChanged(invitedUserProperty);
				}
			}
		}
		public ObservableCollection<UserModel> AllUsers
		{
			get => allUsers;
			set
			{
				allUsers = value;
				NotifyPropertyChanged(allUserEventsProperty);
			}
		}
		public ObservableCollection<UserModel> InvitedUsers
		{
			get => invitedUsers;
			set
			{
				invitedUsers = value;
				NotifyPropertyChanged(invitedUsersProperty);
			}
		}
		public ObservableCollection<EventModel> CurrentUserEventsCollection
		{
			get => currentUserEventsCollection;
			set
			{
				currentUserEventsCollection = value;
				NotifyPropertyChanged(currentUserEventsCollectionProperty);
			}
		}
		public EventModel CustomEvent
		{
			get => customEvent;
			set
			{
				customEvent = value;
				NotifyPropertyChanged(customEventProperty);
			}
		}
		public int CustomEventId
		{
			get => customEventId;
			set
			{
				customEventId = value;
				CustomEvent = GetEventById(customEventId);
				NotifyPropertyChanged(customEventIdProperty);
			}
		}
		public RelayCommand CreateEventCommand
		{
			get;
		}
		public RelayCommand AddInvitedUsersCommand
		{
			get;
		}
		public RelayCommand EditEventCommand
		{
			get;
		}
		public RelayCommand DeleteEventCommand
		{
			get;
		}
		#endregion

		#region Private Methods
		private bool CanCreateEvent()
		{
			return true;
		}
		private void OnCreateEvent()
		{
			const string messageBoxTitle = "Alerta.";
			if (AreValidData(Title, StartDateAndTime, FinishDateAndTime))
			{
				CreateEvent(Title, StartDateAndTime, FinishDateAndTime, Description);
				CurrentUserEvents = GetUserEvents(CurrentUser);
				MessageBox.Show(Constants.SuccessfulEvent, CurrentUser.UserName, MessageBoxButton.OK);
				return;
			}

			MessageBox.Show(Constants.FailedEvent, messageBoxTitle, MessageBoxButton.OK);
		}
		private void CreateEvent(string titleOfEvent, DateTime startDateAndTimeOfEvent, DateTime finishDateAndTimeOfEvent, string descriptionOfEvent)
		{
			UserModel currentUserCopy = db.Users.Include(u => u.UserEvents).ThenInclude(ue => ue.Event)
				.First(u => u.UserName == CurrentUser.UserName);
			var newEvent = new EventModel(currentUserCopy, titleOfEvent, startDateAndTimeOfEvent, finishDateAndTimeOfEvent, descriptionOfEvent);
			UserEventModel newUserEvents = new UserEventModel(currentUserCopy, newEvent);
			db.Add(newUserEvents);
			db.SaveChanges();
			InviteOtherUsers(newEvent);
			CurrentUser = currentUserCopy;
			CurrentUserEvents.Add(newEvent);
			CurrentUserEventsCollection.Add(newEvent);

		}
		private void InviteOtherUsers(EventModel eventModel)
		{
			if (InvitedUsers.Count > 0)
			{
				foreach (var invitedUser in InvitedUsers)
				{
					UserModel invitedUserCopy = db.Users.Include(u => u.UserEvents).ThenInclude(ue => ue.Event)
						.First(u => u.UserName == invitedUser.UserName);
					var newEvent = eventModel;
					UserEventModel newUserEvents = new UserEventModel(invitedUserCopy, newEvent);
					db.Add(newUserEvents);
					db.SaveChanges();

				}
			}
		}

		private bool CanInviteUsers()
		{
			return true;
		}
		private void OnInviteUsers()
		{
			const string messageBoxTitle = "Agregar Invitado.";
			if (InvitedUser == null)
			{
				MessageBox.Show(Constants.InvalidInvited, messageBoxTitle, MessageBoxButton.OK);
				return;
			}
			var invited = db.Users.First(u => u.UserName == InvitedUser);
			if (!InvitedUsers.Contains(invited))
			{
				InvitedUsers.Add(invited);
			}

			MessageBox.Show(Constants.InvitedAdded, messageBoxTitle, MessageBoxButton.OK);
		}

		private bool CanEditEvent()
		{
			return true;
		}
		private void OnEditEvent()
		{
			const string messageBoxTitle = "Editar Evento.";
			var isNotValidEvent = GetEventById(CustomEventId) == null;
			if (isNotValidEvent)
			{
				MessageBox.Show(Constants.DeletedEvent, messageBoxTitle, MessageBoxButton.OK);
				return;
			}

			if (!CurrentUserIsOwner())
			{
				MessageBox.Show(Constants.NotOwnerEditEvent, messageBoxTitle, MessageBoxButton.OK);
				return;
			}
			if (AreValidData(CustomEvent.Title, CustomEvent.StartDateAndTime, CustomEvent.FinishDateAndTime))
			{
				db.Events.Update(CustomEvent);
				db.SaveChanges();
				MessageBox.Show(Constants.SuccessfulEditEvent, messageBoxTitle, MessageBoxButton.OK);
				return;
			}
			MessageBox.Show(Constants.FailedEditEvent, messageBoxTitle, MessageBoxButton.OK);
			return;
		}

		private bool CanDeleteEvent()
		{
			return true;
		}
		private void OnDeleteEvent()
		{
			const string messageBoxTitle = "Eliminar Evento.";
			if (CustomEvent == null)
			{
				MessageBox.Show(Constants.DeletedEvent, messageBoxTitle, MessageBoxButton.OK);
				return;
			}

			if (EventCanBeDeleted())
			{
				var eventToDelete = db.Events.First(e => e.Id == CustomEventId);
				db.Events.Remove(eventToDelete);
				CurrentUserEvents.Remove(eventToDelete);
				CurrentUserEventsCollection.Remove(eventToDelete);
				db.SaveChanges();
				MessageBox.Show(Constants.SuccessfulEditEvent, messageBoxTitle, MessageBoxButton.OK);
				return;
			}

			MessageBox.Show(Constants.NotOwnerDeleteEvent, messageBoxTitle, MessageBoxButton.OK);
		}
		private bool EventCanBeDeleted()
		{
			var eventToDelete = db.Events.FirstOrDefault(e => e.Id == CustomEventId);
			return CurrentUserIsOwner() && EventsListContainsCustomEvent() && eventToDelete != null;
		}
		private bool CurrentUserIsOwner()
		{
			return CustomEvent.Owner.UserName == CurrentUser.UserName;
		}
		private bool EventsListContainsCustomEvent()
		{
			foreach (var eventModel in CurrentUserEvents)
			{
				if (eventModel.Id == CustomEventId)
				{
					return true;
				}
			}

			return false;
		}

		private List<UserModel> GetAllUsers()
		{
			var users = new List<UserModel>();
			foreach (var user in db.Users.ToList())
			{
				if (user.UserName != CurrentUser.UserName)
				{
					users.Add(user);
				}
			}
			return users;
		}
		private List<EventModel> GetUserEvents(UserModel user)
		{
			var getUserEvents = new List<EventModel>();
			var userEventsQuantity = user.UserEvents.Count;
			for (var eventIndex = Constants.FirstElement; eventIndex < userEventsQuantity; eventIndex++)
			{
				getUserEvents.Add(CurrentUser.UserEvents[eventIndex].Event);
			}
			return getUserEvents;
		}
		private bool AreValidData(string titleOfEvent, DateTime startDateAndTimeOfEvent, DateTime finishDateAndTimeOfEvent)
		{
			return startDateAndTimeOfEvent < finishDateAndTimeOfEvent && titleOfEvent != null;
		}
		private EventModel GetEventById(int id)
		{
			var eventModel = db.Events.FirstOrDefault(e => e.Id == id);
			return eventModel;
		}
		#endregion

	}
}
