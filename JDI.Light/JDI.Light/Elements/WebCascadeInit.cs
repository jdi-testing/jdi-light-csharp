using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Composite;
using JDI.Light.Extensions;
using JDI.Light.Factories;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;

namespace JDI.Light.Elements
{
    public static class WebCascadeInit
    {
        private static readonly Type[] Decorators = { typeof(IBaseElement), typeof(IList<IBaseElement>) };
        private static readonly Type[] StopTypes = { typeof(object), typeof(WebPage), typeof(Section), typeof(UIElement) };

        public static T InitSite<T>(string driverName) where T : ISite, new ()
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
            InitMembers(site, driverName);
            return site;
        }

        public static IBaseElement InitMembers(this IBaseElement targetElement, string driverName)
        {
            var elementMembers = targetElement.GetMembers(Decorators, StopTypes);
            var members = elementMembers.Where(m => Decorators.Any(type => type.IsAssignableFrom(m.GetMemberType())));
            foreach (var member in members)
            {
                var type = member.GetMemberType();
                var instance = typeof(IPage).IsAssignableFrom(type)
                    ? targetElement.GetInstancePage(member)
                    : targetElement.GetInstanceElement(member);
                instance.Name = member.GetElementName();
                instance.DriverName = driverName;
                member.SetMemberValue(targetElement, instance);
                InitMembers(instance, driverName);
            }

            return targetElement;
        }
    }
}