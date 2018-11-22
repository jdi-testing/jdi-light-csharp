using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Core.Attributes;
using JDI.Core.Attributes.JAttributes;
using JDI.Core.Enums;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Base;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Selenium.DriverFactory;
using JDI.Core.Selenium.Elements.Base;
using JDI.Core.Selenium.Elements.Complex;
using JDI.Core.Selenium.Elements.Complex.Table.Interfaces;
using JDI.Core.Selenium.Elements.Composite;
using JDI.Core.Settings;
using JDI.Core.Utils;
using OpenQA.Selenium;
using RestSharp.Extensions;
using Menu = JDI.Core.Selenium.Elements.Complex.Menu;
using Table = JDI.Core.Selenium.Elements.Complex.Table.Table;

namespace JDI.Core.Selenium.Elements
{
    public class WebCascadeInit
    {
        protected static Type[] Decorators = { typeof(IBaseElement), typeof(IList) };

        protected static Type[] StopTypes => new[]
        {
            typeof(object),
            typeof(WebPage),
            typeof(Section),
            typeof(UIElement)
        };
        
        public static void InitStaticPages(Type parentType, string driverName)
        {
            SetFields(null,
                parentType.StaticFields().GetFields(Decorators), parentType, driverName);
        }

        private static void SetFields(object parent, List<FieldInfo> fields, Type parentType, string driverName)
        {
            fields.Where(field => Decorators.ToList().Any(type => type.IsAssignableFrom(field.FieldType))).ToList()
                .ForEach(field =>
                {
                    ExceptionUtils.ActionWithException(() =>
                        {
                            var type = field.FieldType;
                            var instance = typeof(IPage).IsAssignableFrom(type)
                                ? GetInstancePage(parent, field, type, parentType)
                                : GetInstanceElement(parent, type, parentType, field, driverName);
                            instance.Name = field.GetElementName();
                            instance.DriverName = driverName;
                            instance.Parent = parent;
                            field.SetValue(parent, instance);
                            SetFields(parent, parent.GetFields(Decorators, StopTypes), parent.GetType(), driverName);
                        },
                        ex =>
                            $"Error in SetElement for field '{field.Name}' with parent '{parentType?.Name ?? "NULL Class" + ex.FromNewLine()}'");
                });
        }
        
        protected static IPage GetInstancePage(object parent, FieldInfo field, Type type, Type parentType)
        {
            var instance = (IPage)(field.GetValue(parent)
                                           ?? Activator.CreateInstance(type));
            var pageAttribute = field.GetAttribute<PageAttribute>();
            var page = (WebPage) instance;
            var url = pageAttribute.Url;
            var site = parentType.GetCustomAttribute<SiteAttribute>(false);
            if (!WebSettings.HasDomain && site != null)
                WebSettings.Domain = site.Domain;
            url = url.Contains("://") || !WebSettings.HasDomain
                ? url
                : WebPage.GetUrlFromUri(url);
            var title = pageAttribute.Title;
            var urlTemplate = pageAttribute.UrlTemplate;
            var urlCheckType = pageAttribute.UrlCheckType;
            var titleCheckType = pageAttribute.TitleCheckType;
            if (!string.IsNullOrEmpty(urlTemplate))
            {
                urlTemplate = urlTemplate.Contains("://") || !WebSettings.HasDomain ||
                              urlCheckType != CheckPageType.Match
                    ? urlTemplate
                    : WebPage.GetMatchFromDomain(urlTemplate);
            }
            page.UpdatePageData(url, title, urlCheckType, titleCheckType, urlTemplate);
            return instance;
        }

