using System;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Common
{
    public class Button : ClickableText, IButton
    {
        protected new Func<UIElement, string> GetTextFunc =
            el => el.FindImmediately(() => el.WebElement.GetAttribute("value"), "");

        public Button() : this(null)
        {
        }

        public Button(By byLocator = null, IWebElement webElement = null, UIElement element = null)
            : base(byLocator, webElement, element)
        {
        }
    }
}