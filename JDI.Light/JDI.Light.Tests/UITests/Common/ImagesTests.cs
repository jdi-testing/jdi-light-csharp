using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class ImagesTests : TestBase
    {
        private const string Alt = "ALT";
        private const string Src = "https://epam.github.io/JDI/images/Logo_Epam_Color.svg";
        private readonly IImage _logoImage = TestSite.HomePage.LogoImage;

        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Home page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            TestSite.HomePage.IsOpened();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClickTest()
        {
            TestSite.ContactFormPage.Open();
            _logoImage.Click();
            TestSite.HomePage.IsOpened();
        }

        [Test]
        public void SetAttributeTest()
        {
            var _attributeName = "testAttr";
            var _value = "testValue";
            _logoImage.SetAttribute(_attributeName, _value);
            JDI.Assert.AreEquals(_logoImage.GetAttribute(_attributeName), _value);
        }

        [Test]
        public void GetSourceTest()
        {
            JDI.Assert.AreEquals(_logoImage.GetSource(), Src);
        }

        [Test]
        public void GetTipTest()
        {
            JDI.Assert.AreEquals(_logoImage.GetAlt(), Alt);
        }
    }
}