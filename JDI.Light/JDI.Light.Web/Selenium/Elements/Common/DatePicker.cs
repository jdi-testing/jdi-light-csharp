using JDI.Core.Interfaces.Common;
using JDI.Web.Selenium.Base;
using OpenQA.Selenium;

namespace JDI.Web.Selenium.Elements.Common
{
    public class DatePicker : TextField, IDatePicker
    {
        public DatePicker() : this(null)
        {
        }

        public DatePicker(By byLocator = null, IWebElement webElement = null, WebBaseElement element = null)
            : base(byLocator, webElement, element)
        {
        }
    }
}