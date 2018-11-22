using System;
using JDI.Core.Base;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.DriverFactory;
using JDI.Core.Settings;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Composite
{
    public class WebSite : Application
    {
        public IWebDriver WebDriver => WebSettings.WebDriverFactory.GetDriver(DriverName);
        public string Url => WebDriver.Url;
        public string BaseUrl => new Uri(WebDriver.Url).GetLeftPart(UriPartial.Authority);
        public string Title => WebDriver.Title;
        private static WebCascadeInit WebCascadeInit => new WebCascadeInit();

        public static void Init(Type siteType)
        {
            WebCascadeInit.InitStaticPages(siteType, WebSettings.WebDriverFactory.CurrentDriverName);
            CurrentSite = siteType;
        }

        public static T Init<T>(Type siteType, string driverName) where T : Application
        {
            return WebCascadeInit.InitPages<T>(siteType, driverName);
        }

        public static T Init<T>(Type siteType, DriverTypes driverType = DriverTypes.Chrome) where T : Application
        {
            return Init<T>(siteType, WebSettings.UseDriver(driverType));
        }

        public T Init<T>(string driverName) where T : Application
        {
            DriverName = driverName;
            return Init<T>(GetType(), driverName);
        }

        public static void Open()
        {
            WebSettings.WebDriver.Navigate().GoToUrl(WebSettings.Domain);
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