﻿using System;
using System.Linq;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using static JDI.Light.Elements.Composite.WebPage;
using JDI.Light.Tests.UIObjects;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class WebPageTests : TestBase
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
        public void GetGenericPageTest()
        {
            var p = TestSite.Get<ContactPage>("/contacts.html", "Contact Form");
            p.Open();
            p.CheckOpened();
        }

        [Test]
        public void GetCurrentUrlTest()
        {
            TestSite.ContactFormPage.CheckOpened();
            Assert.AreEqual(PageUrl, TestSite.ContactFormPage.Url);
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
            Jdi.Assert.AreEquals(TestSite.HomePage.Cookies.GetCookieNamed(cookie.Name).Value,
                cookie.Value);
        }

        [Test]
        public void DeleteAllCookiesTest()
        {
            Client client = new Client();
            TestSite.HomePage.Open();
            var cookie1 = new Cookie($"key: {Guid.NewGuid()}", $"value: {Guid.NewGuid()}");
            var cookie2 = new Cookie($"key: {Guid.NewGuid()}", $"value: {Guid.NewGuid()}");
            TestSite.HomePage.AddCookie(cookie1);
            TestSite.HomePage.AddCookie(cookie2);
            Jdi.Assert.AreEquals(TestSite.HomePage.Cookies.GetCookieNamed(cookie1.Name).Value,
                cookie1.Value);
            TestSite.HomePage.DeleteAllCookies();
            TestSite.HomePage.Refresh();
            var cookies = TestSite.HomePage.Cookies.AllCookies;
            var cookiesCount = cookies.Count;
            Jdi.Assert.AreEquals(cookiesCount, 0);
            TestSite.HomePage.Profile.Click();
            TestSite.LoginFormPage.AsForm<Client>().Login(client.DefaultClient);
        }

        [Test]
        public void DeleteCookieTest()
        {
            var cookie = new Cookie($"key: {Guid.NewGuid()}", $"value: {Guid.NewGuid()}");
            TestSite.ContactFormPage.AddCookie(cookie);
            Jdi.Assert.AreEquals(TestSite.HomePage.Cookies.GetCookieNamed(cookie.Name).Value,
                cookie.Value);
            TestSite.ContactFormPage.DeleteCookie(cookie);
            Jdi.Assert.IsFalse(TestSite.HomePage.Cookies.AllCookies.Any(c => c.Name.Equals(cookie.Name)));
        }

        [Test]
        public void CheckOpenedTest()
        {
            TestSite.ContactFormPage.CheckOpened();
        }
    }
}