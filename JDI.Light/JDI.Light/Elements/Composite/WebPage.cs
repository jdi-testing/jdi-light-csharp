using System.Linq;
using System.Text.RegularExpressions;
using JDI.Light.Elements.WebActions;
using JDI.Light.Enums;
using JDI.Light.Extensions;
using JDI.Light.Interfaces;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Complex;
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
        public IBaseElement Parent { get; set; }

        public IWebDriver WebDriver { get; set; }
        public Timer Timer { get; set; }

        public WebPage(string url = null, string title = null)
        {
            Url = url;
            Title = title;
            Name = $"{Title} ({Url})";
            WebDriver = JDI.DriverFactory.GetDriver();
            Timer = new Timer(JDI.Timeouts.CurrentTimeoutSec * 1000);
        }

        public string Url
        {
            get => _url == null || _url.Contains("://") || !JDI.HasDomain
                ? _url
                : GetUrlFromUri(_url);
            set => _url = value;
        }

        public void Open()
        {
            Invoker.DoAction($"Open page {Name} by url {Url}",
                () => WebDriver.Navigate().GoToUrl(Url));
            if (CheckAfterOpen)
                CheckOpened();
        }

        public static string GetUrlFromUri(string uri)
        {
            return JDI.Domain.Replace("/*$", "") + "/" + new Regex("^//*").Replace(uri, "");
        }

        public static string GetMatchFromDomain(string uri)
        {
            return JDI.Domain.Replace("/*$", "").Replace(".", "\\.") + "/" + uri.Replace("^/*", "");
        }

        public void UpdatePageData(string url, string title, CheckPageType checkUrlType, CheckPageType checkTitleType,
            string urlTemplate)
        {
            if (_url == null)
                Url = url;
            if (Title == null)
                Title = title;
            CheckUrlType = checkUrlType;
            CheckTitleType = checkTitleType;
            UrlTemplate = urlTemplate;
        }
        
        private bool IsOnPage()
        {
            var url = WebDriver.Url;
            if (string.IsNullOrEmpty(UrlTemplate)
                && new[] {CheckPageType.None, CheckPageType.Equal}.Contains(CheckUrlType))
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

        public void IsOpened()
        {
            ExceptionUtils.ActionWithException(() =>
            {
                if (!IsOnPage())
                    Open();
                JDI.Logger.Info($"Page {Name} is opened");
            }, ex => $"Can't open page {Name}. Reason: {ex}");
        }

        public void Refresh()
        {
            Invoker.DoAction($"Refresh page {Name}", () => WebDriver.Navigate().Refresh());
        }

        public void Back()
        {
            Invoker.DoAction("Go back to previous page", () => WebDriver.Navigate().Back());
        }

        public void Forward()
        {
            Invoker.DoAction("Go forward to next page", () => WebDriver.Navigate().Forward());
        }

        public void AddCookie(Cookie cookie)
        {
            Invoker.DoAction("Add cookie for the page", () => WebDriver.Manage().Cookies.AddCookie(cookie));
        }

        public void DeleteAllCookies()
        {
            Invoker.DoAction("Delete page cookies", () => WebDriver.Manage().Cookies.DeleteAllCookies());
        }

        public void CheckUrl()
        {
            JDI.Logger.Info($"Checking page url. Url = '{Url}', UrlTemplate = '{UrlTemplate}', CheckType = {CheckUrlType}");
            if (string.IsNullOrEmpty(UrlTemplate) &&
                new[] {CheckPageType.None, CheckPageType.Equal}.Contains(CheckUrlType))
            {
                if (string.IsNullOrEmpty(Url)) return;
                JDI.Assert.IsTrue(Timer.Wait(() => WebDriver.Url.Equals(Url)));
            }
            else
                switch (CheckUrlType)
                {
                    case CheckPageType.None:
                        JDI.Assert.IsTrue(WebDriver.Url.Contains(UrlTemplate) || WebDriver.Url.Matches(UrlTemplate));
                        break;
                    case CheckPageType.Equal:
                        JDI.Assert.IsTrue(Timer.Wait(() => WebDriver.Url.Equals(Url)));
                        break;
                    case CheckPageType.Match:
                        JDI.Assert.IsTrue(Timer.Wait(() => WebDriver.Url.Matches(UrlTemplate)));
                        break;
                    case CheckPageType.Contains:
                        JDI.Assert.IsTrue(Timer.Wait(() => WebDriver.Url.Contains(string.IsNullOrEmpty(UrlTemplate)
                            ? Url
                            : UrlTemplate)));
                        break;
                }
        }

        public void CheckTitle()
        {
            JDI.Logger.Info($"Checking page title. Title = '{Title}', CheckType = {CheckTitleType}");
            switch (CheckTitleType)
            {
                case CheckPageType.None:
                    JDI.Assert.IsTrue(Timer.Wait(() => WebDriver.Title.Equals(Title)));
                    break;
                case CheckPageType.Equal:
                    JDI.Assert.IsTrue(Timer.Wait(() => WebDriver.Title.Equals(Title)));
                    break;
                case CheckPageType.Match:
                    JDI.Assert.IsTrue(Timer.Wait(() => WebDriver.Title.Matches(Title)));
                    break;
                case CheckPageType.Contains:
                    JDI.Assert.IsTrue(Timer.Wait(() => WebDriver.Title.Contains(Title)));
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