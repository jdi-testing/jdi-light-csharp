using System;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Base;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Common
{
    public class Text : UIElement, IText
    {
        protected Func<UIElement, string> GetValueFunc = el => ((Text) el).GetTextAction(el);

        public Text() : this(null)
        {
        }

        public Text(By byLocator = null, IWebElement webElement = null, UIElement element = null)
            : base(byLocator, webElement, element: element)
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

        public string GetText => Actions.GetText(GetTextAction);

        public string Value => Actions.GetValue(GetValueFunc);

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