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
        }

        [Test]
        public void SetRangeTest()
        {
            TestSite.Html5Page.VolumeRange.SetRange(50);
            var setValue = TestSite.Html5Page.VolumeRange.GetRange();

            Assert.AreEqual(setValue, 50);
        }

        [Test]
        public void RangeTest()
        {
            var min = TestSite.Html5Page.VolumeRange.Min();
            var max = TestSite.Html5Page.VolumeRange.Max();

            Assert.AreEqual(min, 10);
            Assert.AreEqual(max, 100);
        }
    }
}