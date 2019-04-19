using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class ProgressBar : UIElement, IProgressBar
    {
        protected ProgressBar(By byLocator) : base(byLocator)
        {
        }

        public string Value()
        {
            return GetAttribute("value");
        }

        public string Max()
        {
            return GetAttribute("max");
        }
    }
}