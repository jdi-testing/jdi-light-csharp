using System;
using System.Collections.Generic;
using JDI.Core.Matchers;

namespace JDI.Matchers.NUnit
{
    public class Assert
    {
        private static readonly ScreenshotState Screen = ScreenshotState.OnFail;
        private static readonly BaseMatcher Matcher = new Check().SetScreenshot(Screen);

        public static void Contains(string actual, string expected)
        {
            Matcher.Contains(actual, expected);
        }

        public static void Matches(string actual, string regEx)
        {
            Matcher.Matches(actual, regEx);
        }

        public static void IsTrue(bool condition)
        {
            Matcher.IsTrue(condition);
        }

        public static void IsFalse(bool condition)
        {
            Matcher.IsFalse(condition);
        }

        public static BaseMatcher IgnoreCase()
        {
            return Matcher.IgnoreCase();
        }

        public static BaseMatcher.ListChecker<T> Each<T>(IEnumerable<T> collection)
        {
            // TODO: Need to use interface instead of ListChecker<T> ???
            return Matcher.EachElementOf(collection);
        }

        public static void CollectionEquals<T>(IEnumerable<T> actual, IEnumerable<T> expected)
        {
            Matcher.CollectionEquals(actual, expected);
        }

        public static void IsSortedByAsc(IEnumerable<int> collection)
        {
            Matcher.IsSortedByAsc(collection);
        }

        public static void IsSortedByDesc(IEnumerable<int> collection)
        {
            Matcher.IsSortedByDesc(collection);
        }

        public static void AreEquals<T>(Func<T> actualFunc, T expected)
        {
            Matcher.AreEquals(actualFunc, expected);
        }

        public static void AreEquals<T>(T actual, T expected)
        {
            Matcher.AreEquals(actual, expected);
        }

        public static void Contains(Func<string> actualFunc, string expected)
        {
            Matcher.Contains(actualFunc, expected);
        }

        public static BaseMatcher WaitTimeout(long timeout)
        {
            return Matcher.SetTimeout(timeout);
        }

        public static void ThrowException(Action throwException, string exceptionMessage)
        {
            Matcher.ThrowException(throwException, null, exceptionMessage);
        }

        public static void ThrowException(Action throwException, Type exceptionType, string exceptionMessage = null)
        {
            Matcher.ThrowException(throwException, exceptionType, exceptionMessage);
        }

        public static void HasNoException(Action action)
        {
            Matcher.HasNoException(action);
        }
    }
}