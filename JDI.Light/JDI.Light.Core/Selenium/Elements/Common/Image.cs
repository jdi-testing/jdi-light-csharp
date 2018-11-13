using System;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Common
{
    public class Image : Clickable, IImage
    {
        protected Func<WebBaseElement, string> GetAltFunc =
            el => el.WebAvatar.FindImmediately(() => el.WebElement.GetAttribute("alt"), "");

        protected Func<WebBaseElement, string> GetSourceFunc =
            el => el.WebAvatar.FindImmediately(() => el.WebElement.GetAttribute("src"), "");

        public Image() : this(null)
        {
        }

        public Image(By byLocator = null, IWebElement webElement = null, WebBaseElement element = null)
            : base(byLocator, webElement, element)
        {
        }

        public string GetSource()
        {
            return Invoker.DoJActionResult("Get image source for Element " + this, GetSourceFunc);
        }

        public string GetAlt()
        {
            return Invoker.DoJActionResult("Get image title for Element " + this, GetAltFunc);
        }
    }
}