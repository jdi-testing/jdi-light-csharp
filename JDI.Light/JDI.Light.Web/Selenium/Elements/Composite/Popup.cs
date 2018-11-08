using System;
using JDI.Core.Attributes.Functions;
using JDI.Core.Interfaces.Complex;
using JDI.Web.Selenium.Base;
using JDI.Web.Selenium.Elements.Common;

namespace JDI.Web.Selenium.Elements.Composite
{
    public class Popup : Text, IPopup
    {
        public Popup() { }

        protected override Func<WebBaseElement, string> GetTextAction => p => GetElementClass.GetTextElement().GetText;

        public void Ok()
        {
            GetElementClass.GetButton(Functions.Ok).Click();
        }

        public void Cancel()
        {
            GetElementClass.GetButton(Functions.Cancel).Click();
        }

        public void Close()
        {
            GetElementClass.GetButton(Functions.Close).Click();
        }
    }
}
