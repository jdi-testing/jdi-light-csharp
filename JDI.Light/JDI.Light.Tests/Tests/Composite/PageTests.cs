using JDI.Light.Settings;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Composite
{
    public class PageTests
    {
        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Contact page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            TestSite.ContactFormPage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RefreshTest()
        {
            TestSite.ContactFormPage.ContactSubmit.Click();
            new Check().AreEquals(TestSite.ContactFormPage.Result.GetText, "Summary: 3");
            TestSite.ContactFormPage.Refresh();
            new Check().AreEquals(TestSite.ContactFormPage.Result.GetText, "");
            TestSite.ContactFormPage.CheckOpened();
        }

        [Test]
        public void BackTest()
        {
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckOpened();
            TestSite.HomePage.Back();
            TestSite.ContactFormPage.CheckOpened();
        }

        [Test]
        public void ForwardTest()
        {
            TestSite.HomePage.Open();
            TestSite.HomePage.Back();
            TestSite.ContactFormPage.CheckOpened();
            TestSite.ContactFormPage.Forward();
            TestSite.HomePage.CheckOpened();
        }

        [Test]
        public void AddCookieTest()
        {
            TestSite.HomePage.WebDriver.Manage().Cookies.DeleteAllCookies();
            new Check().IsTrue(TestSite.HomePage.WebDriver.Manage().Cookies.AllCookies.Count == 0);
            var cookie = new Cookie("key", "value");
            TestSite.ContactFormPage.AddCookie(cookie);
            new Check().AreEquals(TestSite.HomePage.WebDriver.Manage().Cookies.GetCookieNamed(cookie.Name).Value,
                cookie.Value);
        }

        [Test]
        public void ClearCacheTest()
        {
            var cookie = new Cookie("key", "value");
            TestSite.HomePage.WebDriver.Manage().Cookies.AddCookie(cookie);
            new Check().IsFalse(TestSite.HomePage.WebDriver.Manage().Cookies.AllCookies.Count == 0);
            TestSite.ContactFormPage.ClearCache();
            new Check().IsTrue(TestSite.HomePage.WebDriver.Manage().Cookies.AllCookies.Count == 0);
        }

        [Test]
        public void CheckOpenedTest()
        {
            TestSite.ContactFormPage.CheckOpened();
        }

        [TearDown]
        public void TearDown()
        {
            var loginCookie = new Cookie("authUser", "true", "jdi-framework.github.io", "/", null);
            TestSite.HomePage.WebDriver.Manage().Cookies.AddCookie(loginCookie);
        }
    }
}