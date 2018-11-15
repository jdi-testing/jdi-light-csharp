using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Core.Interfaces.Base;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.DriverFactory;
using JDI.Core.Selenium.Elements.Base;
using JDI.Core.Settings;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.APIInteract
{
    public class GetElementModule : IAvatar
    {
        private IWebElement _webElement;
        private List<IWebElement> _webElements;
        public By ByLocator;
        public By FrameLocator;
        public Func<IWebElement, bool> LocalElementSearchCriteria;
        public WebBaseElement RootElement;

        public GetElementModule(WebBaseElement element, By byLocator = null)
        {
            Element = element;
            ByLocator = byLocator;
            if (string.IsNullOrEmpty(DriverName) && WebSettings.WebDriverFactory != null &&
                !string.IsNullOrEmpty(WebSettings.WebDriverFactory.CurrentDriverName))
                DriverName = WebSettings.WebDriverFactory.CurrentDriverName;
        }

        public WebBaseElement Element { get; set; }

        public IWebDriver WebDriver
            => WebSettings.WebDriverFactory.GetDriver(DriverName);

        public Timer Timer => new Timer(JDISettings.Timeouts.CurrentTimeoutSec * 1000);
        public bool HasLocator => ByLocator != null;

        public IWebElement WebElement
        {
            get
            {
                JDISettings.Logger.Debug($"Get Web Element: {Element}");
                var element = Timer.GetResultByCondition(GetWebElementAction, el => el != null);
                JDISettings.Logger.Debug("OneElement found");
                return element;
            }
            set => _webElement = value;
        }

        public List<IWebElement> WebElements
        {
            get
            {
                JDISettings.Logger.Debug($"Get Web Elements: {Element}");
                var elements = GetWebElementsAction();
                JDISettings.Logger.Debug($"Found {elements.Count} elements");
                return elements;
            }
            set => _webElements = value;
        }

        private Func<IWebElement, bool> GetSearchCriteria
            => LocalElementSearchCriteria ?? WebSettings.WebDriverFactory.ElementSearchCriteria;

        public string DriverName { get; set; }

        public GetElementModule Copy(By byLocator)
        {
            var clone = new GetElementModule(Element, byLocator)
            {
                LocalElementSearchCriteria = LocalElementSearchCriteria,
                FrameLocator = FrameLocator,
                RootElement = RootElement,
                DriverName = DriverName,
                Element = Element,
                WebElement = _webElement,
                WebElements = _webElements
            };
            return clone;
        }

        public T FindImmediately<T>(Func<T> func, T ifError)
        {
            Element.SetWaitTimeout(0);
            var temp = LocalElementSearchCriteria;
            LocalElementSearchCriteria = el => true;
            T result;
            try
            {
                result = func.Invoke();
            }
            catch
            {
                result = ifError;
            }

            LocalElementSearchCriteria = temp;
            Element.RestoreWaitTimeout();
            return result;
        }

        private IWebElement GetWebElementAction()
        {
            if (_webElement != null)
                return _webElement;
            var timeout = JDISettings.Timeouts.CurrentTimeoutSec;
            var result = GetWebElementsAction();
            switch (result.Count)
            {
                case 0:
                    throw JDISettings.Exception($"Can't find Element '{Element}' during {timeout} seconds");
                case 1:
                    return result[0];
                default:
                    if (WebDriverFactory.OnlyOneElementAllowedInSearch)
                        throw JDISettings.Exception(
                            $"Find {result.Count} elements instead of one for Element '{Element}' during {timeout} seconds");
                    return result[0];
            }
        }

        private List<IWebElement> GetWebElementsAction()
        {
            if (_webElements != null)
                return _webElements;
            var result = Timer.GetResultByCondition(
                SearchElements,
                els => els.Count(GetSearchCriteria) > 0);
            JDISettings.Timeouts.DropTimeouts();
            if (result == null)
                throw JDISettings.Exception("Can't get Web Elements");
            return result.Where(GetSearchCriteria).ToList();
        }

        private ISearchContext SearchContext(object element)
        {
            WebBaseElement el;
            if (element == null || (el = element as WebBaseElement) == null
                                || el.Parent == null && el.FrameLocator == null)
                return WebDriver.SwitchTo().DefaultContent();
            var elem = element as WebElement;
            if (elem?.WebAvatar._webElement != null)
                return elem.WebElement;
            var locator = el.Locator;
            var searchContext = locator.ContainsRoot()
                ? WebDriver.SwitchTo().DefaultContent()
                : SearchContext(el.Parent);
            locator = locator.ContainsRoot()
                ? locator.TrimRoot()
                : locator;
            var frame = el.WebAvatar.FrameLocator;
            if (frame != null)
                WebDriver.SwitchTo().Frame(WebDriver.FindElement(frame));
            return locator != null
                ? searchContext.FindElement(locator.CorrectXPath())
                : searchContext;
        }

        public GetElementModule SearchAll()
        {
            LocalElementSearchCriteria = el => el != null;
            return this;
        }

        private List<IWebElement> SearchElements()
        {
            var searchContext = ByLocator.ContainsRoot()
                ? WebDriver.SwitchTo().DefaultContent()
                : SearchContext(Element.Parent);
            var locator = ByLocator.ContainsRoot()
                ? ByLocator.TrimRoot()
                : ByLocator;
            return searchContext.FindElements(locator.CorrectXPath()).ToList();
        }
    }
}