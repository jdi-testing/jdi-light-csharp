using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Extensions;
using JDI.Light.Factories;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Settings;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Elements
{
    public class WebCascadeInit
    {
        protected Type[] Decorators = { typeof(IBaseElement), typeof(IList) };
        protected Type[] StopTypes = { typeof(object), typeof(WebPage), typeof(Section), typeof(UIElement) };

        public T InitPages<T>(string driverName) where T : IBaseElement, new ()
        {
            var site = new T();
            SetMembers(site, typeof(T).InstanceMembers().FilterMembers(Decorators), driverName);
            return site;
        }

        private void SetMembers(IBaseElement parent, IEnumerable<MemberInfo> parentMembers, string driverName)
        {
            var members = parentMembers.Where(field => Decorators.Any(type => type.IsAssignableFrom(field.GetMemberType())));
            foreach (var member in members)
            {
                var type = member.GetMemberType();
                var instance = typeof(IPage).IsAssignableFrom(type)
                    ? GetInstancePage(parent, member)
                    : GetInstanceElement(parent, member, driverName);
                instance.Name = member.GetElementName();
                instance.DriverName = driverName;
                member.SetMemberValue(parent, instance);
                SetMembers(instance, instance.GetFields(Decorators, StopTypes), driverName);
            }
        }
        
        protected IPage GetInstancePage(IBaseElement parent, MemberInfo memberInfo)
        {
            var parentType = parent.GetType();
            var instance = (IPage)(memberInfo.GetMemberValue(parent)
                                           ?? Activator.CreateInstance(memberInfo.GetMemberType()));
            var pageAttribute = memberInfo.GetCustomAttribute<PageAttribute>(false);
            var site = parentType.GetCustomAttribute<SiteAttribute>(false);
            if (!Jdi.HasDomain && site?.Domain != null)
            {
                Jdi.Domain = site.Domain;
            }
            else if (site?.DomainProviderMethodName != null && site.DomainProviderType != null)
            {
                Jdi.Domain = site.GetDomainFunc.Invoke();
            }
            instance.Parent = parent;
            instance.Url = pageAttribute.Url;
            instance.UrlTemplate = pageAttribute.UrlTemplate;
            instance.Title = pageAttribute.Title;
            instance.CheckUrlType = pageAttribute.UrlCheckType;
            instance.CheckTitleType = pageAttribute.TitleCheckType;
            return instance;
        }

        protected IBaseElement GetInstanceElement(IBaseElement parent, MemberInfo member, string driverName)
        {
            var type = member.GetMemberType();
            var instance = (IBaseUIElement)member.GetMemberValue(parent);
            instance.Parent = parent;
            type = type.IsInterface ? MapInterfaceToElement.ClassFromInterface(type) : type;
            var element = (UIElement) instance ?? UIElementFactory.CreateInstance(type, member.GetFindsBy());
            var checkedAttr = member.GetCustomAttribute<IsCheckedAttribute>(false);
            if (checkedAttr != null && typeof(ICheckBox).IsAssignableFrom(member.GetMemberType()))
            {
                var checkBox = (CheckBox)element;
                checkBox.SetIsCheckedFunc(checkedAttr.CheckedDelegate);
            }
            if (parent == null || type != null)
            {
                var frameBy = member.GetCustomAttribute<FrameAttribute>(false)?.FrameLocator;
                if (frameBy != null)
                    element.FrameLocator = frameBy;
                By template;
                if (element.Parent is Form<IConvertible> form && !element.HasLocator
                                                && (template = form.LocatorTemplate) != null)
                    element.Locator = template.FillByTemplate(member.Name);
            }
            return element;
        }
    }
}