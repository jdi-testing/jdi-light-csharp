using System;
using System.Drawing;
using System.Linq;
using JDI.Light.Elements;
using JDI.Light.Factories;
using JDI.Light.Interfaces;
using JDI.Light.Logging;
using JDI.Light.Settings;
using JDI.Light.Utils;
using OpenQA.Selenium;
using static JDI.Light.Utils.ExceptionUtils;

namespace JDI.Light
{
    public static class JDI
    {
        public static bool IsDemoMode;
        public static bool UseCache;
        public static WebCascadeInit WebInit;
        public static IDriverFactory<IWebDriver> DriverFactory;
        public static Timeouts Timeouts;
        public static IAssert Assert;
        public static ILogger Logger;
        public static string Domain;
        public static bool HasDomain => Domain != null && Domain.Contains("://");
        public static bool GetLatestDriver = true;
        public static IJavaScriptExecutor JsExecutor => DriverFactory.GetDriver() as IJavaScriptExecutor;

        static JDI()
        {
            Timeouts = new Timeouts();
            WebInit = new WebCascadeInit();

            GetFromPropertiesAvoidExceptions(p => DriverFactory.RegisterDriver(p), "Driver");
            GetFromPropertiesAvoidExceptions(p => DriverFactory.SetRunType(p), "RunType");
            GetFromPropertiesAvoidExceptions(p => Timeouts.WaitElementSec = int.Parse(p), "TimeoutWaitElement");
            GetFromPropertiesAvoidExceptions(p => UseCache = p.ToLower().Equals("true") || p.ToLower().Equals("1"), "Cache");
            GetFromPropertiesAvoidExceptions(p => UseCache = p.ToLower().Equals("true") || p.ToLower().Equals("1"), "DemoMode");
            GetFromPropertiesAvoidExceptions(p => DriverFactory.DriverPath = p, "DriversFolder");
            GetFromPropertiesAvoidExceptions(p =>
            {
                p = p.ToLower();
                if (p.Equals("soft"))
                    p = "any,multiple";
                if (p.Equals("strict"))
                    p = "visible,single";
                if (p.Split(',').Length != 2) return;
                var parameters = p.Split(',').ToList();
                if (parameters.Contains("visible") || parameters.Contains("displayed"))
                    DriverFactory.ElementSearchCriteria = el => el.Displayed;
                if (parameters.Contains("any") || parameters.Contains("all"))
                    DriverFactory.ElementSearchCriteria = el => el != null;
                if (parameters.Contains("single") || parameters.Contains("displayed"))
                    WebDriverFactory.OnlyOneElementAllowedInSearch = true;
                if (parameters.Contains("multiple") || parameters.Contains("displayed"))
                    WebDriverFactory.OnlyOneElementAllowedInSearch = false;
            }, "SearchElementStrategy");
            GetFromPropertiesAvoidExceptions(p => Domain = p, "Domain");
            GetFromPropertiesAvoidExceptions(p => GetLatestDriver = p.ToLower().Equals("true") || p.ToLower().Equals("1"), "GetLatest");
            GetFromPropertiesAvoidExceptions(p =>
            {
                string[] split = null;
                if (p.Split(',').Length == 2)
                    split = p.Split(',');
                if (p.ToLower().Split('x').Length == 2)
                    split = p.ToLower().Split('x');
                if (split != null)
                    WebDriverFactory.BrowserSize = new Size(int.Parse(split[0]), int.Parse(split[1]));
            }, "BrowserSize");
        }

        public static void Init(ILogger logger = null, IAssert assert = null,
            Timeouts timeouts = null, IDriverFactory<IWebDriver> driverFactory = null)
        {
            Logger = logger ?? new ConsoleLogger();
            Assert = assert ?? new BaseAsserter();
            Assert.SetUpLogger(Logger);
            DriverFactory = driverFactory ?? new WebDriverFactory();
            Timeouts = timeouts ?? new Timeouts();
        }

        public static void InitSite(Type siteType)
        {
            WebInit.InitStaticPages(siteType, DriverFactory.CurrentDriverName);
        }
    }
}