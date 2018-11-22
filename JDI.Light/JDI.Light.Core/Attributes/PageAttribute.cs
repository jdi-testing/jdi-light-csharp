using System;
using JDI.Core.Selenium.Elements.Composite;

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
    }
}