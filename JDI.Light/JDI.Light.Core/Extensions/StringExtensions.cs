using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JDI.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comparison)
        {
            return source.IndexOf(toCheck, comparison) >= 0;
        }

        public static bool Matches(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        public static string Simplify(this string s)
        {
            return new Regex("[^a-z0-9]").Replace(s.ToLower(), "");
        }

        public static bool SimplifiedEqual(this string s1, string s2)
        {
            return s1.Simplify().Equals(s2.Simplify());
        }

        public static string FromNewLine(this string s)
        {
            return " " + Environment.NewLine + s;
        }

        public static IList<string> Split(this string s, string separator)
        {
            return s.Split(new[] { separator }, StringSplitOptions.None);
        }
    }
}