        protected static IBaseElement GetInstanceElement(object parent, Type type, Type parentType, FieldInfo field,
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
                var table = (Table) instance;
                table.SetUp(jTable.Root.ByLocator, jTable.Cell.ByLocator,
                    jTable.Row.ByLocator, jTable.Column.ByLocator, jTable.Footer.ByLocator,
                    jTable.ColStartIndex, jTable.RowStartIndex);

                if (jTable.Header != null)
                    table.ColumnHeaders = jTable.Header;
                if (jTable.RowsHeader != null)
                    table.RowHeaders = jTable.RowsHeader;

                if (jTable.Height > 0)
                    table.SetColumnsCount(jTable.Height);
                if (jTable.Width > 0)
                    table.SetRowsCount(jTable.Width);
                if (!jTable.Size.Equals(""))
                {
                    var split = jTable.Size.Split('x');
                    if (split.Length == 1)
                        split = jTable.Size.Split('X');
                    if (split.Length != 2)
                        throw JDISettings.Exception("Can't setup Table from attribute. Bad size: " + jTable.Size);
                    table.SetColumnsCount(int.Parse(split[0]));
                    table.SetRowsCount(int.Parse(split[1]));
                }

                table.HeaderType = jTable.HeaderType;
                table.UseCache(jTable.UseCache);
            }
            var jDropdown = field.GetAttribute<JDropdownAttribute>();
            if (jDropdown != null && typeof(IDropDown).IsAssignableFrom(field.FieldType))
            {
                var dropdown = (Dropdown) instance;
                dropdown.SetUp(jDropdown.Root.ByLocator, jDropdown.Value.ByLocator,
                    jDropdown.List.ByLocator, jDropdown.Expand.ByLocator,
                    jDropdown.ElementByName.ByLocator);
            }
            var jMenu = field.GetAttribute<JMenuAttribute>();
            if (jMenu != null && typeof(IMenu).IsAssignableFrom(field.FieldType))
            {
                var menu = (Menu) instance;
                menu.SetUp(jMenu.LevelLocators.Select(findby => findby.ByLocator).ToList());
                if (!jMenu.Separator.Equals(""))
                    menu.UseSeparator(jMenu.Separator);
            }
            element = (UIElement)instance;
            if (parent == null || type != null)
            {
                var frameBy = field.GetCustomAttribute<FrameAttribute>(false)?.FrameLocator;
                if (frameBy != null)
                    element.FrameLocator = frameBy;
                By template;
                if (element.Parent is Form form && !element.HasLocator
                                                && (template = form.LocatorTemplate) != null)
                    element.Locator = template.FillByTemplate(field.Name);
            }

            return element;
        }
        
        protected static IBaseElement GetElementInstance(FieldInfo field, string driverName, object parent)
        {
            var type = field.FieldType;
            var fieldName = field.Name;
            return ExceptionUtils.ActionWithException(() =>
                {
                    var newLocator = GetNewLocator(field);
                    UIElement instance = null;
                    if (type == typeof(List<>))
                        throw JDISettings.Exception(
                            $"Can't init element {fieldName} with type 'List<>'. Please use 'IList<>' or 'Elements<>' instead");

                    if (type.IsInterface)
                        type = MapInterfaceToElement.ClassFromInterface(type);
                    if (type != null)
                    {
                        instance = (UIElement)Activator.CreateInstance(type);
                        if (newLocator != null)
                            instance.Locator = newLocator;
                    }
                    if (instance == null)
                        throw JDISettings.Exception("Unknown interface: " + type +
                                                    ". Add relation interface -> class in VIElement.InterfaceTypeMap");
                    instance.DriverName = driverName;
                    return instance;
                },
                ex =>
                    $"Error in GetElementInstance for field '{fieldName}'{(parent != null ? "in " + parent.GetClassName() : "")} with type '{type.Name + ex.FromNewLine()}'");
        }
        
        protected static By GetNewLocator(FieldInfo field)
        {
            return ExceptionUtils.ActionWithException(() => 
                    field.GetAttribute<JFindByAttribute>()?.ByLocator 
                    ?? field.GetCustomAttribute<FindByAttribute>(false)?.ByLocator 
                    ?? field.GetFindsBy(),
                ex => $"Error in get locator for type '{field.Name + ex.FromNewLine()}'");
        }
    }
}