using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Core.Attributes;
using JDI.Core.Attributes.Objects;
using JDI.Core.Base;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Base;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Selenium.DriverFactory;
using JDI.Core.Selenium.Elements.Complex;
using JDI.Core.Selenium.Elements.Complex.Table.Interfaces;
using JDI.Core.Selenium.Elements.Composite;
using JDI.Core.Settings;
using JDI.Core.Utils;
using OpenQA.Selenium;
using RestSharp.Extensions;
using Menu = JDI.Core.Selenium.Elements.Complex.Menu;
using Table = JDI.Core.Selenium.Elements.Complex.Table.Table;

namespace JDI.Core.Selenium.Base
{
    public class WebCascadeInit
    {
        protected Type[] Decorators = { typeof(IBaseElement), typeof(IList) };

        protected Type[] StopTypes => new[]
        {
            typeof(object),
            typeof(WebPage),
            typeof(Section),
            typeof(UIElement)
        };

        public void InitElements(object parent, string driverName)
        {
            SetFields(parent, parent.GetFields(Decorators, StopTypes), parent.GetType(), driverName);
        }

        public void InitStaticPages(Type parentType, string driverName)
        {
            SetFields(null,
                parentType.StaticFields().GetFields(Decorators), parentType, driverName);
        }

        private void SetFields(object parent, List<FieldInfo> fields, Type parentType, string driverName)
        {
            fields.Where(field => Decorators.ToList().Any(type => type.IsAssignableFrom(field.FieldType))).ToList()
                .ForEach(field => SetElement(parent, parentType, field, driverName));
        }

        public T InitPages<T>(Type site, string driverName) where T : Application
        {
            var instance = (T)Activator.CreateInstance(site);
            instance.DriverName = driverName;
            InitElements(instance, driverName);
            return instance;
        }

        protected IBaseElement GetInstancePage(object parent, FieldInfo field, Type type, Type parentType)
        {
            var instance = (IBaseElement)(field.GetValue(parent)
                                           ?? Activator.CreateInstance(type));
            var pageAttribute = field.GetAttribute<PageAttribute>();
            pageAttribute?.FillPage((WebPage)instance, parentType);

            return instance;
        }

        protected IBaseElement GetInstanceElement(object parent, Type type, Type parentType, FieldInfo field,
            string driverName)
        {
            var instance = (IBaseElement)field.GetValue(parent);
            var element = (UIElement)instance;
            if (instance == null)
            {
                instance = ExceptionUtils.ActionWithException(
                    () => GetElementInstance(field, driverName, parent),
                    ex =>
                        $"Can't create child for parent '{parentType.Name}' with type '{field.FieldType.Name}'. Exception: {ex}");
            }
            else
            {
                if (!element.HasLocator)
                    element.Locator = GetNewLocator(field);
            }
            instance.Parent = parent;
            var jTable = field.GetAttribute<JTableAttribute>();
            if (jTable != null && typeof(ITable).IsAssignableFrom(field.FieldType))
            {
                FillFromAnnotationRules.SetUpTable((Table)instance, jTable);
            }
            var jDropdown = field.GetAttribute<JDropdownAttribute>();
            if (jDropdown != null && typeof(IDropDown).IsAssignableFrom(field.FieldType))
            {
                FillFromAnnotationRules.SetUpDropdown((Dropdown)instance, jDropdown);
            }
            var jMenu = field.GetAttribute<JMenuAttribute>();
            if (jMenu != null && typeof(IMenu).IsAssignableFrom(field.FieldType))
            {
                FillFromAnnotationRules.SetUpMenu((Menu)instance, jMenu);
            }
            element = (UIElement)instance;
            if (parent == null || type != null)
            {
                var frameBy = FrameAttribute.GetFrame(field);
                if (frameBy != null)
                    element.FrameLocator = frameBy;
                By template;
                if (element.Parent is Form form && !element.HasLocator
                                                && (template = form.LocatorTemplate) != null)
                    element.Locator = template.FillByTemplate(field.Name);
            }

            return element;
        }
        
        protected void SetElement(object parent, Type parentType, FieldInfo field, string driverName)
        {
            ExceptionUtils.ActionWithException(() =>
            {
                var type = field.FieldType;
                var instance = typeof(IPage).IsAssignableFrom(type)
                    ? GetInstancePage(parent, field, type, parentType)
                    : GetInstanceElement(parent, type, parentType, field, driverName);
                instance.Name = NameAttribute.GetElementName(field);
                instance.DriverName = driverName;
                instance.Parent = parent;
                field.SetValue(parent, instance);
                InitElements(instance, driverName);
            },
                ex =>
                    $"Error in SetElement for field '{field.Name}' with parent '{parentType?.Name ?? "NULL Class" + ex.FromNewLine()}'");
        }

        protected IBaseElement GetElementInstance(FieldInfo field, string driverName, object parent)
        {
            var type = field.FieldType;
            var fieldName = field.Name;
            return ExceptionUtils.ActionWithException(() => GetElementsRules(field, driverName, type, fieldName),
                ex =>
                    $"Error in GetElementInstance for field '{fieldName}'{(parent != null ? "in " + parent.GetClassName() : "")} with type '{type.Name + ex.FromNewLine()}'");
        }

        protected IBaseElement GetElementsRules(FieldInfo field, string driverName, Type type,
            string fieldName)
        {
            var newLocator = GetNewLocator(field);
            UIElement instance = null;
            if (type == typeof(List<>))
                throw JDISettings.Exception(
                    $"Can't init element {fieldName} with type 'List<>'. Please use 'IList<>' or 'Elements<>' instead");
            if (typeof(IList).IsAssignableFrom(type))
            {
                var elementClass = type.GetGenericArguments()[0];
                if (elementClass != null)
                    instance = (UIElement) Activator.CreateInstance(typeof(WebElements<>)
                        .MakeGenericType(elementClass));
            }
            else
            {
                if (type.IsInterface)
                    type = MapInterfaceToElement.ClassFromInterface(type);
                if (type != null)
                {
                    instance = (UIElement) Activator.CreateInstance(type);
                    if (newLocator != null)
                        instance.Locator = newLocator;
                }
            }

            if (instance == null)
                throw JDISettings.Exception("Unknown interface: " + type +
                                            ". Add relation interface -> class in VIElement.InterfaceTypeMap");
            instance.DriverName = driverName;
            return instance;
        }

        protected By GetNewLocator(FieldInfo field)
        {
            return ExceptionUtils.ActionWithException(() => GetNewLocatorFromField(field),
                ex => $"Error in get locator for type '{field.Name + ex.FromNewLine()}'");
        }

        protected By GetNewLocatorFromField(FieldInfo field)
        {
            By byLocator = null;
            var locatorGroup = JDIData.AppVersion;
            if (locatorGroup == null)
                return FindByAttribute.Locator(field) ?? field.GetFindsBy();
            var jFindBy = field.GetAttribute<JFindByAttribute>();
            if (jFindBy != null && locatorGroup.Equals(jFindBy.Group))
                byLocator = jFindBy.ByLocator;
            return byLocator ?? FindByAttribute.Locator(field) ?? field.GetFindsBy();
        }
    }
}