using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class HomePage : BasePage
    {
        [FindBy(Css = ".epam-logo img")]
        public IImage LogoImage;

        [FindBy(Css = "[class=icon-search]")]
        public IButton OpenSearchButton;

        [FindBy(Css = ".main-txt")]
        public Text Text;
    }
}