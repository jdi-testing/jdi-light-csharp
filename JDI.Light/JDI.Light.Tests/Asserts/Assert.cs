using System.Collections.Generic;
using JDI.Light.Matchers;

namespace JDI.Light.Tests.Asserts
{
    public class Assert
    {
        private static readonly BaseAsserter Asserter = new Check();

        public static void Contains(string actual, string expected)
        {
            Asserter.Contains(actual, expected);
        }

        public static void IsTrue(bool condition)
        {
            Asserter.IsTrue(condition);
        }

        public static void IsFalse(bool condition)
        {
            Asserter.IsFalse(condition);
        }

        public static void CollectionEquals<T>(IEnumerable<T> actual, IEnumerable<T> expected)
        {
            Asserter.CollectionEquals(actual, expected);
        }

        public static void AreEquals<T>(T actual, T expected)
        {
            Asserter.AreEquals(actual, expected);
        }
    }
}