using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Tests.UIObjects.PseudoSections;

namespace JDI.Light.Tests.UIObjects.PseudoSite
{
    public class PseudoSitePage : WebPage
    {
        [FindBy(Css = ".customSectionUI")]
        public CustomSection CustomSection { get; set; }

        [FindBy(Css = ".extendedSectionUI")]
        public ExtendedSection ExtendedSection { get; set; }
    }
}
