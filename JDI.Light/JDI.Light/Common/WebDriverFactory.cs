using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JDI.Light.Common
{
    public class WebDriverFactory : IWebDriverFactory
    {
        private IWebDriver _currentWebDriver;
        private IWebDriver _defaultWebDriver;

        public WebDriverFactory()
        {
        }

        public WebDriverFactory(IWebDriver webDriver)
        {
            _currentWebDriver = webDriver;
        }

        public IWebDriver DefaultWebDriver
        {
            get => _defaultWebDriver ?? new ChromeDriver();
            set => _defaultWebDriver = value;
        }

        public IWebDriver GetWebDriver()
        {
            return _currentWebDriver ?? DefaultWebDriver;
        }

        public void SetCurrentWebDriver(IWebDriver webDriver)
        {
            _currentWebDriver = webDriver;
        }

        public void SetDefaultWebDriver(IWebDriver webDriver)
        {
            _defaultWebDriver = webDriver;
        }

        public void Dispose()
        {
            _currentWebDriver.Close();
            _currentWebDriver.Dispose();
            DefaultWebDriver.Close();
            DefaultWebDriver.Dispose();
        }
    }
}