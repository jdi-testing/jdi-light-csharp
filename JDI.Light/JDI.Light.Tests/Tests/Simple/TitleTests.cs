using JDI.Light.Matchers.StringMatchers;
using NUnit.Framework;
using static JDI.Light.Elements.Base.BaseValidation;
using static JDI.Light.Matchers.Is;
using static JDI.Light.Matchers.StringMatchers.IsMatcher;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class TitleTests : TestBase
    {
        private readonly string _text = "JDI TESTING PLATFORM";

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void GetTextTest()
        {
            Assert.AreEqual(TestSite.Html5Page.JdiTitle.GetText(), _text);
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual(TestSite.Html5Page.JdiTitle.GetValue(), _text);
        }

        [Test]
        public void ClickTest()
        {
            TestSite.Html5Page.JdiTitle.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "JDI Title");
            TestSite.Html5Page.GetAlert().AcceptAlert();
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.JdiTitle.Is.Enabled();
            TestSite.Html5Page.JdiTitle.Is.Text(EqualTo(_text));
            TestSite.Html5Page.JdiTitle.Is.Text(Is(_text));
            TestSite.Html5Page.JdiTitle.Is.Text(EqualToIgnoringCaseMatcher.EqualTo("jdi TESTING platform"));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.JdiTitle.AssertThat.Text(EqualTo(_text));
        }

        [Test]
        public void BaseValidationTest()
        {
            BaseElementValidation(TestSite.Html5Page.JdiTitle);
        }
    }
}