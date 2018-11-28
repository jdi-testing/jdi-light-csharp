using JDI.Light.Selenium.DriverFactory;
using JDI.Light.Settings;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests
{
    [SetUpFixture]
    public class TestBase
    {
        [OneTimeSetUp]
        protected void SetUp()
        {
            WebSettings.Init(null, new NUnitAsserter());
            WebSettings.Logger.Info("Init test run...");
            WinProcUtils.KillAllRunWebDrivers();
            JDI.InitSite(typeof(TestSite));
            TestSite.HomePage.Open();
            TestSite.LoginForm.Submit(User.DefaultUser);
            WebSettings.Logger.Info("Run test...");
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            // Some log outputs
            WinProcUtils.KillAllRunWebDrivers();
        }
    }
}