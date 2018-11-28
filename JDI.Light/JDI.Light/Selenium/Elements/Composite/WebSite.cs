using System;
using JDI.Light.Settings;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Composite
{
    public class WebSite
    {
        public string DriverName { set; get; }
        public IWebDriver WebDriver => WebSettings.WebDriverFactory.GetDriver(DriverName);
        public string Url => WebDriver.Url;
        public string BaseUrl => new Uri(WebDriver.Url).GetLeftPart(UriPartial.Authority);
        public string Title => WebDriver.Title;
        
        public void Open()
        {
            WebDriver.Navigate().GoToUrl(WebSettings.Domain);
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