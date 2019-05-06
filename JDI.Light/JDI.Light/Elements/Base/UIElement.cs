using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using JDI.Light.Asserts;
using JDI.Light.Elements.WebActions;
using JDI.Light.Exceptions;
using JDI.Light.Factories;
using JDI.Light.Interfaces;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Elements.Base
{
    public class UIElement : IBaseUIElement, IVisible
    {
        private IWebElement _webElement;
        private string _name;

        public By Locator { get; set; }
        public List<By> SmartLocators { get; set; }
        public bool OnlyOneElementAllowedInSearch { get; set; } = true;
        public ActionInvoker Invoker { get; set; }
        public ILogger Logger { get; set; }
        public string DriverName { get; set; } = Jdi.DriverFactory.CurrentDriverName;

        public string Name
        {
            get
            {
                if(_name != null)
                    return _name;
                return _name = GetType().Name;
            }
            set => _name = value;
        }

        public IBaseElement Parent { get; set; }

        public Func<IWebElement, bool> LocalElementSearchCriteria;

        public IJavaScriptExecutor JsExecutor => (IJavaScriptExecutor)WebDriver;
        public IWebDriver WebDriver => Jdi.DriverFactory.GetDriver(DriverName);
        public string TagName => WebElement.TagName;
        public string Text => WebElement.Text;
        public bool Enabled => WebElement.Enabled;
        public bool Selected => WebElement.Selected;
        public Point Location => WebElement.Location;
        public Size Size => WebElement.Size;
        protected Func<UIElement, bool> IsDisplayedAction = el => el.FindImmediately(() => el.WebElement.Displayed, false);
        public bool Displayed => Invoker.DoActionWithResult("Is element displayed", () => FindImmediately(() => WebElement.Displayed, false));
        public bool Hidden => Invoker.DoActionWithResult("Is element hidden", () => !IsDisplayedAction(this));
        public Func<UIElement, bool> WaitDisplayedAction => el => WebElement.Displayed;

        protected UIElement(By byLocator)
        {
            Logger = Jdi.Logger;
            Invoker = new ActionInvoker(Logger, Jdi.Timeouts.WaitElementMSec, Jdi.Timeouts.RetryMSec);
            Locator = byLocator;
        }

        public T Get<T>(By locator, bool onlyOneElementAllowedInSearch = false) where T : IBaseUIElement
        {
            var element = UIElementFactory.CreateInstance<T>(locator, this);
            element.InitMembers();
            element.OnlyOneElementAllowedInSearch = onlyOneElementAllowedInSearch;
            return element;
        }

        public IWebElement WebElement
        {
            get
            {
                Logger.Debug($"Get Web Element: {this}, Locator: {Locator}");
                if (_webElement != null)
                {
                    try
                    {
                        var displayed = _webElement.Displayed;
                        return _webElement;
                    }
                    catch (WebDriverException e)
                    {
                        Logger.Debug($"Element {this} state is invalid, exception message: {e.Message}");
                        _webElement = null;
                    }
                }
                var result = GetWebElements();
                switch (result.Count)
                {
                    case 0:
                        throw Jdi.Assert.Exception($"Can't find Element '{this}' during {Jdi.Timeouts.WaitElementMSec} milliseconds");
                    case 1:
                        Logger.Debug($"One Web Element found: '{this}'");
                        break;
                    default:
                        if (OnlyOneElementAllowedInSearch)
                            throw Jdi.Assert.Exception(
                                $"Find {result.Count} elements instead of one for Element '{this}' during {Jdi.Timeouts.WaitElementMSec} milliseconds");
                        break;
                }
                return _webElement = result[0];
            }
            set => _webElement = value;
        }
        
        public List<IWebElement> WebElements
        {
            get
            {
                Logger.Debug($"Get Web Elements: {this}");
                var elements = GetWebElements();
                Logger.Debug($"Found {elements.Count} elements");
                return elements;
            }
        }

        protected List<IWebElement> GetWebElements()
        {
            var criteria = LocalElementSearchCriteria ?? Jdi.DriverFactory.ElementSearchCriteria;
            var context = GetSearchContext(Parent);
            var result = Invoker.GetResultByCondition(
                () =>
                {
                    if (Locator != null)
                    {
                        return context.FindElements(Locator).ToList();
                    }

                    foreach (var smartLocator in SmartLocators)
                    {
                        var smartElements = context.FindElements(smartLocator).ToList();
                        if (smartElements.Any())
                        {
                            return smartElements;
                        }
                    }
                    return context.FindElements(Locator).ToList();
                }, 
                els => els.Count(criteria) > 0);
            return result.Where(criteria).ToList();
        }

        private ISearchContext GetSearchContext(IBaseElement parent)
        {
            var el = parent as UIElement;
            if (parent == null || el?.Parent == null)
            {
                return WebDriver.SwitchTo().DefaultContent();
            }
            var parentUiElement = (UIElement) parent;
            var locator = parentUiElement.Locator;
            if (locator != null)
                return parentUiElement.WebElement;
            return GetSearchContext(parentUiElement.Parent);
        }
        
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
            SetWaitTimeout(Jdi.Timeouts.WaitElementMSec);
            return result;
        }
        
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
        
        public void SetWaitTimeout(int mSeconds)
        {
            Logger.Debug("Set wait timeout to " + mSeconds);
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(mSeconds);
        }

        public new string ToString()
        {
            return $"Name: '{Name ?? ""}', Type: '{GetType().Name}', Locator: '{Locator}', In: '{Parent?.GetType().Name ?? ""}'";
        }
        
        public void WaitDisplayed()
        {
            Invoker.DoActionWithResult("Wait element displayed", () => WaitDisplayedAction(this));
        }

        public void WaitVanished()
        {
            Invoker.DoActionWithResult("Wait element vanished", () => !IsDisplayedAction(this));
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
        
        protected void CheckEnabled(bool toCheck)
        {
            if (toCheck)
            {
                if (!Enabled)
                {
                    throw new ElementDisabledException(this);
                }
            }
        }

        public IsAssert Is => new IsAssert(this);
        public IsAssert AssertThat => Is;
        public IsAssert Has => Is;
        public IsAssert WaitFor => Is;
        public IsAssert ShouldBe => Is;
    }
}