using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JDI.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FindByAttribute : Attribute
    {
        public string Id
        {
            set => ByLocator = By.Id(value);
            get => "";
        }

        public string Name
        {
            set => ByLocator = By.Name(value);
            get => "";
        }

        public string ClassName
        {
            set => ByLocator = By.ClassName(value);
            get => "";
        }

        public string Css
        {
            set => ByLocator = By.CssSelector(value);
            get => "";
        }

        public string XPath
        {
            set => ByLocator = By.XPath(value);
            get => "";
        }

        public string Tag
        {
            set => ByLocator = By.TagName(value);
            get => "";
        }

        public string LinkText
        {
            set => ByLocator = By.LinkText(value);
            get => "";
        }

        public string PartialLinkText
        {
            set => ByLocator = By.PartialLinkText(value);
            get => "";
        }

        public By ByLocator { get; private set; }

        public static By FindsByLocator(FindsByAttribute fbAttr)
        {
            switch (fbAttr.How)
            {
                case How.Id:
                    return By.Id(fbAttr.Using);
                case How.Name:
                    return By.Name(fbAttr.Using);
                case How.ClassName:
                    return By.ClassName(fbAttr.Using);
                case How.CssSelector:
                    return By.CssSelector(fbAttr.Using);
                case How.XPath:
                    return By.XPath(fbAttr.Using);
                case How.TagName:
                    return By.TagName(fbAttr.Using);
                case How.LinkText:
                    return By.LinkText(fbAttr.Using);
                case How.PartialLinkText:
                    return By.PartialLinkText(fbAttr.Using);
                default:
                    return By.Id("Undefined locator");
            }
        }

        public static By Locator(FieldInfo field)
        {
            var locator = field.GetCustomAttribute<FindByAttribute>(false);
            return locator?.ByLocator;
        }

        public static By FindsByLocator(FieldInfo field)
        {
            var locator = field.GetCustomAttribute<FindsByAttribute>(false);
            return locator != null ? FindsByLocator(locator) : null;
        }

        public static By Locator(object obj)
        {
            var locator = obj.GetType().GetCustomAttribute<FindByAttribute>(false);
            return locator?.ByLocator;
        }
    }
}