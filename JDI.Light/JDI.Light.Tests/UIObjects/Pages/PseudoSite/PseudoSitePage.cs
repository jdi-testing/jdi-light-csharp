using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Tests.UIObjects.Sections.PseudoSections;

namespace JDI.Light.Tests.UIObjects.Pages.PseudoSite
{
    public class PseudoSitePage : WebPage
    {
        [FindBy(Css = ".customSectionUI")]
        public CustomSection CustomSectionUI { get; set; }

        public CustomSection CustomSection { get; set; }

        [FindBy(Css = ".extendedSectionUI")]
        public ExtendedSection ExtendedSectionUI { get; set; }

        public ExtendedSection ExtendedSection { get; set; }
    }
}
