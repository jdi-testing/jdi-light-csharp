using System;
using System.Reflection;
using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Composite;

namespace JDI.Light.Factories
{
    public static class WebPageFactory
    {
        public static WebPage CreateInstance(this Type t, IWebSite parent)
        {
            var instance = (WebPage)Activator.CreateInstance(t);
            instance.DriverName = parent.DriverName;
            instance.Parent = parent;
            return instance;
        }
        
        public static IPage GetInstancePage(this IBaseElement parent, MemberInfo memberInfo)
        {
            var pageAttribute = memberInfo.GetCustomAttribute<PageAttribute>(false);
            var instance = (IPage) (memberInfo.GetMemberValue(parent)
                ?? memberInfo.GetMemberType().CreateInstance((IWebSite) parent));
            if (pageAttribute == null) return instance;
            instance.Url = pageAttribute.Url;
            instance.Title = pageAttribute.Title;
            instance.UrlTemplate = pageAttribute.UrlTemplate;
            instance.CheckUrlType = pageAttribute.UrlCheckType;
            instance.CheckTitleType = pageAttribute.TitleCheckType;
            return instance;
        }
    }
}