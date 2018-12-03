using System;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Base
{
    public class ClickableText : Clickable, IText
    {
        protected Func<UIElement, string> GetTextFunc =
            el =>
            {
                var getText = el.WebElement.Text ?? "";
                if (!getText.Equals(""))
                    return getText;
                var getValue = el.WebElement.GetAttribute("value");
                return getValue ?? getText;
            };

        public ClickableText(By byLocator = null)
            : base(byLocator)
        {
        }

        public string Value => Actions.GetValue(GetTextFunc);

        public string GetValue()
        {
            return Value;
        }

        public string GetText => Invoker.DoActionWithResult("Get text", GetTextFunc);

        public string WaitText(string text)
        {
            return Actions.WaitText(text, GetTextFunc);
        }

        public string WaitMatchText(string regEx)
        {
            return Actions.WaitMatchText(regEx, GetTextFunc);
        }
    }
}