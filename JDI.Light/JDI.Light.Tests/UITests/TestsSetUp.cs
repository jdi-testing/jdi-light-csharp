using JDI.Light.Enums;
using JDI.Light.Tests.Asserts;
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
            Jdi.Init(assert: new NUnitAsserter());
            Jdi.Logger.LogLevel = LogLevel.Debug;
            Jdi.GetLatestDriver = false;
            Jdi.DriverVersion = "2.41";
            Jdi.Logger.Info("Init test run...");
            Jdi.Timeouts.WaitElementSec = 20;
            Jdi.Timeouts.WaitPageLoadSec = 30;
            WinProcUtils.ProcessToKill = new[] { "chromedriver" };
            WinProcUtils.KillAllRunningDrivers();
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            WinProcUtils.KillAllRunningDrivers();
        }
    }
}