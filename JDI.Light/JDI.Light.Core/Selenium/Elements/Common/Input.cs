using OpenQA.Selenium;

namespace JDI.Web.Selenium.Elements.Common
{
    public class Input : TextField
    {
        public Input() : base(null)
        {
        }

        public Input(By byLocator) : base(byLocator)
        {
        }
    }
}