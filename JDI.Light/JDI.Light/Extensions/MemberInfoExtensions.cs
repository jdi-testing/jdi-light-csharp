using System;
using System.Reflection;
using JDI.Light.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JDI.Light.Extensions
{
    public static class MemberInfoExtensions
    {
        public static string GetElementName(this MemberInfo memberInfo)
        {
            var attr = memberInfo.GetCustomAttribute<NameAttribute>(false);
            return attr?.Name.SplitCamelCase() ?? memberInfo.Name;
        }

        public static Type GetMemberType(this MemberInfo mi)
        {
            switch (mi.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)mi).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)mi).PropertyType;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static object GetMemberValue(this MemberInfo mi, object instance)
        {
            switch (mi.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)mi).GetValue(instance);
                case MemberTypes.Property:
                    return ((PropertyInfo)mi).GetValue(instance);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void SetMemberValue(this MemberInfo mi, object instance, object value)
        {
            switch (mi.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)mi).SetValue(instance, value);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)mi).SetValue(instance, value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public static By GetFindsBy(this MemberInfo member)
        {
            var locator = member.GetCustomAttribute<FindByAttribute>(false)?.ByLocator;
            if (locator != null)
            {
                return locator;
            }
            var findsBy = member.GetCustomAttribute<FindsByAttribute>(false);
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