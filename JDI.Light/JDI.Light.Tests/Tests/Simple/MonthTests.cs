using System;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class MonthTests : TestBase
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
            Assert.AreEqual(TestSite.Html5Page.MonthDate.Value(), "2018-05");
        }

        [Test]
        public void MinTest()
        {
            Assert.AreEqual(TestSite.Html5Page.MonthDate.Min(), "2015-03");
        }

        [Test]
        public void MaxTest()
        {
            Assert.AreEqual(TestSite.Html5Page.MonthDate.Max(), "2020-12");
        }

        [Test]
        public void SetDateTimeTest()
        {
            Assert.DoesNotThrow(() => TestSite.Html5Page.MonthDate.SetDateTime("2018-10", true));
            Assert.AreEqual(TestSite.Html5Page.MonthDate.Value(), "2018-10");
        }

        [Test]
        public void SetMonthTest()
        {
            TestSite.Html5Page.MonthDate.Format = "yyyy-MM";
            TestSite.Html5Page.MonthDate.SetDateTime(_dateTime);

            Assert.AreEqual(TestSite.Html5Page.MonthDate.Value(), "2019-04");
        }
    }
}