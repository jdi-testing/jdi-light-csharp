﻿using System.Linq;
using System.Text.RegularExpressions;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.WebActions;
using JDI.Light.Enums;
using JDI.Light.Extensions;
using JDI.Light.Factories;
using JDI.Light.Interfaces;
using JDI.Light.Interfaces.Composite;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class WebPage : IPage
    {
        public bool CheckAfterOpen = false;
        private string _url;
        public CheckPageType CheckTitleType { get; set; } = CheckPageType.None;
        public CheckPageType CheckUrlType { get; set; } = CheckPageType.None;
        public string Title { get; set; }
        public string UrlTemplate { get; set; }
        public ActionInvoker Invoker { get; set; }
        public ILogger Logger { get; set; }
        public string DriverName { get; set; } = Jdi.DriverFactory.CurrentDriverName;
        public string Name { get; set; }
        public IWebSite Parent { get; set; }
        public IWebDriver WebDriver => Jdi.DriverFactory.GetDriver(DriverName);

        protected WebPage()
        {
            Logger = Jdi.Logger;
            Invoker = new ActionInvoker(Logger, Jdi.Timeouts.WaitPageLoadMSec, Jdi.Timeouts.RetryMSec);
            Name = $"{Title} ({Url})";
        }
        
        public T Get<T>(By locator) where T : UIElement
        {
            var element = UIElementFactory.CreateInstance<T>(locator, this);
            element.InitMembers();
            return element;
        }

        public static string PageUrl => Jdi.WebDriver.Url;
        public static string PageTitle => Jdi.WebDriver.Title;

        public string Url
        {
            get => _url == null || _url.Contains("://") || !Parent.HasDomain
                ? _url
                : new Regex("//*$").Replace(Parent.Domain, "") + "/" + new Regex("^//*").Replace(_url, "");
            set => _url = value;
        }

        public Form<T> AsForm<T>()
        {
            var form = new Form<T>().SetPageObject(this);
            form.Name = Name + " Form";
            return form;
        }

        public void Open()
        {
            Invoker.DoActionWithWait($"Open page {Name} by url {Url}",
                () => WebDriver.Navigate().GoToUrl(Url));
            if (CheckAfterOpen)
            {
                CheckOpened();
            }
        }

        public bool IsOpened
        {
            get
            {
                var url = WebDriver.Url;
                if (string.IsNullOrEmpty(UrlTemplate)
                    && new[] {CheckPageType.None, CheckPageType.Equal}.Contains(CheckUrlType))
                {
                    return url.Equals(Url);
                }
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
                    default: break;
                }

                return false;
            }
        }

        public INavigation Navigation => WebDriver.Navigate();

        public void Refresh()
        {
            Invoker.DoActionWithWait($"Refresh page {Name}", () => Navigation.Refresh());
        }

        public void Back()
        {
            Invoker.DoActionWithWait("Go back to previous page", () => Navigation.Back());
        }

        public void Forward()
        {
            Invoker.DoActionWithWait("Go forward to next page", () => Navigation.Forward());
        }

        public ICookieJar Cookies => WebDriver.Manage().Cookies;

        public void AddCookie(Cookie cookie)
        {
            Invoker.DoActionWithWait("Add cookie for the page", () => Cookies.AddCookie(cookie));
        }

        public void DeleteCookie(Cookie cookie)
        {
            Invoker.DoActionWithWait("Add cookie for the page", () => Cookies.DeleteCookie(cookie));
        }

        public void DeleteAllCookies()
        {
            Invoker.DoActionWithWait("Delete page cookies", () => Cookies.DeleteAllCookies());
        }

        public void CheckUrl()
        {
            Jdi.Logger.Info($"Checking page url. Url = '{Url}', UrlTemplate = '{UrlTemplate}', CheckType = {CheckUrlType}");
            if (string.IsNullOrEmpty(UrlTemplate) &&
                new[] {CheckPageType.None, CheckPageType.Equal}.Contains(CheckUrlType))
            {
                if (string.IsNullOrEmpty(Url))
                {
                    return;
                }
                Jdi.Assert.IsTrue(Invoker.Wait("Checking that URL matches the template", () =>
                {
                    Logger.Debug($"Current URL: {WebDriver.Url}");
                    return WebDriver.Url.Equals(Url);
                }));
            }
            else
            {
                switch (CheckUrlType)
                {
                    case CheckPageType.None:
                        Jdi.Assert.IsTrue(Invoker.Wait("Checking that URL matches the template", () =>
                        {
                            Logger.Debug($"Current URL: {WebDriver.Url}");
                            return WebDriver.Url.Contains(UrlTemplate) || WebDriver.Url.Matches(UrlTemplate);
                        }));
                        break;
                    case CheckPageType.Equal:
                        Jdi.Assert.IsTrue(Invoker.Wait("Checking that URL equals the expected value", () => WebDriver.Url.Equals(Url)));
                        break;
                    case CheckPageType.Match:
                        Jdi.Assert.IsTrue(Invoker.Wait("Checking that URL matches the template", () => WebDriver.Url.Matches(UrlTemplate)));
                        break;
                    case CheckPageType.Contains:
                        Jdi.Assert.IsTrue(Invoker.Wait("Checking that URL contains the template", () => WebDriver.Url.Contains(string.IsNullOrEmpty(UrlTemplate)
                            ? Url
                            : UrlTemplate)));
                        break;
                }
            }
        }

        public void CheckTitle()
        {
            Jdi.Logger.Info($"Checking page title. Title = '{Title}', CheckType = {CheckTitleType}");
            switch (CheckTitleType)
            {
                case CheckPageType.None:
                    Jdi.Assert.IsTrue(Invoker.Wait("Checking that page title equals to expected", () =>
                    {
                        Logger.Debug($"Actual: '{WebDriver.Title}', Expected: '{Title}'");
                        return WebDriver.Title.Equals(Title);
                    }));
                    break;
                case CheckPageType.Equal:
                    Jdi.Assert.IsTrue(Invoker.Wait("Checking that page title equals to expected", () =>
                    {
                        Logger.Debug($"Actual: '{WebDriver.Title}', Expected: '{Title}'");
                        return WebDriver.Title.Equals(Title);
                    }));
                    break;
                case CheckPageType.Match:
                    Jdi.Assert.IsTrue(Invoker.Wait("Checking that page title matches expected", () =>
                    {
                        Logger.Debug($"Actual: '{WebDriver.Title}', Expected: '{Title}'");
                        return WebDriver.Title.Matches(Title);
                    }));
                    break;
                case CheckPageType.Contains:
                    Jdi.Assert.IsTrue(Invoker.Wait("Checking that page title contains expected", () =>
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

        public Alert GetAlert()
        {
            var alert = new Alert
            {
                Parent = this
            };
            return alert;
        }
    }
}