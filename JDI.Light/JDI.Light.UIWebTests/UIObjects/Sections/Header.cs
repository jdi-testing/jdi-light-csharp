using JDI.Core.Interfaces.Common;
using JDI.Web.Attributes;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.UIWebTests.UIObjects.Sections
{
    public class Header:Section
    {
        [FindBy(XPath = "//img[@src=\"label/Logo_Epam_Color.svg\"]")]
        public IImage Image;

        public JdiSearch Search;
    }
}
