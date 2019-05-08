using System;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class DateTests : TestBase
    {
        private readonly DateTime _dateTime = new DateTime(2019, 4, 1, 15, 0, 0);

        [SetUp]
        public void Setup()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void GetDateTest()
        {
            Assert.AreEqual(TestSite.Html5Page.BirthDate.Value(), "1985-06-18");
        }

        [Test]
        public void MinTest()
        {
            Assert.AreEqual(TestSite.Html5Page.BirthDate.Min(), "1970-01-01");
        }

        [Test]
        public void MaxTest()
        {
            Assert.AreEqual(TestSite.Html5Page.BirthDate.Max(), "2030-12-31");
        }

        [Test]
        public void SetDateTimeTest()
        {
            Assert.DoesNotThrow(() => TestSite.Html5Page.BirthDate.SetDateTime("2018-11-13", true));
            Assert.AreEqual(TestSite.Html5Page.BirthDate.Value(), "2018-11-13");
        }

        [Test]
        public void SetMonthTest()
        {
            TestSite.Html5Page.BirthDate.Format = "yyyy-MM-dd";
            TestSite.Html5Page.BirthDate.SetDateTime(_dateTime);

            Assert.AreEqual(TestSite.Html5Page.BirthDate.Value(), "2019-04-01");
        }
    }
}