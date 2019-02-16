using System;
using OpenQA.Selenium;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class XPathAttribute : Attribute
    {
        public XPathAttribute(string locator)
        {
            Value = By.XPath(locator);
        }
        public By Value { get; }
    }
}