using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CalendarApp.Model;


namespace Tests.Model
{

    class CalendarMonthModelTests
    {
        private CalendarMonthModel calendarMonthModel;

        [SetUp]
        public void Setup()
        {
            calendarMonthModel = new CalendarMonthModel(11, 2019);
        }

        [Test]
        public void TestSettingMonthNumberShouldSetDaysOfMonthCorrectly()
        {
            var weekendDays = new List<int> { 2, 3, 9, 10, 16, 17, 23, 24, 30 };
            calendarMonthModel.MonthNumber = 11;
            List<CalendarDayModel> daysOfMonth = calendarMonthModel.DaysOfMonth;
            var daysInNovember = new List<CalendarDayModel>();
            for (int dayNumber = 1; dayNumber < 31; dayNumber++)
            {
                string dayColor = Constants.ColorOfWeek;
                if (weekendDays.Contains(dayNumber))
                {
                    dayColor = Constants.ColorOfWeekend;
                }

                daysInNovember.Add(new CalendarDayModel(new DateTime(2019, 11, dayNumber), dayColor));
            }

            Assert.That(daysOfMonth, Is.EquivalentTo(daysInNovember));
        }

        [Test]
        public void TestShouldReturnEqualCalendarMonthModel()
		{
            Assert.AreEqual(new CalendarMonthModel(11, 2019), calendarMonthModel);
		}

    }
}