using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JDI.Light.Utils
{
    public static class ReflectionUtils
    {
        public static List<FieldInfo> InstanceFields(this Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList();
        }
        
        public static List<FieldInfo> StaticFields(this Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).ToList();
        }

        public static List<FieldInfo> GetFieldsDeep(this Type type, params Type[] types)
        {
            if (types.Contains(type))
                return new List<FieldInfo>();
            var result = type.InstanceFields();
            result.AddRange(GetFieldsDeep(type.BaseType, types));
            return result;
        }

        public static List<FieldInfo> GetFields(this object obj, params Type[] types)
        {
            return GetFields(obj, types, typeof(object));
        }

        public static List<FieldInfo> GetFields(this object obj, Type[] types, params Type[] stopTypes)
        {
            return GetFields(GetFieldsDeep(obj.GetType(), stopTypes), types);
        }

        public static List<FieldInfo> GetFields(this List<FieldInfo> fields, Type[] types)
        {
            return types == null || types.Length == 0
                ? fields
                : fields.Where(field => types.Any(t => t.IsAssignableFrom(field.FieldType))).ToList();
        }
    }
}