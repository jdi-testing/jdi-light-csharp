using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JDI.Light.Enums;
using JDI.Light.Interfaces;
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
        public bool GetLatestDriver { get; set; } = true;
        public Size BrowserSize = new Size();
        public string RemoteUrl = "";

        public WebDriverFactory()
        {
            Drivers = new Dictionary<string, Func<IWebDriver>>();
            WebDriverSettings = driver =>
            {
                if (BrowserSize.Height == 0)
                {
                    driver.Manage().Window.Maximize();
                }
                else
                {
                    driver.Manage().Window.Size = BrowserSize;
                }
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(Jdi.Timeouts.WaitElementMSec);
                return driver;
            };
            RunDrivers = new ConcurrentDictionary<string, IWebDriver>();
            DriverPath = AppDomain.CurrentDomain.BaseDirectory;
            RunType = RunType.Local;
            DriverVersion = "";
        }

        private readonly Dictionary<DriverType, string> _driverNamesDictionary = new Dictionary<DriverType, string>
        {
            {DriverType.Chrome, "chrome"},
            {DriverType.Firefox, "firefox"},
            {DriverType.IE, "internet explorer"}
        };

        private readonly Dictionary<DriverType, Func<string, IWebDriver>> _driversDictionary = new Dictionary<DriverType, Func<string, IWebDriver>>
        {
                {DriverType.Chrome, path =>
                    {
                        var o = new ChromeOptions();
                        o.AddArgument("-no-sandbox");
                        return string.IsNullOrEmpty(path) ? new ChromeDriver(o) : new ChromeDriver(path, o, TimeSpan.FromSeconds(150));
                    }
                },
                {DriverType.Firefox, path => string.IsNullOrEmpty(path) ? new FirefoxDriver() : new FirefoxDriver(path)},
                {DriverType.IE, path => string.IsNullOrEmpty(path) ? new InternetExplorerDriver() : new InternetExplorerDriver(path)}
            };

        private string _currentDriverName;

        public Func<IWebDriver, IWebDriver> WebDriverSettings;
        public string DriverPath { get; set; }
        public string DriverVersion { get; set; }
        private Dictionary<string, Func<IWebDriver>> Drivers { get; }
        private ConcurrentDictionary<string, IWebDriver> RunDrivers { get; }
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

        public IWebDriver GetDriver()
        {
            try
            {
                if (!string.IsNullOrEmpty(CurrentDriverName))
                {
                    return GetDriver(CurrentDriverName);
                }
                RegisterDriver(DriverType.Chrome);
                return GetDriver(DriverType.Chrome);
            }
            catch (Exception e)
            {
                throw new Exception($"Can't get driver: {CurrentDriverName}.{Environment.NewLine}Message: {e.Message}, {Environment.NewLine}" +
                                    $"Stack trace:{Environment.NewLine}{e.StackTrace}");
            }
        }

        public IWebDriver GetDriver(string driverName)
        {
            driverName = driverName ?? CurrentDriverName;
            if (!Drivers.ContainsKey(driverName))
            {
                if (Drivers.Count == 0)
                {
                    RegisterDriver(driverName);
                }
                else
                {
                    throw new Exception($"Can't find driver with name {driverName}");
                }
            }
            try
            {
                if (RunDrivers.TryGetValue(driverName, out var driver))
                {
                    return driver;
                }

                if (Drivers.TryGetValue(driverName, out var getFunc))
                {
                    var result = getFunc();
                    RunDrivers.TryAdd(driverName, result);
                    return result;
                }
                throw new ApplicationException($"Can't get WebDriver {driverName}. This Driver name is not registered");
            }
            catch (Exception e)
            {
                throw new Exception($"Can't get driver: {e.Message}; StackTrace: {e.StackTrace}", e);
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
                case "remote":
                    RunType = RunType.Remote;
                    return;
                default:
                    RunType = RunType.Local;
                    return;
            }
        }

        public bool HasDrivers()
        {
            return Drivers.Any();
        }

        public bool HasRunDrivers()
        {
            return !RunDrivers.IsEmpty;
        }

        private string RegisterLocalDriver(DriverType driverType)
        {
            if (GetLatestDriver)
            {
                if (!WebDriverUtils.IsLocalVersionLatestVersion(driverType, DriverPath))
                {
                    DriverPath = WebDriverUtils.GetLatestVersion(driverType);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(DriverVersion))
                {
                    DriverPath = WebDriverUtils.GetSpecifiedVersion(driverType, DriverVersion);
                }
            }

            return RegisterDriver(GetDriverName(_driverNamesDictionary[driverType]),
                () => WebDriverSettings(_driversDictionary[driverType](DriverPath)));
        }

        private string GetDriverName(string driverName)
        {
            if (!Drivers.ContainsKey(driverName))
            {
                return driverName;
            }
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
            {
                throw Jdi.Assert.Exception(
                    $"Can't register WebDriver {driverName}. Driver with the same name already registered");
            }
            try
            {
                Drivers.Add(driverName, driver);
                CurrentDriverName = driverName;
            }
            catch (Exception e)
            {
                throw Jdi.Assert.Exception($"Can't register WebDriver {driverName}. StackTrace: {e.StackTrace}");
            }

            return driverName;
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
                () => new RemoteWebDriver(new Uri(RemoteUrl), capabilities));
        }

        public void Close()
        {
            while (!RunDrivers.IsEmpty)
            {
                var keys = RunDrivers.Keys.ToArray();
                foreach (var key in keys)
                {
                    if (RunDrivers.TryRemove(key, out var driver))
                    {
                        driver.Quit();
                    }

                    Drivers.Remove(key);
                }
            }
        }
    }
}