using System;
using JDI.Core.Attributes.Functions;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.Elements.Common;

namespace JDI.Core.Selenium.Elements.Composite
{
    public class Popup : Text, IPopup
    {
        protected override Func<WebBaseElement, string> GetTextAction => p => GetTextElement().GetText;

        public void Ok()
        {
            GetButton(Functions.Ok).Click();
        }

        public void Cancel()
        {
            GetButton(Functions.Cancel).Click();
        }

        public void Close()
        {
            GetButton(Functions.Close).Click();
        }
    }
}