using JDI.Light.Attributes;
using JDI.Light.Interfaces.Common;
using JDI.Light.Selenium.Elements.Common;
using JDI.Light.Selenium.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class HomePage : WebPage
    {
        [FindBy(Css = ".epam-logo img")] public IImage LogoImage;

        [FindBy(Css = "[class=icon-search]")] public IButton OpenSearchButton;

        [FindBy(Css = ".main-txt")] public Text Text;
    }
}