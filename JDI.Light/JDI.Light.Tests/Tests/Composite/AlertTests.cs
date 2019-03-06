using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class AlertTests : TestBase
    {
        [Test]
        public void AlertActionsTest()
        {
            Jdi.Logger.Info("Navigating to HTML 5 page.");
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();

            TestSite.Html5Page.ClickJdiTitle();
            Assert.AreEqual(TestSite.Html5Page.GetAlertText(), "JDI Title");
            TestSite.Html5Page.OkAlertAction();

            TestSite.Html5Page.ClickBlueButton();
            Assert.AreEqual(TestSite.Html5Page.GetAlertText(), "Blue button");
            TestSite.Html5Page.CancelAlertAction();
        }
    }
}