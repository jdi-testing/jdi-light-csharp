using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Image : Clickable, IImage
    {
        protected Func<UIElement, string> GetAltFunc =
            el => el.FindImmediately(() => el.WebElement.GetAttribute("alt"), "");

        protected Func<UIElement, string> GetSourceFunc =
            el => el.FindImmediately(() => el.WebElement.GetAttribute("src"), "");

        public Image() : this(null)
        {
        }

        public Image(By byLocator = null)
            : base(byLocator)
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