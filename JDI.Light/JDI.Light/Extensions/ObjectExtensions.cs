using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Interfaces.Common;
using JDI.Light.Utils;

namespace JDI.Light.Extensions
{
    public static class ObjectExtensions
    {
        private static Dictionary<string, string> CreateDictionary(object o, IEnumerable<MemberInfo> fields, 
            Func<MemberInfo, object, object> getValue)
        {
            var dict = new Dictionary<string, string>();
            foreach (var prop in fields)
            {
                var v = getValue.Invoke(prop, o);
                string strValue = null;
                switch (v)
                {
                    case null:
                        strValue = "null";
                        break;
                    case string s:
                        strValue = s;
                        break;
                    case IConvertible _:
                        strValue = v.ToString();
                        break;
                }
                var attr = prop.GetCustomAttribute<NameAttribute>(false);
                var n = attr?.Name.SplitCamelCase() ?? "";
                var strKey = string.IsNullOrWhiteSpace(n) ? prop.Name : n;
                dict.Add(strKey, strValue);
            }

            return dict;
        }
        public static Dictionary<string, string> PropertiesToDictionary(this object o)
        {
            var props = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dictProperties = CreateDictionary(o, props.Select(p => (MemberInfo)p), 
                (m, obj) => ((PropertyInfo) m).GetValue(obj));
            var dictFields = CreateDictionary(o, o.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance),
                (m, obj) => ((FieldInfo) m).GetValue(obj));
            var result = dictProperties.Union(dictFields).ToDictionary(s => s.Key, s => s.Value);
            return result;
        }

        public static IEnumerable<MemberInfo> GetFields(this object o)
        {
            var props = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(p => (MemberInfo)p);
            var fields = o.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return props.Union(fields);
        }
        public static IEnumerable<MemberInfo> GetFieldsOfType(this object o, Type type)
        {
            var fields = o.GetFields();
            return fields.Where(p =>  type.IsAssignableFrom(p.GetMemberType()));
        }
    }
}