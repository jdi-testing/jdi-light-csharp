using System.Collections.Generic;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Extensions
{
    public static class UIElementExtensions
    {
        public static string[] GetClasses(this IBaseUIElement element)
        {
            return element.GetAttribute("class").Split(' ');
        }

        public static Dictionary<string, object> GetAllAttributes(this UIElement element)
        {
            Dictionary<string, object> result;
            try
            {
                result = element.JsExecutor.ExecuteScript(
                    "var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;",
                    element.WebElement) as Dictionary<string, object>;
            }
            catch
            {
                result = new Dictionary<string, object>();
            }
            return result;
        }
    }
}