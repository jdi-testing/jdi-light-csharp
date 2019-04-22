using System;
using JDI.Light.Extensions;
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
            RootLocator = how.GetLocator(root);
            ValuesLocator = how.GetLocator(values);
        }
    }
}