using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DateTimeTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void SetTimeTest()
        {
            TestSite.Html5Page.BookingTime.SetDateTime("15:00");
            var setValue = TestSite.Html5Page.BookingTime.GetValue();
            Assert.AreEqual(setValue, "15:00");
        }

        [Test]
        public void SetMonthTest()
        {
            TestSite.Html5Page.MonthOfHolidays.SetDateTime("2019-01");
            var setValue = TestSite.Html5Page.MonthOfHolidays.GetValue();
            Assert.AreEqual(setValue, "2019-01");
        }

        [Test]
        public void SetBirthDateTest()
        {
            TestSite.Html5Page.BirthDate.SetDateTime("2000-01-01");
            var setValue = TestSite.Html5Page.BirthDate.GetValue();
            Assert.AreEqual(setValue, "2000-01-01");
        }

        [Test]
        public void SetPartyTimeTest()
        {
            TestSite.Html5Page.PartyTime.SetDateTime("2000-01-01T12:00");
            var setValue = TestSite.Html5Page.PartyTime.GetValue();
            Assert.AreEqual(setValue, "2000-01-01T12:00");
        }

        [Test]
        public void AutumnDateTimeTest()
        {
            TestSite.Html5Page.AutumnDateTime.SetDateTime("2019-W11");
            var setValue = TestSite.Html5Page.AutumnDateTime.GetValue();
            Assert.AreEqual(setValue, "2019-W11");
        }
    }
}