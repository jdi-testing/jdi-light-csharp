using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Tests.UIObjects.Pages;

namespace JDI.Light.Tests.UIObjects
{
    [Site(DomainProviderType = typeof(TestSiteCustomDomain), DomainProviderMethodName = nameof(GetDomain))]
    public class TestSiteCustomDomain : WebSite
    {
        public static string GetDomain()
        {
            return "https://epam.github.io/JDI";
        }

        [Page(Url = "/index.html", Title = "Home Page")]
        public HomePage HomePage;
    }
}