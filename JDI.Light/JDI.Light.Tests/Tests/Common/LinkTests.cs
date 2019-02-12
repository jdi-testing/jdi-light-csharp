using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class LinkTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClickTest()
        {
            TestSite.Footer.About.Click();
            Assert.IsTrue(TestSite.SupportPage.IsOpened);
        }

        [Test]
        public void GetReferenceTest()
        {
            var reference = TestSite.Footer.About.GetReference();
            Assert.AreEqual(reference, TestSite.SupportPage.Url);
        }
    }
}