using OpenQA.Selenium;

namespace JDI.Light.Common
{
    public interface IWebDriverFactory
    {
        IWebDriver DefaultWebDriver { get; set; }
        IWebDriver GetWebDriver();
        void SetCurrentWebDriver(IWebDriver webDriver);
        void SetDefaultWebDriver(IWebDriver webDriver);
        void Dispose();
    }
}