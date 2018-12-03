using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
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
            Invoker.DoAction($"Input text '{text}' in text field", el => InputAction(this, text));
        }

        public new string Value
        {
            get => base.Value;
            set => Invoker.DoAction("Get value", el => SetValueAction(this, value));
        }

        public void SendKeys(string text)
        {
            Input(text);
        }

        public void Clear()
        {
            Invoker.DoAction("Clear text field", ClearAction);
        }

        public void Focus()
        {
            Invoker.DoAction("Focus on text field", FocusAction);
        }
    }
}