using System;
using System.Reflection;
using JDI.Light.Attributes;

namespace JDI.Light.Extensions
{
    public static class FieldInfoExtensions
    {
        public static T GetAttribute<T>(this FieldInfo prop) where T : Attribute
        {
            return Attribute.GetCustomAttribute(prop, typeof(T)) as T;
        }

        public static string GetElementName(this FieldInfo field)
        {
            var attr = field.GetCustomAttribute<NameAttribute>(false);
            return attr?.Name.SplitCamelCase() ?? field.Name;
        }

        public static By GetFindsBy(this FieldInfo field)
        {
            var locator = field.GetCustomAttribute<FindByAttribute>(false)?.ByLocator;
            if (locator != null)
            {
                return locator;
            }
            var findsBy = field.GetCustomAttribute<FindsByAttribute>(false);
            if (findsBy == null)
                return null;
            var how = findsBy.How;
            var @using = findsBy.Using;
            switch (how)
            {
                case How.Id:
                    return By.Id(@using);
                case How.Name:
                    return By.Name(@using);
                case How.TagName:
                    return By.TagName(@using);
                case How.ClassName:
                    return By.ClassName(@using);
                case How.CssSelector:
                    return By.CssSelector(@using);
                case How.LinkText:
                    return By.LinkText(@using);
                case How.PartialLinkText:
                    return By.PartialLinkText(@using);
                case How.XPath:
                    return By.XPath(@using);
                default:
                    return null;
            }
        }
    }
}