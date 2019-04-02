using System;
using System.Collections.Generic;
using System.Reflection;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Settings
{
    public class SmartLocators
    {
        public static List<string> SmartSearchLocators = new List<string>();

        public static Func<string, string> SmartSearchName = (smartSearchName) =>
            smartSearchName.Substring(0, smartSearchName.IndexOf("-")).Trim();
        /*
        public static Func<UIElement, UIElement> SmartSearch = (smartSearch) =>
        {
            var locatorName = SmartSearchName.Invoke(smartSearch.Name);
            foreach (var template in SmartSearchLocators)
            {
                UIElement ui = null;

                ui.Name = smartSearch.Name;
            }
            return null;
        };*/

        public static By SmartSearch(Type t, string memberName, IBaseElement parent)
        {
            t = t.IsInterface ? MapInterfaceToElement.ClassFromInterface(t) : t;
            var constructors = t.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var con in constructors)
            {
                var conParams = con.GetParameters();
                switch (conParams.Length)
                {
                    case 1:
                    {
                        var instance = (UIElement)con.Invoke(new object[] { By.Id(memberName) });
                        instance.DriverName = parent.DriverName;
                        instance.Parent = parent;
                        return By.Id(memberName);
                        }
                    case 0:
                    {
                        var instance = (UIElement)Activator.CreateInstance(t, true);
                        instance.DriverName = parent.DriverName;
                        instance.Locator = By.Id(memberName);
                        instance.Parent = parent;
                        return By.Id(memberName);
                        }
                }
            }
            throw new MissingMethodException($"Can't find correct constructor to create instance of type {t}");
            
        }
    }
}