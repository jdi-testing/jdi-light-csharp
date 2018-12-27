using JDI.Light.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Section : CompositeUIElement
    {
        public Section() : base(null)
        {
        }

        public Section(By locator) : base(locator)
        {
        }
    }
}