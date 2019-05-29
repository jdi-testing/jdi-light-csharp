using System;
using NUnit.Framework;
using static JDI.Light.Matchers.LongMatchers.IsMatcher;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Simple
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
        public void GetDateTest()
        {            
            TestSite.Html5Page.PartyTime.AssertThat().SelectedTime(Is.EqualToIgnoringCase("2018-06-12T19:30"));
        }

        [Test]
        public void MinTest()
        {            
            TestSite.Html5Page.PartyTime.AssertThat().HasMinTime(Is.EqualToIgnoringCase("2018-05-07T00:00"));
        }

        [Test]
        public void MaxTest()
        {            
            TestSite.Html5Page.PartyTime.AssertThat().HasMaxTime(Is.EqualToIgnoringCase("2018-06-14T00:00"));
        }

        [Test]
        public void SetDateTimeTest()
        {
            Assert.DoesNotThrow(() => TestSite.Html5Page.PartyTime.SetDateTime("2017-05-10T00:00", true));            
            TestSite.Html5Page.PartyTime.AssertThat().SelectedTime(Is.EqualToIgnoringCase("2017-05-10T00:00"));
        }

        [Test]
        public void SetPartyTimeTest()
        {
            TestSite.Html5Page.PartyTime.Format = "yyyy-MM-ddTHH:mm";
            TestSite.Html5Page.PartyTime.SetDateTime(_dateTime);
            TestSite.Html5Page.PartyTime.Is().SelectedTime(Is(_dateTime.ToBinary()));            
        }
    }
}