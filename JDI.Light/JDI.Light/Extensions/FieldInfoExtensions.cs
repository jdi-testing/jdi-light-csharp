using System.Reflection;
using JDI.Light.Attributes;

namespace JDI.Light.Extensions
{
    public static class FieldInfoExtensions
    {
        public static string GetElementName(this FieldInfo field)
        {
            var attr = field.GetCustomAttribute<NameAttribute>(false);
            return attr?.Name.SplitCamelCase() ?? field.Name;
        }
    }
}