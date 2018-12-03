using System;
using JDI.Light.Extensions;
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

        private Func<string> TextAction(UIElement el) => () => GetTextFunc(el);

        public ClickableText(By byLocator = null)
            : base(byLocator)
        {
        }

        public string Value => Invoker.DoActionWithResult("Get value", GetTextFunc);

        public string GetValue()
        {
            return Value;
        }

        public string GetText => Invoker.DoActionWithResult("Get text", GetTextFunc);

        public string WaitText(string text)
        {
            return Invoker.DoActionWithResult($"Wait text contains '{text}'",
                el => TextAction(this).GetByCondition(t => t.Contains(text)));
        }

        public string WaitMatchText(string regEx)
        {
            return Invoker.DoActionWithResult($"Wait text match regex '{regEx}'",
                el => TextAction(this).GetByCondition(t => t.Matches(regEx)));
        }
    }
}