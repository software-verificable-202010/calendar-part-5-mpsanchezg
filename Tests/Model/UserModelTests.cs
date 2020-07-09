using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CalendarApp.Model;

namespace Tests.Model
{
	class UserModelTests
	{
		[SetUp]
		public void Setup()
		{

		}
		[Test]
		public void TestShouldReturnAnUserWithUsername()
		{
			var user = new UserModel("test");
			Assert.IsTrue(user.UserName == "test");
		}

		[Test]
		public void TestShouldReturnAnUserWithEmptyListOfEventsInitially()
		{
			var user = new UserModel("test");
			Assert.IsTrue(user.UserEvents.Count == 0);
		}
	}
}
