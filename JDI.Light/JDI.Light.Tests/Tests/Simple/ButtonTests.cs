using NUnit.Framework;
using System;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class ButtonTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void GetTextTest()
        {
            Assert.AreEqual(TestSite.Html5Page.RedButton.GetText(), "Big Red Button-Input");
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual(TestSite.Html5Page.RedButton.GetValue(), "Big Red Button-Input");
        }

        [Test]
        public void ClickTest()
        {
            TestSite.Html5Page.RedButton.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "Red button");
            TestSite.Html5Page.GetAlert().AcceptAlert();


            TestSite.Html5Page.BlueButton.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "Blue button");
            TestSite.Html5Page.GetAlert().AcceptAlert();

            TestSite.Html5Page.DisabledButton.Click();
            Assert.Throws<NoAlertPresentException>(() => TestSite.Html5Page.GetAlert().AcceptAlert());
        }

        [Test]
        public void SuspendButtonTest()
        {
            TestSite.Html5Page.SuspendButton.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "Suspend button");
            TestSite.Html5Page.GetAlert().AcceptAlert();
        }
}
}