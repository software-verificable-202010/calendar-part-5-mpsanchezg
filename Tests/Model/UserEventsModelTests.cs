using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CalendarApp.Model;

namespace Tests.Model
{
	class UserEventsModelTests
	{
		private UserModel user;
		private EventModel _event;
		[SetUp]
		public void Setup()
		{
			user = new UserModel("test");
			_event = new EventModel(
				user, 
				"title", 
				new DateTime(1996, 11, 11), 
				new DateTime(1996, 11, 12), 
				"description"
			);
		}

		[Test]
		public void TestUserShouldExistInUserModel()
		{
			var userEvent = new UserEventModel(user, _event);
			Assert.IsTrue(userEvent.User != null);
		}

		[Test]
		public void TestEventShouldExistInUserModel()
		{
			var userEvent = new UserEventModel(user, _event);
			Assert.IsTrue(userEvent.Event != null);
		}
	}
}
