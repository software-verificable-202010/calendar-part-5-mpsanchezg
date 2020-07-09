using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Model
{
	public class CalendarModelContext : DbContext
	{
		#region Private Fields

		private const string connectionString = "Data Source=calendar.db";

		#endregion

		#region Properties

		public DbSet<EventModel> Events
		{
			get; 
			set;
		}
		public DbSet<UserModel> Users
		{
			get;
			set;
		}
		public DbSet<UserEventModel> UserEvent
		{
			get;
			set;
		}

		#endregion

		#region Protected Methods
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite(connectionString);

		#endregion

	}
}
