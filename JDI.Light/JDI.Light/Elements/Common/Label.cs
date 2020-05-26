using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Label : TextElement, ILabel
    {
        public Label() : this(null)
        {
        }

        public Label(By byLocator)
            : base(byLocator)
        {
        }
    }
}