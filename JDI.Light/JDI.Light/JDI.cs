using System.Collections.Generic;
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
        public static readonly IDriverFactory<IWebDriver> DriverFactory;
        public static IWebDriver WebDriver => DriverFactory.GetDriver();
        public static readonly Timeouts Timeouts;
        public static readonly IAssert Assert;
        public static readonly ILogger Logger;
        public static readonly IKillDriver KillDriver;
        public static List<ISmartLocator> SmartLocators { get; set; }

        static Jdi()
        {
            Timeouts = new Timeouts();
            KillDriver = new WinProcUtils();
        }

        public static void Init(IAssert assert = null, ILogger logger = null, 
            Timeouts timeouts = null, IDriverFactory<IWebDriver> driverFactory = null, List<ISmartLocator> smartLocators = null)
        {
            Assert = assert ?? new BaseAsserter();
            Logger = logger ?? new ConsoleLogger();
            Assert.Logger = Logger;
            DriverFactory = driverFactory ?? new WebDriverFactory();
            Timeouts = timeouts ?? new Timeouts();
            SmartLocators = smartLocators ?? new List<ISmartLocator>{new SmartLocatorById(), new SmartLocatorByCss()};
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