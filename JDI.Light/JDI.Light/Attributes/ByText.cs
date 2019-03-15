using System;
using OpenQA.Selenium;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ByText : Attribute
    {
        public ByText(string text)
        {
            Value = By.XPath($"//*[text()='{text}']");
        }
        public By Value { get; }
    }
}