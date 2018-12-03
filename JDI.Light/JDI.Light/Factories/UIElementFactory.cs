using System;
using JDI.Light.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Factories
{
    public class UIElementFactory
    {
        public static UIElement CreateInstance(Type t, By locator)
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
    }
}