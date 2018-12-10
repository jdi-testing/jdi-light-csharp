using System;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Elements.Base
{
    public class ClickableText : Clickable, IText
    {
        protected string GetTextFunc()
        {
            var getText = WebElement.Text ?? "";
            if (!getText.Equals(""))
                return getText;
            var getValue = WebElement.GetAttribute("value");
            return getValue ?? getText;
        }

        private Func<string> TextAction() => GetTextFunc;
        
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
                () => TextAction().GetByCondition(t => t.Contains(text)));
        }

        public string WaitMatchText(string regEx)
        {
            return Invoker.DoActionWithResult($"Wait text match regex '{regEx}'",
                () => TextAction().GetByCondition(t => t.Matches(regEx)));
        }
    }
}