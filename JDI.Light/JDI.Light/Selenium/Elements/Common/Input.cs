using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Common
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