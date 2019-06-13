using System;
using JDI.Light.Elements.Base;
using NUnit.Framework;
using JDI.Light.Matchers.StringMatchers;
using OpenQA.Selenium;
using static JDI.Light.Elements.Base.BaseValidation;
using static NUnit.Framework.Assert;
using Is = JDI.Light.Matchers.Is;

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
            AreEqual(TestSite.Html5Page.RedButton.GetText(), "Big Red Button-Input");
        }

        [Test]
        public void GetValueTest()
        {
            AreEqual(TestSite.Html5Page.RedButton.GetValue(), "Big Red Button-Input");
        }

        [Test]
        public void ClickTest()
        {
            TestSite.Html5Page.RedButton.Click();
            AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "Red button");
            TestSite.Html5Page.GetAlert().AcceptAlert();

            TestSite.Html5Page.BlueButton.Click();
            AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "Blue button");
            TestSite.Html5Page.GetAlert().AcceptAlert();

            TestSite.Html5Page.DisabledButton.Click();
            Throws<NoAlertPresentException>(() => TestSite.Html5Page.GetAlert().AcceptAlert());
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.RedButton.Is.Displayed()
                .Enabled()
                .Text(Is.EqualTo("Big Red Button-Input"))
                .Text(ContainsStringMatcher.ContainsString("Red Button"))
                .CssClass(Is.EqualTo("uui-button red"))
                .Attr("type", Is.EqualTo("button"))
                .Tag(Is.EqualTo("input"));
            TestSite.Html5Page.BlueButton.Is.Text(ContainsStringMatcher.ContainsString("Blue Button".ToUpper()));
            TestSite.Html5Page.DisabledButton.Is.Text(ContainsStringMatcher.ContainsString("Disabled Button".ToUpper()))
                .Disabled();
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.RedButton.AssertThat.Text(Is.EqualTo("Big Red Button-Input"));
        }

        [Test]
        [Ignore("This test is ignored because of fail on duration condition")]
        public void SuspendButtonTest()
        {
            TestSite.Html5Page.Refresh();
            DurationMoreThan(3, () => TestSite.Html5Page.SuspendButton.Click());
            AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "Suspend button");
            TestSite.Html5Page.GetAlert().AcceptAlert();
        }

        [Test]
        [Ignore("This test is ignored because of fail on duration condition")]
        public void IsNotAppearFailedButtonTest()
        {
            TestSite.Html5Page.Refresh();
            try
            {
                DurationImmediately(() => TestSite.Html5Page.GhostButton.Is.Disappear());
            }
            catch (Exception e)
            {
                IsTrue(e.Message.Contains("element not disappeared failed"));
            }
        }
    }
}