using JDI.Light.Attributes;
using JDI.Light.Selenium.Elements.Common;
using JDI.Light.Selenium.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Footer : Section
    {
        [FindBy(PartialLinkText = "About")] public Link About;
    }
}