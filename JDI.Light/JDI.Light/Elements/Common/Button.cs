using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Button : TextElement, IButton
    {
        public Button()
        {
        }

        public Button(By byLocator) : base(byLocator)
        {
        }
    }
}