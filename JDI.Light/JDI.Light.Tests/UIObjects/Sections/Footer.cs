using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Footer : Section
    {
        [FindBy(PartialLinkText = "About")]
        public Link AboutLink { get; set; }
    }
}