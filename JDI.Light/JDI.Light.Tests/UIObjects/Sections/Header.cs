using JDI.Light.Attributes;
using JDI.Light.Interfaces.Common;
using JDI.Light.Selenium.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Header : Section
    {
        [FindBy(XPath = "//img[@src=\"label/Logo_Epam_Color.svg\"]")]
        public IImage Image;

        public JdiSearch Search;
    }
}