using System;
using System.IO;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using static JDI.Light.Tests.Entities.Client;
using static JDI.Light.Tests.Entities.User;
using static JDI.Light.Tests.UIObjects.TestSite;

namespace JDI.Light.Tests.Tests
{
    public class TestBase
    {
        public TestSite TestSite { get; set; }

        [SetUp]
        public virtual void SetUpTest()
        {
            Jdi.Logger.Info("Test Base Set up started...");
            TestSite = Jdi.InitSite<TestSite>();
            //TestSite.HomePage.Open();
            HomePageStatic.Open();
            TestSite.HomePage.Profile.Click();
            LoginFormPage.AsForm<Client>().Login(DefaultClient);
            Jdi.Logger.Info("Test Base Set up done.");
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