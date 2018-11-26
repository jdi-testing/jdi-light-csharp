using JDI.Light.Interfaces.Common;
using JDI.Light.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Common
{
    public class Button : ClickableText, IButton
    {
        public Button(By byLocator = null)
            : base(byLocator)
        {
        }
    }
}