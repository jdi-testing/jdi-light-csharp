using NUnit.Framework;

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
            TestSite.Html5Page.Volume.SetVolume(90);
        }
        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual(TestSite.Html5Page.DisabledRange.Volume(), 50);
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
            TestSite.Html5Page.Volume.SetVolume(10);
            Assert.AreEqual(TestSite.Html5Page.Volume.Volume(), 10);
        }

        [Test]
        public void RangeTest()
        {
            TestSite.Html5Page.Volume.SetVolume("30");
            Assert.AreEqual(TestSite.Html5Page.Volume.GetValue(), "30");
        }
    }
}