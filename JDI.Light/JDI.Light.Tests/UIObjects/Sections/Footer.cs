using JDI.Core.Attributes;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Selenium.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Footer : Section
    {
        [FindBy(PartialLinkText = "About")] public Link About;
    }
}