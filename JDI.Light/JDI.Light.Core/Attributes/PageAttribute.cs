using System;
using System.Reflection;
using JDI.Core.Selenium.Elements.Composite;
using JDI.Core.Settings;

namespace JDI.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class PageAttribute : Attribute
    {
        public string Title = "";
        public CheckPageTypes TitleCheckType = CheckPageTypes.None;
        public string Url = "";
        public CheckPageTypes UrlCheckType = CheckPageTypes.None;
        public string UrlTemplate = "";

        public void FillPage(WebPage page, Type parentClass)
        {
            var url = Url;
            var site = parentClass.GetCustomAttribute<SiteAttribute>(false);
            if (!WebSettings.HasDomain && site != null)
                WebSettings.Domain = site.Domain;
            url = url.Contains("://") || !WebSettings.HasDomain
                ? url
                : WebPage.GetUrlFromUri(url);
            var title = Title;
            var urlTemplate = UrlTemplate;
            var urlCheckType = UrlCheckType;
            var titleCheckType = TitleCheckType;
            if (!string.IsNullOrEmpty(urlTemplate))
                urlTemplate = urlTemplate.Contains("://") || !WebSettings.HasDomain ||
                              urlCheckType != CheckPageTypes.Match
                    ? urlTemplate
                    : WebPage.GetMatchFromDomain(urlTemplate);
            page.UpdatePageData(url, title, urlCheckType, titleCheckType, urlTemplate);
        }
    }
}