using System;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using JDI.Light.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using LogLevel = JDI.Light.Enums.LogLevel;

namespace JDI.Light.Tests.Tests
{
    [SetUpFixture]
    public class TestBase
    {
        [OneTimeSetUp]
        protected void OneTimeSetUp()
        {
            JDI.Init(assert: new NUnitAsserter());
            JDI.Logger.LogLevel = LogLevel.Debug;
            JDI.GetLatestDriver = false;
            JDI.DriverVersion = "2.41";
            JDI.Logger.Info("Init test run...");
            JDI.Timeouts.WaitElementSec = 10;
            JDI.Timeouts.WaitPageLoadSec = 10;
            WinProcUtils.KillAllRunWebDrivers();
            JDI.InitSite(typeof(TestSite));
            TestSite.HomePage.Open();
            TestSite.LoginForm.Submit(User.DefaultUser);
            JDI.Logger.Info("Run test...");
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            WinProcUtils.KillAllRunWebDrivers();
        }

        [TearDown]
        public void TestTearDown()
        {
            var res = TestContext.CurrentContext.Result.Outcome;
            if (res.Equals(ResultState.Failure) || res.Equals(ResultState.Error))
            {
                JDI.DriverFactory.GetDriver().TakeScreenshot().SaveAsFile($"C:\\projects\\jdi-light-csharp\\JDI.Light\\JDI.Light.Tests\\{Guid.NewGuid()}.png", ScreenshotImageFormat.Png);
            }
        }
    }
}