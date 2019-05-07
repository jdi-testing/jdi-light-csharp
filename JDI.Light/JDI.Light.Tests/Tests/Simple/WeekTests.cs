using System;
using System.Globalization;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class WeekTests : TestBase
    {
        private readonly DateTime _dateTime = new DateTime(2019, 4, 1, 15, 0, 0);

        [SetUp]
        public void Setup()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.AutumnWeek.SetDateTime("2018-W40");
        }

        [Test]
        public void GetDateTest()
        {
            Assert.AreEqual(TestSite.Html5Page.AutumnWeek.Value(), "2018-W40");
        }

        [Test]
        public void MinTest()
        {
            Assert.AreEqual(TestSite.Html5Page.AutumnWeek.Min(), "2018-W35");
        }

        [Test]
        public void MaxTest()
        {
            Assert.AreEqual(TestSite.Html5Page.AutumnWeek.Max(), "2018-W48");
        }

        [Test]
        public void SetDateTimeTest()
        {
            Assert.DoesNotThrow(() => TestSite.Html5Page.AutumnWeek.SetDateTime("2018-W12", true));
            Assert.AreEqual(TestSite.Html5Page.AutumnWeek.Value(), "2018-W12");
        }

        [Test]
        public void AutumnDateTimeTest()
        {
            var calendar = new GregorianCalendar();
            var weekNum = calendar.GetWeekOfYear(_dateTime, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            TestSite.Html5Page.AutumnWeek.Format = "yyyy-" + $"W{weekNum}";
            TestSite.Html5Page.AutumnWeek.SetDateTime(_dateTime);
            var setValue = TestSite.Html5Page.AutumnWeek.Value();
            Assert.AreEqual(setValue, "2019-W13");
        }
    }
}