using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Core.Interfaces.Base;
using JDI.Core.Logging;
using JDI.Core.Selenium.DriverFactory;
using JDI.Core.Selenium.Elements.WebActions;
using JDI.Core.Settings;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Base
{
    public class UIElement : IBaseElement, IVisible
    {
        private IWebElement _webElement;
        public ElementsActions Actions;
        public By FrameLocator;
        public ActionInvoker<UIElement> Invoker;

        public UIElement(By byLocator = null, IWebElement webElement = null,
            List<IWebElement> webElements = null)
        {
            Invoker = new ActionInvoker<UIElement>(this);
            Actions = new ElementsActions(Invoker);
            _webElement = webElement;
            Locator = byLocator;
            Timer = new Timer(JDISettings.Timeouts.CurrentTimeoutSec * 1000);
            if (string.IsNullOrEmpty(DriverName) && WebSettings.WebDriverFactory != null &&
                !string.IsNullOrEmpty(WebSettings.WebDriverFactory.CurrentDriverName))
                DriverName = WebSettings.WebDriverFactory.CurrentDriverName;
        }

        public Timer Timer { get; set; }

        public By Locator;
        
        public IWebDriver WebDriver
            => WebSettings.WebDriverFactory.GetDriver(DriverName);

        public string DriverName { get; set; }

        public IWebElement WebElement
        {
            get
            {
                JDISettings.Logger.Debug($"Get Web Element: {this}");
                var element = Timer.GetResultByCondition(GetWebElementAction, el => el != null);
                JDISettings.Logger.Debug("OneElement found");
                return element;
            }
            set => _webElement = value;
        }

        public UIElement SearchAll()
        {
            LocalElementSearchCriteria = el => el != null;
            return this;
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
                    throw JDISettings.Exception($"Can't find Element '{this}' during {timeout} seconds");
                case 1:
                    return result[0];
                default:
                    if (WebDriverFactory.OnlyOneElementAllowedInSearch)
                        throw JDISettings.Exception(
                            $"Find {result.Count} elements instead of one for Element '{this}' during {timeout} seconds");
                    return result[0];
            }
        }

        public Func<IWebElement, bool> LocalElementSearchCriteria;

        private Func<IWebElement, bool> GetSearchCriteria
            => LocalElementSearchCriteria ?? WebSettings.WebDriverFactory.ElementSearchCriteria;

        public T FindImmediately<T>(Func<T> func, T ifError)
        {
            SetWaitTimeout(0);
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
            RestoreWaitTimeout();
            return result;
        }

        private List<IWebElement> GetWebElementsAction()
        {
            var result = Timer.GetResultByCondition(SearchElements, els => els.Count(GetSearchCriteria) > 0);
            JDISettings.Timeouts.DropTimeouts();
            if (result == null)
                throw JDISettings.Exception("Can't get Web Elements");
            return result.Where(GetSearchCriteria).ToList();
        }
        
        private List<IWebElement> SearchElements()
        {
            var searchContext = Locator.ContainsRoot() ? WebDriver.SwitchTo().DefaultContent() : SearchContext(Parent);
            var locator = Locator.ContainsRoot() ? Locator.TrimRoot(): Locator;
            return searchContext.FindElements(locator.CorrectXPath()).ToList();
        }
        
        private ISearchContext SearchContext(object element)
        {
            UIElement el;
            if (element == null || (el = element as UIElement) == null
                                || el.Parent == null && el.FrameLocator == null)
                return WebDriver.SwitchTo().DefaultContent();
            var elem = element as UIElement;
            if (_webElement != null)
                return elem.WebElement;
            var locator = el.Locator;
            var searchContext = locator.ContainsRoot()
                ? WebDriver.SwitchTo().DefaultContent()
                : SearchContext(el.Parent);
            locator = locator.ContainsRoot()
                ? locator.TrimRoot()
                : locator;
            var frame = el.FrameLocator;
            if (frame != null)
                WebDriver.SwitchTo().Frame(WebDriver.FindElement(frame));
            return locator != null
                ? searchContext.FindElement(locator.CorrectXPath())
                : searchContext;
        }

        public List<IWebElement> WebElements
        {
            get
            {
                JDISettings.Logger.Debug($"Get Web Elements: {this}");
                var elements = GetWebElementsAction();
                JDISettings.Logger.Debug($"Found {elements.Count} elements");
                return elements;
            }
        }

        public bool HasLocator => Locator != null;

        public IJavaScriptExecutor JsExecutor => (IJavaScriptExecutor) WebDriver;

        public object Parent { get; set; }

        public string GetAttribute(string name)
        {
            return GetWebElement().GetAttribute(name);
        }

        public void WaitAttribute(string name, string value)
        {
            var result = Timer.GetResultByCondition(() => GetWebElement().GetAttribute(name).Equals(value), r => r);
            JDISettings.Asserter.IsTrue(result);
        }

        public void SetAttribute(string attributeName, string value)
        {
            Invoker.DoJAction($"Set Attribute '{attributeName}'='{value}'",
                el => el.JsExecutor.ExecuteScript($"arguments[0].setAttribute('{attributeName}',arguments[1]);",
                    WebElement, value));
        }

        public IWebElement GetWebElement()
        {
            return Invoker.DoJActionResult("Get web element",
                el => WebElement, level: LogLevels.Debug);
        }

        public string Name { get; set; }
        public string ParentTypeName => Parent?.GetType().Name ?? "";

        public string TypeName => GetType().Name;

        public void SetWaitTimeout(long mSeconds)
        {
            JDISettings.Logger.Debug("Set wait timeout to " + mSeconds);
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(mSeconds);
            JDISettings.Timeouts.CurrentTimeoutSec = (int) (mSeconds / 1000);
        }

        public void RestoreWaitTimeout()
        {
            SetWaitTimeout(JDISettings.Timeouts.WaitElementSec);
        }

        public new string ToString()
        {
            return JDISettings.ShortLogMessagesFormat
                ? $"{TypeName} '{Name}' ({ParentTypeName}.{Name};)"
                : $"Name: '{Name}', Type: '{TypeName}' In: '{ParentTypeName}'";
        }
        
        protected Func<UIElement, bool> IsDisplayedAction =
            el => el.FindImmediately(() => el.WebElement.Displayed, false);

        public bool Displayed => Actions.IsDisplayed(IsDisplayedAction);
        public bool Hidden => Actions.IsDisplayed(el => !IsDisplayedAction(el));

        public void WaitDisplayed()
        {
            Actions.WaitDisplayed(el => WebElement.Displayed);
        }

        public void WaitVanished()
        {
            Actions.WaitVanished(el => Timer.Wait(() => !IsDisplayedAction(el)));
        }

        public void Highlight()
        {
            WebSettings.WebDriverFactory.Highlight(this);
        }

        public void Highlight(HighlightSettings highlightSettings)
        {
            WebSettings.WebDriverFactory.Highlight(this, highlightSettings);
        }
    }
}