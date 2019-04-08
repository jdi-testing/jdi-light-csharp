using System.Reflection;
using JDI.Light.Extensions;
using OpenQA.Selenium;

namespace JDI.Light.Settings
{
    public class SmartLocatorByCss : ISmartLocator
    {
        public string SmartSearchLocator { get; set; } = ".{0}";

        public string SmartSearchName(string name) => StringExtensions.SplitHyphen(name);

        public By SmartSearch(MemberInfo member)
        {
            var smartSearchName = SmartSearchName(member.Name);
            return By.CssSelector(string.Format(SmartSearchLocator, smartSearchName));
        }
    }
}