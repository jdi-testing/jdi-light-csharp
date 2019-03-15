using System;
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
        public static IBaseUIElement CreateInstance(Type t, By locator, IBaseElement parent)
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
                        instance.Parent = parent;
                        return instance;
                    }
                    case 0:
                    {
                        var instance = (UIElement)Activator.CreateInstance(t, true);
                        instance.DriverName = parent.DriverName;
                        instance.Locator = locator;
                        instance.Parent = parent;
                        return instance;
                    }
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
            var element = (UIElement)instance ?? CreateInstance(type, member.GetFindsBy(), parent);
            var checkedAttr = member.GetCustomAttribute<IsCheckedAttribute>(false);
            if (checkedAttr != null && typeof(ICheckBox).IsAssignableFrom(type))
            {
                var checkBox = (CheckBox)element;
                checkBox.SetIsCheckedFunc(checkedAttr.CheckedDelegate);
            }
            return element;
        }
    }
}