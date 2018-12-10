﻿using JDI.Light.Enums;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using JDI.Light.Utils;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests
{
    [SetUpFixture]
    public class TestsSetUp
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
    }
}