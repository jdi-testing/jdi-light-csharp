using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Light.Extensions;

namespace JDI.Light.Utils
{
    public static class ReflectionUtils
    {
        public static IEnumerable<MemberInfo> InstanceMembers(this Type type)
        {
            return type.GetMembers(BindingFlags.Public | BindingFlags.Instance);
        }

        private static List<FieldInfo> GetFieldsDeep(this Type type, params Type[] types)
        {
            if (types.Contains(type))
                return new List<FieldInfo>();
            var result = type.GetFields(BindingFlags.Public | BindingFlags.Instance).ToList();
            result.AddRange(GetFieldsDeep(type.BaseType, types));
            return result;
        }

        private static List<MemberInfo> GetMembersDeep(this Type type, params Type[] types)
        {
            if (types.Contains(type))
                return new List<MemberInfo>();
            var result = InstanceMembers(type).ToList();
            result.ToList().AddRange(GetMembersDeep(type.BaseType, types));
            return result;
        }

        public static List<FieldInfo> GetFields(this object obj, params Type[] types)
        {
            return GetFields(obj, types, typeof(object));
        }

        public static List<FieldInfo> GetFields(this object obj, Type[] types, params Type[] stopTypes)
        {
            return FilterFields(GetFieldsDeep(obj.GetType(), stopTypes), types);
        }

        public static IEnumerable<MemberInfo> GetMembers(this object obj, params Type[] types)
        {
            return GetMembers(obj, types, typeof(object));
        }

        public static IEnumerable<MemberInfo> GetMembers(this object obj, Type[] types, params Type[] stopTypes)
        {
            return FilterMembers(GetMembersDeep(obj.GetType(), stopTypes), types);
        }

        public static List<FieldInfo> FilterFields(this List<FieldInfo> fields, Type[] types)
        {
            return types == null || types.Length == 0
                ? fields
                : fields.Where(field => types.Any(t => t.IsAssignableFrom(field.FieldType))).ToList();
        }

        public static IEnumerable<MemberInfo> FilterMembers(this IEnumerable<MemberInfo> members, Type[] types)
        {
            return types == null || types.Length == 0
                ? members
                : members.Where(m => types.Any(t => t.IsAssignableFrom(m.GetMemberType())));
        }
    }
}