using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using static JDI.Light.Elements.Base.BaseValidation;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;
using static JDI.Light.Matchers.StringMatchers.EqualToMatcher;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class TextTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
            TestSite.HomePage.Open();
            Jdi.Logger.Info("Setup method finished");
        }

        private readonly string _expectedText = ("Lorem ipsum dolor sit amet, consectetur adipisicing elit,"
                                                 + " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                                                 + " Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris"
                                                 + " nisi ut aliquip ex ea commodo consequat Duis aute irure dolor in"
                                                 + " reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
            ).ToUpper();

        private readonly string _regEx = ".* IPSUM DOLOR SIT AMET.*";
        private readonly string _contains = "ENIM AD MINIM VENIAM, QUIS NOSTRUD";

        [Test]
        public void GetTextTest()
        {
            Jdi.Assert.AreEquals(TestSite.HomePage.Text.Value, _expectedText);
        }

        [Test]
        public void GetValueTest()
        {
            Jdi.Assert.AreEquals(TestSite.HomePage.Text.Value, _expectedText);
        }

        [Test]
        public void SetAttributeTest()
        {
            var attributeName = "testAttr";
            var value = "testValue";
            TestSite.HomePage.Text.SetAttribute(attributeName, value);
            Jdi.Assert.AreEquals(TestSite.HomePage.Text.GetAttribute(attributeName), value);
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.HomePage.Text.Is.Enabled();
            TestSite.HomePage.Text.Is.Text(EqualTo(_expectedText));
            TestSite.HomePage.Text.Is.Text(ContainsString(_contains));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.HomePage.Text.AssertThat.Text(EqualTo(_expectedText));
        }

        [Test]
        public void BaseValidationTest()
        {
            TestSite.Html5Page.Open();
            BaseElementValidation(TestSite.Html5Page.JdiText);
        }
    }
}