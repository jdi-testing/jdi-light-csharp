using System;
using JDI.Light.Extensions;
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

        public JDropDown(string root, string value, string list, string expander, How how = How.CssSelector)
        {
            RootLocator = how.GetLocator(root);
            ValueLocator = how.GetLocator(value);
            ListLocator = how.GetLocator(list);
            ExpandLocator = how.GetLocator(expander);
        }
    }
}