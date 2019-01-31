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

        protected virtual string GetTextAction()
        {
            var getText = WebElement.Text ?? "";
            if (!getText.Equals(""))
                return getText;
            var getValue = WebElement.GetAttribute("value");
            return getValue ?? getText;
        }

        private Func<string> TextAction() => GetTextAction;

        public string Value => Invoker.DoActionWithResult("Get value", GetTextAction);

        public string GetValue()
        {
            return Value;
        }

        public string GetText()
        {
            return Value;
        }

        public string WaitText(string text)
        {
            return Invoker.DoActionWithResult($"Wait text contains '{text}'", TextAction(), checkResultFunc: t => t.Contains(text));
        }

        public string WaitMatchText(string regEx)
        {
            return Invoker.DoActionWithResult($"Wait text match regex '{regEx}'", TextAction(), checkResultFunc: t => t.Matches(regEx));
        }
    }
}