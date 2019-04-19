using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class JDataList : Attribute
    {
        public By RootLocator { get; private set; }
        public By ValuesLocator { get; private set; }

        public JDataList(string root, string values, How how = How.CssSelector)
        {
            switch (how)
            {
                case How.CssSelector:
                {
                    RootLocator = By.CssSelector(root);
                    ValuesLocator = By.CssSelector(values);
                    return;
                }
                case How.XPath:
                {
                    RootLocator = By.XPath(root);
                    ValuesLocator = By.XPath(values);
                    return;
                }
                default:
                    return;
            }
        }
    }
}