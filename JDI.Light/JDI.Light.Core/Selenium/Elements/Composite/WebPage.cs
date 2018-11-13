using System;
using System.Linq;
using System.Text.RegularExpressions;
using JDI.Core;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Settings;
using JDI.Core.Utils;
using JDI.Web.Selenium.Base;
using JDI.Web.Settings;
using OpenQA.Selenium;

namespace JDI.Web.Selenium.Elements.Composite
{
    public class WebPage : WebBaseElement, IPage
    {
        public static bool CheckAfterOpen = false;
        public static WebPage CurrentPage;
        private string _url;
        protected CheckPageTypes CheckTitleType = CheckPageTypes.None;
        protected CheckPageTypes CheckUrlType = CheckPageTypes.None;

        public string Title;
        protected string UrlTemplate;

        public WebPage()
        {
        }

        public WebPage(string url = null, string title = null)
        {
            Url = url;
            Title = title;
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
                new[] {CheckPageTypes.None, CheckPageTypes.Equal}.Contains(CheckUrlType))
                CheckUrl().Equal();
            else
                switch (CheckUrlType)
                {
                    case CheckPageTypes.None:
                        JDISettings.Asserter.IsTrue(GetUrl().Contains(UrlTemplate)
                                                    || GetUrl().Matches(UrlTemplate));
                        break;
                    case CheckPageTypes.Equal:
                        CheckUrl().Equal();
                        break;
                    case CheckPageTypes.Match:
                        CheckUrl().Match();
                        break;
                    case CheckPageTypes.Contains:
                        CheckUrl().Contains();
                        break;
                }
            switch (CheckTitleType)
            {
                case CheckPageTypes.None:
                    CheckTitle().Equal();
                    break;
                case CheckPageTypes.Equal:
                    CheckTitle().Equal();
                    break;
                case CheckPageTypes.Match:
                    CheckTitle().Match();
                    break;
                case CheckPageTypes.Contains:
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
            CurrentPage = this;
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

        public void UpdatePageData(string url, string title, CheckPageTypes checkUrlType, CheckPageTypes checkTitleType,
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
                && new[] {CheckPageTypes.None, CheckPageTypes.Equal}.Contains(CheckUrlType))
                return url.Equals(Url);
            switch (CheckUrlType)
            {
                case CheckPageTypes.None:
                    return url.Contains(UrlTemplate) || url.Matches(UrlTemplate);
                case CheckPageTypes.Equal:
                    return url.Equals(Url);
                case CheckPageTypes.Match:
                    return url.Matches(UrlTemplate);
                case CheckPageTypes.Contains:
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