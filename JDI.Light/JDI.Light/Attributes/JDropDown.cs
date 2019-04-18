using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class JDropDown : Attribute
    {
        public By RootLocator { get; private set; }
        public By ValueLocator { get; private set; }
        public By ListLocator { get; private set; }
        public By ExpandLocator { get; private set; }

       
        public JDropDown(string root, string value, string list, string expander, How how)
        {
            switch (how)
            {
                case How.CssSelector:
                {
                    RootLocator = By.CssSelector(root);
                    ValueLocator = By.CssSelector(value);
                    ListLocator = By.CssSelector(list);
                    ExpandLocator = By.CssSelector(expander);
                    return;
                }
                default:
                    return;
            }
        }
    }
}