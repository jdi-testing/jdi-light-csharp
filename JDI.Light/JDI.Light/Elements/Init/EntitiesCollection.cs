using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Composite;
using System;
using System.Collections.Generic;

namespace JDI.Light.Elements.Init
{
    public class EntitiesCollection
    {
        protected EntitiesCollection()
        {
        }

        public static Dictionary<string, IPage> Pages { get; set; } = new Dictionary<string, IPage>();
        public static Dictionary<string, List<IBaseElement>> Elements { get; set; } = new Dictionary<string, List<IBaseElement>>();

        public static T getPage<T>(string pageName, T type)
        {
            var page = getPage(pageName);
            if (page != null && page.GetType().IsClass)
                return (T)page;
            throw new Exception($"Can't cast element {page.GetType()} to {type}");
        }

        public static IPage getPage(string pageName)
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
                throw new Exception($"No page found with name {pageName}");
            }
        }
    }
}
