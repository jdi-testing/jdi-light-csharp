using JDI.Web.Attributes;
using JDI.Web.Selenium.Elements.Common;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.UIWebTests.UIObjects.Sections
{
    public class Footer:Section
    {
        [FindBy(PartialLinkText = "About")]            
        public Link About;
    }
}
