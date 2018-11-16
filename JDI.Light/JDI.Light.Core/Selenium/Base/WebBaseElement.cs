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
using JDI.Core.Selenium.Elements.APIInteract;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Selenium.Elements.WebActions;
using JDI.Core.Settings;
using JDI.Core.Utils;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Base
{
    public class WebBaseElement : IBaseElement, IVisible
    {
        private readonly IWebElement _webElement;
        private string _typeName;
        private string _varName;
        public ElementsActions Actions;

        public Functions Function = Functions.None;

        public ActionInvoker Invoker;

        public WebBaseElement(By byLocator = null, IWebElement webElement = null,
            List<IWebElement> webElements = null, WebBaseElement element = null)
        {
            Invoker = new ActionInvoker(this);
            Actions = new ElementsActions(Invoker);
            WebAvatar = new WebAvatar(this, byLocator) {WebElement = webElement, WebElements = webElements};
            _webElement = webElement;
            if (element != null)
            {
                WebAvatar.DriverName = element.WebAvatar.DriverName;
                Parent = element.Parent;
            }
        }

        public By Locator => WebAvatar.ByLocator;
        public By FrameLocator => WebAvatar.FrameLocator;

        public WebAvatar WebAvatar { get; set; }

        private string VarName => _varName ?? Name;

        protected Timer Timer => WebAvatar.Timer;

        public IWebDriver WebDriver => WebAvatar.WebDriver;

        public IWebElement WebElement
        {
            get => _webElement ?? WebAvatar.WebElement;
            set => WebAvatar.WebElement = value;
        }

        public List<IWebElement> WebElements
        {
            get => WebAvatar.WebElements;
            set => WebAvatar.WebElements = value;
        }

        public bool HasLocator => WebAvatar.HasLocator;

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

        public void SetAttribute(string attributeName, string value)
        {
            Invoker.DoJAction($"Set Attribute '{attributeName}'='{value}'",
                el => el.JsExecutor.ExecuteScript($"arguments[0].setAttribute('{attributeName}',arguments[1]);",
                    WebElement, value));
        }

        public IWebElement GetWebElement()
        {
            return Invoker.DoJActionResult("Get web element",
                el => WebElement ?? WebAvatar.WebElement, level: LogLevels.Debug);
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
            WebAvatar.ByLocator = Locator.FillByTemplate(name);
        }

        public WebBaseElement SetAvatar(WebAvatar avatar = null, By byLocator = null)
        {
            WebAvatar = (avatar ?? WebAvatar).Copy(byLocator);
            return this;
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

        public void LogAction(string actionName, LogLevels level)
        {
            JDISettings.ToLog(string.Format(JDISettings.ShortLogMessagesFormat
                ? "{0} for {1}"
                : "Perform action '{0}' with WebElement ({1})", actionName, ToString()), level);
        }

        public void LogAction(string actionName)
        {
            LogAction(actionName, LogLevels.Info);
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
            el => el.WebAvatar.FindImmediately(() => el.WebElement.Displayed, false);

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
    }
}