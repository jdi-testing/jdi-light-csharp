using System.Globalization;
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

        public double Min()
        {
            return double.Parse(GetAttribute("min"));
        }

        public double Max()
        {
            return double.Parse(GetAttribute("max"));
        }

        public double Value()
        {
            return double.Parse(GetAttribute("value"));
        }

        public double Step()
        {
            return double.Parse(GetAttribute("step"));
        }

        public void SetNumber(double number)
        {
            Clear();
            SendKeys(number.ToString());
        }
    }
}