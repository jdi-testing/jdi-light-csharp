using System;
using System.Globalization;
using JDI.Light.Elements.Common;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DateTimeTests : TestBase
    {
        private readonly DateTime _dateTime = new DateTime(2019, 4, 1, 15, 0, 0);

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void SetTimeTest()
        {
            TestSite.Html5Page.BookingTime.Format = "H:mm";
            TestSite.Html5Page.BookingTime.SetDateTime(_dateTime);
            var setValue = TestSite.Html5Page.BookingTime.GetValue();
            Assert.AreEqual(setValue, "15:00");
        }

        [Test]
        public void SetMonthTest()
        {
            TestSite.Html5Page.MonthOfHolidays.Format = "yyyy-MM";
            TestSite.Html5Page.MonthOfHolidays.SetDateTime(_dateTime);
            var setValue = TestSite.Html5Page.MonthOfHolidays.GetValue();
            Assert.AreEqual(setValue, "2019-04");
        }

        [Test]
        public void SetBirthDateTest()
        {
            TestSite.Html5Page.BirthDate.Format = "yyyy-MM-dd";
            TestSite.Html5Page.BirthDate.SetDateTime(_dateTime);
            var setValue = TestSite.Html5Page.BirthDate.GetValue();
            Assert.AreEqual(setValue, "2019-04-01");
        }

        [Test]
        public void SetPartyTimeTest()
        {
            TestSite.Html5Page.PartyTime.Format = "yyyy-MM-ddTHH:mm";
            TestSite.Html5Page.PartyTime.SetDateTime(_dateTime);
            var setValue = TestSite.Html5Page.PartyTime.GetDateTime();
            Assert.AreEqual(setValue, _dateTime);
        }

        [Test]
        public void AutumnDateTimeTest()
        {
            var calendar = new GregorianCalendar();
            var weekNum = calendar.GetWeekOfYear(_dateTime, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            TestSite.Html5Page.AutumnDateTime.Format = "yyyy-" + $"W{weekNum}";

            TestSite.Html5Page.AutumnDateTime.SetDateTime(_dateTime);
            var setValue = TestSite.Html5Page.AutumnDateTime.GetValue();

            Assert.AreEqual(setValue, "2019-W13");
        }
    }
}