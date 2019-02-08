using System.Reflection;
using JDI.Light.Attributes;
using JDI.Light.Elements;
using JDI.Light.Interfaces.Composite;

namespace JDI.Light.Factories
{
    public static class WebSiteFactory
    {
        public static T GetInstanceSite<T>(string driverName) where T : IWebSite, new()
        {
            var siteType = typeof(T);
            var site = new T { DriverName = driverName };
            var siteAttribute = siteType.GetCustomAttribute<SiteAttribute>(false);
            if (siteAttribute?.Domain != null)
            {
                site.Domain = siteAttribute.Domain;
            }
            else if (siteAttribute?.DomainProviderMethodName != null && siteAttribute.DomainProviderType != null)
            {
                site.Domain = siteAttribute.GetDomainFunc.Invoke();
            }

            site.InitMembers();
            return site;
        }
    }
}