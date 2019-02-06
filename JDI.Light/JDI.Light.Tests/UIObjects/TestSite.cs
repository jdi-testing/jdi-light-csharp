using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Tests.UIObjects.Forms;
using JDI.Light.Tests.UIObjects.Pages;
using JDI.Light.Tests.UIObjects.Sections;

namespace JDI.Light.Tests.UIObjects
{
    [Site(Domain = "https://epam.github.io/JDI")]
    public class TestSite : WebSite
    {
        [Page(Url = "/index.html", Title = "Home Page")]
        public HomePage HomePage;

        [Page(Url = "/contacts.html", Title = "Contact Form")]
        public ContactPage ContactFormPage;

        [Page(Url = "/metals-colors.html", Title = "Metal and Colors")]
        public MetalsColorsPage MetalsColorsPage;

        [Page(Url = "/support.html", Title = "Support")]
        public SupportPage SupportPage;

        [Page(Url = "/dates.html", Title = "Dates")]
        public DatesPage Dates;

        [Page(Url = "simple-table.html", Title = "Simple Table")]
        public SimpleTablePage SimpleTablePage;

        [Page(Url = "complex-table.html", Title = "Complex Table")]
        public ComplexTablePage ComplexTablePage;

        [FindBy(Css = ".uui-header")]
        public Header Header;

        [FindBy(Css = ".footer-content")]
        public Footer Footer;

        [FindBy(Id = "login-form")]
        public LoginForm LoginForm;

        [FindBy(Css = ".logs li")]
        public TextList ActionsLog;
    }
}