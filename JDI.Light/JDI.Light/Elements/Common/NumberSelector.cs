using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class NumberSelector: UIElement, INumberSelector
    {
        protected NumberSelector(By byLocator) : base(byLocator)
        {
        }

        public string Placeholder()
        {
            return GetAttribute("placeholder");
        }

        public string Min()
        {
            return GetAttribute("min");
        }

        public string Max()
        {
            return GetAttribute("max");
        }

        public string Value()
        {
            return GetAttribute("value");
        }

        public string Step()
        {
            return GetAttribute("step");
        }

        public void SetNumber(string number)
        {
            Clear();
            SendKeys(number);
        }
    }
}