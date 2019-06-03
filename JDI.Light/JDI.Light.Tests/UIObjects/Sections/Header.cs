using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Header : Section
    {
        [FindBy(XPath = "//img[@src=\"label/Logo_Epam_Color.svg\"]")]
        public Image Image { get; set; }

        [FindBy(Css = "ul.uui-navigation.nav")]
        public Menu Menu { get; set; }

        public JdiSearch Search { get; set; }

        [FindBy(Css = "#user-icon")]
        public Icon UserIcon { get; set; }
    }
}