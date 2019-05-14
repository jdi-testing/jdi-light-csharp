using JDI.Light.Interfaces.Common;
using JDI.Light.Matchers.StringMatchers;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class ImagesTests : TestBase
    {
        private IImage JdiLogo => TestSite.Html5Page.JdiLogo;

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
        }

        private const string Text = "https://epam.github.io/JDI/images/jdi-logo.jpg";

        [Test]
        public void GetSrcTest()
        {
            Jdi.Assert.AreEquals(JdiLogo.Src, Text);
        }

        [Test]
        public void GetAltTest()
        {
            Jdi.Assert.AreEquals(JdiLogo.Alt, "Jdi Logo 2");
        }

        [Test]
        public void ClickTest()
        {
            JdiLogo.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "JDI Logo");
            TestSite.Html5Page.GetAlert().AcceptAlert();
        }

        [Test]
        public void IsValidationTest()
        {
            JdiLogo.Is().Src(ContainsStringMatcher.ContainsString("jdi-logo.jpg"))
                .Alt(Is.EqualTo("Jdi Logo 2"));
            JdiLogo.AssertThat().Height(Is.EqualTo(100))
                .Width(Is.EqualTo(101));
        }

        [Test]
        public void AssertValidationTest()
        {
            JdiLogo.AssertThat().Alt(Is.EqualTo("Jdi Logo 2"));
        }
    }
}