using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Image : UIElement, IImage
    {
        public Image() : this(null)
        {
        }

        public Image(By byLocator = null)
            : base(byLocator)
        {
        }

        public string GetSource()
        {
            return Invoker.DoActionWithResult("Get image source for Element " + this, () => FindImmediately(() => WebElement.GetAttribute("src"), ""));
        }

        public string GetAlt()
        {
            return Invoker.DoActionWithResult("Get image title for Element " + this, () => FindImmediately(() => WebElement.GetAttribute("alt"), ""));
        }
    }
}