using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Tests.UIObjects.Forms;
using JDI.Light.Tests.UIObjects.Pages;
using JDI.Light.Tests.UIObjects.Pages.PseudoSite;
using JDI.Light.Tests.UIObjects.Sections;

namespace JDI.Light.Tests.UIObjects
{
    [Site(Domain = "https://epam.github.io/JDI")]
    public class TestSite : WebSite
    {
        [Page(Url = "/index.html", Title = "Home Page")]
        public HomePage HomePage;

        [Page(Url = "/index.html", Title = "Home Page")]
        public static readonly HomePage HomePageStatic;

        [Page(Url = "/contacts.html", Title = "Contact Form")]
        public ContactPage ContactFormPage;

        [Page(Url = "/metals-colors.html", Title = "Metal and Colors")]
        public MetalsColorsPage MetalsColorsPage;

        [Page(Url = "/support.html", Title = "Support")]
        public SupportPage SupportPage;

        [Page(Url = "/dates.html", Title = "Dates")]
        public DatesPage Dates;

        [Page(Url = "/performance.html", Title = "Performance page")]
        public PerformancePage PerformancePage { get; set; }

        [Page(Url = "simple-table.html", Title = "Simple Table")]
        public SimpleTablePage SimpleTablePage;

        [Page(Url = "/html5.html", Title = "HTML 5")]
        public Html5Page Html5Page { get; set; }

        [Page(Url = "complex-table.html", Title = "Complex Table")]
        public ComplexTablePage ComplexTablePage;

        [Page(Url = "/pseudo-site.html", Title = "Pseudo Site")]
        public PseudoSitePage PseudoSitePage { get; set; }

        [Page(Url = "pagewithurl.com")]
        public PageWithUrl PageWithUrl { get; set; }

        [Page(Url = "/pagewithurl.com")]
        public PageWithUrl SlashPageWithUrl { get; set; }

        [Page(Title = "Page with Title")]
        public PageWithTitle PageWithTitle { get; set; }

        [Page(Url = "pagewithboth.com", Title = "Page with both")]
        public PageWithBoth PageWithBoth { get; set; }

        public PageWithoutBoth PageWithoutBoth { get; set; }
        
        [Page(Url = "user-table.html", Title = "User Table")]
        public UsersTablePage UsersPage { get; set; }

        [FindBy(Css = ".uui-header")]
        public Header Header { get; set; }

        public JdiSearch JdiSearch { get; set; }

        [FindBy(Css = "ul.sidebar-menu")]
        public Menu SidebarMenu;

        [FindBy(Css = ".footer-content")]
        public Footer Footer { get; set; }

        [FindBy(Css = ".logs li")]
        public TextList ActionsLog;

        [FindBy(Id = "login-form")]
        public static LoginFormClient LoginFormPage;
    }
}