using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Button : Text, IButton
    {
        public Button()
        {
        }

        public Button(By byLocator) : base(byLocator)
        {
        }
    }
}