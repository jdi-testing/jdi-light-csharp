using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Matchers.StringMatchers;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class IconTests : TestBase
    {
        private Icon JdiLogo => TestSite.Header.UserIcon;

        [SetUp]
        public void SetUp()
        {
            TestSite.HomePage.Open();
        }

        private const string Text = "https://jdi-testing.github.io/jdi-light/images/icons/user-icon.jpg";

        [Test]
        public void GetSrcTest()
        {
            Jdi.Assert.AreEquals(JdiLogo.Src, Text);
        }

        [Test]
        public void GetAltTest()
        {
            Jdi.Assert.AreEquals(JdiLogo.Alt, "");
        }

        [Test]
        public void IsValidationTest()
        {
            JdiLogo.Is().Src(ContainsStringMatcher.ContainsString("user-icon.jpg"))
                .Alt(Is.EqualTo(""));
            JdiLogo.AssertThat().Height(Is.EqualTo(30))
                .Width(Is.EqualTo(30));
        }

        [Test]
        public void AssertValidationTest()
        {
            JdiLogo.AssertThat().Src(Is.EqualTo(Text));
        }

        [Test]
        public void BaseValidationTest()
        {
            BaseValidation.BaseElementValidation(JdiLogo);
        }
    }
}
