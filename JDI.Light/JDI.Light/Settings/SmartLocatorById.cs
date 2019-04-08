using System.Collections.Generic;
using System.Reflection;
using JDI.Light.Extensions;
using OpenQA.Selenium;

namespace JDI.Light.Settings
{
    public class SmartLocatorById : ISmartLocator
    {
        public string SmartSearchLocator { get; set; }
        
        public string SmartSearchName(string name) => StringExtensions.SplitHyphen(name);

        public By SmartSearch(MemberInfo member)
        {
            var smartSearchName = SmartSearchName(member.Name);
            return By.Id(smartSearchName);
        }
    }
}