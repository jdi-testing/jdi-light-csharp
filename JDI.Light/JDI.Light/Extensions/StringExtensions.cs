using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace JDI.Light.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Checks driver version by parsing *.exe file of driver
        /// </summary>
        /// <param name="path">Path to driver binary</param>
        /// <param name="version">Version to check</param>
        /// <returns>True - if versions are equals, else false</returns>
        public static bool CheckDriverVersionFromExe(this string path, string version)
        {
            var result = false;

            if (File.Exists(path))
            {
                var exeContent = File.ReadAllText(path);
                if (exeContent.Contains(version))
                    result = true;
            }

            return result;
        }

        /// <summary>
        ///     Checks driver version by parsing binary attributes
        /// </summary>
        /// <param name="path">Path to driver binary</param>
        /// <param name="version">Version to check</param>
        /// <returns>True - if versions are equals, else false</returns>
        public static bool CheckDriverVersionFormExeAttributes(this string path, string version)
        {
            var result = false;
            if (File.Exists(path))
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(path);
                if (versionInfo.ProductVersion.Contains(version))
                    result = true;
            }

            return result;
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

        public static string SplitCamelCase(this string camel)
        {
            var result = camel.ToUpper().FirstOrDefault().ToString();
            for (var i = 1; i < camel.Length - 1; i++)
                result += (char.IsUpper(camel[i]) && !char.IsUpper(camel[i - 1]) ? " " : "") + camel[i];
            return result + camel[camel.Length - 1];
        }

        public static string SplitHyphen(string value)
        {
            var text = _cleanupString(value);
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            else
            {
                var result = char.ToString(char.ToLower(text.ElementAt(0)));

                for (var i = 1; i < text.Length; ++i)
                {
                    var symbol = text.ElementAt(i);
                    if (char.IsUpper(symbol))
                    {
                        result = result + "-";
                    }

                    result = result + char.ToLower(symbol);
                }

                return result;
            }
        }

        private static string _cleanupString(string text)
        {
            return string.IsNullOrEmpty(text) ? "" : text.Replace("[^a-zA-Z0-9]", "");
        }
    }
}