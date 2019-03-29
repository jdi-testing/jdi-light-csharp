using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Range : UIElement, IRange
    {
        protected Range(By byLocator) : base(byLocator)
        {
        }

        public void SetRange(string value)
        {
            SetAttribute("value", value);
        }

        public string GetValue()
        {
            return GetAttribute("value");
        }
    }
}