using System;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
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

        private Func<string> TextAction(UIElement el) => () => GetTextAction(el);

        public string GetText => Invoker.DoActionWithResult("Get text", GetTextAction);

        public string Value => Invoker.DoActionWithResult("Get value", GetTextAction);

        public string GetValue()
        {
            return Value;
        }

        public string WaitText(string text)
        {
            return Invoker.DoActionWithResult($"Wait text contains '{text}'",
                el => TextAction(el).GetByCondition(t => t.Contains(text)));
        }

        public string WaitMatchText(string regEx)
        {
            return Invoker.DoActionWithResult($"Wait text match regex '{regEx}'",
                el => TextAction(this).GetByCondition(t => t.Matches(regEx)));
        }
    }
}