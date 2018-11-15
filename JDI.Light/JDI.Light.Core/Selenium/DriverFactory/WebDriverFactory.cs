using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using JDI.Core.Interfaces;
using JDI.Core.Interfaces.Base;
using JDI.Core.Selenium.Elements.Base;
using JDI.Core.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace JDI.Core.Selenium.DriverFactory
{
    public enum RunTypes
    {
        Local,
        Remote
    }

    public enum DriverTypes
    {
        Chrome,
        Firefox,
        IE
    }

    public class WebDriverFactory : IDriverFactory<IWebDriver>
    {
        public static bool OnlyOneElementAllowedInSearch = true;

        public static Size BrowserSize = new Size();

        private readonly Dictionary<DriverTypes, string> _driverNamesDictionary = new Dictionary<DriverTypes, string>
        {
            {DriverTypes.Chrome, "chrome"},
            {DriverTypes.Firefox, "firefox"},
            {DriverTypes.IE, "internet explorer"}
        };

        private readonly Dictionary<DriverTypes, Func<string, IWebDriver>> _driversDictionary = new Dictionary
            <DriverTypes, Func<string, IWebDriver>>
            {
                {DriverTypes.Chrome, path => string.IsNullOrEmpty(path) ? new ChromeDriver() : new ChromeDriver(path)},
                {DriverTypes.Firefox, path => new FirefoxDriver()},
                {
                    DriverTypes.IE,
                    path => string.IsNullOrEmpty(path)
                        ? new InternetExplorerDriver()
                        : new InternetExplorerDriver(path)
                }
            };

        private readonly object _locker = new object();

        private string _currentDriverName;
        public Func<IWebElement, bool> ElementSearchCriteria = el => el.Displayed;
        public HighlightSettings HighlightSettings = new HighlightSettings();

        public Func<IWebDriver, IWebDriver> WebDriverSettings = driver =>
        {
            if (BrowserSize.Height == 0)
                driver.Manage().Window.Maximize();
            else
                driver.Manage().Window.Size = BrowserSize;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(JDISettings.Timeouts.WaitElementSec);
            return driver;
        };

        public WebDriverFactory()
        {
            Drivers = new Dictionary<string, Func<IWebDriver>>();
            RunDrivers = new ThreadLocal<Dictionary<string, IWebDriver>>(() => new Dictionary<string, IWebDriver>());
            DriverPath = AppDomain.CurrentDomain.BaseDirectory;
            RunType = RunTypes.Local;
        }

        private Dictionary<string, Func<IWebDriver>> Drivers { get; }

        private ThreadLocal<Dictionary<string, IWebDriver>> RunDrivers { get; }
        public RunTypes RunType { get; set; }

        public string CurrentDriverName
        {
            get
            {
                if (string.IsNullOrEmpty(_currentDriverName))
                {
                    _currentDriverName = _driverNamesDictionary[DriverTypes.Chrome];
                    RegisterLocalDriver(DriverTypes.Chrome);
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
                RegisterDriver(DriverTypes.Chrome);
                return GetDriver(DriverTypes.Chrome);
            }
            catch
            {
                throw new Exception(); // TODO
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
                throw new Exception(); // TODO
            }
        }

        public void SetRunType(string runType)
        {
            switch (runType)
            {
                case "local":
                    RunType = RunTypes.Local;
                    return;
                case "remote":
                    RunType = RunTypes.Remote;
                    return;
            }

            RunType = RunTypes.Local;
        }

        public bool HasDrivers()
        {
            return Drivers.Any();
        }

        public bool HasRunDrivers()
        {
            return RunDrivers.Value != null && RunDrivers.Value.Any();
        }

        public void Highlight(IElement element)
        {
            Highlight(element, HighlightSettings);
        }

        public void Highlight(IElement element, HighlightSettings highlightSettings)
        {
            if (highlightSettings == null)
                highlightSettings = new HighlightSettings();
            var orig = ((WebElement) element).GetWebElement().GetAttribute("style");
            element.SetAttribute("style",
                $"border: 3px solid {highlightSettings.FrameColor}; background-color: {highlightSettings.BgColor};");
            Thread.Sleep(highlightSettings.TimeoutInSec * 1000);
            element.SetAttribute("style", orig);
        }

        private string RegisterLocalDriver(DriverTypes driverType)
        {
            if (WebSettings.GetLatestDriver)
                if (!DriverManager.WebDriverManager.IsLocalVersionLatestVersion(driverType, DriverPath))
                    DriverPath = DriverManager.WebDriverManager.GetLatestVersion(driverType);
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
                throw JDISettings.Exception(
                    $"Can't register WebDriver {driverName}. Driver with the same name already registered");
            try
            {
                Drivers.Add(driverName, driver);
                CurrentDriverName = driverName;
            }
            catch (Exception e)
            {
                throw JDISettings.Exception($"Can't register WebDriver {driverName}. StackTrace: {e.StackTrace}");
            }

            return driverName;
        }

        public string RegisterDriver(Func<IWebDriver> driver)
        {
            return RegisterDriver("Driver" + (Drivers.Count + 1), driver);
        }

        public IWebDriver GetDriver(DriverTypes driverType)
        {
            return GetDriver(_driverNamesDictionary[driverType]);
        }

        public string RegisterDriver(DriverTypes driverType)
        {
            switch (RunType)
            {
                case RunTypes.Local:
                    return RegisterLocalDriver(driverType);
                case RunTypes.Remote:
                    return RegisterRemoteDriver(driverType);
            }

            throw new Exception(); // TODO
        }

        private string RegisterRemoteDriver(DriverTypes driverType)
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
                GetDriver(); // TODO
        }

        public void Close()
        {
            foreach (var driver in RunDrivers.Value)
                driver.Value.Quit();
            RunDrivers.Value.Clear();
        }
    }
}