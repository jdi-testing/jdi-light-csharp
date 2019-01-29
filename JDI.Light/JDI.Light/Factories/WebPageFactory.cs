using System;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Factories
{
    public static class WebPageFactory
    {
        public static WebPage CreateInstance(Type t, string url, string title)
        {
            WebPage instance = null;
            var constructors = t.GetConstructors();
            foreach (var con in constructors)
            {
                var conParams = con.GetParameters();
                switch (conParams.Length)
                {
                    case 2:
                    {
                        instance = (WebPage)Activator.CreateInstance(t, url, title);
                        break;
                    }
                    case 1:
                    {
                        instance = (WebPage)Activator.CreateInstance(t, url);
                        instance.Title = title;
                        break;
                    }
                    case 0:
                    {
                        instance = (WebPage)Activator.CreateInstance(t);
                        instance.Url = url;
                        instance.Title = title;
                        break;
                    }
                }
            }
            return instance ?? throw new MissingMethodException($"Can't find correct constructor to create instance of type {t}");
        }
    }
}