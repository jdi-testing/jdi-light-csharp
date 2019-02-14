using System;
using System.Collections.Generic;
using System.Reflection;
using JDI.Light.Attributes;
using JDI.Light.Utils;

namespace JDI.Light.Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, string> FieldsAndPropertiesToDictionary(this object o)
        {
            var dict = new Dictionary<string, string>();
            var props = o.GetMembers();
            foreach (var prop in props)
            {
                var v = prop.GetMemberValue(o);
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
    }
}