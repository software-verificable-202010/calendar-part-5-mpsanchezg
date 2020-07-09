using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CalendarApp.ViewModel;
using CalendarApp.Model;

namespace Tests.ViewModel
{

    class CalendarMonthViewModelTests
    {
        private CalendarMonthViewModel calendarMonthViewModel;

        [SetUp]
        public void Setup()
        {
            calendarMonthViewModel = new CalendarMonthViewModel();
        }

        [Test]
        public void TestSettingCurrentMonthShouldSetMonthNumberStartingWithZeroAsFirstMonth()
        {
            calendarMonthViewModel.CurrentMonth = 0;
            Assert.AreEqual(12, calendarMonthViewModel.CurrentMonth);
        }

        [Test]
        public void TestCalendarMonthViewModelShouldReturnCurrentDayAtFirst()
		{
            Assert.IsTrue(calendarMonthViewModel.CurrentDate.Date == DateTime.Today);
		}

        [Test]
        public void TestCalendarMonthViewModelShouldReturnCurrentMonth()
        {
            Assert.IsTrue(calendarMonthViewModel.CurrentMonth == DateTime.Today.Month);
        }

        [Test]
        public void TestCalendarMonthViewModelShouldReturnCurrentYear()
        {
            Assert.IsTrue(calendarMonthViewModel.CurrentYear == DateTime.Today.Year);
        }


        [Test]
        public void TestCalendarMonthViewModelShouldReturnSameCalendarMonthdModel()
		{
            var calendarMonthModel = new CalendarMonthModel(DateTime.Today.Month, DateTime.Today.Year);
            Assert.AreEqual(calendarMonthModel, calendarMonthViewModel.CurrentCalendarMonth);
		}
    }
}