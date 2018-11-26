using System.Collections.Generic;
using JDI.Light.Matchers;

namespace JDI.Light.Tests.Asserts
{
    public class Assert
    {
        private static readonly BaseMatcher Matcher = new Check();

        public static void Contains(string actual, string expected)
        {
            Matcher.Contains(actual, expected);
        }

        public static void IsTrue(bool condition)
        {
            Matcher.IsTrue(condition);
        }

        public static void IsFalse(bool condition)
        {
            Matcher.IsFalse(condition);
        }

        public static void CollectionEquals<T>(IEnumerable<T> actual, IEnumerable<T> expected)
        {
            Matcher.CollectionEquals(actual, expected);
        }

        public static void AreEquals<T>(T actual, T expected)
        {
            Matcher.AreEquals(actual, expected);
        }
    }
}