using System;
using System.Collections.Generic;
using System.Text;
using CalendarApp.Model;
using NUnit.Framework;

namespace Tests.Model
{
	class CalendarDayModelTests
	{
		private CalendarDayModel calendarDay;

		[SetUp]
		public void Setup()
		{
			calendarDay = new CalendarDayModel(new DateTime(1996, 11, 11), "#FFFFFFFF");
		}

		[Test]
		public void TestShouldReturnTrueIfDateWasSavedInCalendarDay()
		{
			Assert.IsTrue(calendarDay.Date.Year == 1996 && calendarDay.Date.Month == 11 && calendarDay.Date.Day == 11);
		}
		
		[Test]
		public void TestShouldReturnTrueIfColorWasSavedInCalendarDay()
		{
			Assert.IsTrue(calendarDay.Color == "#FFFFFFFF");
		}

		[Test]
		public void TestSouldReturnAnEqualCalendarDay()
		{
			Assert.AreEqual(new CalendarDayModel(new DateTime(1996, 11, 11, 14, 0, 0), "#FFFFFFFF"), calendarDay);
		}
	}
}
