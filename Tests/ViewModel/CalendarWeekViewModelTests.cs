using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CalendarApp.ViewModel;
using CalendarApp.Model;

namespace Tests.ViewModel
{
	class CalendarWeekViewModelTests
	{
		private CalendarWeekViewModel calendarWeek;
		[SetUp]
		public void Setup()
		{
			calendarWeek = new CalendarWeekViewModel();
		}

		[Test]
		public void TestShouldReturnCurrentDate()
		{
			Assert.AreEqual(DateTime.Today, calendarWeek.CurrentDay.Date);
		}
	}
}
