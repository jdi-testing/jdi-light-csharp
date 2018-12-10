using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Tests.UIObjects.Pages;
using JDI.Light.Tests.UIObjects.Sections;

namespace JDI.Light.Tests.UIObjects
{
    [Site(Domain = "https://epam.github.io/JDI")]
    public class TestSite : WebSite
    {
        [Page(Url = "/index.html", Title = "Home Page")]
        public static HomePage HomePage;

        [Page(Url = "/contacts.html", Title = "Contact Form")]
        public static ContactPage ContactFormPage;

        [Page(Url = "/metals-colors.html", Title = "Metal and Colors")]
        public static MetalsColorsPage MetalsColorsPage;

        [Page(Url = "/support.html", Title = "Support")]
        public static SupportPage SupportPage;

        [Page(Url = "/dates.html", Title = "Dates")]
        public static DatesPage Dates;

        [Page(Url = "simple-table.html", Title = "Simple Table")]
        public static SimpleTablePage SimpleTablePage;

        [FindBy(Css = "form")]
        public static Login Login;

        [FindBy(Css = ".uui-header")]
        public static Header Header;

        [FindBy(Css = ".footer-content")]
        public static Footer Footer;

        [FindBy(Css = "form.form-horizontal")]
        public static LoginForm LoginForm;

        [FindBy(Css = ".logs li")]
        public static TextList ActionsLog;
    }
}