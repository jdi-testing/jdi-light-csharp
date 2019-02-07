using System;
using JDI.Light.Elements.WebActions;
using JDI.Light.Factories;
using JDI.Light.Interfaces;
using JDI.Light.Interfaces.Composite;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class WebSite : ISite
    {
        public ActionInvoker Invoker { get; set; }
        public ILogger Logger { get; set; }
        public string DriverName { set; get; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public bool HasDomain => Domain != null && Domain.Contains("://");
        public IWebDriver WebDriver => Jdi.DriverFactory.GetDriver(DriverName);
        public string Url => WebDriver.Url;
        public string BaseUrl => new Uri(WebDriver.Url).GetLeftPart(UriPartial.Authority);
        public string Title => WebDriver.Title;

        public void Open()
        {
            WebDriver.Navigate().GoToUrl(Domain);
        }
        
        public T Get<T>(string relativeUrl, string title = "") where T : WebPage
        {
            var page = typeof(T).CreateInstance(relativeUrl, title);
            page.Parent = this;
            page.InitMembers(DriverName);
            return (T)page;
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