using JDI.Light.Enums;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using JDI.Light.Utils;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests
{
    [SetUpFixture]
    public class TestBase
    {
        [OneTimeSetUp]
        protected void SetUp()
        {
            JDI.Init(assert: new NUnitAsserter());
            JDI.Logger.LogLevel = LogLevel.Debug;
            JDI.GetLatestDriver = false;
            JDI.DriverVersion = "2.41";
            JDI.Logger.Info("Init test run...");
            JDI.Timeouts.WaitElementSec = 40;
            JDI.Timeouts.WaitPageLoadSec = 100;
            WinProcUtils.KillAllRunWebDrivers();
            JDI.InitSite(typeof(TestSite));
            TestSite.HomePage.Open();
            TestSite.LoginForm.Submit(User.DefaultUser);
            JDI.Logger.Info("Run test...");
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            WinProcUtils.KillAllRunWebDrivers();
        }
    }
}