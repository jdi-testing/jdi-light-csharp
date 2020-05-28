using System;
using System.Collections.Generic;
using System.Reflection;
using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Settings;
using OpenQA.Selenium;

namespace JDI.Light.Factories
{
    public static class UIElementFactory
    {
        public static IBaseUIElement CreateInstance(Type t, By locator, IBaseElement parent, List<By> locators = null)
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
                        var instance = (UIElement)con.Invoke(new object[] {locator});
                        instance.DriverName = parent.DriverName;
                        instance.SmartLocators = locators;
                        instance.Parent = parent;
                        return instance;
                    }
                    case 0:
                    {
                        var instance = (UIElement)Activator.CreateInstance(t, true);
                        instance.DriverName = parent.DriverName;
                        instance.Locator = locator;
                        instance.SmartLocators = locators;
                        instance.Parent = parent;
                        return instance;
                    }
                    default: break;
                }
            }
            throw new MissingMethodException($"Can't find correct constructor to create instance of type {t}");
        }
        
        public static T CreateInstance<T>(By locator, IBaseElement parent) where T : IBaseUIElement
        {
            return (T)CreateInstance(typeof(T), locator, parent);
        }

        public static T CreateInstance<T>(By locator, IBaseElement parent, IWebElement e) where T : IBaseUIElement
        {
            var instance = CreateInstance<T>(locator, parent);
            instance.WebElement = e;
            return instance;
        }

        public static IBaseElement GetInstanceElement(this IBaseElement parent, MemberInfo member)
        {
            var type = member.GetMemberType();
            var v = member.GetMemberValue(parent);
            var instance = (IBaseUIElement)v;
            
            var defaultLocator = member.GetLocatorByAttribute();
            var smartLocators = new List<By>();
            var jDropdown = member.GetCustomAttribute<JDropDown>(false);
            var jDataList = member.GetCustomAttribute<JDataList>(false);

            if (defaultLocator != null)
            {
                smartLocators.Add(defaultLocator);
            }

            if (jDropdown != null)
            {
                defaultLocator = jDropdown.RootLocator;
            }

            if (jDataList != null)
            {
                defaultLocator = jDataList.RootLocator;
            }
            else
            {
                foreach (var smartLocator in Jdi.SmartLocators)
                {
                    if (smartLocator.SmartSearch(member) != null)
                    {
                        smartLocators.Add(smartLocator.SmartSearch(member));
                    }
                }
            }
            var element = (UIElement)instance ?? CreateInstance(type, defaultLocator, parent, smartLocators);

            var checkedAttr = member.GetCustomAttribute<IsCheckedAttribute>(false);
            if (checkedAttr != null && typeof(ICheckBox).IsAssignableFrom(type))
            {
                var checkBox = (CheckBox)element;
                checkBox.SetIsCheckedFunc(checkedAttr.CheckedDelegate);
            }

            if (jDropdown != null)
            {
                var dropList = (DropList)element;
                dropList.Setup(jDropdown.ValueLocator, jDropdown.ListLocator, jDropdown.ExpandLocator);
            }

            if (jDataList != null)
            {
                var dataList = (DataList)element;
                dataList.Setup(jDataList.ValuesLocator);
            }

            return element;
        }
    }
}