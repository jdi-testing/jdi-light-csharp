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

        public void SetRange(int value)
        {
            SetAttribute("value", value.ToString());
        }

        public string GetValue()
        {
            return GetAttribute("value");
        }

        public int GetRange()
        {
            return Convert.ToInt32(GetAttribute("value"));
        }
        public int Min()
        {
            return Convert.ToInt32(GetAttribute("min"));
        }

        public int Max()
        {
            return Convert.ToInt32(GetAttribute("max"));
        }
    }
}