using System;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class WebSite
    {
        public string DriverName { set; get; }
        public IWebDriver WebDriver => JDI.DriverFactory.GetDriver(DriverName);
        public string Url => WebDriver.Url;
        public string BaseUrl => new Uri(WebDriver.Url).GetLeftPart(UriPartial.Authority);
        public string Title => WebDriver.Title;
        
        public void Open()
        {
            WebDriver.Navigate().GoToUrl(JDI.Domain);
        }

        public void OpenUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void OpenBaseUrl()
        {
            WebDriver.Navigate().GoToUrl(BaseUrl);
        }

        public void Refresh()
        {
            WebDriver.Navigate().Refresh();
        }

        public void Forward()
        {
            WebDriver.Navigate().Forward();
        }

        public void Back()
        {
            WebDriver.Navigate().Back();
        }
    }
}