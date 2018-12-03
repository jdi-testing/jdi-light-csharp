using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using JDI.Light.Enums;
using JDI.Light.Interfaces;
using JDI.Light.Settings;
using JDI.Light.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace JDI.Light.Factories
{
    public class WebDriverFactory : IDriverFactory<IWebDriver>
    {
        public static bool OnlyOneElementAllowedInSearch = true;

        public static Size BrowserSize = new Size();

        private readonly Dictionary<DriverType, string> _driverNamesDictionary = new Dictionary<DriverType, string>
        {
            {DriverType.Chrome, "chrome"},
            {DriverType.Firefox, "firefox"},
            {DriverType.IE, "internet explorer"}
        };

        private readonly Dictionary<DriverType, Func<string, IWebDriver>> _driversDictionary = new Dictionary
            <DriverType, Func<string, IWebDriver>>
            {
                {DriverType.Chrome, path => string.IsNullOrEmpty(path) ? new ChromeDriver() : new ChromeDriver(path)},
                {DriverType.Firefox, path => new FirefoxDriver()},
                {
                    DriverType.IE,
                    path => string.IsNullOrEmpty(path)
                        ? new InternetExplorerDriver()
                        : new InternetExplorerDriver(path)
                }
            };

        private readonly object _locker = new object();

        private string _currentDriverName;

        public Func<IWebDriver, IWebDriver> WebDriverSettings = driver =>
        {
            if (BrowserSize.Height == 0)
                driver.Manage().Window.Maximize();
            else
                driver.Manage().Window.Size = BrowserSize;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(JDI.Timeouts.WaitElementSec);
            return driver;
        };

        public WebDriverFactory()
        {
            Drivers = new Dictionary<string, Func<IWebDriver>>();
            RunDrivers = new ThreadLocal<Dictionary<string, IWebDriver>>(() => new Dictionary<string, IWebDriver>());
            DriverPath = AppDomain.CurrentDomain.BaseDirectory;
            RunType = RunType.Local;
        }

        private Dictionary<string, Func<IWebDriver>> Drivers { get; }

        private ThreadLocal<Dictionary<string, IWebDriver>> RunDrivers { get; }
        public RunType RunType { get; set; }

        public Func<IWebElement, bool> ElementSearchCriteria { get; set; } = el => el.Displayed;

        public string CurrentDriverName
        {
            get
            {
                if (string.IsNullOrEmpty(_currentDriverName))
                {
                    _currentDriverName = _driverNamesDictionary[DriverType.Chrome];
                    RegisterLocalDriver(DriverType.Chrome);
                }

                return _currentDriverName;
            }
            set => _currentDriverName = value;
        }

        public string DriverPath { get; set; }

        public IWebDriver GetDriver()
        {
            try
            {
                if (!string.IsNullOrEmpty(CurrentDriverName))
                    return GetDriver(CurrentDriverName);
                RegisterDriver(DriverType.Chrome);
                return GetDriver(DriverType.Chrome);
            }
            catch
            {
                throw new Exception($"Can't get driver: {CurrentDriverName}");
            }
        }

        public IWebDriver GetDriver(string driverName)
        {
            if (!Drivers.ContainsKey(driverName))
                if (Drivers.Count == 0)
                    RegisterDriver(driverName);
                else
                    throw new Exception($"Can't find driver with name {driverName}");
            try
            {
                IWebDriver result;
                lock (_locker)
                {
                    if (RunDrivers.Value == null || !RunDrivers.Value.ContainsKey(driverName))
                    {
                        var rDrivers = RunDrivers.Value ?? new Dictionary<string, IWebDriver>();
                        var resultDriver = Drivers[driverName]();
                        if (resultDriver == null)
                            throw new Exception(
                                $"Can't get WebDriver {driverName}. This Driver name is not registered");
                        rDrivers.Add(driverName, resultDriver);
                        RunDrivers.Value = rDrivers;
                    }

                    result = RunDrivers.Value[driverName];
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception($"Can't get driver: {e.Message}; StackTrace: {e.StackTrace}");
            }
        }

        public string RegisterDriver(string driverName)
        {
            try
            {
                var driverType = _driverNamesDictionary.FirstOrDefault(x => x.Value == driverName).Key;
                return RegisterLocalDriver(driverType);
            }
            catch
            {
                throw new Exception($"Can't register driver: {driverName}");
            }
        }

        public void SetRunType(string runType)
        {
            switch (runType)
            {
                case "local":
                    RunType = RunType.Local;
                    return;
                case "remote":
                    RunType = RunType.Remote;
                    return;
            }

            RunType = RunType.Local;
        }

        public bool HasDrivers()
        {
            return Drivers.Any();
        }

        public bool HasRunDrivers()
        {
            return RunDrivers.Value != null && RunDrivers.Value.Any();
        }

        private string RegisterLocalDriver(DriverType driverType)
        {
            if (JDI.GetLatestDriver)
                if (!WebDriverUtils.IsLocalVersionLatestVersion(driverType, DriverPath))
                    DriverPath = WebDriverUtils.GetLatestVersion(driverType);
            return RegisterDriver(GetDriverName(_driverNamesDictionary[driverType]),
                () => WebDriverSettings(_driversDictionary[driverType](DriverPath)));
        }

        private string GetDriverName(string driverName)
        {
            if (!Drivers.ContainsKey(driverName))
                return driverName;
            string newName;
            var i = 1;
            do
            {
                newName = driverName + i++;
            } while (Drivers.ContainsKey(newName));

            return newName;
        }

        public string RegisterDriver(string driverName, Func<IWebDriver> driver)
        {
            if (Drivers.ContainsKey(driverName))
                throw JDI.Assert.Exception(
                    $"Can't register WebDriver {driverName}. Driver with the same name already registered");
            try
            {
                Drivers.Add(driverName, driver);
                CurrentDriverName = driverName;
            }
            catch (Exception e)
            {
                throw JDI.Assert.Exception($"Can't register WebDriver {driverName}. StackTrace: {e.StackTrace}");
            }

            return driverName;
        }

        public string RegisterDriver(Func<IWebDriver> driver)
        {
            return RegisterDriver("Driver" + (Drivers.Count + 1), driver);
        }

        public IWebDriver GetDriver(DriverType driverType)
        {
            return GetDriver(_driverNamesDictionary[driverType]);
        }

        public string RegisterDriver(DriverType driverType)
        {
            switch (RunType)
            {
                case RunType.Local:
                    return RegisterLocalDriver(driverType);
                case RunType.Remote:
                    return RegisterRemoteDriver(driverType);
            }

            throw new Exception($"Can't register driver: {driverType}");
        }

        private string RegisterRemoteDriver(DriverType driverType)
        {
            var capabilities = new DesiredCapabilities(new Dictionary<string, object>
            {
                {"browserName", _driverNamesDictionary[driverType]},
                {"version", string.Empty},
                {"javaScript", true}
            });

            return RegisterDriver("Remote_" + _driverNamesDictionary[driverType],
                () => new RemoteWebDriver(new Uri(Properties.Settings.Default.remote_url), capabilities));
        }

        public void SwitchToDriver(string driverName)
        {
            if (Drivers.ContainsKey(driverName))
                CurrentDriverName = driverName;
            else
                throw new Exception($"Can't switch to WebDriver {driverName}. This Driver name not registered");
        }

        public void ReopenDriver()
        {
            ReopenDriver(CurrentDriverName);
        }

        public void ReopenDriver(string driverName)
        {
            var rDriver = RunDrivers.Value;
            if (rDriver.ContainsKey(driverName))
            {
                rDriver[driverName].Close();
                rDriver.Remove(driverName);
                RunDrivers.Value = rDriver;
            }

            if (Drivers.ContainsKey(driverName))
                GetDriver();
        }

        public void Close()
        {
            foreach (var driver in RunDrivers.Value)
                driver.Value.Quit();
            RunDrivers.Value.Clear();
        }
    }
}