using System;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Base;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Common
{
    public class TextField : Text, ITextField
    {
        protected Action<WebBaseElement> ClearAction = cl => cl.WebElement.Clear();
        protected Action<WebBaseElement> FocusAction = fa => fa.WebElement.Click();

        protected Func<WebBaseElement, string> GetTextFunc =
            el => el.WebAvatar.FindImmediately(() => el.WebElement.GetAttribute("value"), "");
        
        protected Action<WebBaseElement, string> InputAction =
            (el, text) => el.WebElement.SendKeys(text);

        protected Action<WebBaseElement, string> SetValueAction = (el, val) =>
            ((TextField) el).NewInput(val);

        public TextField() : this(null)
        {
        }

        public TextField(By byLocator = null, IWebElement webElement = null, WebBaseElement element = null)
            : base(byLocator, webElement, element)
        {
        }

        public void NewInput(string text)
        {
            Clear();
            Input(text);
        }

        public void Input(string text)
        {
            Actions.Input(text, InputAction);
        }

        public new string Value
        {
            get => base.Value;
            set => Actions.SetValue(value, SetValueAction);
        }

        public void SendKeys(string text)
        {
            Input(text);
        }

        public void Clear()
        {
            Actions.Clear(ClearAction);
        }

        public void Focus()
        {
            Actions.Focus(FocusAction);
        }
    }
}