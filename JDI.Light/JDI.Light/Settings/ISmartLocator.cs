using System.Reflection;
using OpenQA.Selenium;

namespace JDI.Light.Settings
{
    public interface ISmartLocator
    {
        string SmartSearchLocator { get; set; }
        string SmartSearchName(string name);
        By SmartSearch(MemberInfo member);
    }
}