using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Header : Section
    {
        [FindBy(XPath = "//img[@src=\"label/Logo_Epam_Color.svg\"]")]
        public IImage Image;

        [FindBy(Css = "ul.uui-navigation.nav")]
        public Menu Menu;
        
        public JdiSearch Search;

        [FindBy(Css = "#user-icon")]
        public IIcon UserIcon { get; set; }
    }
}