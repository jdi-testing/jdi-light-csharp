using System;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Common
{
    public class TextField : Text, ITextField
    {
        protected Action<UIElement> ClearAction = cl => cl.WebElement.Clear();
        protected Action<UIElement> FocusAction = fa => fa.WebElement.Click();

        protected Func<UIElement, string> GetTextFunc =
            el => el.FindImmediately(() => el.WebElement.GetAttribute("value"), "");
        
        protected Action<UIElement, string> InputAction =
            (el, text) => el.WebElement.SendKeys(text);

        protected Action<UIElement, string> SetValueAction = (el, val) =>
            ((TextField) el).NewInput(val);

        public TextField(By byLocator = null)
            : base(byLocator)
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