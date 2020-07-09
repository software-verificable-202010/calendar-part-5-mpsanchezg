using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Model
{
	public class UserModel
	{
		#region Private Variables

		private int id;
		private string userName;
		private List<UserEventModel> userEvents;
		
		#endregion

		public UserModel()
		{

		}
		#region Properties

		public UserModel(string userName)
		{
			UserName = userName;
			UserEvents = new List<UserEventModel>();
		}

		public int Id
		{
			get => id;
			set => id = value;
		}

		public string UserName
		{
			get => userName;
			set => userName = value;
		}

		public List<UserEventModel> UserEvents
		{
			get => userEvents;
			set => userEvents = value;
		}

		#endregion
	}
}
