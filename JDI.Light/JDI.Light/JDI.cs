using System;
using JDI.Light.Elements;
using JDI.Light.Factories;
using JDI.Light.Interfaces;
using JDI.Light.Logging;
using JDI.Light.Settings;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light
{
    public static class Jdi
    {
        public static WebCascadeInit WebInit;
        public static IDriverFactory<IWebDriver> DriverFactory;
        public static IWebDriver WebDriver => DriverFactory.GetDriver();
        public static Timeouts Timeouts;
        public static IAssert Assert;
        public static ILogger Logger;
        public static string Domain;
        public static bool HasDomain => Domain != null && Domain.Contains("://");
        public static bool GetLatestDriver = true;
        public static string DriverVersion = "";

        static Jdi()
        {
            Timeouts = new Timeouts();
            WebInit = new WebCascadeInit();
        }

        public static void Init(ILogger logger = null, IAssert assert = null,
            Timeouts timeouts = null, IDriverFactory<IWebDriver> driverFactory = null)
        {
            Logger = logger ?? new ConsoleLogger();
            Assert = assert ?? new BaseAsserter();
            Assert.Logger = Logger;
            DriverFactory = driverFactory ?? new WebDriverFactory();
            Timeouts = timeouts ?? new Timeouts();
        }

        public static void InitSite(Type siteType)
        {
            WebInit.InitStaticPages(siteType, DriverFactory.CurrentDriverName);
        }

        public static object ExecuteScript(string script, params object[] args)
        {
            return ((IJavaScriptExecutor) WebDriver).ExecuteScript(script, args);
        }
    }
}