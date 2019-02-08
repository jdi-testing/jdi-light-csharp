using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Composite;
using JDI.Light.Extensions;
using JDI.Light.Factories;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Elements
{
    public static class WebCascadeInit
    {
        private static readonly Type[] Decorators = { typeof(IBaseElement), typeof(IWebElement), typeof(IList<IBaseElement>) };
        private static readonly Type[] StopTypes = { typeof(object), typeof(WebPage), typeof(Section), typeof(UIElement) };

        public static IBaseElement InitMembers(this IBaseElement targetElement)
        {
            var elementMembers = targetElement.GetMembers(Decorators, StopTypes);
            var members = elementMembers.Where(m => Decorators.Any(type => type.IsAssignableFrom(m.GetMemberType())));
            foreach (var member in members)
            {
                var type = member.GetMemberType();
                var instance = typeof(IPage).IsAssignableFrom(type)
                    ? targetElement.GetInstancePage(member)
                    : targetElement.GetInstanceElement(member);
                instance.Name = member.GetElementName();
                member.SetMemberValue(targetElement, instance);
                InitMembers(instance);
            }

            return targetElement;
        }
    }
}