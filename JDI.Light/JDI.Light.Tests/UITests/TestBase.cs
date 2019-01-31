using System;
using System.IO;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace JDI.Light.Tests.UITests
{
    public class TestBase
    {
        public TestSite TestSite { get; set; }

        [SetUp]
        public virtual void SetUpTest()
        {
            TestSite = Jdi.InitSite<TestSite>();
            TestSite.HomePage.Open();
            TestSite.HomePage.Profile.Click();
            TestSite.HomePage.LoginForm.Submit(User.DefaultUser, "Login");
            Jdi.Logger.Info("Run test...");
        }

        [TearDown]
        public virtual void TestTearDown()
        {
            Jdi.Logger.Info("Run test tear down...");
            var folder = @"C:\Screenshots";
            Directory.CreateDirectory(folder);
            var res = TestContext.CurrentContext.Result.Outcome;
            if (res.Equals(ResultState.Failure) || res.Equals(ResultState.Error))
            {
                Jdi.WebDriver.TakeScreenshot()
                    .SaveAsFile(Path.Combine(folder, $"{Guid.NewGuid()}.png"), ScreenshotImageFormat.Png);
            }
            TestSite.HomePage.Open();
            TestSite.HomePage.Profile.Click();
            TestSite.HomePage.LogoutButton.Click();
            Jdi.Logger.Info("Run test tear down done.");
        }
    }
}