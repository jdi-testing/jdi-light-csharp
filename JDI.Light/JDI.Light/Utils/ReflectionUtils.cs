using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JDI.Light.Extensions;

namespace JDI.Light.Utils
{
    public static class ReflectionUtils
    {
        public static IEnumerable<MemberInfo> InstanceMembers(this Type type)
        {
            return type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }

        private static IEnumerable<MemberInfo> GetMembersDeep(this Type type, params Type[] stopTypes)
        {
            if (stopTypes.Contains(type))
            {
                return new List<MemberInfo>();
            }
            var result = InstanceMembers(type).ToList();
            result.ToList().AddRange(GetMembersDeep(type.BaseType, stopTypes));
            return result;
        }

        public static IEnumerable<MemberInfo> GetMembers(this object obj, params Type[] types)
        {
            return GetMembers(obj, types, typeof(object));
        }

        public static IEnumerable<MemberInfo> GetMembers(this object obj, Type[] types, params Type[] stopTypes)
        {
            return FilterMembers(GetMembersDeep(obj.GetType(), stopTypes), types);
        }

        public static IEnumerable<MemberInfo> FilterMembers(this IEnumerable<MemberInfo> members, Type[] types)
        {
            var membersArray = members.ToArray();

            var fieldMembers = membersArray.Where(m => m.MemberType == MemberTypes.Field
                                                       && m.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
            var propertyMembers = membersArray.Where(m => m.MemberType == MemberTypes.Property
                                                          && ((PropertyInfo)m).SetMethod != null
                                                          && m.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
            members = fieldMembers.Concat(propertyMembers.Where(p => p.Name != "WebElement")).Where(m => m.Name != "Parent" && m.Name != "WebElement");
            return types == null || types.Length == 0
                ? members
                : members.Where(m => types.Any(t => t.IsAssignableFrom(m.GetMemberType())));
        }
    }
}