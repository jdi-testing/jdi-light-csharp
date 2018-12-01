using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JDI.Light.Extensions;

namespace JDI.Light.Utils
{
    public static class ParseObjectUtil
    {
        public static Dictionary<string, string> ParseAsString(this string objString)
        {
            return ExceptionUtils.ActionWithException(() =>
            {
                if (objString == null)
                    return null;
                var result = new Dictionary<string, string>();
                var values = new List<string>();
                var i = 1;
                var str = objString;
                while (objString.IndexOf("#(#", StringComparison.Ordinal) > 0)
                {
                    values.Add(objString.Substring(objString.IndexOf("#(#", StringComparison.Ordinal) + 3,
                        objString.IndexOf("#)#", StringComparison.Ordinal)));
                    str = objString.Replace("#\\(#.*#\\)#", "#VAL" + i++);
                }

                var fields = str.Split("#;#");
                fields.ForEach(field =>
                {
                    var splitField = field.Split("#:#");
                    if (splitField.Count == 2)
                        result.Add(splitField[0], ProcessValue(splitField[1], values));
                });
                return result;
            }, ex => $"Can't parse string '{objString}' to Object");
        }

        private static string ProcessValue(string input, IList<string> values)
        {
            if (input.Equals("#NULL#"))
                return null;
            return input.Matches("#VAL\\d*")
                ? values[int.Parse(input.Substring(4)) - 1]
                : input;
        }

        public static Dictionary<string, string> AsDictionary(this object obj)
        {
            return obj == null
                ? new Dictionary<string, string>()
                : ParseObjectAsString(PrintObject(obj));
        }

        public static Dictionary<string, string> ToDictionary(this object o)
        {
            var dict = new Dictionary<string, string>();
            o.GetFields().ForEach(f =>
            {
                var v = f.GetValue(o);
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
                var n = f.GetElementName();
                var strKey = string.IsNullOrWhiteSpace(n) ? f.Name : n;
                dict.Add(strKey, strValue);
            });
            return dict;
        }

        private static string PrintObject(object obj)
        {
            var result = new List<string>();
            obj.GetFields().ForEach(field =>
            {
                var value = field.GetValue(obj);
                string strValue = null;
                if (value == null)
                    strValue = "#NULL#";
                else if (value is string s)
                    strValue = s;
                else if (value is IConvertible)
                    strValue = value.ToString();
                if (strValue != null)
                    result.Add($"{field.GetElementName()}#:#{strValue}");
            });
            return result.FormattedJoin("#;#");
        }

        private static Dictionary<string, string> ParseObjectAsString(string objString)
        {
            if (objString == null)
                return null;
            var result = new Dictionary<string, string>();
            var values = new List<string>();
            var i = 1;
            var str = objString;
            int from;
            while ((from = str.IndexOf("#(#", StringComparison.Ordinal)) > 0)
            {
                var to = str.IndexOf("#)#", StringComparison.Ordinal);
                values.Add(str.Substring(from + 3, to - from - 3));
                str = new Regex("#\\(#.*#\\)#").Replace(str, "#VAL" + i++);
            }

            var fields = str.Split("#;#");
            fields.ForEach(field =>
            {
                var splitField = field.Split("#:#");
                if (splitField.Count == 2)
                    result.Add(splitField[0], ProcessValue(splitField[1], values));
            });
            return result;
        }
    }
}