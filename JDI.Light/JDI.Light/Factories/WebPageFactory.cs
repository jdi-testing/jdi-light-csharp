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
        public static WebPage CreateInstance(this Type t, string url, string title)
        {
            WebPage instance = null;
            var constructors = t.GetConstructors();
            foreach (var con in constructors)
            {
                var conParams = con.GetParameters();
                switch (conParams.Length)
                {
                    case 2:
                    {
                        instance = (WebPage)Activator.CreateInstance(t, url, title);
                        break;
                    }
                    case 1:
                    {
                        instance = (WebPage)Activator.CreateInstance(t, url);
                        instance.Title = title;
                        break;
                    }
                    case 0:
                    {
                        instance = (WebPage)Activator.CreateInstance(t);
                        instance.Url = url;
                        instance.Title = title;
                        break;
                    }
                }
            }
            return instance ?? throw new MissingMethodException($"Can't find correct constructor to create instance of type {t}");
        }
        
        public static IPage GetInstancePage(this IBaseElement parent, MemberInfo memberInfo)
        {
            var pageAttribute = memberInfo.GetCustomAttribute<PageAttribute>(false);
            var instance = (IPage)(memberInfo.GetMemberValue(parent)
                                   ?? memberInfo.GetMemberType().CreateInstance(pageAttribute.Url, pageAttribute.Title));
            instance.Parent = (ISite)parent;
            instance.DriverName = parent.DriverName;
            instance.UrlTemplate = pageAttribute.UrlTemplate;
            instance.CheckUrlType = pageAttribute.UrlCheckType;
            instance.CheckTitleType = pageAttribute.TitleCheckType;
            return instance;
        }
    }
}