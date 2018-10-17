using OpenQA.Selenium;

namespace JDI.Light.Common
{
    public interface IWebDriverFactory
    {
        IWebDriver DefaultWebDriver { get; set; }
        IWebDriver GetWebDriver();
        void SetWebDriver(IWebDriver webDriver);
        void Dispose();
    }
}