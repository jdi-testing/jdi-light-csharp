using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Core.Attributes;
using JDI.Core.Attributes.Functions;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Base;
using JDI.Core.Interfaces.Common;
using JDI.Core.Logging;
using JDI.Core.Selenium.DriverFactory;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Selenium.Elements.WebActions;
using JDI.Core.Settings;
using JDI.Core.Utils;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Base
{
    public class WebBaseElement : IBaseElement, IVisible
    {
        private IWebElement _webElement;
        private List<IWebElement> _webElements;
        private string _typeName;
        private string _varName;
        public ElementsActions Actions;
        public By FrameLocator;

        public Functions Function = Functions.None;

        public ActionInvoker<WebBaseElement> Invoker;

        public WebBaseElement(By byLocator = null, IWebElement webElement = null,
            List<IWebElement> webElements = null, WebBaseElement element = null)
        {
            Invoker = new ActionInvoker<WebBaseElement>(this);
            Actions = new ElementsActions(Invoker);
            _webElement = webElement;
            _webElements = webElements;
            Locator = byLocator;
            if (element != null)
            {
                Parent = element.Parent;
            }
            Timer = new Timer(JDISettings.Timeouts.CurrentTimeoutSec * 1000);
            if (string.IsNullOrEmpty(DriverName) && WebSettings.WebDriverFactory != null &&
                !string.IsNullOrEmpty(WebSettings.WebDriverFactory.CurrentDriverName))
                DriverName = WebSettings.WebDriverFactory.CurrentDriverName;
        }

        public Timer Timer { get; set; }

        public By Locator;

        private string VarName => _varName ?? Name;

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

        public WebBaseElement SearchAll()
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
        
        private List<IWebElement> SearchElements()
        {
            var searchContext = Locator.ContainsRoot()
                ? WebDriver.SwitchTo().DefaultContent()
                : SearchContext(this.Parent);
            var locator = Locator.ContainsRoot()
                ? Locator.TrimRoot()
                : Locator;
            return searchContext.FindElements(locator.CorrectXPath()).ToList();
        }
        
        private ISearchContext SearchContext(object element)
        {
            WebBaseElement el;
            if (element == null || (el = element as WebBaseElement) == null
                                || el.Parent == null && el.FrameLocator == null)
                return WebDriver.SwitchTo().DefaultContent();
            var elem = element as WebBaseElement;
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
            set => _webElements = value;
        }

        public bool HasLocator => Locator != null;

        public IJavaScriptExecutor JsExecutor => (IJavaScriptExecutor) WebDriver;

        public object Parent { get; set; }

        public void SetFunction(Functions function)
        {
            Function = function;
        }

        public string GetAttribute(string name)
        {
            return GetWebElement().GetAttribute(name);
        }

        public void WaitAttribute(string name, string value)
        {
            Wait(el => el.GetAttribute(name).Equals(value));
        }

        /**
         * @param resultFunc Specify expected function result
         * Waits while condition with WebElement happens during specified timeout and returns result using resultFunc
         */
        public void Wait(Func<IWebElement, bool> resultFunc)
        {
            var result = Wait(resultFunc, r => r);
            JDISettings.Asserter.IsTrue(result);
        }

        /**
         * @param resultFunc Specify expected function result
         * @param condition  Specify expected function condition
         * @return Waits while condition with WebElement happens and returns result using resultFunc
         */
        public T Wait<T>(Func<IWebElement, T> resultFunc, Func<T, bool> condition)
        {
            return Timer.GetResultByCondition(() => resultFunc.Invoke(GetWebElement()), condition.Invoke);
        }
        
        /**
         * @param resultFunc Specify expected function result
         * @param timeoutSec Specify timeout
         * Waits while condition with WebElement happens during specified timeout and returns wait result
         */
        public void Wait(Func<IWebElement, bool> resultFunc, int timeoutSec)
        {
            var result = Wait(resultFunc, r => r, timeoutSec);
            JDISettings.Asserter.IsTrue(result);
        }

        /**
         * @param resultFunc Specify expected function result
         * @param timeoutSec Specify timeout
         * @param condition  Specify expected function condition
         * @return Waits while condition with WebElement and returns wait result
         */
        public T Wait<T>(Func<IWebElement, T> resultFunc, Func<T, bool> condition, int timeoutSec)
        {
            SetWaitTimeout(timeoutSec);
            var result =
                new Timer(timeoutSec).GetResultByCondition(() => resultFunc.Invoke(GetWebElement()), condition.Invoke);
            RestoreWaitTimeout();
            return result;
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

        public string TypeName
        {
            get => _typeName ?? GetType().Name;
            set => _typeName = value;
        }

        public void SetName(FieldInfo field)
        {
            Name = NameAttribute.GetElementName(field);
            _varName = field.Name;
        }

        public void FillLocatorTemplate(string name)
        {
            Locator = Locator.FillByTemplate(name);
        }

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
                ? $"{TypeName} '{Name}' ({ParentTypeName}.{VarName};)"
                : $"Name: '{Name}', Type: '{TypeName}' In: '{ParentTypeName}'";
        }

        private string ToButton(string buttonName)
        {
            return buttonName.ToLower().Contains("button") ? buttonName : buttonName + "Button";
        }

        public Button GetButton(string buttonName)
        {
            var fields = _webElement.GetFields(typeof(IButton));
            switch (fields.Count)
            {
                case 0:
                    throw JDISettings.Exception($"Can't find ny buttons on form {ToString()}'");
                case 1:
                    return (Button)fields[0].GetValue(_webElement);
                default:
                    var buttons = fields.Select(f => (Button)f.GetValue(_webElement)).ToList();
                    var button = buttons.FirstOrDefault(b => ToButton(b.Name).SimplifiedEqual(ToButton(buttonName)));
                    if (button == null)
                        throw JDISettings.Exception($"Can't find button '{buttonName}' for Element '{ToString()}'." +
                                                    $"(Found following buttons: {buttons.Select(el => el.Name).Print()})."
                                                        .FromNewLine());
                    return button;
            }
        }

        public Button GetButton(Functions funcName)
        {
            var fields = _webElement.GetFields(typeof(IButton));
            if (fields.Count == 1)
                return (Button)fields[0].GetValue(_webElement);
            var buttons = fields.Select(f => (Button)f.GetValue(_webElement)).ToList();
            var button = buttons.FirstOrDefault(b => b.Function.Equals(funcName));
            if (button != null) return button;
            var name = funcName.ToString();
            button = buttons.FirstOrDefault(b => ToButton(b.Name).SimplifiedEqual(ToButton(name)));
            if (button == null)
                throw JDISettings.Exception($"Can't find button '{name}' for Element '{ToString()}'");
            return button;
        }

        public Text GetTextElement()
        {
            var textField = this.GetFirstField(typeof(Text), typeof(IText));
            if (textField == null)
                throw JDISettings.Exception($"Can't find Text Element '{ToString()}'");
            return (Text)textField.GetValue(_webElement);
        }

        protected Func<WebBaseElement, bool> IsDisplayedAction =
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