using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Model
{
	public class UserEventModel
	{
		#region Private Variables

		private int id;
		private UserModel userModel;
		private EventModel eventModel;

		#endregion
		public UserEventModel()
		{

		}

		#region Properties
		public UserEventModel(UserModel userModel, EventModel eventModel)
		{
			User = userModel;
			Event = eventModel;
		}
		public int Id
		{
			get => id;
			set => id = value;
		}
		public UserModel User
		{
			get => userModel;
			set => userModel = value;
		}
		public EventModel Event
		{
			get => eventModel;
			set => eventModel = value;
		}

		#endregion
	}
}
