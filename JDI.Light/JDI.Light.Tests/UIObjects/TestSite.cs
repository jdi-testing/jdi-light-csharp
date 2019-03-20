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

        [Page(Url = "/index.html", Title = "Home Page")]
        public static HomePage HomePageStatic;

        [Page(Url = "/contacts.html", Title = "Contact Form")]
        public ContactPage ContactFormPage;

        [Page(Url = "/metals-colors.html", Title = "Metal and Colors")]
        public MetalsColorsPage MetalsColorsPage;

        [Page(Url = "/support.html", Title = "Support")]
        public SupportPage SupportPage;

        [Page(Url = "/dates.html", Title = "Dates")]
        public DatesPage Dates;

        [Page(Url = "/performance.html", Title = "Performance page")]
        public PerformancePage PerformancePage;

        [Page(Url = "simple-table.html", Title = "Simple Table")]
        public SimpleTablePage SimpleTablePage;

        [Page(Url = "/html5.html", Title = "HTML 5")]
        public Html5Page Html5Page { get; set; }

        [Page(Url = "complex-table.html", Title = "Complex Table")]
        public ComplexTablePage ComplexTablePage;

        [FindBy(Css = ".uui-header")]
        public Header Header { get; set; }

        [FindBy(Css = ".footer-content")]
        public Footer Footer;

        [FindBy(Css = ".logs li")]
        public TextList ActionsLog;

        [FindBy(Id = "login-form")]
        public static LoginFormClient LoginFormPage;
    }
}