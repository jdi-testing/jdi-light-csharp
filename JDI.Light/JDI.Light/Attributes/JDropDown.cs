using System;
using OpenQA.Selenium;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class JDropDown : Attribute
    {
  
        public string[] Css
        {
            set
            {
                RootLocator = By.CssSelector(value[0]);
                ValueLocator = By.CssSelector(value[1]);
                ListLocator = By.CssSelector(value[2]);
                ExpandLocator = By.CssSelector(value[3]);
            }
            get => new[] {""};
        }

        public By RootLocator { get; private set; }
        public By ValueLocator { get; private set; }
        public By ListLocator { get; private set; }
        public By ExpandLocator { get; private set; }

        /*
        public JDropDown(string root, string value, string list, string expander)
        {
            RootLocator = By.CssSelector(root);
            ValueLocator = By.CssSelector(value);
            ListLocator  = By.CssSelector(list);
            ExpandLocator = By.CssSelector(expander);
        }
        
        public string RootByCss
        {
            set => RootLocator = By.CssSelector(value);
            get => "";
        }
        public string ValueByCss
        {
            set => ValueLocator = By.CssSelector(value);
            get => "";
        }
        public string ListByCss
        {
            set => RootLocator = By.CssSelector(value);
            get => "";
        }
        public string ExpanderByCss
        {
            set { RootLocator = By.CssSelector(value); }
            get => "";
        }
        */
    }
}