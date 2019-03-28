using System.Configuration;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class DateTimeSelector : UIElement, IDateTimeSelector
    {
        protected DateTimeSelector(By byLocator) : base(byLocator)
        {
        }

        public void SetDateTime(string value)
        {
            SetAttribute("value", value);
        }

        public string GetValue()
        {
            return GetAttribute("value");
        }
    }
}