using System;
using System.Globalization;
using JDI.Light.Asserts;
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
        private IFormatProvider format = CultureInfo.InvariantCulture;
        public string Placeholder => GetAttribute("placeholder");

        public double Min => double.Parse(GetAttribute("min"), format);

        public double Max => double.Parse(GetAttribute("max"), format);

        public double Value => double.Parse(GetAttribute("value"), format);

        public double Step => double.Parse(GetAttribute("step"), format);

        public void SetNumber(double number, bool checkEnabled = true)
        {
            CheckEnabled(checkEnabled);
            Clear();
            SendKeys(number.ToString(format));
        }

        public new NumberAssert Is() => new NumberAssert(this);
        public new NumberAssert AssertThat() => Is();
    }
}