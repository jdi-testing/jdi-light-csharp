using JDI.Core.Attributes;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Selenium.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class HomePage : WebPage
    {
        [FindBy(Css = ".epam-logo img")] public IImage LogoImage;

        [FindBy(Css = "[class=icon-search]")] public IButton OpenSearchButton;

        [FindBy(Css = ".main-txt")] public Text Text;
    }
}