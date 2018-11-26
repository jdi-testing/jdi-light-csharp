using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Common
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