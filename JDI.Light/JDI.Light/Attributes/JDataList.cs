using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class JDataList : Attribute
    {
        public By RootLocator { get; private set; }
        public By ValueLocator { get; private set; }
        public By ListLocator { get; private set; }

        public JDataList(string root, string value, string list, How how = How.CssSelector)
        {
            switch (how)
            {
                case How.CssSelector:
                {
                    RootLocator = By.CssSelector(root);
                    ValueLocator = By.CssSelector(value);
                    ListLocator = By.CssSelector(list);
                    return;
                }
                case How.XPath:
                {
                    RootLocator = By.XPath(root);
                    ValueLocator = By.XPath(value);
                    ListLocator = By.XPath(list);
                    return;
                }
                default:
                {
                    return;
                }
            }
        }
    }
}