using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Common
{
    public class DatePicker : TextField, IDatePicker
    {
        public DatePicker() : this(null)
        {
        }

        public DatePicker(By byLocator = null)
            : base(byLocator)
        {
        }
    }
}