using System;
using System.Globalization;
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
        
        public void SetVolume(string value)
        {
            SetAttribute("value", value);
        }

        public void SetVolume(double value)
        {
            SetAttribute("value", value.ToString(CultureInfo.InvariantCulture));
        }

        public string GetValue()
        {
            return GetAttribute("value");
        }

        public double Volume()
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
        public double Step()
        {
            return Convert.ToDouble(GetAttribute("step"));
        }
    }
}