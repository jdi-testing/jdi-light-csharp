using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.UITests.Composite
{
    [TestFixture]
    public class PageTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Contact page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RefreshTest()
        {
            TestSite.ContactFormPage.CheckOpened();
            TestSite.ContactFormPage.ContactSubmit.Click();
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, "Summary: 3");
            TestSite.ContactFormPage.Refresh();
            Jdi.Assert.AreEquals(TestSite.ContactFormPage.Result.Value, "");
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
            TestSite.HomePage.CheckOpened();
            TestSite.HomePage.Back();
            TestSite.ContactFormPage.CheckOpened();
            TestSite.ContactFormPage.Forward();
            TestSite.HomePage.CheckOpened();
        }

        [Test]
        public void AddCookieTest()
        {
            var cookie = new Cookie($"key: {Guid.NewGuid()}", $"value: {Guid.NewGuid()}");
            TestSite.ContactFormPage.AddCookie(cookie);
            Jdi.Assert.AreEquals(TestSite.HomePage.WebDriver.Manage().Cookies.GetCookieNamed(cookie.Name).Value,
                cookie.Value);
        }

        [Test]
        public void DeleteCookieTest()
        {
            var cookie = new Cookie($"key: {Guid.NewGuid()}", $"value: {Guid.NewGuid()}");
            TestSite.ContactFormPage.AddCookie(cookie);
            Jdi.Assert.AreEquals(TestSite.HomePage.WebDriver.Manage().Cookies.GetCookieNamed(cookie.Name).Value,
                cookie.Value);
            TestSite.ContactFormPage.DeleteCookie(cookie);
            Jdi.Assert.IsFalse(TestSite.HomePage.WebDriver.Manage().Cookies.AllCookies.Any(c => c.Name.Equals(cookie.Name)));
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