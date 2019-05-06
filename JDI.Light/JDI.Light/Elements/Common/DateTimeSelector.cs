using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class DateTimeSelector : UIElement, IDateTimeSelector
    {
        public string Format { get; set; }

        protected DateTimeSelector(By byLocator) : base(byLocator)
        {
        }
        
        public void SetDateTime(string value, bool checkEnabled = true)
        {
            CheckEnabled(checkEnabled);
            SetAttribute("value", value);
        }

        public void SetDateTime(DateTime dateTime, bool checkEnabled = true)
        {
            CheckEnabled(checkEnabled);
            var value = dateTime.ToString(Format);
            SetAttribute("value", value);
        }
        
        public string Value()
        {
            return GetAttribute("value");
        }

        public string Max()
        {
            return GetAttribute("max");
        }

        public string Min()
        {
            return GetAttribute("min");
        }

        public DateTime GetDateTime()
        {
            return DateTime.ParseExact(GetAttribute("value"), Format,
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}