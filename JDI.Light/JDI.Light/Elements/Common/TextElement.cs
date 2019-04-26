using System;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class TextElement : UIElement, ITextElement
    {
        public TextElement() : this(null)
        {
        }

        public TextElement(By byLocator = null)
            : base(byLocator)
        {
        }

        protected virtual string GetTextAction()
        {
            var e = WebElement;
            var getText = e.Text ?? "";
            if (!getText.Equals(""))
                return getText;
            var getValue = e.GetAttribute("value");
            return getValue ?? getText;
        }

        private Func<string> TextAction() => GetTextAction;

        public string Value => Invoker.DoActionWithResult("Get value", GetTextAction);

        public virtual string GetValue()
        {
            return Value;
        }

        public virtual string GetText()
        {
            return Value;
        }

        public new string Text => GetText();

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