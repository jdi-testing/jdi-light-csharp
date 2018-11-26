using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Interfaces.Base;
using JDI.Light.Selenium.DriverFactory;
using JDI.Light.Selenium.Elements.WebActions;
using JDI.Light.Settings;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Base
{
    public class UIElement : IBaseUIElement, IVisible
    {
        private IWebElement _webElement;
        public ElementsActions Actions;
        public By FrameLocator;
        public ActionInvoker<UIElement> Invoker;

        public UIElement(By byLocator = null)
        {
            Invoker = new ActionInvoker<UIElement>(this);
            Actions = new ElementsActions(Invoker);
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
                JDISettings.Logger.Debug($"Get Web Element: {ToString()}");
                var element = Timer.GetResultByCondition(() =>
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
                }, el => el != null);
                JDISettings.Logger.Debug("One Web Element found");
                return element;
            }
            set => _webElement = value;
        }

        protected List<IWebElement> GetWebElementsAction()
        {
            var result = Timer.GetResultByCondition(() =>
            {
                var locator = Locator.ContainsRoot() ? Locator.TrimRoot() : Locator;
                return SearchContext.FindElements(locator.CorrectXPath()).ToList();
            }, els => els.Count(GetSearchCriteria) > 0);
            JDISettings.Timeouts.DropTimeouts();
            if (result == null)
                throw JDISettings.Exception("Can't get Web Elements");
            return result.Where(GetSearchCriteria).ToList();
        }

        public ISearchContext SearchContext => Locator.ContainsRoot() 
            ? WebDriver.SwitchTo().DefaultContent() 
            : GetSearchContext(Parent);

        private ISearchContext GetSearchContext(IBaseElement element)
        {
            UIElement el;
            if (element == null || (el = element as UIElement) == null
                                || el.Parent == null && el.FrameLocator == null)
                return WebDriver.SwitchTo().DefaultContent();
            var uiElement = (UIElement) element;
            if (_webElement != null)
                return uiElement.WebElement;
            var locator = el.Locator;
            var searchContext = locator.ContainsRoot()
                ? WebDriver.SwitchTo().DefaultContent()
                : GetSearchContext(el.Parent);
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
            SetWaitTimeout(JDISettings.Timeouts.WaitElementSec);
            return result;
        }

        public bool HasLocator => Locator != null;

        public IJavaScriptExecutor JsExecutor => (IJavaScriptExecutor) WebDriver;

        public IBaseElement Parent { get; set; }

        public string GetAttribute(string name)
        {
            return WebElement.GetAttribute(name);
        }

        public void WaitAttribute(string name, string value)
        {
            var result = Timer.GetResultByCondition(() => WebElement.GetAttribute(name).Equals(value), r => r);
            JDISettings.Asserter.IsTrue(result);
        }

        public void SetAttribute(string attributeName, string value)
        {
            Invoker.DoJAction($"Set Attribute '{attributeName}'='{value}'",
                el => el.JsExecutor.ExecuteScript($"arguments[0].setAttribute('{attributeName}',arguments[1]);",
                    WebElement, value));
        }

        public string Name { get; set; }

        public string TypeName => GetType().Name;

        public void SetWaitTimeout(long mSeconds)
        {
            JDISettings.Logger.Debug("Set wait timeout to " + mSeconds);
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(mSeconds);
            JDISettings.Timeouts.CurrentTimeoutSec = (int) (mSeconds / 1000);
        }

        public new string ToString()
        {
            return JDISettings.ShortLogMessagesFormat
                ? $"{TypeName} '{Name}' ({Parent?.GetType().Name ?? ""}.{Name};)"
                : $"Name: '{Name}', Type: '{TypeName}' In: '{Parent?.GetType().Name ?? ""}'";
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