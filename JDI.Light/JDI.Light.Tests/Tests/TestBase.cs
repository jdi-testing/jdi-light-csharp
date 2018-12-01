using JDI.Light.Selenium.DriverFactory;
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
            JDI.Init(null, new NUnitAsserter());
            JDI.Logger.Info("Init test run...");
            WinProcUtils.KillAllRunWebDrivers();
            JDI.InitSite(typeof(TestSite));
            TestSite.HomePage.Open();
            TestSite.LoginForm.Submit(User.DefaultUser);
            JDI.Logger.Info("Run test...");
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            // Some log outputs
            WinProcUtils.KillAllRunWebDrivers();
        }
    }
}