using System;
using System.Reflection;
using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Settings;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Factories
{
    public static class UIElementFactory
    {
        public static UIElement CreateInstance(this Type t, By locator)
        {
            var constructors = t.GetConstructors();
            foreach (var con in constructors)
            {
                var conParams = con.GetParameters();
                switch (conParams.Length)
                {
                    case 1:
                    {
                        var instance = (UIElement)Activator.CreateInstance(t, locator);
                        return instance;
                    }
                    case 0:
                    {
                        var instance = (UIElement)Activator.CreateInstance(t);
                        instance.Locator = locator;
                        return instance;
                    }
                }
            }
            throw new MissingMethodException($"Can't find correct constructor to create instance of type {t}");
        }
        
        public static IBaseElement GetInstanceElement(this IBaseElement parent, MemberInfo member)
        {
            var type = member.GetMemberType();
            var instance = (IBaseUIElement)member.GetMemberValue(parent);
            type = type.IsInterface ? MapInterfaceToElement.ClassFromInterface(type) : type;
            var element = (UIElement)instance ?? type.CreateInstance(member.GetFindsBy());
            element.Parent = parent;
            element.DriverName = parent.DriverName;
            var checkedAttr = member.GetCustomAttribute<IsCheckedAttribute>(false);
            if (checkedAttr != null && typeof(ICheckBox).IsAssignableFrom(member.GetMemberType()))
            {
                var checkBox = (CheckBox)element;
                checkBox.SetIsCheckedFunc(checkedAttr.CheckedDelegate);
            }
            var frameBy = member.GetCustomAttribute<FrameAttribute>(false)?.FrameLocator;
            if (frameBy != null)
                element.FrameLocator = frameBy;
            By template;
            if (element.Parent is Form<IConvertible> form 
                && !element.HasLocator && (template = form.LocatorTemplate) != null)
            {
                element.Locator = template.FillByTemplate(member.Name);
            }
            return element;
        }
    }
}