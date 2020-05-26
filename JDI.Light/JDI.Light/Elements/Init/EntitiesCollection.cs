using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Composite;
using System;
using System.Collections.Generic;
using System.Linq;


namespace JDI.Light.Elements.Init
{
    public class EntitiesCollection
    {
        protected EntitiesCollection()
        {
        }

        public static Dictionary<string, IPage> Pages { get; set; } = new Dictionary<string, IPage>();
        public static Dictionary<string, List<IBaseElement>> Elements { get; set; } = new Dictionary<string, List<IBaseElement>>();

        public static T GetPage<T>(string pageName)
        {
            var page = GetPage(pageName);
            if (page != null)
            {
                if (page.GetType().IsClass)
                {
                    return (T)page;
                }
                else
                {
                    throw new InvalidCastException($"Can't cast element {page.GetType()} to {nameof(T)}");
                }
            }
            throw new ElementNotFoundException($"No page found with name {pageName}");
        }

        public static IPage GetPage(string pageName)
        {
            if (Pages.ContainsKey(pageName))
            {
                return Pages[pageName];
            }
            else
            if (Pages.ContainsKey(pageName + " Page"))
            {
                return Pages[pageName + " Page"];
            }
            else
            {
                throw new ElementNotFoundException($"No page found with name {pageName}");
            }
        }

        public static IBaseElement GetWebElement(string elementName)
        {
            if (Elements.ContainsKey(elementName))
            {
                List<IBaseElement> foundElements = Elements[elementName];
                if (foundElements.Count > 1)
                {
                    var element = foundElements.First();
                    if (element != null)
                    {
                        return element;
                    }
                }
                return foundElements[0];
            }
            else
            {
                throw new ElementNotFoundException($"No elements were found with name {elementName}");
            }
        }

        public static T GetWebElement<T>(string elementName)
        {
            var foundElement = GetWebElement(elementName);
            if (foundElement != null)
            {
                if (foundElement.GetType().IsClass)
                {
                    return (T)foundElement;
                }
                else
                {
                    throw new InvalidCastException($"Can't cast element {foundElement.GetType()} to {nameof(T)}");
                }
            }
            throw new ElementNotFoundException($"No entity were found with name {elementName}");
        }
    }
}