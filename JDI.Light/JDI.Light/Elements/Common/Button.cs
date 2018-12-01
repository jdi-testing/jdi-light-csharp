using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Button : ClickableText, IButton
    {
        public Button()
        {
        }

        public Button(By byLocator) : base(byLocator)
        {
        }
    }
}