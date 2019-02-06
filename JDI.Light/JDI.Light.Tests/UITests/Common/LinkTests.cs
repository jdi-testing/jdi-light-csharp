using JDI.Light.Interfaces.Common;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class LinkTests : TestBase
    {
        private ILink AboutLink => TestSite.Footer.About;

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
            AboutLink.Click();
            Assert.IsTrue(TestSite.SupportPage.IsOpened);
        }

        [Test]
        public void GetReferenceTest()
        {
            Jdi.Assert.AreEquals(AboutLink.GetReference(), TestSite.SupportPage.Url);
        }
    }
}