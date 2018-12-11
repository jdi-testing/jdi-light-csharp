using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
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