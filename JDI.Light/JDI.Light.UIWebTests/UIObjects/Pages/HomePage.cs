using JDI.Core.Interfaces.Common;
using JDI.Web.Attributes;
using JDI.Web.Selenium.Elements.Common;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.UIWebTests.UIObjects.Pages
{
    public class HomePage:WebPage
    {
        [FindBy(Css = ".main-txt")]
        public Text Text;

        [FindBy(Css = ".epam-logo img")]
        public IImage LogoImage;

        [FindBy(Css = "[class=icon-search]")]
        public IButton OpenSearchButton;
    }
}
