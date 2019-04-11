using System.Collections.Generic;
using JDI.Light.Matchers.CollectionMatchers;
using JDI.Light.Matchers.IntegerMachers;

namespace JDI.Light.Matchers
{
    public static class Is
    {
        public static GreaterThanMatcher GreaterThan(int rightNumber) => GreaterThanMatcher.GreaterThan(rightNumber);

        public static GreaterThanOrEqualToMatcher GreaterThanOrEqualTo(int rightNumber) =>
            GreaterThanOrEqualToMatcher.GreaterThanOrEqualTo(rightNumber);

        public static LessThanMatcher LessThan(int rightNumber) => LessThanMatcher.LessThan(rightNumber);

        public static LessThanOrEqualToMatcher LessThanOrEqualTo(int rightNumber) => LessThanOrEqualToMatcher.LessThanOrEqualTo(rightNumber);

        public static SubsequenceOfMatcher<T> SubsequenceOf<T>(IEnumerable<T> rightSubsequence) =>
            SubsequenceOfMatcher<T>.SubsequenceOf(rightSubsequence);
    }
}
