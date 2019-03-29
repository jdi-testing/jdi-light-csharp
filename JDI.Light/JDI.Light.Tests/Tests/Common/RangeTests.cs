using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
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
            TestSite.Html5Page.VolumeRange.SetRange("50");
            var setValue = TestSite.Html5Page.VolumeRange.GetValue();
            Assert.AreEqual(setValue, "50");
        }
    }
}