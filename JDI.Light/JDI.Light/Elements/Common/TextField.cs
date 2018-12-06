using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class TextField : Text, ITextField
    {
        protected Func<UIElement, string> GetTextFunc =
            el => el.FindImmediately(() => el.WebElement.GetAttribute("value"), "");
        
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
            Invoker.DoActionWithWait($"Input text '{text}' in text field", () => WebElement.SendKeys(text));
        }

        public new string Value
        {
            get => base.Value;
            set => Invoker.DoActionWithWait("Get value", () => SetValueAction(this, value));
        }

        public void SendKeys(string text)
        {
            Input(text);
        }

        public void Clear()
        {
            Invoker.DoActionWithWait("Clear text field", () => WebElement.Clear());
        }

        public void Focus()
        {
            Invoker.DoActionWithWait("Focus on text field", () => WebElement.Click());
        }
    }
}