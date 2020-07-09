using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using CalendarApp.Model;
using CalendarApp.ViewModel;
using NUnit.Framework;

namespace Tests
{
	public class CalendarWeekModelTests
	{
		private CalendarWeekModel calendarWeekModel;
		private DateTime currentDate;
		private DateTime mondayDate;
		[SetUp]
		public void Setup()
		{
			currentDate = DateTime.Today;
			mondayDate = new DateTime(1996, 11,11);
			calendarWeekModel = new CalendarWeekModel(currentDate);
		}

		[Test]
		public void TestShouldReturnASpecificColorIfDayIsTodayColor()
		{
			string colorOfDay = "";
			for(int indexOfDay = Constants.FirstElement; indexOfDay < 7; indexOfDay++)
			{
				if(calendarWeekModel.DaysOfWeek[indexOfDay].Date == DateTime.Today)
				{
					colorOfDay = calendarWeekModel.DaysOfWeek[indexOfDay].Color;
					break;
				}
			}

			Assert.AreEqual(Constants.ColorOfToday, colorOfDay);
		}

		[Test]
		public void TestShouldReturnASpecificColorIfDayIsWeek()
		{
			var calendarWeek = new CalendarWeekModel(mondayDate);

			Assert.IsTrue(calendarWeek.DaysOfWeek[0].Color == Constants.ColorOfWeek);
		}

		[Test]
		public void TestShouldReturnASpecificColorIfDayIsWeekend()
		{
			var calendarWeek = new CalendarWeekModel(mondayDate);
			
			Assert.IsTrue(calendarWeek.DaysOfWeek[6].Color == Constants.ColorOfWeekend);
		}


		[Test]
		public void TestShouldReturnListOfCalendarDaysOfWeekBySelectedDay()
		{ 
			var expected = new List<CalendarDayModel>()
			{
				new CalendarDayModel(mondayDate, Constants.ColorOfWeek),
				new CalendarDayModel(mondayDate.AddDays(1), Constants.ColorOfWeek),
				new CalendarDayModel(mondayDate.AddDays(2), Constants.ColorOfWeek),
				new CalendarDayModel(mondayDate.AddDays(3), Constants.ColorOfWeek),
				new CalendarDayModel(mondayDate.AddDays(4), Constants.ColorOfWeek),
				new CalendarDayModel(mondayDate.AddDays(5), Constants.ColorOfWeekend),
				new CalendarDayModel(mondayDate.AddDays(6), Constants.ColorOfWeekend),
			};
			var result = new CalendarWeekModel(mondayDate).DaysOfWeek;
			CollectionAssert.AreEqual(expected, result);
		}
	}
}