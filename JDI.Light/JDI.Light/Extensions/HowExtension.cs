using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JDI.Light.Extensions
{
    public static class HowExtension
    {
        public static By GetLocator(this How how, string value)
        {
            switch (how)
            {
                case How.Id:
                    return By.Id(value);
                case How.Name:
                    return By.Name(value);
                case How.TagName:
                    return By.TagName(value);
                case How.ClassName:
                    return By.ClassName(value);
                case How.CssSelector:
                    return By.CssSelector(value);
                case How.LinkText:
                    return By.LinkText(value);
                case How.PartialLinkText:
                    return By.PartialLinkText(value);
                case How.XPath:
                    return By.XPath(value);
                default:
                    throw new Exception($"Can't get locator How.{how} for {value} value.");
            }
        }
    }
}