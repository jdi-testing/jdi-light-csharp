using JDI.Light.Elements;
using JDI.Light.Factories;
using JDI.Light.Interfaces;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Interfaces.Utils;
using JDI.Light.Logging;
using JDI.Light.Settings;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light
{
    public static class Jdi
    {
        public static IDriverFactory<IWebDriver> DriverFactory;
        public static IWebDriver WebDriver => DriverFactory.GetDriver();
        public static Timeouts Timeouts;
        public static IAssert Assert;
        public static ILogger Logger;
        public static IKillDriver KillDriver;

        static Jdi()
        {
            Timeouts = new Timeouts();
            KillDriver = new WinProcUtils();
        }

        public static void Init(IAssert assert = null, ILogger logger = null, 
            Timeouts timeouts = null, IDriverFactory<IWebDriver> driverFactory = null)
        {
            Assert = assert ?? new BaseAsserter();
            Logger = logger ?? new ConsoleLogger();
            Assert.Logger = Logger;
            DriverFactory = driverFactory ?? new WebDriverFactory();
            Timeouts = timeouts ?? new Timeouts();
        }

        public static T InitSite<T>() where T : IWebSite, new()
        {
            return WebSiteFactory.GetInstanceSite<T>(DriverFactory.CurrentDriverName);
        }

        public static object ExecuteScript(string script, params object[] args)
        {
            return ((IJavaScriptExecutor) WebDriver).ExecuteScript(script, args);
        }

        public static void CloseDriver()
        {
            DriverFactory.Close();
        }

        public static void KillAllDrivers()
        {
            KillDriver.KillAllRunningDrivers();
        }
    }
}