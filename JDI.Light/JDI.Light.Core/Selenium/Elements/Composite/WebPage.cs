using System;
using System.Linq;
using System.Text.RegularExpressions;
using JDI.Core.Enums;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Base;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Selenium.Elements.WebActions;
using JDI.Core.Settings;
using JDI.Core.Utils;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Composite
{
    public class WebPage : IPage
    {
        public static bool CheckAfterOpen = false;
        private string _url;
        public CheckPageType CheckTitleType { get; set; } = CheckPageType.None;
        public CheckPageType CheckUrlType { get; set; } = CheckPageType.None;

        public string Title { get; set; }
        public string UrlTemplate { get; set; }

        public ActionInvoker<WebPage> Invoker { get; set; }
        public string DriverName { get; set; }
        public string Name { get; set; }
        public IBaseElement Parent { get; set; }

        public IWebDriver WebDriver { get; set; }
        public Timer Timer { get; set; }

        public WebPage(string url = null, string title = null)
        {
            Url = url;
            Title = title;
            Invoker = new ActionInvoker<WebPage>(this);
            Name = $"{Title} ({Url})";
            WebDriver = WebSettings.WebDriverFactory.GetDriver();
            Timer = new Timer(JDISettings.Timeouts.CurrentTimeoutSec * 1000);
        }

        public string Url
        {
            get => _url == null || _url.Contains("://") || !WebSettings.HasDomain
                ? _url
                : GetUrlFromUri(_url);
            set => _url = value;
        }

        public void CheckOpened()
        {
            if (string.IsNullOrEmpty(UrlTemplate) &&
                new[] {CheckPageType.None, CheckPageType.Equal}.Contains(CheckUrlType))
                CheckUrl().Equal();
            else
                switch (CheckUrlType)
                {
                    case CheckPageType.None:
                        JDISettings.Asserter.IsTrue(GetUrl().Contains(UrlTemplate)
                                                    || GetUrl().Matches(UrlTemplate));
                        break;
                    case CheckPageType.Equal:
                        CheckUrl().Equal();
                        break;
                    case CheckPageType.Match:
                        CheckUrl().Match();
                        break;
                    case CheckPageType.Contains:
                        CheckUrl().Contains();
                        break;
                }
            switch (CheckTitleType)
            {
                case CheckPageType.None:
                    CheckTitle().Equal();
                    break;
                case CheckPageType.Equal:
                    CheckTitle().Equal();
                    break;
                case CheckPageType.Match:
                    CheckTitle().Match();
                    break;
                case CheckPageType.Contains:
                    CheckTitle().Contains();
                    break;
            }
        }

        public void Open()
        {
            Invoker.DoJAction($"Open page {Name} by url {Url}",
                el => WebDriver.Navigate().GoToUrl(Url));
            if (CheckAfterOpen)
                CheckOpened();
        }

        public static string GetUrlFromUri(string uri)
        {
            return WebSettings.Domain.Replace("/*$", "") + "/" + new Regex("^//*").Replace(uri, "");
        }

        public static string GetMatchFromDomain(string uri)
        {
            return WebSettings.Domain.Replace("/*$", "").Replace(".", "\\.") + "/" + uri.Replace("^/*", "");
        }

        public static void OpenUrl(string url)
        {
            new WebPage(url).Open();
        }

        public static string GetUrl()
        {
            return WebSettings.WebDriver.Url;
        }

        public static string GetTitle()
        {
            return WebSettings.WebDriver.Title;
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

        public StringCheckType CheckUrl()
        {
            return new StringCheckType(() => WebDriver.Url, Url, UrlTemplate, "url", () => Timer);
        }

        public StringCheckType CheckTitle()
        {
            return new StringCheckType(() => WebDriver.Title, Title, Title, "title", () => Timer);
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
                JDISettings.Logger.Info($"Page {Name} is opened");
            }, ex => $"Can't open page {Name}. Reason: {ex}");
        }

        /**
         * Refresh current page
         */

        public void Refresh()
        {
            Invoker.DoJAction($"Refresh page {Name}",
                el => WebDriver.Navigate().Refresh());
        }

        /**
         * Go back to previous page
         */

        public void Back()
        {
            Invoker.DoJAction("Go back to previous page",
                el => WebDriver.Navigate().Back());
        }

        /**
         * Go forward to next page
         */

        public void Forward()
        {
            Invoker.DoJAction("Go forward to next page",
                el => WebDriver.Navigate().Forward());
        }

        /**
         * @param cookie Specify cookie
         *               Add cookie in browser
         */

        public void AddCookie(Cookie cookie)
        {
            Invoker.DoJAction("Go forward to next page",
                el => WebDriver.Manage().Cookies.AddCookie(cookie));
        }

        /**
         * Clear browsers cache
         */

        public void ClearCache()
        {
            Invoker.DoJAction("Go forward to next page",
                el => WebDriver.Manage().Cookies.DeleteAllCookies());
        }

        public class StringCheckType
        {
            private readonly Func<string> _actual;
            private readonly string _equals;
            private readonly string _template;
            private readonly Func<Timer> _timer;
            private readonly string _what;

            public StringCheckType(Func<string> actual, string equals, string template, string what, Func<Timer> timer)
            {
                _actual = actual;
                _equals = equals;
                _template = template;
                _what = what;
                _timer = timer;
            }

            /**
             * Check that current page url/title equals to expected url/title
             */

            public void Equal()
            {
                if (string.IsNullOrEmpty(_equals)) return;
                JDISettings.Logger.Info($"Page {_what} equals to '{_equals}'");
                JDISettings.Asserter.IsTrue(_timer().Wait(() => _actual().Equals(_equals)));
            }

            /**
             * Check that current page url/title matches to expected url/title-matcher
             */

            public void Match()
            {
                if (string.IsNullOrEmpty(_template)) return;
                JDISettings.Logger.Info($"Page {_what} matches to '{_template}'");
                JDISettings.Asserter.IsTrue(_timer().Wait(() => _actual().Matches(_template)));
            }

            /**
             * Check that current page url/title contains expected url/title-matcher
             */

            public void Contains()
            {
                var url = string.IsNullOrEmpty(_template)
                    ? _equals
                    : _template;
                JDISettings.Logger.Info($"Page {_what} contains to '{url}'");
                JDISettings.Asserter.IsTrue(_timer().Wait(() => _actual().Contains(url)));
            }
        }
    }
}