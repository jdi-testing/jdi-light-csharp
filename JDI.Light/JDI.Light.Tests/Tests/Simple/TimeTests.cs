using System;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class TimeTests : TestBase
    {
        private readonly DateTime _dateTime = new DateTime(2019, 4, 1, 15, 0, 0);

        [SetUp]
        public void Setup()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.MonthDate.SetDateTime("2018-05");
        }

        [Test]
        public void GetDateTest()
        {
            Assert.AreEqual(TestSite.Html5Page.BookingTime.Value(), "11:00");
        }

        [Test]
        public void MinTest()
        {
            Assert.AreEqual(TestSite.Html5Page.BookingTime.Min(), "9:00");
        }

        [Test]
        public void MaxTest()
        {
            Assert.AreEqual(TestSite.Html5Page.BookingTime.Max(), "18:00");
        }

        [Test]
        public void SetDateTimeTest()
        {
            Assert.DoesNotThrow(() => TestSite.Html5Page.BookingTime.SetDateTime("05:00", true));
            Assert.AreEqual(TestSite.Html5Page.BookingTime.Value(), "05:00");
        }

        [Test]
        public void SetMonthTest()
        {
            TestSite.Html5Page.BookingTime.Format = "H:mm";
            TestSite.Html5Page.BookingTime.SetDateTime(_dateTime);
            Assert.AreEqual(TestSite.Html5Page.BookingTime.Value(), "15:00");
        }
    }
}