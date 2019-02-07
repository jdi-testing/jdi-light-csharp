﻿using JDI.Light.Tests.Entities;
using JDI.Light.Utils;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Core
{
    public class WebDriverFactoryTests : TestBase
    {
        [Test]
        public void CanRecreateKilledDriver()
        {
            TestSite.HomePage.CheckOpened();
            Jdi.CloseDriver();
            WinProcUtils.KillAllRunningDrivers();
            TestSite.HomePage.Open();
            TestSite.HomePage.Profile.Click();
            TestSite.HomePage.LoginForm.Submit(User.DefaultUser, "Login");
            TestSite.HomePage.CheckOpened();
        }
    }
}