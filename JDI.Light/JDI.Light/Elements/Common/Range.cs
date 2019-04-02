using System;
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

        public void SetRange(double value)
        {
            SetAttribute("value", value.ToString());
        }

        public string GetValue()
        {
            return GetAttribute("value");
        }

        public double GetRange()
        {
            return Convert.ToDouble(GetAttribute("value"));
        }
        public double Min()
        {
            return Convert.ToDouble(GetAttribute("min"));
        }

        public double Max()
        {
            return Convert.ToDouble(GetAttribute("max"));
        }
    }
}