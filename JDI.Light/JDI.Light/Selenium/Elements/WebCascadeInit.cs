using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Light.Attributes;
using JDI.Light.Attributes.JAttributes;
using JDI.Light.Enums;
using JDI.Light.Extensions;
using JDI.Light.Interfaces;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Selenium.DriverFactory;
using JDI.Light.Selenium.Elements.Base;
using JDI.Light.Selenium.Elements.Complex;
using JDI.Light.Selenium.Elements.Complex.Table.Interfaces;
using JDI.Light.Selenium.Elements.Composite;
using JDI.Light.Settings;
using JDI.Light.Utils;
using OpenQA.Selenium;
using Menu = JDI.Light.Selenium.Elements.Complex.Menu;
using Table = JDI.Light.Selenium.Elements.Complex.Table.Table;

namespace JDI.Light.Selenium.Elements
{
    public class WebCascadeInit
    {
        public ILogger Logger { get; set; } = JDI.Logger;

        protected Type[] Decorators = { typeof(IBaseElement), typeof(IList) };

        protected Type[] StopTypes => new[]
        {
            typeof(object),
            typeof(WebPage),
            typeof(Section),
            typeof(UIElement)
        };
        
        public void InitStaticPages(Type parentType, string driverName)
        {
            SetFields(null, parentType.StaticFields().GetFields(Decorators), parentType, driverName);
        }

        private void SetFields(IBaseElement parent, List<FieldInfo> fields, Type parentType, string driverName)
        {
            fields.Where(field => Decorators.ToList().Any(type => type.IsAssignableFrom(field.FieldType))).ToList()
                .ForEach(field =>
                {
                    var type = field.FieldType;
                    var instance = typeof(IPage).IsAssignableFrom(type)
                        ? GetInstancePage(parent, field, type, parentType)
                        : GetInstanceElement(parent, type, parentType, field, driverName);
                    instance.SetUp(Logger);
                    instance.Name = field.GetElementName();
                    instance.DriverName = driverName;
                    instance.Parent = parent;
                    field.SetValue(parent, instance);
                    SetFields(instance, instance.GetFields(Decorators, StopTypes), instance.GetType(), driverName);
                });
        }
        
        protected IPage GetInstancePage(object parent, FieldInfo field, Type type, Type parentType)
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

        protected IBaseElement GetInstanceElement(IBaseElement parent, Type type, Type parentType, FieldInfo field,
            string driverName)
        {
            var instance = (IBaseElement)field.GetValue(parent);
            type = type.IsInterface ? MapInterfaceToElement.ClassFromInterface(type) : type;
            var element = (UIElement)instance ?? (UIElement)Activator.CreateInstance(type, field.GetFindsBy());
            var jTable = field.GetAttribute<JTableAttribute>();
            if (jTable != null && typeof(ITable).IsAssignableFrom(field.FieldType))
            {
                var table = (Table) element;
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
                        throw JDI.Assert.Exception("Can't setup Table from attribute. Bad size: " + jTable.Size);
                    table.SetColumnsCount(int.Parse(split[0]));
                    table.SetRowsCount(int.Parse(split[1]));
                }
                table.HeaderType = jTable.HeaderType;
                table.UseCache(jTable.UseCache);
            }
            var jDropdown = field.GetAttribute<JDropdownAttribute>();
            if (jDropdown != null && typeof(IDropDown).IsAssignableFrom(field.FieldType))
            {
                var dropdown = (Dropdown) element;
                dropdown.SetUp(jDropdown.Root.ByLocator, jDropdown.Value.ByLocator,
                    jDropdown.List.ByLocator, jDropdown.Expand.ByLocator,
                    jDropdown.ElementByName.ByLocator);
            }
            var jMenu = field.GetAttribute<JMenuAttribute>();
            if (jMenu != null && typeof(IMenu).IsAssignableFrom(field.FieldType))
            {
                var menu = (Menu) element;
                menu.SetUp(jMenu.LevelLocators.Select(findby => findby.ByLocator).ToList());
                if (!jMenu.Separator.Equals(""))
                    menu.UseSeparator(jMenu.Separator);
            }
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
    }
}