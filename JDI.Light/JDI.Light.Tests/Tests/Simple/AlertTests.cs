using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class AlertTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void AcceptAlertTest()
        {
            TestSite.Html5Page.RedButton.Click();
            TestSite.Html5Page.GetAlert().AcceptAlert();
            TestSite.Html5Page.BlueButton.Click();
            TestSite.Html5Page.GetAlert().AcceptAlert();
            TestSite.Html5Page.GhostButton.Click();
            TestSite.Html5Page.GetAlert().AcceptAlert();
        }

        [Test]
        public void DismissAlertTest()
        {
            TestSite.Html5Page.RedButton.Click();
            TestSite.Html5Page.GetAlert().DismissAlert();
            TestSite.Html5Page.BlueButton.Click();
            TestSite.Html5Page.GetAlert().DismissAlert();
            TestSite.Html5Page.GhostButton.Click();
            TestSite.Html5Page.GetAlert().DismissAlert();
        }

        [Test]
        public void GetAlertTextTest()
        {
            TestSite.Html5Page.RedButton.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "Red button");
            TestSite.Html5Page.GetAlert().AcceptAlert();

            TestSite.Html5Page.BlueButton.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "Blue button");
            TestSite.Html5Page.GetAlert().AcceptAlert();

            TestSite.Html5Page.GhostButton.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "Ghost button");
            TestSite.Html5Page.GetAlert().AcceptAlert();
        }

        [Test]
        public void InputAndAcceptAlertTest()
        {
            TestSite.Html5Page.GhostButton.Click();
            TestSite.Html5Page.GetAlert().InputAndAcceptAlert("Some text");
        }
    }
}