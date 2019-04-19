using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class ProgressTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual(TestSite.Html5Page.Progress.Value(), "70");
        }

        [Test]
        public void MaxTest()
        {
            Assert.AreEqual(TestSite.Html5Page.Progress.Max(), "100");
        }
    }
}