using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Extensions;
using OpenQA.Selenium;

namespace JDI.Light.Utils
{
    public static class LocatorUtils
    {
        public static By FillByTemplate(this By by, params object[] args)
        {
            var byLocatorString = by.ToString();
            if (!byLocatorString.Contains("{0}"))
                throw new Exception(GetBadLocatorMsg(byLocatorString, args));

            var locatorAsString = byLocatorString;
            byLocatorString = string.Format(locatorAsString, args);
            return by.GetByFunc()(byLocatorString);
        }

        private static Func<string, By> GetByFunc(this By by)
        {
            var byTypes = new Dictionary<string, Func<string, By>>
            {
                { "CssSelector", By.CssSelector },
                { "ClassName", By.ClassName },
                { "Id", By.Id },
                { "LinkText", By.LinkText },
                { "Name", By.Name },
                { "PartialLinkText", By.PartialLinkText },
                { "TagName", By.TagName },
                { "XPath", By.XPath }
            };
            
            return byTypes.FirstOrDefault(el => by.ToString().Contains(el.Key)).Value;
        }

        private static string GetBadLocatorMsg(this string byLocator, params object[] args)
        {
            return $"Locator without parameter placeholder provided '{byLocator}'. Args: {args.Select(el => el.ToString()).FormattedJoin(", ", "'{0}'")}.";
        }
    }
}