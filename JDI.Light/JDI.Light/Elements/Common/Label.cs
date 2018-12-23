using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Label : Text, ILabel
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