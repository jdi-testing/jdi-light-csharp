using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Text : UIElement, IText
    {
        public Text() : this(null)
        {
        }

        public Text(By byLocator = null)
            : base(byLocator)
        {
        }

        protected virtual Func<UIElement, string> GetTextAction { get; set; } = el =>
        {
            var getText = el.WebElement.Text ?? "";
            if (!getText.Equals(""))
                return getText;
            var getValue = el.WebElement.GetAttribute("value");
            return getValue ?? getText;
        };

        public string GetText => Invoker.DoActionWithResult("Get text", GetTextAction);

        public string Value => Invoker.DoActionWithResult("Get value", GetTextAction);

        public string GetValue()
        {
            return Value;
        }

        public string WaitText(string text)
        {
            return Actions.WaitText(text, GetTextAction);
        }

        public string WaitMatchText(string regEx)
        {
            return Actions.WaitMatchText(regEx, GetTextAction);
        }
    }
}