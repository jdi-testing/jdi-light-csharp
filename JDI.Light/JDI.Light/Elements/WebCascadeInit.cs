using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Composite;
using JDI.Light.Elements.Init;
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
                IBaseElement instance = null;
                var type = member.GetMemberType();
                if (typeof(IPage).IsAssignableFrom(type))
                {
                    instance = targetElement.GetInstancePage(member);
                    if (!EntitiesCollection.Pages.Keys.Contains(member.GetElementName()))
                    {
                        EntitiesCollection.Pages.Add(member.GetElementName(), targetElement.GetInstancePage(member));
                    }
                }
                else
                {
                    instance = targetElement.GetInstanceElement(member);
                    if (!EntitiesCollection.Elements.Keys.Contains(member.GetElementName()))
                    {
                        var elements = new List<IBaseElement>();
                        elements.Add(targetElement.GetInstanceElement(member));
                        EntitiesCollection.Elements.Add(member.GetElementName(), elements);
                    }
                    else
                    {
                        EntitiesCollection.Elements.Single(k => k.Key.Equals(member.GetElementName())).
                            Value.Add(targetElement.GetInstanceElement(member));
                    }
                }
                instance.Name = member.GetElementName();
                member.SetMemberValue(targetElement, instance);
                InitMembers(instance);
            }
            return targetElement;
        }
    }
}