using JDI.Core.Interfaces.Common;
using JDI.Core.Settings;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    public class ImagesTests
    {
        private const string ALT = "ALT";
        private const string SRC = "https://jdi-framework.github.io/tests/images/Logo_Epam_Color.svg";
        private readonly IImage _logoImage = TestSite.HomePage.LogoImage;

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            TestSite.HomePage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
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
            new Check().AreEquals(_logoImage.GetAttribute(_attributeName), _value);
        }

        [Test]
        public void GetSourceTest()
        {
            new Check().AreEquals(_logoImage.GetSource(), SRC);
        }

        [Test]
        public void GetTipTest()
        {
            new Check().AreEquals(_logoImage.GetAlt(), ALT);
        }
    }
}