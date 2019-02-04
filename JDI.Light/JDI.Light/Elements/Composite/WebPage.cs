using System.Linq;
using System.Text.RegularExpressions;
using JDI.Light.Elements.WebActions;
using JDI.Light.Enums;
using JDI.Light.Extensions;
using JDI.Light.Interfaces;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class WebPage : IPage
    {
        public static bool CheckAfterOpen = false;
        private string _url;
        public CheckPageType CheckTitleType { get; set; } = CheckPageType.None;
        public CheckPageType CheckUrlType { get; set; } = CheckPageType.None;
        public string Title { get; set; }
        public string UrlTemplate { get; set; }
        public ActionInvoker Invoker { get; set; }
        public ILogger Logger { get; set; }
        public string DriverName { get; set; }
        public string Name { get; set; }
        public ISite Parent { get; set; }
        public IWebDriver WebDriver => Jdi.DriverFactory.GetDriver(DriverName);
        public Timer Timer { get; set; }

        public WebPage() : this(null)
        {
        }

        public WebPage(string url) : this(url, null)
        {
        }

        public WebPage(string url, string title)
        {
            _url = url;
            Title = title;
            Logger = Jdi.Logger;
            Timer = new Timer(Jdi.Timeouts.WaitPageLoadMSec, Jdi.Timeouts.RetryMSec, Logger);
            Invoker = new ActionInvoker(Logger, Timer);
            Name = $"{Title} ({Url})";
        }

        public string Url
        {
            get => _url == null || _url.StartsWith("http://") || _url.StartsWith("https://") || !Parent.HasDomain
                ? _url
                : Parent.Domain + "/" + new Regex("^//*").Replace(_url, "");
            set => _url = value;
        }

        public void Open()
        {
            Invoker.DoActionWithWait($"Open page {Name} by url {Url}",
                () => WebDriver.Navigate().GoToUrl(Url));
            if (CheckAfterOpen)
                CheckOpened();
        }

        public bool IsOpened
        {
            get
            {
                var url = WebDriver.Url;
                if (string.IsNullOrEmpty(UrlTemplate)
                    && new[] { CheckPageType.None, CheckPageType.Equal }.Contains(CheckUrlType))
                    return url.Equals(Url);
                switch (CheckUrlType)
                {
                    case CheckPageType.None:
                        return url.Contains(UrlTemplate) || url.Matches(UrlTemplate);
                    case CheckPageType.Equal:
                        return url.Equals(Url);
                    case CheckPageType.Match:
                        return url.Matches(UrlTemplate);
                    case CheckPageType.Contains:
                        return url.Contains(string.IsNullOrEmpty(UrlTemplate) ? Url : UrlTemplate);
                }

                return false;
            }
        }

        public void Refresh()
        {
            Invoker.DoActionWithWait($"Refresh page {Name}", () => WebDriver.Navigate().Refresh());
        }

        public void Back()
        {
            Invoker.DoActionWithWait("Go back to previous page", () => WebDriver.Navigate().Back());
        }

        public void Forward()
        {
            Invoker.DoActionWithWait("Go forward to next page", () => WebDriver.Navigate().Forward());
        }

        public void AddCookie(Cookie cookie)
        {
            Invoker.DoActionWithWait("Add cookie for the page", () => WebDriver.Manage().Cookies.AddCookie(cookie));
        }

        public void DeleteCookie(Cookie cookie)
        {
            Invoker.DoActionWithWait("Add cookie for the page", () => WebDriver.Manage().Cookies.DeleteCookie(cookie));
        }

        public void DeleteAllCookies()
        {
            Invoker.DoActionWithWait("Delete page cookies", () => WebDriver.Manage().Cookies.DeleteAllCookies());
        }

        public void CheckUrl()
        {
            Jdi.Logger.Info($"Checking page url. Url = '{Url}', UrlTemplate = '{UrlTemplate}', CheckType = {CheckUrlType}");
            if (string.IsNullOrEmpty(UrlTemplate) &&
                new[] {CheckPageType.None, CheckPageType.Equal}.Contains(CheckUrlType))
            {
                if (string.IsNullOrEmpty(Url)) return;
                Jdi.Assert.IsTrue(Timer.Wait(() =>
                {
                    Logger.Debug($"Current URL: {WebDriver.Url}");
                    return WebDriver.Url.Equals(Url);
                }));
            }
            else
                switch (CheckUrlType)
                {
                    case CheckPageType.None:
                        Jdi.Assert.IsTrue(Timer.Wait(() =>
                        {
                            Logger.Debug($"Current URL: {WebDriver.Url}");
                            return WebDriver.Url.Contains(UrlTemplate) || WebDriver.Url.Matches(UrlTemplate);
                        }));
                        break;
                    case CheckPageType.Equal:
                        Jdi.Assert.IsTrue(Timer.Wait(() => WebDriver.Url.Equals(Url)));
                        break;
                    case CheckPageType.Match:
                        Jdi.Assert.IsTrue(Timer.Wait(() => WebDriver.Url.Matches(UrlTemplate)));
                        break;
                    case CheckPageType.Contains:
                        Jdi.Assert.IsTrue(Timer.Wait(() => WebDriver.Url.Contains(string.IsNullOrEmpty(UrlTemplate)
                            ? Url
                            : UrlTemplate)));
                        break;
                }
        }

        public void CheckTitle()
        {
            Jdi.Logger.Info($"Checking page title. Title = '{Title}', CheckType = {CheckTitleType}");
            switch (CheckTitleType)
            {
                case CheckPageType.None:
                    Jdi.Assert.IsTrue(Timer.Wait(() =>
                    {
                        Logger.Debug($"Actual: '{WebDriver.Title}', Expected: '{Title}'");
                        return WebDriver.Title.Equals(Title);
                    }));
                    break;
                case CheckPageType.Equal:
                    Jdi.Assert.IsTrue(Timer.Wait(() =>
                    {
                        Logger.Debug($"Actual: '{WebDriver.Title}', Expected: '{Title}'");
                        return WebDriver.Title.Equals(Title);
                    }));
                    break;
                case CheckPageType.Match:
                    Jdi.Assert.IsTrue(Timer.Wait(() =>
                    {
                        Logger.Debug($"Actual: '{WebDriver.Title}', Expected: '{Title}'");
                        return WebDriver.Title.Matches(Title);
                    }));
                    break;
                case CheckPageType.Contains:
                    Jdi.Assert.IsTrue(Timer.Wait(() =>
                    {
                        Logger.Debug($"Actual: '{WebDriver.Title}', Expected: '{Title}'");
                        return WebDriver.Title.Contains(Title);
                    }));
                    break;
            }
        }

        public void CheckOpened()
        {
            CheckUrl();
            CheckTitle();
        }
    }
}