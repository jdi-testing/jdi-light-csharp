using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JDI.Light.Common
{
    public class WebDriverFactory : IWebDriverFactory
    {
        private IWebDriver _currentWebDriver;

        public WebDriverFactory()
        {
            DefaultWebDriver =  new ChromeDriver();
        }

        public WebDriverFactory(IWebDriver webDriver)
        {
            _currentWebDriver = webDriver;
            DefaultWebDriver = new ChromeDriver();
        }

        public IWebDriver DefaultWebDriver { get; set; }

        public IWebDriver GetWebDriver()
        {
            return _currentWebDriver ?? DefaultWebDriver;
        }

        public void SetWebDriver(IWebDriver webDriver)
        {
            _currentWebDriver = webDriver;
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