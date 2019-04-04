using System.Collections.Generic;
using System.Reflection;
using JDI.Light.Extensions;
using OpenQA.Selenium;

namespace JDI.Light.Settings
{
    public class SmartLocators : ISmartLocators
    {
        public List<string> SmartSearchLocators = new List<string>();
        
        public string SmartSearchName(string name) => StringExtensions.SplitHyphen(name);

        public By SmartSearch(MemberInfo member)
        {
            var smartSearchName = SmartSearchName(member.Name);
            return By.Id(smartSearchName);
        }
    }
}