using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium;

namespace JDI.Light.Settings
{
    public interface ISmartLocators
    {
        string SmartSearchName(string name);
        By SmartSearch(MemberInfo member);
    }
}