using JDI.Light.Exceptions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.UITests.Composite
{
    [TestFixture]
    public class MenuTests : TestBase
    {
        [Test]
        public void SingleLevelTest()
        {
            TestSite.HomePage.Open();
            TestSite.Header.Menu.MenuItemLocator = By.XPath(".//li/a");
            TestSite.Header.Menu.Select("CONTACT FORM");
            TestSite.ContactFormPage.CheckOpened();
        }

        [Test]
        public void SingleLevelNegativeTest()
        {
            TestSite.HomePage.Open();
            TestSite.Header.Menu.MenuItemLocator = By.XPath(".//li/a");
            Assert.Throws<ElementNotFoundException>(() => TestSite.Header.Menu.Select("12345"));
        }

        [Test]
        public void MultiLevelTest()
        {
            TestSite.HomePage.Open();
            TestSite.Header.Menu.MenuItemLocator = By.XPath(".//li/a");
            TestSite.Header.Menu.Select("SERVICE", "SUPPORT");
            TestSite.SupportPage.CheckOpened();
        }
        
        [Test]
        public void MultiLevelNegativeTest()
        {
            TestSite.HomePage.Open();
            TestSite.Header.Menu.MenuItemLocator = By.XPath(".//li/a");
            Assert.Throws<ElementNotFoundException>(() => TestSite.Header.Menu.Select("SERVICE", "A111222"));
        }
    }
}