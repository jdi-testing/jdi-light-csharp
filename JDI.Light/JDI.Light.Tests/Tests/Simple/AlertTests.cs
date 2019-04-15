using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class AlertTests : TestBase
    {
        [Test]
        public void AcceptAlert()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
            TestSite.Html5Page.JdiLabel.Click();
            TestSite.Html5Page.GetAlert().Ok();
        }

        [Test]
        public void CancelAlert()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
            TestSite.Html5Page.BlueButton.Click();
            TestSite.Html5Page.GetAlert().Cancel();
        }

        [Test]
        public void GetAlertText()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
            TestSite.Html5Page.JdiLabel.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetText(), "JDI Title");
            TestSite.Html5Page.GetAlert().Ok();
        }
    }
}