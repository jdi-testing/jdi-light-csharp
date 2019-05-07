using JDI.Light.Exceptions;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class RangeTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.Volume.SetValue(90);
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual(TestSite.Html5Page.DisabledRange.Value(), 50);
        }

        [Test]
        public void MinTest()
        {
            Assert.AreEqual(TestSite.Html5Page.Volume.Min(), 10);
        }

        [Test]
        public void MaxTest()
        {
            Assert.AreEqual(TestSite.Html5Page.Volume.Max(), 100);
        }

        [Test]
        public void StepTest()
        {
            Assert.AreEqual(TestSite.Html5Page.Volume.Step(), 5);
        }

        [Test]
        public void SetRangeTest()
        {
            TestSite.Html5Page.Volume.SetValue(10);
            Assert.AreEqual(TestSite.Html5Page.Volume.Value(), 10);
        }

        [Test]
        public void RangeTest()
        {
            TestSite.Html5Page.Volume.SetValue("30");
            Assert.AreEqual(TestSite.Html5Page.Volume.GetValue(), "30");
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.Volume.Is().Enabled();
            TestSite.Html5Page.Volume.AssertThat().MinValue(Is.EqualTo(10))
                .MaxValue(Is.EqualTo(100))
                .Step((Is.EqualTo(5)));
            TestSite.Html5Page.Volume.Is().Value(Is.GreaterThanOrEqualTo(10))
                .Value(Is.LessThanOrEqualTo(100))
                .Value(Is.EqualTo(90));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.Volume.AssertThat().Value(Is.GreaterThan(0))
                .Value(Is.LessThan(200));
            TestSite.Html5Page.DisabledRange.AssertThat().Value(Is.EqualTo(50));
        }

        [Test]
        public void CheckEnabledTest()
        {
            Assert.Throws<ElementDisabledException>(() => TestSite.Html5Page.DisabledRange.SetValue("30", true));
            Assert.AreEqual(TestSite.Html5Page.DisabledRange.GetValue(), "50");
            Assert.DoesNotThrow(() => TestSite.Html5Page.DisabledRange.SetValue("30", false));
        }
    }
}