using System;
using JDI.Core.Enums;
using JDI.Core.Selenium.Elements.Composite;

namespace JDI.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class PageAttribute : Attribute
    {
        public string Title = "";
        public CheckPageType TitleCheckType = CheckPageType.None;
        public string Url = "";
        public CheckPageType UrlCheckType = CheckPageType.None;
        public string UrlTemplate = "";
    }
}