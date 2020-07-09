using CalendarApp.Model;
using NUnit.Framework;

namespace Tests.Model
{
    public class CalendarModelTests
    {
        private CalendarModel calendarModel;

        [SetUp]
        public void Setup()
        {
            calendarModel = new CalendarModel(2020, 12);
        }

        [Test]
        public void TestShouldInstanciateCalendarMonthModelWithGivenMonthNumber()
        {
            Assert.IsTrue(calendarModel.CalendarMonth.MonthNumber == 12);
        }

        [Test]
        public void TestShouldInstanciateCalendarMonthModelWithGivenYearNumber()
        {
            Assert.IsTrue(calendarModel.CalendarMonth.YearOfMonth == 2020);
        }
    }
}