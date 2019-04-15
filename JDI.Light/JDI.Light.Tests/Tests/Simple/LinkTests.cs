using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
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
            TestSite.Footer.AboutLink.Click();
            Assert.IsTrue(TestSite.SupportPage.IsOpened);
        }

        [Test]
        public void GetReferenceTest()
        {
            var reference = TestSite.Footer.AboutLink.GetReference();
            Assert.AreEqual(TestSite.SupportPage.Url, reference);
        }

        [Test]
        public void GetReferenceTooltipTest()
        {
            var tooltip = TestSite.Footer.AboutLink.GetTooltip();
            Assert.AreEqual("Tip title", tooltip);
        }

        [Test]
        public void WaitReferenceContainsTest()
        {
            var reference = TestSite.Footer.AboutLink.WaitReferenceContains("support");
            Assert.AreEqual(reference, TestSite.SupportPage.Url);
        }

        [Test]
        public void WaitReferenceMatchesTest()
        {
            var reference = TestSite.Footer.AboutLink.WaitReferenceMatches(".*");
            Assert.AreEqual(reference, TestSite.SupportPage.Url);
        }
    }
}