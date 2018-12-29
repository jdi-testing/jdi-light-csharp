using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using JDI.Light.Elements.WebActions;
using JDI.Light.Factories;
using JDI.Light.Interfaces;
using JDI.Light.Interfaces.Base;
using JDI.Light.Utils;
using OpenQA.Selenium;
using Timer = JDI.Light.Utils.Timer;

namespace JDI.Light.Elements.Base
{
    public class UIElement : IBaseUIElement, IVisible
    {
        private IWebElement _webElement;

        public By FrameLocator;
        public By Locator;

        public IWebDriver WebDriver => JDI.DriverFactory.GetDriver(DriverName);

        public ActionInvoker Invoker { get; set; }
        public ILogger Logger { get; set; }
        public Timer Timer { get; set; }
        public string DriverName { get; set; }

        public UIElement(By byLocator)
        {
            Logger = JDI.Logger;
            Invoker = new ActionInvoker(Logger);
            Locator = byLocator;
            Timer = new Timer(JDI.Timeouts.CurrentTimeoutMSec);
            if (string.IsNullOrEmpty(DriverName) && JDI.DriverFactory != null &&
                !string.IsNullOrEmpty(JDI.DriverFactory.CurrentDriverName))
                DriverName = JDI.DriverFactory.CurrentDriverName;
        }

        public IWebElement WebElement
        {
            get
            {
                JDI.Logger.Debug($"Get Web Element: {ToString()}");
                var element = Timer.GetResultByCondition(() =>
                {
                    if (_webElement != null)
                        return _webElement;
                    var result = GetWebElements();
                    switch (result.Count)
                    {
                        case 0:
                            throw JDI.Assert.Exception($"Can't find Element '{this}' during {JDI.Timeouts.CurrentTimeoutMSec} milliseconds");
                        case 1:
                            return result[0];
                        default:
                            if (WebDriverFactory.OnlyOneElementAllowedInSearch)
                                throw JDI.Assert.Exception(
                                    $"Find {result.Count} elements instead of one for Element '{this}' during {JDI.Timeouts.CurrentTimeoutMSec} milliseconds");
                            return result[0];
                    }
                }, el => el != null);
                JDI.Logger.Debug("One Web Element found");
                return element;
            }
            set => _webElement = value;
        }
        
        public List<IWebElement> WebElements
        {
            get
            {
                JDI.Logger.Debug($"Get Web Elements: {this}");
                var elements = GetWebElements();
                JDI.Logger.Debug($"Found {elements.Count} elements");
                return elements;
            }
        }

        protected List<IWebElement> GetWebElements()
        {
            var result = Timer.GetResultByCondition(() =>
            {
                var locator = Locator.ContainsRoot() ? Locator.TrimRoot() : Locator;
                return SearchContext.FindElements(locator.CorrectXPath()).ToList();
            }, els => els.Count(GetSearchCriteria) > 0);
            if (result == null)
                throw JDI.Assert.Exception("Can't get Web Elements");
            return result.Where(GetSearchCriteria).ToList();
        }

        public ISearchContext SearchContext => Locator.ContainsRoot() 
            ? WebDriver.SwitchTo().DefaultContent() 
            : GetSearchContext(Parent);

        private ISearchContext GetSearchContext(IBaseElement element)
        {
            var el = element as UIElement;
            if (element == null || el == null || (el.Parent == null && el.FrameLocator == null))
            {
                return WebDriver.SwitchTo().DefaultContent();
            }
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
            => LocalElementSearchCriteria ?? JDI.DriverFactory.ElementSearchCriteria;

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
            SetWaitTimeout(JDI.Timeouts.WaitElementSec);
            return result;
        }

        public bool HasLocator => Locator != null;

        public IJavaScriptExecutor JsExecutor => (IJavaScriptExecutor) WebDriver;

        public IBaseElement Parent { get; set; }

        public string GetAttribute(string name)
        {
            return WebElement.GetAttribute(name);
        }

        public string GetProperty(string propertyName)
        {
            return WebElement.GetProperty(propertyName);
        }

        public string GetCssValue(string propertyName)
        {
            return WebElement.GetCssValue(propertyName);
        }

        public string TagName => WebElement.TagName;
        public string Text => WebElement.Text;
        public bool Enabled => WebElement.Enabled;
        public bool Selected => WebElement.Selected;
        public Point Location => WebElement.Location;
        public Size Size => WebElement.Size;

        public void SetAttribute(string attributeName, string value)
        {
            Invoker.DoActionWithWait($"Set Attribute '{attributeName}'='{value}'",
                () => JsExecutor.ExecuteScript($"arguments[0].setAttribute('{attributeName}',arguments[1]);",
                    WebElement, value));
        }

        public void Highlight(string borderColor = "red", string backgroundColor = "yellow", int highlightMillisecondsTime = 1000)
        {
            var originalStyle = GetAttribute("style");
            SetAttribute("style",
                $"border: 3px solid {borderColor}; background-color: {backgroundColor};");
            Thread.Sleep(highlightMillisecondsTime);
            SetAttribute("style", originalStyle);
        }

        public string Name { get; set; }

        public string TypeName => GetType().Name;

        public void SetWaitTimeout(int mSeconds)
        {
            JDI.Logger.Debug("Set wait timeout to " + mSeconds);
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(mSeconds);
            JDI.Timeouts.CurrentTimeoutMSec = mSeconds;
        }

        public new string ToString()
        {
            return $"Name: '{Name}', Type: '{TypeName}' In: '{Parent?.GetType().Name ?? ""}'";
        }
        
        protected Func<UIElement, bool> IsDisplayedAction =
            el => el.FindImmediately(() => el.WebElement.Displayed, false);

        public bool Displayed => Invoker.DoActionWithResult("Is element displayed", () => FindImmediately(() => WebElement.Displayed, false));
        public bool Hidden => Invoker.DoActionWithResult("Is element hidden", () => !IsDisplayedAction(this));
        public Func<UIElement, bool> WaitDisplayedAction => el => WebElement.Displayed;

        public void WaitDisplayed()
        {
            Invoker.DoActionWithResult("Wait element displayed", () => WaitDisplayedAction(this));
        }

        public void WaitVanished()
        {
            Invoker.DoActionWithResult("Wait element vanished", () => Timer.Wait(() => !IsDisplayedAction(this)));
        }

        public void Clear()
        {
            Invoker.DoActionWithWait("Clear an Element", () => WebElement.Clear());
        }

        public void SendKeys(string text)
        {
            Invoker.DoActionWithWait($"Send keys '{text}' into Element", () => WebElement.SendKeys(text));
        }

        public void Submit()
        {
            Invoker.DoActionWithWait("Submit an Element", () => WebElement.Submit());
        }

        public void Click()
        {
            Invoker.DoActionWithWait("Click on Element", () => WebElement.Click());
        }

        public IWebElement FindElement(By by)
        {
            return WebElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return WebElement.FindElements(by);
        }
    }
}