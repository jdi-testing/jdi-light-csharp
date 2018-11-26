using JDI.Light.Interfaces.Common;
using JDI.Light.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Common
{
    public class Label : ClickableText, ILabel
    {
        public Label() : this(null)
        {
        }

        public Label(By byLocator = null)
            : base(byLocator)
        {
        }
    }
}