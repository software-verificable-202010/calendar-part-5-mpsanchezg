using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CalendarApp.Model;


namespace Tests.Model
{
	class EventModelTests
	{
		private UserModel owner;
		[SetUp]
		public void Setup()
		{
			owner = new UserModel("test");
		}

		[Test]
		public void TestEventShouldHaveOwner()
		{
			EventModel eventModel = new EventModel(
				owner,
				"title",
				new DateTime(1996, 11, 11),
				new DateTime(1996, 11, 12),
				"description"
			);

			Assert.IsTrue(eventModel.Owner != null);
		}

		[Test]
		public void TestEventShouldHaveTitle()
		{
			EventModel eventModel = new EventModel(
				owner,
				"title",
				new DateTime(1996, 11, 11),
				new DateTime(1996, 11, 12),
				"description"
			);

			Assert.IsTrue(eventModel.Title != null);
		}
		
		[Test]
		public void TestEventShouldHaveDatesAndTimes()
		{
			EventModel eventModel = new EventModel(
				owner,
				"title",
				new DateTime(1996, 11, 11),
				new DateTime(1996, 11, 12),
				"description"
			);

			Assert.IsTrue( eventModel.StartDateAndTime != null && eventModel.FinishDateAndTime != null);
		}
	}
}
