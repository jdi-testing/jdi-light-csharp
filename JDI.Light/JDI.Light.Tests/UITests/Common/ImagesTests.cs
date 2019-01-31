using JDI.Light.Interfaces.Common;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class ImagesTests : TestBase
    {
        private const string Alt = "ALT";
        private const string Src = "https://epam.github.io/JDI/images/Logo_Epam_Color.svg";
        private IImage LogoImage => TestSite.HomePage.LogoImage;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Home page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClickTest()
        {
            TestSite.ContactFormPage.Open();
            LogoImage.Click();
            Assert.IsTrue(TestSite.HomePage.IsOpened);
        }

        [Test]
        public void SetAttributeTest()
        {
            var _attributeName = "testAttr";
            var _value = "testValue";
            LogoImage.SetAttribute(_attributeName, _value);
            Jdi.Assert.AreEquals(LogoImage.GetAttribute(_attributeName), _value);
        }

        [Test]
        public void GetSourceTest()
        {
            Jdi.Assert.AreEquals(LogoImage.GetSource(), Src);
        }

        [Test]
        public void GetTipTest()
        {
            Jdi.Assert.AreEquals(LogoImage.GetAlt(), Alt);
        }
    }
}